using TaskManagementAppDataAccess;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementAppConsole
{
    public class ConsoleApp
    {
        private readonly TaskRepository taskRepository;

        public ConsoleApp(TaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }        

        public async void Run()
        {
            bool exit = false;

            while (!exit)
            {
                DisplayMenu();
                int choice = ReadUserChoice();

                switch (choice)
                {
                    case 1:
                        AddTask();
                        break;
                    case 2:
                        MarkTaskAsCompleted();
                        break;
                    case 3:
                        ListPendingTasks();
                        break;
                    case 4:
                        ShowStatistics();
                        break;
                    case 5:
                        Console.Clear();
                        Run();
                        break;
                    case 6:
                        Console.WriteLine("=========================================");
                        Console.WriteLine("Saliendo de la aplicación...");
                        Console.WriteLine("=========================================\n");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, elige nuevamente.");
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            // Mostrar las opciones del menú al usuario.
            Console.WriteLine("Menú:");
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Marcar tarea como completada");
            Console.WriteLine("3. Listar tareas pendientes");
            Console.WriteLine("4. Mostrar estadísticas");
            Console.WriteLine("5. Limpiar pantalla");
            Console.WriteLine("6. Salir");
            Console.WriteLine("=========================================\n");
        }

        private async void AddTask()
        {
            Console.Write("Nombre de la tarea: ");
            string taskName = Console.ReadLine();

            TaskItem task = new()
            {
                Name = taskName,
                IsCompleted = false,
                Created_at = DateTime.Now,
            };

            taskRepository.CreateTask(task);

            Console.WriteLine("=========================================");
            Console.WriteLine("Tarea agregada exitosamente.");
            Console.WriteLine("=========================================\n");
        }

        private async void MarkTaskAsCompleted()
        {
            Console.Write("Ingrese el ID de la tarea que desea marcar como completada: ");

            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task<TaskItem> taskToMark = taskRepository.GetTaskById(taskId);

                if (taskToMark.Result != null)
                {
                    if (!taskToMark.IsCompleted)
                    {
                        taskRepository.MarkTaskAsCompleted(taskId); 
                        Console.WriteLine("=========================================");
                        Console.WriteLine("Tarea marcada como completada.");
                        Console.WriteLine("=========================================\n");
                    }
                    else
                    {
                        Console.WriteLine("=========================================");
                        Console.WriteLine("Esta tarea ya está completada.");
                        Console.WriteLine("=========================================\n");
                    }
                }
                else
                {
                    Console.WriteLine("=========================================");
                    Console.WriteLine("No se encontró ninguna tarea con el ID proporcionado.");
                    Console.WriteLine("=========================================\n");
                }
            }
            else
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("Entrada inválida. Por favor, ingresa un número válido.");
                Console.WriteLine("=========================================\n");
            }                        
        }

        private async void ListPendingTasks()
        {
            Console.WriteLine("Iniciando búsqueda de tareas pendientes...");
            Console.WriteLine("Espera unos segundos, mientras se cargan los datos...");            

            List<TaskItem> pendingTasks = await taskRepository.GetPendingTasks();

            if (pendingTasks.Count > 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Tareas pendientes:");
                Console.WriteLine("=========================================");

                foreach (TaskItem task in pendingTasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Nombre: {task.Name}, Creada el: {task.Created_at}");
                }

                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("No hay tareas pendientes.");
                Console.WriteLine("=========================================\n");
            }
            
            await Task.Delay(TimeSpan.FromSeconds(1));
            Run();
        }

        private async void ShowStatistics()
        {
            Console.WriteLine("Iniciando cálculo de estadísticas...");
            Console.WriteLine("\n");
            int totalTasks = await taskRepository.GetTotalTasks();
            int completedTasks = await taskRepository.GetCompletedTasks();
            int pendingTasks = totalTasks - completedTasks;            

            Console.WriteLine("\n");
            Console.WriteLine("=========================================");
            Console.WriteLine("Estadísticas:");
            Console.WriteLine($"Total de tareas: {totalTasks}");
            Console.WriteLine($"Tareas completadas: {completedTasks}");
            Console.WriteLine($"Tareas pendientes: {pendingTasks}");
            Console.WriteLine("=========================================\n");
            
            await Task.Delay(1000);
            Run();
        }

        private int ReadUserChoice()
        {
            int choice = 0;
            bool validChoice = false;

            while (!validChoice)
            {
                Console.Write("Elige una opción: ");                

                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("=========================================");
                    Console.WriteLine("Entrada inválida. Por favor, ingresa un número válido.");
                    Console.WriteLine("=========================================\n");
                }
            }

            return choice;
        }
    }
}
