using System.Threading.Tasks;

namespace TaskStatus
{
    public class TaskStatus
    {

        [Fact]
        public void UpdateTaskStatus_ShouldChangeStatus()
        {
            // Arrange
            var taskManager = new TaskManager();
            var taskId = 1;
            taskManager.AddTask(new Task { Id = taskId, Status = "�����" });

            // Act
            taskManager.UpdateTaskStatus(taskId, "����������");

            // Assert
            var updatedTask = taskManager.GetTaskById(taskId);
            Assert.Equal("����������",updatedTask.Status); 
        }


    }
}

public class Task
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
}
