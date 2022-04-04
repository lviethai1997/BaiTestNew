using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Users;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels.Catalog.Login;

namespace AdminApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var login = await _userService.Login(request);

            if (login == null)
            {
                return View("Error");
            }

            HttpContext.Session.SetString("SessionUser", request.Username);

            var authPropreties = new AuthenticationProperties
            {
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name,request.Username),
            new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authPropreties);

            return RedirectToAction("Index", "Home");
        }

   
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("SessionUser");
            return RedirectToAction("Index", "Login");
        }
    }
}