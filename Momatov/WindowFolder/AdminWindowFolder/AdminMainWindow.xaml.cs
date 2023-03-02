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
using Momatov.ClassFolder;
using Momatov.PageFolder.AdminPageFolder;

namespace Momatov.WindowFolder.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new ListUserPage());
        }

        private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.Exit();
        }

        private void ListUserButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListUserPage());
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddUserPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
