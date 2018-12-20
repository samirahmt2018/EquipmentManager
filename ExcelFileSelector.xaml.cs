using Microsoft.Win32;
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
    /// Interaction logic for ExcelFileSelector.xaml
    /// </summary>
    public partial class ExcelFileSelector : Window
    {
        public string fileName { get; set; }
        public string sheetName { get; set; }
        public string cellValues { get; set; } 
        public ExcelFileSelector()
        {
            InitializeComponent();
            fileName = "";
            sheetName = "Sheet1";
            sheetBox.Text = "Sheet1";
            cellValues = "";
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
        
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                fileNameBox.Text = openFileDialog.FileName;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            fileName = fileNameBox.Text;
            sheetName = sheetBox.Text;
            cellValues = cellsBox.Text;
            this.Close();
        }
    }
}
