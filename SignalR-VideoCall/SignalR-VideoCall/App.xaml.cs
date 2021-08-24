using SignalRVideoCall.Model;
using SignalRVideoCall.Service.Interface;
using SignalRVideoCall.View;
using Xamarin.Forms;

namespace SignalRVideoCall
{
    public partial class App : Application
    {
        public static string Email { get; set; }
        public static UserModel CurrentUser { get; set; }
        public static UserModel CallToFriend { get; set; }
        public static UserModel CallFromFriend { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
