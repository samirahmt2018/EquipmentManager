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
    /// Interaction logic for ServiceRequests.xaml
    /// </summary>
    public partial class ServiceRequests : Window
    {
        string serviceRequestID = "", equipmentID = "";
        DataTable servReq;
        public ServiceRequests()
        {
            InitializeComponent();

            try
            {
                servReq = DBManager.SelectPendingServiceRequests();
                service_requestes.ItemsSource = servReq.AsDataView();
            }
            catch
            {

            }
        }

        private void service_requestes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                serviceRequestID = servReq.Rows[service_requestes.SelectedIndex][0].ToString();

                equipmentID = servReq.Rows[service_requestes.SelectedIndex][1].ToString();
            }
            catch
            {

            }
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (serviceRequestID != "")
                {
                    ServiceOrder wo = new ServiceOrder(serviceRequestID, equipmentID);
                    wo.ShowDialog();
                    servReq = DBManager.SelectPendingServiceRequests();
                    service_requestes.ItemsSource = servReq.AsDataView();
                }
            }
            catch
            {

            }

        }
    }
}
