#pragma checksum "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99c5556bbeac11822dba879902d2e9641d2ff74d"
// <auto-generated/>
#pragma warning disable 1591
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
#line 2 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
using TSI_App.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public partial class Login : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddMarkupContent(1, "\r\n    ");
            __builder.AddMarkupContent(2, "<h1>Login</h1>\r\n    Email    ");
            __builder.OpenElement(3, "input");
            __builder.AddAttribute(4, "type", "text");
            __builder.AddAttribute(5, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 10 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                           email

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => email = __value, email));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(7, "<br>\r\n    Password ");
            __builder.OpenElement(8, "input");
            __builder.AddAttribute(9, "type", "password");
            __builder.AddAttribute(10, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 11 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                           password

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => password = __value, password));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "<br>\r\n    ");
            __builder.OpenElement(13, "button");
            __builder.AddAttribute(14, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 12 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                      loginUser

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(15, "Login");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n    <br>\r\n\r\n    ");
            __builder.AddMarkupContent(17, "<h1>Create New User</h1>\r\n    Email            ");
            __builder.OpenElement(18, "input");
            __builder.AddAttribute(19, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 16 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                                   newEmail

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(20, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => newEmail = __value, newEmail));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "<br>\r\n    Twitch Username  ");
            __builder.OpenElement(22, "input");
            __builder.AddAttribute(23, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 17 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                                   twitchUsername

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => twitchUsername = __value, twitchUsername));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "<br>\r\n    Password         ");
            __builder.OpenElement(26, "input");
            __builder.AddAttribute(27, "type", "password");
            __builder.AddAttribute(28, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 18 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                                                   newPassword

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(29, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => newPassword = __value, newPassword));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "<br>\r\n    Confirm Password ");
            __builder.OpenElement(31, "input");
            __builder.AddAttribute(32, "type", "password");
            __builder.AddAttribute(33, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 19 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                                                   confirmPassword

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(34, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => confirmPassword = __value, confirmPassword));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "<br>\r\n    ");
            __builder.OpenElement(36, "button");
            __builder.AddAttribute(37, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 20 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                      createNewUser

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(38, "Create");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n    ");
            __builder.OpenElement(40, "p");
            __builder.AddAttribute(41, "style", "color:red");
            __builder.AddContent(42, 
#nullable restore
#line 21 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
                          displayError

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 24 "C:\Users\caleb\source\repos\TSI_App\TSI_App\Pages\Login.razor"
       
        const string SPOTIFY_CLIENT_ID = "1a4dc6b858f844e386a120a023479599";

    private string email;
    private string password;

    private string newEmail;
    private string twitchUsername;
    private string newPassword;
    private string confirmPassword;
    private string displayError = "";

    private async void createNewUser()
    {
        displayError = "";
        if (!String.IsNullOrWhiteSpace(newPassword) && !String.IsNullOrWhiteSpace(confirmPassword))
        {
            if (newPassword == confirmPassword)
            {
                int id = loginService.AddUser(newEmail, newPassword, twitchUsername);
                if (id > 0)
                {
                    await localStorage.SetItemAsync<int>("ID", id);
                    await localStorage.SetItemAsync<string>("Email", email);
                    spotifyLogin();
                }
                else
                    displayError = "Error creating new user!";
            }
        }
    }

    private void spotifyLogin()
    {
        navigationManager.NavigateTo(
        "https://accounts.spotify.com/authorize?client_id=" + SPOTIFY_CLIENT_ID +
        "&response_type=code&redirect_uri=https%3A%2F%2Flocalhost%3A44355%2Fcallback&scope=user-read-currently-playing&state=34fFs29kd09");
    }

    private async void loginUser()
    {
        if (loginService.VerifyPassword(password, email))
        {
            int id = loginService.FindUser(email);
            await localStorage.SetItemAsync<int>("ID", id);
            await localStorage.SetItemAsync<string>("Email", email);

            navigationManager.NavigateTo("/");
        }
        else
        {
            displayError = "Incorrect username or password! Try again";
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591
