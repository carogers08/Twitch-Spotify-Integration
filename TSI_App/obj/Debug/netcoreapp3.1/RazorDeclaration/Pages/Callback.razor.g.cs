// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace TSI_App.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using TSI_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\caleb\source\repos\TSI_App\TSI_App\_Imports.razor"
using TSI_App.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Callback.razor"
using TSI_App.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Callback.razor"
using TSI_App.Database;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Callback.razor"
using TSI_App.JSON;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/callback")]
    public partial class Callback : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 19 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Callback.razor"
       
    private bool loginSuccess = false;

    protected override async Task OnInitializedAsync()
    {
        base.OnInitializedAsync();

        string url = navigationManager.ToAbsoluteUri(navigationManager.Uri).ToString();
        if (url.Contains("error"))
        {
            loginSuccess = false;
            //do something
        }
        else
        {
            loginSuccess = true;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            SpotifyTokenResponse tokens = await spotifyService.GetTokenAsync(navigationManager.ToAbsoluteUri(navigationManager.Uri).ToString());
            var ID = await localStorage.GetItemAsync<int>("ID");
            spotifyService.SaveTokens(tokens, ID);
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SpotifyLoginService spotifyService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
    }
}
#pragma warning restore 1591
