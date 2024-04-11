using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using System.Collections.Generic;

namespace DrinkAndGo.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Category> Categories => _appDbContext.Categories;
    }
}
