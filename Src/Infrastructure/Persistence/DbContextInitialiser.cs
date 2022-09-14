using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DbContextInitialiser
    {
        private readonly PostContext _dbContext;

        public DbContextInitialiser
            (
                PostContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task InitialiseAsync()
        {
          
            if (_dbContext.Database.IsSqlServer())
            {
                await _dbContext.Database.MigrateAsync();
            }
             
        }

        public async Task SeedAsync()
        {
            // Insert Some Data
        }


    }
}
