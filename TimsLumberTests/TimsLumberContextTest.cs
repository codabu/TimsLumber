using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumberTests
{
    public class TimsLumberContextTest
    {
        protected TimsLumberContextTest(DbContextOptions<TimsLumberContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<TimsLumberContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new TimsLumberContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
        }
    }
}
