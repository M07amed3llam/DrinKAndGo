using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DrinkAndGo.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly AppDbContext _appDbContext;
        public DrinkRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Drink> Drinks => _appDbContext.Drinks.Include(c => c.Category);

        public IEnumerable<Drink> PreferredDrinks => _appDbContext.Drinks
            .Where(p => p.IsPreferredDrink)
            .Include(c => c.Category);

        public Drink GetDrinkById(int drinkId) => _appDbContext.Drinks
            .FirstOrDefault(p => p.DrinkId == drinkId);
    }
}
