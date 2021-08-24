using SignalRVideoCall.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRVideoCall.Service.Interface
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetUserFriendsAsync(long userId);
    }
}
