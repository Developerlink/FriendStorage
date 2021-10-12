using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Events;
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
        private FriendSavedEvent _friendSavedEvent;

        public NavigationViewModelTests()
        {
            // Creating a real event instead of a mock because we want to
            // call the publish method on that event in the test to see if
            // we are subscribing to this event
            _friendSavedEvent = new FriendSavedEvent();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(ea => ea.GetEvent<FriendSavedEvent>())
                .Returns(_friendSavedEvent);

            var navigationDataProviderMock = new Mock<INavigationDataProvider>();
            navigationDataProviderMock.Setup(dp => dp.GetAllFriends()).Returns(new List<LookUpItem>
            {
                new LookUpItem { Id = 1, DisplayMember = "Clark" },
                new LookUpItem { Id = 2, DisplayMember = "Bruce" }
            });

            _viewModel = new NavigationViewModel(navigationDataProviderMock.Object, eventAggregatorMock.Object);
        }

        [Fact]
        public void ShouldLoadFriends()
        {
            _viewModel.Load();

            Assert.Equal(2, _viewModel.Friends.Count);

            var friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Clark", friend.DisplayMember);

            friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 2);
            Assert.NotNull(friend);
            Assert.Equal("Bruce", friend.DisplayMember);
        }

        [Fact]
        public void ShouldLoadFriendsOnlyOnce()
        {
            _viewModel.Load();
            _viewModel.Load();

            Assert.Equal(2, _viewModel.Friends.Count);

            var friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 1);
            Assert.NotNull(friend);
            Assert.Equal("Clark", friend.DisplayMember);

            friend = _viewModel.Friends.SingleOrDefault(f => f.Id == 2);
            Assert.NotNull(friend);
            Assert.Equal("Bruce", friend.DisplayMember);
        }

        [Fact]
        public void ShouldUpdateNavigationItemWhenFriendIsSaved()
        {
            _viewModel.Load();
            var navigationItem = _viewModel.Friends.First();

            var friendId = navigationItem.Id;

            _friendSavedEvent.Publish(
                new Friend
                {
                    Id = friendId,
                    FirstName = "Anna",
                    LastName = "Huber"
                });

            Assert.Equal("Anna Huber", navigationItem.DisplayMember);
        }

        [Fact]
        public void ShouldAddNavigationItemWhenAddedFriendIsSaved()
        {
            _viewModel.Load();

            const int newFriendId = 97;

            _friendSavedEvent.Publish(new Friend
            {
                Id = newFriendId,
                FirstName = "Anna",
                LastName = "Huber"
            });

            Assert.Equal(3, _viewModel.Friends.Count);

            var addedItem = _viewModel.Friends.SingleOrDefault(f => f.Id == newFriendId);
            Assert.NotNull(addedItem);
            Assert.Equal("Anna Huber", addedItem.DisplayMember);
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
