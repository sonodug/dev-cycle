using System.Data.Entity;

namespace wpf_game_dev_cycle.Model
{
    public class CompanyContext : DbContext
    {
        public DbSet<admin_account> admin_accounts { get; set; }

        public CompanyContext() : base("SQLServer connections")
        {

        }
    }
}