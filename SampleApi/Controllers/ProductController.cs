using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApi.Application.Data;
namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ShopDbContext _shopDbContext;
        public ProductController(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        [HttpGet()]
        public List<Product> Get([FromQuery] int? categoryId)
        {
            return _shopDbContext
                .Products
                .Where(x => x.CategoryId == categoryId || categoryId == null)
                .ToList();
        }

        //Adaya sorulacak soru
        //Get methodunu performanslý hale getirmek için neler yapýlmalý?
    }
}