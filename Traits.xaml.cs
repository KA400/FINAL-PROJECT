using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Traits.xaml
    /// </summary>
    public partial class Traits : Window
    {
        int ID;
        public Traits(int profile_ID)
        {
            InitializeComponent();
            ID = profile_ID;
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAB113PC06\LOCALHOST;Initial Catalog=TopDate;Integrated Security=True;");

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += Window_Loaded;
            Gender_SelectionChanged(null, null);
        }

        private void Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Gender.SelectedIndex == 0)
            {
                string[] bodyType = new string[] { "Ectomorph", "Endomorph", "Mesomorph" };
                AddToCombo(bodyType, BodyType);
            }
            else
            {
                string[] bodyType = new string[] { "Triangle", "Inverted Triangle", "Rectangle", "Hourglass", "Oval" };
                AddToCombo(bodyType, BodyType);
            }
        }

        public void AddToCombo(string[] array, ComboBox c)
        {
            c.Items.Clear();
            foreach (string item in array)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = item;
                c.Items.Add(comboBoxItem);
            }
        }
        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                sqlCon.Open();

                string querry = "INSERT INTO Traits(ID, Gender, Weight, Height, EyeColor, HairColor, BodyType) values ('" + ID + "' , '" + this.Gender.SelectedValue + "' , '" + this.WeightSlider.Value + "' , '" + this.HeightSlider.Value + "' , '" + this.EyeColor.SelectedValue + "' , '" + this.HairColor.SelectedValue + "' , '" + this.BodyType.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(querry, sqlCon);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Updated Traits!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            Expectations ep = new Expectations(ID);
            ep.Show();
            this.Close();
        }
    }
}
