using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly LocationService _locationService;
        private readonly CommentService _commentService;
        private readonly AccommodationService _accommodationService;

        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
            _locationService = new LocationService();
            _commentService = new CommentService();
            _accommodationService = new AccommodationService();
        }

        public Forum Save(string status, int locationId, int creatorId)
        {
            if (IsAlreadyCreated(locationId))
            {
                //save comment
                return null;
            }
            else
            {
                //save comment
                return _forumRepository.Save(status, locationId, creatorId);
            }
        }

        private bool IsAlreadyCreated(int locationId)
        {
            return _forumRepository.GetAll().Exists(x => x.LocationId == locationId);
        }

        public IEnumerable<Forum> GetAll()
        {
            var forums = _forumRepository.GetAll();
            foreach(Forum forum in forums)
            {
                forum.Location = _locationService.GetLocationById(forum.LocationId);
            }
            return forums;
        }

        public List<Forum> GetForumsForOwner(User owner)
        {
            List<Forum> forumsForOwner = new List<Forum>();

            IEnumerable<Accommodation> ownersAccommodations = _accommodationService.GetByOwnerId(owner.Id);

            List<Forum> allForums = _forumRepository.GetAll();

            foreach (var forum in allForums)
            {
                if (ownersAccommodations.FirstOrDefault(oa => oa.LocationId == forum.LocationId) != null)
                {
                    forum.Location = ownersAccommodations.FirstOrDefault(oa => oa.LocationId == forum.LocationId).Location;
                    forumsForOwner.Add(forum);
                }
            }

            return forumsForOwner;
        }
    }
}
