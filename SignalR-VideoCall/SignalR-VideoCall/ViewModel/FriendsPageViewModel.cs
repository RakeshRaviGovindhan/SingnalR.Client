using SignalRVideoCall.Core;
using SignalRVideoCall.Model;
using SignalRVideoCall.Service.Interface;
using SignalRVideoCall.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalRVideoCall.ViewModel
{
    public class FriendsPageViewModel : BindableObject
    {

        #region Properties     

        private IEnumerable<UserModel> friendsList { get; set; }
        public IEnumerable<UserModel> FriendsList
        {
            get => friendsList;
            set
            {
                if (value == friendsList)
                    return;

                friendsList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand GoToPrivateDiscussionCommand { get; }
        private async void GoToPrivateDiscussion(object obj)
        {
            App.CallToFriend = obj as UserModel;
            await Application.Current.MainPage.Navigation.PushAsync(new PrivateChatPage());
        }

        public ICommand VideoCallCommand { get; }
        private async void VideoCall(object obj)
        {
            App.CallToFriend = obj as UserModel;
            await NativeOperation.ChatService.CallFriend(App.CallToFriend.Email);
        }

        #endregion

        #region Methods

        public async void Initialize()
        {
            var currentUser = await NativeOperation.SessionService.GetConnectedUser();
            FriendsList = await NativeOperation.UsersService.GetUserFriendsAsync(currentUser.ID);
            try
            {
                NativeOperation.ChatService.ReceivePrivateVideoCall(GetVideoCall);
                NativeOperation.ChatService.AcceptVideoCallByFriend(AcceptVideoCallByFriend);
                await NativeOperation.ChatService.Connect(currentUser.Email);
            }
            catch (System.Exception exp)
            {
                throw;
            }
        }

        private async void AcceptVideoCallByFriend(string currentUser, string friendEmail)
        {
            App.CallToFriend = FriendsList.SingleOrDefault(x => x.Email == currentUser);
            await Application.Current.MainPage.Navigation.PushAsync(new CallPage());
        }

        private async void GetVideoCall(string from)
        {
            App.CallFromFriend = FriendsList.SingleOrDefault(x => x.Email == from);
            await Application.Current.MainPage.Navigation.PushAsync(new IncomeCallPage());
        }

        #endregion

        public FriendsPageViewModel()
        {
            GoToPrivateDiscussionCommand = new Command(GoToPrivateDiscussion);
            VideoCallCommand = new Command(VideoCall);
            Initialize();
        }
    }
}
