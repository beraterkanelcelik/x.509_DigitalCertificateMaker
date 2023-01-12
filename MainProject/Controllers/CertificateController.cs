using MainProject.Business;
using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainProject.DAC;
using System.Threading.Tasks;

namespace MainProject.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Certificate
        readonly CertificateService certService = new CertificateService();
        readonly UserService userService = new UserService();

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // GET: Certificate
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(Certificate cert)
        {
            ViewBag.isCreated = false;
            if (!ModelState.IsValid)
            {
                return View("Create");
            }   
            await certService.CreateCertificate(cert, User.Identity.Name);
            await userService.AddUserLog(User.Identity.Name, LogType.CreateCertificate);
            ViewBag.isCreated = true;
            return View();
        }
        [Authorize(Roles = "User,Admin")]
        public async  Task<ActionResult> Delete(int id)
        {
            await certService.RemoveCertificateAsync(certService.FindCertificateById(id));
            await userService.AddUserLog(User.Identity.Name, LogType.RemoveCertificate);
            return RedirectToAction("MyCertificate", "Home", new { area = "" });
        }
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Download(int id,string fileformat)
        {
            System.IO.MemoryStream ms; 
            ms = await certService.MakeCertificate(certService.FindCertificateById(id), fileformat);
            await userService .AddUserLog(User.Identity.Name, LogType.DownloadCertificate);
            return File(ms.ToArray(),"application/x-x509-ca-cert", $"{certService.FindCertificateById(id).CertificateName}.{fileformat}");
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public ActionResult Convert()
        {
            return View();
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<ActionResult> Convert(string fileformat, HttpPostedFileBase file)
        {
            if (file == null)
            {
                return View("Convert");
            }
            System.IO.MemoryStream ms;
            ms = await certService.ConvertCertificate(fileformat, file,User.Identity.Name);
            await userService.AddUserLog(User.Identity.Name, LogType.ConvertCertificate);
            return File(ms.ToArray(), "application/x-x509-ca-cert", $"{file.FileName.Split('.')[0]}.{fileformat}");
        }
        public async Task<JsonResult> IsCertificateNameExist(string CertificateName)
        {
            string userMail = User.Identity.Name;
            int userId = userService.GetUserIDbyUserMail(userMail);
            Certificate certificate = await certService.FindCertificateByName(CertificateName, userId);
            if (certificate != null)
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