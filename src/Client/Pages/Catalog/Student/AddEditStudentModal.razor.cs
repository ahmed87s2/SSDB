using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Application.Features.Students.Commands;
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
using SSDB.Client.Infrastructure.Managers.Catalog.Student;
using SSDB.Application.Features.Utilities.Queries;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;
using SSDB.Application.Enums;

namespace SSDB.Client.Pages.Catalog.Student
{
    public partial class AddEditStudentModal
    {
        [Inject] private IStudentManager StudentManager { get; set; }
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }

        [Parameter] public AddEditStudentCommand AddEditStudentModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<DropDownListItemResponse> _Admissions = new();
        private List<DropDownListItemResponse> _Departments = new();
        private List<DropDownListItemResponse> _Fuculties = new();
        private List<DropDownListItemResponse> _Semesters = new();
        private List<DropDownListItemResponse> _Batches = new();
        private List<DropDownListItemResponse> _Currencies = new();
        private List<DropDownListItemResponse> _Nationalities = new();
        private List<DropDownListItemResponse> _Programs = new();
        private List<DropDownListItemResponse> _Specializations = new();
        private List<DropDownListItemResponse> _Degrees = new();
        decimal totalFees;
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await StudentManager.SaveAsync(AddEditStudentModel);
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

        public decimal getTotal()
        {
            totalFees = AddEditStudentModel.MedicalFees + AddEditStudentModel.RegistrationFees + AddEditStudentModel.StudyFees + AddEditStudentModel.Panalty;
            return totalFees;
        }

        private void getBatchFeesInfo(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            DropDownListItemResponse selectedItem = _Batches.FirstOrDefault(x => x.Key?.Split('|')[0] == value);
            if (selectedItem != null)
            {
                AddEditStudentModel.RegistrationFees = decimal.Parse(selectedItem?.Key.Split('|')[1].ToString());
                AddEditStudentModel.StudyFees = decimal.Parse(selectedItem?.Key.Split('|')[2].ToString());
            }
            


        }
        private async Task LoadDataAsync()
        {
            _Fuculties = await GetDropDownListDataAsync(ListType.Fuculties);
            _Admissions = await GetDropDownListDataAsync(ListType.Addmission);
            _Departments = await GetDropDownListDataAsync(ListType.Departments);
            _Semesters = await GetDropDownListDataAsync(ListType.Semesters);
            _Batches = await GetDropDownListDataAsync(ListType.Batches);
            _Currencies = await GetDropDownListDataAsync(ListType.Currency);
            _Nationalities = await GetDropDownListDataAsync(ListType.Nationalities);
            _Programs = await GetDropDownListDataAsync(ListType.Programs);
            _Specializations = await GetDropDownListDataAsync(ListType.Specializations);
            _Degrees = await GetDropDownListDataAsync(ListType.Degrees);
        }
        private async Task<IEnumerable<int>> SearchInFuculties(string value) => await SearchItemsInIntList(value, _Fuculties);
        private async Task<IEnumerable<int>> SearchInAddmissions(string value) => await SearchItemsInIntList(value, _Admissions);
        private async Task<IEnumerable<int>> SearchInDepartments(string value) => await SearchItemsInIntList(value, _Departments);
        private async Task<IEnumerable<int>> SearchInSemesters(string value) => await SearchItemsInIntList(value, _Semesters);
        private async Task<IEnumerable<int>> SearchInBatches(string value) => await SearchItemsInIntList(value, _Batches);
        private async Task<IEnumerable<int>> SearchInCurrencies(string value) => await SearchItemsInIntList(value, _Currencies);
        private async Task<IEnumerable<int>> SearchInNationalities(string value) => await SearchItemsInIntList(value, _Nationalities);
        private async Task<IEnumerable<int>> SearchInPrograms(string value) => await SearchItemsInIntList(value, _Programs);
        private async Task<IEnumerable<int>> SearchInSpecializations(string value) => await SearchItemsInIntList(value, _Specializations);
        private async Task<IEnumerable<int>> SearchInDegrees(string value) => await SearchItemsInIntList(value, _Degrees);

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

    }
}