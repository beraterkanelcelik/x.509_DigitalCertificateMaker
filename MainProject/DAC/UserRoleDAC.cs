using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProject.DAC
{
    public class UserRoleDAC:BaseRepository<UserRole>
    {
        public UserRole GetUserRoleByRoleId(int id)
        {
            UserRole userRole = new UserRole();
            userRole = Find(x=>x.RoleID == id);
            return userRole;
        }
    }
}