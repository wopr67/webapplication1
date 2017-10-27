using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private IDictionary<string, Tuple<string, User>> _users =
             new Dictionary<string, Tuple<string, User>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        public UserService(IDictionary<string, string> users)
        {
            foreach (var user in users)
            {
                _users.Add(user.Key.ToLower(),
                    //Tuple.Create(BCrypt.Net.BCrypt.HashPassword(user.Value, 10), new User(user.Key)));
                    Tuple.Create(user.Value, new User(user.Key)));
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
            if (_users.ContainsKey(username.ToLower()))
            {
                return false;
                //return Task.FromResult(false);
            }

            if (password != password2)
            {
                return false;
                //return Task.FromResult(false);
            }

            _users.Add(username.ToLower(),
                //Tuple.Create(BCrypt.Net.BCrypt.HashPassword(password, 10), new User(username)));
                Tuple.Create(password, new User(username)));

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
            var key = username.ToLower();
            if (_users.ContainsKey(key))
            {
                var hash = _users[key].Item1;
                //if (BCrypt.Net.BCrypt.Verify(password, hash))
                if (password == hash)
                {
                    user = _users[key].Item2;
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
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
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
