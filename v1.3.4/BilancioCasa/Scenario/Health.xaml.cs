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
    /// Logica di interazione per Health.xaml
    /// </summary>
    public partial class Health : UserControl
    {
        public static Health main;
        public List<ButtonMenulist> menu1;
        private ObservableCollection<Item> Listprd;
        database db;
        public Health()
        {
            InitializeComponent();
            main = this;
            combobox.SelectedItem = combobox.Items.GetItemAt(0);
            string[] array1 = { "Recent...", "File" };
            menu1 = new List<ButtonMenulist>();
            CreateMenu(array1, menu1);
            lbl1.ContextMenu.ItemsSource = menu1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            db = new database();
            db.RetriveFamily(combo2);

            if (Title.Content != "Medic. Exam")
            {
                Listprd = new ObservableCollection<Item>();
                if (Title.Content == "Body Cure")
                {
                    grp2.Visibility = Visibility.Hidden;
                    grp1.Visibility = Visibility.Visible;
                    grp1.Header = "Center";
                    db.RetriveCenter(combo1, "Body Cure");
                }
            }
            else
            {
                grp2.Visibility = Visibility.Hidden;
                grp1.Visibility = Visibility.Visible;
                grp1.Header = "Medic. Center";
                db.RetriveCenter(combo1, "Medic. Exam");
            }

            db = null;
        }

        private void label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            if (Title.Content == "Body Cure")
            {
                if (label.Content == "Center")
                {
                    label.Content = "Person Con...";
                    grp3.Visibility = Visibility.Hidden;
                    grp1.Visibility = Visibility.Visible;
                }
                else
                {
                    label.Content = "Center";
                    grp1.Visibility = Visibility.Hidden;
                    grp3.Visibility = Visibility.Visible;
                }
            }
            else if (Title.Content == "Medic. Exam")
            {
                if (label.Content == "Medic. Center")
                {
                    label.Content = "Person Con...";
                    grp3.Visibility = Visibility.Hidden;
                    grp1.Visibility = Visibility.Visible;
                }
                else
                {
                    label.Content = "Medic. Center";
                    grp1.Visibility = Visibility.Hidden;
                    grp3.Visibility = Visibility.Visible;
                }
            }
            else if (Title.Content == "Medicine")
            {
                if (label.Content == "Products")
                {
                    label.Content = "Person Con...";
                    grp3.Visibility = Visibility.Hidden;
                    grp2.Visibility = Visibility.Visible;
                }
                else
                {
                    label.Content = "Products";
                    grp2.Visibility = Visibility.Hidden;
                    grp3.Visibility = Visibility.Visible;
                }
            }
        }
        public void CreateMenu(string[] test, List<ButtonMenulist> prova)
        {
            foreach (string s in test)
            {
                prova.Add(new ButtonMenulist() { NameList = s });
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
            if (Title.Content == "Medicine")
            {
                MakePrd();
            }
            else
                MakeHealth();
        }

        private void MakeHealth()
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && (!string.IsNullOrEmpty(txt1.Text) || !string.IsNullOrEmpty(txt2.Text)))
            {
                if(rd1.IsChecked == true || (rd2.IsChecked == true && combo2.SelectedIndex >-1))
                {
                    if (combo1.Items.Count > 0 && combo1.SelectedIndex > -1)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            db = new database();
                            string value = Title.Content.ToString();

                            string bill = txt1.Text + "." + txt2.Text;
                            string data = date.Text;
                            string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                            string value2 = "";
                            if (rd1.IsChecked == true) value2 = "0";
                            else if (rd2.IsChecked == true)
                            {
                                value2 = (combo2.SelectedItem as Familiar).KeyId.ToString();
                            }
                            db.CreateHealthCenter(ultdata, bill, value, txt3.Text, combobox.Text, combo1.SelectedItem.ToString(),value2);

                            db = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Plese insert medic center", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else MessageBox.Show("Plese specify person concerned", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void MakePrd()
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && (!string.IsNullOrEmpty(txt1.Text) || !string.IsNullOrEmpty(txt2.Text)))
            {
                if (rd1.IsChecked == true || (rd2.IsChecked == true && combo2.SelectedIndex > -1))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        db = new database();
                        string value = "Medicine";
                        string bill = txt1.Text + "." + txt2.Text;
                        string data = date.Text;
                        string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");


                        string value2 = "";
                        if (rd1.IsChecked == true) value2 = "0";
                        else if (rd2.IsChecked == true)
                        {
                            if (combo2.SelectedItem.ToString() == "Me")
                            {
                                value2 = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                            }
                            value2 = (combo2.SelectedItem as Familiar).KeyId.ToString();
                        }
                        db.CreateHealthPrd(ultdata, bill, value, txt3.Text, combobox.Text,value2, Listprd);
                        
                        db = null;
                    }
                }
                else MessageBox.Show("Plese specify person concerned", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
            if(Title.Content == "Medicine")
            {
                combobox2.ItemsSource = null;
                Listprd.Clear();
            }
            else
            {
                combo1.SelectedIndex = -1;
                combo1.Text = "Insert/Select Center";
            }
        }

        private void rd2_Checked(object sender, RoutedEventArgs e)
        {
            if (!combo2.IsEnabled)
                combo2.IsEnabled = true;
        }

        private void rd1_Checked(object sender, RoutedEventArgs e)
        {
            if (combo2.IsEnabled)
            {
                combo2.IsEnabled = false;
                combo2.SelectedIndex = -1;
            }
        }
    }
}
