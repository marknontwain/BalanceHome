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
    /// Logica di interazione per InsertBill.xaml
    /// </summary>
    public partial class InsertBill : Window
    {
        DataGridColumn gridcol { get; set; }
        CustomBool mybool { get; set; }
        private string OriginalBill { get; set; }
        private string Category { get; set; }
        private Object MyItem { get; set; }

        public InsertBill(string Bill, DataGridColumn dt, CustomBool bl, string category, Object item)
        {
            InitializeComponent();
            OriginalBill = Bill;
            FormatBill();
            gridcol = dt;
            mybool = bl;
            Category = category;
            MyItem = item;
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
        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int caretindex = txt.CaretIndex;
            txt.Text = txt.Text.Replace(" ", "");
            txt.CaretIndex = caretindex;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bill = txt1.Text;
            if (!string.IsNullOrEmpty(txt2.Text))
                bill += "," + txt2.Text;

            if (bill != OriginalBill)
            {
                MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    string newbill = bill.Replace(',', '.');
                    switch (Category)
                    {
                        case "travel":
                            IntDetails.main.db.UpdateTravel(IntDetails.main.datagrid, "bill", newbill, (TravelItem)MyItem);
                            ((TravelItem)MyItem).Bill = bill;
                            break;
                        case "children":
                            IntDetails.main.db.UpdateKid(IntDetails.main.datagrid, "bill", newbill, (KidItem)MyItem);
                            ((KidItem)MyItem).Bill = bill;
                            break;
                        case "animals":
                            IntDetails.main.db.UpdateAnimal(IntDetails.main.datagrid, "bill", newbill, (AnimalItem)MyItem);
                            ((AnimalItem)MyItem).Bill = bill;
                            break;
                        case "vehicles":
                            IntDetails.main.db.UpdateVehicle(IntDetails.main.datagrid, "bill", newbill, (VehicleItem)MyItem);
                            ((VehicleItem)MyItem).Bill = bill;
                            break;
                        case "billandlease":
                            IntDetails.main.db.UpdateBillandL(IntDetails.main.datagrid, "bill", newbill, (BLItem)MyItem);
                            ((BLItem)MyItem).Bill = bill;
                            break;
                        case "personalactivities":
                            IntDetails.main.db.UpdateSpenPS(IntDetails.main.datagrid, "bill", newbill, (PSItem)MyItem);
                            ((PSItem)MyItem).Bill = bill;
                            break;
                        case "housegen":
                            IntDetails.main.db.UpdateHouseGen(IntDetails.main.datagrid, "bill", newbill, (HouseItem)MyItem);
                            ((HouseItem)MyItem).Bill = bill;
                            break;
                        case "healthcare":
                            IntDetails.main.db.UpdateHealthC(IntDetails.main.datagrid, "bill", newbill, (HealthItem)MyItem);
                            ((HealthItem)MyItem).Bill = bill;
                            break;
                    }
                    mybool.Bool = false;
                    gridcol.IsReadOnly = true;
                    this.Close();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FormatBill();
        }

        private void FormatBill()
        {
            if (OriginalBill.Contains(','))
            {
                string[] partsbill = OriginalBill.Split(',');
                txt1.Text = partsbill[0];
                txt2.Text = partsbill[1];
            }
            else if (OriginalBill.Contains('.'))
            {
                string[] partsbill = OriginalBill.Split('.');
                txt1.Text = partsbill[0];
                txt2.Text = partsbill[1];
            }
            else
                txt1.Text = OriginalBill;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mybool.Bool = false;
            gridcol.IsReadOnly = true;
        }
    }
}
