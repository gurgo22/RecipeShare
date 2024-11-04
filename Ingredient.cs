namespace RecipeShare.Models {
    public class Ingredient {

        public int Id { get; private set; }

        public int RecipeId { get; set; }
        
        public Recipe Recipe { get; private set; }

        public string Name { get; private set; }

        public int Protein { get; private set; }

        public Ingredient() { }

        public Ingredient(int id, int recipeId, Recipe recipe, string name, int protein) {
            
            Id = id;
            RecipeId = recipeId;
            Recipe = recipe;
            Name = name;
            Protein = protein;
        
        }

    }
}
