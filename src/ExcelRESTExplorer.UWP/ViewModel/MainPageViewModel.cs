/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml.Navigation;

using Template10.Mvvm;
using Template10.Services.NavigationService;
using ExcelServiceExplorer.Model;
using ExcelServiceExplorer.Views;
using Office365Service.User;
using Office365Service.OneDrive;
using System;
using Office365Service.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ExcelServiceExplorer.ViewModel
{
    public class MainPageViewModel : Template10.Mvvm.ViewModelBase
    {
        // Views
        public static Dictionary<string, View> Views = new Dictionary<string, View>();

        #region Constructor
        public MainPageViewModel()
        {
        }

        static MainPageViewModel()
        {
            // Setup views

            // User
            Views.Add("GetUser", new View(typeof(GetUser), App.UserService.GetUserApi));

            // Item
            Views.Add("UploadFile", new View(typeof(UploadFile), App.OneDriveService.UploadFileApi));
            Views.Add("GetItemMetadata", new View(typeof(GetItemMetadata), App.OneDriveService.GetItemMetadataApi));

            // Session
            Views.Add("CreateSession", new View(typeof(CreateSession), App.ExcelService.CreateSessionApi));
            Views.Add("CloseSession", new View(typeof(CloseSession), App.ExcelService.CloseSessionApi));

            // Application
            Views.Add("Calculate", new View(typeof(Calculate), App.ExcelService.CalculateApi));

            // Worksheet
            Views.Add("ListWorksheets", new View(typeof(ListWorksheets), App.ExcelService.ListWorksheetsApi));
            Views.Add("AddWorksheet", new View(typeof(AddWorksheet), App.ExcelService.AddWorksheetApi));
            Views.Add("WorksheetGetUsedRange", new View(typeof(WorksheetGetUsedRange), App.ExcelService.WorksheetGetUsedRangeApi));

            // Range
            Views.Add("GetRange", new View(typeof(GetRange), App.ExcelService.GetRangeApi));
            Views.Add("UpdateRange", new View(typeof(UpdateRange), App.ExcelService.UpdateRangeApi));

            // Table
            Views.Add("ListTables", new View(typeof(ListTables), App.ExcelService.ListTablesApi));
            Views.Add("GetTable", new View(typeof(GetTable), App.ExcelService.GetTableApi));
            Views.Add("AddTable", new View(typeof(AddTable), App.ExcelService.AddTableApi));
            Views.Add("UpdateTable", new View(typeof(UpdateTable), App.ExcelService.UpdateTableApi));
            Views.Add("AddTableRow", new View(typeof(AddTableRow), App.ExcelService.AddTableRowApi));
            Views.Add("AddTableColumn", new View(typeof(AddTableColumn), App.ExcelService.AddTableColumnApi));
            Views.Add("GetTableRange", new View(typeof(GetTableRange), App.ExcelService.GetTableRangeApi));
            Views.Add("GetTableHeaderRowRange", new View(typeof(GetTableRange), App.ExcelService.GetTableHeaderRowRangeApi));
            Views.Add("GetTableDataBodyRange", new View(typeof(GetTableRange), App.ExcelService.GetTableDataBodyRangeApi));

            // Charts
            Views.Add("ListCharts", new View(typeof(ListCharts), App.ExcelService.ListChartsApi));
            Views.Add("AddChart", new View(typeof(AddChart), App.ExcelService.AddChartApi));
            Views.Add("GetChart", new View(typeof(GetChart), App.ExcelService.GetChartApi));
            Views.Add("GetChartTitle", new View(typeof(GetChartTitle), App.ExcelService.GetChartTitleApi));
            Views.Add("GetChartImage", new View(typeof(GetChartImage), App.ExcelService.GetChartImageApi));

            // Named Items
            Views.Add("ListNamedItems", new View(typeof(ListNamedItems), App.ExcelService.ListNamedItemsApi));
            Views.Add("GetNamedItem", new View(typeof(GetNamedItem), App.ExcelService.GetNamedItemApi));
            Views.Add("NamedItemRange", new View(typeof(NamedItemRange), App.ExcelService.NamedItemRangeApi));
        }
        #endregion

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoApiPage(object sender, RoutedEventArgs e) 
        {
            var link = (HyperlinkButton)sender;

            if (MainPageViewModel.Views.ContainsKey(link.Name))
            {
                var view = MainPageViewModel.Views[link.Name];
                var api = view.Api;

                api.Service.RequestViewModel = new RequestViewModel();
                api.Service.ResponseViewModel = new ResponseViewModel();

                api.Service.RequestViewModel.Api = RestApiViewModelFactory.Create(api);

                api.Service.RequestViewModel.Clear();
                api.Service.ResponseViewModel.Clear();

                NavigationService.Navigate(view.Page);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{link.Name} is not a valid view");
            }
        }
    }
}



