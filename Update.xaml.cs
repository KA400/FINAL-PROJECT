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
        int ID;
        public Update(int profile_ID)
        {
            InitializeComponent();
            ID = profile_ID;
            try
            {
                sqlCon.Open();

                string query = " select * from Profiles where ID = " + ID;

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

                    string querry = "UPDATE Profiles SET first_name = '" + this.FirstName.Text + "' , last_name = '" + this.LastName.Text + "' , Email = '" + this.Email.Text + "' , Instagram = '" + this.Instagram.Text + "' , Password = '" + this.Password.Text + "'";
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

                Matches m = new Matches(ID);
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

            if (Email.Text.Equals("") || Email.Text.Contains("@") == false)
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
            else if (Password.Text.Length < 10)
            {
                MessageBox.Show("Your password should be at least 10 symbols long. Try again");
                sqlCon.Close();
                return false;
            }
            sqlCon.Close();
            return true;
        }
    }
}
