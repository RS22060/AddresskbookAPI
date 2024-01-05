using AddresskbookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AddresskbookAPI.Infrastructure
{
    public class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Address> Addresses { get; set; }
    }
}
