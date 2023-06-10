using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        private readonly IComplexTourPartRepository _complexTourPartRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = Injector.CreateInstance<IComplexTourRequestRepository>();
            _complexTourPartRepository = Injector.CreateInstance<IComplexTourPartRepository>();
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return _complexTourRequestRepository.Save(complexTourRequest);
        }

    }
}
