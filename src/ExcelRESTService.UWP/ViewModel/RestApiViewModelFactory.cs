/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365Service.ViewModel
{
    public static class RestApiViewModelFactory
    {
        #region Methods
        public static RestApiViewModel Create(IRestApi api)
        {
            switch (api.GetType().Name)
            {
                case "UserApi":
                    return new UserApiViewModel(api);
                case "ItemApi":
                    return new ItemApiViewModel(api);
                case "WorkbookApi":
                    return new WorkbookApiViewModel(api);
                case "WorksheetApi":
                    return new WorksheetApiViewModel(api);
                case "RangeApi":
                    return new RangeApiViewModel(api);
                case "TableApi":
                    return new TableApiViewModel(api);
                case "ChartApi":
                    return new ChartApiViewModel(api);
                default:
                    throw new ArgumentOutOfRangeException($"{api.GetType().Name} is not a valid API type");
            }
        }
        #endregion
    }
}
