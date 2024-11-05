using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShare.Services {
    
    //UTILITY CLASS FOR HANDLING RECIPE CACHING
    public class RecipeCacheService {

        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RecipeCacheService> _logger;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private const string recipesCacheKey = "recipes_cache_key";

        public RecipeCacheService(ApplicationDbContext context, IMemoryCache cache, ILogger<RecipeCacheService> logger) {
            
            _context = context;
            _cache = cache;
            _logger = logger;

            _cacheOptions = new MemoryCacheEntryOptions();
            _cacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(60));
            _cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
        }

        //SETS CACHE IF THERE ARE NO RECIPES IN CACHE ALREADY
        public void SetCache () {

            if (_cache.TryGetValue(recipesCacheKey, out IEnumerable<Recipe> recipes)) {

                //Recipes in cache
            }
            else { 

                recipes = _context.Recipe.ToList();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();

                cacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(60));
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));

                _cache.Set(recipesCacheKey, recipes, cacheOptions);
            }
        }

        //UPDATES THE CACHE AFTER THE RECIPE HAS BEEN EDITED
        public void UpdateRecipeInCache(int recipeId) {

            Recipe recipe = _context.Recipe.Find(recipeId);
            
            if (recipe != null) {
            
                _cache.Remove(recipesCacheKey);
                var updatedRecipes = _context.Recipe.ToList();
                _cache.Set(recipesCacheKey, updatedRecipes, _cacheOptions);
            }
        }
    }
}
