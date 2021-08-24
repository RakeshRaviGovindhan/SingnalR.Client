using SignalRVideoCall.Model;
using System.Threading.Tasks;

namespace SignalRVideoCall.Service.Interface
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Makes login for user.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>The authenticated user.Null if not.</returns>
        Task<UserModel> Login(LoginModel loginModel);

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>This methode returns nothing.</returns>
        Task LogOut();

        /// <summary>
        /// Refresh user token.
        /// </summary>
        /// <param name="token">The old token.</param>
        /// <returns>Returns the user with the new token.</returns>
        Task<UserModel> RefreshToken(string token);
    }
}
