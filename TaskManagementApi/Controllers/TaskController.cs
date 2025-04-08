using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskModel = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskModel> _tasks = new();

        [HttpGet]
        //why IEnumerable<Task> instead of List<Task>?
        //IEnumerable is more flexible and allows for deferred execution, while List is a concrete collection type.
        //Using IEnumerable allows for more efficient memory usage and can work with different types of collections.
        //It also allows for LINQ queries to be performed on the collection without needing to convert it to a List first.
        public ActionResult<IEnumerable<TaskModel>> GetTasks()
        {
            return Ok(_tasks);
        }

        [HttpGet("{id}")]
        //why ActionResult<Task> instead of Task?
        //ActionResult allows for returning different types of responses (e.g., NotFound, Ok) while Task is just the data type.
        public ActionResult<TaskModel> GetTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskModel> CreateTask([FromBody] TaskModel task)
        {
            task.Id = _tasks.Count + 1; // Simple ID generation
            _tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
    }
}