/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Threading.Tasks;

using Microsoft.ExcelServices;
using Microsoft.Edm;

namespace Office365Service.Excel
{
    public partial class ExcelRESTService : RESTService 
    {
        #region Constructor
        public ExcelRESTService(Func<Task<string>> getAccessTokenAsync) : base(getAccessTokenAsync)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods

        #region Session
        // CreateSessionAsync
        WorkbookApi createSessionApi;
        public IRestApi CreateSessionApi
        {
            get
            {
                if (createSessionApi == null)
                {
                    createSessionApi =
                        new WorkbookApi(
                            this,
                            "Create Session",
                            "Create a new session.",
                            "POST",
                            "/createSession",
                            typeof(SessionInfo)
                        );
                    createSessionApi.BodyProperties["persistChanges"] = true;
                }
                return createSessionApi;
            }
        }

        public async Task<SessionInfo> CreateSessionAsync(string id, bool persistChanges = true)
        {
            CreateSessionApi.BodyProperties["persistChanges"] = persistChanges;
            return (SessionInfo)(await ((WorkbookApi)(CreateSessionApi)).InvokeAsync(id));
        }

        // CloseSessionAsync
        WorkbookApi closeSessionApi;
        public IRestApi CloseSessionApi
        {
            get
            {
                if (closeSessionApi == null)
                {
                    closeSessionApi =
                        new WorkbookApi(
                            this,
                            "Close Session",
                            "Close the current session.",
                            "POST",
                            "/closeSession",
                            null
                        );
                }
                return closeSessionApi;
            }
        }

        public async Task CloseSessionAsync(string id, string sessionId = "")
        {
            await ((WorkbookApi)(CloseSessionApi)).InvokeAsync(id, sessionId);
        }
        #endregion

        #region Application  
        
        // CalculateAsync
        WorkbookApi calculateApi;

        public IRestApi CalculateApi
        {
            get
            {
                if (calculateApi == null)
                    calculateApi =
                        new WorkbookApi(
                            this,
                            "Calculate",
                            "Recalculate all currently opened workbooks in Excel.",
                            "POST",
                            "/application/calculate",
                            null
                        );
                calculateApi.BodyProperties["calculationType"] = "Recalculate";
                return calculateApi;
            }
        }

        public async Task CalculateAsync(string id, string sessionId = "", string calculationType = "Recalculate")
        {
            CalculateApi.BodyProperties["calculationType"] = calculationType;
            ((WorkbookApi)(CalculateApi)).SessionId = sessionId;
            await ((WorkbookApi)(CalculateApi)).InvokeAsync(id);
        }
        #endregion

        #region Worksheets
        // ListWorksheets
        WorkbookApi listWorksheetsApi;

        public IRestApi ListWorksheetsApi
        {
            get
            {
                if (listWorksheetsApi == null)
                    listWorksheetsApi =
                        new WorkbookApi(
                            this,
                            "List Worksheets",
                            "Retrieve a list of worksheet objects.",
                            "GET",
                            "/worksheets",
                            typeof(Worksheet[])
                        );
                return listWorksheetsApi;
            }
        }

        public async Task<Worksheet[]> ListWorksheetsAsync(string id, string sessionId = "", string queryParameters = "")
        {
            return (Worksheet[])(await ((WorkbookApi)(ListWorksheetsApi)).InvokeAsync(id, sessionId, queryParameters));
        }

        // AddWorksheetAsync
        WorkbookApi addWorksheetApi;

        public IRestApi AddWorksheetApi
        {
            get
            {
                if (addWorksheetApi == null)
                {
                    addWorksheetApi =
                        new WorkbookApi(
                            this,
                            "Add Worksheet",
                            "Adds a new worksheet to the workbook.The worksheet will be added at the end of existing worksheets.",
                            "POST",
                            "/worksheets",
                            typeof(Worksheet)
                        );
                    addWorksheetApi.BodyProperties["name"] = string.Empty;
                }
                return addWorksheetApi;
            }
        }

        public async Task<Worksheet> AddWorksheetAsync(string id, string name, string sessionId = "")
        {
            AddWorksheetApi.BodyProperties["name"] = name;
            return (Worksheet)(await ((WorkbookApi)(AddWorksheetApi)).InvokeAsync(id, sessionId));
        }

        // WorksheetGetUsedRangeAsync
        WorksheetApi worksheetGetUsedRangeApi;

        public IRestApi WorksheetGetUsedRangeApi
        {
            get
            {
                if (worksheetGetUsedRangeApi == null)
                    worksheetGetUsedRangeApi = 
                        new WorksheetApi(
                            this,
                            "Worksheet: UsedRange",
                            "The used range is the smallest range that encompasses any cells that have a value or formatting assigned to them. If the worksheet is blank, this function will return the top left cell.",
                            "GET",
                            "/usedRange(valuesOnly=true)",
                            typeof(Range)
                        );
                return worksheetGetUsedRangeApi;
            }
        }

        public async Task<Range> WorksheetGetUsedRangeAsync(string id, string worksheetName, string sessionId = "", string queryParameters = "")
        {
            return (Range)(await ((WorksheetApi)(WorksheetGetUsedRangeApi)).InvokeAsync(id, worksheetName, sessionId, queryParameters));
        }
        #endregion

        #region Tables
        // ListTablesAsync
        WorkbookApi listTablesApi;

        public IRestApi ListTablesApi
        {
            get
            {
                if (listTablesApi == null)
                {
                    listTablesApi =
                        new WorkbookApi(
                            this,
                            "List Tables",
                            "Retrieve a list of table objects.",
                            "GET",
                            "/tables",
                            typeof(Table[])
                        );
                }
                return listTablesApi;
            }
        }

        public async Task<Table[]> ListTablesAsync(string id, string sessionId = "", string queryParameters = "")
        {
            return (Table[])(await ((WorkbookApi)(ListTablesApi)).InvokeAsync(id, sessionId, queryParameters));
        }

        // AddTableAsync
        WorkbookApi addTableApi;

        public IRestApi AddTableApi
        {
            get
            {
                if (addTableApi == null)
                {
                    addTableApi =
                        new WorkbookApi(
                            this,
                            "Add Table",
                            "Create a new table. The range source address determines the worksheet under which the table will be added. If the table cannot be added (e.g., because the address is invalid, or the table would overlap with another table), an error will be thrown.",
                            "POST",
                            "/tables/$/add",
                            typeof(Table)
                        );
                    addTableApi.BodyProperties["address"] = string.Empty;
                    addTableApi.BodyProperties["hasHeaders"] = true;
                }
                return addTableApi;
            }
        }

        public async Task<Table> AddTableAsync(string id, string address, bool hasHeaders, string sessionId = "")
        {
            AddTableApi.BodyProperties["address"] = address;
            AddTableApi.BodyProperties["hasHeaders"] = hasHeaders;
            return (Table)(await ((WorkbookApi)(AddTableApi)).InvokeAsync(id, sessionId));
        }

        // UpdateTableAsync
        TableApi updateTableApi;

        public IRestApi UpdateTableApi
        {
            get
            {
                if (updateTableApi == null)
                {
                    updateTableApi =
                        new TableApi(
                            this,
                            "Update Table",
                            "Update the properties of table object.",
                            "PATCH",
                            string.Empty,
                            typeof(Table)
                        );
                    updateTableApi.BodyProperties["name"] = string.Empty;
                    updateTableApi.BodyProperties["showHeaders"] = null;
                    updateTableApi.BodyProperties["showTotals"] = null;
                    updateTableApi.BodyProperties["style"] = string.Empty;
                }
                return updateTableApi;
            }
        }

        public async Task<Table> UpdateTableAsync(string id, string tableName, Table updatedTable, string sessionId = "")
        {
            UpdateTableApi.BodyProperties["name"] = updatedTable.Name;
            UpdateTableApi.BodyProperties["showHeaders"] = updatedTable.ShowHeaders;
            UpdateTableApi.BodyProperties["showTotals"] = updatedTable.ShowTotals;
            UpdateTableApi.BodyProperties["style"] = updatedTable.Style;
            return (Table)(await ((TableApi)(UpdateTableApi)).InvokeAsync(id, tableName, sessionId));
        }

        // AddTableRowAsync
        TableApi addTableRowApi;

        public IRestApi AddTableRowApi
        {
            get
            {
                if (addTableRowApi == null)
                    addTableRowApi =
                        new TableApi(
                            this,
                            "Add Table Row",
                            "Adds a new row to a table.",
                            "POST",
                            "/rows",
                            typeof(TableRow)
                        );
                return addTableRowApi;
            }
        }

        public async Task<TableRow> AddTableRowAsync(string id, string tableName, object[] values, int? index = null, string sessionId = "")
        {
            AddTableRowApi.BodyProperties["index"] = index;
            AddTableRowApi.BodyProperties["values"] = values;
            return (TableRow)(await ((TableApi)(AddTableRowApi)).InvokeAsync(id, tableName, sessionId));
        }

        // AddTableColumnAsync
        TableApi addTableColumnApi;

        public IRestApi AddTableColumnApi
        {
            get
            {
                if (addTableColumnApi == null)
                    addTableColumnApi =
                        new TableApi(
                            this,
                            "Add Table Column",
                            "Adds a new column to a table.",
                            "POST",
                            "/columns",
                            typeof(TableRow)
                        );
                return addTableColumnApi;
            }
        }

        // TODO: Return value should be of type TableColumn
        public async Task<TableRow> AddTableColumnAsync(string id, string tableName, object[] values, int? index = null, string sessionId = "")
        {
            AddTableColumnApi.BodyProperties["index"] = index;
            AddTableColumnApi.BodyProperties["values"] = values;
            return (TableRow)(await ((TableApi)(AddTableColumnApi)).InvokeAsync(id, tableName, sessionId));
        }

        // GetTableHeaderRowRangesync
        TableApi getTableHeaderRowRangeApi;

        public IRestApi GetTableHeaderRowRangeApi
        {
            get
            {
                if (getTableHeaderRowRangeApi == null)
                    getTableHeaderRowRangeApi =
                        new TableApi(
                            this,
                            "Table: Get Header Row Range",
                            "Gets the range object associated with header row of the table.",
                            "GET",
                            "/headerRowRange",
                            typeof(Range)
                        );
                return getTableHeaderRowRangeApi;
            }
        }

        public async Task<Range> GetTableHeaderRowRangeAsync(string id, string tableName, string sessionId = "", string queryParameters = "") 
        {
            return (Range)(await ((TableApi)(GetTableHeaderRowRangeApi)).InvokeAsync(id, tableName, sessionId, queryParameters));
        }

        // GetTableDataBodyRangeAsync
        TableApi getTableDataBodyRangeApi;

        public IRestApi GetTableDataBodyRangeApi
        {
            get
            {
                if (getTableDataBodyRangeApi == null)
                    getTableDataBodyRangeApi =
                        new TableApi(
                            this,
                            "Table: Get Data Body Range",
                            "Gets the range object associated with the data body of the table.",
                            "GET",
                            "/dataBodyRange",
                            typeof(Range)
                        );
                return getTableDataBodyRangeApi;
            }
        }
        public async Task<Microsoft.ExcelServices.Range> GetTableDataBodyRangeAsync(string id, string tableName, string sessionId = "", string queryParameters = "")
        {
            return (Range)(await ((TableApi)(GetTableDataBodyRangeApi)).InvokeAsync(id, tableName, sessionId, queryParameters));
        }
        #endregion

        #region Ranges
        
        // GetRangeAsync
        RangeApi getRangeApi;

        public IRestApi GetRangeApi
        {
            get
            {
                if (getRangeApi == null)
                {
                    getRangeApi =
                        new RangeApi(
                            this,
                            "Get Range",
                            "Retrieve the properties and relationships of range object.",
                            "GET",
                            string.Empty,
                            typeof(Range)
                        );
                }
                return getRangeApi;
            }
        }
        
        public async Task<Range> GetRangeAsync(string id, string worksheetId, string address, string sessionId = "", string queryParameters = "")
        {
            return (Range)(await ((RangeApi)(GetRangeApi)).InvokeAsync(id, worksheetId, address, sessionId, queryParameters));
        }

        // UpdateRange
        RangeApi updateRangeApi;

        public IRestApi UpdateRangeApi
        {
            get
            {
                if (updateRangeApi == null)
                {
                    updateRangeApi =
                        new RangeApi(
                            this,
                            "Update Range",
                            "Update the properties of range object.",
                            "PATCH",
                            string.Empty,
                            typeof(Range)
                        );
                }
                return updateRangeApi;
            }
        }

        public async Task<Range> UpdateRangeAsync(string id, string worksheetId, string address, object[] values, string sessionId = "", string queryParameters = "")
        {
            UpdateRangeApi.BodyProperties["values"] = values;
            return (Range)(await ((RangeApi)(UpdateRangeApi)).InvokeAsync(id, worksheetId, address, sessionId, queryParameters));
        }
        #endregion

        #region Charts
        // ListChartsAsync
        WorksheetApi listChartsApi;

        public IRestApi ListChartsApi
        {
            get
            {
                if (listChartsApi == null)
                    listChartsApi =
                        new WorksheetApi(
                            this,
                            "List Charts",
                            "Retrieve a list of chart objects.",
                            "GET",
                            "/charts",
                            typeof(Chart[])
                        );
                return listChartsApi;
            }
        }

        public async Task<Chart[]> ListChartsAsync(string id, string worksheetId, string sessionId = "", string queryParameters = "")
        {
            return (Chart[])(await ((WorksheetApi)(ListChartsApi)).InvokeAsync(id, worksheetId, sessionId, queryParameters));
        }

        // AddChartAsync
        WorksheetApi addChartApi;

        public IRestApi AddChartApi
        {
            get
            {
                if (addChartApi == null)
                    addChartApi =
                        new WorksheetApi(
                            this,
                            "Add Chart",
                            "Creates a new chart.",
                            "POST",
                            "/charts/$/add",
                            typeof(Chart)
                        );
                addChartApi.BodyProperties["type"] = "ColumnClustered";
                addChartApi.BodyProperties["sourceData"] = "Sheet3!A1:B4";
                addChartApi.BodyProperties["seriesBy"] = "Auto";
                return addChartApi;
            }
        }

        public async Task<Chart> AddChartAsync(string id, string worksheetId, string type, string sourceData, string seriesBy = "Auto", string sessionId = "")
        {
            AddChartApi.BodyProperties["type"] = type;
            AddChartApi.BodyProperties["sourceData"] = sourceData;
            AddChartApi.BodyProperties["seriesBy"] = seriesBy;
            return (Chart)(await ((WorksheetApi)(AddChartApi)).InvokeAsync(id, worksheetId, sessionId));
        }

        // GetChartAsync
        ChartApi getChartApi;

        public IRestApi GetChartApi
        {
            get
            {
                if (getChartApi == null)
                    getChartApi =
                        new ChartApi(
                            this,
                            "Get Chart",
                            "Get a chart object.",
                            "GET",
                            string.Empty,
                            typeof(Chart)
                        );
                return getChartApi;
            }
        }

        public async Task<Chart> GetChartAsync(string id, string worksheetId, string chartId, string sessionId = "", string queryParameters = "")
        {
            return (Chart)(await ((ChartApi)(GetChartApi)).InvokeAsync(id, worksheetId, chartId, sessionId, queryParameters));
        }

        // GetChartTitleAsync
        ChartApi getChartTitleApi;

        public IRestApi GetChartTitleApi
        {
            get
            {
                if (getChartTitleApi == null)
                    getChartTitleApi =
                        new ChartApi(
                            this,
                            "Get Chart Title",
                            "Get chart title object.",
                            "GET",
                            "/title",
                            typeof(ChartTitle)
                        );
                return getChartTitleApi;
            }
        }
        public async Task<ChartTitle> GetChartTitleAsync(string id, string worksheetId, string chartId, string sessionId = "")
        {
            return (ChartTitle)(await ((ChartApi)(GetChartTitleApi)).InvokeAsync(id, worksheetId, chartId, sessionId));
        }

        // GetChartImageAsync
        ChartApi getChartImageApi;

        public IRestApi GetChartImageApi
        {
            get
            {
                if (getChartImageApi == null)
                    getChartImageApi =
                        new ChartApi(
                            this,
                            "Get Chart Image",
                            "Retrieve the image of a chart object. Returns the image as a base64 encoded string.",
                            "GET",
                            "/Image(width=0,height=0,fittingMode='fit')",
                            typeof(EdmString)
                        );
                return getChartImageApi;
            }
        }

        public async Task<string> GetChartImageAsync(string id, string worksheetId, string chartId, string sessionId = "")
        {
            return ((EdmString)(await ((ChartApi)(GetChartImageApi)).InvokeAsync(id, worksheetId, chartId, sessionId)))?.Value;
        }

        #endregion
        #endregion

        #region Logging
        private void LogResult(object result)
        {
            if (ResponseViewModel != null)
            {
                ResponseViewModel.Result = result;
            }
        }
        #endregion
    }
}

