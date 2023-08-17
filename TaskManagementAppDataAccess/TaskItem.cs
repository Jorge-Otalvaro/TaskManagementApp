namespace TaskManagementAppDataAccess
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Created_at { get; set; }
    }
}
