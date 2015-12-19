using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace BilancioCasa.Scenario
{
    /// <summary>
    /// Logica di interazione per PersonalActivities.xaml
    /// </summary>
    public partial class PersonalActivities : UserControl
    {
        public List<ButtonMenulist> menu1;
        public static PersonalActivities main;
        public bool isshopping { get; set; }
        database db;
        Label lab;
        GroupBox grp2;
        ComboBox combobox2;
        Button btn;
        private ObservableCollection<Item> Listprd;

        public PersonalActivities()
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lbl1.ContextMenu.PlacementTarget = sender as UIElement;
            lbl1.ContextMenu.Visibility = Visibility.Visible;
            lbl1.ContextMenu.IsOpen = true;
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
                    string value= Title.Content.ToString();

                    string bill = txt1.Text + "." + txt2.Text;
                    if (rd1.IsChecked == true) value2 = "0";
                    else if (rd2.IsChecked == true)
                    {
                        value2 = (combo2.SelectedItem as Familiar).KeyId.ToString();
                    }
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");

                    if (value != "Shopping")
                        db.CreatePersonal(ultdata, bill, value, txt3.Text, combobox.Text, value2);
                    else
                        db.CreatePersonalPrd(ultdata, bill, value, txt3.Text, combobox.Text, value2,Listprd); //shop overrid

                    db = null;
                }
            }
            else
            {
                if (date.GetValue(DatePicker.SelectedDateProperty) != null && !string.IsNullOrEmpty(txt1.Text) && rd2.IsChecked != false && combo2.SelectedItem == null)
                {
                    if(combo2.Items.Count > 0)
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

            if (isshopping)
            {
                lab = new Label();
                lab.Content = "Products";
                lab.FontSize = 10;
                lab.Margin = new Thickness(340, -25, 170, 380);

                var converter = new System.Windows.Media.BrushConverter();
                lab.Foreground = (Brush)converter.ConvertFromString("#FF6899C3");
                lab.Cursor = Cursors.Hand;
                lab.MouseLeftButtonDown += new MouseButtonEventHandler(Label2_MouseLeftButtonDown);
                this.grid1.Children.Add(lab);
                grp2 = new GroupBox();
                grp2.Header = "Products";
                grp2.Height = 60;
                grp2.Width = 314;
                grp2.FontSize = 15;
                grp2.Margin = new Thickness(281, -14, 0, 0);
                grp2.VerticalAlignment = VerticalAlignment.Top;
                grp2.HorizontalAlignment = HorizontalAlignment.Left;
                grp2.Foreground = (Brush)converter.ConvertFromString("#FF6899C3"); 
                Grid grid2 = new Grid();
                combobox2 = new ComboBox {VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, FontSize = 13, Opacity = 0.7, Width = 180, Height = 24, Margin = new Thickness(10, 6, 0, 0), Foreground = (Brush)converter.ConvertFromString("#FF8D8D8D") };
                grid2.Children.Add(combobox2);
                btn = new Button { Opacity = 0.7, FontSize = 12, Margin = new Thickness(201, 6, 10, 4), Content = "Insert Products" };
                btn.Click += InsertProducts;
                grid2.Children.Add(btn);
                grp2.Content = grid2;
                grp2.Visibility = Visibility.Hidden;
                this.grid1.Children.Add(grp2);

                Listprd = new ObservableCollection<Item>();
            }
        }
        private void Label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lab.Content == "Products")
            {
                lab.Content = "Person Con...";
                grp1.Visibility = Visibility.Hidden;
                grp2.Visibility = Visibility.Visible;
            }
            else
            {
                lab.Content = "Products";
                grp2.Visibility = Visibility.Hidden;
                grp1.Visibility = Visibility.Visible;
            }
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
        private void InsertProducts(object sender, RoutedEventArgs e)
        {
            InsertPrd prd = new InsertPrd(combobox2,Listprd);
            prd.Show();
        }
        private void ClearAllFields()
        {
            date.ClearValue(DatePicker.SelectedDateProperty);
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            rd1.IsChecked = false;
            rd2.IsChecked = false;
            combo2.SelectedIndex = -1;
            combo2.IsEnabled = false;
            if(isshopping)
            {
                combobox2.ItemsSource = null;
                Listprd.Clear();
            }
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
