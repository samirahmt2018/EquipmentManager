using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using iTextSharp.text.pdf;
using System.Timers;
using ClosedXML.Excel;

namespace Equipment_Manager
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public static string logMessage;
        private int userLevel;
        private string userID;
        DataTable eqGridSource = new DataTable();
        public Home(string user_name, int user_level, int userlocation, string user_ID)
        {

            InitializeComponent();
            DataTable levl = DBManager.selectFromUserLevel(user_level);
            userID = user_ID;
            try
            {
                statBar.Content = user_name + ", " + levl.Rows[0][0].ToString();
                userLevel = user_level;

                if (user_level == 8 || user_level == 8)
                {
                    file_menu.Items.Remove(new_employee_menu);
                    file_menu.Items.Remove(new_supplier_info_menu);
                    file_menu.Items.Remove(update_depreciated_value_menu);
                    file_menu.Items.Remove(new_project_site_menu);
                    file_menu.Items.Remove(new_equipment_menu);
                    file_menu.Items.Remove(new_purchase_request_menu);
                    menu.Items.Remove(reports_menu);
                   // reports_menu.Items.Remove(monthly_equipment_report_menu);
                    menu.Items.Remove(approval_menu);
                   

                   
                    //eqGridSource = DBManager.selectallEquipmentStatus(userlocation);
                }
                else if (user_level == 4)
                {
                   
                    menu.Items.Remove(approval_menu);
                    
                    //eqGridSource = DBManager.selectallEquipmentStatus();
                }
                else if (userLevel == 5)
                {
                    menu.Items.Remove(status_menu);
                    menu.Items.Remove(reports_menu);
                    menu.Items.Remove(file_menu);

                    //eqGridSource = DBManager.selectallEquipmentStatus();
                }
                else if (userLevel == 7)
                {
                   
                }

                else
                {
                    menu.Items.Remove(status_menu);
                    menu.Items.Remove(reports_menu);
                    menu.Items.Remove(file_menu);
                    menu.Items.Remove(approval_menu);

                }
                eqGridSource = DBManager.selectallEquipmentYearlyStatus();
                equipmentList.ItemsSource = eqGridSource.AsDataView();
            }
            catch
            {

            }
        }


        private void new_equipment_Click(object sender, RoutedEventArgs e)
        {
            addEquipment addEquipment = new addEquipment();
            addEquipment.Show();
        }
        private void new_supplier_info_menu_Click(object sender, RoutedEventArgs e)
        {
            AddSupplierInfo addSupplier = new AddSupplierInfo();
            addSupplier.Show();
        }
        private void new_machine_category_Click(object sender, RoutedEventArgs e)
        {
            addMachines addMachinesModels = new addMachines();
            addMachinesModels.Show();
        }

        private void new_project_site_Click(object sender, RoutedEventArgs e)
        {
            addSites addSite = new addSites();
            addSite.Show();
        }

        private void new_employee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployees addUsers = new AddEmployees();
            addUsers.Show();
        }

        private void mExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void equipment_status_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //eqGridSource.Clear();

                eqGridSource = DBManager.selectallEquipmentStatus();
                equipmentList.ItemsSource = eqGridSource.AsDataView();
            }
            catch
            {

            }
        }

        private void Equipment_request_menu_Click(object sender, RoutedEventArgs e)
        {
            AllocateItem alI = new AllocateItem();
            alI.Show();
        }

        private void equipment_maintainance_menu_Click(object sender, RoutedEventArgs e)
        {
            MaintainanceRequests mR = new MaintainanceRequests();
            mR.Show();
        }
        private void equipment_service_menu_Click(object sender, RoutedEventArgs e)
        {
            ServiceRequests mR = new ServiceRequests();
            mR.Show();
        }



        private void logout_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string trasactQuery = "('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Logging Out','" + userID + "')";
                DBManager.InsertIntoTransaction(trasactQuery);
            }
            catch
            {

            }
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }


        private void change_password_menu_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword chPass = new ChangePassword(userID);
            chPass.ShowDialog();

        }

        private void new_maintainance_request_menu_Click(object sender, RoutedEventArgs e)
        {
            NewMaintainanceRequest AMR = new NewMaintainanceRequest();
            AMR.Show();
        }
        private void new_service_request_menu_Click(object sender, RoutedEventArgs e)
        {
            NewServiceRequest AMR = new NewServiceRequest();
            AMR.Show();
        }

        private void new_allocation_request_menu_Click(object sender, RoutedEventArgs e)
        {
            AllocationRequests allReq = new AllocationRequests();
            allReq.Show();
        }

        private void new_purchase_request_menu_Click(object sender, RoutedEventArgs e)
        {
            PurchaseRequest purReq = new PurchaseRequest();
            purReq.Show();
        }

        private void equipment_monitoring_menu_Click(object sender, RoutedEventArgs e)
        {

        }
        private void weekley_sparepart_report_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


                // if (DBManager.ExportToPdf(DBManager.SelectForSparepartPurchase(), filePath+"\\weekley sparepart report.pdf"))
                // {
                //     MessageBox.Show("Succefully written to"+filePath +" \\weekley sparepart report.pdf", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // }
                // else
                // {
                //     MessageBox.Show("Error Occured when Creating the pdf", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //} 

                if (Home.printDatatableToExcel("Weekley Sparepart Purchase", "sparepart_purchase", DBManager.SelectForSparepartPurchase()))
                {
                    myMessageBox mm = new myMessageBox("Exported to Excel Successfully", "Success", "Ok", 0);
                    mm.ShowDialog();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                    mm.ShowDialog();
                }

                
            }
            catch
            {

            }
        }

        private void monthly_equipment_allocation_report_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //if (DBManager.ExportToPdf(DBManager.SelectForMonthlyEquipmentAllocationReport(), filePath + "\\Monthly Allocation report.pdf"))
                //{
                //    MessageBox.Show("Succefully written to " + filePath + "\\Monthly Allocation report.pdf", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Error Occured when Creating the pdf", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //}
                if (Home.printDatatableToExcel("Monthly Equipment Allocation", "allocated_items", DBManager.SelectForMonthlyEquipmentAllocationReport()))
                {
                    myMessageBox mm = new myMessageBox("Exported to Excel Successfully", "Success", "Ok", 0);
                    mm.ShowDialog();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                    mm.ShowDialog();
                }
            }
            catch
            {

            }
        }

        private void weekley_maintainance_report_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //if (DBManager.ExportToPdf(DBManager.SelectForWeekleyMaintainanceReport(), filePath + "\\weekley maintenance report.pdf"))
                //{
                //    MessageBox.Show("Succefully written to " + filePath + "\\weekley maintenance report.pdf", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Error Occured when Creating the pdf", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //}
                if (Home.printDatatableToExcel("Weekley Maintainace Report", "maintained_items", DBManager.SelectForWeekleyMaintainanceReport()))
                {
                    myMessageBox mm = new myMessageBox("Exported to Excel Successfully", "Success", "Ok", 0);
                    mm.ShowDialog();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                    mm.ShowDialog();
                }
            }
            catch
            {

            }
        }




        private void weekley_service_report_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //if (DBManager.ExportToPdf(DBManager.SelectForWeekleyServiceReport(), filePath + "\\weekley Service report.pdf"))
                //{
                //    MessageBox.Show("Succefully written to " + filePath + "\\weekley Service report.pdf", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Error Occured when Creating the pdf", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //}
                if (Home.printDatatableToExcel("Weekley Service Report", "serviced_items", DBManager.SelectForWeekleyServiceReport()))
                {
                    myMessageBox mm = new myMessageBox("Exported to Excel Successfully", "Success", "Ok", 0);
                    mm.ShowDialog();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                    mm.ShowDialog();
                }
            }
            catch
            {

            }
        }

        private void monthly_equipment_report_menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void weekley_utilization_report_menu_Click(object sender, RoutedEventArgs e)
        {
            
                
        }

        private void new_equipment_monitoring_menu_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentsReport eqR = new AddEquipmentsReport(userID);
            eqR.Show();
        }

        private void annual_report_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //eqGridSource.Clear();

                eqGridSource = DBManager.selectallEquipmentYearlyStatus();
                equipmentList.ItemsSource = eqGridSource.AsDataView();
            }
            catch
            {

            }
        }

        private void monthly_report_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //eqGridSource.Clear();

                eqGridSource = DBManager.selectallEquipmentMonthlyStatus();
                equipmentList.ItemsSource = eqGridSource.AsDataView();
            }
            catch
            {

            }
        }

        private void weeley_report_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //eqGridSource.Clear();

                eqGridSource = DBManager.selectallEquipmentWeekleyStatus();
                equipmentList.ItemsSource = eqGridSource.AsDataView();
            }
            catch
            {

            }
        }

        private void request_summary_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void new_insurance_menu_Click(object sender, RoutedEventArgs e)
        {
            NewInsurancePayment NIP = new NewInsurancePayment();
            NIP.Show();
        }
        private void update_depreciated_value_menu_Click(object sender, RoutedEventArgs e)
        {
            DepreciatedValue NIP = new DepreciatedValue();
            NIP.Show();
        }

        private void approve_allocation_menu_Click(object sender, RoutedEventArgs e)
        {
            ApproveAllocation APA = new ApproveAllocation();
            APA.Show();
        }

        private void approve_maintainance_menu_Click(object sender, RoutedEventArgs e)
        {
            ApproveMaintainanceRequest AMR = new ApproveMaintainanceRequest();
            AMR.Show();
        }
        private void approve_service_menu_Click(object sender, RoutedEventArgs e)
        {
            ApproveServiceRequest AMR = new ApproveServiceRequest();
            AMR.Show();
        }

        private void approve_purchase_menu_Click(object sender, RoutedEventArgs e)
        {
            ApproveSparepartPurchase APS = new ApproveSparepartPurchase();
            APS.Show();
        }

        private void new_sparepart_purchase_request_menu_Click(object sender, RoutedEventArgs e)
        {
            NewSparepartPurchase nsp = new NewSparepartPurchase();
            nsp.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Timer retryTimer = new Timer();
            retryTimer.Elapsed += new ElapsedEventHandler(RetryFailedCxs);
            retryTimer.Interval = 16000;//to try mc connection every 5 minuits
            retryTimer.Start();
        }

        private void RetryFailedCxs(object sender, ElapsedEventArgs e)
        {
            try
            {
                string appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\errors_log"  + ".txt";
                File.AppendAllText(appPath, logMessage);
                logMessage = "";
            }

            catch (Exception ex)
            {

            }
        }
        public static bool printDatatableToExcel(string fileName, string sheetName, DataTable values)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook();

                wb.Worksheets.Add(values, sheetName);
                wb.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\"+fileName+".xlsx");
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void contact_info_Click(object sender, RoutedEventArgs e)
        {
            myMessageBox mm = new myMessageBox("Email: netsitadi@gmail.com ---> Flintstones ", "Contact Info", "OK", 0);
            mm.ShowDialog();
        }

        private void monthly_activity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Home.printDatatableToExcel("Last 30 Days Transaction Summary", "Transaction Summary", DBManager.selectMonthlyTransaction()))
                {
                    myMessageBox mm = new myMessageBox("Exported to Excel Successfully", "Success", "Ok", 0);
                    mm.ShowDialog();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                    mm.ShowDialog();
                }
            }
            catch
            {

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string trasactQuery = "('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Logging Out','" + userID + "')";
                DBManager.InsertIntoTransaction(trasactQuery);
            }
            catch
            {

            }
        }
    }
}
