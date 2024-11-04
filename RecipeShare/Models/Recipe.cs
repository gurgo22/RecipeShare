namespace RecipeShare.Models {
    public class Recipe {

        public int Id { get; set; }

        public string AuthorId { get; set; } = "";

        public string Name { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Instructions { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Recipe() {

            Ingredients = new List<Ingredient>();
            Ratings = new List<Rating>();
            Comments = new List<Comment>();

        }

        public Recipe(int id, string authorId, string name, string countryOfOrigin, string instructions, List<Ingredient> ingredients, List<Rating> ratings, List<Comment> comments) {

            Id = id;
            AuthorId = authorId;
            Name = name;
            CountryOfOrigin = countryOfOrigin;
            Instructions = instructions;    
            Ingredients = ingredients;
            Ratings = ratings;
            Comments = comments;

        }

        public double TotalProtein(int servings) {
            return servings * Ingredients.Sum(ingredient => ingredient.Protein * (ingredient.Quantity / 100));
        }

        public double CalculateRating () {

            if (Ratings.Count != 0) {

                return (Ratings.Sum(rating => rating.Value) / Ratings.Count);

            } else {
                return 0;
            }

        }

    }
}
