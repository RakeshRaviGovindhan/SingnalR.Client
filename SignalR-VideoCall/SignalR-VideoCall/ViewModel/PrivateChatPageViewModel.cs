using SignalRVideoCall.Core;
using SignalRVideoCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalRVideoCall.ViewModel
{
    public class PrivateChatPageViewModel : BindableObject
    {
        #region Properties

        private string title { get; set; }
        public string Title
        {
            get => title;
            set
            {
                if (value == title)
                    return;

                title = value;
                OnPropertyChanged();
            }
        }

        private string message { get; set; }
        public string Message
        {
            get => message;
            set
            {
                if (value == message)
                    return;

                message = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<MessageModel> messagesList { get; set; }
        public IEnumerable<MessageModel> MessagesList
        {
            get => messagesList;
            set
            {
                if (value == messagesList)
                    return;

                messagesList = value;
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

        public ICommand SendMessageCommand { get; }
        private async void SendMessage()
        {
            await NativeOperation.ChatService.SendMessage(App.CallToFriend.Email, Message, true);
            AddMessage(App.CallToFriend.Name, Message, true);
        }

        #endregion

        #region Methods

        public async void Initialize()
        {
            Friend = App.CallToFriend;
            Title = Friend.Name;
            CurrentUser = await NativeOperation.SessionService.GetConnectedUser();
            MessagesList = new List<MessageModel>();
            try
            {
                NativeOperation.ChatService.ReceiveMessage(GetMessage, true);
                await NativeOperation.ChatService.Connect(CurrentUser.Email);
            }
            catch (System.Exception exp)
            {
                throw;
            }
        }

        public async void OnNavigatedFrom()
        {
            await NativeOperation.ChatService.Disconnect(CurrentUser.Email);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void GetMessage(string userName, string message)
        {
            AddMessage(userName, message, false);
        }

        private void AddMessage(string userName, string message, bool isOwner)
        {
            var tempList = MessagesList.ToList();
            tempList.Add(new MessageModel { IsOwnerMessage = isOwner, Message = message, UseName = userName });
            MessagesList = new List<MessageModel>(tempList);
            Message = string.Empty;
        }

        #endregion

        public PrivateChatPageViewModel()
        {
            Initialize();
            SendMessageCommand = new Command(SendMessage);
        }

    }
}
