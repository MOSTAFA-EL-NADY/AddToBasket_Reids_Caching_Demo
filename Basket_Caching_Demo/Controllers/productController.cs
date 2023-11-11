using Basket_Caching_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basket_Caching_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public productController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var allProducts = await _storeContext.Products.ToListAsync();
            return Ok(allProducts);
        }
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _storeContext.Products.Add(product);
                _storeContext.SaveChanges();



                return Ok(product);
            }
            return BadRequest("Failed To Save");
        }
    }
}
