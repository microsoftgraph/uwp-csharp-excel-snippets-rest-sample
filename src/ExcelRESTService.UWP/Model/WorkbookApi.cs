/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Data.Json;

using Office365Service.OneDrive;

using Microsoft.ExcelServices;

namespace Office365Service.Excel
{
    class WorkbookApi : ItemApi, IRestApi
    {
        #region Constructor
        public WorkbookApi(RESTService service, string title, string description, string method, string resourceFormat, Type resultType)
            : base(service, title, description, method, string.Empty, resultType)
        {
            ResourceFormat = resourceFormat;
        }
        #endregion

        #region Properties
        // ResourceFormat
        public new string ResourceFormat { get; set; }
        // Resource
        public override string Resource
        {
            get
            {
                return base.Resource + $"/workbook{ResourceFormat}";
            }
        }
        // WorkbookId
        public string WorkbookId
        {
            get
            {
                return base.ItemId;
            }
            set
            {
                base.ItemId = value;
            }
        }
        // WorkbookPath
        public string WorkbookPath
        {
            get
            {
                return base.ItemPath;
            }
            set
            {
                base.ItemPath = value;
            }
        }
        // WorkbookName
        public string WorkbookName
        {
            get
            {
                return base.ItemName;
            }
            set
            {
                base.ItemName = value;
            }
        }
        // SessionId
        private const string sessionIdHeaderKey = "Workbook-Session-Id";

        private string sessionId = string.Empty;
        public string SessionId
        {
            get
            {
                return sessionId;
            }
            set
            {
                sessionId = value;
                if (sessionId != string.Empty)
                {
                    if (!(Resource.EndsWith("/createSession")))
                    {
                        Headers[sessionIdHeaderKey] = sessionId;
                    }
                    else
                    {
                        if (Headers.ContainsKey(sessionIdHeaderKey))
                        {
                            Headers.Remove(sessionIdHeaderKey);
                        }
                    }
                    
                }
                else
                {
                    if (Headers.ContainsKey(sessionIdHeaderKey))
                    {
                        Headers.Remove(sessionIdHeaderKey);
                    }
                }
                
            }
        }
        #endregion

        #region Methods
        public async Task<object> InvokeAsync(string id, string sessionId = "", string queryParameters = "")
        {
            WorkbookId = id;
            SessionId = sessionId;
            QueryParameters = queryParameters;
            return await InvokeAsync();
        }
        #endregion

        #region Mapping
        protected override object MapResult(JsonObject jsonResult)
        {
            object result;
            switch (ResultType?.Name)
            {
                case "SessionInfo":
                    result = SessionInfo.MapFromJson(jsonResult);
                    LogResult(result);
                    SessionInfo.LastId = ((SessionInfo)result).Id;
                    return result;
                case "Range":
                    result = Range.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;
                case "Worksheet":
                    result = Worksheet.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;
                case "Worksheet[]":
                    var worksheets = new List<Worksheet>();
                    foreach (var jsonWorksheet in jsonResult.GetNamedArray("value"))
                    {
                        worksheets.Add(Worksheet.MapFromJson(jsonWorksheet.GetObject()));
                    }
                    LogResult(worksheets);
                    return worksheets.ToArray();
                case "Table":
                    result = Table.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;
                case "Table[]":
                    var tables = new List<Table>();
                    foreach (var jsonTable in jsonResult.GetNamedArray("value"))
                    {
                        tables.Add(Table.MapFromJson(jsonTable.GetObject()));
                    }
                    LogResult(tables);
                    return tables.ToArray();
                case "TableRow":
                    result = TableRow.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;
                case "Chart":
                    result = Chart.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;
                case "Chart[]":
                    var charts = new List<Chart>();
                    foreach (var jsonChart in jsonResult.GetNamedArray("value"))
                    {
                        charts.Add(Chart.MapFromJson(jsonChart.GetObject()));
                    }
                    LogResult(charts);
                    return charts.ToArray();
                case "ChartTitle":
                    result = ChartTitle.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;

                default:
                    return base.MapResult(jsonResult);
            }
        }
        #endregion

    }
}
