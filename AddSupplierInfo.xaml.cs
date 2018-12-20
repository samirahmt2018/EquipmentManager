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
    /// Interaction logic for AddSupplierInfo.xaml
    /// </summary>
    public partial class AddSupplierInfo : Window
    {
        public AddSupplierInfo()
        {
            InitializeComponent();
        }

        private void addSuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            if (!myValidation.IsEmpty(supplierNameBox.Text))
            {
                string retD = DBManager.InsertIntoSupplierInfo("('" + supplierNameBox.Text + "','" + supplierAddressBox.Text + "','" + supplierPhone1Box.Text + "','" + supplierPhone2Box.Text + "','" + supplierPoBox.Text + "','" + supplierEmailBox.Text + "')");
                if (retD.Contains("Success"))
                {
                    myMessageBox mm = new myMessageBox("Succesfully Added", "Success", "Ok", 0);
                    mm.ShowDialog();
                    this.Close();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Error Occured", "Error", "Ok", 1);
                    mm.ShowDialog();
                }
            }
        }
    }
}
