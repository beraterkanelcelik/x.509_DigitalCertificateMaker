using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace MainProject.DAC
{
    public class UserDAC:BaseRepository<User>
    {
        public UserDAC()
        {

        }
        public User FindUserByMail(string mail)
        {

            return Find(x => x.UserMail == mail);
        }
        public PagedList.IPagedList<User> GetUserListWithRolesAsync(string searchMail, string searchRole, int page)
        {
            PagedList.IPagedList<User> Result;
            List<User> List;
            if (string.IsNullOrEmpty(searchMail) && string.IsNullOrEmpty(searchRole))
            {
                List = GetDbContext().Include("UserRole").OrderBy(x => x.UserRole.RoleName).ToList();
            }
            else if (searchMail!=null && string.IsNullOrEmpty(searchRole))
            {
                List = GetDbContext().Include("UserRole").Where(x => x.UserMail.ToLower().Contains(searchMail.ToLower())).OrderBy(x => x.UserRole.RoleName).ToList();
            }
            else if (string.IsNullOrEmpty(searchMail) && searchRole != null)
            {
                List = GetDbContext().Include("UserRole").Where(x => x.UserRole.RoleName.ToLower().Contains(searchRole.ToLower())).OrderBy(x => x.UserRole.RoleName).ToList();
            }
            else
            {
                List = GetDbContext().Include("UserRole").Where(x => x.UserMail.ToLower().Contains(searchMail.ToLower()) && x.UserRole.RoleName.ToLower().Contains(searchRole.ToLower())).OrderBy(x => x.UserRole.RoleName).ToList();
                
            }

            Result = List.ToPagedList(page,10);
            return Result;
        }
    }

}
