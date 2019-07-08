using System.Linq;
using Domains.Abstract;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Web.Infrastructure;
using Web.Models;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;

        private readonly Cart cart;

        public CartController(IProductRepository repository, Cart cart)
        {
            _repository = repository;
            this.cart = cart;
        }

        public RedirectToActionResult AddToCart(int id)
        {
            var product = _repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int id)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index");
        }
        
        public ViewResult Index()
        {
            var res = new CartIndexViewModel
            {
                Cart = cart
            };
            return View(res);
        }
    }
}