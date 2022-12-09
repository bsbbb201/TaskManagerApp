using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib
{
    /// <summary>
    /// Класс для работы с файлом, где хранятся процессы и адреса их .exe
    /// Файл-эмулятор рабочего стола
    /// </summary>
    internal class ReferenceListController
    {
        public ReferenceListController()
        {
            _processesList = new Dictionary<string, string>();
            LoadProcessesList();
        }

        private string _path = "processesList.txt";        // путь к файлу со списком процессов

        public Dictionary<string, string> _processesList;

        public Dictionary<string, string> ProcesssList 
        { 
            get 
            { 
                Dictionary<string, string> result = new Dictionary<string, string>();
                result = _processesList;

                foreach(var el in _defaultProccessesList)
                {
                    result.TryAdd(el, ""); // добавляем в словарь дефолтные процессы, которые лежат в коде, а не в файле
                }
                return _processesList; 
            } 
        }

        /// <summary>
        /// Список стандартных процессов, когда не требуется ссылка на .exe
        /// </summary>
        private List<string> _defaultProccessesList = new List<string>()
        {
            "notepad",      // блокнот            
            "calc",         // калькулятор            
            "explorer",
            "svchost",
            "lsass",
            "sihost",
            "wininit",
            "dwm",
            "smss",
            "services"
            // Дописать еще процессы! штук 8-10 всего стандартных
            // загуглить имена стандартных процессов, которые запускаются без адреса .exe
        };

        /// <summary>
        /// Метод загрузки словаря из файла
        /// </summary>
        private void LoadProcessesList()
        {
            if (File.Exists(_path))
            {
                string[] readText = File.ReadAllLines(_path);
                foreach (string str in readText)
                {
                    string[] split = str.Split('>');
                    _processesList.TryAdd(split[0], split[1]);
                }
            }
            else
            {
                _processesList.Add("Google", "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");
            }
        }

        /// <summary>
        /// Метод добавляет новый бразуер в файл .txt
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        public void AddNewBrowser(string process, string address)
        {
            bool isAlreadyInFile = false;               // переменная для проверки, что такой процесс уже есть в файле

            foreach (var myProccess in _processesList)
            {
                if (myProccess.Key.ToLower() == process.ToLower())
                {
                    isAlreadyInFile = true;
                }
            }
            if (!isAlreadyInFile)
            {
                _processesList.Add(process, address);
                string textToWrite = ProcessesListFromDictionaryToString();
                File.WriteAllText(_path, textToWrite);
            }
        }

        /// <summary>
        /// Метод преобразует словарь в строку для записи в .txt
        /// </summary>        
        /// <returns></returns>
        private string ProcessesListFromDictionaryToString()
        {
            string textToWrite = "";
            foreach (var myProccess in _processesList)
            {
                textToWrite += myProccess.Key + ">" + myProccess.Value + "\n";
            }
            return textToWrite;
        }

        /// <summary>
        /// Метод удаляет процесс из файла .txt
        /// </summary>
        /// <param name="process"></param>       
        public void DeleteProcesses(string process)
        {
            foreach (var myProccess in _processesList)
            {
                if (myProccess.Key.ToLower() == process.ToLower())
                {
                    _processesList.Remove(myProccess.Key);
                }
            }
            string textToWrite = ProcessesListFromDictionaryToString();
            File.WriteAllText(_path, textToWrite);
        }

    }
}
