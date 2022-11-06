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
using SSDB.Application.Features.Registrations.Commands;
using SSDB.Client.Infrastructure.Managers.Catalog.Registration;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;

namespace SSDB.Client.Pages.Catalog.Registration
{
    public partial class Registrations
    {
        [Inject] private IRegistrationManager RegistrationManager { get; set; }
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedRegistrationsResponse> _pagedData;
        private MudTable<GetAllPagedRegistrationsResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateRegistrations;
        private bool _canEditRegistrations;
        private bool _canDeleteRegistrations;
        private bool _canExportRegistrations;
        private bool _canSearchRegistrations;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateRegistrations = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Registrations.Create)).Succeeded;
            _canEditRegistrations = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Registrations.Edit)).Succeeded;
            _canDeleteRegistrations = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Registrations.Delete)).Succeeded;
            _canExportRegistrations = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Registrations.Export)).Succeeded;
            _canSearchRegistrations = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Registrations.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetAllPagedRegistrationsResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedRegistrationsResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new GetAllPagedRegistrationsRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await RegistrationManager.GetRegistrationsAsync(request);
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
            var response = await RegistrationManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Registrations).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Registrations exported"]
                    : _localizer["Filtered Registrations exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var Registration = _pagedData.FirstOrDefault(c => c.Id == id);
                if (Registration != null)
                {
                    var registrationModelData = await RegistrationManager.GetForAddEdit(id);
                    parameters.Add(nameof(AddEditRegistrationModal.AddEditRegistrationModel), registrationModelData.Data);
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditRegistrationModal>(id == null ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

    }
}