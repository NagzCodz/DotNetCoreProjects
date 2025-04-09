using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //why IEnumerable<Task> instead of List<Task>?
        //IEnumerable is more flexible and allows for deferred execution, while List is a concrete collection type.
        //Using IEnumerable allows for more efficient memory usage and can work with different types of collections.
        //It also allows for LINQ queries to be performed on the collection without needing to convert it to a List first.
        //async is used to indicate that the method is asynchronous, allowing for non-blocking operations.
        //Task is a representation of an asynchronous operation that can return a value.
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            await Task.Delay(10); // Simulate async work
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        //why ActionResult<Task> instead of Task?
        //ActionResult allows for returning different types of responses (e.g., NotFound, Ok) while Task is just the data type.
        public async Task<ActionResult<Tasks>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Tasks>> CreateTask([FromBody] Tasks task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
    }
}