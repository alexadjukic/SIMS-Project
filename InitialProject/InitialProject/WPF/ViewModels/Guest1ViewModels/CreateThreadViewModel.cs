using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class CreateThreadViewModel : ViewModelBase
    {
        #region PROPERTIES
        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        #endregion
        public CreateThreadViewModel()
        {
            CreateThreadCommand = new RelayCommand(CreateThreadCommand_Execute, CreateThreadCommand_CanExecute);
        }

        #region COMMANDS
        public RelayCommand CreateThreadCommand { get; }

        public void CreateThreadCommand_Execute(object? parameter)
        {

        }

        public bool CreateThreadCommand_CanExecute(object? parameter)
        {
            return true;
        }
        #endregion
    }
}
