using Microsoft.AspNetCore.Components;
using Common.Model;
using AdminApp.Services;
using MudBlazor;
using Common.Enums;
using Common.Services;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Login Komponente.
    /// </summary>
    public partial class Login
    {
        /// <summary>
        ///     Initialisierungmethode.
        /// </summary>
        protected override void OnInitialized()
        {
            if (AuthenticationStateService.IsLoggedIn())
            {
                NavigationManager.NavigateTo("/dashboard");
            }
            base.OnInitialized();
        }

        /// <summary>
        ///     Authentifizierungstatusservice.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        /// <summary>
        ///     Authentication Service.
        /// </summary>
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        /// <summary>
        ///     Email Addresse.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     Passwort.
        /// </summary>
        public string Password { get; set; } = string.Empty;

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
                    MudSnackbar.Add("Ung�ltiger Benutzername oder Passwort. Bitte versuchen Sie es erneut.", Severity.Error);
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