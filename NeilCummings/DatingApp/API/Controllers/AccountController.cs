using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
public class AccountController : BaseApiController
{
    // to inject some data to controller we need the constructor
    // to create a controller type ctor
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    // also inject the tokenService with the ITokenService
    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")] //POST: api/account/register
    // ActionResult<UserDto> is the return type
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        //check to see if username already exists
        if (await UserExists(registerDto.Username))
        {
            return BadRequest("username is taken!!!");
        }
        else
        {
            //when we specify using in front of variable creation - it will do automatic garbage collection - for the HMACSHA512 because it implements the dispose method
            using var hmac = new HMACSHA512();
            // create a user
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            // save changes to db
            await _context.SaveChangesAsync();
            //saved user will be sent as response to the api endpoint
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
            };
        }
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        // if user not present we get null in response.
        // SingleOrDefault - throws exception if more than one value is present
        //FirstOrDefault - returns first value or default value
        var user = await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
        if (user == null)
        {
            return Unauthorized();
        }
        else
        {
            // check if the user password is valid by passing in the stored password salt to the hashing algorithm
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            // check the saved password salt with the computed hash
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("invalid password");
                }
            }
            //success - return the user
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                //only if we include the photos .Include(p => p.Photos) the photos are displayed
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
        }
    }
    //check to see if username already exists
    private async Task<bool> UserExists(string username)
    {
        // AnyAsync- will determine if the value exists in the list or not
        // return true or false if username exists in the list
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}
