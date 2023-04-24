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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FINAL_PROJECT
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC07\LOCALHOST; Initial Catalog = TopDate; Integrated Security = True;");

        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Authentication() == true)
            {
                try
                {
                    sqlCon.Open();

                    string querry = "INSERT INTO Profiles(first_name, last_name, Email, Instagram, Password) values ('" + this.FirstName.Text + "' , '" + this.LastName.Text + "' , '" + this.Email.Text + "' , '" + this.Instagram.Text + "' , '" + this.Password.Password + "' )";
                    SqlCommand cmd = new SqlCommand(querry, sqlCon);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully signed up!");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }

                LogIn li = new LogIn();
                li.Show();
                this.Close();
            }
        }

        public bool Authentication()
        {
            try
            {
                sqlCon.Open();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (Email.Text.Equals("") || Email.Text.Contains("@") == false || Convert.ToInt32(new SqlCommand("SELECT Count(1) FROM Profiles Where Email='" + Email.Text + "'", sqlCon).ExecuteScalar()) == 1)
            {
                MessageBox.Show("Invalid email");
                sqlCon.Close();
                return false;
            }
            else if (FirstName.Text.Equals(""))
            {
                MessageBox.Show("You cannot leave First Name field empty!");
                sqlCon.Close();
                return false;
            }
            else if (LastName.Text.Equals(""))
            {
                MessageBox.Show("You cannot leave Last Name field empty!");
                sqlCon.Close();
                return false;
            }
            else if (Instagram.Text.Equals(""))
            {
                MessageBox.Show("You cannot leave Instagram field empty!");
                sqlCon.Close();
                return false;
            }
            else if (Password.Password.Length < 10)
            {
                MessageBox.Show("Your password should be at least 10 symbols long. Try again");
                sqlCon.Close();
                return false;
            }
            sqlCon.Close();
            return true;
        }
        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            LogIn li = new LogIn();
            li.Show();
            this.Close();
        }
    }
}
