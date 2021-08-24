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
    public class ChatRoomPageViewModel : BindableObject
    {
        #region Properties

        private string userName { get; set; }
        public string UserName
        {
            get => userName;
            set
            {
                if (value == userName)
                    return;

                userName = value;
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

        #endregion

        #region Commands

        public ICommand SendMessageCommand { get; }
        private void SendMessage()
        {
            NativeOperation.ChatService.SendMessage(UserName, Message);
            AddMessage(UserName, Message, true);
        }

        #endregion

        #region Methods

        public async void Initialize()
        {
            //UserName = parameters.GetValue<string>("UserNameId");
            MessagesList = new List<MessageModel>();
            try
            {
                NativeOperation.ChatService.ReceiveMessage(GetMessage);
                await NativeOperation.ChatService.Connect(App.CurrentUser.Email);//Doubt
            }
            catch (System.Exception exp)
            {
                throw;
            }

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

        public ChatRoomPageViewModel()
        {
            Initialize();
            SendMessageCommand = new Command(SendMessage);
        }
    }
}
