/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.Popups;

using Office365Service.User;
using Office365Service.OneDrive;
using Office365Service.Excel;
using Microsoft.Identity.Client;

namespace ExcelRESTService.UnitTests.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        // Azure AD Authentication and API Settings
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        //public const string ClientId = "67c64841-9567-4a6b-aec3-e34e7677ee9d"; // PROD

        // const string tenant = "yourtenant.onmicrosoft.com";
        // const string authority = "https://login.microsoftonline.com/" + tenant;
        //public const string Authority = "organizations";

        // To authenticate to the directory Graph, the client needs to know its App ID URI.
        public const string Resource = "https://graph.microsoft.com";

        private const string UserApiVersion = "v1.0";
        private const string OneDriveApiVersion = "v1.0";
        private const string ExcelApiVersion = "v1.0";

        // Windows10 universal apps require redirect URI in the format below
        //public string RedirectURI = string.Format("ms-appx-web://Microsoft.AAD.BrokerPlugIn/{0}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper());

        //public static WebAccountProvider WAP = null;
        public static User UserAccount = null;

        // User Service Settings
        public static UserService UserService =
                new UserService(
                        async () =>
                        {
                            // Craft the token request for the Graph api
                            //WebTokenRequest wtr = new WebTokenRequest(WAP, string.Empty, ClientId);
                            //wtr.Properties.Add("resource", Resource);

                            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

                            // Perform the token request without showing any UX
                            //WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(wtr, UserAccount);
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


        // OneDrive Service Settings
        public static OneDriveService OneDriveService =
                new OneDriveService(
                        async () =>
                        {
                            // Craft the token request for the Graph api
                            //WebTokenRequest wtr = new WebTokenRequest(WAP, string.Empty, ClientId);
                            //wtr.Properties.Add("resource", Resource);

                            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

                            // Perform the token request without showing any UX
                            //WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(wtr, UserAccount);
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
        public static Office365Service.Excel.ExcelRESTService ExcelService =
            new Office365Service.Excel.ExcelRESTService(
                        async () =>
                        {
                            // Craft the token request for the Graph api
                            //WebTokenRequest wtr = new WebTokenRequest(WAP, string.Empty, ClientId);
                            //wtr.Properties.Add("resource", Resource);

                            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

                            // Perform the token request without showing any UX
                            //WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(wtr, UserAccount);
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

        ApplicationDataContainer appSettings = null;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = false;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            
            Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.CreateDefaultUI();

            // Ensure the current window is active
            Window.Current.Activate();

            // Authenticate 

            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

            if (accessToken == null)
            {
                UpdateUXonSignOut();
                MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                await messageDialog.ShowAsync();
            }


            //appSettings = ApplicationData.Current.RoamingSettings;
            //App.WAP = await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.microsoft.com", App.Authority);

            //WebTokenRequest wtr = new WebTokenRequest(App.WAP, string.Empty, App.ClientId);
            //wtr.Properties.Add("resource", App.Resource);

            //// Check if there's a record of the last account used with the app
            //var userID = appSettings.Values["userID"];
            //if (userID != null)
            //{
            //    // Get an account object for the user
            //    App.UserAccount = await WebAuthenticationCoreManager.FindAccountAsync(App.WAP, (string)userID);
            //    if (App.UserAccount != null)
            //    {
            //        // Ensure that the saved account works for getting the token we need
            //        WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr, App.UserAccount);
            //        if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
            //        {
            //            App.UserAccount = wtrr.ResponseData[0].WebAccount;
            //        }
            //        else
            //        {
            //            // The saved account could not be used for getitng a token
            //            MessageDialog messageDialog = new MessageDialog("We tried to sign you in with the last account you used with this app, but it didn't work out. Please sign in as a different user.");
            //            await messageDialog.ShowAsync();
            //            // Make sure that the UX is ready for a new sign in
            //            UpdateUXonSignOut();
            //        }
            //    }
            //    else
            //    {
            //        // The WebAccount object is no longer available. Let's attempt a sign in with the saved username
            //        wtr.Properties.Add("LoginHint", appSettings.Values["login_hint"].ToString());
            //        WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
            //        if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
            //        {
            //            App.UserAccount = wtrr.ResponseData[0].WebAccount;
            //        }
            //    }
            //}
            //else
            //{
            //    // There is no recorded user. Let's start a sign in flow without imposing a specific account.                             
            //    WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
            //    if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
            //    {
            //        App.UserAccount = wtrr.ResponseData[0].WebAccount;
            //    }
            //}

            //if (App.UserAccount != null) // we succeeded in obtaining a valid user
            //{
            //    // save user ID in local storage
            //    UpdateUXonSignIn();
            //}
            //else
            //{
            //    // nothing we tried worked. Ensure that the UX reflects that there is no user currently signed in.
            //    UpdateUXonSignOut();
            //    MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
            //    await messageDialog.ShowAsync();
            //}

            // Run the tests
            Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.Run(e.Arguments);
        }

        // Update the UX and the app settings to show that a user is signed in
        private void UpdateUXonSignIn()
        {
            //appSettings.Values["userID"] = App.UserAccount.Id;
            //appSettings.Values["login_hint"] = App.UserAccount.UserName;
            //textSignedIn.Text = string.Format("You are signed in as {0}", App.UserAccount.UserName);
            //btnSignInOut.Content = "Sign in as a different user";
        }

        // update the UX and the app settings to show that no user is signed in at the moment
        private void UpdateUXonSignOut()
        {
            //appSettings.Values["userID"] = null;
            //appSettings.Values["login_hint"] = null;
            //textSignedIn.Text = "You are not signed in.";
            //btnSignInOut.Content = "Sign in";
        }
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
