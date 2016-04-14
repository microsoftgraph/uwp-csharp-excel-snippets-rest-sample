/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Threading.Tasks;
using System.Net;

namespace Office365Service.User
{
    public partial class UserService : RESTService 
    {
        #region Constructor
        public UserService(Func<Task<string>> getAccessTokenAsync) : base(getAccessTokenAsync)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods

        #region User
        // GetUserAsync
        UserApi getUserApi;

        public IRestApi GetUserApi
        {
            get
            {
                if (getUserApi == null)
                    getUserApi =
                        new UserApi(
                            this,
                            "Get User",
                            "Retrieve the metadata for the signed in user.",
                            "GET",
                            "/me",
                            typeof(Microsoft.User.User)
                        );
                return getUserApi;
            }
        }
        public async Task<Microsoft.User.User> GetUserAsync()
        {
            return (Microsoft.User.User)(await GetUserApi.InvokeAsync());
        }
        #endregion

        #endregion
    }
}

