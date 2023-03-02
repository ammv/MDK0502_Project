using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Momatov.ClassFolder
{
    class MBClass
    {
        public static void Error(Exception error)
        {
            Error(error.Message);
        }
        public static void Error(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Information(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static bool Question(string message)
        {
            return MessageBoxResult.Yes == MessageBox.Show(
                message, "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public static void Exit()
        {
            if(Question("Вы действительно хотите выйти?"))
            {
                App.Current.Shutdown();
            }
        }
    }
}
