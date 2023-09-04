using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<AppUser>> Register(string username, string password)
    {
        //when we specify using in front of variable creation - it will do automatic garbage collection - for the HMACSHA512 because it implements the dispose method
        using var hmac = new HMACSHA512();
        // create a user
        var user = new AppUser
        {
            UserName = username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };
        _context.Users.Add(user);
        // save changes to db
        await _context.SaveChangesAsync();
        //saved user will be sent as response to the api endpoint
        return user;
    }
}
