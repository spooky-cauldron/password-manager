using Microsoft.EntityFrameworkCore;

namespace PasswordManager;

public class PasswordDbContext : DbContext
{
    public PasswordDbContext(DbContextOptions<PasswordDbContext> options)
        : base(options)
    { }

    public DbSet<PasswordStore> Passwords { get; set; }
}