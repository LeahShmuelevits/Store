using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IMapper _imapper;
        IProductService _iproductService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService iproductService,IMapper imapper,ILogger<ProductsController>logger)
        {
            _logger = logger;
            _imapper = imapper;
            _iproductService = iproductService;
        }

        // GET: api/<Product>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery]string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            IEnumerable<Product> product = await _iproductService.Get(desc, minPrice, maxPrice, categoryIds);
            IEnumerable<ProductDTO> productDTO = _imapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
            _logger.LogInformation("The application load succefully!!!!!!!");
            return Ok(productDTO);

            

        }
    }
}
