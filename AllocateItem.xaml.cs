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
    /// Interaction logic for AllocateItem.xaml
    /// </summary>
    public partial class AllocateItem : Window
    {
        DataTable values = DBManager.SelectFromAlocationRequests();
        DataTable eqAll;
        string selectedEqID="", selected_allocation_id="",fromSiteID="",toSiteID="";
        public AllocateItem()
        {
            InitializeComponent();
            try
            {
                

               allocation_request.ItemsSource = values.AsDataView();
            }
            catch (Exception ex)
            {

            }

        }

        
        private void alllocateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectedEqID!="" && selected_allocation_id != "")
                {
                    string transportCost = "";
                    EnterTransportationCost nn = new EnterTransportationCost();
                    nn.ShowDialog();
                    if (nn.DialogResult.Value)
                    {
                        transportCost = nn.trCost;
                    }
                    string resp = DBManager.InsertIntoAllocationTable("('" + selected_allocation_id + "','" + selectedEqID + "','" + fromSiteID + "','" + toSiteID + "','"+transportCost+ "')");
                    if (resp.Contains("Success"))
                    {
                        myMessageBox mm = new myMessageBox("Successfully allocated. Waiting for OM approval", "Success", "OK", 0);
                        mm.ShowDialog();

                      //  MessageBox.Show("Successfully Allocated waiting for OM approval", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        eqAll.Clear();
                        
                    }
                    else
                    {
                        myMessageBox mm = new myMessageBox("Allocation Failed!\n Maybe the selected equipment id was already allocated to this project", "Error", "OK", 1);
                        mm.ShowDialog();

                       // MessageBox.Show("", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        eqAll.Clear();

                    }
                }
            }
            catch
            {

            }
        }

        private void cancelRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected_allocation_id != "")
                {
                    string query = "UPDATE `allocation_request` SET `request_status`='4' WHERE id='" + selected_allocation_id + "'";
                    DBManager.UpdateDatabaseQuery(query);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void equipment_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                  
                selectedEqID = eqAll.Rows[equipment_list.SelectedIndex][0].ToString();
                fromSiteID=eqAll.Rows[equipment_list.SelectedIndex][1].ToString();
            }
            catch
            {

            }
        }

        private void allocation_request_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                toSiteID = values.Rows[allocation_request.SelectedIndex][3].ToString();
                eqAll = DBManager.SelectEquipmentForAlocation(values.Rows[allocation_request.SelectedIndex][1].ToString(),toSiteID);

                equipment_list.ItemsSource = eqAll.AsDataView();
                selected_allocation_id = values.Rows[allocation_request.SelectedIndex][0].ToString();
               
            }
            catch
            {

            }
        }
    }
}
