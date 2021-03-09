using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class DatabaseContextFactory : IDesignTimeDbContextFactory<MoodyDbContext>
    {
        public MoodyDbContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfig = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<MoodyDbContext>();
            opsBuilder.UseSqlServer(appConfig.sqlConnectionString);
            return new MoodyDbContext(opsBuilder.Options);
        }
    }
}
