// Onlab_v4.Client/Models/TaskItemModel.cs (or a shared project like Onlab.Transfer)
// Make sure the namespace matches your project structure.
namespace Onlab.Transfer
{
    public class TaskItemModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Unique ID for the task
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // e.g., "New", "In Progress", "Review", "Done"
        public DateTime? DueDate { get; set; }
        public List<string> AssignedTo { get; set; } = new List<string>(); // Placeholder for user IDs or names
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Order { get; set; } // For maintaining order within a column

        // You can add more properties like Priority, Tags, etc.
    }
}
