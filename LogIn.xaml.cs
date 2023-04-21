using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            SignUp su = new SignUp();
            su.Show();
            this.Close();
        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC07\LOCALHOST;Initial Catalog=Dropshipping;Integrated Security=True; Trust Server Certificate=true");

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM Profiles Where Email=@Email and Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Email", Email.Text);
                sqlCmd.Parameters.AddWithValue("@Password", Password.Password);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    int profile_ID = Convert.ToInt32(new SqlCommand("SELECT customer_ID FROM Profiles Where Email='" + Email.Text + "'", sqlCon).ExecuteScalar());
                    if (Convert.ToInt32(new SqlCommand("SELECT COUNT(1) FROM Traits Where ID='" + profile_ID + "'", sqlCon).ExecuteScalar()) != 1)
                    {
                        Traits hp = new Traits(profile_ID);
                        hp.Show();
                        this.Close();
                    }
                    else if (Convert.ToInt32(new SqlCommand("SELECT COUNT(1) FROM Expectations Where ID='" + profile_ID + "'", sqlCon).ExecuteScalar()) != 1)
                    {
                        Expectations hp = new Expectations(profile_ID);
                        hp.Show();
                        this.Close();
                    }
                    else
                    {
                        HomePage hp = new HomePage(profile_ID);
                        hp.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Username or password are not correct!");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
