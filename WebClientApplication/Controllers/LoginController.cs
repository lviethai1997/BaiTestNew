using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Users;
using System.Threading.Tasks;
using ViewModels.Catalog.Login;

namespace WebClientApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public const string UserKey = "SessionUser";
        private readonly INotyfService _notyf;

        public LoginController(IUserService userService, INotyfService notyfService)
        {
            _userService = userService;
            _notyf = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var login = await _userService.Login(request);

            if (login == false)
            {
                _notyf.Error("Tài khoản hoặc mật khẩu không đúng");
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString(UserKey, request.Username);
            _notyf.Success("Đăng nhập thành công!");
            return RedirectToAction("Index", "Home");
        }
    }
}