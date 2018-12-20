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
    /// Interaction logic for addSites.xaml
    /// </summary>
    public partial class addSites : Window
    {
        public addSites()
        {
            InitializeComponent();
            siteStatusBox.Items.Add("Completed");
            siteStatusBox.Items.Add("On Progress");
            siteStatusBox.Items.Add("Office");
        }

        private void addSitesButton_Click(object sender, RoutedEventArgs e)
        {
            string resp = DBManager.InsertIntoSites("('" + codeNameBox.Text + "','" + siteNameBox.Text + "','" + siteStatusBox.SelectedValue.ToString()+ "')");
            if (resp.Contains("Success"))
            {
                myMessageBox mm = new myMessageBox("Site Added Succcessfully", "Success", "OK", 0);
                mm.ShowDialog();

                //MessageBox.Show("Success", "Site added Succesfully!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                myMessageBox mm = new myMessageBox(resp, "Error", "OK", 1);
                mm.ShowDialog();

//                MessageBox.Show("error", resp, MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
