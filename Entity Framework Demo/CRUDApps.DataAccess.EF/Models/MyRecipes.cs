using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Models
{
    public partial class MyRecipes
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string MealType { get; set; }
        public string MainIngredient { get; set; }
        public byte PrepTime { get; set; }
    }
}
