using SignalRVideoCall.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SignalRVideoCall.Core
{
    public static class NativeOperation
    {
        public static IAuthenticationService AuthenticationService { get; } = DependencyService.Get<IAuthenticationService>();
        public static IChatService ChatService { get; } = DependencyService.Get<IChatService>();
        public static ISessionService SessionService { get; } = DependencyService.Get<ISessionService>();
        public static IUsersService UsersService { get; } = DependencyService.Get<IUsersService>();
        public static IWebViewService WebViewService { get; } = DependencyService.Get<IWebViewService>();
    }
}
