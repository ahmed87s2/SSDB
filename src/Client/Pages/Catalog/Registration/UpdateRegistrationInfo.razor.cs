using SSDB.Application.Requests.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Enums;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Components;
using SSDB.Client.Infrastructure.Managers.Catalog.Registration;

namespace SSDB.Client.Pages.Catalog.Registration
{
    public partial class UpdateRegistrationInfo
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private UpdateRegistrationInfoCommand _command = new();
        private List<DropDownListItemResponse> _Universities = new();
        private List<DropDownListItemResponse> _Students = new();
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }
        [Inject] private IRegistrationManager registrationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _Universities = await GetDropDownListDataAsync(ListType.Universities);
            _Students = await GetDropDownListDataAsync(ListType.Students);
        }

        private async Task SubmitAsync()
        {
            var result = await registrationManager.UpdateRegistrationAsync(_command);
            if (result.Succeeded)
            {
                _snackBar.Add("Successfull, "+result.Data, Severity.Success);
                _navigationManager.NavigateTo("/catalog/Registrations", true);
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task<IEnumerable<int>> SearchInUniversities(string value) => await SearchItemsInIntList(value, _Universities);
        private async Task<IEnumerable<string>> SearchInStudents(string value)
        {
            if (string.IsNullOrEmpty(value))
                return await Task.FromResult(_Students.Select(x => x.Value));

            return await Task.FromResult(_Students.Where(x => x.Key.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Value));
        }

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