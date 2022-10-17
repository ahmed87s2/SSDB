using SSDB.Application.Features.Students.Queries.GetAllPaged;
using SSDB.Application.Requests.Catalog;
using SSDB.Client.Extensions;
using SSDB.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SSDB.Application.Features.Students.Commands.AddEdit;
using SSDB.Client.Infrastructure.Managers.Catalog.Student;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;

namespace SSDB.Client.Pages.Catalog
{
    public partial class Students
    {
        [Inject] private IStudentManager StudentManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedStudentsResponse> _pagedData;
        private MudTable<GetAllPagedStudentsResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateStudents;
        private bool _canEditStudents;
        private bool _canDeleteStudents;
        private bool _canExportStudents;
        private bool _canSearchStudents;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateStudents = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Students.Create)).Succeeded;
            _canEditStudents = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Students.Edit)).Succeeded;
            _canDeleteStudents = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Students.Delete)).Succeeded;
            _canExportStudents = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Students.Export)).Succeeded;
            _canSearchStudents = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Students.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetAllPagedStudentsResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedStudentsResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] {$"{state.SortLabel} {state.SortDirection}"} : new[] {$"{state.SortLabel}"};
            }

            var request = new GetAllPagedStudentsRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await StudentManager.GetStudentsAsync(request);
            if (response.Succeeded)
            {
                _totalItems = response.TotalCount;
                _currentPage = response.CurrentPage;
                _pagedData = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            var response = await StudentManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Students).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Students exported"]
                    : _localizer["Filtered Students exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var Student = _pagedData.FirstOrDefault(c => c.Id == id);
                if (Student != null)
                {
                    parameters.Add(nameof(AddEditStudentModal.AddEditStudentModel), new AddEditStudentCommand
                    {
                        Id = Student.Id,
                        Name = Student.Name,
                        Description = Student.Description,
                        Rate = Student.Rate,
                        UniversityId = Student.UniversityId,
                        Barcode = Student.Barcode
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditStudentModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await StudentManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    OnSearch("");
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    OnSearch("");
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }
    }
}