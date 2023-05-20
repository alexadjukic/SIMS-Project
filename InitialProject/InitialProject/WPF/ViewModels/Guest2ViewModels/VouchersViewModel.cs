using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest2Views;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class VouchersViewModel : ViewModelBase
    {
        #region PROPERTIES
        public User LoggedUser { get; set; }

        public ObservableCollection<Voucher> Vouchers { get; set; }
        private readonly Window _vouchersView;
        private readonly VoucherService _voucherService;
        

        #endregion

        public VouchersViewModel(Window vouchersView, User user)
        {
            _vouchersView = vouchersView;
            LoggedUser = user;
            Vouchers = new ObservableCollection<Voucher>();
            _voucherService = new VoucherService();
            LoadVouchers();

            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowToursViewCommand = new RelayCommand(ShowToursViewCommand_Execute);
            ShowTourRequestsCommand = new RelayCommand(ShowTourRequestsCommand_Execute);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsCommand_Execute);
        }

        public void LoadVouchers()
        {
            foreach(var voucher in _voucherService.LoadAllById(LoggedUser.Id))
            {
                Vouchers.Add(voucher);
            }
        }

        #region COMMANDS

        public RelayCommand ShowToursViewCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }
        public RelayCommand ShowTourRequestsCommand { get; }
        public RelayCommand ShowStatisticsCommand { get; }

        public void ShowStatisticsCommand_Execute(object? parameter)
        {
            RequestedTourStatisticsView requestedTourStatisticsView = new RequestedTourStatisticsView(LoggedUser);
            requestedTourStatisticsView.Show();
            _vouchersView.Close();
        }

        public void ShowTourRequestsCommand_Execute(object? parameter)
        {
            TourRequestFormView tourRequestFormView = new TourRequestFormView(LoggedUser);
            tourRequestFormView.Show();
            _vouchersView.Close();
        }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _vouchersView.Close();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            _vouchersView.Close();
        }

        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
            _vouchersView.Close();
        }

        public void ShowToursViewCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
            _vouchersView.Close();
        }

        #endregion
    }
}
