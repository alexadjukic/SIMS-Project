using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CheckpointsArrivalWindow.xaml
    /// </summary>
    public partial class CheckpointArrivalWindow : Window, INotifyPropertyChanged
    {
        TourReservationRepository _tourReservationRepository;


        private User _selectedArrivedGuest;
        public User SelectedArrivedGuest
        {
            get => _selectedArrivedGuest;
            set
            {
                if (_selectedArrivedGuest != value)
                {
                    _selectedArrivedGuest = value;
                    OnPropertyChanged(nameof(SelectedArrivedGuest));
                }
            }
        }

        private User _selectedUnarrivedGuest;
        public User SelectedUnarrivedGuest
        {
            get => _selectedUnarrivedGuest;
            set
            {
                if (_selectedUnarrivedGuest != value)
                {
                    _selectedUnarrivedGuest = value;
                    OnPropertyChanged(nameof(SelectedUnarrivedGuest));
                }
            }
        }

        ObservableCollection<User> ArrivedGuests;
        ObservableCollection<User> UnarrivedGuests;
        public CheckpointArrivalWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
