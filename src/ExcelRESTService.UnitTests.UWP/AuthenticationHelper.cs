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
        static string clientId = "02139874-fa17-4b59-88a8-717796ced4ba";

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
                            "https://graph.microsoft.com/User.Read",
                            "https://graph.microsoft.com/User.ReadBasic.All",
                            "https://graph.microsoft.com/Files.ReadWrite",


             };

            try
            {
                authResult = await IdentityClientApp.AcquireTokenSilentAsync(scopes);
                TokenForUser = authResult.Token;
                // save user ID in local storage
                _settings.Values["userID"] = authResult.User.UniqueId;
                _settings.Values["login_hint"] = authResult.User.Name;
            }

            catch (Exception)
            {
                if (TokenForUser == null || Expiration <= DateTimeOffset.UtcNow.AddMinutes(5))
                {
                    authResult = await IdentityClientApp.AcquireTokenAsync(scopes);

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
