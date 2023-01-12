using MainProject.DAC;
using MainProject.Models.Classes;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace MainProject.Business
{
    public class UserService
    {
        readonly UserDAC userDAC = new UserDAC();
        readonly LogDAC logDAC = new LogDAC();
        public async Task AddUser(User user)
        {

            user.Password = HashPassword(user.Password);
            await Task.Run(() =>
            {
                userDAC.Add(user);
                userDAC.Save();
            });


        }

        public async Task<int> Login(User user, HttpResponseBase responseBase)
        {
            int loginint = await Task.Run(() =>
            {
                User newUser = userDAC.FindUserByMail(user.UserMail);
                if (HashPassword(user.Password) == newUser.Password)
                {
                    if (newUser != null)
                    {
                        FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(newUser.UserMail, true, 3000);
                        string Encrypt = FormsAuthentication.Encrypt(Ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypt)
                        {
                            Expires = DateTime.Now.AddHours(3000),
                            HttpOnly = true
                        };
                        responseBase.Cookies.Add(cookie);
                        return newUser.RoleID;
                    }
                }
                return 0;
            });

            return loginint;
        }
        //public async Task LogoutAsync()
        //{
        //    await Task.Run(() =>
        //    {
        //    FormsAuthentication.SignOut();
        //    });

        //}
        public void Logout()
        {

                FormsAuthentication.SignOut();


        }
        public string HashPassword(string password)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hashedPassword;
            }
        }
        public async Task AddUserLog(string userName, LogType LogType)
        {
            await Task.Run(() =>
            {
                logDAC.AddUserLog(userDAC.FindUserByMail(userName), LogType);
            });
        }
        public async Task<User> FindUserByMail(string mail)
        {
            User user = await Task.Run(() =>
            {
                return userDAC.FindUserByMail(mail);
            });
            return user;
        }
        public async Task<PagedList.IPagedList<User>> GetUserListWithRolesAsync(string searchMail, string searchRole, int page)
        {
            PagedList.IPagedList<User> users = await Task.Run(() =>
            {
                return userDAC.GetUserListWithRolesAsync(searchMail, searchRole, page);
            });
            return users;
        }
        public async Task<PagedList.IPagedList<Log>> GetLogswithUserAsync(DateTime? startDate, DateTime? endDate, int page,string searchMail, string searchMessage, string sort_order)
        {
            PagedList.IPagedList<Log> logs = await Task.Run(() =>
            {
                return logDAC.GetLogswithUserAsync(startDate, endDate,page, searchMail,searchMessage, sort_order);
            });
            return logs;
        }
        public int GetUserIDbyUserMail(string userMail)
        {
            User user =userDAC.Find(x=>x.UserMail==userMail);
            return user.UserID;
        }
    }
}