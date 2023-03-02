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

        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }

        //public List<User> GenerateRandomUser(int startID)
        //{
        //    List<User> users = new List<User>();
        //    Random rnd = new Random();
        //    for (int i = 0; i < 50; i++)
        //    {
        //        User user = new User
        //        {
        //            ID = startID+i,
        //            Login = GenerateName(rnd.Next(5, 12)),
        //            Password = Guid.NewGuid().ToString("d").Substring(1, 8),
        //            RoleID = rnd.Next(1, 2)
        //        };
        //        User.Add(user);
        //        //users.Add(new string[]{ GenerateName (rnd.Next(5, 12)), Guid.NewGuid().ToString("d").Substring(1, 8)});
        //    }
        //    return users;
        //}

        private void Application_Startup(object sender, StartupEventArgs e)
        {
//            SqlConnection conn = new SqlConnection(App.ConnectionString);
//            conn.Open();
//            SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.Users')", conn);
//            int startID = (int)cmd.ExecuteScalar();

//            var context = DBEntities.GetContext();
//            foreach (var user in GenerateRandomUsers(startID))
//{
//                context.Users.Add(user);
//                DBEntities.GetContext().SaveChanges();
//            }
            //DBEntities.GetContext().SaveChanges();

        }
    }
}
