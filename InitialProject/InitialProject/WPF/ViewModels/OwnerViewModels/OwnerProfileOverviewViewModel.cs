using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerProfileOverviewViewModel : ViewModelBase
    {
        private int _numberOfRatings;
        public int NumberOfRagings
        {
            get
            {
                return _numberOfRatings;
            }
            set
            {
                if (_numberOfRatings != value)
                {
                    _numberOfRatings = value;
                    OnPropertyChanged(nameof(NumberOfRagings));
                }
            }
        }

        private double _totalRating;
        public double TotalRating
        {
            get
            {
                return _totalRating;
            }
            set
            {
                if (_totalRating != value)
                {
                    _totalRating = value;
                    OnPropertyChanged(nameof(TotalRating));
                }
            }
        }

        private readonly int _ownerId;

        private readonly Window _ownerProfileOverview;

        public OwnerProfileOverviewViewModel(Window ownerProfileOverview, int ownerId)
        {
            _ownerProfileOverview = ownerProfileOverview;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            _ownerId = ownerId;
        }

        public RelayCommand CloseWindowCommand { get; }
        
        public void CloseWindowCommand_Execute(object? parameter)
        {
            _ownerProfileOverview.Close();
        }
    }
}
