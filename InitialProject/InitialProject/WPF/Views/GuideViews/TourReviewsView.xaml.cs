using InitialProject.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for TourReviewsView.xaml
    /// </summary>
    public partial class TourReviewsView : Page
    {
        public TourReviewsView()
        {
            InitializeComponent();
            this.DataContext = new TourReviewsViewModel();
        }
    }
}
