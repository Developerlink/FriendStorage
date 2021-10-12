using FriendStorage.UI.Events;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase, INavigationItemViewModel
    {
        private IEventAggregator _eventAggregator;
        private string displayMember;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendEditViewCommand = new DelegateCommand(OnFriendEditViewExecute);
            _eventAggregator = eventAggregator;
        }


        public int Id { get; private set; }
        public string DisplayMember 
        { 
            get => displayMember;
            set
            {
                displayMember = value;
                OnPropertyChanged();
            }
        }
        public ICommand OpenFriendEditViewCommand { get; private set; }


        private void OnFriendEditViewExecute()
        {
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>()
                .Publish(Id);
        }

    }
}
