using DrinkAndGo.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrinkAndGo.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;
        public DrinkController(IDrinkRepository drinkRepository, ICategoryRepository categoryRepository)
        {
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
        }
        public ViewResult List()
        {
            var drinks = _drinkRepository.Drinks;
            return View(drinks);
        }
    }
}
