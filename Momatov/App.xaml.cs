using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.RightsManagement;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Momatov
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string ConnectionString =
            @"Initial catalog=user31;" +
            @"Data source=10.128.14.64\SQLEXPRESS" +
            @"User ID = user31; Password=wsruser31;";


        private static User currentUser;

        public static User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        public static string GenerateFirstName()
        {
            Random r = new Random();
            string[] names = {"Олег", "Артем", "Глеб", "Мурад", "Евгений", "Андрей"};

            return names[r.Next(names.Length)];
        }

        public static string GenerateMiddleName()
        {
            Random r = new Random();
            string[] names = { "Моматов", "Отводов", "Иванов", "Петров", "Лаврушев", "Медведев" };

            return names[r.Next(names.Length)];
        }

        public static string GenerateLastName()
        {
            Random r = new Random();
            string[] names = { "Дмитриевич", "Петрович", "Арестович", "Георгиевич", "Андреевич", "Михаилович" };

            return names[r.Next(0, names.Length)];
        }

        public static string GeneratePhone()
        {
            Random r = new Random();
            return r.Next(89000, 89999).ToString() + r.Next(100000, 999999).ToString();
        }

        public static string GeneratePhoto()
        {
            Random r = new Random();
            string[] names = { 
                @"C:\Users\Моматов\Downloads\1.jpg",
                @"C:\Users\Моматов\Downloads\2.jpg",
                @"C:\Users\Моматов\Downloads\3.jpg",
                @"C:\Users\Моматов\Downloads\4.jpg",
                @"C:\Users\Моматов\Downloads\5.jpg"};

            return ImageClass.ConvertImageToBase64String(names[r.Next(0, names.Length)]);
        }

        public List<User> GenerateRandomUser(int startID)
        {
            List<User> users = new List<User>();
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    User user = new User
                    {
                        Login = Guid.NewGuid().ToString("d").Substring(1, 8),
                        Password = Guid.NewGuid().ToString("d").Substring(1, 8),
                        RoleID = rnd.Next(1, 2)
                    };
                    users.Add(user);
                }
                catch
                {

                }
                
            }
            return users;
        }

        public List<Staff> GenerateRandomStaff()
        {
            List<Staff> staffs = new List<Staff>();
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    User user = new User
                    {
                        Login = Guid.NewGuid().ToString("d").Substring(1, 8),
                        Password = Guid.NewGuid().ToString("d").Substring(1, 8),
                        RoleID = rnd.Next(1, 3)
                    };

                    DBEntities.GetContext().User.Add(user);
                    DBEntities.GetContext().SaveChanges();

                    Staff staff = new Staff
                    {
                        FirstName = GenerateFirstName(),
                        MiddleName = GenerateMiddleName(),
                        LastName = GenerateLastName(),
                        UserID = user.ID,
                        Phone = GeneratePhone(),
                        Photo = GeneratePhoto()
                    };
                    staffs.Add(staff);
                }
                catch
                {

                }

            }
            return staffs;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //            SqlConnection conn = new SqlConnection(App.ConnectionString);
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.Users')", conn);
            //            int startID = (int)cmd.ExecuteScalar();

            //var context = DBEntities.GetContext();
            //foreach (var staff in GenerateRandomStaff())
            //{
            //    context.Staff.Add(staff);
            //    DBEntities.GetContext().SaveChanges();
            //}


        }
    }
}
