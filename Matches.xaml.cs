using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FINAL_PROJECT
{
    /// <summary>
    /// Interaction logic for Matches.xaml
    /// </summary>
    public partial class Matches : Window
    {
        int ID;
        int match = -1;
        string profile1;
        int random = -1;

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LIDY;Initial Catalog=TopDate;Integrated Security=True;");
        public Matches(int profile_ID)
        {
            InitializeComponent();
            ID = profile_ID;
            
            try
            {
                sqlCon.Open();
                
                DataTable dt = new DataTable();

                string query1 = "SELECT TOP (1) profile1 FROM Matches WHERE profile2 = '" + ID + "' and match = null";
                SqlCommand cmd1 = new SqlCommand(query1, sqlCon);
                profile1 = Convert.ToString(cmd1.ExecuteScalar());

                if(profile1 != null)
                {
                    string query3 = "SELECT * FROM Profiles WHERE ID = '" + profile1 + "'";
                    SqlCommand cmd3 = new SqlCommand(query3, sqlCon);
                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd3);
                    da.Fill(dt);

                    MyDataGrid.ItemsSource = dt.DefaultView;
                    match = 1;

                }
                else
                {
                    string query2 = "SELECT COUNT(ID) FROM Profiles WHERE ID != '" + ID + "'";
                    SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
                    int count = Convert.ToInt32(cmd2.ExecuteScalar());

                    bool checker = false;

                    while(checker == false)
                    {
                        Random rand = new Random();
                        random = rand.Next(1, count + 1);

                        string query6 = "SELECT COUNT(1) FROM Matches WHERE profile2 = '" + ID + "'";
                        SqlCommand cmd6 = new SqlCommand(query6, sqlCon);

                        if (Convert.ToInt32(cmd6.ExecuteScalar()) == 0)
                        {
                            checker = true;
                        }
                    }


                    string query4 = "SELECT * FROM Profiles WHERE ID = '" + profile1 + "'";
                    SqlCommand cmd4 = new SqlCommand(query4, sqlCon);

                    SqlDataAdapter da = new SqlDataAdapter(cmd4);
                    da.Fill(dt);

                    MyDataGrid.ItemsSource = dt.DefaultView;

                    match = 0;
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

        private void Match_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                sqlCon.Open();
                if (match == 0)
                {
                    string query5 = "INSERT INTO Matches(profile1, profile2) values('" + ID + "', '" + random + "' )";
                    SqlCommand cmd5 = new SqlCommand(query5, sqlCon);
                    cmd5.ExecuteNonQuery();
                    MessageBox.Show("Awaiting Response!");
                }
                else if(match == 1)
                {
                    string query5 = "UPADTE Matches SET match = 1 WHERE profile1 = '" + profile1 + "' and profile 2 = '" + ID + "'";
                    SqlCommand cmd5 = new SqlCommand(query5, sqlCon);
                    cmd5.ExecuteNonQuery();

                    string query7 = "SELECT Instagram FROM Profiles WHERE ID = '" + profile1 + "'";
                    SqlCommand cmd7 = new SqlCommand(query7, sqlCon);
                    MessageBox.Show("Instagram: "+ Convert.ToString(cmd7.ExecuteScalar()));
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

            Matches m = new Matches(ID);
            m.Show();
            this.Close();
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Open();
                if (match == 0)
                {
                    string query5 = "INSERT INTO Matches(profile1, profile2, match) values('" + ID + "', '" + random + "', 0 )";
                    SqlCommand cmd5 = new SqlCommand(query5, sqlCon);
                    cmd5.ExecuteNonQuery();
                    MessageBox.Show("Generating New Match!");
                }
                else if (match == 1)
                {
                    string query5 = "UPADTE Matches SET match = 0 WHERE profile1 = '" + profile1 + "' and profile 2 = '" + ID + "'";
                    SqlCommand cmd5 = new SqlCommand(query5, sqlCon);
                    cmd5.ExecuteNonQuery();
                    MessageBox.Show("Generating New Match!");
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

            Matches m = new Matches(ID);
            m.Show();
            this.Close();
        }
    }
}
