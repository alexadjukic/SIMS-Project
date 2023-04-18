using InitialProject.Application.UseCases;
using InitialProject.Domain.Models;
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
        }

        public void LoadVouchers()
        {
            foreach(var voucher in _voucherService.LoadAllById(LoggedUser.Id))
            {
                Vouchers.Add(voucher);
            }
        }

        #region COMMANDS
        #endregion
    }
}
