using Momatov.ClassFolder;
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
using System.Windows.Shapes;

namespace Momatov.WindowFolder.WorkerWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для WorkerMainWindow.xaml
    /// </summary>
    public partial class WorkerMainWindow : Window
    {
        public WorkerMainWindow()
        {
            InitializeComponent();
        }

        private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.Exit();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
