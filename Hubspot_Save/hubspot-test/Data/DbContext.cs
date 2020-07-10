using hubspot_test.Model;
using Microsoft.EntityFrameworkCore;

namespace hubspot_test.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Deals> deals { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConection = System.Configuration.ConfigurationManager.AppSettings["dbConection"];
            string conectionString = dbConection;

            optionsBuilder.UseSqlServer(conectionString);
        }
    }
}
