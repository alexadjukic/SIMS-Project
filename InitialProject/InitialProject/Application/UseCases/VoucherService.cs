using InitialProject.Domain.Models;
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

        public IEnumerable<Voucher> LoadAllById(int userId)
        {
            return _voucherRepository.GetAllByUserId(userId);
        }
    }
}
