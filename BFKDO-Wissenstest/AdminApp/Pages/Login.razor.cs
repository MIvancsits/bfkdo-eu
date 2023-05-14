using Microsoft.AspNetCore.Components;
using Common.Model;
using AdminApp.Services;
using MudBlazor;
using Common.Enums;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Login Komponente.
    /// </summary>
    public partial class Login
    {
        /// <summary>
        ///     Authentication Service.
        /// </summary>
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        /// <summary>
        ///     F�r den Login ben�tigte Properties: Email und Passwort.
        /// </summary>
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     Loginfunktion.
        /// </summary>
        ///
        public async void LoginUser()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                var response = await AuthService.Auth(new ModelAdminAuthData(Email, Password));

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    NavigationManager.NavigateTo("/dashboard");
                }

                else
                {
                    MudSnackbar.Add("Invalid email or password. Please try again.", Severity.Error);
                }
            }
        }

        /// <summary>
        ///     Properties f�r die Passwortanzeige-Funktion.
        /// </summary>
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        /// <summary>
        ///     Methode zum Anzeigen oder Verbergen des Passworts.
        /// </summary>
        void ButtonTestclick()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}