using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainProject.Models.Classes
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(80)]
        [Index("UserMail", IsUnique = true)]
        [Required(ErrorMessage ="UserMail cannot be empty!")]
        [Remote("IsUserMailExist","Authentication",ErrorMessage ="UserMail already Exist!")]
        public string UserMail { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be empty!")]
        public string Password { get; set; }
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public UserRole UserRole { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public static object Identity { get; internal set; }

        public User()
        {
            RoleID = 1;
        }
    }
}