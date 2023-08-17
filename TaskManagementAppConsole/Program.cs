// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementAppConsole;
using TaskManagementAppDataAccess;


internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var serviceProvider = new ServiceCollection().AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    "your_connection_string")
                ).AddTransient<TaskRepository>().BuildServiceProvider();

            TaskRepository dbContext = serviceProvider.GetRequiredService<TaskRepository>();
            ConsoleApp consoleApp = new(dbContext);
            consoleApp.Run();            
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }        
    }
}