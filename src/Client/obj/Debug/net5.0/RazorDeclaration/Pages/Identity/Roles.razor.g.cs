// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace SSDB.Client.Pages.Identity
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 2 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using Blazored.FluentValidation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Identity.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Identity.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Identity.Roles;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Identity.RoleClaims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Identity.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Preferences;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Interceptors;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Catalog.Student;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Catalog.University;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Dashboard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Communication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Audit;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Misc.Document;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Managers.Misc.DocumentType;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Shared.Constants.Permission;

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Shared.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Shared.Dialogs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Application.Requests.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Infrastructure.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
using SSDB.Client.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\WorkSpace\SSDB_Universities\src\Client\Pages\Identity\Roles.razor"
using SSDB.Application.Responses.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\WorkSpace\SSDB_Universities\src\Client\Pages\Identity\Roles.razor"
using System.ComponentModel.Design;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "D:\WorkSpace\SSDB_Universities\src\Client\_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\WorkSpace\SSDB_Universities\src\Client\Pages\Identity\Roles.razor"
           [Authorize(Policy = Permissions.Roles.View)]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/identity/roles")]
    public partial class Roles : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.Extensions.Localization.IStringLocalizer<Roles> _localizer { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime _jsRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILocalStorageService _localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IUserManager _userManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ClientPreferenceManager _clientPreferenceManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IHttpInterceptorManager _interceptor { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient _httpClient { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IDialogService _dialogService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ISnackbar _snackBar { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAuthorizationService _authorizationService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BlazorHeroStateProvider _stateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager _navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAccountManager _accountManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAuthenticationManager _authenticationManager { get; set; }
    }
}
#pragma warning restore 1591
