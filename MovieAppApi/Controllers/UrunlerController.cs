using AngularExampleAPI.Data;
using AngularExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularExampleAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UrunlerController : Controller
    {
        private readonly AngularExampleDbContext _angularExampleDbContext;
        public UrunlerController(AngularExampleDbContext angularExampleDbContext)
        {
            _angularExampleDbContext = angularExampleDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUrunler()
        {
            var urunler = await _angularExampleDbContext.Urunler.ToListAsync();

            return Ok(urunler);
        }

        [HttpPost]
        public async Task<IActionResult> AddUrun([FromBody] Urun urun)
        {
            urun.Id = Guid.NewGuid();

            await _angularExampleDbContext.Urunler.AddAsync(urun);
            await _angularExampleDbContext.SaveChangesAsync();

            return Ok(urun);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUrun(Guid id)
        {
            var product = await _angularExampleDbContext.Urunler.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, Urun updateProduct)
        {
            var product = await _angularExampleDbContext.Urunler.FindAsync(id);

            if (product == null)
                return NotFound();

            product.Name = updateProduct.Name;
            product.UrunId = updateProduct.UrunId;
            product.Category = updateProduct.Category;
            product.Price = updateProduct.Price;

            await _angularExampleDbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _angularExampleDbContext.Urunler.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _angularExampleDbContext.Urunler.Remove(product);
            await _angularExampleDbContext.SaveChangesAsync();

            return Ok(product);
        }

    }
}
