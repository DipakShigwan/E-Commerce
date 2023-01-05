using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public UsersController(ECommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> Getusers()
        {
            return await _context.userRegistrations.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistration>> GetUser(Guid id)
        {
            var user = await _context.userRegistrations.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [HttpGet("{Email}/{Password}")]
        public async Task<ActionResult<UserRegistration>> IsApprovedUser(string Email,string Password)
        {

            //bool isUserExist = await _context.userRegistrations.FindAsync(e => e.Email == Email && e.Password == Password);
            //if (isUserExist == false)
            //{
            //    return NotFound();
            //}
            var user = await _context.userRegistrations.Where(e => e.Email == Email && e.Password == Password).FirstOrDefaultAsync(); 
            if (user == null)
            {
                return NotFound();
            }

            //bool isUserApproved = await _context.userRegistrations.FindAsync(e => e.Id == user.Id);
            //if (isUserApproved == false)
            //{
            //    return NotFound();
            //}


            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserRegistration user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRegistration>> PostUser(UserRegistration user)
        {
            _context.userRegistrations.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.userRegistrations.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.userRegistrations.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.userRegistrations.Any(e => e.Id == id);
        }
       
    }
}
