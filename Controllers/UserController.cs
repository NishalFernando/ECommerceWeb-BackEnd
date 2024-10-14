using ECommerceWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ECommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public UserController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> getUsers()
        {
            if (_dbContext.User == null)
            {
                return NotFound();
            }
            return await _dbContext.User.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> getUser(int id)
        {
            if (_dbContext.User == null)
            {
                return NotFound();
            }
            var user = await _dbContext.User.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> createUser(User user)
        {
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> updateUser(int id,User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteUser(int id)
        {
            if(_dbContext.User == null)
            {
                return NotFound();
            }
            var user = await _dbContext.User.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            _dbContext.User.Remove(user);
            await _dbContext.SaveChangesAsync();
            
            return Ok(user);
        }


    }
}
