﻿using FriendStorage.UI.Event;
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
    public class NavigationItemViewModel : INavigationItemViewModel
    {
        private IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendEditViewCommand = new DelegateCommand(OnFriendEditViewExecute);
            _eventAggregator = eventAggregator;
        }


        public int Id { get; private set; }
        public string DisplayMember { get; private set; }
        public ICommand OpenFriendEditViewCommand { get; private set; }


        private void OnFriendEditViewExecute()
        {
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>()
                .Publish(Id);
        }

    }
}
