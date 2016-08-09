using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.ApplicationModel.Activation;
using Windows.Security.Credentials;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Authentication.Web;

using Template10.Controls;

using Microsoft.ApplicationInsights;

using Office365Service.Excel;
using Office365Service.User;
using Office365Service.OneDrive;

using ExcelServiceExplorer.Views;
using Microsoft.Identity.Client;

namespace ExcelServiceExplorer
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {

        // To authenticate to the directory Graph, the client needs to know its App ID URI.
        public const string Resource = "https://graph.microsoft.com";

        private const string UserApiVersion = "v1.0";
        private const string OneDriveApiVersion = "v1.0";
        private const string ExcelApiVersion = "v1.0";

        public static User UserAccount = null;

        // User Service Settings
        public static UserService UserService =
                new UserService(
                        async () =>
                        {

                            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

                            if (accessToken != null)
                            {
                                return accessToken;
                            }
                            else
                            {
                                throw new Exception("We tried to get a token for the Graph as the account you are currently signed in, but it didn't work out. Please sign in as a different user.");
                            }
                        }
                )
                {
                    Url = $"{Resource}/{UserApiVersion}"
                };


        // User Service Settings
        public static OneDriveService OneDriveService =
                new OneDriveService(
                        async () =>
                        {

                            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

                            if (accessToken != null)
                            {
                                return accessToken;
                            }
                            else
                            {
                                throw new Exception("We tried to get a token for the Graph as the account you are currently signed in, but it didn't work out. Please sign in as a different user.");
                            }
                        }
                )
                {
                    Url = $"{Resource}/{OneDriveApiVersion}"
                };

        // Excel Service Settings
        public static ExcelRESTService ExcelService =
            new ExcelRESTService(
                        async () =>
                        {

                            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

                            if (accessToken != null)
                            {
                                return accessToken;
                            }
                            else
                            {
                                throw new Exception("We tried to get a token for the Graph as the account you are currently signed in, but it didn't work out. Please sign in as a different user.");
                            }
                        }
            )
            {
                Url = $"{Resource}/{ExcelApiVersion}"
            };

        public App()
            {
            // Initialize telemetry
            WindowsAppInitializer.InitializeAsync();

            InitializeComponent();
            SplashFactory = (e) => new Splash(e);
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // content may already be shell when resuming
            if ((Window.Current.Content as ModalDialog) == null)
            {
                // setup hamburger shell inside a modal dialog
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Shell(nav)
                };
            }
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here

            NavigationService.Navigate(typeof(MainPage));
            await Task.CompletedTask;
        }
    }
}

