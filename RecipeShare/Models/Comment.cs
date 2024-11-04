namespace RecipeShare.Models {
    public class Comment {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Recipe Recipe { get; set; }

        public Comment () { }

        public Comment(int id, int recipeId, string author, string content, DateTime createdAt, Recipe recipe) {
        
            Id = id;
            RecipeId = recipeId;
            Author = author;
            Content = content;
            CreatedAt = createdAt;
            Recipe = recipe;
        
        }
    }
}
