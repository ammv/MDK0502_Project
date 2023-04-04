using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
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
    public partial class ChoiceRegionForWorker : Window
    {
        Action<int> setRegionID;
        public ChoiceRegionForWorker(Action<int> setRegionID)
        {
            
            InitializeComponent();

            this.setRegionID = setRegionID;
            WorkshopComboBox.ItemsSource = DBEntities.GetContext().Workshop
                .OrderBy(x => x.Name)
                .ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(WorkshopComboBox.SelectedIndex != -1)
            {
                setRegionID((RegionComboBox.SelectedItem as Region).ID);
                Close();
            }
            
        }

        private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WorkshopComboBox.SelectedIndex != -1)
            {
                if(RegionComboBox.SelectedIndex != -1)
                {
                    if (MBClass.Question("Вы выбрали регион, вы уверены, что хотите закрыть окно?"))
                    {
                        Close();
                    }
                }
                else
                {
                    if (MBClass.Question("Вы не выбрали регион, вы уверены, что хотите закрыть окно?"))
                    {
                        Close();
                    }
                }
                
            }
            else
            {
                if (MBClass.Question("Вы не выбрали цех, вы уверены, что хотите закрыть окно?"))
                {
                    Close();
                }
            }
        }

        private void WorkshopComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int workshopID = (WorkshopComboBox.SelectedItem as Workshop).ID;
            RegionComboBox.ItemsSource = DBEntities.GetContext().Region
                .Where(r => r.WorkshopID == workshopID)
                .ToList();
        }
    }
}
