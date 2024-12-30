using System.Threading.Tasks;

namespace TaskStatus
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } // "Очікує", "Виконується", "Завершено"
    }
}
