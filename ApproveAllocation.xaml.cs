


using ClosedXML.Excel;
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
    /// Interaction logic for ApproveAllocation.xaml
    /// </summary>
    public partial class ApproveAllocation : Window
    {
        DataTable values,valuesC;
        string allocationID="",allocateToID="";
        string equipmentID = "", requestID = "";
        int myGridSelectedIndex = 0;

        private void printButton_Click(object sender, RoutedEventArgs e)
        {

            if(Home.printDatatableToExcel("Approved Allocations", "approved_items", valuesC))
            {
                myMessageBox mm = new myMessageBox("Exported to Excel Succefully", "Success", "Ok", 0);
                mm.ShowDialog();
            }
            else
            {
                myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                mm.ShowDialog();
            }
            
        }

        public ApproveAllocation()
        {
            InitializeComponent();
            try
            {

                values = DBManager.selectForAllocationApproval();
                valuesC = values.Clone();
                approved_Items.ItemsSource = valuesC.AsDataView();
                approve_allocation_request.ItemsSource = values.AsDataView();
            }
            catch
            {

            }

        }


   
        private void approve_allocation_request_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                myGridSelectedIndex = approve_allocation_request.SelectedIndex;
                allocationID = values.Rows[approve_allocation_request.SelectedIndex][0].ToString();

                equipmentID = values.Rows[approve_allocation_request.SelectedIndex][1].ToString();
                allocateToID= values.Rows[approve_allocation_request.SelectedIndex][5].ToString();
                requestID = values.Rows[approve_allocation_request.SelectedIndex][6].ToString();

            }
            catch
            {

            }
        }

        private void approveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((equipmentID != "") && (allocationID != "") && allocateToID != "")
                {
                    string query = "UPDATE allocate_item SET allocation_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',om_approved='1' WHERE id='" + allocationID + "'";
                    string query2 = "UPDATE equipment_list SET current_Location='" + allocateToID + "' WHERE equipment_id='" + equipmentID + "'";
                    int allocatedNo = Convert.ToInt32(DBManager.selectAllocatedNumber(requestID).Rows[0][0].ToString());
                    string query3 = "UPDATE allocation_request SET allocated_item_no=" + (allocatedNo + 1) + " WHERE id='" + requestID + "'";
                    if (DBManager.UpdateDatabaseQuery(query) && DBManager.UpdateDatabaseQuery(query2) && DBManager.UpdateDatabaseQuery(query3))
                    {
                        valuesC.Rows.Add(values.Rows[myGridSelectedIndex].ItemArray);
                        //approved_Items.Columns.Add(approve_allocation_request.)
                        myMessageBox mm = new myMessageBox("Approved Successfully", "Success", "OK", 0);
                        mm.ShowDialog();

                    //    MessageBox.Show("Approved Succefully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        values = DBManager.selectForAllocationApproval();
                        approve_allocation_request.ItemsSource = values.AsDataView();
                        allocationID = "";
                        allocateToID = "";
                        equipmentID = "";

                    }
                    else
                    {
                        myMessageBox mm = new myMessageBox("Error Occured", "Error", "OK", 1);
                        mm.ShowDialog();

                       // MessageBox.Show("Error Occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }


                }
            }
            catch
            {

            }
        }
    }
}

