using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyTool.Database;
using SurveyTool.Enumerations;
using SurveyTool.Models;

namespace SurveyTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSetsController : ControllerBase
    {
        private readonly SurveyToolDbContextBase _context;
        private CsvConfiguration configuration = new CsvConfiguration(new System.Globalization.CultureInfo("en"));
        public List<CSVFile> results = new List<CSVFile>();

        public QuestionSetsController(SurveyToolDbContextBase context)
        {
            _context = context;
        }

        // GET: api/QuestionSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionSet>>> GetQuestionSet()
        {
            return await _context.QuestionSet.ToListAsync();
        }

        // GET: api/QuestionSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionSet>> GetQuestionSet(int id)
        {
            var questionSet = await _context.QuestionSet.FindAsync(id);

            if (questionSet == null)
            {
                return NotFound();
            }

            return questionSet;
        }

        // PUT: api/QuestionSets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionSet(int id, QuestionSet questionSet)
        {
            if (id != questionSet.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionSetExists(id))
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
        [HttpPost]
        public async Task<ActionResult<QuestionSet>> PostQuestionSet([FromForm] QuestionSet data, IFormFile fileSource)
        {
            try
            {
                //Create and add the Questions
                var csvReader = new CsvReader(new StreamReader(fileSource.OpenReadStream()), configuration);
                configuration.HeaderValidated = null;
                configuration.MissingFieldFound = null;
                var rows = csvReader.GetRecords<CSVFile>();

                //need to bring ResponseTypes into a list to allow for case-insensitvity.
                var responseTypes = _context.ResponseType.ToList();

                foreach (CSVFile q in rows)
                {
                    var rt = responseTypes.Single(rt => rt.ResponseTypeName.Equals(q.Type, StringComparison.CurrentCultureIgnoreCase));

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CSVFile, Question>()
                            .ForMember(d => d.Id, m => m.Ignore())
                            .ForMember(d => d.QuestionText, m => m.MapFrom(s => s.Prompt))
                            .ForMember(d => d.OrdinalPosition, m => m.MapFrom(s => string.IsNullOrEmpty(s.Order) ? s.ID : Convert.ToInt32(s.Order)))
                            .ForMember(d => d.Min, m => m.MapFrom(s => rt.Id.Equals((int)ResponseTypeEnum.Binary)
                                ? 0
                                : string.IsNullOrEmpty(s.Min)
                                    ? rt.Id.Equals((int)ResponseTypeEnum.Scale)
                                        ? 1
                                        : 0
                                    : Convert.ToInt32(s.Min)))
                            .ForMember(d => d.Max, m => m.MapFrom(s => rt.Id.Equals((int)ResponseTypeEnum.Binary)
                                ? 1
                                : string.IsNullOrEmpty(s.Max)
                                    ? rt.Id.Equals((int)ResponseTypeEnum.Scale)
                                        ? 10
                                        : 0
                                    : Convert.ToInt32(s.Max)))
                            .ForMember(d => d.DependencyQuestionId, m => m.MapFrom(s => string.IsNullOrEmpty(s.Dependency) ? 0 : Convert.ToInt32(s.Dependency)))
                            .ForMember(d => d.ResponseTypeId, m => m.MapFrom(s => rt.Id))
                            .ForMember(d => d.ResponseType, m => m.MapFrom(s => rt))
                            .ForMember(d => d.QuestionCategoryId, m => m.MapFrom(s => QuestionCategoryEnum.Sentiment));
                    });

                    var mapper = config.CreateMapper();
                    Question question = mapper.Map<Question>(q);

                    switch (question.ResponseTypeId)
                    {
                        case (int)ResponseTypeEnum.Binary:
                            question.Response.Add(new Response
                            {
                                ResponseValue = 0,
                                OrdinalPosition = 1,
                                ResponseText = string.IsNullOrWhiteSpace(q.MinLabel) ? "No" : q.MinLabel.Trim()
                            });

                            question.Response.Add(new Response
                            {
                                ResponseValue = 1,
                                OrdinalPosition = 2,
                                ResponseText = string.IsNullOrWhiteSpace(q.MinLabel) ? "Yes" : q.MaxLabel.Trim()
                            });

                            break;

                        case (int)ResponseTypeEnum.Scale:
                            if (question.Min.HasValue && question.Max.HasValue)
                            {
                                int i;
                                for (i = question.Min.Value; i <= question.Max.Value; i++)
                                {
                                    var response = new Response
                                    {
                                        ResponseValue = i,
                                        OrdinalPosition = i,
                                        ResponseText = i.ToString()
                                    };

                                    if (i.Equals(question.Min.Value))
                                    {
                                        response.ResponseText = string.IsNullOrWhiteSpace(q.MinLabel) ? $"{i} - Strongly Disagree" : $"{i} - {q.MinLabel}";
                                    }

                                    if (i.Equals(question.Max.Value))
                                    {
                                        response.ResponseText = string.IsNullOrWhiteSpace(q.MaxLabel) ? $"{i} - Strongly Agree" : $"{i} - {q.MaxLabel}";
                                    }

                                    question.Response.Add(response);
                                }
                            }
                            break;
                    }

                    //Map user-provided response options to new Response objects.
                    var responseOptions = q.ResponseOptions.Trim();

                    if (!string.IsNullOrWhiteSpace(responseOptions))
                    {
                        var responseArray = responseOptions.Split(',');
                        int length = responseArray.Length;

                        for (int i = 0; i < length; i++)
                        {
                            question.Response.Add(new Response
                            {
                                OrdinalPosition = i + 1,
                                ResponseText = responseArray[i].Trim()
                            });
                        }
                    }

                    data.Questions.Add(question);
                }
                data.DateTimeCreated = DateTime.UtcNow;
                _context.QuestionSet.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ioe)
            {
                //TODO: Logging to ASP.NET Core Logging service could take place here, but we would need to have initialized such a service during application startup.
                //For now, just return a Problem response with the error's message.

                return this.Problem(ioe.Message);
            }
            return CreatedAtAction("PostQuestionSet", new { id = data.Id }, data);
        }

        // DELETE: api/QuestionSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionSet>> DeleteQuestionSet(int id)
        {
            var questionSet = await _context.QuestionSet.FindAsync(id);
            if (questionSet == null)
            {
                return NotFound();
            }

            _context.QuestionSet.Remove(questionSet);
            await _context.SaveChangesAsync();

            return questionSet;
        }

        private bool QuestionSetExists(int id)
        {
            return _context.QuestionSet.Any(e => e.Id == id);
        }

        [HttpGet("GetQuestionSetsBySurveyId/{surveyId}")]
        public async Task<ActionResult<List<QuestionSet>>> GetQuestionSetsBySurveyId(int surveyId)
        {
            var questionSets = _context.SurveyQuestionSets.Where(sqs => sqs.SurveyId.Equals(surveyId))
                .Include(sqs => sqs.QuestionSet)
                    .ThenInclude(qs => qs.Questions)
                        .ThenInclude(q => q.Response)
                        .Select(sqs => sqs.QuestionSet);

            foreach (var qs in questionSets)
            {
                foreach (var q in qs.Questions)
                {
                    q.Response = q.Response.OrderBy(r => r.OrdinalPosition).ToList();
                }
            }

            return await questionSets.ToListAsync();
        }
    }
}
