using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Infra.Data.Context
{
    public class TemporaryDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Data Source=SQL5021.myWindowsHosting.com;Initial Catalog=DB_A059B6_CrmHub;User Id=DB_A059B6_CrmHub_admin;Password=CrmHub1234");
            var context = new ApplicationDbContext(builder.Options);
            return context;
        }
    }
}