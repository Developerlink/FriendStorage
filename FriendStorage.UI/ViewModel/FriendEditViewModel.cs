using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using System;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        void Load(int friendId);
        Friend Friend { get; set; }
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel 
    {
        private IFriendDataProvider _friendDataProvider;

        public FriendEditViewModel(IFriendDataProvider friendDataProvider)
        {
            _friendDataProvider = friendDataProvider;
        }

        private Friend _friend;

        public Friend Friend
        {
            get { return _friend; }
            set { _friend = value;
                OnPropertyChanged();
            }
        }
         

        public void Load(int friendId)
        {
            var friend = _friendDataProvider.GetFriendById(friendId);
            Friend = friend;
        }
    }
}
