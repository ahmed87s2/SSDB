using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Requests;
using SSDB.Client.Extensions;
using SSDB.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using SSDB.Client.Infrastructure.Managers.Catalog.University;
using SSDB.Client.Infrastructure.Managers.Catalog.Registration;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Application.Enums;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Client.Pages.Catalog.Registration
{
    public partial class AddEditRegistrationModal
    {
        [Inject] private IRegistrationManager RegistrationManager { get; set; }
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }

        [Parameter] public AddEditRegistrationCommand AddEditRegistrationModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<DropDownListItemResponse> _Students = new();
        private List<DropDownListItemResponse> _Semesters = new();
        private List<DropDownListItemResponse> _Currencies = new();
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await RegistrationManager.SaveAsync(AddEditRegistrationModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            _Students = await GetDropDownListDataAsync(ListType.Students);
            _Semesters = await GetDropDownListDataAsync(ListType.Semesters);
            _Currencies = await GetDropDownListDataAsync(ListType.Currency);
        }

        private async Task<IEnumerable<int>> SearchInSemesters(string value) => await SearchItemsInIntList(value, _Semesters);
        private async Task<IEnumerable<int>> SearchInCurrencies(string value) => await SearchItemsInIntList(value, _Currencies);
        private async Task<IEnumerable<string>> SearchInStudents(string value) => await SearchItemsInStringList(value, _Students);
        private async Task<List<DropDownListItemResponse>> GetDropDownListDataAsync(ListType type)
        {
            var data = await utilitiesManager.GetDropDownListDataAsync(type);
            if (data.Succeeded)
            {
                return data.Data;
            }
            return new List<DropDownListItemResponse>();
        }

        private async Task<IEnumerable<int>> SearchItemsInIntList(string value, List<DropDownListItemResponse> list)
        {
            if (string.IsNullOrEmpty(value))
                return await Task.FromResult(list.Select(x => int.Parse(x.Value)));

            return await Task.FromResult(list.Where(x => x.Key.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => int.Parse(x.Value)));
        }

        private async Task<IEnumerable<string>> SearchItemsInStringList(string value, List<DropDownListItemResponse> list)
        {
            if (string.IsNullOrEmpty(value))
                return await Task.FromResult(list.Select(x => x.Value));

            return await Task.FromResult(list.Where(x => x.Key.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Value));
        }


    }
}