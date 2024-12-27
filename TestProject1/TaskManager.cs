using System.Threading.Tasks;

namespace TaskStatus
{
    public class TaskManager
    {
        private readonly List<Task> tasks = new();

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void UpdateTaskStatus(int taskId, string newStatus)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new ArgumentException("Завдання не знайдено");

            if (!IsStatusTransitionValid(task.Status, newStatus))
                throw new InvalidOperationException("Недозволена зміна статусу");

            task.Status = newStatus;
        }

        private bool IsStatusTransitionValid(string currentStatus, string newStatus)
        {
            var allowedTransitions = new Dictionary<string, List<string>>
            {
                { "Очікує", new List<string> { "Виконується" } },
                { "Виконується", new List<string> { "Завершено" } }
            };

            return allowedTransitions.ContainsKey(currentStatus) && allowedTransitions[currentStatus].Contains(newStatus);
        }

        public Task GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }
        public List<Task> GetTasksByStatus(string status)
        {
            return tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
    public class TaskManagerTests
    {
        [Fact]
        public void UpdateTaskStatus_ShouldChangeStatus()
        {
            // Arrange
            var taskManager = new TaskManager();
            var taskId = 1;
            taskManager.AddTask(new Task { Id = taskId, Status = "Очікує" });

            // Act
            taskManager.UpdateTaskStatus(taskId, "Виконується");

            // Assert
            var updatedTask = taskManager.GetTaskById(taskId);
            Assert.Equal("Виконується", updatedTask.Status);
        }

        [Fact]
        public void GetTasksByStatus_ShouldReturnTasksWithSpecifiedStatus()
        {
            // Arrange
            var taskManager = new TaskManager();
            taskManager.AddTask(new Task { Id = 1, Status = "Очікує" });
            taskManager.AddTask(new Task { Id = 2, Status = "Виконується" });
            taskManager.AddTask(new Task { Id = 3, Status = "Очікує" });

            // Act
            var tasks = taskManager.GetTasksByStatus("Очікує");

            // Assert
            Assert.Equal(2, tasks.Count);
            Assert.All(tasks, t => Assert.Equal("Очікує", t.Status));
        }
    }
}