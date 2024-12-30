global using Xunit;
using TaskStatus;
using System;
using System.Linq;

namespace TaskStatus
{
    public class TaskManagerTests
    {
        [Fact]
        public void UpdateTaskStatus_ShouldUpdateStatus()
        {
            // Arrange
            var taskManager = new TaskManager();
            var task = new Task { Id = 1, Name = "Test Task", Status = "Очікує" };
            taskManager.AddTask(task);

            // Act
            taskManager.UpdateTaskStatus(1, "Виконується");

            // Assert
            Assert.Equal("Виконується", task.Status);
        }

        [Fact]
        public void UpdateTaskStatus_ShouldThrowExceptionForInvalidTransition()
        {
            // Arrange
            var taskManager = new TaskManager();
            var task = new Task { Id = 1, Name = "Test Task", Status = "Завершено" };
            taskManager.AddTask(task);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => taskManager.UpdateTaskStatus(1, "Очікує"));
        }

        [Fact]
        public void GetTasksByStatus_ShouldReturnCorrectTasks()
        {
            // Arrange
            var taskManager = new TaskManager();
            taskManager.AddTask(new Task { Id = 1, Name = "Task 1", Status = "Очікує" });
            taskManager.AddTask(new Task { Id = 2, Name = "Task 2", Status = "Виконується" });
            taskManager.AddTask(new Task { Id = 3, Name = "Task 3", Status = "Очікує" });

            // Act
            var tasks = taskManager.GetTasksByStatus("Очікує");

            // Assert
            Assert.Equal(2, tasks.Count);
            Assert.All(tasks, t => Assert.Equal("Очікує", t.Status));
        }
    }
}
