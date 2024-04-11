using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using System.Collections.Generic;

namespace DrinkAndGo.Data.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category{CategoryName = "Alchoholic", Description = "All alchoholic drinks"},
                    new Category{CategoryName = "Non-alchoholic", Description = "All Non-alchoholic drinks"}
                };
            }
        }
    }
} 
