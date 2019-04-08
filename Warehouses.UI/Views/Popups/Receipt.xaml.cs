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
            SettingsWindow.settingsEvent.changeLanguage += ChangeLanguage;
            SetLanguageDictionary();
        }

        private void ChangeLanguage()
        {
            ResourceDictionary dict = new ResourceDictionary();
            var path = @"Resources\Strings\" + SettingsWindow.languageFileName;
            dict.Source = new Uri(path, UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dict);

            if (SettingsWindow.languageFileName.Equals("العربية.xaml"))
            {
                root.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                root.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void SetLanguageDictionary()
        {
            string language = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(language))
            {
                language = Properties.Settings.Default.Language = "english";
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
            SettingsWindow.changeLanguageEvent(language);
        }
        private IReceiptViewModel ReceiptTable { get; set; }
    }
}
