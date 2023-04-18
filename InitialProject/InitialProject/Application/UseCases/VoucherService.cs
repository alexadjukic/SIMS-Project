﻿using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();
        }

        public void RemoveVoucher(Voucher voucher)
        {
            _voucherRepository.Remove(voucher);
        }

        public IEnumerable<Voucher> LoadAllById(int userId)
        {

            var vouchersTemp = _voucherRepository.GetAll();//.Where(x => x.UserId == userId).ToList();
            var vouchers = new List<Voucher>();

            foreach(var v in vouchersTemp)
            {
                if(v.UserId == userId)
                {
                    vouchers.Add(v);
                }
            }

            foreach (var voucher in vouchers.ToList())
            {
                if(DateTime.Compare(DateTime.Now, voucher.ExpiryDate) >= 0)
                {
                    vouchers.Remove(voucher);
                }
            }
            return vouchers;
        }
    }
}
