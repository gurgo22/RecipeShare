using RecipeShare.Models;

namespace Test {
    public class ModelTests {
        public class CommentTests {

            [Test]
            public void ConstructorTest() {
                
                int id = 1;
                int recipeId = 10;
                string author = "Test Author";
                string content = "This is a test comment.";
                DateTime createdAt = DateTime.Now;
                Recipe recipe = new Recipe { Id = recipeId, Name = "Test Recipe" };

                Comment comment = new Comment(id, recipeId, author, content, createdAt, recipe);

                Assert.AreEqual(id, comment.Id);
                Assert.AreEqual(recipeId, comment.RecipeId);
                Assert.AreEqual(author, comment.Author);
                Assert.AreEqual(content, comment.Content);
                Assert.AreEqual(createdAt, comment.CreatedAt);
                Assert.AreEqual(recipe, comment.Recipe);
            }

            [Test]
            public void DefaultConstructortTest() {

                Comment comment = new Comment();

                Assert.AreEqual(0, comment.Id);
                Assert.AreEqual(0, comment.RecipeId);
                Assert.IsNull(comment.Author);
                Assert.IsNull(comment.Content);
                Assert.AreEqual(default(DateTime), comment.CreatedAt);
                Assert.IsNull(comment.Recipe);
            }

            [Test]
            public void SetPropertyTest() {

                Comment comment = new Comment();
                int id = 1;
                int recipeId = 10;
                string author = "Updated Author";
                string content = "Updated content.";
                DateTime createdAt = DateTime.Now;
                Recipe recipe = new Recipe { Id = recipeId, Name = "Updated Recipe" };

                comment.Id = id;
                comment.RecipeId = recipeId;
                comment.Author = author;
                comment.Content = content;
                comment.CreatedAt = createdAt;
                comment.Recipe = recipe;
                
                Assert.AreEqual(id, comment.Id);
                Assert.AreEqual(recipeId, comment.RecipeId);
                Assert.AreEqual(author, comment.Author);
                Assert.AreEqual(content, comment.Content);
                Assert.AreEqual(createdAt, comment.CreatedAt);
                Assert.AreEqual(recipe, comment.Recipe);
            }
        }
    }
}