using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eShop.Web.Controllers
{
    public class AuthenticateController : Controller
    {
        [Route("/authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery]string usr, [FromQuery]string pwd)
        {
            if (usr == "admin" && pwd == "adminadmin")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usr),
                    new Claim(ClaimTypes.Email, "admin@eshop.com"),
                    new Claim(ClaimTypes.HomePhone, "12345678")
                };

                var userIdentity = new ClaimsIdentity(userClaims, "eShop.CookieAuth");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync("eShop.CookieAuth", userPrincipal);
            }            

            return Redirect("/outstandingorders");
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/outstandingorders");
        }
    }
}
