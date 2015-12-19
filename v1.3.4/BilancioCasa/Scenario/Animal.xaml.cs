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
    /// Logica di interazione per Animal.xaml
    /// </summary>
    public partial class Animal : UserControl
    {
        public static Animal main;
        public List<ButtonMenulist> menu1;
        private ObservableCollection<Item> Listprd;
        database db;
        public bool isaccessories { get; set; }
        public Animal()
        {
            InitializeComponent();
            main = this;
            combobox.SelectedItem = combobox.Items.GetItemAt(0);
            string[] array1 = { "Recent...", "File" };
            menu1 = new List<ButtonMenulist>();
            CreateMenu(array1, menu1);
            lbl1.ContextMenu.ItemsSource = menu1;
        }

        private void Label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (label.Content == "Medicine")
            {
                grp1.Visibility = Visibility.Hidden;
                grp2.Visibility = Visibility.Visible;
                label.Content = "Medic Center";
            }
            else
            {
                grp2.Visibility = Visibility.Hidden;
                grp1.Visibility = Visibility.Visible;
                label.Content = "Medicine";
            }
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
            if(isaccessories)
            {
                Listprd = new ObservableCollection<Item>();
                grp1.Visibility = Visibility.Hidden;
                grp2.Visibility = Visibility.Visible;
                grp2.Header = "Products";
                insertbtn.Content = "Insert Products";
                label.IsEnabled = false;
                label.Visibility = Visibility.Hidden;
                ScrollViewer sv = new ScrollViewer();
                sv.Content = combobox2;
                combobox2.MaxDropDownHeight = 70;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else if (Title.Content=="Health")
            {
                Listprd = new ObservableCollection<Item>();
                db = new database();
                db.RetriveCenter(combo1,"Health");
                db = null;
                ScrollViewer sv = new ScrollViewer();
                sv.Content = combobox2;
                combobox2.MaxDropDownHeight = 70;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
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
            if (isaccessories)
            {
                MakeAccessories();
            }
            else if(Title.Content == "Health")
            {
                MakeHealht();
            }
            //else MakeFeed();
        }
        private void MakeHealht()
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && !string.IsNullOrEmpty(txt1.Text) && combo1.SelectedIndex >-1)
            {
                MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db = new database();
                    string value = "Health";
                    string bill = txt1.Text + "." + txt2.Text;
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                    var centerm = "";
                    if (combo1.SelectedIndex > -1)
                        centerm = combo1.SelectedItem.ToString();

                    db.CreateAnimalCenter(ultdata, bill, value, txt3.Text, combobox.Text, centerm,Listprd);

                    db = null;
                }
            }
            else
            {
                if (date.GetValue(DatePicker.SelectedDateProperty) != null && !string.IsNullOrEmpty(txt1.Text) && combo1.SelectedIndex ==-1)
                {
                    MessageBox.Show("Plese insert medic center", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                    MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MakeAccessories()
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && !string.IsNullOrEmpty(txt1.Text))
            {
                MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db = new database();
                    string value = "Accessories";
                    string bill = txt1.Text + "." + txt2.Text;
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                    db.CreateAnimalPrd(ultdata, bill, value, txt3.Text, combobox.Text,Listprd);

                    db = null;
                }
            }
            else
            {
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool alreadyexist = false;
            string itembox = "";
            if (combo1.Text != "Insert/Select Center" && !string.IsNullOrEmpty(combo1.Text) && !string.IsNullOrWhiteSpace(combo1.Text))
            {
                foreach (var item in combo1.Items)
                {
                    if (item == combo1.Text)
                    {
                        alreadyexist = true;
                        combo1.SelectedIndex = combo1.Items.IndexOf(item);
                        return;
                    }
                }
                if (!alreadyexist)
                {
                    itembox = combo1.Text;
                    combo1.SelectedIndex = combo1.Items.Add(itembox);
                }
            }
            else if (string.IsNullOrEmpty(combo1.Text) || string.IsNullOrWhiteSpace(combo1.Text))
                MessageBox.Show("Can't insert blank center", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void insertbtn_Click(object sender, RoutedEventArgs e)
        {
            InsertPrd prd = new InsertPrd(combobox2, Listprd);
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
            if (!isaccessories)
            {
                combo1.SelectedIndex = -1;
                combo1.Text = "Insert/Select Center";
            }
            Listprd.Clear();
            combobox2.ItemsSource = null;
        }
    }
}
