using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace wpf_game_dev_cycle.Model
{
    public class CompanyContext : DbContext
    {
        public DbSet<AdministratorAccount> AdministratorAccounts { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PublisherAccount> PublisherAccounts { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<DeveloperAccount> DeveloperAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<DevelopmentTeam> DevelopmentTeams { get; set; }

        public CompanyContext() : base("SQLServer connections")
        {

        }
    }

    public class Table
    {
        public string Name { get; set; }
    }

    public abstract class Entity {}

    [Table("admin_accounts", Schema = "dev_cycle_company")]
    public class AdministratorAccount : Entity
    {
        [Key] public Guid Account_id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    [Table("publishers", Schema = "dev_cycle_company")]
    public class Publisher : Entity
    {
        [Key] public string Publisher_code { get; set; }
        public string Full_name { get; set; }
        public string Position { get; set; }
    }
    
    [Table("publisher_accounts", Schema = "dev_cycle_company")]
    public class PublisherAccount : Entity
    {
        [Key] public Guid Account_id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public string Publisher_code { get; set; }
    }

    [Table("verifications", Schema = "dev_cycle_company")]
    public class Verification : Entity
    {
        [Key, Column(Order = 0)] public string Verification_code { get; set; }
        [Key, Column(Order = 1)] public string Employee_position { get; set; }
        [Key, Column(Order = 2)] public string Employee_full_name { get; set; }
        [Key, Column(Order = 3)] public string Publisher_code { get; set; }
    }
    
    [Table("developers", Schema = "dev_cycle_company")]
    public class Developer : Entity
    {
        [Key] public string Employee_code { get; set; }
        public string Full_name { get; set; }
        public string Position { get; set; }
        public DateTime Birth_date { get; set; }
        public DateTime Registration_date { get; set; }
        public string Education { get; set; }
        public DateTime Employment_date { get; set; }
        public string Status_of_dismissal { get; set; }
        public string Status_of_work_on_the_project { get; set; }
        public string Technology_stack { get; set; }
    }
    
    [Table("developer_accounts", Schema = "dev_cycle_company")]
    public class DeveloperAccount : Entity
    {
        [Key, Column(Order = 0)] public Guid Account_id { get; set; }
        [Key, Column(Order = 1)] public string Login { get; set; }
        [Key, Column(Order = 2)] public string Password { get; set; }
        [Key, Column(Order = 3)] public string Email { get; set; }
        [Key, Column(Order = 4)] public string? Phone_number = "Null";
        [Key, Column(Order = 5)] public string Verification_code { get; set; }
        [Key, Column(Order = 6)] public string Employee_code { get; set; }
    }
    
    [Table("customers", Schema = "dev_cycle_company")]
    public class Customer : Entity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Customer_id { get; set; }
        public string Order_name { get; set; }
    }
    
    [Table("contracts", Schema = "dev_cycle_company")]
    public class Contract : Entity
    {
        [Key, Column(Order = 0)] public int Contract_id { get; set; }
        [Key, Column(Order = 1)] public int Customer_id { get; set; }
        [Key, Column(Order = 2)] public int Price { get; set; }
        [Key, Column(Order = 3)] public DateTime Date { get; set; }
    }
    
    [Table("projects", Schema = "dev_cycle_company")]
    public class Project : Entity
    {
        [Key] public int Project_id { get; set; }
        public int Customer_id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Deadline_date { get; set; }
        public int Contract_id { get; set; }
    }
    
    [Table("repositories", Schema = "dev_cycle_company")]
    public class Repository : Entity
    {
        [Key] public int Repository_id { get; set; }
        public string Repository_name { get; set; }
        public DateTime Creation_date { get; set; }
        public int Project_id { get; set; }
    }
    
    [Table("development_teams", Schema = "dev_cycle_company")]
    public class DevelopmentTeam : Entity
    {
        [Key, Column(Order = 0)] public int Team_member_id { get; set; }
        [Key, Column(Order = 1)] public string Employee_code { get; set; }
        [Key, Column(Order = 2)] public int Repository_id { get; set; }
        [Key, Column(Order = 3)] public string? Position = "Null";
        [Key, Column(Order = 4)] public string? Work_status = "Null";
    }
}