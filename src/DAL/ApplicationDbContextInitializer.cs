using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContextInitializer(AppDbContext context)
{
    public async Task InitializeAsync()
    {
        await context.Database.MigrateAsync();
    }
}