using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MeetingsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // get all meetings
        [HttpGet]
        public async Task<ActionResult<List<Meeting>>> Get()
        {
            return await _dbContext.Meetings.ToListAsync();
        }

        // get RSVPs from meeting
        [Authorize]
        [HttpGet("{name}")]
        public async Task<ActionResult<List<RSVP>>> Get(string name)
        {
            var meeting = await _dbContext.Meetings.FindAsync(name);
            return meeting.RSVPs;
        }

        // post new meeting 
        [Authorize]
        [HttpPost]
        public async Task Post(Meeting model)
        {
            await _dbContext.Meetings.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        // post new RSVP
        [Authorize]
        [HttpPost]
        public async Task Post(RSVP model)
        {
            await _dbContext.RSVPs.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        // update RSVP
        [Authorize]
        [HttpPut("{name}")]
        public async Task<ActionResult> Put(string value, string name, Meeting model)
        {
            var exists = await _dbContext.Meetings.AnyAsync(m => m.Name == name);
            if (!exists)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Meetings.Update(model);

                await _dbContext.SaveChangesAsync();

                return Ok();
            } 
        }
    }
}