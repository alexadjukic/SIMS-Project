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

        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
            _locationService = new LocationService();
            _commentService = new CommentService();
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
    }
}
