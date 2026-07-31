using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductsDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams, CancellationToken ct)
        {
            var products = await _productService.GetAllProductsAsync(queryParams, ct);
            return ToActionResult(products);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductsDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsDto>> GetProduct(int id , CancellationToken ct)
        {
            var product = await _productService.GetProductByIdAsync(id, ct);
            return ToActionResult(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var brands = await _productService.GetAllBrandsAsync(ct);
            return ToActionResult(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllTypes(CancellationToken ct)
        {
            var types = await _productService.GetAllBrandsAsync(ct);
            return ToActionResult(types);
        }

    }
}
