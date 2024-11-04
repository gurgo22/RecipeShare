namespace RecipeShare.Models {
    public class Recipe {

        public int Id { get; private set; }

        public int AuthorId { get; private set; }

        public string Name { get; private set; }

        public string CountryOfOrigin { get; private set; }

        public string Instructions { get; private set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public Recipe() {

            
        
        }

        public Recipe(int id, int authorID, string name, string countryOfOrigin, List<Ingredient> ingredients, string instructions) {
        
            Id = id;
            AuthorId = authorID;
            Name = name;
            CountryOfOrigin = countryOfOrigin;
            Ingredients = ingredients ?? new List<Ingredient>();
            Instructions = instructions;
        
        }

    }

}
