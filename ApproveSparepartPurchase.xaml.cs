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
    /// Interaction logic for ApproveSparepartPurchase.xaml
    /// </summary>
    public partial class ApproveSparepartPurchase : Window
    {
        DataTable values,valuesC;
        string purchaseID = "";
        int myGridSelectedIndex = 0;
        public ApproveSparepartPurchase()
        {
            InitializeComponent();
            try
            {

                values = DBManager.selectForPurchaseApproval();
                approve_sparepart_purchase.ItemsSource = values.AsDataView();
                valuesC = values.Clone();
                approved_Items.ItemsSource = valuesC.AsDataView();
            }
            catch
            {

            }

        }



        private void approve_sparepart_purchase_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            try
            {
                myGridSelectedIndex = approve_sparepart_purchase.SelectedIndex;
                purchaseID = values.Rows[approve_sparepart_purchase.SelectedIndex][0].ToString();

            }
            catch
            {

            }
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            if (Home.printDatatableToExcel("Approved Sparepart Purchase", "approved_items", valuesC))
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
            if (purchaseID != "")
            {
                string query = "UPDATE sparepart_purchase SET om_approved='1',date_purchased='"+DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id='" + purchaseID + "'";
                  if (DBManager.UpdateDatabaseQuery(query) )
                {
                    valuesC.Rows.Add(values.Rows[myGridSelectedIndex].ItemArray);
                    myMessageBox mm = new myMessageBox("Approved Successfully", "Success", "OK", 0);
                    mm.ShowDialog();

                    //                    MessageBox.Show("Approved Succefully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    values = DBManager.selectForPurchaseApproval();
                    approve_sparepart_purchase.ItemsSource = values.AsDataView();
                    purchaseID = "";
                   

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


