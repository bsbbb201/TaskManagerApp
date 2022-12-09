using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib
{
    public class TaskManager
    {

        public TaskManager()
        {
            controller = new ReferenceListController();
        }

        Dictionary<string, string> ProcessesList { get => controller._processesList; }

        /// <summary>
        /// Список процессов
        /// </summary>
        /// <returns></returns>
        public List<string> GetProcessesList()
        {
            List<string> list = new List<string>();

            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                list.Add($"{process.Id} \t{process.ProcessName}");
            }
            return list;
        }


        ReferenceListController controller;

        /// <summary>
        /// Запуска процесса
        /// </summary>
        public void ExecuteProcess(string processName, string arg)
        {
            string path = "";

            if (ProcessesList.ContainsKey(processName))
                path = ProcessesList[processName];

            if (path == "")
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {processName}"));
            }
            else
            {
                if (File.Exists(path))
                {
                    if (arg != "")
                    {
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {path} {arg}"));
                    }
                    else
                    {
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {path}"));
                    }
                }
                else
                {
                    throw new Exception("Не удалось запустить процесс! Указанного файла не существует");
                }
            }            
        }

        /// <summary>
        /// Метод удаления процесса по ID
        /// </summary>
        /// <param name="id"></param>
        public void KillProcess(int id)
        {
            Process.GetProcessById(id).Kill();
        }

        /// <summary>
        /// Метод удаления процесса по Имени
        /// </summary>
        /// <param name="name"></param>
        public void KillProcess(string name)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(name);

                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Невозможно убить процесс! Ошибка: {e.Message}");
            }
        }

        public void AddNewReference(string name, string path) 
        {
            controller.AddNewBrowser(name, path);
        }

        public void RemoveReference(string name) 
        {
            controller.DeleteProcesses(name);
        }
    }
}
