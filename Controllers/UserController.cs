using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task4AppMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Usermain()
        {
            return View();
        }
    }
}
