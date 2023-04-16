using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RequestsOverviewViewModel
    {
        #region PROPERTIES

        public ObservableCollection<Request> Requests { get; set; }

        private readonly RequestService _requestService;
        private readonly Window _requestsOverview;
        #endregion

        public RequestsOverviewViewModel(Window requestsOverview)
        {
            _requestsOverview = requestsOverview;
            _requestService = new RequestService();

            Requests = new ObservableCollection<Request>();
            LoadOnHoldRequests();

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            DeclineRequestCommand = new RelayCommand(DeclineRequestCommand_Execute, DeclineRequestCommand_CanExecute);
        }

        public void LoadOnHoldRequests()
        {
            Requests.Clear();

            foreach (var request in _requestService.GetOnHoldRequests())
            {
                Requests.Add(request);
            }
        }

        #region COMMANDS
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand DeclineRequestCommand { get; }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            _requestsOverview.Close();
        }

        public bool DeclineRequestCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void DeclineRequestCommand_Execute(object? parameter)
        {
            RequestDeclinedForm requestDeclinedForm = new RequestDeclinedForm();
            requestDeclinedForm.Show();
        }
        #endregion
    }
}
