using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

namespace otel_collector_api_connect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {
        public record TodoItem(int UserId, int Id, string Title, bool Completed);

        private readonly ILogger<TodoItemsController> _logger;
        private readonly HttpClient _httpClient;
        private static readonly ActivitySource activitySource = new ActivitySource("TodoItemsController");

        public TodoItemsController(ILogger<TodoItemsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet(Name = "GetTodos")]
        public async Task<IEnumerable<TodoItem>> GetTodos()
        {
            using (var activity = activitySource.StartActivity("GetTodos"))
            {
                _logger.LogInformation("Starting to fetch todos from external service");

                var todos = await FetchTodos();

                _logger.LogInformation("Todos retrieved successfully.");
                return todos;
            }
        }

        private async Task<List<TodoItem>> FetchTodos()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to retrieve todos: {response.StatusCode}");
                return new List<TodoItem>();
            }

            var todoItems = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
            if (todoItems is null)
            {
                _logger.LogError("Failed to deserialize todo items.");
                return new List<TodoItem>();
            }

            _logger.LogInformation($"Fetched {todoItems.Count} todos from the external API.");
            return todoItems;
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
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Ok("Data processed successfully");
        }
    }
}
