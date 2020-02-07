using Microsoft.EntityFrameworkCore;

namespace api_server.Models
{
    public class DealsContext : DbContext
    {
        public DealsContext(DbContextOptions<DealsContext> options)
            : base(options)
        {
        }

        public DbSet<Deal> Deals { get; set; }
    }
}
