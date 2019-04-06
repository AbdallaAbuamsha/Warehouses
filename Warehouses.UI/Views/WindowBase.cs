using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Warehouses.UI.Views.Popups;

namespace Warehouses.UI.Views
{
    public abstract class WindowBase : Window
    {
        protected Grid _root;
        public object MyDataContext;
        public WindowBase()
        {
            MyDataContext = DataContext;
            Loaded += WindowBase_Loaded;
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            _root = GetRoot();
            SettingsWindow.settingsEvent.changeLanguage += ChangeLanguage;
            SetLanguageDictionary();
        }

        public abstract Grid GetRoot();

        protected virtual void ChangeLanguage()
        {
            ResourceDictionary dict = new ResourceDictionary();
            var path = @"Resources\Strings\" + SettingsWindow.languageFileName;
            dict.Source = new Uri(path, UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dict);

            if (SettingsWindow.languageFileName.Equals("العربية.xaml"))
            {
                _root.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                _root.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        protected void SetLanguageDictionary()
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
    }
}
