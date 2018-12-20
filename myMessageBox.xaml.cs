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
    /// Interaction logic for myMessageBox.xaml
    /// </summary>
    public partial class myMessageBox : Window
    {
       
        public myMessageBox(string message, string windowTitle, string okButtonName, string cancelButtonName, int type)
        {
            InitializeComponent();
            messageLabel.Text = message;
            okButton.Content = okButtonName;
            cancelButton.Content = cancelButtonName;
            Title = windowTitle;
            changeColors(type);
        }
        public myMessageBox(string message, string windowTitle, string okButtonName, int type)
        {
            InitializeComponent();
            messageLabel.Text = message;
            okButton.Content = okButtonName;
            cancelButton.Visibility = Visibility.Hidden;
            Title = windowTitle;
            changeColors(type);
        }
        private void changeColors(int i)
        {
            switch (i)
            {
                case 0:
                    img_place.Source= new BitmapImage(new Uri("Images/Success.png", UriKind.Relative));
                    messageLabel.Foreground = Brushes.Green;
                    return;
                case 1:
                    img_place.Source = new BitmapImage(new Uri("Images/Error.png", UriKind.Relative));

                    messageLabel.Foreground = Brushes.Red;
                    return;
                case 2:
                    img_place.Source = new BitmapImage(new Uri("Images/Warning.png", UriKind.Relative));


                    messageLabel.Foreground = Brushes.Goldenrod;
                    return;
                default:

                    messageLabel.Foreground = Brushes.Black;
                    return;
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
