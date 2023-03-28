using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4AppMvc.Models;

namespace Task4AppMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _context;
        public UserController(ILogger<UserController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Usermain()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Usermain(string name, string email, string password)
        {
            var newUser = new User {
                Name = name,
                Email = email,
                Password = password,
                RegistrationTime = DateTime.Now,
                LastLoginTime = null,
                IsActive = true
            };
            _context.Add(newUser);
            _context.SaveChanges();
            return View();
        }
    }
}
