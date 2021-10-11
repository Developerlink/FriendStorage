using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.ViewModel;
using Moq;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FriendStorage.UITests.ViewModel
{
    public class NavigationViewModelTests
    {
        private NavigationViewModel _viewModel;

        public NavigationViewModelTests()
        {
            var eventAggregator = new Mock<IEventAggregator>();
            var navigationDataProviderMock = new Mock<INavigationDataProvider>();
            navigationDataProviderMock.Setup(dp => dp.GetAllFriends()).Returns(new List<LookUpItem>
            {
                new LookUpItem { Id = 1, DisplayMember = "Clark" },
                new LookUpItem { Id = 2, DisplayMember = "Bruce" },
                new LookUpItem { Id = 3, DisplayMember = "Diana" },
                new LookUpItem { Id = 4, DisplayMember = "Harley" },
                new LookUpItem { Id = 5, DisplayMember = "Peter" },
                new LookUpItem { Id = 6, DisplayMember = "MJ" },
                new LookUpItem { Id = 7, DisplayMember = "Peggy" },
                new LookUpItem { Id = 8, DisplayMember = "Natasha" },
                new LookUpItem { Id = 9, DisplayMember = "Wanda" }
            });

            _viewModel = new NavigationViewModel(navigationDataProviderMock.Object, eventAggregator.Object);
        }

        [Fact]
        public void ShouldLoadFriends()
        {
            _viewModel.Load();

            Assert.Equal(9, _viewModel.Friends.Count);

            var friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Clark", friend.DisplayMember);

            friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 9);
            Assert.NotNull(friend);
            Assert.Equal("Wanda", friend.DisplayMember);
        }

        [Fact]
        public void ShouldLoadFriendsOnlyOnce()
        {
            _viewModel.Load();
            _viewModel.Load();

            Assert.Equal(9, _viewModel.Friends.Count);

            var friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Clark", friend.DisplayMember);

            friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 9);
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
