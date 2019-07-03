
using System.Collections.Generic;
using IdentityServerAPI.Models;

namespace IdentityAPI.Services
{
    public interface IUserService
    {
        UserDto SignIn(UserDto user);

        List<UserDto> GetAllUsers();
    }
}
