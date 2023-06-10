using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Forum Save(string status, int locationId, int creatorId, string comment)
        {
            if  (GetByLocationId(locationId) != null)
            {
                Forum forum = GetByLocationId(locationId);
                if (forum.Status.Equals("Closed"))
                {
                    forum.Status = status;
                    forum.CreatorId = creatorId;
                    _forumRepository.Update(forum);
                }
                _commentService.Save(forum.Id, comment, creatorId);
                return forum;
            }
            else
            {
                Forum forum = _forumRepository.Save(status, locationId, creatorId);
                _commentService.Save(forum.Id, comment, creatorId);
                return forum;
            }
        }

        private Forum GetByLocationId(int locationId)
        {
            return _forumRepository.GetAll().Find(x => x.LocationId == locationId);
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

        public void Close(Forum forum)
        {
            forum.Status = "Closed";
            _forumRepository.Update(forum);
        }
    }
}
