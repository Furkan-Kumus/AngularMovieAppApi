using AngularExampleAPI.Data;
using AngularExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularExampleAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UrunlerController : ControllerBase //controller or controllerbase?
    {
        private readonly AngularExampleDbContext _angularExampleDbContext;
        public UrunlerController(AngularExampleDbContext angularExampleDbContext)
        {
            _angularExampleDbContext = angularExampleDbContext;
        }

        #region videolar deneme
        //[HttpGet]
        //public IEnumerable<Urun> GetUrunler()
        //{
        //    return new List<Urun>(){
        //        new Urun{
        //            Id = Guid.NewGuid(),
        //            Name = "?",
        //            Category ="?",
        //            UrunId = "?",
        //            Price = 11,
        //        },
        //        new Urun{
        //            Id = Guid.NewGuid(),
        //            Name = "?",
        //            Category ="?",
        //            UrunId = "?",
        //            Price = 12,
        //        }
        //    };
        //}

        //[HttpGet("{price:int}")]
        //public Urun GetUrunById(int price)
        //{
        //    var uruns = new List<Urun>(){
        //        new Urun{
        //            Id = Guid.NewGuid(),
        //            Name = "?",
        //            Category ="?",
        //            UrunId = "?",
        //            Price = 11,
        //        },
        //        new Urun{
        //            Id = Guid.NewGuid(),
        //            Name = "?",
        //            Category = "?",
        //            UrunId = "?",
        //            Price = 12,
        //        }
        //    };

        //    return uruns.Where(x => x.Price == price).FirstOrDefault();
        //} 
        #endregion

        #region çalışan kısım get/add/update/delete


        [HttpGet]
        public async Task<IActionResult> GetAllUrunler()
        {
            var urunler = await _angularExampleDbContext.Urunler.ToListAsync();

            return Ok(urunler);
        }

        
        //[HttpGet("{name:alpha}")] // for alphabet
        //[HttpGet("{price:min(1):max(100)}")] // sınırlama
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUrun(Guid id)
        {
            var product = await _angularExampleDbContext.Urunler.FirstOrDefaultAsync(x => x.Id == id);

            if (id == null)
            {
                return BadRequest();
            }

            if (product == null)
            {
                return NotFound($"Id'si '{id}' olan kayıt bulunamadı!");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddUrun([FromBody] Urun urun)
        {
            urun.Id = Guid.NewGuid();

            await _angularExampleDbContext.Urunler.AddAsync(urun);
            await _angularExampleDbContext.SaveChangesAsync();

            return Ok(urun);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        #endregion
    }
}
