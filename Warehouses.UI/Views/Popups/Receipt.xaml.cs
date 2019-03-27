using System.Windows;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {

        public Receipt(IReceiptViewModel receiptViewModel)
        {
            InitializeComponent();
            ReceiptTable = receiptViewModel;
            this.DataContext = receiptViewModel;
            Loaded += Receipt_Loaded;
        }

        private void Receipt_Loaded(object sender, RoutedEventArgs e)
        {
            ReceiptTable.Load();
        }
        private IReceiptViewModel ReceiptTable { get; set; }
    }
}
