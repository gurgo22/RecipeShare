namespace RecipeShare.Models {
    public class Rating {

        public int Id { get; set; }

        public string UserId { get; set; }

        public int RecipeId { get; set; }

        public int Value { get; set; }

        public Rating() { }

        public Rating(int id, string userId, int recipeId, int value) { 
        
            Id = id;
            UserId = userId;
            RecipeId = recipeId;
            Value = value;      
        
        }

    }
}
