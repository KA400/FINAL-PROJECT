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
    /// Interaction logic for Expectations.xaml
    /// </summary>
    public partial class Expectations : Window
    {
        int ID;
        public Expectations(int profile_ID)
        {
            InitializeComponent();
            ID = profile_ID;
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LIDY;Initial Catalog=TopDate;Integrated Security=True;");

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

                string querry = "INSERT INTO Expectations(ID, Gender, Weight_top, Weight_bottom, Height_top, Height_bottom, EyeColor1, EyeColor2, EyeColor3, EyeColor4, EyeColor5, EyeColor6, EyeColor7, HairColor1, HairColor2, HairColor3, HairColor4, HairColor5, BodyType) values ('" + ID + "' , '" + this.Gender.SelectedValue + "' , '" + this.WeightSliderMax.Value + "' , '" + this.WeightSliderMin.Value + "' , '" + this.HeightSliderMax.Value + "' , '" + this.HeightSliderMin.Value + "' , '" + Convert.ToByte(this.EyeColor1.IsChecked) + "' , '" + Convert.ToByte(this.EyeColor2.IsChecked) + "' , '" + Convert.ToByte(this.EyeColor3.IsChecked) + "' , '" + Convert.ToByte(this.EyeColor4.IsChecked) + "' , '" + Convert.ToByte(this.EyeColor5.IsChecked) + "' , '" + Convert.ToByte(this.EyeColor6.IsChecked) + "' , '" + Convert.ToByte(this.EyeColor7.IsChecked) + "' , '" + Convert.ToByte(this.HairColor1.IsChecked) + "' , '" + Convert.ToByte(this.HairColor2.IsChecked) + "' , '" + Convert.ToByte(this.HairColor3.IsChecked) + "' , '" + Convert.ToByte(this.HairColor4.IsChecked) + "' , '" + Convert.ToByte(this.HairColor5.IsChecked) + "' , '" + this.BodyType.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(querry, sqlCon);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Updated Expectations!");
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
