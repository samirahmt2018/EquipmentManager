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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Equipment_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable user = DBManager.selectFromUsers(email_box.Text, password_box.Password);
                if (user.Rows.Count == 1)
                {
                    // MessageBox.Show("Welcome " + user.Rows[0][1].ToString()+" " + user.Rows[0][2].ToString());
                    Home homeWin = new Home(user.Rows[0][1].ToString() + " " + user.Rows[0][2].ToString(), Convert.ToInt16(user.Rows[0][8].ToString()), Convert.ToInt16(user.Rows[0][4].ToString()), user.Rows[0][0].ToString());
                    homeWin.Show();
                    try
                    {
                        string trasactQuery = "('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Logging In','" + user.Rows[0][0].ToString() + "')";
                        DBManager.InsertIntoTransaction(trasactQuery);
                    }
                    catch
                    {

                    }
                    this.Close();
                }
                else
                {
                    myMessageBox mm = new myMessageBox("Login was unsuccessfull! \rCheck your user name or password", "Couldn't Login", "OK", 1);
                    mm.ShowDialog();
                }
            }
            catch
            {
                myMessageBox mm = new myMessageBox("Login was unsuccessfull! \rCheck your  Database connection", "Couldn't Login", "OK", 1);
                mm.ShowDialog();

                //MessageBox.Show("Couldnt Log in", "Login was unsuccefull! check user name, password or Database connection", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void Change_button_Click(object sender, RoutedEventArgs e)
        {
            ChangeSettings cSet = new ChangeSettings();
            cSet.ShowDialog();

        }
    }
}
