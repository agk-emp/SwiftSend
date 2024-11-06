using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwiftSend.data.Entities;
using SwiftSend.data.Entities.Identity;

namespace SwiftSend.infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser,
        IdentityRole<int>,
        int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        public DbSet<Customer> Custoemrs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
