using System;
using System.Windows;
using System.Windows.Input;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for AddUnit.xaml
    /// </summary>
    public partial class AddUnit : Window
    {
        public AddUnit(IAddUnitViewModel addUnitViewModel)
        {
            InitializeComponent();
            AddUnitViewModel = addUnitViewModel;
            this.DataContext = addUnitViewModel;
            Loaded += AddUnit_Loaded;
        }

        private void AddUnit_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(start_point);
            AddUnitViewModel.Load();
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

        public IAddUnitViewModel AddUnitViewModel { get; set; }
    }
}
