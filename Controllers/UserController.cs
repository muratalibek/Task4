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
        [HttpGet]
        public IActionResult Login()
        {
            var userName = HttpContext.Session.GetString("User");
            if(string.IsNullOrEmpty(userName) )
            {
                ViewData["Message"] = "Please, Login";
            }
            else
            {
                ViewData["Message"] = "Welcome" + userName;
            }
            return View();
        }
        [HttpGet]
        public IActionResult UserGrid()
        {

            var userName = HttpContext.Session.GetString("User");
            var userList = new List<User>();
            if (string.IsNullOrEmpty(userName))
            {
                ViewData["Message"] = "You do not have access to this page. Please, Login!";
            }
            else
            {
                ViewData["Message"] = "Welcome " + userName;
                userList = _context.Users.ToList();
            }
            return View(userList);
        }
        
        public IActionResult Login(string name, string password)
        {
            
            var user = (from u in _context.Users
                       where u.Name == name && u.Password == password
                       select u).FirstOrDefault();
            if(user == null)
            {
                ViewData["Message"] = "Wrong username & password";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("User", user.Name);
                user.LastLoginTime= DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("UserGrid");
            } 
        }
        [HttpPost]
        public IActionResult Usermain(string name, string email, string password)
        {

            var userName = HttpContext.Session.GetString("User");
            
            if (string.IsNullOrEmpty(userName))
            {
                ViewData["Message"] = "You do not have access to this page. Please, Login!";
            }
            else
            {
                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    RegistrationTime = DateTime.Now,
                    LastLoginTime = null,
                    IsActive = true
                };
                _context.Add(newUser);
            }
            _context.SaveChanges();
            return View();
        }
        /*This code uses the Keys property of the Request.Form collection to get the names of all submitted checkboxes. 
         * It then filters the list to only include those that start with "Delete-", and extracts the ID from each name. 
         * Finally, it loops through the list of IDs to delete and removes the corresponding user records from the database.*/
        [HttpPost("/User/UserGrid")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser()
        {
            var idsToDelete = Request.Form.Keys
                .Where(k => k.StartsWith("Delete-"))
                .Select(k => int.Parse(k.Substring("Delete-".Length)))
                .ToList();

            if (idsToDelete.Count == 0)
            {
                return RedirectToAction(nameof(UserGrid));
            }

            foreach (var id in idsToDelete)
            {
                var result = _context.Users.Find(id);
                if (result != null)
                {
                    _context.Users.Remove(result);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(UserGrid));
        }

        [HttpPost]
        public IActionResult BlockUsers(int[] ids, bool isActive)
        {
            foreach (int id in ids)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.IsActive = isActive;
                    _context.Users.Update(user);
                }
            }
            _context.SaveChanges();
            return Ok();
        }

    }
}
