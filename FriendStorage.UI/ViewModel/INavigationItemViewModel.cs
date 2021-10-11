using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public interface INavigationItemViewModel
    {
        string DisplayMember { get; }
        int Id { get; }
        ICommand OpenFriendEditViewCommand { get; }
    }
}