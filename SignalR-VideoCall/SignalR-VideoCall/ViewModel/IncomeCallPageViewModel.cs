using SignalRVideoCall.Core;
using SignalRVideoCall.Model;
using SignalRVideoCall.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalRVideoCall.ViewModel
{
    public class IncomeCallPageViewModel : BindableObject
    {
        #region Properties

        private string friendName { get; set; }
        public string FriendName
        {
            get => friendName;
            set
            {
                if (value == friendName)
                    return;

                friendName = value;
                OnPropertyChanged();
            }
        }

        private UserModel friend { get; set; }
        public UserModel Friend
        {
            get => friend;
            set
            {
                if (value == friend)
                    return;

                friend = value;
                OnPropertyChanged();
            }
        }

        private UserModel currentUser { get; set; }
        public UserModel CurrentUser
        {
            get => currentUser;
            set
            {
                if (value == currentUser)
                    return;

                currentUser = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand AcceptCallCommand { get; }
        private async void AcceptCall()
        {
            await NativeOperation.ChatService.AcceptVideoCall(App.CurrentUser.Email, App.CallFromFriend.Email);
            await Application.Current.MainPage.Navigation.PushAsync(new CallPage());
        }

        public ICommand RejectCallCommand { get; }
        private async void RejectCall()
        {
            await NativeOperation.ChatService.RejectVideoCall(App.CurrentUser.Email, App.CallFromFriend.Email);
            await Application.Current.MainPage.Navigation.PushAsync(new FriendsPage());
        }

        #endregion

        #region Methods

        public async void Initialize()
        {
            CurrentUser = await NativeOperation.SessionService.GetConnectedUser();
            Friend = App.CallFromFriend;
            FriendName = App.CallFromFriend.Name;
            try
            {
                await NativeOperation.ChatService.Connect(CurrentUser.Email);
            }
            catch (System.Exception exp)
            {
                throw;
            }
        }

        #endregion

        public IncomeCallPageViewModel()
        {
            Initialize();
            AcceptCallCommand = new Command(AcceptCall);
            RejectCallCommand = new Command(RejectCall);
        }
    }
}
