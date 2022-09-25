using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DesignTime
{
    internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<PostContext>
    {
        public PostContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostContext>();

            optionsBuilder.UseSqlServer(@"Server=.\sqlnegin;Database=NzPortfolio;User Id=sa;Password=123456;MultipleActiveResultSets=True;");

            return new PostContext(optionsBuilder.Options, null,new AuditableEntitySaveChangesInterceptor());


        }
    }
}
