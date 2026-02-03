
ASCII.PrintWelcome();
ASCII.PrintName();
ASCII.PrintArt();

Console.WriteLine();
Console.WriteLine("This is a simple task tracking application.");
Console.WriteLine("You can add, remove, list, complete, and edit tasks.");
Console.WriteLine("Tasks are stored in a local file for persistence.");
Console.WriteLine("Enjoy managing your tasks!");
Console.WriteLine("========================================");
Console.WriteLine();

Console.WriteLine("Add a new task below:");
// Dictionary<int, string> tasksList = new Dictionary<int, string>();

Tasks taskManager = new Tasks();

MENU:
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. List Tasks");
    Console.WriteLine("0. Exit");

Console.Write("Select an option : ");
int option = Convert.ToInt32(Console.ReadLine());

switch (option)
{
    case 0: // Exit
        goto EXIT_APP;
    case 1: // Add Task
        Console.Clear();
        Console.Write("Enter task name: ");
        string taskName = Console.ReadLine() ?? string.Empty;
        taskManager.AddTask(taskName);
        break;
    case 2: // List Tasks
        Console.Clear();
        taskManager.ListTasks();
        break;
    default:
        Console.WriteLine("Invalid option selected.");
        break;
}

Console.Clear();

goto MENU;

EXIT_APP:
    Console.WriteLine();
    ASCII.PrintGoodbye();
    Console.WriteLine();

    Console.WriteLine("Press any key to exit...");
