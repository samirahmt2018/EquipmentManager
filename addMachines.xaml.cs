using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for addMachines.xaml
    /// </summary>
    public partial class addMachines : Window
    {
        public addMachines()
        {
            InitializeComponent();
            DataTable functs = DBManager.selectFromFunctions();
            DataTable groups = DBManager.selectFromGroups();
            majorFunctionsBox.ItemsSource = functs.AsDataView();
            machineGroupBox.ItemsSource = groups.AsDataView();
        }

        private void addMachinesButton_Click(object sender, RoutedEventArgs e)
        {
            string resp = DBManager.InsertIntoMachines("('"+machineNameBox.Text + "','" + majorFunctionsBox.SelectedValue.ToString() + "','" + machineModelBox.Text + "','" + productionMeasurementBox.Text + "','" + machineCapacityBox.Text + "','" + machineGroupBox.SelectedValue.ToString()+"')");
            if (resp.Contains("Success"))
            {
                myMessageBox mm = new myMessageBox("Machine Added Succesfully", "Success", "OK", 0);
                mm.ShowDialog();

              //  MessageBox.Show("Success", "Machine added Succesfully!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                //MessageBox.Show("error", resp, MessageBoxButton.OK, MessageBoxImage.Error);
                myMessageBox mm = new myMessageBox(resp, "Error", "OK", 1);
                mm.ShowDialog();


            }
        }
    }
}
