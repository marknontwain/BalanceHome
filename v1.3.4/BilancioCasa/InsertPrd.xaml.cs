using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per InsertPrd.xaml
    /// </summary>
    public partial class InsertPrd : Window
    {
        private bool ModifiyingDetails { get; set; }
        private ObservableCollection<Item> listprd { get; set; }
        DataGridColumn thisgridcol { get; set; }
        CustomBool mybol { get; set; }
        string Category { get; set; }
        string KeyId { get; set; }

        database db;

        public InsertPrd(ComboBox cb, ObservableCollection<Item> listmy)
        {
            this.DataContext = this;
            InitializeComponent();
            listprd = new ObservableCollection<Item>();
            listprd = listmy;
            cb.ItemsSource = listmy;
            lst.ItemsSource = listprd;
        }
        public InsertPrd(ObservableCollection<Item> listmy,DataGridColumn dt,CustomBool mybool,string cat,string keyid)
        {
            this.DataContext = this;
            InitializeComponent();
            listprd = new ObservableCollection<Item>();
            db = new database();
            listprd = listmy;
            lst.ItemsSource = listprd;
            ModifiyingDetails = true;
            thisgridcol = dt;
            mybol = mybool;
            Category = cat;
            KeyId = keyid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(name.Text) )
            {
                string prc = "";
                if (!string.IsNullOrEmpty(txt2.Text))
                {
                    if (string.IsNullOrEmpty(txt1.Text))
                        prc = 0 + "." + txt2.Text;
                    else
                    prc = txt1.Text + "." + txt2.Text;
                }
                else
                {
                    if (string.IsNullOrEmpty(txt1.Text))
                        prc = "0";
                    else
                    prc = txt1.Text;
                }
                listprd.Add(new Item { name = name.Text, price = prc });

                if (ModifiyingDetails)
                {
                    db.AddProduct(Category, KeyId, new Item { name = name.Text, price = prc });
                }
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int caretindex = txt.CaretIndex;
            txt.Text = txt.Text.Replace(" ", "");
            txt.CaretIndex = caretindex;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    if(ModifiyingDetails)
                    {
                        for (int i = lst.SelectedItems.Count - 1; i >= 0; i--)
                        {
                            Item myitem = lst.SelectedItems[i] as Item;
                            listprd.Remove(myitem);
                            db.DeleteProduct(myitem.Category,myitem.KeyId);
                        }
                    }
                    else
                        for (int i = lst.SelectedItems.Count - 1; i >= 0; i--)
                        {
                            listprd.Remove(lst.SelectedItems[i] as Item);
                        }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ModifiyingDetails)
            {
                mybol.Bool = false;
                thisgridcol.IsReadOnly = true;
            }
        }
    }
    public class Item
    {
        public string name { get; set; }
        public string price { get; set; }
        public string KeyId { get; set; }
        public string Category { get; set; }
        public Item()
        {          
        }
        public override string ToString()
        {
            return "Name: "+ name +"  Price:"+ price;
        }
    }
}
