using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic : BaseLogic
    {
        public UserLogic(MoodyDbContext moodyContext) : base(moodyContext)
        {
            this.moodyContext = moodyContext;
        }
        public string getIpOfUser(string name)
        {
            return moodyContext.Users.FirstOrDefault(usr=>usr.UserName==name).Ip;
        }
        public void UpdateIpOfUser(string name, string ip)
        {
            User user = moodyContext.Users.First(usr => usr.UserName == name);
            user.Ip = ip;
            moodyContext.SaveChanges();
        }
        public bool IsIpUserOrInsert(string name, string ip)
        {
            User user = moodyContext.Users.First(usr => usr.UserName == name);
            if(user.Ip == null || user.Ip == "")
            {
                user.Ip = ip;
                moodyContext.SaveChanges();
                return true;
            }
            return user.Ip == ip;
        }
    }
}
