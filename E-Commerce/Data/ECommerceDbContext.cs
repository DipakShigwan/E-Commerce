using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserRegistration> userRegistrations { get; set; }
        public DbSet<UserApproval> userApprovals { get; set; }
    }
}
