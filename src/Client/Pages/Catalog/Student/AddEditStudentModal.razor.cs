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
using SSDB.Application.Interfaces.Services;
using SSDB.Shared.Wrapper;
using SSDB.Client.Pages.Identity;
using SSDB.Domain.Enums;
using MudBlazor.Charts;

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
        private List<DropDownListItemResponse> _Departments = new();
        private List<DropDownListItemResponse> _Fuculties = new();
        private List<DropDownListItemResponse> _Semesters = new();
        private List<DropDownListItemResponse> _Batches = new();
        private List<DropDownListItemResponse> _Currencies = new();
        private List<DropDownListItemResponse> _Nationalities = new();
        private List<DropDownListItemResponse> _Programs = new();
        private List<DropDownListItemResponse> _Universities = new();

        private List<DropDownListItemResponse> _FilteredDepartments = new();
        private List<DropDownListItemResponse> _FilteredFuculties = new();
        private List<DropDownListItemResponse> _FilteredSemesters = new();
        private List<DropDownListItemResponse> _FilteredBatches = new();
        private List<DropDownListItemResponse> _FilteredCurrencies = new();
        private List<DropDownListItemResponse> _FilteredNationalities = new();
        private List<DropDownListItemResponse> _FilteredPrograms = new();
        private List<DropDownListItemResponse> _FilteredUniversities = new();
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

        private async Task getBatchFeesInfo(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            DropDownListItemResponse selectedItem = _Batches.FirstOrDefault(x => x.Key?.Split('|')[0] == value);
            if (selectedItem != null)
            {
                AddEditStudentModel.RegistrationFees = decimal.Parse(selectedItem?.Key.Split('|')[1].ToString());
                AddEditStudentModel.StudyFees = decimal.Parse(selectedItem?.Key.Split('|')[2].ToString());
                AddEditStudentModel.BatchId = int.Parse(selectedItem.Value);
                _FilteredSemesters = await GetDropDownListDataAsync(ListType.Semesters, AddEditStudentModel.BatchId);
            }
        }

        private async Task getUniversityInfo(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            DropDownListItemResponse selectedItem = _Universities.FirstOrDefault(x => x.Key == value);
            if (selectedItem != null)
            {
                _FilteredFuculties = await GetDropDownListDataAsync(ListType.Fuculties,int.Parse(selectedItem.Value));
                _FilteredCurrencies = await GetDropDownListDataAsync(ListType.Currency, int.Parse(selectedItem.Value));
                AddEditStudentModel.UniversityId = int.Parse(selectedItem.Value);
            }
        }
        private async Task getFucultiesInfo(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            DropDownListItemResponse selectedItem = _Fuculties.FirstOrDefault(x => x.Key == value);
            if (selectedItem != null)
            {
                _FilteredDepartments = await GetDropDownListDataAsync(ListType.Departments, int.Parse(selectedItem.Value));
                AddEditStudentModel.FucultyId = int.Parse(selectedItem.Value);
            }
        }

        private async Task getDepartmentsInfo(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
                

            DropDownListItemResponse selectedItem = _Departments.FirstOrDefault(x => x.Key == value);
            if (selectedItem != null)
            {
                _FilteredPrograms = await GetDropDownListDataAsync(ListType.Programs, int.Parse(selectedItem.Value));
                AddEditStudentModel.DepartmentId = int.Parse(selectedItem.Value);
            }
        }

        private async Task getProgramsInfo(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            DropDownListItemResponse selectedItem = _Programs.FirstOrDefault(x => x.Key == value);
            if (selectedItem != null)
            {
                _FilteredBatches = await GetDropDownListDataAsync(ListType.Batches, int.Parse(selectedItem.Value));
                AddEditStudentModel.ProgramId = int.Parse(selectedItem.Value);
            }
        }


        private async Task LoadDataAsync()
        {
            _Universities = await GetDropDownListDataAsync(ListType.Universities);
            _Fuculties = await GetDropDownListDataAsync(ListType.Fuculties);
            _Departments = await GetDropDownListDataAsync(ListType.Departments);
            _Semesters = await GetDropDownListDataAsync(ListType.Semesters);
            _Batches = await GetDropDownListDataAsync(ListType.Batches);
            _Currencies = await GetDropDownListDataAsync(ListType.Currency);
            _Nationalities = await GetDropDownListDataAsync(ListType.Nationalities);
            _Programs = await GetDropDownListDataAsync(ListType.Programs);

            _FilteredUniversities = _Universities;
            AddEditStudentModel.UniversityId = int.Parse(_Universities.FirstOrDefault().Value);
        }


        private async Task<IEnumerable<int>> SearchInUniversities(string value) => await SearchItemsInIntList(value, _Universities);
        private async Task<IEnumerable<int>> SearchInFuculties(string value) => await SearchItemsInIntList(value, _FilteredFuculties, AddEditStudentModel.UniversityId);
        private async Task<IEnumerable<int>> SearchInDepartments(string value) => await SearchItemsInIntList(value, _FilteredDepartments, AddEditStudentModel.FucultyId);
        private async Task<IEnumerable<int>> SearchInSemesters(string value) => await SearchItemsInIntList(value, _FilteredSemesters, AddEditStudentModel.BatchId);
        private async Task<IEnumerable<int>> SearchInBatches(string value) => await SearchItemsInIntList(value, _FilteredBatches, AddEditStudentModel.ProgramId);
        private async Task<IEnumerable<int>> SearchInCurrencies(string value) => await SearchItemsInIntList(value, _FilteredCurrencies, AddEditStudentModel.UniversityId);
        private async Task<IEnumerable<int>> SearchInNationalities(string value) => await SearchItemsInIntList(value, _Nationalities);
        private async Task<IEnumerable<int>> SearchInPrograms(string value) => await SearchItemsInIntList(value, _FilteredPrograms, AddEditStudentModel.FucultyId);

        private async Task<List<DropDownListItemResponse>> GetDropDownListDataAsync(ListType type,int reference=0)
        {
            var data = await utilitiesManager.GetDropDownListDataAsync(type);
            if (data.Succeeded)
            {
                if (reference != 0)
                {
                    return data.Data.Where(x => x.Reference == reference.ToString()).ToList();
                }
                return data.Data;
            }



            return new List<DropDownListItemResponse>();
        }


        private async Task<IEnumerable<int>> SearchItemsInIntList(string value, List<DropDownListItemResponse> list,int reference = 0)
        {
            if (string.IsNullOrEmpty(value))
                return await Task.FromResult(list.Select(x => int.Parse(x.Value)));

            var result =  await Task.FromResult(list.Where(x => x.Key.Contains(value, StringComparison.InvariantCultureIgnoreCase) )
                .Select(x => int.Parse(x.Value)));

            if(reference !=0)
            {
                result = await Task.FromResult(list.Where(x => x.Key.Contains(value, StringComparison.InvariantCultureIgnoreCase) && x.Reference == reference.ToString())
                .Select(x => int.Parse(x.Value)));
            }

            return result;
        }

    }
}