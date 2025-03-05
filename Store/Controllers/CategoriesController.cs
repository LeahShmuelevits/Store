//using AutoMapper;
//using DTO;
//using Entities;
//using Microsoft.AspNetCore.Mvc;
//using Services;
//using System.Collections.Generic;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace Store.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoriesController : ControllerBase
//    {
//        IMapper _imapper;
//        ICategoryService _icategoryService;

//        public CategoriesController(ICategoryService icategoryService, IMapper imapper)
//        {
//            _imapper = imapper;
//            _icategoryService = icategoryService;
//        }

//        // GET: api/<CategoriesController>
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Category>>> Get()
//        {
//            IEnumerable<Category> category = await _icategoryService.Get();
//            IEnumerable<CategoryDTO> categoryDTO = _imapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(category);
//            if (categoryDTO != null)
//                return Ok(categoryDTO);
//            return NoContent();

//        }


//    }
//}
using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _imapper;
        private readonly ICategoryService _icategoryService;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "CategoriesCacheKey";
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        public CategoriesController(ICategoryService icategoryService, IMapper imapper, IMemoryCache memoryCache)
        {
            _imapper = imapper;
            _icategoryService = icategoryService;
            _memoryCache = memoryCache;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<CategoryDTO> categoryDTO))
            {
                var category = await _icategoryService.Get();
                categoryDTO = _imapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(category);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                };

                _memoryCache.Set(CacheKey, categoryDTO, cacheEntryOptions);
            }

            if (categoryDTO != null)
                return Ok(categoryDTO);
            return NoContent();
        }
    }
}