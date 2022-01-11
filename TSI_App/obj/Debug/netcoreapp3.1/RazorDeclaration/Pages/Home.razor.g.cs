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
#line 2 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Home.razor"
using TSI_App.JSON;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Home.razor"
using TSI_App.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Home.razor"
using TSI_App.Database;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Home.razor"
using Microsoft.Extensions.Hosting;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Home : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 13 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Home.razor"
       
    const string SPOTIFY_CLIENT_ID = "1a4dc6b858f844e386a120a023479599";

    private void spotifyLogin()
    {
        navigationManager.NavigateTo(
        "https://accounts.spotify.com/authorize?client_id=" + SPOTIFY_CLIENT_ID +
        "&response_type=code&redirect_uri=https%3A%2F%2Flocalhost%3A44355%2Fcallback&scope=user-read-currently-playing&state=34fFs29kd09");
    }

    private void loginUser()
    {
        navigationManager.NavigateTo("/login");
    }

    protected override async Task OnInitializedAsync()
    {
        base.OnInitializedAsync();

        //Save the new user's spotify tokens
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SpotifyLoginService spotifyService { get; set; }
    }
}
#pragma warning restore 1591