using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyTool.Database;
using SurveyTool.Models;

namespace SurveyTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsesController : ControllerBase
    {
        private readonly SurveyToolDbContextBase _context;

        public ResponsesController(SurveyToolDbContextBase context)
        {
            _context = context;
        }

        // GET: api/Responses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> GetResponse()
        {
            return await _context.Response.ToListAsync();
        }

        // GET: api/Responses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetResponse(int id)
        {
            var response = await _context.Response.FindAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        // PUT: api/Responses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponse(int id, Response response)
        {
            if (id != response.Id)
            {
                return BadRequest();
            }

            _context.Entry(response).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponseExists(id))
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

        // POST: api/Responses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Response>> PostResponse(Response response)
        //{
        //    _context.Response.Add(response);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetResponse", new { id = response.Id }, response);
        //}

        [HttpPost]
        public async Task<ActionResult<Response>> PostResponse([FromBody] Dictionary<string, string> jsonObject)
        {
            Console.WriteLine(jsonObject.ToString() ?? string.Empty);
            return await _context.Response.FirstOrDefaultAsync();
        }

        // DELETE: api/Responses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteResponse(int id)
        {
            var response = await _context.Response.FindAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            _context.Response.Remove(response);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool ResponseExists(int id)
        {
            return _context.Response.Any(e => e.Id == id);
        }
    }
}
