using HomeBudgetTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetTracker.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
}