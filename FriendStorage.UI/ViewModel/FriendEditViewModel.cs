using FriendStorage.Model;
using FriendStorage.UI.Command;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Events;
using FriendStorage.UI.Wrapper;
using Prism.Events;
using System;
using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        void Load(int? friendId);
        FriendWrapper Friend { get; set; }
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private IFriendDataProvider _friendDataProvider;
        private readonly IEventAggregator _eventAggregator;

        public FriendEditViewModel(IFriendDataProvider friendDataProvider, IEventAggregator eventAggregator)
        {
            _friendDataProvider = friendDataProvider;
            _eventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);

        }

        public ICommand SaveCommand { get; set; }
        private FriendWrapper _friend;

        public FriendWrapper Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public void Load(int? friendId)
        {
            var friend = friendId.HasValue
                ? _friendDataProvider.GetFriendById(friendId.Value)
                : new Friend();            

            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += Friend_PropertyChanged;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void Friend_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnSaveExecute(object obj)
        {
            _friendDataProvider.SaveFriend(Friend.Model);
            Friend.AcceptChanges();
            _eventAggregator.GetEvent<FriendSavedEvent>()
                .Publish(Friend.Model);
        }

        private bool OnSaveCanExecute(object arg)
        {
            return Friend != null && Friend.IsChanged;
        }
    }
}
