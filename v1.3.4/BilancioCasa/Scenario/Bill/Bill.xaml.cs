using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace BilancioCasa.Scenario.Bill
{
    /// <summary>
    /// Logica di interazione per Bill.xaml
    /// </summary>
    public partial class Bill : UserControl
    {
        public static Bill main;
        public List<ButtonMenulist> menu1;
        database db;
        public Bill()
        {
            InitializeComponent();
            main = this;
            string[] array1 = { "Recent...", "File"};
            menu1 = new List<ButtonMenulist>();
            CreateMenu(array1, menu1);
            lbl1.ContextMenu.ItemsSource = menu1;
            combobox.SelectedItem = combobox.Items.GetItemAt(0);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lbl1.ContextMenu.PlacementTarget = sender as UIElement;
            lbl1.ContextMenu.Visibility = Visibility.Visible;
            lbl1.ContextMenu.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && (!string.IsNullOrEmpty(txt1.Text) || !string.IsNullOrEmpty(txt2.Text)))
            {
                MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db = new database();
                    string value = Title.Content.ToString();
                    if (Title.Content.ToString().Contains("House"))
                    value = "House Lease";

                    string bill = txt1.Text + "." + txt2.Text;
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                    
                    if (value != "Animal Feed")
                        db.CreateBill(ultdata, bill, value, txt3.Text, combobox.Text);
                    else
                        db.CreateAnimal(ultdata, bill, value, txt3.Text, combobox.Text);

                    db = null;
                    ultdata = null;
                }
            }
            else
                MessageBox.Show("Please fill out default fields","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
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

        private void ClearAllFields()
        {
            date.ClearValue(DatePicker.SelectedDateProperty);
            txt1.Clear();
            txt2.Clear();
            combobox.SelectedIndex = 0;
            txt3.Clear();
        }
    }
}
