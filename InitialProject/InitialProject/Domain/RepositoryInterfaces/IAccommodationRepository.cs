using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    internal interface IAccommodationRepository
    {
        public List<Accommodation> GetAll();
        public Accommodation Save(string name, Location location, AccommodationType type, string capacity, string minDaysForStay, string minDaysBeforeCancel, int ownerId, LocationRepository _repositoryLocation);
        public int NextId();
        public List<int> AccommodationIdsByOwnerId(int ownerId);
        Accommodation GetById(int accommodationId);
    }
}
