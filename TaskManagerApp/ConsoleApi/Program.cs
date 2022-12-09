using TaskManagerLib;

internal class Program
{
    private static string commandName = "";
    private static List<string> avaliableCommands = new() { "start", "delref", "addref", "kill" };
    private static TaskManager taskManager = new();
    private static void Main()
    {
        string command = "";

        while (true)
        {
            Console.WriteLine("Введите команду: ");
            command = Console.ReadLine();

            string[] splittedCommand = command.Split(">");

            Execute(splittedCommand);
        }
    }

    private static void Execute(string[] commandStrings)
    {
        if (commandStrings.Length < 2)
        {
            Console.WriteLine("Unavaliable command format");
            return;
        }

        MyParse(commandStrings[0]);
        string firstArgs = commandStrings[1];
        string secondArgs = "";

        if (commandStrings.Length > 2)
            secondArgs = commandStrings[2];

        switch(commandName)
        {
            case "start":
                taskManager.ExecuteProcess(firstArgs, secondArgs);
                break;
            case "addref":
                if (secondArgs == "")
                    break;
                taskManager.AddNewReference(firstArgs, secondArgs);
                break;
            case "delref":
                taskManager.RemoveReference(firstArgs);
                break;
            case "kill":
                int id = -1;

                if (int.TryParse(firstArgs, out id))
                {
                    taskManager.KillProcess(id);
                    break;
                }

                taskManager.KillProcess(firstArgs);
                break;
            default:
                Console.WriteLine("Undefined command!");
                break;
        }
    }

    private static string[] GetArrayOfCommands(string command) => command.Split(">");

    private static void MyParse(string commandString)
    {
        // разделяете на команду и аргумент
        // start>notepad                        - вызов метода запуска, что запускаем
        // start>google>www.youtube.com         - вызов метода запуска, что запускаем, страничка
        // delref>Winword                       - вызов метода удалить ярлык, что удалить
        // addref>Winword>C:\..\winword.exe     - вызов метода добавить ярлык, что добавить, адрес exe
        // kill>25                              - удалить процесс по ID, ID = 25 (преобразуется в Int?)
        // kill>notepad                         - удалить процесс по name, (преобразуется в Int?)== False -> name = notepad

        if (avaliableCommands.Contains(commandString))
        {
            commandName = commandString;
            return;
        }

        commandName = "";
    }

    // Методы для каждой команды:
    // 1. Запустить процесс
    // 2. Удалить по ID
    // 3. Удалить по имени
    // 4. Добавить ярлык (имя процесса и путь к нему в файл)
    // 5. Удалить ярлык (имя процесса и путь к нему из файла)
}