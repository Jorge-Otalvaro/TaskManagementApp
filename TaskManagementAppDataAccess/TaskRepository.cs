using Microsoft.EntityFrameworkCore;

namespace TaskManagementAppDataAccess
{
    public class TaskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskRepository(ApplicationDbContext dbContextOptions)
        {
            _dbContext = dbContextOptions;
        }

        // Obtener todas las tareas.
        private async Task<List<Task>> GetAllTasks()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        // Agregar una nueva tarea a la lista.
        public async void CreateTask(TaskItem taskItem)
        {
            Task task = new()
            {
                Name = taskItem.Name,
                IsCompleted = false,
                Created_at = taskItem.Created_at
            };
            
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        // Marcar una tarea como completada.
        public async void MarkTaskAsCompleted(int taskId)
        {
            Task task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);            

            if (task != null)
            {
                task.IsCompleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        // Eliminar una tarea de la lista.
        public async void DeleteTask(int taskId)
        {
            Task taskToRemove = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (taskToRemove != null)
            {
                _dbContext.Tasks.Remove(taskToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Obtener una tarea por su Id.
        public async Task<TaskItem> GetTaskById(int taskId)
        {
            Task task = await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == taskId);

            if (task != null)
            {
                return new TaskItem
                {
                    Id = task.Id,
                    Name = task.Name,
                    IsCompleted = task.IsCompleted,
                    Created_at = task.Created_at
                };
            }

            return null;
        }

        // Obtener todas las tareas pendientes.
        public async Task<List<TaskItem>> GetPendingTasks()
        {
            List<Task> tasks = await _dbContext.Tasks.Where(task => !task.IsCompleted).ToListAsync();

            return tasks.Select(task => new TaskItem
            {
                Id = task.Id,
                Name = task.Name,
                Created_at = task.Created_at
            }).ToList();
        }
        
        // Obtener el total de tareas.
        public async Task<int> GetTotalTasks()
        {
            int totalTasks = await _dbContext.Tasks.CountAsync();            
            return totalTasks;
        }

        // Obtener el total de tareas completadas.
        public async Task<int> GetCompletedTasks()
        {
            List<Task> tasks = await GetAllTasks();
            int completedTasksCount = tasks.Count(task => task.IsCompleted);
            return completedTasksCount;
        }
    }
}
