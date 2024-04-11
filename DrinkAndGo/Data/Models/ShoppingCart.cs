using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkAndGo.Data.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public ShoppingCart(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;

        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", CartId);

            return new ShoppingCart(context) { ShoppingCartId = CartId };
        }

        public void AddToCart(Drink drink, int amount)
        {
            var shoppingCartItem =
                _appDbContext.shoppingCartItems.SingleOrDefault(
                    s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId
                    );

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Drink = drink,
                    Amount = 1
                };

                _appDbContext.shoppingCartItems.Add(shoppingCartItem);

            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Drink drink)
        {
            var shoppingCartItem = _appDbContext.shoppingCartItems.SingleOrDefault(
                s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId
                );

            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.shoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _appDbContext.shoppingCartItems
                                        .Where(c => c.ShoppingCartId == ShoppingCartId)
                                        .Include(s => s.Drink)
                                        .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext.shoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.shoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext
                            .shoppingCartItems
                            .Where(c => c.ShoppingCartId == c.ShoppingCartId)
                            .Select(c => c.Drink.Price * c.Amount).Sum();
            return total;

        }
    }
}
