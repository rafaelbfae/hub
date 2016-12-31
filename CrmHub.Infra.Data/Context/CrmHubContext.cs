using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CrmHub.Domain.Models.Identity;
using CrmHub.Infra.Data.Interface;

namespace CrmHub.Infra.Data.Context
{
    public partial class CrmHubContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CrmHubContext" /> class using the default connection string
        ///     <code>SpinnakerSqlServer</code>.
        /// </summary>
        public CrmHubContext(DbContextOptions<CrmHubContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        
    }
}
