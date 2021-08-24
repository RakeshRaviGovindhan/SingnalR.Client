using Newtonsoft.Json;
using SignalRVideoCall.Core;
using SignalRVideoCall.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalRVideoCall.ViewModel
{
    public class CallPageViewModel : BindableObject
    {

        #region Properties

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

        private bool isAudioActive { get; set; }
        public bool IsAudioActive
        {
            get => isAudioActive;
            set
            {
                if (value == isAudioActive)
                    return;

                isAudioActive = value;
                OnPropertyChanged();
            }
        }

        private bool isVideoActive { get; set; }
        public bool IsVideoActive
        {
            get => isVideoActive;
            set
            {
                if (value == isVideoActive)
                    return;

                isVideoActive = value;
                OnPropertyChanged();
            }
        }

        private string callWebViewSource { get; set; }
        public string CallWebViewSource
        {
            get => callWebViewSource;
            set
            {
                if (value == callWebViewSource)
                    return;

                callWebViewSource = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands


        public ICommand EndCallCommand { get; }
        private void EndCall()
        {
            CallWebViewSource = "about:blank";
        }

        #endregion

        #region Methods

        public async void Initialize()
        {
            CurrentUser = await NativeOperation.SessionService.GetConnectedUser();
            Friend = App.CallFromFriend;
        }

        #endregion

        public CallPageViewModel()
        {
            Initialize();
            EndCallCommand = new Command(EndCall);
        }

    }
}
