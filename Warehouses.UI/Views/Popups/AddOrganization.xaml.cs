﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for AddOrganization.xaml
    /// </summary>
    public partial class AddOrganization : Window
    {
        public AddOrganization(IAddOrganizationViewModels addOrganizationViewModels)
        {
            InitializeComponent();
            AddOrganizationViewModels = addOrganizationViewModels;
            this.DataContext = addOrganizationViewModels;
            Loaded += AddOrganization_Loaded;
        }

        private void AddOrganization_Loaded(object sender, RoutedEventArgs e)
        {
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

        public IAddOrganizationViewModels AddOrganizationViewModels { get; set; }
    }
}
