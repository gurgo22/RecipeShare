using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using RecipeShare.Data;
using RecipeShare.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecipeShare.Services {

    //UTILITY CLASS FOR RECIPECONTROLLER
    public class RecipeService {

        //DETERMINES IF THE USER HAS ALREADY RATED THE RECIPE
        public static bool CanAddRating(ApplicationDbContext _context, IdentityUser user, Rating currentRating, Recipe recipe) {
            
            bool rate = false;

            if (currentRating.Value > 0) {

                rate = !(_context.Rating.Where(r => r.UserId == user.Id && r.RecipeId == recipe.Id).ToList().IsNullOrEmpty());

                if (!rate && currentRating.Value > 0) {

                    return true;
                }
            }

            return false;
        }

        //ADDS A USER RATING IF CRITERIAS ARE MET
        public static void AddRating (Rating currentRating, IdentityUser user, Recipe recipe, ApplicationDbContext _context) {

            bool hasRated = false;

            if (CanAddRating(_context, user, currentRating, recipe)) {

                currentRating.UserId = user.Id;

                recipe.Ratings.Add(currentRating);

                _context.Add(currentRating);
                //return RedirectToAction("Details", new { id = recipe.Id });
            }
        }

        //ADDS A USER COMMENT IF CRITERIAS ARE MET
        public static void AddComment (ApplicationDbContext _context, IdentityUser user, Recipe recipe, Comment currentComment) {

            if (!string.IsNullOrEmpty(currentComment.Content)) {

                if (user != null) {
                    currentComment.Author = user.UserName;
                    currentComment.RecipeId = recipe.Id;
                    currentComment.CreatedAt = DateTime.Now;
                    currentComment.Recipe = recipe;
                }

                recipe.Comments.Add(currentComment);

                _context.Add(currentComment);
            }

        }

        //SETS THE IMAGE OF THE RECIPE
        public static async Task SetFoodImage(IFormFile foodImage, string recipeName) {

            if (foodImage != null && foodImage.Length > 0) {

                var filePath = Path.Combine("wwwroot/images/recipes", recipeName + ".jpg");

                using (var stream = new FileStream(filePath, FileMode.Create)) {

                    await foodImage.CopyToAsync(stream);
                }
            }
        }

        //SETS ALL INGREDIENTS RECIPE ID
        public static async Task AssignRecipeIdsToIngredients(Recipe recipe) {

            if (recipe.Ingredients.Count > 0) {

                foreach (var ingredient in recipe.Ingredients) {

                    ingredient.Recipe = recipe;
                }
            }
        }

        /*
        public static void UpdateIngredients(ApplicationDbContext _context, Recipe recipe) {

            var existingIngredients = _context.Ingredient.Where(i => i.RecipeId == recipe.Id).ToList();
            _context.Ingredient.RemoveRange(existingIngredients);
            _context.Ingredient.AddRange(recipe.Ingredients);
        }
        */

        //CHECKS IF THE RECIPE ALREADY EXISTS
        public static bool RecipeExists(ApplicationDbContext _context, int id) {

            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
