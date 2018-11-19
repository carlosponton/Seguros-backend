using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seguros.Models;

namespace Seguros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        SecureContext db = new SecureContext();

        // GET: api/Policies
        [HttpGet]
        public IEnumerable<Policy> GetPolicy()
        {
            var query = (from p in db.Policy
                         join c in db.Client on p.ClientId equals c.Id
                         join t in db.Type on p.TypeId equals t.Id
                         select new Policy
                         {
                             Id = p.Id,
                             Description = p.Description,
                             ClientId = p.ClientId,
                             Danger = p.Danger,
                             Date = p.Date,
                             Name = p.Name,
                             Period = p.Period,
                             TypeId = p.TypeId,
                             Price = p.Price,
                             Client = c,
                             Type = t
                         });
            return query;
        }

        // GET: api/Policies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var policy = await db.Policy.FindAsync(id);

            var query = (from p in db.Policy
                         join c in db.Client on p.ClientId equals c.Id
                         join t in db.Type on p.TypeId equals t.Id
                         where p.Id == id
                         select new Policy
                         {
                             Id = p.Id,
                             Description = p.Description,
                             ClientId = p.ClientId,
                             Danger = p.Danger,
                             Date = p.Date,
                             Name = p.Name,
                             Price = p.Price,
                             Period = p.Period,
                             TypeId = p.TypeId,
                             Client = c,
                             Type = t
                         }).FirstOrDefault();

            if (query == null)
            {
                return NotFound();
            }

            return Ok(query);
        }

        // PUT: api/Policies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicy([FromRoute] int id, [FromBody] Policy policy)
        {

            var type = db.Type.Find(policy.TypeId);

            if (type.Percentage > 50 && policy.Danger == Danger.Alto)
            {
                return BadRequest();
            }

            if (id != policy.Id)
            {
                return BadRequest();
            }

            db.Entry(policy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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

        // POST: api/Policies
        [HttpPost]
        public async Task<IActionResult> PostPolicy(Policy policy)
        {
            var type = db.Type.Find(policy.TypeId);
            System.Diagnostics.Debug.WriteLine(type);

            if (type.Percentage > 50 && policy.Danger == Danger.Alto)
            {
                return BadRequest();
            }

            db.Policy.Add(policy);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetPolicy", new { id = policy.Id }, policy);
        }

        // DELETE: api/Policies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy([FromRoute] int id)
        {

            var policy = await db.Policy.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            db.Policy.Remove(policy);
            await db.SaveChangesAsync();

            return Ok(policy);
        }

        // GET: api/Policies/type
        [HttpGet("types")]
        public IEnumerable<Models.Type> GetType()
        {
            return db.Type;
        }

        private bool PolicyExists(int id)
        {
            return db.Policy.Any(e => e.Id == id);
        }
    }
}