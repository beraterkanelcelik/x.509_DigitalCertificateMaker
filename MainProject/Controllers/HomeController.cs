using MainProject.Business;
using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MainProject.Controllers
{
    public class HomeController : Controller
    {
        readonly CertificateService certService = new CertificateService();
        readonly UserService userService = new UserService();

        public async Task<ActionResult> Index()//Home Page
        {
            await Task.Run(() => { 
            return View();
        });
        return View();
    }
        [Authorize(Roles ="User,Admin")]
        public async Task<ActionResult> MyCertificate(string searchName,string sort_order,int page = 1)
        {
            string userMail = User.Identity.Name;
            ViewBag.searchName = searchName;
            ViewBag.sort_order = sort_order;
            return View(await certService.GetCertificateListAsync(searchName,sort_order,page,userService.GetUserIDbyUserMail(userMail)));
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UserList(string searchMail,string searchRole,int page = 1)
        {
            ViewBag.searchMail = searchMail;
            ViewBag.searchRole = searchRole;
            return View(await userService.GetUserListWithRolesAsync(searchMail,searchRole,page));
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UserLogList(DateTime? startDate, DateTime? endDate, string searchMail,string searchMessage, string sort_order, int page = 1)
        {
            ViewBag.sort_order = sort_order;
            ViewBag.searchMail = searchMail;
            ViewBag.searchMessage = searchMessage;
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            return View(await userService.GetLogswithUserAsync(startDate,endDate, page, searchMail,searchMessage, sort_order));
        }
    }
}