using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;

namespace RealEstate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<EstateModel> Estate { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<FavoriteModel> Favorites { get; set; }
        public DbSet<CommentModel> Comments { get; set; }


    }
}