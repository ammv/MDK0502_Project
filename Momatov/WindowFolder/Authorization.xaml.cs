using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Momatov.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MBClass.Error("Вы не ввели логин!");
                LoginTextBox.Focus();
            }
            else if(string.IsNullOrEmpty(PasswordPasswordBox.Password))
            {
                MBClass.Error("Вы не ввели логин!");
                LoginTextBox.Focus();
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext().User.FirstOrDefault(u => u.Login == LoginTextBox.Text);
                    if (user == null)
                    {
                        MBClass.Error("Пользователь с таким логином не существует!");
                        LoginTextBox.Focus();
                        return;
                    }
                    if(user.Password != PasswordPasswordBox.Password)
                    {
                        MBClass.Error("Введен неверный пароль!");
                        PasswordPasswordBox.Focus();
                        return;
                    }

                    App.CurrentUser = user;

                    switch (user.RoleID)
                    {
                        case 1:
                            MBClass.Information("Администратор");
                            new AdminWindowFolder.AdminMainWindow().ShowDialog();
                            break;
                        case 2:
                            MBClass.Information("Менеджер");
                            new ManagerWindowFolder.ManagerMainWindow().ShowDialog();
                            break;
                    }

                }
                catch (Exception error)
                {
                    MBClass.Error(error);
                }
            }
        }

        private void GoToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            new Registration().ShowDialog();
        }
    }
}
