using SignalRVideoCall.Core;
using SignalRVideoCall.Model;
using SignalRVideoCall.Service;
using SignalRVideoCall.Service.Interface;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace SignalRVideoCall.Service
{
    class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        /// <summary>
        /// Makes login for user.
        /// </summary>
        /// <param name="loginDto">The login dto.</param>
        /// <returns>The authenticated user.Null if not.</returns>
        public async Task<UserModel> Login(LoginModel loginDto)
        {
            return await Post<UserModel, LoginModel>("Users/login", loginDto);
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>This methode returns nothing.</returns>
        public async Task LogOut()
        {
            await NativeOperation.SessionService.LogOut();
        }

        public async Task<UserModel> RefreshToken(string token)
        {
            return await PostRefresh<UserModel, string>("Users/refresh", token);
        }
    }
}
