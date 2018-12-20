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
    /// Interaction logic for MaintainanceRequests.xaml
    /// </summary>
    public partial class MaintainanceRequests : Window
    {
        string maintainanceRequestID="",equipmentID="";
        DataTable maintReq;
        
        public MaintainanceRequests()
        {
            InitializeComponent();
            
            try
            {
                maintReq = DBManager.SelectPendingMaintainanceRequests();
                maintainance_requestes.ItemsSource = maintReq.AsDataView();
            }
            catch
            {

            }
        }

        private void maintainance_requestes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                maintainanceRequestID = maintReq.Rows[maintainance_requestes.SelectedIndex][0].ToString();

                equipmentID= maintReq.Rows[maintainance_requestes.SelectedIndex][1].ToString();
            }
            catch
            {

            }
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (maintainanceRequestID != "")
                {
                    WorkOrder wo = new WorkOrder(maintainanceRequestID, equipmentID);
                    wo.ShowDialog();
                    maintReq = DBManager.SelectPendingMaintainanceRequests();
                    maintainance_requestes.ItemsSource = maintReq.AsDataView();
                }
            }
            catch
            {

            }
                
      }
    }
}
