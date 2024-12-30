using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskStatus
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void UpdateTaskStatus(int taskId, string newStatus)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new ArgumentException("Task not found.");

            if (!IsValidStatusChange(task.Status, newStatus))
                throw new InvalidOperationException("Invalid status transition.");

            task.Status = newStatus;
        }

        public List<Task> GetTasksByStatus(string status)
        {
            return tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private bool IsValidStatusChange(string currentStatus, string newStatus)
        {
            var validTransitions = new Dictionary<string, List<string>>
            {
                { "Очікує", new List<string> { "Виконується" } },
                { "Виконується", new List<string> { "Завершено" } },
                { "Завершено", new List<string>() }
            };

            return validTransitions.ContainsKey(currentStatus) && validTransitions[currentStatus].Contains(newStatus);
        }
    }
}

