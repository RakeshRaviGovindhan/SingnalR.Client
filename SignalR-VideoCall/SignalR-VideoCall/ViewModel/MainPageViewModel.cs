using SignalRVideoCall.Core;
using SignalRVideoCall.Model;
using SignalRVideoCall.Service;
using SignalRVideoCall.Service.Interface;
using SignalRVideoCall.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalRVideoCall.ViewModel
{
    public class MainPageViewModel : BindableObject
    {
        
        #region Properties

        private bool isLoading { get; set; }
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                if (value == isLoading)
                    return;

                isLoading = value;
                OnPropertyChanged();
            }
        }

        private string email { get; set; }
        public string Email
        {
            get => email;
            set
            {
                if (value == email)
                    return;

                email = value;
                OnPropertyChanged();
            }
        }

        private string pwd { get; set; }
        public string Pwd
        {
            get => pwd;
            set
            {
                if (value == pwd)
                    return;

                pwd = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand SignInTappedCommand { get; }
        private async void SignInTapped()
        {
            if (string.IsNullOrEmpty(Email.Trim()) && string.IsNullOrEmpty(Pwd.Trim()))
            {
                await Application.Current.MainPage.DisplayAlert("", "Fields Should not be empty", "OK");
                return;
            }

            var user = await NativeOperation.AuthenticationService.Login(new LoginModel { Email = Email, Password = Pwd });
            if (user != null)
            {
                App.CurrentUser = user;
                await NativeOperation.SessionService.SetConnectedUser(user);

                await NativeOperation.SessionService.SetToken(new TokenModel
                {
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    TokenExpireTime = user.TokenExpireTimes
                });

                await Application.Current.MainPage.Navigation.PushAsync(new FriendsPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Login error", "Cancel");
            }
        }

        #endregion

        public MainPageViewModel()
        {
            SignInTappedCommand = new Command(SignInTapped);
        }
    }
}
