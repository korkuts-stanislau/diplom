using Microsoft.AspNetCore.Mvc;
using Resource.Data;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }
    
    [Route("")]
    [HttpGet]
    public IActionResult Test()
    {
        return Ok(_context.Profiles);
    }
}