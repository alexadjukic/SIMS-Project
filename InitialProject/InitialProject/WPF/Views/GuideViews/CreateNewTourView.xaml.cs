﻿using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.WPF.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for CreateNewTourView.xaml
    /// </summary>
    public partial class CreateNewTourView : Window
    {
        public CreateNewTourView(User guide)
        {
            InitializeComponent();
            this.DataContext = new CreateNewTourViewModel(guide);
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
