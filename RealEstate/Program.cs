using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Implementation;
using RealEstate.Services.Interface;
using RealEstate.Utilities;

namespace RealEstate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region DataBase
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            #endregion

            #region Identity
            builder.Services.AddDefaultIdentity<UserModel>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion

            #region Authorization
            builder.Services.AddAuthorization(options=>
            {
                options.AddPolicy(AuthorizationPolicies.AdminPolicy, p => p.RequireRole(Roles.Admin));
            });
            #endregion

            #region RazorPages
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin",AuthorizationPolicies.AdminPolicy);
            });
            #endregion

            #region Scope
            builder.Services.AddScoped<IManagementService, ManagementService>();
            #endregion

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}