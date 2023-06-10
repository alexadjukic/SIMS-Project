using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserService _userService;

        public CommentService()
        {
            _commentRepository = Injector.CreateInstance<ICommentRepository>();
            _userService = new UserService();
        }
    
        public Comment Save(int forumId, string text, int userId)
        {
            return _commentRepository.Save(new Comment(-1, forumId, text, userId));
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public List<Comment> GetAllByForumId(int forumId)
        {
            List<Comment> comments = GetAll().FindAll(c => c.ForumId == forumId);
            comments = LoadUsers(comments);
            return comments;
        }

        public List<Comment> LoadUsers(List<Comment> comments)
        {
            List<Comment> updatedComments = new List<Comment>();

            foreach (var comment in comments)
            {
                comment.User = _userService.GetById(comment.UserId);
                updatedComments.Add(comment);
            }

            return updatedComments;
        }

        public void ReportComment(Comment selectedComment)
        {
            selectedComment.NumberOfReports++;
            _commentRepository.Update(selectedComment);
        }
    }
}
