using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Classes;
using WebApplication1.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {

        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public class signinwrapper
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class signupwrapper
        {
            public string username { get; set; }
            public string password { get; set; }
            public string password2 { get; set; }
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin ([FromBody] signinwrapper w)
        {
            User user;
            if (await _userService.VerifyCredentials(w.username, w.password, out user))
            {
                await _userService.SignInUserAsync(HttpContext, user.Username);
                return Ok();
            }

            return Unauthorized();
        }


        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup([FromBody] signupwrapper w)
        {
            if (await _userService.AddUserAsync(HttpContext, w.username, w.password, w.password2))
                return Ok();

            return BadRequest();
        }


        [HttpGet]
        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _userService.SignOutUserAsync(HttpContext);
            return Ok();
        }



        [HttpGet]
        [Route("authenticate")]
        public IActionResult Authenticate()
        {
            
            AppUser appUser = new AppUser();
            if (User.Identity.IsAuthenticated)
            {
                appUser.IsAuthenticated = true;
                appUser.Username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return new OkObjectResult(appUser);
            //var modelState = new ModelStateDictionary();
            //modelState.AddModelError("Name", "Name is required.");
            //return BadRequest(modelState);
            //return appUser;
            //return NoContent();
            //return Unauthorized();
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Getx()
        //{
        //    return new string[] { "abc", "xyz" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "csn";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
