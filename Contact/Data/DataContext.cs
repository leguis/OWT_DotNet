using Microsoft.EntityFrameworkCore;
using Contact.Data.Models;

namespace Contact.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Contacts_Skills>()
            .HasOne(c => c.Contact)
            .WithMany(cs => cs.Contacts_Skills)
            .HasForeignKey(ci => ci.ContactId);
        modelBuilder.Entity<Contacts_Skills>()
            .HasOne(s => s.Skill)
            .WithMany(cs => cs.Contacts_Skills)
            .HasForeignKey(si => si.SkillsId);
    }

    public DbSet<Contacts> Contacts {get; set;}
    public DbSet<Skills> Skills {get; set;}
    public DbSet<Contacts_Skills> Contacts_SKills { get; set; }

}