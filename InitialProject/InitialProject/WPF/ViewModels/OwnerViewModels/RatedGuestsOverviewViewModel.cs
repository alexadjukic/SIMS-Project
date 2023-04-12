using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    internal class RatedGuestsOverviewViewModel : ViewModelBase
    {
        private readonly Window _ratedGuestsOverview;

        public RatedGuestsOverviewViewModel(Window ratedGuestsOverview)
        {
            _ratedGuestsOverview = ratedGuestsOverview;
        }
    }
}
