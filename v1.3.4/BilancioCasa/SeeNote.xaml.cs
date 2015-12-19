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
    /// <summary>
    /// Logica di interazione per SeeNote.xaml
    /// </summary>
    public partial class SeeNote : Window
    {
        DataGridColumn gridcol { get; set; }
        CustomBool mybool { get; set; }
        private string OriginalNote { get; set; }
        private string Category { get; set; }
        private Object MyItem { get; set; }

        public SeeNote(string Note,DataGridColumn dt,CustomBool bl,string category,Object item)
        {
            InitializeComponent();
            OriginalNote = Note;
            txt1.Text = Note;
            gridcol = dt;
            mybool = bl;
            Category = category;
            MyItem = item;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mybool.Bool = false;
            gridcol.IsReadOnly = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            txt1.Text = OriginalNote;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (txt1.Text != OriginalNote)
            {
                MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    switch (Category)
                    {
                        case "travel":
                            IntDetails.main.db.UpdateTravel(IntDetails.main.datagrid, "note", txt1.Text, (TravelItem)MyItem);
                            ((TravelItem)MyItem).Note = txt1.Text;
                            break;
                        case "children":
                            IntDetails.main.db.UpdateKid(IntDetails.main.datagrid, "note", txt1.Text, (KidItem)MyItem);
                            ((KidItem)MyItem).Note = txt1.Text;
                            break;
                        case "animals":
                            IntDetails.main.db.UpdateAnimal(IntDetails.main.datagrid, "note", txt1.Text, (AnimalItem)MyItem);
                            ((AnimalItem)MyItem).Note = txt1.Text;
                            break;
                        case "vehicles":
                            IntDetails.main.db.UpdateVehicle(IntDetails.main.datagrid, "note", txt1.Text, (VehicleItem)MyItem);
                            ((VehicleItem)MyItem).Note = txt1.Text;
                            break;
                        case "billandlease":
                            IntDetails.main.db.UpdateBillandL(IntDetails.main.datagrid, "note", txt1.Text, (BLItem)MyItem);
                            ((BLItem)MyItem).Note = txt1.Text;
                            break;
                        case "personalactivities":
                            IntDetails.main.db.UpdateSpenPS(IntDetails.main.datagrid, "note", txt1.Text, (PSItem)MyItem);
                            ((PSItem)MyItem).Note = txt1.Text;
                            break;
                        case "housegen":
                            IntDetails.main.db.UpdateHouseGen(IntDetails.main.datagrid, "note", txt1.Text, (HouseItem)MyItem);
                            ((HouseItem)MyItem).Note = txt1.Text;
                            break;
                        case "healthcare":
                            IntDetails.main.db.UpdateHealthC(IntDetails.main.datagrid, "note", txt1.Text, (HealthItem)MyItem);
                            ((HealthItem)MyItem).Note = txt1.Text;
                            break;
                    }
                    mybool.Bool = false;
                    gridcol.IsReadOnly = true;
                    this.Close();
                }
            }
        }
    }
}
