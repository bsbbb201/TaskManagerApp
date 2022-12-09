using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManagerLib;

namespace WpfApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string commandName = "";
        private static List<string> avaliableCommands = new() { "start", "delref", "addref", "kill" };
        private static TaskManager taskManager = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProcButton_Click(object sender, RoutedEventArgs e)
        {
            taskManager.ExecuteProcess(ProcNameTB.Text, ProcArgsTB.Text);
        }

        private void KillProcButton_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;

            if (int.TryParse(ProcNameTB.Text, out id))
            {
                taskManager.KillProcess(id);
                return;
            }

            taskManager.KillProcess(ProcNameTB.Text);
            return;
        }

        private void AddRefButton_Click(object sender, RoutedEventArgs e)
        {
            if (RefPath.Text.Trim() == "")
            {
                MessageBox.Show("Nothing writed in path field");
                return;
            }

            taskManager.AddNewReference(RefName.Text, RefPath.Text);
        }

        private void DelRefButton_Click(object sender, RoutedEventArgs e)
        {
            taskManager.RemoveReference(RefName.Text);
        }
    }
}
