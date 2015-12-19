using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace BilancioCasa.Scenario
{
    /// <summary>
    /// Logica di interazione per HouseGen.xaml
    /// </summary>
    public partial class HouseGen : UserControl
    {
        public static HouseGen main;
        public List<ButtonMenulist> menu1;
        private ObservableCollection<Item> Listprd;
        database db;
        public bool isrepandservice { get; set; }
        public HouseGen()
        {
            InitializeComponent();
            main = this;
            combobox.SelectedItem = combobox.Items.GetItemAt(0);
            string[] array1 = { "Recent...", "File" };
            menu1 = new List<ButtonMenulist>();
            CreateMenu(array1, menu1);
            lbl1.ContextMenu.ItemsSource = menu1;
        }
        public void CreateMenu(string[] test, List<ButtonMenulist> prova)
        {
            foreach (string s in test)
            {
                prova.Add(new ButtonMenulist() { NameList = s });
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isrepandservice)
            {
                grp1.Visibility = Visibility.Visible;
                grp2.Visibility = Visibility.Hidden;
                db = new database();
                db.RetriveCenter(txtsel, "Rep. & Ser.");
                db = null;
            }
            else
                Listprd = new ObservableCollection<Item>();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && (!string.IsNullOrEmpty(txt1.Text) || !string.IsNullOrEmpty(txt2.Text)))
            {
                if (Title.Content != "Rep. & Ser.")
                {
                    MakeGenPrd();
                }
                else
                    MakeGenCenter();
            }
            else
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void MakeGenPrd()
        {
            MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result.Equals(MessageBoxResult.Yes))
            {
                db = new database();
                string value = Title.Content.ToString();
                string bill = txt1.Text + "." + txt2.Text;
                string data = date.Text;
                string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                db.CreateHouseGenPrd(ultdata, bill, value, txt3.Text, combobox.Text,Listprd);

                db = null;
                ultdata = null;
            }
        }
        private void MakeGenCenter()
        {
            if (txtsel.Items.Count > 0 && txtsel.SelectedIndex > -1)
            {
                MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db = new database();
                    string value = "Rep. & Ser.";

                    string bill = txt1.Text + "." + txt2.Text;
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                    db.CreateHouseGenCenter(ultdata, bill, value, txt3.Text, combobox.Text, txtsel.SelectedItem.ToString());
                    db = null;
                    ultdata = null;
                }
            }
            else
            {
                MessageBox.Show("Please select Service", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool alreadyexist = false;
            string itembox = "";
            if (txtsel.Text != "Insert/Select Service" && !string.IsNullOrEmpty(txtsel.Text) && !string.IsNullOrWhiteSpace(txtsel.Text))
            {
                foreach (var item in txtsel.Items)
                {
                    if (item == txtsel.Text)
                    {
                        alreadyexist = true;
                        txtsel.SelectedIndex = txtsel.Items.IndexOf(item);
                        return;
                    }
                }
                if (!alreadyexist)
                {
                    itembox = txtsel.Text;
                    txtsel.SelectedIndex = txtsel.Items.Add(itembox);
                }
            }
            else if (string.IsNullOrEmpty(txtsel.Text) || string.IsNullOrWhiteSpace(txtsel.Text))
                MessageBox.Show("Can't insert blank center", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InsertPrd prd = new InsertPrd(combo2, Listprd);
            prd.Show();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }
        private void ClearAllFields()
        {
            date.ClearValue(DatePicker.SelectedDateProperty);
            txt1.Clear();
            txt2.Clear();
            combobox.SelectedIndex = 0;
            txt3.Clear();
            if (isrepandservice)
            {
                txtsel.SelectedIndex = -1;
                txtsel.Text = "Insert/Select Service";
            }
            else
            {
                combo2.ItemsSource = null;
                Listprd.Clear();
            }
        }
        
    }
}
