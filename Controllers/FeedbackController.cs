using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyTool.Database;
using SurveyTool.Models;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Net.Http.Headers;

namespace SurveyTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly SurveyToolDbContextBase _context;

        public FeedbackController(SurveyToolDbContextBase context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
        {
            return await _context.Feedback.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(Guid id)
        {
            var feedback = await _context.Feedback.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/Feedback/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(Guid id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedback
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            try
            {
                _context.Feedback.Add(feedback);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            return feedback;
        }

        [HttpPost("PostFeedbackArray")]
        public async Task<ActionResult<Feedback>[]> PostFeedbackArray(Feedback[] feedback)
        {
            try
            {
                return await Task.WhenAll(feedback.Select(f => PostFeedback(f)));

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return new ActionResult<Feedback>[] { Problem("A problem occurred while adding Feedback array.") };
        }
        
        [HttpPost("ExportFeedbackArray")]
        public FileStreamResult ExportFeedbackArray(Feedback[] feedback)
        {
            CsvConfiguration configuration = new CsvConfiguration(new System.Globalization.CultureInfo("en"));
            var records = _context.Feedback.Where(f => (feedback.Select(f => f.Id).Contains(f.Id)))
                .Include(f => f.Respondent)
                .Include(f => f.ResultResponse);

            MemoryStream ms;
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, configuration))
            {
                var data = records.SelectMany(fb => fb.ResultResponse.OrderBy(result => result.Id)).Select(rr => new
                {
                    SurveyId = rr.Feedback.SurveyId,
                    Survey = rr.Feedback.Survey.SurveyName,
                    RespondentId = rr.Feedback.RespondentId,
                    Completed = rr.Feedback.DateTimeCreated.ToShortDateString(),
                    ResponseType = rr.Question.ResponseType,
                    Question = rr.Question.QuestionText,
                    Response = rr.ResponseText ?? rr.Response.ResponseText
                }).ToList();
                csvWriter.WriteRecords(data);
                streamWriter.Flush();
                var result = memoryStream.ToArray();
                ms = new MemoryStream(result);
            }
            //return memory stream;
            Response.ContentType = new MediaTypeHeaderValue("application/octet-stream").ToString();// Content type
            return new FileStreamResult(ms, "text/csv");
        }


        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feedback>> DeleteFeedback(Guid id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedback.Remove(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        private bool FeedbackExists(Guid id)
        {
            return _context.Feedback.Any(e => e.Id == id);
        }
    }
}
