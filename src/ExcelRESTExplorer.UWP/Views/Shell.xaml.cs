/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;

using Windows.UI.Xaml.Controls;
using Windows.Security.Authentication.Web.Core;
using Windows.UI.Popups;

using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Xaml;
using Windows.Storage;
using System.Threading.Tasks;

namespace ExcelServiceExplorer.Views
{
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu { get { return Instance.MyHamburgerMenu; } }

        ApplicationDataContainer appSettings = null;

        public Shell()
        {
            Instance = this;
            InitializeComponent();
        }

        public Shell(INavigationService navigationService) : this()
        {
            SetNavigationService(navigationService);
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            MyHamburgerMenu.NavigationService = navigationService;
        }

        public async Task SignIn()
        {
            appSettings = ApplicationData.Current.RoamingSettings;
            App.WAP = await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.microsoft.com", App.Authority);

            WebTokenRequest wtr = new WebTokenRequest(App.WAP, string.Empty, App.ClientId);
            wtr.Properties.Add("resource", App.Resource);

            // Check if there's a record of the last account used with the app
            var userID = appSettings.Values["userID"];
            if (userID != null)
            {
                // Get an account object for the user
                App.UserAccount = await WebAuthenticationCoreManager.FindAccountAsync(App.WAP, (string)userID);
                if (App.UserAccount != null)
                {
                    // Ensure that the saved account works for getting the token we need
                    WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr, App.UserAccount);
                    if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
                    {
                        App.UserAccount = wtrr.ResponseData[0].WebAccount;
                    }
                    else
                    {
                        // The saved account could not be used for getitng a token
                        MessageDialog messageDialog = new MessageDialog("We tried to sign you in with the last account you used with this app, but it didn't work out. Please sign in as a different user.");
                        await messageDialog.ShowAsync();
                        // Make sure that the UX is ready for a new sign in
                        UpdateUXonSignOut();
                    }
                }
                else
                {
                    // The WebAccount object is no longer available. Let's attempt a sign in with the saved username
                    wtr.Properties.Add("LoginHint", appSettings.Values["login_hint"].ToString());
                    WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
                    if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
                    {
                        App.UserAccount = wtrr.ResponseData[0].WebAccount;
                    }
                }
            }
            else
            {
                // There is no recorded user. Let's start a sign in flow without imposing a specific account.                             
                WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
                if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
                {
                    App.UserAccount = wtrr.ResponseData[0].WebAccount;
                }
            }

            if (App.UserAccount != null) // we succeeded in obtaining a valid user
            {
                // save user ID in local storage
                UpdateUXonSignIn();
            }
            else
            {
                // nothing we tried worked. Ensure that the UX reflects that there is no user currently signed in.
                UpdateUXonSignOut();
                MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                await messageDialog.ShowAsync();
            }
        }

        // Change the currently signed in user
        private async void btnSignInOut_Click(object sender, RoutedEventArgs e)
        {
            // Prepare a request with 'WebTokenRequestPromptType.ForceAuthentication', 
            // which guarantees that the user will be able to enter an account of their choosing
            // regardless of what accounts are already present on the system
            WebTokenRequest wtr = new WebTokenRequest(App.WAP, string.Empty, App.ClientId, WebTokenRequestPromptType.ForceAuthentication);
            wtr.Properties.Add("resource", App.Resource);
            WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
            if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
            {
                App.UserAccount = wtrr.ResponseData[0].WebAccount;
                UpdateUXonSignIn();
            }
            else
            {
                UpdateUXonSignOut();
                MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                await messageDialog.ShowAsync();
            }
        }

        // Update the UX and the app settings to show that a user is signed in
        private void UpdateUXonSignIn()
        {
            appSettings.Values["userID"] = App.UserAccount.Id;
            appSettings.Values["login_hint"] = App.UserAccount.UserName;
            textSignedIn.Text = App.UserAccount.UserName;
        }

        // update the UX and the app settings to show that no user is signed in at the moment
        private void UpdateUXonSignOut()
        {
            appSettings.Values["userID"] = null;
            appSettings.Values["login_hint"] = null;
            textSignedIn.Text = "Sign in";
        }
    }
}

