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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LIDY;Initial Catalog=TopDate;Integrated Security=True;");

        public Update()
        {
            InitializeComponent();
            try
            {
                sqlCon.Open();

                string query = " select * from Profiles where customer_ID = " + ID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    FirstName.Text = reader["first_name"].ToString();
                    LastName.Text = reader["last_name"].ToString();
                    Email.Text = reader["Email"].ToString();
                    Instagram.Text = reader["Instagram"].ToString();
                    Password.Text = reader["Password"].ToString();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Authentication() == true)
            {
                try
                {
                    sqlCon.Open();

                    string querry = "UPDATE Profiles SET first_name = '" + this.FirstName.Text + "' , last_name = '" + this.LastName.Text + "' , Email + '" + this.Email.Text + "' , Instagram = '" + this.Instagram.Text + "' , Password = '" + this.Password.Password + "'";
                    SqlCommand cmd = new SqlCommand(querry, sqlCon);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully updated information up!");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }

                Matches m = new Matches();
                m.Show();
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
            else if (Password.Password != RepeatPassword.Password)
            {
                MessageBox.Show("Your passwords do not match!");
                sqlCon.Close();
                return false;
            }
            sqlCon.Close();
            return true;
        }
    }
}
