using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TasksAPI.Data;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskNotesController : ControllerBase
    {
        private readonly TasksAPIContext _context;

        public TaskNotesController(TasksAPIContext context)
        {
            _context = context;
        }

        // GET: api/TaskNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskNote>>> GetTaskNote()
        {
            string? userid = User.FindFirstValue(nameof(Models.User.Id));
            if(userid == null)
            {
                return Unauthorized();
            }
            int useridInt = int.Parse(userid);
            return await _context.TaskNote.Where(t => t.UserId == useridInt).ToListAsync();
        }

        // GET: api/TaskNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskNote>> GetTaskNote(int taskid)
        {
			string? userid = User.FindFirstValue(nameof(Models.User.Id));
			if (userid == null)
			{
				return Unauthorized();
			}

			var taskNote = await _context.TaskNote.FindAsync(taskid);
            if (taskNote == null)
            {
                return NotFound();
            }
            if(taskNote.UserId != int.Parse(userid))
            {
                return Unauthorized();
            }

            return taskNote;
        }

        // PUT: api/TaskNotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{taskid}")]
        public async Task<IActionResult> UpdateTaskNote(int taskid, TaskNote taskNote)
        {
            if (taskid != taskNote.Id)
            {
                return BadRequest("URL id does not equal task note id");
            }

			string? userid = User.FindFirstValue(nameof(Models.User.Id));
			if (userid == null)
			{
				return Unauthorized();
			}

            var existingTaskNote = await _context.TaskNote.FindAsync(taskid);
            if (existingTaskNote == null)
            {
                return NotFound();
            }
            if (existingTaskNote.UserId != int.Parse(userid))
            {
                return Unauthorized();
            }
            _context.Entry(existingTaskNote).State = EntityState.Detached;

            taskNote.UserId = int.Parse(userid);
			_context.Entry(taskNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskNoteExists(taskid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/TaskNotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskNote>> AddTaskNote(TaskNote taskNote)
        {
			string? id = User.FindFirstValue(nameof(Models.User.Id));
			if (id == null)
			{
				return Unauthorized();
			}

            taskNote.UserId = int.Parse(id);
			_context.TaskNote.Add(taskNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskNote", new { id = taskNote.Id }, taskNote);
        }

        // DELETE: api/TaskNotes/5
        [HttpDelete("{taskid}")]
        public async Task<IActionResult> DeleteTaskNote(int taskid)
        {
			string? id = User.FindFirstValue(nameof(Models.User.Id));
			if (id == null)
			{
				return Unauthorized();
			}

			var taskNote = await _context.TaskNote.FindAsync(taskid);
            if (taskNote == null)
            {
                return NotFound();
            }
			if (taskNote.UserId != int.Parse(id))
			{
				return Unauthorized();
			}

			_context.TaskNote.Remove(taskNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskNoteExists(int id)
        {
            return _context.TaskNote.Any(e => e.Id == id);
        }
    }
}
