using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IMapper _imapper;
        IOrderService _iorderService;
        private readonly ILogger<UsersController> _logger;
        public OrdersController(IOrderService iorderService,IMapper imapper, ILogger<UsersController> logger)
        {
            _logger = logger;
            _imapper = imapper;
            _iorderService = iorderService;
        }


        [HttpPost]
        public async Task<ActionResult<GetOrderDTO>> Post([FromBody] OrderDTO order)
        {

            Order order1 = _imapper.Map<OrderDTO, Order>(order);

            Order check = await _iorderService.Post(order1);

            if (check!=null)
            {   
                GetOrderDTO order2 = _imapper.Map<Order, GetOrderDTO>(order1);
             return Ok(order2);
            }
            else
            {
                _logger.LogInformation($"{order.UserId} try to change the sum of the order!!!");
                return Unauthorized();
            }
           
            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await _iorderService.GetById(id);
            GetOrderDTO orderDTO = _imapper.Map<Order, GetOrderDTO>(order);
            if (orderDTO == null)
                return NoContent();
            return Ok(orderDTO);

        }

    
    }
}
