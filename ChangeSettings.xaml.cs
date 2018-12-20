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
    /// Interaction logic for ChangeSettings.xaml
    /// </summary>
    public partial class ChangeSettings : Window
    {
        myMessageBox mm;
        public ChangeSettings()
        {
            InitializeComponent();

            try
            {
                mySqlServerBox.Text = Properties.Settings.Default.mySqlServerAdress;
                mySqlPortBox.Text = Properties.Settings.Default.mySqlPort;
                mySqlDatabaseNameBox.Text = Properties.Settings.Default.mySqlDatabase;
                mySqlPassswordBox.Password = Properties.Settings.Default.MysqlPassword;
                mySqlUserNameBox.Text = Properties.Settings.Default.mySqlUserName;
              }
            catch
            {

            }

            //sysPasswordBox.Password = Properties.Settings.Default.passCode;
        }

        private void ChangeMysqlButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string mserver, mdb, muser, mpass;

                mserver = mySqlServerBox.Text.Trim(' ');
                mdb = mySqlDatabaseNameBox.Text.Trim(' ');
                muser = mySqlUserNameBox.Text.Trim(' ');
                mpass = mySqlPassswordBox.Password.Trim(' ');
                if (myValidation.IsEmpty(mdb) || myValidation.IsEmpty(mserver) || myValidation.IsEmpty(muser) || myValidation.IsEmpty(mySqlPortBox.Text.Trim(' ')))
                {
                    mm = new myMessageBox("One or more fields are empty correct them and try again", "Empty Fields", "Ok", 1);
                    mm.ShowDialog();
                    return;
                }
                int mport = Convert.ToInt32(mySqlPortBox.Text.Trim(' '));
                if (mport < 0 || mport > 65535)
                {
                    mm = new myMessageBox("Invalid Port Number! it should be in the range 0-655535", "Invalid Port", "Ok", 1);
                    mm.ShowDialog();
                    return;
                }
                Properties.Settings.Default.mySqlServerAdress = "Server=" + mserver + ";Port=" + mport + ";Database=" + mdb + ";Uid=" + muser + ";Pwd=" + mpass + "; CharSet=utf8";
                Properties.Settings.Default.mySqlServerAdress = mserver;
                Properties.Settings.Default.mySqlPort = mySqlPortBox.Text.Trim(' ');
                Properties.Settings.Default.mySqlDatabase = mdb;
                Properties.Settings.Default.mySqlUserName = muser;
                Properties.Settings.Default.MysqlPassword = mpass;
                Properties.Settings.Default.Save();
                mm = new myMessageBox("Connection String changed to \"Server=" + mySqlServerBox.Text + ";Port=" + mySqlPortBox.Text + ";Database=" + mySqlDatabaseNameBox.Text + ";Uid=" + mySqlUserNameBox.Text + ";Pwd=" + new String(mySqlPassswordBox.PasswordChar, mySqlPassswordBox.Password.Length) + "; CharSet=utf8\"", "Info", "OK", 0);
                mm.ShowDialog();


            }
            catch
            {
                mm = new myMessageBox("Error Occured when prossesing Values", "Error", "Ok", 1);
                mm.ShowDialog();
                return;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


     
        private void mySqlPortBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !myValidation.IsIntegerNumberKey(e.Key);
        }

        private void updateEveryBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !myValidation.IsIntegerNumberKey(e.Key);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            myMessageBox mm = new myMessageBox(DBManager.RunScript("flintstones_db.sql").ToString(), "Database Creation", "Ok", 2);
            mm.ShowDialog();
        }
    }
}
