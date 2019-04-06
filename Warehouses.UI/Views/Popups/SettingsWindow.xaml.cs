using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Warehouses.UI.Events;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public static SettingsEvents settingsEvent = new SettingsEvents();
        public static string languageFileName = "english.xaml";
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = System.Environment.CurrentDirectory;
            string path2 = path.Substring(0, path.LastIndexOf("bin")) + "Resources" + "\\Strings";
            DirectoryInfo d = new DirectoryInfo(path2);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            string[] comboBoxItems = new string[Files.Length];
            int i = 0;
            int selected = -1;
            foreach (FileInfo file in Files)
            {
                string name = file.Name.Split('.')[0];
                if (name.Equals(Properties.Settings.Default.Language))
                    selected = i;
                comboBoxItems[i++] = name;
            }
            languageComboBox.Items.Clear();
            languageComboBox.ItemsSource = comboBoxItems;
            if(i > -1)
            {
                languageComboBox.SelectedIndex = selected;
            }
            settingsEvent.changeLanguage += changeLanguage;
            SettingsWindow.settingsEvent.changeLanguage += changeLanguageEvent;
            SetLanguageDictionary();
        }

        #region language settings
        private void changeLanguageEvent()
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("Resources\\Strings\\" + SettingsWindow.languageFileName, UriKind.Relative);
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
                Properties.Settings.Default.Language = "english";
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
            SettingsWindow.changeLanguageEvent(language);
        }
        #endregion
        string value = "";
        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeLanguageEvent((string)languageComboBox.SelectedItem);
            
        }

        public static void changeLanguageEvent(string newValue)
        {
            string value = newValue;
            languageFileName = value.ToLower() + ".xaml";
            Properties.Settings.Default.Language = value.ToLower();
            Properties.Settings.Default.Save();

            if (settingsEvent.changeLanguage != null)
                settingsEvent.changeLanguage();
        }
        private void changeLanguage()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (value)
            {
                case "English":
                    dict.Source = new Uri("Resources\\Strings\\english.xaml", UriKind.Relative);
                    break;
                case "Arabic":
                    dict.Source = new Uri("Resources\\Strings\\arabic.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("Resources\\Strings\\english.xaml", UriKind.Relative);
                    break;

            }
            this.Resources.MergedDictionaries.Add(dict);
        }
        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
