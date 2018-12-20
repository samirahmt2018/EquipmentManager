using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Equipment_Manager
{
    /// <summary>
    /// Interaction logic for EnterTransportationCost.xaml
    /// </summary>
    public partial class EnterTransportationCost : Window
    {
        public string trCost { get; set; }
        public EnterTransportationCost()
        {
            InitializeComponent();
        }

        private void okBut_Click(object sender, RoutedEventArgs e)
        {
            trCost = transportCost.Text;
            DialogResult = true;
        }
    }
}
