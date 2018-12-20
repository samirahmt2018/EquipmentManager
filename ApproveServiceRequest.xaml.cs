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
    /// Interaction logic for ApproveServiceRequest.xaml
    /// </summary>
    public partial class ApproveServiceRequest : Window
    {
        DataTable values,valuesC;
        int myGridSelectedIndex = 0;
        string serviceID = "";
        public ApproveServiceRequest()
        {
            InitializeComponent();
            try
            {

                values = DBManager.selectForServiceApproval();
                valuesC = values.Clone();
                approved_Items.ItemsSource = valuesC.AsDataView();
                approve_service_request.ItemsSource = values.AsDataView();
            }
            catch
            {

            }

        }



        private void approve_service_request_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                myGridSelectedIndex = approve_service_request.SelectedIndex;
                serviceID = values.Rows[approve_service_request.SelectedIndex][0].ToString();


            }
            catch
            {

            }
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            if (Home.printDatatableToExcel("Approved Services", "approved_items", valuesC))
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

        private void approveButton_Click(object sender, RoutedEventArgs e)
        {
            if (serviceID != "")
            {
                string query = "UPDATE service_request SET om_approved='1' WHERE id='" + serviceID + "'";
                if (DBManager.UpdateDatabaseQuery(query))
                {
                    valuesC.Rows.Add(values.Rows[myGridSelectedIndex].ItemArray);
                    myMessageBox mm = new myMessageBox("Approved Succesfully", "Success", "OK", 0);
                    mm.ShowDialog();

                 //   MessageBox.Show("Approved Succefully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    values = DBManager.selectForMaintainanceApproval();
                    approve_service_request.ItemsSource = values.AsDataView();
                    serviceID = "";


                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "OK", 1);
                    mm.ShowDialog();

                   // MessageBox.Show("Error Occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }
        }
    }
}


