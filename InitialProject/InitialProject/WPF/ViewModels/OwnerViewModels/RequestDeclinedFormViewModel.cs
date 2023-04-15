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
        private readonly Window _requestDeclinedForm;

        public RequestDeclinedFormViewModel(Window requestDeclinedForm)
        {
            _requestDeclinedForm = requestDeclinedForm;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
        }

        public RelayCommand CloseWindowCommand { get; }
        public void CloseWindowCommand_Execute(object? parameter)
        {
            _requestDeclinedForm.Close();
        }
    }
}
