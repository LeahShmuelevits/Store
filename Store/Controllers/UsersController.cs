using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IMapper _imapper;
        IUserService _iuserservice ;
         private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService iuserservice, IMapper imapper, ILogger<UsersController> logger)
        {   _logger = logger;
            _imapper = imapper;
            _iuserservice = iuserservice;
        }
        
        
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "how", "are you" };
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            User user = await _iuserservice.GetById(id);
            GetUserDTO userDTO = _imapper.Map<User, GetUserDTO>(user);
            if (userDTO == null)
                return NoContent();
            return Ok(userDTO);

        }

        //POST api/<UsersController>0w
       [HttpPost]
       [Route("login")]
        public async Task<ActionResult<GetUserDTO>> PostLogin([FromQuery] string username,string password)
        {
            //where we will put the ask of the null?
            User user = await _iuserservice.PostLoginS(username, password);
            GetUserDTO userDTO = _imapper.Map<User, GetUserDTO>(user);
            if (userDTO != null)
            {
                _logger.LogCritical($"Login attempted with User name-{username} and with password-{password}");
                return Ok(userDTO);
            }
               
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> PostNewUser([FromBody] UserDTO user)
        {
            User user1 = _imapper.Map<UserDTO, User>(user);
            User newUser = await _iuserservice.Post(user1);
            if (newUser == null)
                return BadRequest();
            GetUserDTO newUser1 = _imapper.Map<User, GetUserDTO>(newUser);
            if (newUser1 != null)
                return Ok(newUser1);
            return NoContent();
        }



        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetUserDTO>> Put(int id, [FromBody] UserDTO user)
        {
            User user1 = _imapper.Map<UserDTO, User>(user);
            User user2=await _iuserservice.Put(id, user1);
            if (user2 == null)
            {
                return NotFound();
            }
            GetUserDTO newUser = _imapper.Map<User, GetUserDTO>(user2);
            return Ok(newUser);
        }

        [HttpPost("CheckPassword")]
        public ActionResult<int> CheckPassword([FromBody] string password)
        {
           int resPassword = _iuserservice.CheckPassword(password);
            return resPassword;


        }

    }
}
