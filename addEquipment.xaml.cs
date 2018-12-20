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
    /// Interaction logic for addEquipment.xaml
    /// </summary>
    public partial class addEquipment : Window
    {
        DataTable operators;
        DataTable machines;

        DataTable siteLoc;
        DataTable eqType;
        DataTable eqStatus;
        DataTable insuranceStat;
        DataTable suppliers;

       
        public addEquipment()
        {
            InitializeComponent();
            try
            {
                 machines = DBManager.selectFromMachines();

                siteLoc = DBManager.selectSiteLocations();
                eqType = DBManager.selectEquipmentType();
                eqStatus = DBManager.selectEquipmentStatus();
                insuranceStat = DBManager.selectInsuranceStatuses();

                operators = DBManager.selectDriversAndOperators();
                suppliers= DBManager.selectSuppliers();
                machineTypeBox.ItemsSource = machines.AsDataView();
                currentLocationBox.ItemsSource = siteLoc.AsDataView();
                equipmentTypeBox.ItemsSource = eqType.AsDataView();
                currentStatusBox.ItemsSource = eqStatus.AsDataView();
                insuranceStatusBox.ItemsSource = insuranceStat.AsDataView();
                operatorNameBox.ItemsSource = operators.AsDataView();
                supplierBox.ItemsSource = suppliers.AsDataView();
            }
            catch
            {

            }
        }

        private void addEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            string opId;
            if (operatorNameBox.SelectedValue == null)
            {
                opId = "";
            }
            else
            {
                opId = operatorNameBox.SelectedValue.ToString();
            }
            string resp = DBManager.InsertIntoEquipments("('"+ equipmentIdBox.Text +"','" + machineTypeBox.SelectedValue.ToString()+ "','" + plateNumberBox.Text + "','" + manufacturedYearBox.Text + "','" + currentLocationBox.SelectedValue.ToString() + "','" + currentStatusBox.SelectedValue.ToString() + "','" + investmentCostBox.Text+"','"+servicedEveryBox.Text+"','"+ equipmentTypeBox.SelectedValue.ToString() +"','"+insuranceStatusBox.SelectedValue.ToString()+"','"+ opId + "','" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"')");
            if (resp.Contains("Success"))
            {
                myMessageBox mm = new myMessageBox("Equipment Added Successfully", "Success", "OK", 0);
                mm.ShowDialog();

                //MessageBox.Show("Success", "Equipment added Succesfully!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                myMessageBox mm = new myMessageBox(resp, "Error", "OK", 1);
                mm.ShowDialog();

               // MessageBox.Show("error", resp, MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }


        private void equipmentTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string eqID = DBManager.selectIdTag(equipmentTypeBox.SelectedValue.ToString()).Rows[0][2].ToString()+"-" + DBManager.countEquipments(equipmentTypeBox.SelectedValue.ToString()).ToString("D6");
                int i = 0;
                while (DBManager.countDuplicateEquipments(eqID) != 0)
                {
                    i = i + 1;
                    eqID = DBManager.selectIdTag(equipmentTypeBox.SelectedValue.ToString()).Rows[0][2].ToString() + "-" + (DBManager.countEquipments(equipmentTypeBox.SelectedValue.ToString())+i).ToString("D6");
                }
                equipmentIdBox.Text = eqID;
            }
            catch
            {

            }
        }

        private void machineTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectdMachine = machineTypeBox.SelectedValue.ToString();
                DataTable machinGrp = DBManager.selectMachineGroups(selectdMachine);
                
                if (Convert.ToInt16(machinGrp.Rows[0][0].ToString()) == 4)
                {
                    operators = DBManager.selectOperators("5");
                }
                else
                {
                    operators = DBManager.selectOperators("9");
                }

                operatorNameBox.ItemsSource = operators.AsDataView();
            }
            catch
            {

            }
        }
    }
}
