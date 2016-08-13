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


        // OneDrive Service Settings
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
        public static Office365Service.Excel.ExcelRESTService ExcelService =
            new Office365Service.Excel.ExcelRESTService(
                        async () =>
                        {
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
                MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                await messageDialog.ShowAsync();
            }

            // Run the tests
            Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.Run(e.Arguments);
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
