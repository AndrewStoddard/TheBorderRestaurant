using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheBorderRestaurant.Models.DomainModels;

namespace TheBorderRestaurant.Models.DataLayer
{
    public class BorderContext : IdentityDbContext<User>
    {
        #region Properties

        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }

        #endregion

        #region Constructors

        public BorderContext(DbContextOptions<BorderContext> options) : base(options)
        {
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FoodItem>().HasKey(f => f.Id);
            builder.Entity<FoodOrder>().HasKey(o => o.Id);
            builder.Entity<FoodOrder>().HasMany(fo => fo.FoodOrderItems).WithMany(fo => fo.FoodOrders);
            builder.Entity<FoodOrderItem>().HasKey(foi => foi.Id);
            builder.Entity<FoodOrderItem>().HasMany(foi => foi.FoodOrders).WithMany(foi => foi.FoodOrderItems);
            builder.Entity<FoodItem>().HasData(
                new FoodItem {
                    Id = 1,
                    Name = "Taco",
                    Description =
                        "Traditional Mexican dish consisting of a small hand-sized corn or wheat tortilla topped with a filling.",
                    Price = 3.00,
                    ImageName = "Taco.jpg"
                },
                new FoodItem {
                    Id = 2,
                    Name = "Quesadilla",
                    Description =
                        "Mexican cuisine dish, consisting of a tortilla that is filled primarily with cheese, and sometimes meats, spices, and other fillings, and then cooked on a griddle or stove.",
                    Price = 6.00,
                    ImageName = "Quesadilla.jpg"
                },
                new FoodItem {
                    Id = 3,
                    Name = "Queso and Chips",
                    Description =
                        "A blend of queso cheese, peppers, and spices that's a little smoky, a little spicy, and very cheesy.",
                    Price = 5.00,
                    ImageName = "Queso.jpg"
                },
                new FoodItem {
                    Id = 4,
                    Name = "Enchilada",
                    Description =
                        "A corn tortilla rolled around a filling and covered with a savory sauce.",
                    Price = 8.00,
                    ImageName = "Enchiladas.jpg"
                }
            );
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var username = "admin";
            var password = "password";

            var roleName = "Admin";

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new User {
                    UserName = username,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@border.com",
                    Street = "1210 Maple St",
                    City = "Carrollton",
                    State = "Georgia",
                    Zip = "30117"
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        #endregion
    }
}