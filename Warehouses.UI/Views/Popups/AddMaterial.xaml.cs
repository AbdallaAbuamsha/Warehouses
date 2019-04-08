using System;
using System.Windows;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for AddMaterial.xaml
    /// </summary>
    public partial class AddMaterial : Window
    {
        public AddMaterial(IAddMaterialViewModel addMaterialViewModel)
        {
            InitializeComponent();
            AddMaterialViewModel = addMaterialViewModel;
            this.DataContext = addMaterialViewModel;
            Loaded += AddMaterial_Loaded;
        }

        private void AddMaterial_Loaded(object sender, RoutedEventArgs e)
        {
            AddMaterialViewModel.Load();
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

        public IAddMaterialViewModel AddMaterialViewModel { get; set; }
    }
}
