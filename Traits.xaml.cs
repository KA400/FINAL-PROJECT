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
using System.Windows.Forms;
using System.Threading.Tasks;
using SYstem.ComponentModel;
using System.Text;

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
            string[] bodyType;
            if(Gender.SelectedIndex == Gender.Items.Count - 2)
            {
                bodyType = { "Ectomorph", "Endomorph", "Mesomorph" };
            }
            else
            {
                bodyType = { "Triangle", "Inverted Triangle", "Rectangle", "Hourglass", "Oval" };
            }
            foreach (var item in bodyType)
            {
                BodyType.Items.Add(item);
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
