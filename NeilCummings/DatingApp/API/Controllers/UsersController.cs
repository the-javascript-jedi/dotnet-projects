using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
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
            var users = await _userRepository.getMembersAsync();
            // using automapper
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(users);

        }
        // [Authorize] is applied commonly to the api
        [HttpGet("{username}")] // /api/users/2
                                // https://localhost:5001/api/users/2
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // // return await _context.Users.FindAsync(id);
            // var user = await _userRepository.GetUserByUsernameAsync(username);
            // // using automapper
            // return _mapper.Map<MemberDto>(user);
            return await _userRepository.getMemberAsync(username);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();
            // use mapper to match the DTO to the user
            _mapper.Map(memberUpdateDto, user);
            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return NotFound();
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            // if first photo set the photo as main photo
            if (user.Photos.Count == 0) photo.IsMain = true;
            user.Photos.Add(photo);
            // NSTODO getting error
            if (await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);
            return BadRequest("Problem Adding Photo");
        }

    }
}