using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(HttpContext http, string username, string password, string password2);
        Task<bool> VerifyCredentials(string username, string password, out User user);
        Task SignInUserAsync(HttpContext http, string username);
        Task SignOutUserAsync(HttpContext http);

    }

    public class User
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
    }
}
