using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using RecipeShare.Data;
using RecipeShare.Models;
using RecipeShare.Services;

namespace RecipeShare.Controllers {
    public class RecipesController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RecipesController> _logger;
        private readonly RecipeCacheService _recipeCacheService;

        //CONSTRUCTOR THAT INITIALIZES DEPENDENCIES
        public RecipesController(ApplicationDbContext context, UserManager<IdentityUser > userManager, ILogger<RecipesController> logger, RecipeCacheService recipeCacheService) {
            
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _recipeCacheService = recipeCacheService;
        }

        //RETURNS RECIPES FROM DATABASE FOR THE PAGE
        //FILTERS ON SEARCH TERM AND COUNTRY
        public async Task<IActionResult> Index(string searchString, string countryFilter) {

            _recipeCacheService.SetCache();

            IQueryable<Recipe> searchQuery = _context.Recipe.Include(r => r.Ingredients).AsQueryable();

            if (!string.IsNullOrEmpty(countryFilter)) {

                searchQuery = searchQuery.Where(r => r.CountryOfOrigin.Contains(countryFilter));
            }

            if (!string.IsNullOrEmpty(searchString)) {
                
                searchQuery = searchQuery.Where(r => r.Name.Contains(searchString));
            }
            
            List<Recipe> foundRecipes = await searchQuery.ToListAsync();
            
            return View(foundRecipes);
        }

        //RETURN THE DETAILED DATA ABOUT RECIPE
        public async Task<IActionResult> Details(int? id, [Bind("Value")] Rating currentRating, [Bind("Content")] Comment currentComment) {

            if (id == null) {

                return NotFound();
            }

            Recipe recipe = await _context.Recipe
                .Include(r => r.Ingredients) 
                .Include(r => r.Ratings) 
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null) {
             
                return NotFound();
            }

            IdentityUser user = await _userManager.GetUserAsync(User);

            RecipeService.AddRating(currentRating, user, recipe, _context);

            ViewData["HasRated"] = RecipeService.CanAddRating(_context, user, currentRating, recipe);
            ViewData["IsLoggedIn"] = user != null;

            RecipeService.AddComment(_context, user, recipe, currentComment);

            await _context.SaveChangesAsync();

            return View(recipe);
        }

        //RECIPE CREATING PAGE
        // GET: Recipes/Create
        [Authorize]
        public IActionResult Create() {
            return View();
        }

        //USER CREATING RECIPE
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CountryOfOrigin,Instructions,Ingredients")] Recipe recipe, IFormFile foodImage) {

            if (ModelState.IsValid) {
                Console.WriteLine("ModelState valid");

                await RecipeService.AssignRecipeIdsToIngredients(recipe);

                RecipeService.SetFoodImage(foodImage, recipe.Name);

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid) {
                LogModelStateErrors();
            }

            return View(recipe);
        }

        //LOGS ERRORS REGARDING MODEL STATE
        public void LogModelStateErrors() {
            Console.WriteLine("ModelState invalid");
            foreach (var state in ModelState) {

                foreach (var error in state.Value.Errors) {

                    Console.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
        }

        //RETURNS A RECIPE FOR EDITING
        [Authorize (Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                return NotFound();  
            }

            var recipe = await _context.Recipe
                .Include(r => r.Ingredients) 
                .FirstOrDefaultAsync(r => r.Id == id);
            
            if (recipe == null) {
                return NotFound();
            }

            return View(recipe);
        }

        //UPDATES A RECIPE BASED ON THE PROVIDED DATA
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CountryOfOrigin,Instructions,Ingredients")] Recipe recipe) {
            
            if (id != recipe.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {

                    _recipeCacheService.UpdateRecipeInCache(recipe.Id);

                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException exc) {

                    Console.WriteLine(exc + " \r\n An error occurred while updating the recipe");

                    if (!RecipeService.RecipeExists(_context, recipe.Id)) {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        //RETURNS A RECIPE FOR DELETION
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Ingredients) // Include ingredients for deletion
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (recipe == null) {
                return NotFound();
            }

            return View(recipe);
        }

        //DELETES A RECIPE BASED ON THE PROVIDED DATA
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            Recipe recipe = await _context.Recipe.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.Id == id);
            
            if (recipe != null) {

                _context.Recipe.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}