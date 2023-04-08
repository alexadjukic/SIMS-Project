using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Commands
{
    public class CancelTourCommand : CommandBase
    {
        public Tour? Tour { get; set; }
        private readonly YourToursViewModel _viewModel;

        public CancelTourCommand(YourToursViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return Tour is not null && Tour.Status != TourStatus.CANCELED && Tour.StartTime.Subtract(DateTime.Now).TotalHours > 48;
        }

        public override void Execute(object? parameter)
        {
            Tour.Status = TourStatus.CANCELED;
            _viewModel.LoadFutureTours();
        }
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(YourToursViewModel.SelectedFutureTour))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
