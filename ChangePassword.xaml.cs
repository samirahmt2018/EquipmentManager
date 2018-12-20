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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private string curruserID=""; 
        public ChangePassword(string userID)
        {
            InitializeComponent();
            curruserID = userID;
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password.Count() >= 6)
            {
                if (newPassword.Password.Equals(newPassword1.Password))
                {
                    string passFromDB = DBManager.selectPasswordFromUsers(curruserID).Rows[0][0].ToString();
                    if (oldPassword.Password.Equals(passFromDB))
                    {
                        if(DBManager.updatePasswordForUsers(curruserID, newPassword.Password))
                        {
                            myMessageBox mm = new myMessageBox("Passwords Changed Succesfully", "Succesfully Updated", "OK", 0);
                            mm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            myMessageBox mm = new myMessageBox("Error Occured when updating", "Database Error", "OK", 1);
                            mm.ShowDialog();
                        }
                    }
                    else
                    {
                        myMessageBox mm = new myMessageBox("Old Password was wrong", "Incorrect input", "OK", 1);
                        mm.ShowDialog();
                    }
                }
                else
                {
                    myMessageBox mm = new myMessageBox("New Passwords doesnt match", "Incorrect input", "OK", 1);
                    mm.ShowDialog();
                }

            }
            else
            {
                myMessageBox mm = new myMessageBox("Password Length should be between 8 and 32", "Incorrect input", "OK", 1);
                mm.ShowDialog();
            }
        }
    }
}
