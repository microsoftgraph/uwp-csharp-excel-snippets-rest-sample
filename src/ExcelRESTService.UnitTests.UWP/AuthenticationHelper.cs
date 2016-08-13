using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Windows.Storage;

namespace ExcelRESTService.UnitTests.UWP
{
    public class AuthenticationHelper
    {
        // The Client ID is used by the application to uniquely identify itself to the v2.0 authentication endpoint.
        static string clientId = "67c64841-9567-4a6b-aec3-e34e7677ee9d";

        public static PublicClientApplication IdentityClientApp = new PublicClientApplication(clientId);

        public static string TokenForUser = null;
        public static DateTimeOffset Expiration;
        public static ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;

        /// <summary>
        /// Get Token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> GetTokenForUserAsync()
        {
            AuthenticationResult authResult;
            var scopes = new string[]

             {
                            "User.Read",
                            "User.ReadBasic.All",
                            "Files.ReadWrite",


             };

            try
            {
                //Specify the "organizations" authority, for now, because Excel API currently works only with work and school accounts.
                authResult = await IdentityClientApp.AcquireTokenSilentAsync(scopes, (string)_settings.Values["userID"], "https://login.microsoftonline.com/organizations/", null, false);
                TokenForUser = authResult.Token;
                // save user ID in local storage
                _settings.Values["userID"] = authResult.User.UniqueId;
                _settings.Values["login_hint"] = authResult.User.Name;
                App.UserAccount = authResult.User;
            }

            catch (Exception)
            {
                if (TokenForUser == null || Expiration <= DateTimeOffset.UtcNow.AddMinutes(5))
                {
                    //Specify the "organizations" authority, for now, because Excel API currently works only with work and school accounts.
                    authResult = await IdentityClientApp.AcquireTokenAsync(scopes, "", UiOptions.SelectAccount, null, null, "https://login.microsoftonline.com/organizations/", null);
                    TokenForUser = authResult.Token;
                    Expiration = authResult.ExpiresOn;
                    // save user ID in local storage
                    _settings.Values["userID"] = authResult.User.UniqueId;
                    _settings.Values["login_hint"] = authResult.User.Name;
                    App.UserAccount = authResult.User;
                }
            }

            return TokenForUser;
        }

        /// <summary>
        /// Signs the user out of the service.
        /// </summary>
        public static void SignOut()
        {
            foreach (var user in IdentityClientApp.Users)
            {
                user.SignOut();
            }

            TokenForUser = null;
            _settings.Values["userID"] = null;
            _settings.Values["login_hint"] = null;
        }

    }
}
