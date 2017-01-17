using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Identity.Context
{
    public class TemporaryDbContextFactory : IDbContextFactory<CrmIdentityDbContext>
    {
        public CrmIdentityDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CrmIdentityDbContext>();
            builder.UseSqlServer("Data Source=SQL5033.myWindowsHosting.com;Initial Catalog=DB_A059B6_CrmHubIdentity;User Id=DB_A059B6_CrmHubIdentity_admin;Password=CrmHub1234");
            var context = new CrmIdentityDbContext(builder.Options);
            return context;
        }
    }
}