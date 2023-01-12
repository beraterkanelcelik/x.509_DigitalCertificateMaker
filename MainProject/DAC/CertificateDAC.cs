using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace MainProject.DAC
{
    public class CertificateDAC:BaseRepository<Certificate>
    {
        readonly UserDAC userDac = new UserDAC();
        public CertificateDAC()
        {
            
        }
        public PagedList.IPagedList<Certificate> GetCertificateListAsync(string searchName, string sort_order, int page,int userID)
        {
            PagedList.IPagedList<Certificate> Result;
            List<Certificate> List;
            if (string.IsNullOrEmpty(searchName))
            {
                switch (sort_order)
                {
                    case "CertNameUp":
                        List = GetAll().Where(x => x.UserID == userID).OrderBy(x => x.CertificateName).ToList();
                        break;
                    case "CertNameDown":
                        List = GetAll().Where(x => x.UserID == userID).OrderByDescending(x => x.CertificateName).ToList();
                        break;
                    case "HashAlgorithmUp":
                        List = GetAll().Where(x => x.UserID == userID).OrderBy(x => x.HashAlgorithm).ToList();
                        break;
                    case "HashAlgorithmDown":
                        List = GetAll().Where(x => x.UserID == userID).OrderByDescending(x => x.HashAlgorithm).ToList();
                        break;
                    case "SignatureAlgorithmUp":
                        List = GetAll().Where(x => x.UserID == userID).OrderBy(x => x.SignatureAlgorithm).ToList();
                        break;
                    case "SignatureAlgorithmDown":
                        List = GetAll().Where(x => x.UserID == userID).OrderByDescending(x => x.SignatureAlgorithm).ToList();
                        break;
                    default:
                        List = GetAll().Where(x => x.UserID == userID).OrderBy(x => x.CertificateName).ToList();
                        break;
                }
                
            }
            else
            {
                switch (sort_order)
                {
                    case "CertNameUp":
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderBy(x => x.CertificateName).ToList();
                        break;
                    case "CertNameDown":
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderByDescending(x => x.CertificateName).ToList();
                        break;
                    case "HashAlgorithmUp":
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderBy(x => x.HashAlgorithm).ToList();
                        break;
                    case "HashAlgorithmDown":
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderByDescending(x => x.HashAlgorithm).ToList();
                        break;
                    case "SignatureAlgorithmUp":
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderBy(x => x.SignatureAlgorithm).ToList();
                        break;
                    case "SignatureAlgorithmDown":
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderByDescending(x => x.SignatureAlgorithm).ToList();
                        break;
                    default:
                        List = GetAll().Where(x => x.UserID == userID).Where(x => x.CertificateName.ToLower().Contains(searchName.ToLower())).OrderBy(x => x.CertificateName).ToList();
                        break;
                }
                
            }
            Result = List.ToPagedList(page,10);
            return Result;
        }
        public Certificate FindCertificateById(int id)
        {
            Certificate certificate = Find(x => x.CertID == id);
            return certificate;
        }
        public void CreateCertificate(Certificate cert,string mail)
        {
            cert.UserID = userDac.FindUserByMail(mail).UserID;
            Add(cert);
            Save();
        }
        public void RemoveCertificate(Certificate cert)
        {
            Remove(cert);
            Save();
        }
        public Certificate FindCertificateByName(string certName,int userId)
        {
            return Find(x=>x.CertificateName == certName && x.UserID == userId);
        }
    }
}