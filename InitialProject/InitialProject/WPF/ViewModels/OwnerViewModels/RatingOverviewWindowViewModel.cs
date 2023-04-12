using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RatingOverviewWindowViewModel : ViewModelBase
    {
        private readonly Window _ratingOverviewWindow;

        public RatingOverviewWindowViewModel(Window ratingOverviewWindow)
        {
            _ratingOverviewWindow = ratingOverviewWindow;
        }
    }
}
