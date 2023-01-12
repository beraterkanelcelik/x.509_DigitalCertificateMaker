using MainProject.Business;
using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MainProject.Controllers
{
    public class AuthenticationController : Controller
    {
        readonly UserService userService = new UserService();

        // GET: Authentication
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            ViewBag.isRegistered = false;
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            await userService.AddUser(user);
            await userService.AddUserLog(user.UserMail, LogType.Register);
            ViewBag.isRegistered = true;
            return View("Register");
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(User login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            int RoleID = await userService.Login(login, this.Response); // RoleID == 1 ->User , RoleID ==2 ->Admin
            if (RoleID == 1)
            {
                await userService.AddUserLog(login.UserMail, LogType.Login);
                return RedirectToAction("Index", "Home");
            }
            else if (RoleID == 0)
            {
                ViewBag.NotValidUser = login;
                return View("Login");
            }
            else
            {

                await userService.AddUserLog(login.UserMail, LogType.Login);
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Logout()
        {

            await userService.AddUserLog(User.Identity.Name, LogType.Logout);
            userService.Logout();
            return RedirectToAction("Login", "Authentication");

        }
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> MyProfile()
        {
            return View(await userService.FindUserByMail(User.Identity.Name));
        }
        public async Task<JsonResult> IsUserMailExist(string UserMail)
        {
            User user = await userService.FindUserByMail(UserMail);
            if (user != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}