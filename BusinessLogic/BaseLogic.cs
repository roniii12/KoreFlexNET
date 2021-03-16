using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BaseLogic
    {
        protected MoodyDbContext moodyContext;

        public BaseLogic(MoodyDbContext moodyContext)
        {
            this.moodyContext = moodyContext;
        }

        public BaseLogic(MoodyDbContext moodyContext, MoodyDbContext moodyContext1)
        {
            this.moodyContext = moodyContext;
        }
    }
}
