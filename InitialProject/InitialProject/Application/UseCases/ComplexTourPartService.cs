using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class ComplexTourPartService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        private readonly IComplexTourPartRepository _complexTourPartRepository;

        public ComplexTourPartService()
        {
            _complexTourRequestRepository = Injector.CreateInstance<IComplexTourRequestRepository>();
            _complexTourPartRepository = Injector.CreateInstance<IComplexTourPartRepository>();
        }

        public ComplexTourPart Save(ComplexTourPart complexTourPart)
        {
            return _complexTourPartRepository.Save(complexTourPart);
        }

        public ComplexTourPart GetByRequest(TourRequest tourRequest)
        {
            return _complexTourPartRepository.GetAll().ToList().Find(p => p.TourRequestId == tourRequest.Id);
        }

        public IEnumerable<ComplexTourPart> GetAllByComplexRequest(ComplexTourRequest complexTourRequest)
        {
            return _complexTourPartRepository.GetAll().Where(p => p.ComplexTourId == complexTourRequest.Id);
        }
    }
}
