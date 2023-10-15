using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GetUsers does not need to be authorized
        // [AllowAnonymous]
        [HttpGet]
        // https://localhost:5001/api/users
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            // return users;
            var users = await _userRepository.GetUsersAsync();
            // using automapper
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToReturn);

        }
        // [Authorize] is applied commonly to the api
        [HttpGet("{username}")] // /api/users/2
                                // https://localhost:5001/api/users/2
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // return await _context.Users.FindAsync(id);
            var user = await _userRepository.GetUserByUsernameAsync(username);
            // using automapper
            return _mapper.Map<MemberDto>(user);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null) return NotFound();
            // use mapper to match the DTO to the user
            _mapper.Map(memberUpdateDto, user);
            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");
        }
    }
}