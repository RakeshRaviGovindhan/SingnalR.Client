using SignalRVideoCall.Model;
using SignalRVideoCall.Service;
using SignalRVideoCall.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(UsersService))]
namespace SignalRVideoCall.Service
{
    class UsersService : BaseService, IUsersService
    {
        public UsersService()
        {
        }
      
        public async Task<IEnumerable<UserModel>> GetUserFriendsAsync(long userId)
        {
            return await Get<IEnumerable<UserModel>>($"Users/getMyFriends/{userId}");
        }
    }
}
