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

namespace ExcelServiceExplorer
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {

        // Azure AD Authentication Settings

        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        public const string ClientId = "67c64841-9567-4a6b-aec3-e34e7677ee9d"; // PROD

        // const string tenant = "yourtenant.onmicrosoft.com";
        // const string authority = "https://login.microsoftonline.com/" + tenant;
        public const string Authority = "organizations";

        // To authenticate to the directory Graph, the client needs to know its App ID URI.
        public const string Resource = "https://graph.microsoft.com";

        private const string UserApiVersion = "v1.0";
        private const string OneDriveApiVersion = "v1.0";
        private const string ExcelApiVersion = "v1.0";

        // Windows10 universal apps require redirect URI in the format below
        public string RedirectURI = string.Format("ms-appx-web://Microsoft.AAD.BrokerPlugIn/{0}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper());

        public static WebAccountProvider WAP = null;
        public static WebAccount UserAccount = null;

        // User Service Settings
        public static UserService UserService =
                new UserService(
                        async () =>
                        {
                            // Craft the token request for the Graph api
                            WebTokenRequest wtr = new WebTokenRequest(WAP, string.Empty, ClientId);
                            wtr.Properties.Add("resource", Resource);

                            // Perform the token request without showing any UX
                            WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(wtr, UserAccount);
                            if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
                            {
                                return wtrr.ResponseData[0].Token;
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
                            // Craft the token request for the Graph api
                            WebTokenRequest wtr = new WebTokenRequest(WAP, string.Empty, ClientId);
                            wtr.Properties.Add("resource", Resource);

                            // Perform the token request without showing any UX
                            WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(wtr, UserAccount);
                            if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
                            {
                                return wtrr.ResponseData[0].Token;
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
                        // Craft the token request for the Graph api
                        WebTokenRequest wtr = new WebTokenRequest(WAP, string.Empty, ClientId);
                        wtr.Properties.Add("resource", Resource);

                        // Perform the token request without showing any UX
                        WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(wtr, UserAccount);
                        if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
                        {
                            return wtrr.ResponseData[0].Token;
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

