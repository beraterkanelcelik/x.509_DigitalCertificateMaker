using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainProject.Models.Classes
{
    public class Certificate : IValidatableObject
    {
        [Key]
        public int CertID { get; set; }
        [Required(ErrorMessage = "Certificate Name cannot be empty!")]
        [StringLength(20)]
        [Remote("IsCertificateNameExist", "Certificate", ErrorMessage = "Certificate Name already Exist!")]
        public string CertificateName { get; set; }
        [Required(ErrorMessage = "Issuer Name cannot be empty!")]
        public string IssuerName { get; set; }
        [Required(ErrorMessage = "HashAlgoritm Name cannot be empty!")]
        public string HashAlgorithm { get; set; }
        [Required(ErrorMessage = "SignatureAlgorithm Name cannot be empty!")]
        public string SignatureAlgorithm { get; set; }
        public int SignatureBit { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "ValidFrom Date cannot be empty!")]
        public DateTime ValidFrom { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "ValidTo Date cannot be empty!")]
        public DateTime ValidTo { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ValidTo < ValidFrom)
            {
                yield return new ValidationResult("Valid from Date must be greater than Valid To Date");
            }
        }
    }
}