using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Equipment_Manager
{
    class myValidation
    {
        public static bool IsfloatNumberKey(Key inKey)
        {

            if (inKey == Key.D0 || inKey == Key.D1 || inKey == Key.D2 || inKey == Key.D3 || inKey == Key.D4 || inKey == Key.D5 || inKey == Key.D6 || inKey == Key.D7 || inKey == Key.D8 || inKey == Key.D9 || inKey == Key.OemPeriod || inKey == Key.Back || inKey == Key.NumPad0 || inKey == Key.NumPad1 || inKey == Key.NumPad2 || inKey == Key.NumPad3 || inKey == Key.NumPad4 || inKey == Key.NumPad5 || inKey == Key.NumPad6 || inKey == Key.NumPad7 || inKey == Key.NumPad8 || inKey == Key.NumPad9 || inKey == Key.Tab)
            {
                // MessageBox.Show(inKey.ToString(), (inKey < Keys.D0 || inKey > Keys.D9).ToString());
                return true;
            }
            // MessageBox.Show("Failed");
            return false;
        }
        public static bool IsIntegerNumberKey(Key inKey)
        {

            if (inKey == Key.D0 || inKey == Key.D1 || inKey == Key.D2 || inKey == Key.D3 || inKey == Key.D4 || inKey == Key.D5 || inKey == Key.D6 || inKey == Key.D7 || inKey == Key.D8 || inKey == Key.D9 || inKey == Key.Back || inKey == Key.NumPad0 || inKey == Key.NumPad1 || inKey == Key.NumPad2 || inKey == Key.NumPad3 || inKey == Key.NumPad4 || inKey == Key.NumPad5 || inKey == Key.NumPad6 || inKey == Key.NumPad7 || inKey == Key.NumPad8 || inKey == Key.NumPad9)
            {
                // MessageBox.Show(inKey.ToString(), (inKey < Keys.D0 || inKey > Keys.D9).ToString());
                return true;
            }
            // MessageBox.Show("Failed");
            return false;
        }
        public static bool IsBulkPhoneNumberKey(Key inKey)
        {

            if (inKey == Key.D0 || inKey == Key.D1 || inKey == Key.D2 || inKey == Key.D3 || inKey == Key.D4 || inKey == Key.D5 || inKey == Key.D6 || inKey == Key.D7 || inKey == Key.D8 || inKey == Key.D9 || inKey == Key.Back || inKey == Key.OemComma || inKey == Key.NumPad0 || inKey == Key.NumPad1 || inKey == Key.NumPad2 || inKey == Key.NumPad3 || inKey == Key.NumPad4 || inKey == Key.NumPad5 || inKey == Key.NumPad6 || inKey == Key.NumPad7 || inKey == Key.NumPad8 || inKey == Key.NumPad9 || inKey == Key.Tab || inKey == Key.OemPlus || inKey == Key.OemPlus || inKey == Key.Add || inKey == Key.LeftShift || inKey == Key.RightShift || inKey == Key.Multiply)
            {
                // MessageBox.Show(inKey.ToString(), (inKey < Keys.D0 || inKey > Keys.D9).ToString());
                return true;
            }
            // MessageBox.Show("Failed");
            return false;
        }
        public static bool IsPhoneNumberKey(Key inKey)
        {

            if (inKey == Key.D0 || inKey == Key.D1 || inKey == Key.D2 || inKey == Key.D3 || inKey == Key.D4 || inKey == Key.D5 || inKey == Key.D6 || inKey == Key.D7 || inKey == Key.D8 || inKey == Key.D9 || inKey == Key.Back || inKey == Key.NumPad0 || inKey == Key.NumPad1 || inKey == Key.NumPad2 || inKey == Key.NumPad3 || inKey == Key.NumPad4 || inKey == Key.NumPad5 || inKey == Key.NumPad6 || inKey == Key.NumPad7 || inKey == Key.NumPad8 || inKey == Key.NumPad9 || inKey == Key.Tab || inKey == Key.OemPlus || inKey == Key.Add || inKey == Key.LeftShift || inKey == Key.RightShift)
            {
                // MessageBox.Show(inKey.ToString(), (inKey < Keys.D0 || inKey > Keys.D9).ToString());
                return true;
            }
            // MessageBox.Show("Failed");
            return false;
        }

        public static bool IsEmpty(string Val)
        {
            if (Val == null || Val == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
