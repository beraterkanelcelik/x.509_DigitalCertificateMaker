using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MainProject.Models.Classes
{
    public enum LogType
    {
        [Display(Name ="Default Message")]
        Default,
        [Display(Name = "User Registered")]
        Register,
        [Display(Name = "User Logged in")]
        Login,
        [Display(Name = "User Logged out")]
        Logout,
        [Display(Name = "User Created Certificate")]
        CreateCertificate,
        [Display(Name = "User Removed Certificate")]
        RemoveCertificate,
        [Display(Name = "User Downloaded Certificate")]
        DownloadCertificate,
        [Display(Name = "User Converted Certificate")]
        ConvertCertificate,
    }
    public class Log
    {
        [Key]
        public int LogNumber { get; set; }
        public int UserID { get; set; }
        public LogType Type { get; set; }
        public DateTime LogTime { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public Log()
        {

        }
        
    }

}