using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
// in BaseApiController we specify the below commented values and use inheritance and apply the values globally in the BaseApiController
// [ApiController]
// [Route("api/[controller]")] // /api/users - (UsersController)
public class UsersController : BaseApiController
{

    //this field we can use in rest of methods
    private readonly DataContext _context;
    // to inject some data to controller we need the constructor
    //to create a controller type ctor
    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    // https://localhost:5001/api/users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id}")] // /api/users/2
    // https://localhost:5001/api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
