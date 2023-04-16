using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RequestDeclinedFormViewModel
    {
        #region PROPERTIES
        private readonly Window _requestDeclinedForm;
        #endregion

        public RequestDeclinedFormViewModel(Window requestDeclinedForm)
        {
            _requestDeclinedForm = requestDeclinedForm;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand CloseWindowCommand { get; }
        public void CloseWindowCommand_Execute(object? parameter)
        {
            _requestDeclinedForm.Close();
        }
        #endregion
    }
}
