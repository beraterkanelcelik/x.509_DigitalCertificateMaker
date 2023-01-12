using MainProject.DAC;
using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;

namespace MainProject.Business
{
    public class CertificateService
    {
        readonly UserDAC userDac = new UserDAC();
        readonly CertificateDAC certificateDAC = new CertificateDAC();

        public async Task<PagedList.IPagedList<Certificate>> GetCertificateListAsync(string searchName, string sort_order, int page, int userID)
        {
            PagedList.IPagedList<Certificate> certificates = await Task.Run(() =>
            {
                return certificateDAC.GetCertificateListAsync(searchName,sort_order,page, userID);
            });
            return certificates;
        }

        public async Task CreateCertificate(Certificate cert, string mail)
        {
            await Task.Run(() =>
            {
                certificateDAC.CreateCertificate(cert, mail);
                certificateDAC.Save();
            });
        }
        public Certificate FindCertificateById(int id)
        {
            return certificateDAC.FindCertificateById(id);
        }
        public async Task RemoveCertificateAsync(Certificate certificate)
        {
            await Task.Run(() =>
            {
                certificateDAC.RemoveCertificate(certificate);
                certificateDAC.Save();
            });
        }
        public async Task<MemoryStream> MakeCertificate(Certificate certificate, string fileformat)
        {
            MemoryStream _stream = await Task.Run(() => { 
            if (certificate.HashAlgorithm.ToUpper() == "SHA512")
            {
                if (fileformat == "cer" || fileformat == "der")
                {
                    X509Certificate2 cert = MakeCertificate(HashAlgorithmName.SHA512, certificate, certificate.SignatureAlgorithm);
                    MemoryStream stream = new MemoryStream(cert.Export(X509ContentType.Cert));
                    return stream;
                }
                else
                {
                    X509Certificate2 cert = MakeCertificate(HashAlgorithmName.SHA512, certificate, certificate.SignatureAlgorithm);
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("-----BEGIN CERTIFICATE-----\r\n" + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks) + "\r\n-----END CERTIFICATE-----"));
                    return stream;
                }

            }
            else
            {
                if (fileformat == "cer" || fileformat == "der")
                {
                    X509Certificate2 cert = MakeCertificate(HashAlgorithmName.SHA256, certificate, certificate.SignatureAlgorithm);
                    MemoryStream stream = new MemoryStream(cert.Export(X509ContentType.Cert));
                    return stream;
                }
                else
                {
                    X509Certificate2 cert = MakeCertificate(HashAlgorithmName.SHA256, certificate, certificate.SignatureAlgorithm);
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("-----BEGIN CERTIFICATE-----\r\n" + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks) + "\r\n-----END CERTIFICATE-----"));
                    return stream;
                }
            }
            });
            return _stream;
        }
        private X509Certificate2 MakeCertificate(HashAlgorithmName hashAlgorithmName, Certificate certificate, string signatureAlgorithm)
        {
            if (signatureAlgorithm == "RSA")
            {
                string name = $"CN={certificate.IssuerName}";
                X500DistinguishedName distinguishedName = new X500DistinguishedName(name);
                RSA rsa = RSA.Create(certificate.SignatureBit);
                CertificateRequest request = new CertificateRequest(distinguishedName, rsa, hashAlgorithmName, RSASignaturePadding.Pkcs1);

                request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));
                request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));
                X509Certificate2 cert = request.CreateSelfSigned(certificate.ValidFrom, certificate.ValidTo);
                return cert;
            }
            else
            {
                ECDsa ecdsa = ECDsa.Create();
                CertificateRequest request = new CertificateRequest($"cn={certificate.IssuerName}", ecdsa, hashAlgorithmName);
                X509Certificate2 cert = request.CreateSelfSigned(certificate.ValidFrom, certificate.ValidTo);
                return cert;
            }
        }
        public async Task<MemoryStream> ConvertCertificate(string fileformat, HttpPostedFileBase file, string username)
        {
            return await MakeCertificate(ReadCert(file, username), fileformat);
        }
        public Certificate ReadCert(HttpPostedFileBase file, string username)
        {
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes(file.ContentLength);
            X509Certificate2 cert = new X509Certificate2(binData);
            string issuerName = cert.Issuer.Substring(3);
            string startDateString = cert.GetEffectiveDateString();
            DateTime startDate = Convert.ToDateTime(startDateString);
            string endDateString = cert.GetExpirationDateString();
            DateTime endDate = Convert.ToDateTime(endDateString);
            string hash = cert.SignatureAlgorithm.FriendlyName.Substring(0, 6).ToUpper();
            string signatureAlgorithm = cert.SignatureAlgorithm.FriendlyName.Substring(6).ToUpper();
            string[] fileName = file.FileName.Split('.');
            User currentUser = userDac.FindUserByMail(username);
            if (signatureAlgorithm == "RSA")
            {
                var signatureBit = cert.GetRSAPublicKey().KeySize;
                Certificate newCert = new Certificate
                {
                    CertificateName = fileName[0],
                    HashAlgorithm = hash,
                    SignatureAlgorithm = signatureAlgorithm,
                    IssuerName = issuerName,
                    ValidFrom = startDate,
                    ValidTo = endDate,
                    SignatureBit = signatureBit
                };
                newCert.UserID = currentUser.UserID;
                newCert.User = currentUser;
                return newCert;
            }
            else
            {

                Certificate newCert = new Certificate
                {
                    CertificateName = fileName[0],
                    HashAlgorithm = hash,
                    SignatureAlgorithm = signatureAlgorithm,
                    IssuerName = issuerName,
                    ValidFrom = startDate,
                    ValidTo = endDate,
                };

                newCert.UserID = currentUser.UserID;
                newCert.User = currentUser;
                return newCert;
            }
        }
        public async Task<Certificate> FindCertificateByName(string certName,int userId)
        {
            Certificate certificate =
            await Task.Run(() =>
            {
                return certificateDAC.FindCertificateByName(certName,userId);
            });
            return certificate;
        }
    }
}