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
    /// Interaction logic for WorkOrder.xaml
    /// </summary>
    public partial class WorkOrder : Window
    {
        string workOrderID = "",maintainanceID="",equipmentID="", sparePart="",laborC="",priorityV="",statuM="",completionD="";
        public WorkOrder(string ID,string eqID)
        {
            InitializeComponent();
            try
            {
                equipmentID = eqID;
                workOrderID = "WOR-" + Convert.ToInt32(ID).ToString("D6") + "-" + DateTime.Now.Year.ToString();
                workorderIdBox.Text = workOrderID;
                maintainanceID = ID;
                DataTable prioriyT = DBManager.selectFromMaintainancePriority();

                DataTable statuT = DBManager.selectFromMaintainanceStatus();
                PriorityBox.ItemsSource = prioriyT.AsDataView();
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
                    sparePart=sparePartBox.Text;
                }
                if(laboreBox.Text != null)
                {
                    laborC = laboreBox.Text;
                }
                if (PriorityBox.SelectedValue != null)
                {
                    priorityV = PriorityBox.SelectedValue.ToString();
                }
                if (StatusBox.SelectedValue!=null)
                {
                    statuM = StatusBox.SelectedValue.ToString();
                }
                if (completionDateBox.SelectedDate !=null)
                {
                    completionD = completionDateBox.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                string query = "UPDATE maintainance_request SET request_status='2',em_approved='1', work_order_Id='" + workOrderID + "',maintainance_sparepart_cost='" + sparePart + "',maintainance_labor_cost='" + laborC + "',maintainance_priority='" + priorityV + "',maintainance_status='" +statuM + "',maintainance_completion_date='" + completionD + "' WHERE maintainance_id='" + maintainanceID + "'";
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
                        myMessageBox mm = new myMessageBox("Successfully updated", "Success", "OK", 0);
                        mm.ShowDialog();

                        // MessageBox.Show("Succesfully Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
