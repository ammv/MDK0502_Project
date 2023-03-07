using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
using Momatov.PageFolder.AdminPageFolder;
using Momatov.PageFolder.ManagerPageFolder;

namespace Momatov.WindowFolder.ManagerWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.Exit();
        }

        private void ListStaffButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListStaffPage());
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddStaffPage());
        }
    }
}
