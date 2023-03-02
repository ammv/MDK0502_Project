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

namespace Momatov.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
            string base64String = DBEntities.GetContext().Staff.First(x => x.UserID == App.CurrentUser.ID).Photo;
            if(base64String != null)
            {
                 StaffPhoto.Source = ImageClass.ConvertBase64ToBitmapImage(base64String);
            }
            
        }

        private void ChangeStaffImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                string filePath = openFileDialog.FileName;

                string base64String = ImageClass.ConvertImageToBase64String(filePath);

                DBEntities.GetContext().Staff.First(x => x.UserID == App.CurrentUser.ID).Photo = base64String;

                DBEntities.GetContext().SaveChanges();

                StaffPhoto.Source = ImageClass.ConvertBase64ToBitmapImage(base64String);
            }
        }
    }
}
