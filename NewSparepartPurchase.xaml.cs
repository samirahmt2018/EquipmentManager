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
    /// Interaction logic for NewSparepartPurchase.xaml
    /// </summary>
    public partial class NewSparepartPurchase : Window
    {
        DataTable values;
        public NewSparepartPurchase()
        {

            InitializeComponent();
            try
            {

                //  sparepart_purchase_request.ItemsSource = values.AsDataView();
                ExcelFileSelector fileS = new ExcelFileSelector();
                fileS.ShowDialog();
                values = DBManager.SelectFromExcel(fileS.fileName, fileS.sheetName, fileS.cellValues);

                sparepart_purchase_request.ItemsSource = values.AsDataView();
            }
            catch
            {

            }

        }

        private void sparepart_purchase_request_AutoGeneratedColumns(object sender, EventArgs e)
        {
            try
            {


                sparepart_purchase_request.Columns[0].Header = "Work Order Number";
                sparepart_purchase_request.Columns[1].Header = "Item Description";
                sparepart_purchase_request.Columns[2].Header = "Unit";
                sparepart_purchase_request.Columns[3].Header = "Quantity";
                


            }
            catch (Exception ex)
            {

                //this.Close();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (values == null || values.Columns.Count != 4)
            {
                this.Close();
                myMessageBox mm = new myMessageBox("Column Count Doesnt match or no data found", "Error", "OK", 1);
                mm.ShowDialog();

                //MessageBox.Show("Invalid Data size", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string datas = "";
            try
            {
                foreach (DataRow row in values.Rows)
                {
                    if (i == 0)
                    {
                        datas = "('" + row[0].ToString() + "','" + row[1].ToString() + "','" + row[2].ToString() + "','" + row[3].ToString() + "')";

                    }
                    else
                    {
                        datas = datas + ",('" + row[0].ToString() + "','" + row[1].ToString() + "','" + row[2].ToString() + "','" + row[3].ToString() +  "')";
                    }
                    i++;
                }
                string res = DBManager.InsertIntoSparepartPurchase(datas);
                if (!res.Contains("Success"))
                {
                    myMessageBox mm = new myMessageBox("Invalid Data Type Found. Check your data again.", "Error", "OK", 1);
                    mm.ShowDialog();

                    // MessageBox.Show("", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    myMessageBox mm = new myMessageBox("Data added successfully", "Success", "OK", 0);
                    mm.ShowDialog();

                    // MessageBox.Show("Data Added Succesfully", "Successfully Added", MessageBoxButton.OK, MessageBoxImage.Information);
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
