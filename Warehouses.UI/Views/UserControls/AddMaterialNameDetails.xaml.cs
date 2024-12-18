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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Warehouses.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MaterialNameDetails.xaml
    /// </summary>
    public partial class AddMaterialNameDetails : UserControl
    {
        public AddMaterialNameDetails()
        {
            InitializeComponent();
            Loaded += AddMaterialNameDetails_Loaded;
        }

        private void AddMaterialNameDetails_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(start_input);
        }
    }
}
