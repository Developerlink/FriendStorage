using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FriendStorage.UITests.ViewModel
{
    public class NavigationViewModelTests
    {
        [Fact]
        public void ShouldLoadFriends()
        {
            var viewModel = new NavigationViewModel(new NavigationDataProviderMock());

            viewModel.Load();

            Assert.Equal(9, viewModel.Friends.Count);

            var friend = viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Clark", friend.DisplayMember);

            friend = viewModel.Friends.SingleOrDefault(f => f.Id == 9);
            Assert.NotNull(friend);
            Assert.Equal("Wanda", friend.DisplayMember);
        }

        [Fact]
        public void ShouldLoadFriendsOnlyOnce()
        {
            var viewModel = new NavigationViewModel(new NavigationDataProviderMock());

            viewModel.Load();
            viewModel.Load();

            Assert.Equal(9, viewModel.Friends.Count);

            var friend = viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Clark", friend.DisplayMember);

            friend = viewModel.Friends.SingleOrDefault(f => f.Id == 9);
            Assert.NotNull(friend);
            Assert.Equal("Wanda", friend.DisplayMember);
        }
    }

    public class NavigationDataProviderMock : INavigationDataProvider
    {
        public IEnumerable<LookUpItem> GetAllFriends()
        {
            yield return new LookUpItem { Id = 1, DisplayMember = "Clark" };
            yield return new LookUpItem { Id = 2, DisplayMember = "Bruce" };
            yield return new LookUpItem { Id = 3, DisplayMember = "Diana" };
            yield return new LookUpItem { Id = 4, DisplayMember = "Harley" };
            yield return new LookUpItem { Id = 5, DisplayMember = "Peter" };
            yield return new LookUpItem { Id = 6, DisplayMember = "MJ" };
            yield return new LookUpItem { Id = 7, DisplayMember = "Peggy" };
            yield return new LookUpItem { Id = 8, DisplayMember = "Natasha" };
            yield return new LookUpItem { Id = 9, DisplayMember = "Wanda" };
        }
    }
}
