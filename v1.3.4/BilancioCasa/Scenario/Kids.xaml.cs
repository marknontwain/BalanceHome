using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BilancioCasa.Scenario
{
    /// <summary>
    /// Logica di interazione per Kids.xaml
    /// </summary>
    public partial class Kids : UserControl
    {
        public static Kids main;
        public List<ButtonMenulist> menu1;
        database db;
        public Kids()
        {
            InitializeComponent();
            main = this;
            combobox.SelectedItem = combobox.Items.GetItemAt(0);
            string[] array1 = { "Recent...", "File" };
            menu1 = new List<ButtonMenulist>();
            CreateMenu(array1, menu1);
            lbl1.ContextMenu.ItemsSource = menu1;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        public void CreateMenu(string[] test, List<ButtonMenulist> prova)
        {
            foreach (string s in test)
            {
                prova.Add(new ButtonMenulist() { NameList = s });
            }
        }
        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lbl1.ContextMenu.PlacementTarget = sender as UIElement;
            lbl1.ContextMenu.Visibility = Visibility.Visible;
            lbl1.ContextMenu.IsOpen = true;
        }
        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int caretindex = txt.CaretIndex;
            txt.Text = txt.Text.Replace(" ", "");
            if (caretindex == 1)
                if (txt.Text == "0")
                    txt.Text = txt.Text.Replace("0", "");
            txt.CaretIndex = caretindex;
        }
        private void txt2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int caretindex = txt.CaretIndex;
            txt.Text = txt.Text.Replace(" ", "");
            txt.CaretIndex = caretindex;
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && (!string.IsNullOrEmpty(txt1.Text) || !string.IsNullOrEmpty(txt2.Text)) && rd1.IsChecked != false || (rd2.IsChecked != false && combo2.SelectedItem != null))
            {
                MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db = new database();
                    string value2 = "";
                    string value = Title.Content.ToString();
                    string bill = txt1.Text + "." + txt2.Text;
                    if (rd1.IsChecked == true) value2 = "0";
                    else if (rd2.IsChecked == true)
                    {
                        value2 = (combo2.SelectedItem as Familiar).KeyId.ToString();
                    }
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                    db.CreateKids(ultdata, bill, value, txt3.Text, combobox.Text, value2);
                }
                    db = null;
            }
            else
            {
                if (date.GetValue(DatePicker.SelectedDateProperty) != null && !string.IsNullOrEmpty(txt1.Text) && rd2.IsChecked != false && combo2.SelectedItem == null)
                {
                    if (combo2.Items.Count > 0)
                        MessageBox.Show("Please select member of family", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                        MessageBox.Show("Please select Family (Generic) or add familiar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                    MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }    
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            db = new database();
            db.RetriveFamily(combo2);
            db = null;
        }
        private void ClearAllFields()
        {
            date.ClearValue(DatePicker.SelectedDateProperty);
            txt1.Clear();
            txt2.Clear();
            combobox.SelectedIndex = 0;
            rd1.IsChecked = false;
            rd2.IsChecked = false;
            combo2.SelectedIndex = -1;
            combo2.IsEnabled = false;
            txt3.Clear();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }

        private void rd2_Checked(object sender, RoutedEventArgs e)
        {
            combo2.IsEnabled = true;
        }

        private void rd1_Checked(object sender, RoutedEventArgs e)
        {
            combo2.IsEnabled = false;
            combo2.SelectedIndex = -1;
        }
    }
}
