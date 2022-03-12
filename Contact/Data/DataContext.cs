using Microsoft.EntityFrameworkCore;
using Contact.Data.Models;

namespace Contact.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Contacts> Contacts {get; set;}

}