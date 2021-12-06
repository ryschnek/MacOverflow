using MacOverflow.Logic.StoredDataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MacOverflow.Data
{
    public class AppDbContext : IdentityDbContext
    { 

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
    }
}