using SSDB.Application.Requests.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using SSDB.Application.Enums;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;
using System;

namespace SSDB.Client.Pages.Identity
{
    public partial class RegisterUserModal
    {
        private FluentValidationValidator _fluentValidationValidator;
        
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly RegisterRequest _registerUserModel = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private List<DropDownListItemResponse> _Universities = new();
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }
        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task<IEnumerable<int>> SearchInUniversities(string value) => await SearchItemsInIntList(value, _Universities);
        protected override async Task OnInitializedAsync()
        {
            _Universities = await GetDropDownListDataAsync(ListType.Universities);
        }
        private async Task SubmitAsync()
        {
            var response = await _userManager.RegisterUserAsync(_registerUserModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
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

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
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