using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
        public DbSet<Lead> Lead { get; set; }

        public DbQuery<Campaign> Campaign { get; set; }
    }
}
