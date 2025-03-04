using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        IMapper _imapper;
        ICategoryService _icategoryService;

        public CategoriesController(ICategoryService icategoryService, IMapper imapper)
        {
            _imapper = imapper;
            _icategoryService = icategoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            IEnumerable<Category> category = await _icategoryService.Get();
            IEnumerable<CategoryDTO> categoryDTO = _imapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(category);
            if (categoryDTO != null)
                return Ok(categoryDTO);
            return NoContent();

        }


    }
}
