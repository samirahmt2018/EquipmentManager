using System;
using System.Collections.Generic;
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
    /// Interaction logic for DepreciatedValue.xaml
    /// </summary>
    public partial class DepreciatedValue : Window
    {
        public DepreciatedValue()
        {
            InitializeComponent();
        }

        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE equipment_list SET depreciated_cost='"+depreciated_value_box.Text+"' WHERE equipment_id='"+equipmentID_box.Text+"'";
                if (DBManager.UpdateDatabaseQuery(query))
                {
                    myMessageBox mm = new myMessageBox("Updated Succefully", "Success", "Ok", 0);
                    mm.ShowDialog();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured When Updating. The supplied equipment id doesnt Exist", "Error", "Ok", 1);
                    mm.ShowDialog();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void depreciated_value_box_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !myValidation.IsfloatNumberKey(e.Key);
        }
    }
}
