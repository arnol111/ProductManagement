using Domain.Entities;
using Domain.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextInicializer
    {
        private readonly ILogger<ApplicationDbContextInicializer> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInicializer(ILogger<ApplicationDbContextInicializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            //Default roles
            var administratorRole = new IdentityRole("Administrator");

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            //Default users
            var administrator = new ApplicationUser { UserName = "admin123@localhost", Email = "admin123@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
            
            // Fake data
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Products
                {
                    Description = "Speed Cube 3x3x3 Jurnwey sin calcomanías con tutorial de cubo, girando rápidamente suavemente cubos mágicos de 3x3 rompecabezas de juguete para niños y adultos.",
                    ProductCode = "USJW0003SF",
                    ExpirationDate = new DateTimeService().Now,
                    ManufacturingDate = new DateTimeService().Now,
                    ProviderCode = "B0881PCPDZ",
                    ProviderDescription = "Jurnwey",
                    ProviderPhone = 312545214,
                    State = "Activo"
                });


                await _context.SaveChangesAsync();
            }
        }
    }
}
