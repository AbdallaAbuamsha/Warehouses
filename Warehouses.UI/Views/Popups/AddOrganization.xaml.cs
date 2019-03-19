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
        }
        public IAddOrganizationViewModels AddOrganizationViewModels { get; set; }
    }
}