using AutoMapper;
using BackendStore.Interfaces;
using BackendStore.Models;
using BackendStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult<IProductsPagination>> GetAllAsync(int page = 1, string? search = null, int size = 50, string? order = null)
        {

            int total = 1;

            List<Product> items = await FakeStoreClient.Instance.getProducts(page , search, size, order);
            

            var response = new { totalPages = total, items };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOne(int id)
        {
            Product product = await FakeStoreClient.Instance.getProductDetailDyID(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }
    }
}
