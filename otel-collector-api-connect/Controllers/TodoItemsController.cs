using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace otel_collector_api_connect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {
        public record TodoItem(int UserId, int Id, string Title, bool Completed);

        private readonly ILogger<TodoItemsController> _logger;
        private static readonly ActivitySource activitySource = new ActivitySource("TodoItemsController");

        public TodoItemsController(ILogger<TodoItemsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTodos")]
        public async Task<IEnumerable<TodoItem>> GetTodos()
        {
            using (var activity = activitySource.StartActivity("GetTodoTrace", ActivityKind.Internal))
            {
                _logger.LogInformation("Starting to fetch todos");

                var todos = await FetchTodos();

                _logger.LogInformation($"Retrieved {todos.Count} todos successfully.");
                return todos;
            }
        }

        [HttpGet("error", Name = "SimulateError")]
        public IActionResult SimulateError()
        {
            _logger.LogInformation("Simulating an error condition in the TodoItems API");
            try
            {
                throw new InvalidOperationException("This is a simulated exception for demonstration purposes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SimulateError");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("process", Name = "ProcessData")]
        public async Task<IActionResult> ProcessData()
        {
            _logger.LogInformation("Processing some data...");
            // Simulate some processing logic
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Ok("Data processed successfully");
        }

        private async Task<List<TodoItem>> FetchTodos()
        {
            // Simulating an external API call by returning hardcoded data
            var todos = new List<TodoItem>
            {
                new TodoItem(1, 1, "delectus aut autem", false),
                new TodoItem(1, 2, "quis ut nam facilis et officia qui", false),
                new TodoItem(1, 3, "fugiat veniam minus", false),
                new TodoItem(1, 4, "et porro tempora", true),
                new TodoItem(1, 5, "laboriosam mollitia et enim quasi adipisci quia provident illum", false),
                new TodoItem(1, 6, "qui ullam ratione quibusdam voluptatem quia omnis", false),
                new TodoItem(1, 7, "illo expedita consequatur quia in", false),
                new TodoItem(1, 8, "quo adipisci enim quam ut ab", true),
                new TodoItem(1, 9, "molestiae perspiciatis ipsa", false),
                new TodoItem(1, 10, "illo est ratione doloremque quia maiores aut", true),
                new TodoItem(2, 11, "vero rerum temporibus dolor", true),
                new TodoItem(2, 12, "ipsa repellendus fugit nisi", true),
                new TodoItem(2, 13, "et doloremque nulla", false),
                new TodoItem(2, 14, "repellendus sunt dolores architecto voluptatum", true),
                new TodoItem(2, 15, "ab voluptatum amet voluptas", true),
                new TodoItem(2, 16, "accusamus eos facilis sint et aut voluptatem", true),
                new TodoItem(2, 17, "quo laboriosam deleniti aut qui", true),
                new TodoItem(2, 18, "dolorum est consequatur ea mollitia in culpa", false),
                new TodoItem(2, 19, "molestiae ipsa aut voluptatibus pariatur dolor nihil", true),
                new TodoItem(2, 20, "ullam nobis libero sapiente ad optio sint", true),
                // Continue adding more items as needed...
            };

            // Shuffling the list using Guid to generate randomness
            Random rng = new Random();
            var shuffledTodos = todos.OrderBy(a => rng.Next()).ToList();

            await Task.Delay(TimeSpan.FromSeconds(1));
            return shuffledTodos;
        }
    }
}
