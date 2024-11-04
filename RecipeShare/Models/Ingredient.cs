namespace RecipeShare.Models {
    public class Ingredient {

        public int Id { get; set; }

        public int RecipeId { get; set; }
        
        public Recipe? Recipe { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Protein { get; set; }

        public Ingredient() { }

        public Ingredient(int id, int recipeId, Recipe recipe, string name, int quantity, int protein) {
            
            Id = id;
            RecipeId = recipeId;
            Recipe = recipe;
            Name = name;
            Quantity = quantity;
            Protein = protein;
        
        }

    }
}
