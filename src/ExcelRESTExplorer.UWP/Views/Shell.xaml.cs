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
using Microsoft.Identity.Client;

namespace ExcelServiceExplorer.Views
{
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu { get { return Instance.MyHamburgerMenu; } }

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
            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

            if (accessToken != null)
            {
                UpdateUXonSignIn();
            }
            else
            {
                UpdateUXonSignOut();
                MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                await messageDialog.ShowAsync();
            }

        }

        // Change the currently signed in user
        private async void btnSignInOut_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationHelper.SignOut();
            UpdateUXonSignOut();

            var accessToken = await AuthenticationHelper.GetTokenForUserAsync();

            if (accessToken != null)
            {
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
            textSignedIn.Text = App.UserAccount.Name;
        }

        // update the UX and the app settings to show that no user is signed in at the moment
        private void UpdateUXonSignOut()
        {
            textSignedIn.Text = "Sign in";
        }
    }
}

