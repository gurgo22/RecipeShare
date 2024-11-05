using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Models;

namespace RecipeShare.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<RecipeShare.Models.Recipe> Recipe { get; set; } = default!;
        public DbSet<RecipeShare.Models.Ingredient> Ingredient { get; set; } = default!;
        public DbSet<RecipeShare.Models.Rating> Rating { get; set; } = default!;
        public DbSet<RecipeShare.Models.Comment> Comments { get; set; } = default!;
        public DbSet<RecipeShare.Models.UserActivity> UserActivities { get; set; } = default!;
    }
}
