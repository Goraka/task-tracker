public class Tasks
{
    private Dictionary<int, string> TaskList = new Dictionary<int, string>();
    private Dictionary<int, string> NewTasks = new Dictionary<int, string>();
    private readonly string fileName = "tasks_data";
    private readonly string folderName = "TaskData";
    private readonly string baseDirectory = AppContext.BaseDirectory;
    private string taskdataFolder = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName ?? string.Empty;
    private string _currentDirectory = Directory.GetCurrentDirectory();

    private readonly string _fullPath = @"D:\Projects\roadmap.sh\tasktrackercli\tasktracker\TaskData\tasks_data"; 

    public Tasks()
    {
        LoadTasksFromFile();
    }

    public Tasks(Dictionary<int, string> tasks)
    {
        TaskList = tasks;
    }

    public void AddTask(string taskName)
    {
        if(string.IsNullOrWhiteSpace(taskName))
        {
            Console.WriteLine("Task name cannot be empty.");
            return;
        }

        // LoadTasksFromFile();

        if(TaskList.Count > 0)
        {
            int newKey = TaskList.Keys.Max() +1;

            if(NewTasks.Count > 0)
            {
                newKey = NewTasks.Keys.Max() +1;
            }

            NewTasks.Add(newKey, taskName);
        }
        else
        {
            NewTasks.Add(1, taskName);
        }
        
        SaveTasksToFile(NewTasks);
        Console.WriteLine($"Task '{taskName}' added successfully!");
    }

    public void RemoveTask(string taskName)
    {
        // Implementation for removing a task
    }

    public Dictionary<int, string> ListTasks()
    {
        // LoadTasksFromFile();

        if(TaskList.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return TaskList;
        }

        Console.WriteLine("Current Tasks:");
        Console.WriteLine("==============");
        Console.WriteLine("Select a task to edit, delete or mark as complete:");

        foreach(var task in TaskList)
        {
            Console.WriteLine($"{task.Key}. {task.Value}");
        }

        Console.WriteLine("==============");
        Console.Write("Enter the task number: ");
        int taskNo = Convert.ToInt32(Console.ReadLine());

        if(TaskList.ContainsKey(taskNo))
        {
            Console.WriteLine($"You selected task: {TaskList[taskNo]}");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Edit Task");
            Console.WriteLine("2. Delete Task");
            Console.WriteLine("3. Mark as Complete");
            Console.Write("Select an option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.Write("Enter new task name: ");
                    string newTaskName = Console.ReadLine() ?? string.Empty;
                    EditTask(taskNo, newTaskName);
                    break;
                case 2:
                    RemoveTask(TaskList[taskNo]);
                    break;
                case 3:
                    CompleteTask(TaskList[taskNo]);
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid task number selected.");
        }   

        return TaskList;
    }

    public void CompleteTask(string taskName)
    {
        // Implementation for marking a task as complete
    }

    public void EditTask(int taskNo, string newTaskName)
    {
        string folderPath = Path.Combine(_currentDirectory, folderName);
        string tasksFilePath = Path.Combine(folderPath, fileName);

        // Implementation for editing a task
        if(TaskList.Count == 0)
        {
            Console.WriteLine("No tasks available to edit.");
            return;
        }

        if (TaskList.ContainsKey(taskNo))
        {
            TaskList[taskNo] = newTaskName;

            var lines = File.ReadAllLines(tasksFilePath).ToList();

            for(int i = 0; i<lines.Count; i++)
            {
                if(lines[i].StartsWith($"{taskNo}:"))
                {
                    lines[i] = $"{taskNo}:{newTaskName}";
                    break;
                }
            }

            File.WriteAllLines(tasksFilePath, lines);

            Console.WriteLine($"Task number {taskNo} updated successfully to '{newTaskName}'!");
        }
        else
        {
            Console.WriteLine("Task number not found.");
            return;
        }
    }

    private void SaveTasksToFile(Dictionary<int, string> tasks)
    {
        string folderPath = Path.Combine(_currentDirectory, folderName);
        string tasksFilePath = Path.Combine(folderPath, fileName);
        
        if(tasks.Count > 0)
        {
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach(var task in tasks)
            {
                TaskList.Add(task.Key, task.Value);
            }

            using (StreamWriter writer = new StreamWriter(tasksFilePath))
            {
                foreach (var task in TaskList)
                {
                    writer.WriteLine($"{task.Key}:{task.Value}");
                }

                writer.Close();
            }
        }
    }

    private Dictionary<int, string> LoadTasksFromFile()
    {
        string folderPath = Path.Combine(_currentDirectory, folderName);
        string tasksFilePath = Path.Combine(folderPath, fileName);

        Console.WriteLine(tasksFilePath);

        using(StreamReader reader = new StreamReader(tasksFilePath))
        {
            string? line;
            while((line=reader.ReadLine()) != null)
            {
                if(string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(':', 2);
                if(parts.Length == 2)
                {
                    if(int.TryParse(parts[0].Trim(), out int key))
                    {
                        string value = parts[1].Trim();
                        TaskList[key] = value;
                    }
                    
                }
            }

            reader.Close();

            return TaskList;
        }
    }

    public void ClearAllTasks()
    {
    }
}