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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DeveloperAccount> DeveloperAccounts { get; set; }
        public DbSet<Developer> Developers { get; set; }
        // public DbSet<Verification> Verifications { get; set; }
        // public DbSet<Customer> Customers { get; set; }
        // public DbSet<Contract> Contracts { get; set; }
        // public DbSet<Project> Projects { get; set; }
        // public DbSet<Repository> Repositories { get; set; }
        // public DbSet<DevelopmentTeam> DevelopmentTeams { get; set; }

        public CompanyContext() : base("SQLServer connections")
        {

        }
    }

    public class Table
    {
        public string Name { get; set; }
    }

    public abstract class Entity {}

    [Table("admin_account", Schema = "company")]
    public class AdministratorAccount : Entity
    {
        [Key] public int Account_id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    
    [Table("employee", Schema = "company")]
    public class Employee : Entity
    {
        [Key] public string Employee_code { get; set; }
        public string Full_name { get; set; }
        public string Position { get; set; }
        public DateTime Birth_date { get; set; }
        public DateTime Registration_date { get; set; }
        public string Education { get; set; }
    }
    
    [Table("publisher", Schema = "company")]
    public class Publisher : Entity
    {
        [Key] public string Employee_code { get; set; }
    }
    
    [Table("publisher_account", Schema = "company")]
    public class PublisherAccount : Entity
    {
        [Key] public int Account_id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public string Employee_code { get; set; }
    }

    [Table("developer", Schema = "company")]
    public class Developer
    {
        [Key] public string Employee_code { get; set; }
        
        public DateTime Employment_date { get; set; }
        public string Status_of_dismissal { get; set; }
        public string Status_of_work_on_the_project { get; set; }
        public string Technology_stack { get; set; }
    }
    
    
    [Table("developer_account", Schema = "company")]
    public class DeveloperAccount
    {
        [Key, Column(Order = 0)] public string Login { get; set; }
        [Key, Column(Order = 1)] public string Password { get; set; }
        [Key, Column(Order = 2)] public int Account_id { get; set; }
        [Key, Column(Order = 3)] public string Email { get; set; }
        [Key, Column(Order = 4)] public string Phone_number { get; set; }
        [Key, Column(Order = 5)] public string Verification_code { get; set; }
        [Key, Column(Order = 6)] public string Employee_code { get; set; }
    }
    
    // [Table("verification", Schema = "company")]
    // public class Verification
    // {
    //     [Key] public string Verification_code { get; set; }
    //     
    //     public string Full_name { get; set; }
    //     public string Position { get; set; }
    // }
    //
    // [Table("customer", Schema = "company")]
    // public class Customer
    // {
    //     [Key] public int Customer_id { get; set; }
    //     
    //     public string Order_name { get; set; }
    // }
    //
    // [Table("contract", Schema = "company")]
    // public class Contract
    // {
    //     [Key] public int Contract_id { get; set; }
    //     [Key] public int Customer_id { get; set; }
    //     
    //     public int Price { get; set; }
    //     public string Date { get; set; }
    // }
    //
    // [Table("project", Schema = "company")]
    // public class Project
    // {
    //     [Key] public int Project_id { get; set; }
    //
    //     public string Name { get; set; }
    //     public string Status { get; set; }
    //     public string Deadline_date { get; set; }
    //     public int Contract_id { get; set; }
    //     public int Customer_id { get; set; }
    // }
    //
    // [Table("repository", Schema = "company")]
    // public class Repository
    // {
    //     [Key] public int Repository_id { get; set; }
    //
    //     public string Repository_name { get; set; }
    //     public string Creation_date { get; set; }
    //     public int Project_id { get; set; }
    // }
    //
    // [Table("development_team", Schema = "company")]
    // public class DevelopmentTeam
    // {
    //     [Key] public int Team_member_id { get; set; }
    //     [Key] public string Employee_code { get; set; }
    //     
    //     public string Position { get; set; }
    //     public string Work_status { get; set; }
    //     public int Repository_id { get; set; }
    // }
}