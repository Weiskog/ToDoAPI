using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly DataContext _context;
        public NoteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Note>>> Get()
        {

            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return BadRequest("Note not found.");
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<List<Note>>> AddNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Note>>> UpdateNote(Note request)
        {
            var note = await _context.Notes.FindAsync(request.Id);
            if (note == null)
            {
                return BadRequest("Note not found.");
            }

            note.ToDoNote = request.ToDoNote;
            note.Priority = request.Priority;
            note.Completed = request.Completed;

            await _context.SaveChangesAsync();
            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Note>>> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return BadRequest("Note not found.");
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notes.ToListAsync());
        }
    }
}
