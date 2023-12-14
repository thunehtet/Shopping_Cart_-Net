using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Shopping.Data;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class LoginController : Controller
    {
        //login page		
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Browser");
            }
            return View();
        }

        //when user click "Login" buttom, data will pass to this Action
        public IActionResult Login(IFormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            User user = null;

            //after login, session has alredy been setted(session is not null).
            //so if user type path "Login/Login" agin, they still cannot access the Login page.
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Browser");
            }

            if (string.IsNullOrEmpty(username))
            {
                //id session is null and username is also null, means user hav'n login yet.
                //then direct them to "login/index" page for filling login info.
                ViewBag.message = "";
                return View("Index");
            }
            else
            {
                //if username != null, then call GetUserByUsername() method to get user object.            
                user = UserData.GetUserByUsername(username);
            }

            //check if username and password are matched with database data
            //user object is null means our database doesn't have this username, so we cannot get user object through this username.
            if (user != null && user.Username == username && user.Password == password)
            {
               
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetInt32("id", user.Id);
                var id = HttpContext.Session.GetString("id");
                HttpContext.Session.SetString("clicked", "");
                string url = HttpContext.Session.GetString("returnUrl");
                if (url != null)
                {
                    return Redirect(url);
                }

                return RedirectToAction("Index", "Browser");
            }
            else
            {
                //if username or password isn't matched, or user object is null, show a error prompt message.
                ViewBag.message = "Username or password is wrong. Please enter again.";
                return View("Index");
            }

        }

        public IActionResult Logout()
        {
            //if user alredy login, clear session and logout
            //if user havn't login yet, redirect to login page
            if (HttpContext.Session.GetString("username") != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Browser");
            }
            return View("Index");
        }
        
    }
           

    

    
}
