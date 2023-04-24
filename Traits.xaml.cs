using System;
using System.Collections.Generic;
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
        public Traits(int profile_ID)
        {
            InitializeComponent();
        }

        private void Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Gender.SelectedIndex == Gender.Items.Count - 2)
            {
                BodyType.ItemsSource = new List<string> { "Ectomorph", "Endomorph", "Mesomorph" };
            }
            else
            {
                BodyType.ItemsSource = new List<string> { "Triangle", "Inverted Triangle", "Rectangle", "Hourglass", "Oval" };
            }
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            /* Expectations hp = new Expectations(profile_ID);
            hp.Show();
            this.Close(); */
        }
    }
}
