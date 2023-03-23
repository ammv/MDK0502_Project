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

namespace Momatov.WindowFolder.ManagerWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для ChoiceWorkshopForWorker.xaml
    /// </summary>
    public partial class ChoiceWorkshopForWorker : Window
    {
        Action<int> setWorkshopID;
        public ChoiceWorkshopForWorker(Action<int> setWorkshopID)
        {
            this.setWorkshopID = setWorkshopID;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(WorkshopComboBox.SelectedIndex != -1)
            {
                setWorkshopID(WorkshopComboBox.SelectedIndex+1);
                Close();
            }
            
        }

        private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WorkshopComboBox.SelectedIndex != -1)
            {
                if(MBClass.Question("Вы выбрали цех, вы уверены, что хотите закрыть окно?"))
                {

                }
            }
        }
    }
}
