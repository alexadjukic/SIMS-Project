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
        private readonly Window _ownerProfileOverview;

        public OwnerProfileOverviewViewModel(Window ownerProfileOverview)
        {
            _ownerProfileOverview = ownerProfileOverview;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
        }

        public RelayCommand CloseWindowCommand { get; }
        
        public void CloseWindowCommand_Execute(object? parameter)
        {
            _ownerProfileOverview.Close();
        }
    }
}
