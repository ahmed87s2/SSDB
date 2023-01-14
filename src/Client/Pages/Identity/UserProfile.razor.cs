using SSDB.Application.Requests.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Enums;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Client.Infrastructure.Managers.Catalog.Utilities;
using System.Linq;
using System;

namespace SSDB.Client.Pages.Identity
{
    public partial class UserProfile
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }
        private List<DropDownListItemResponse> _Universities = new();
        [Inject] private IUtilitiesManager utilitiesManager { get; set; }
        private bool _active;
        private char _firstLetterOfName;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _email;
        private int _universityId;
        private string _universityName;
        private bool _loaded;

        private async Task ToggleUserStatus()
        {
            var request = new ToggleUserStatusRequest { ActivateUser = _active, UserId = Id };
            var result = await _userManager.ToggleUserStatusAsync(request);
            if (result.Succeeded)
            {
                _snackBar.Add(_localizer["Updated User Status."], Severity.Success);
                _navigationManager.NavigateTo("/identity/users");
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        [Parameter] public string ImageDataUrl { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var userId = Id;
            var result = await _userManager.GetAsync(userId);
            if (result.Succeeded)
            {
                _Universities = await GetDropDownListDataAsync(ListType.Universities);
                var user = result.Data;
                if (user != null)
                {
                    _firstName = user.FirstName;
                    _lastName = user.LastName;
                    _email = user.Email;
                    _phoneNumber = user.PhoneNumber;
                    _active = user.IsActive;
                    _universityId = user.University.Id;
                    _universityName = user.University.Name;
                    var data = await _accountManager.GetProfilePictureAsync(userId);
                    if (data.Succeeded)
                    {
                        ImageDataUrl = data.Data;
                    }
                }
                Title = $"{_firstName} {_lastName}'s {_localizer["Profile"]}";
                Description = _email;
                if (_firstName.Length > 0)
                {
                    _firstLetterOfName = _firstName[0];
                }
            }
            _loaded = true;
        }
        private async Task<IEnumerable<int>> SearchInUniversities(string value) => await SearchItemsInIntList(value, _Universities);

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