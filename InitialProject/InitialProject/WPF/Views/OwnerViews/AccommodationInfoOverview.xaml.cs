using InitialProject.Domain.Models;
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
            this.DataContext = new AccommodationInfoOverviewViewModel(selectedAccommodation);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                this.Close();
            }

            if (e.Key == Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                RenovateAccommodationForm renovateAccommodationForm = new RenovateAccommodationForm();
                renovateAccommodationForm.Show();
            }

            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                AccommodationStatisticsOverviewWindow accommodationStatisticsOverviewWindow = new AccommodationStatisticsOverviewWindow();
                accommodationStatisticsOverviewWindow.Show();
            }
        }

        private void ButtonRenovate_Click(object sender, RoutedEventArgs e)
        {
            RenovateAccommodationForm renovateAccommodationForm = new RenovateAccommodationForm();
            renovateAccommodationForm.Show();
        }

        private void ButtonStatistics1_Click(object sender, RoutedEventArgs e)
        {
            AccommodationStatisticsOverviewWindow accommodationStatisticsOverviewWindow = new AccommodationStatisticsOverviewWindow();
            accommodationStatisticsOverviewWindow.Show();
        }
    }
}
