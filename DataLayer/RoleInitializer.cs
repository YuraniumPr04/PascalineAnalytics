using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DataLayer // Або DataLayer.Data, залежно від структури
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. Створюємо ролі, якщо їх немає
            // Ти можеш додати сюди будь-які ролі: "Admin", "Manager", "User"
            string[] roleNames = { "Admin", "User", "Employee" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Створюємо Адміністратора
            string adminEmail = "admin_super@gmail.com";
            string password = "132435465768"; // Пароль має бути складним (Identity вимагає це за замовчуванням)

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    UserName = adminEmail, // Для Identity UserName часто дорівнює Email
                    Email = adminEmail,
                    EmailConfirmed = true, // Важливо: дозволяє вхід без підтвердження пошти
                    PhoneNumber = "+380999999999",

                    // Ініціалізація твоїх кастомних полів (важливо!)
                    UsedStorageBytes = 0,
                    QueriesUsed = 0,
                    LastQueriesResetDate = DateTime.UtcNow // Важливо задати дату, щоб не було помилки SQL (DateTime2)
                };

                IdentityResult result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    
                }
            }
        }
    }
}