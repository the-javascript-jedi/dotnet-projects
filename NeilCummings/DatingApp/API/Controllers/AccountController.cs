using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
public class AccountController : BaseApiController
{
    // to inject some data to controller we need the constructor
    // to create a controller type ctor
    private readonly DataContext _context;
    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")] //POST: api/account/register
    // ActionResult<AppUser> is the return type
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
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
            return user;
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
