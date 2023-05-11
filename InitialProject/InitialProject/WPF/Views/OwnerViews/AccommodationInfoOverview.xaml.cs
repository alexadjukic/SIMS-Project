﻿using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.OwnerViewModels;
using System;
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

namespace InitialProject.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for AccommodationInfoOverview.xaml
    /// </summary>
    public partial class AccommodationInfoOverview : Window
    {
        public AccommodationInfoOverview(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            this.DataContext = new AccommodationInfoOverviewViewModel(this, selectedAccommodation);
        }
    }
}
