using AutoMapper.Features;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class InitialSeeding
    {
        private static readonly List<Role> Roles = new()
    {
        new() { Name = Domain.Enums.Role.Admin.ToString() },
        new() { Name = Domain.Enums.Role.User.ToString() },
        new() { Name = Domain.Enums.Role.Shop.ToString() }
    };

 

        /// <summary>
        /// Initialize and seed the database with roles, categories, and subcategories.
        /// </summary>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EcommerceFashionDbContext>();

            // Seed Roles
            foreach (var role in Roles)
            {
                if (!context.Roles.Any(r => r.Name == role.Name))
                {
                    role.CreationDate = DateTime.UtcNow;
                    context.Roles.Add(role);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
