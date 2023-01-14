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
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using SSDB.Application.Features.RegistrationInfo.Queries;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;
using SSDB.Client.Infrastructure.Managers.Catalog.RegistrationInfo;
using SSDB.Application.Requests.Catalog.RegistrationInfo;

namespace SSDB.Client.Pages.Catalog.RegistrationInfo
{
    public partial class RegistrationInfo
    {
        [Inject] private IRegistrationInfoManager registrationInfoManager { get; set; }
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedRegistrationInfoResponse> _pagedData;
        private MudTable<GetAllPagedRegistrationInfoResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateRegistrationInfo;
        private bool _canEditRegistrationInfo;
        private bool _canDeleteRegistrationInfo;
        private bool _canExportRegistrationInfo;
        private bool _canSearchRegistrationInfo;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canExportRegistrationInfo = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.RegistrationInfo.Export)).Succeeded;
            _canSearchRegistrationInfo = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.RegistrationInfo.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetAllPagedRegistrationInfoResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedRegistrationInfoResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new GetAllPagedRegistrationInfoRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await registrationInfoManager.GetRegistrationInfosAsync(request);
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
            var response = await registrationInfoManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(RegistrationInfo).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["RegistrationInfo exported"]
                    : _localizer["Filtered RegistrationInfo exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }


    }
}