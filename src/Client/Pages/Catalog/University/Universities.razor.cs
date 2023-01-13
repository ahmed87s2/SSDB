using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Client.Extensions;
using SSDB.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SSDB.Application.Features.Universities.Commands;
using SSDB.Client.Infrastructure.Managers.Catalog.University;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace SSDB.Client.Pages.Catalog.University
{
    public partial class Universities
    {
        [Inject] private IUniversityManager UniversityManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllUniversitiesResponse> _UniversityList = new();
        private GetAllUniversitiesResponse _University = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateUniversities;
        private bool _canEditUniversities;
        private bool _canDeleteUniversities;
        private bool _canExportUniversities;
        private bool _canSearchUniversities;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateUniversities = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Universities.Create)).Succeeded;
            _canEditUniversities = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Universities.Edit)).Succeeded;
            _canDeleteUniversities = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Universities.Delete)).Succeeded;
            _canExportUniversities = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Universities.Export)).Succeeded;
            _canSearchUniversities = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Universities.Search)).Succeeded;

            await GetUniversitiesAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetUniversitiesAsync()
        {
            var response = await UniversityManager.GetAllAsync();
            if (response.Succeeded)
            {
                _UniversityList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
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
                var response = await UniversityManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task ExportToExcel()
        {
            var response = await UniversityManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Universities).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Universities exported"]
                    : _localizer["Filtered Universities exported"], Severity.Success);
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
                _University = _UniversityList.FirstOrDefault(c => c.Id == id);
                if (_University != null)
                {
                    parameters.Add(nameof(AddEditUniversityModal.AddEditUniversityModel), new AddEditUniversityCommand
                    {
                        Id = _University.Id,
                        Name = _University.Name,
                        Type = _University.Type,
                        IsActive = _University.IsActive
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditUniversityModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _University = new GetAllUniversitiesResponse();
            await GetUniversitiesAsync();
        }

        private bool Search(GetAllUniversitiesResponse University)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (University.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (University.Type?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}