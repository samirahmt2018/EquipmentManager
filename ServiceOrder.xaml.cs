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
    /// Interaction logic for ServiceOrder.xaml
    /// </summary>
    public partial class ServiceOrder : Window
    {
        string serviceOrderID = "", serviceID = "", equipmentID = "", sparePart = "", laborC = "", statuM = "", completionD = "";
        public ServiceOrder(string ID, string eqID)
        {
            InitializeComponent();
            try
            {
                equipmentID = eqID;
                serviceID = ID;
              
                DataTable statuT = DBManager.selectFromMaintainanceStatus();
                StatusBox.ItemsSource = statuT.AsDataView();
            }
            catch
            {

            }

        }

        private void addWorkorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sparePartBox.Text != null)
                {
                    sparePart = sparePartBox.Text;
                }
                if (laboreBox.Text != null)
                {
                    laborC = laboreBox.Text;
                }
                if (StatusBox.SelectedValue != null)
                {
                    statuM = StatusBox.SelectedValue.ToString();
                }
                if (completionDateBox.SelectedDate != null)
                {
                    completionD = completionDateBox.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                string query = "UPDATE service_request SET em_approved='1', service_sparepart_cost='" + sparePart + "',service_labor_cost='" + laborC + "',service_status='" + statuM + "',service_completion_date='" + completionD + "' WHERE id='" + serviceID + "'";
                if (DBManager.UpdateDatabaseQuery(query))
                {
                    query = "";
                    if (StatusBox.SelectedValue.ToString() == "3")
                    {
                        query = "UPDATE equipment_list SET equipment_list.current_status = '2' WHERE equipment_list.equipment_id = '" + equipmentID + "'";
                    }
                    else
                    {
                        query = "UPDATE equipment_list SET equipment_list.current_status = '3' WHERE equipment_list.equipment_id = '" + equipmentID + "'";

                    }
                    if (DBManager.UpdateDatabaseQuery(query))
                    {
                        myMessageBox mm = new myMessageBox("Succesfully Updated", "Success", "OK", 0);
                        mm.ShowDialog();

                        //MessageBox.Show("Succesfully Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch
            {

            }
        }
    }
}