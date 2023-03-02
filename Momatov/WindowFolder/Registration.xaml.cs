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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {

        public Registration()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateLogin(string login)
        {
            if(string.IsNullOrEmpty(login))
            {
                MBClass.Error("Логин не может быть пустым!");
                LoginTextBox.Focus();
                return false;
            }
            else if(login.Length > 30)
            {
                MBClass.Error("Логин не может быть содержать больше 30 символов!");
                LoginTextBox.Focus();
                return false;
            }
            else if(login.Length < 5)
            {
                MBClass.Error("Логин должен содержать больше 5 символов!");
                LoginTextBox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidatePassword(string password, string repeatPassword)
        {
            if (string.IsNullOrEmpty(password))
            {
                MBClass.Error("Пароль не может быть пустым!");
                PasswordPasswordBox.Focus();
                return false;
            }
            else if (password.Length > 30)
            {
                MBClass.Error("Пароль не может быть содержать больше 30 символов!");
                PasswordPasswordBox.Focus();
                return false;
            }
            else if (password.Length < 7)
            {
                MBClass.Error("Пароль должен содержать больше 7 символов!");
                PasswordPasswordBox.Focus();
                return false;
            }
            else
            {
                string lowerCase = "";
                for (int i = 97; i <= 122; i++)
                {
                    lowerCase += (char)i;
                }
                string upperCase = lowerCase.ToUpper();
                string symbols = @"!@#$%^&*()_+|{}?><~\/-=";

                if (!password.Any(x => lowerCase.Contains(x)))
                {
                    MBClass.Error("Пароль должен содержать буквы в нижнем регистре!");
                    PasswordPasswordBox.Focus();
                    return false;
                }
                else if (!password.Any(x => upperCase.Contains(x)))
                {
                    MBClass.Error("Пароль должен содержать буквы в верхнем регистре");
                    PasswordPasswordBox.Focus();
                    return false;
                }
                else if (!password.Any(x => symbols.Contains(x)))
                {
                    MBClass.Error("Пароль должен содержать специальные символы!");
                    PasswordPasswordBox.Focus();
                    return false;
                }
                else
                {
                    if(password != repeatPassword)
                    {
                        MBClass.Error("Пароли не совпадают!");
                        RepeatPasswordPasswordBox.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateLogin(LoginTextBox.Text) && ValidatePassword(
                PasswordPasswordBox.Password, RepeatPasswordPasswordBox.Password))
            {
                try
                {
                    if(DBEntities.GetContext().User.First(x => x.Login == LoginTextBox.Text) != null)
                    {
                        MBClass.Error("Пользователь с таким логином уже существует!");
                        return;
                    }

                    DBEntities.GetContext().User.Add(new User()
                    {
                        Login = LoginTextBox.Text,
                        Password = PasswordPasswordBox.Password,
                        RoleID = 2
                    });

                    DBEntities.GetContext().SaveChanges();
                    MBClass.Information("Вы зарегистрировались!");
                    Close();
                }
                catch(Exception error)
                {
                    MBClass.Error(error);
                }
            }
        }
    }
}
