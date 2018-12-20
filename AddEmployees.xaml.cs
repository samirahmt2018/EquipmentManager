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
    /// Interaction logic for AddEmployees.xaml
    /// </summary>
    public partial class AddEmployees : Window
    {
        public AddEmployees()
        {
            InitializeComponent();
            DataTable depts = DBManager.selectFromDepartment();
            DataTable usrLevels = DBManager.selectFromUserLevel();
            DataTable sites = DBManager.selectSiteLocations();
            userDepartmentBox.ItemsSource = depts.AsDataView();
            userLevelBox.ItemsSource = usrLevels.AsDataView();
            userLocationBox.ItemsSource = sites.AsDataView();
        }

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string resp = DBManager.InsertIntoEmployees("('" + userIdBox.Text + "','" + userNameBox.Text + "','" + fatherNameBox.Text + "','" + userDepartmentBox.SelectedValue.ToString() + "','" + userLocationBox.SelectedValue.ToString() + "','" + userEmailBox.Text +"','"+userphoneBox.Text+"','"+userPasswordBox.Password+"','"+userLevelBox.SelectedValue.ToString()+"','"+UserStatusBox.IsChecked.ToString()+ "')");
            if (resp.Contains("Success"))
            {
                myMessageBox mm = new myMessageBox("Employee Info Added Successfully!", "Success", "OK", 0);
                mm.ShowDialog();

               // MessageBox.Show("Success", "Employee added Succesfully!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                myMessageBox mm = new myMessageBox(resp, "Error", "OK", 1);
                mm.ShowDialog();

               // MessageBox.Show("error", resp, MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
