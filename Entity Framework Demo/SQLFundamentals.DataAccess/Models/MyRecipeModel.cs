namespace SQLFundamentals.DataAccess.Models
{
    public class MyRecipeModel
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; } = "";
        public string MealType { get; set; } = "";
        public string MainIngredient { get; set; } = "";
        public int PrepTime { get; set; }
    }
}