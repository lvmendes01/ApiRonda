using Microsoft.AspNetCore.Identity;
using RondaSegurancaBack.Models;

namespace RondaSegurancaBack.Data
{
    public static class SeedAdmin
    {
        public static async Task CreateAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminRole = "Admin";
            string adminEmail = "admin@ronda.com";
            string adminPassword = "SenhaForte123!";

            // Cria role Admin se não existir
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Cria usuário Admin se não existir
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new Usuario
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NomeCompleto = "Administrador"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                    Console.WriteLine("✅ Usuário Admin criado com sucesso!");
                }
                else
                {
                    Console.WriteLine("❌ Erro ao criar Admin: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                Console.WriteLine("ℹ️ Usuário Admin já existe.");
            }
        }
    }
}
