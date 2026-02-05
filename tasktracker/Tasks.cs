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

    public string AddTask(string taskName)
    {
        if(string.IsNullOrWhiteSpace(taskName))
        {
            return "Task name cannot be empty.";
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
        return $"Task '{taskName}' added successfully!";
    }

    public void RemoveTask(int taskNo)
    {
        // Implementation for removing a task
        string folderPath = Path.Combine(_currentDirectory, folderName);
        string tasksFilePath = Path.Combine(folderPath, fileName);

        // Implementation for editing a task
        if(TaskList.Count == 0)
        {
            Console.WriteLine("No tasks available to edit.");
            return;
        }

        var task = TaskList[taskNo];

        if (TaskList.ContainsKey(taskNo))
        {
            TaskList.Remove(taskNo);

            var lines = File.ReadAllLines(tasksFilePath).ToList();

            for(int i = 0; i<lines.Count; i++)
            {
                if(lines[i].StartsWith($"{taskNo}:"))
                {
                    lines.RemoveAt(i);
                    break;
                }
            }

            File.WriteAllLines(tasksFilePath, lines);

            Console.Clear();
            Console.WriteLine($"Task {task} removed successfully!");
            Console.WriteLine("=================================================");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Task number not found.");
            return;
        }
        
    }

    public string ListTasks()
    {
        // LoadTasksFromFile();

        TASKS_LISTS:
        if(TaskList.Count == 0)
        {
            return "No tasks available.";
        }

        
        Console.WriteLine("Current Tasks:");
        Console.WriteLine("==============");
        Console.WriteLine("Select a task to edit, delete or mark as complete:");

        foreach(var task in TaskList)
        {
            Console.WriteLine($"{task.Key}. {task.Value}");
        }

        Console.WriteLine("==============");
        Console.Write("Enter the task number or press 0 to return: ");
        int taskNo = Convert.ToInt32(Console.ReadLine());

        if(taskNo == 0)
        {
            return string.Empty;
        }

        if(TaskList.ContainsKey(taskNo))
        {
            Console.Clear();
            Console.WriteLine($"You selected task: {TaskList[taskNo]}");
            Console.WriteLine("Options:");
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Edit Task");
            Console.WriteLine("2. Delete Task");
            Console.WriteLine("3. Mark as Complete");
            Console.Write("Select an option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 0:
                    goto TASKS_LISTS;
                case 1:
                    Console.Write("Enter new task name: ");
                    string newTaskName = Console.ReadLine() ?? string.Empty;
                    EditTask(taskNo, newTaskName);
                    break;
                case 2:
                    RemoveTask(taskNo);
                    break;
                case 3:
                    CompleteTask(TaskList[taskNo]);
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    goto RETURN_TASKS;
            }

            goto TASKS_LISTS;

            RETURN_TASKS:
            return string.Empty;
        }
        else
        {
            Console.WriteLine("Invalid task number selected.");
        }   

        return string.Empty;
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

            return TaskList;
        }
    }

    public void ClearAllTasks()
    {
    }
}