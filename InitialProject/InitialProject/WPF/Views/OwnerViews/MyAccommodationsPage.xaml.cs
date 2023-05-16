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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for MyAccommodationsPage.xaml
    /// </summary>
    public partial class MyAccommodationsPage : Page
    {
        public MyAccommodationsPage(User user)
        {
            InitializeComponent();
            this.DataContext = new MyAccommodationsPageViewModel(this, user);
        }

        private void ButtonRenovate_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAccommodations.SelectedValue != null)
            {
                RenovateAccommodationForm renovateAccommodationForm = new RenovateAccommodationForm();
                renovateAccommodationForm.Show();
            }        
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && DataGridAccommodations.SelectedValue != null)
            {
                RenovateAccommodationForm renovateAccommodationForm = new RenovateAccommodationForm();
                renovateAccommodationForm.Show();
            }

            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && DataGridAccommodations.SelectedValue != null)
            {
                AccommodationStatisticsOverviewWindow accommodationStatisticsOverviewWindow = new AccommodationStatisticsOverviewWindow();
                accommodationStatisticsOverviewWindow.Show();
            }
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAccommodations.SelectedValue != null)
            {
                AccommodationStatisticsOverviewWindow accommodationStatisticsOverviewWindow = new AccommodationStatisticsOverviewWindow();
                accommodationStatisticsOverviewWindow.Show();

            }
        }

        private void DataGridAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonRenovate.IsEnabled = true;
            ButtonStatistics.IsEnabled = true;
            ButtonDelete.IsEnabled = true;
        }
    }
}
