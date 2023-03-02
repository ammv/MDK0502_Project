using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Momatov.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListStaffPage.xaml
    /// </summary>
    public partial class ListStaffPage : Page
    {
        public ListStaffPage()
        {
            InitializeComponent();
            ListStaffListBox.ItemsSource = DBEntities.GetContext()
                .Staff.ToList().OrderBy(s => s.LastName);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    public class StringToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageClass.ConvertBase64StringToBitmapImage((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
