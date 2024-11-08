using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Domain.Entities;
namespace Prova.MarQ.Infra;
public class ProvaMarqDbContext : DbContext
{
    public ProvaMarqDbContext(DbContextOptions<ProvaMarqDbContext> options)
        : base(options)
    {
    }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Clocking> Clocking { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>().HasIndex(c => c.CompanyDocument).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.EmployeeDocument).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.EmployeePin).IsUnique();
        modelBuilder.Entity<Employee>().HasKey(e => e.EmployeePin);
        modelBuilder.Entity<Company>().HasKey(e => e.CompanyId);

        modelBuilder.Entity<Company>().HasQueryFilter(c => !c.IsCompanyDeleted);
        modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.IsEmployeeDeleted);

        modelBuilder.Entity<Clocking>()
            .HasOne(c => c.Employee)
            .WithMany(e => e.Clocking)
            .HasForeignKey(c => c.EmployeePin);

        modelBuilder.Entity<Employee>()
            .HasOne(c => c.Company)
            .WithMany(e => e.Employee)
            .HasForeignKey(c => c.CompanyId);

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlite("Data Source = provaMarqDB.db");
    //    }
    //}
}

