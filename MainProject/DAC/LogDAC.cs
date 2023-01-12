using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
namespace MainProject.DAC
{
    public class LogDAC : BaseRepository<Log>
    {
        public void AddUserLog(User user, LogType type)
        {
            Log newLog = new Log
            {
                UserID = user.UserID,
                LogTime = DateTime.UtcNow,
                Type = type,
            };
            Add(newLog);
            Save();
        }
        public List<Log> GetLogList()
        {
            return (List<Log>)GetAll();
        }
        public PagedList.IPagedList<Log> GetLogswithUserAsync(DateTime? startDate, DateTime? endDate, int page, string searchMail, string searchMessage, string sort_order)
        {
            

            IPagedList<Log> result;
            List<Log> List;
            if (string.IsNullOrEmpty(searchMail) && string.IsNullOrEmpty(searchMessage))
            {
                switch (sort_order)
                {
                    case "UserMailUp":
                        List = GetDbContext().Include("User").OrderBy(x => x.User.UserMail).ToList();
                        break;
                    case "UserMailDown":
                        List = GetDbContext().Include("User").OrderByDescending(x => x.User.UserMail).ToList();
                        break;
                    case "LogMessageUp":
                        List = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageDown":
                        List = GetDbContext().Include("User").OrderByDescending(x => x.Type).ToList();
                        break;
                    case "LogTimeUp":
                        List = GetDbContext().Include("User").OrderBy(x => x.LogTime).ToList();
                        break;
                    case "LogTimeDown":
                        List = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    default:
                        List = GetDbContext().Include("User").OrderBy(x => x.LogNumber).ToList();
                        break;
                }
                
            }
            else if (string.IsNullOrEmpty(searchMessage) && searchMail != null)
            {
                List<Log> GetLogswithUser;

                switch (sort_order)
                {
                    case "UserMailUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.User.UserMail).ToList();
                        break;
                    case "UserMailDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.User.UserMail).ToList();
                        break;
                    case "LogMessage":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.Type).ToList();
                        break;
                    case "LogTimeUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    case "LogTimeDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    default:
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.LogNumber).ToList();
                        break;
                }

                List = GetLogswithUser.Where(x => x.User.UserMail.ToLower().Contains(searchMail.ToLower())).ToList();
            }
            else if (searchMessage != null && string.IsNullOrEmpty(searchMail))
            {
                List<Log> GetLogswithUser;

                switch (sort_order)
                {
                    case "UserMailUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.User.UserMail).ToList();
                        break;
                    case "UserMailDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.User.UserMail).ToList();
                        break;
                    case "LogMessage":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.Type).ToList();
                        break;
                    case "LogTimeUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    case "LogTimeDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    default:
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.LogNumber).ToList();
                        break;
                }

                List = GetLogswithUser.Where(x => x.Type.GetDisplayName().ToLower().Contains(searchMessage.ToLower())).ToList();
            }
            else
            {
                List<Log> GetLogswithUser;

                switch (sort_order)
                {
                    case "UserMailUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.User.UserMail).ToList();
                        break;
                    case "UserMailDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.User.UserMail).ToList();
                        break;
                    case "LogMessage":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.Type).ToList();
                        break;
                    case "LogMessageDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.Type).ToList();
                        break;
                    case "LogTimeUp":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    case "LogTimeDown":
                        GetLogswithUser = GetDbContext().Include("User").OrderByDescending(x => x.LogTime).ToList();
                        break;
                    default:
                        GetLogswithUser = GetDbContext().Include("User").OrderBy(x => x.LogNumber).ToList();
                        break;
                }

                List = GetLogswithUser.Where(x => x.User.UserMail.ToLower().Contains(searchMail.ToLower()) && x.Type.GetDisplayName().ToLower().Contains(searchMessage.ToLower())).ToList();

            }
            if (startDate !=null && endDate!=null)
            {
                result = List.Where(x => x.LogTime > startDate && x.LogTime < endDate).ToPagedList(page,10);
            }
            else if (startDate == null && endDate != null)
            {
                result = List.Where(x => x.LogTime < endDate).ToPagedList(page, 10);
            }
            else if (startDate != null && endDate == null)
            {
                result = List.Where(x => x.LogTime > startDate).ToPagedList(page, 10);
            }
            else
            {
                result = List.ToPagedList(page, 10);
            }
            return result;
        }
    }
}