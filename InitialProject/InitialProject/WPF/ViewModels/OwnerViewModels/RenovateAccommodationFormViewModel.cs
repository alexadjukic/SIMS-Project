using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RenovateAccommodationFormViewModel : ViewModelBase
    {
        #region PROPERTIES
        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get
            {
                return _selectedAccommodation;
            }
            set
            {
                if (_selectedAccommodation != value)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                }
            }
        }
        #endregion

        public RenovateAccommodationFormViewModel(Accommodation selectedAccommodation) 
        {
            SelectedAccommodation = selectedAccommodation;
        }

        #region COMMANDS
        #endregion
    }
}
