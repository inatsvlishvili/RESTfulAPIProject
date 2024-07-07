using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIProject.Dtos;
using RESTfulAPIProject.Models;
using RESTfulAPIProject.Repositories;

namespace RESTfulAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productDTOs);
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            product = await _repository.AddProductAsync(product);
            productDTO = _mapper.Map<ProductDTO>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDTO);
            await _repository.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("SearchProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchProducts(string searchText, int pageNumber = 1, int pageSize = 10)
        {
            var (products, totalRecords) = await _repository.SearchProductsAsync(searchText, pageNumber, pageSize);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            Response.Headers.Append("X-Total-Count", totalRecords.ToString());
            //Response.Headers.Add("X-Total-Count", totalRecords.ToString());
            return Ok(productDTOs);
        }
    }
}
