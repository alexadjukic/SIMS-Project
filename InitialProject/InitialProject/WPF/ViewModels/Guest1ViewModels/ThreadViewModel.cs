using InitialProject.Commands;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class ThreadViewModel : ViewModelBase
    {
        #region PROPERTIES
        private Comment? _newComment;
        public Comment? NewComment
        {
            get
            {
                return _newComment;
            }
            set
            {
                if (_newComment != value)
                {
                    _newComment = value;
                    OnPropertyChanged(nameof(NewComment));
                }
            }
        }
        public Forum SelectedForum { get; set; }
        public ObservableCollection<Comment> Comments { get; set; }
        #endregion
        public ThreadViewModel()
        {

        }

        #region COMMANDS
        public RelayCommand PostCommentCommand { get; }
        #endregion
    }
}
