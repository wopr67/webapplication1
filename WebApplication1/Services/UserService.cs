using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Classes;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private static int _nextUserId = 100;
        private List<User> _users = new List<User>();

        public UserService(List<User> seedusers)
        {
            _users = new List<User>();

            foreach (User u in seedusers)
            {
                _users.Add(new User(u.Username.ToLower(), Crypto.HashPassword(u.Password), _nextUserId++));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="http"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="password2"></param>
        /// <returns></returns>
        public async Task<bool> AddUserAsync(HttpContext http, string username, string password, string password2)
        {
            if (_users.Where (x=>x.Username == username).Any())//.ContainsKey(username.ToLower()))
            {
                return false;
            }

            if (password != password2)
            {
                return false;
            }

            _users.Add(new User(username.ToLower(), Crypto.HashPassword(password), _nextUserId++));

            await SignInUserAsync(http, username);

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> VerifyCredentials(string username, string password, out User user)
        {
            User u = _users.Where(x => x.Username == username.ToLower()).FirstOrDefault();
            if (u != null)
            {
                var hash = u.Password;
                if (Crypto.HashPassword(password) == hash)
                {
                    user = u;
                    return Task.FromResult(true);
                }
            }
            user = null;
            return Task.FromResult(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="http"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task SignInUserAsync(HttpContext http, string username)
        {
            User u = _users.Where(x => x.Username == username.ToLower()).FirstOrDefault();
            if (u!=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, u.ID.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public async Task SignOutUserAsync(HttpContext http)
        {
            await http.SignOutAsync();
        }
    }
}
