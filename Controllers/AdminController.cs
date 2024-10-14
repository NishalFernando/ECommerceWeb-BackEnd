using ECommerceWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public AdminController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> getAdmins()
        {
            if (_dbContext.Admin == null)
            {
                return NotFound();
            }
            return await _dbContext.Admin.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> getAdmin(int id)
        {
            if (_dbContext.Admin == null)
            {
                return NotFound();
            }
            var admin = await _dbContext.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return admin;
        }

        [HttpPost]
        public async Task<ActionResult<Admin>> createAdmin(Admin admin)
        {
            _dbContext.Admin.Add(admin);
            await _dbContext.SaveChangesAsync();

            return Ok(admin);
        }
    }
}
