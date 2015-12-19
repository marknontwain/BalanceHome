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

namespace BilancioCasa
{

    public partial class ViewNote : Window
    {
        private TextBox mytxt { get; set; }
        public ViewNote(string category,TextBox txt)
        {
            InitializeComponent();
            database db = new database();
            db.RetriveNotes(mylistview,category);
            db = null;

            mytxt = txt;
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            mytxt.Text = ((CustomListViewItem)(item.DataContext)).Content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mylistview.SelectedIndex > -1)
            {
                CustomListViewItem item = mylistview.SelectedItem as CustomListViewItem;
                mytxt.Text = item.Content;
            }
        }
        private void listViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (e.Source is ContentPresenter)
            {
                ListViewItem item = sender as ListViewItem;
                if (!((CustomListViewItem)(item.DataContext)).ViewAll)
                {
                    item.Height = Double.NaN;
                    UpdateLayout();
                    if (item.ActualHeight <= 52) 
                        item.Height = 52;
                    ((CustomListViewItem)(item.DataContext)).ViewAll = true;
                }
                else
                {
                    item.Height = 52;
                    ((CustomListViewItem)(item.DataContext)).ViewAll = false;
                }
            }
        }
    }
}
