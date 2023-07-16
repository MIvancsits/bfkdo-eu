using BenutzerApp.Services;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BenutzerApp.Pages.Login
{
    /// <summary>
    ///     Login Komponente.
    /// </summary>
    public partial class ParticipantLogin
    {
        /// <summary>
        ///     Initialisierungmethode.
        /// </summary>
        protected override void OnInitialized()
        {
            //if (AuthenticationStateService.IsLoggedIn())
            //{
            //    NavigationManager.NavigateTo("/dashboard");
            //}

            base.OnInitialized();
        }

        /// <summary>
        /// Service f�r die Verwaltung und �berwachung des Authorisierungsstatus.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;


        /// <summary>
        /// Service f�r die Authorisierung.
        /// </summary>
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        /// <summary>
        /// SybosID
        /// </summary>
        public string SybosID { get; set; } = string.Empty;

        /// <summary>
        ///     Passwort.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Boolean der Auskunft dar�ber gibt, ob die Applikation gerade eine Verarbeitung durchf�hrt.
        /// </summary>
        public bool Processing { get; set; } = false;

        /// <summary>
        /// Loginmethode.
        /// </summary>
        public async Task Login()
        {
            if (!string.IsNullOrEmpty(SybosID) && !string.IsNullOrEmpty(Password))
            {
                Processing = true;
                var response = await AuthService.AuthParticipant(new ModelParticipantAuthData(int.Parse(SybosID), Password));
                Processing = false;

                StateHasChanged();

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    await AuthService.SetParticipantContextId(int.Parse(SybosID));
                    MudSnackbar.Add("Login erfolgreich!", Severity.Success);
                    NavigationManager.NavigateTo("/participant/dashboard");
                }
                else
                {
                    MudSnackbar.Add("Ung�ltige SybosID oder Passwort! Bitte versuchen Sie es erneut!", Severity.Error);
                }

            }
            else
            {
                MudSnackbar.Add("Ung�ltige SybosID oder Passwort! Bitte versuchen Sie es erneut!", Severity.Error);
            }
        }

        /// <summary>
        /// Methode f�r die Weiterverarbeitung eines gescannten QR Codes.
        /// </summary>
        /// <param name="args"></param>
        private async Task QRScanned(string args)
        {
            var credentials = args.Split();
            try
            {
                Processing = true;
                var response = await AuthService.AuthParticipant(new ModelParticipantAuthData(int.Parse(credentials[0]), credentials[1]));
                Processing = false;

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    SybosID = credentials[0];
                    await AuthService.SetParticipantContextId(int.Parse(SybosID));
                    MudSnackbar.Add("Login erfolgreich!", Severity.Success);
                    NavigationManager.NavigateTo("/participant/dashboard");
                }
                else
                {
                    Console.WriteLine($"Fehlerhafter QR-Code: {args}");
                    MudSnackbar.Add("Ung�ltiger QR-Code! Bitte versuchen Sie es erneut!", Severity.Error);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fehlerhafter QR-Code: {args}");
                MudSnackbar.Add("Ung�ltiger QR-Code! Bitte versuchen Sie es erneut!", Severity.Error);
            }
        }

        /// <summary>
        ///     Properties f�r die Passwortanzeige-Funktion.
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InputType PasswordInput { get; set; } = InputType.Password;
        /// <summary>
        /// 
        /// </summary>
        public string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

        /// <summary>
        ///     Methode zum Anzeigen oder Verbergen des Passworts.
        /// </summary>
        private void ShowPassword()
        {
            if (IsShow)
            {
                IsShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                IsShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}