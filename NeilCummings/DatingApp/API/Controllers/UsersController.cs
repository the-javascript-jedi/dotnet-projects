using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // in BaseApiController we specify the below commented values and use inheritance and apply the values globally in the BaseApiController
    // [ApiController]
    // [Route("api/[controller]")] // /api/users - (UsersController)
    // [Authorize] - specifies only authorized users can make request
    [Authorize]
    public class UsersController : BaseApiController
    {

        //this field we can use in rest of methods
        // private readonly DataContext _context;
        // to inject some data to controller we need the constructor
        //to create a controller type ctor
        // public UsersController(DataContext context)
        // {
        //     _context = context;
        // }

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GetUsers does not need to be authorized
        // [AllowAnonymous]
        [HttpGet]
        // https://localhost:5001/api/users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            // return users;
            return Ok(await _userRepository.GetUsersAsync());
        }
        // [Authorize] is applied commonly to the api
        [HttpGet("{username}")] // /api/users/2
                                // https://localhost:5001/api/users/2
        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            // return await _context.Users.FindAsync(id);
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}