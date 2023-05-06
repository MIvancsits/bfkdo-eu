using AdminApp.Services;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace AdminApp.Pages
{
    /// <summary>
    /// Dashboard
    /// </summary>
    public partial class Dashboard
    {
        //private readonly long maxFileSize = 1024 * 1024 * 15;

        /// <summary>
        /// Navigation Manager.
        /// </summary>
        [Inject]
        public NavigationManager Nav { get; set; } = null!;

        /// <summary>
        ///     Authentifizierungs Service.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        /// <summary>
        /// Kommunikations Service.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        /// <summary>
        ///     Snackbar Service.
        /// </summary>
        [Inject]
        public ISnackbar SnackbarService { get; set; } = null!;

        /// <summary>
        /// Test-String f�rs Bytes auslesen.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            if (!AuthenticationStateService.HasJwtAuthentication())
            {
                Nav.NavigateTo("/");
                SnackbarService.Add("Bitte loggen Sie sich zuerst ein!", Severity.Error);
            }

            base.OnInitialized();
        }

        private void NavigateToDetails()
        {
            Nav?.NavigateTo("/knowledgetestdetails/4");
        }

        ///// <summary>
        ///// Methode f�r den Upload von Files.
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //private async Task UploadFile(IBrowserFile file)
        //{
        //    if (file != null)
        //    {
        //        var buffer = new byte[file.Size];
        //        _ = await file.OpenReadStream(maxFileSize).ReadAsync(buffer);
        //        var result = await Service.PostRegistrationsFromFile(buffer);
        //        Message = $"{result.RequestEnum} Bytes aus dem File gelesen!";
        //    }
        //}
    }
}