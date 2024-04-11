using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DrinkAndGo.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(IDrinkRepository drinkRepository, ShoppingCart shoppingCart)
        {
            _drinkRepository = drinkRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;

            var vm = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(vm);
        }

        public RedirectToActionResult AddToShoppingCart(int drinkId)
        {
            var seletedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
            if (seletedDrink != null)
            {
                _shoppingCart.AddToCart(seletedDrink, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
        {
            var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
            if (selectedDrink != null)
            {
                _shoppingCart.RemoveFromCart(selectedDrink);
            }
            return RedirectToAction("Index");
        }
    }
}
