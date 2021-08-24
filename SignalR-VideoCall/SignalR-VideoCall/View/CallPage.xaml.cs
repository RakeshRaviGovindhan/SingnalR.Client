using SignalRVideoCall.Service.Interface;
using SignalRVideoCall.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SignalRVideoCall.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallPage : ContentPage
    {
        private CallPageViewModel ViewModel;

        public CallPage()
        {
            InitializeComponent();

            var urlSource = new UrlWebViewSource();
            string baseUrl = DependencyService.Get<IWebViewService>().GetContent();
            urlSource.Url = baseUrl;
            CallWebView.Source = urlSource;
            ViewModel = BindingContext as CallPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);

            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                var response = await Permissions.RequestAsync<Permissions.Camera>();
                if (response != PermissionStatus.Granted)
                {
                }
            }

            var statusMic = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (statusMic != PermissionStatus.Granted)
            {
                var response = await Permissions.RequestAsync<Permissions.Microphone>();
                if (response != PermissionStatus.Granted)
                {
                }
            }
            await Connect();
        }

        private async Task Connect()
        {
            try
            {
                await CallWebView.EvaluateJavaScriptAsync($"init('{ViewModel.CurrentUser.ID}');");
                await Task.Delay(2000);
                await CallWebView.EvaluateJavaScriptAsync($"startCall('{ViewModel.Friend.ID}');");
            }
            catch (Exception exp)
            {

            }
        }

        private async void ToggelCameraClick(object sender, EventArgs e)
        {
            ViewModel.IsVideoActive = !ViewModel.IsVideoActive;
            await CallWebView.EvaluateJavaScriptAsync($"toggleVideo('{ViewModel.IsVideoActive.ToString().ToLower()}');");
        }

        private async void ToggleMicClick(object sender, EventArgs e)
        {
            ViewModel.IsAudioActive = !ViewModel.IsAudioActive;
            await CallWebView.EvaluateJavaScriptAsync($"toggleAudio('{ViewModel.IsAudioActive.ToString().ToLower()}');");
        }
    }
}