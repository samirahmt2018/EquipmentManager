﻿using System;
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
    /// Interaction logic for NewMaintainanceRequest.xaml
    /// </summary>
    public partial class NewMaintainanceRequest : Window
    {
        DataTable values;
        public NewMaintainanceRequest()
        {
            InitializeComponent();
            try
            {

                //  maintainance_request.ItemsSource = values.AsDataView();
                ExcelFileSelector fileS = new ExcelFileSelector();
                fileS.ShowDialog();
                values = DBManager.SelectFromExcel(fileS.fileName, fileS.sheetName, fileS.cellValues);

                maintainance_request.ItemsSource = values.AsDataView();
            }
            catch(Exception ex)
            {
                
            }

        }

        private void maintainance_request_AutoGeneratedColumns(object sender, EventArgs e)
        {
            try
            {


                maintainance_request.Columns[0].Header = "Equipment ID";
                maintainance_request.Columns[1].Header = "Requested Date";
                maintainance_request.Columns[2].Header = "Due Date";
                maintainance_request.Columns[3].Header = "Problem";
                maintainance_request.Columns[4].Header = "Requested By";
              


            }
            catch (Exception ex)
            {

                //this.Close();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (values == null || values.Columns.Count != 5)
            {
                this.Close();
                // MessageBox.Show("Invalid Data size", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                myMessageBox mm = new myMessageBox("Column Count Doesnt match or no data found", "Error", "OK", 1);
                mm.ShowDialog();

            }
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string datas = "";
            string updateQueries = "";
            try
            {
                foreach (DataRow row in values.Rows)
                {
                    if (i == 0)
                    {
                        datas = "('" + row[0].ToString() + "','" + Convert.ToDateTime(row[1].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(row[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','" + row[3].ToString() + "','" + row[4].ToString()  + "')";
                        
                    }
                    else
                    {
                        datas = datas + ",('" + row[0].ToString() + "','" + Convert.ToDateTime(row[1].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(row[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','" + row[3].ToString() + "','" + row[4].ToString() + "')";
                    }
                    updateQueries += "UPDATE equipment_list SET equipment_list.current_status = '2' WHERE equipment_list.equipment_id = '" + row[0].ToString() + "';\n";
                    i++;
                }
                string res = DBManager.InsertIntoMaintainanceRequest(datas);
                if (!res.Contains("Success"))
                {
                    myMessageBox mm = new myMessageBox("Invalid Data Type found. Check your data again.", "Error", "OK", 1);
                    mm.ShowDialog();

                    //MessageBox.Show("Invalid Data Type Found. Check your data again", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    DBManager.UpdateDatabaseQuery(updateQueries);
                    myMessageBox mm = new myMessageBox("Data added Successfully", "Success", "OK", 0);
                    mm.ShowDialog();

                    //MessageBox.Show("Data Added Succesfully", "Successfully Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

    }
}