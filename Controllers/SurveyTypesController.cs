using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SurveyTypesController : ControllerBase
    {
        private readonly SurveyToolDbContextBase _context;

        public SurveyTypesController(SurveyToolDbContextBase context)
        {
            _context = context;
        }

        // GET: api/SurveyTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyType>>> GetSurveyType()
        {
            var testing = _context.SurveyType.ToList();
            Debug.Write(testing.Count);
            return await _context.SurveyType.ToListAsync();
        }

        // GET: api/SurveyTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyType>> GetSurveyType(int id)
        {
            var surveyType = await _context.SurveyType.FindAsync(id);

            if (surveyType == null)
            {
                return NotFound();
            }

            return surveyType;
        }

        // PUT: api/SurveyTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyType(int id, SurveyType surveyType)
        {
            if (id != surveyType.Id)
            {
                return BadRequest();
            }

            _context.Entry(surveyType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyTypeExists(id))
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

        // POST: api/SurveyTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SurveyType>> PostSurveyType(SurveyType surveyType)
        {
            _context.SurveyType.Add(surveyType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveyType", new { id = surveyType.Id }, surveyType);
        }

        // DELETE: api/SurveyTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SurveyType>> DeleteSurveyType(int id)
        {
            var surveyType = await _context.SurveyType.FindAsync(id);
            if (surveyType == null)
            {
                return NotFound();
            }

            _context.SurveyType.Remove(surveyType);
            await _context.SaveChangesAsync();

            return surveyType;
        }

        private bool SurveyTypeExists(int id)
        {
            return _context.SurveyType.Any(e => e.Id == id);
        }
    }
}
