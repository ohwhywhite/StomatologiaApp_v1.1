using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stomatologia.Models;

namespace Stomatologia.Data;

public static class ApplicationDbInitializer
{
    public static async Task InitializeDb(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var db = services.GetRequiredService<ApplicationDbContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        db.Database.Migrate();

        await InitializeRoles(roleManager);
        await InitializeUsers(userManager);
        await InitializeVisits(db);
    }

    private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in InitialData.Roles)
        {
            if (await roleManager.RoleExistsAsync(role))
            {
                continue;
            }
            
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static async Task InitializeUsers(UserManager<ApplicationUser> userManager)
    {
        foreach (var user in InitialData.Users)
        {
            var userInDb = await userManager.FindByEmailAsync(user.Email);

            if (userInDb is not null)
            {
                continue;
            }

            await userManager.CreateAsync(user, InitialData.InitialPassword);

            await userManager.AddToRoleAsync(user, ApplicationRoles.User);

            if (user is Dentist)
            {
                await userManager.AddToRoleAsync(user, ApplicationRoles.Dentist);
            }

            if (user == InitialData.Users.Last())
            {
                await userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
            }
        }
    }

    private static async Task InitializeVisits(ApplicationDbContext db)
    {
        if (db.Visits.Any())
        {
            return;
        }

        await db.Visits.AddRangeAsync(InitialData.Visits);

        await db.SaveChangesAsync();
    }
}
