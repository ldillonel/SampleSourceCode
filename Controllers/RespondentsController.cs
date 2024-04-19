using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyTool.Database;
using SurveyTool.Models;

namespace SurveyTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondentsController : ControllerBase
    {
        private readonly SurveyToolDbContextBase _context;

        public RespondentsController(SurveyToolDbContextBase context)
        {
            _context = context;
        }

        // GET: api/Respondents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Respondent>>> GetRespondent()
        {
            return await _context.Respondent.ToListAsync();
        }

        // GET: api/Respondents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Respondent>> GetRespondent(string id)
        {
            Guid guid = Guid.Parse(id);

            var respondent = await _context.Respondent.FindAsync(guid);

            if (respondent == null)
            {
                return NotFound();
            }

            return respondent;
        }

        // PUT: api/Respondents/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRespondent(string id, Respondent respondent)
        {
            Guid guid = Guid.Parse(id);

            if (guid != respondent.Id)
            {
                return BadRequest();
            }

            _context.Entry(respondent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespondentExists(guid))
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

        // POST: api/Respondents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Respondent>> PostRespondent(Respondent respondent)
        {
            _context.Respondent.Add(respondent);
            await _context.SaveChangesAsync();

            return respondent;
        }

        // DELETE: api/Respondents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Respondent>> DeleteRespondent(string id)
        {
            Guid guid = Guid.Parse(id);
            var respondent = await _context.Respondent.FindAsync(guid);

            if (respondent == null)
            {
                return NotFound();
            }

            _context.Respondent.Remove(respondent);
            await _context.SaveChangesAsync();

            return respondent;
        }

        private bool RespondentExists(Guid id)
        {
            return _context.Respondent.Any(e => e.Id == id);
        }
    }
}
