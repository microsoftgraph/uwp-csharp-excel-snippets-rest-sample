/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ExcelServiceExplorer.Controls
{
    public sealed partial class IdNameEntry : UserControl
    {
        public IdNameEntry()
        {
            this.InitializeComponent();
        }

        #region Properties

        // ItemId
        public string ItemId
        {
            get { return (string)GetValue(ItemIdProperty); }
            set { SetValue(ItemIdProperty, value); }
        }

        public static readonly DependencyProperty ItemIdProperty =
            DependencyProperty.Register("ItemId", typeof(string), typeof(IdNameEntry), new PropertyMetadata(""));

        // ItemPath
        public string ItemPath
        {
            get { return (string)GetValue(ItemPathProperty); }
            set { SetValue(ItemPathProperty, value); }
        }

        public static readonly DependencyProperty ItemPathProperty =
            DependencyProperty.Register("ItemPath", typeof(string), typeof(IdNameEntry), new PropertyMetadata(""));

        // ItemName
        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); ; }
            set { SetValue(ItemNameProperty, value); }
        }

        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), typeof(IdNameEntry), new PropertyMetadata(""));

        // IdOnly
        public bool IdOnly
        {
            get { return (bool)GetValue(IdOnlyProperty); }
            set { SetValue(IdOnlyProperty, value); }
        }

        public static readonly DependencyProperty IdOnlyProperty =
            DependencyProperty.Register("IdOnly", typeof(bool), typeof(IdNameEntry), new PropertyMetadata(false));

        #endregion
    }
}
