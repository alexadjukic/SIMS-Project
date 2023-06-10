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
    public class ForumViewModel : ViewModelBase
    {
        #region PROPERTIES
        private Forum? _selectedForum;
        public Forum? SelectedForum
        {
            get
            {
                return _selectedForum;
            }
            set
            {
                if (_selectedForum != value)
                {
                    _selectedForum = value;
                    OnPropertyChanged(nameof(SelectedForum));
                }
            }
        }
        public ObservableCollection<Forum> Forums { get; set; }
        #endregion
        public ForumViewModel()
        {

        }

        #region COMMANDS
        public RelayCommand ViewForumCommand { get; }
        public RelayCommand NewThreadCommand { get; }

        public void ViewForumCommand_Execute(object? parameter)
        {

        }
        #endregion
    }
}
