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
using SSDB.Application.Features.Payments.Commands;
using SSDB.Client.Infrastructure.Managers.Catalog.Payment;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using SSDB.Application.Features.Payments.Queries;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;

namespace SSDB.Client.Pages.Catalog.Payment
{
    public partial class Payments
    {
        [Inject] private IPaymentManager PaymentManager { get; set; }
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedPaymentsResponse> _pagedData;
        private MudTable<GetAllPagedPaymentsResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreatePayments;
        private bool _canEditPayments;
        private bool _canDeletePayments;
        private bool _canExportPayments;
        private bool _canSearchPayments;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreatePayments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Payments.Create)).Succeeded;
            _canEditPayments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Payments.Edit)).Succeeded;
            _canDeletePayments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Payments.Delete)).Succeeded;
            _canExportPayments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Payments.Export)).Succeeded;
            _canSearchPayments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Payments.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetAllPagedPaymentsResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedPaymentsResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new GetAllPagedPaymentsRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await PaymentManager.GetPaymentsAsync(request);
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
            var response = await PaymentManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Payments).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Payments exported"]
                    : _localizer["Filtered Payments exported"], Severity.Success);
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