using System.Data.Entity;

namespace wpf_game_dev_cycle.Model
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("Data Source=DESKTOP-PAG57TP;Initial Catalog=company;Integrated Security=True")
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}