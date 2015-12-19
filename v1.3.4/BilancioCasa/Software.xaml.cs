using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using BilancioCasa.Scenario;


namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per Software.xaml
    /// </summary>
    public partial class Software : Window 
    {
        public List<ButtonMenulist> menu1;
        public List<ButtonMenulist> menu2;
        public List<ButtonMenulist> menu3;
        public List<ButtonMenulist> menu4;
        public List<ButtonMenulist> menu5;
        public List<ButtonMenulist> menu6;
        public List<ButtonMenulist> menu7;
        public List<ButtonMenulist> menu8;

        public static CustomBool iscreatedTravel = new CustomBool(1);
        public static CustomBool iscreatedKids = new CustomBool(2);
        public static CustomBool iscreatedAnimal = new CustomBool(3);
        public static CustomBool iscreatedVehicle = new CustomBool(4);
        public static CustomBool iscreatedBill = new CustomBool(5);
        public static CustomBool iscreatedPersonal = new CustomBool(6);
        public static CustomBool iscreatedHouseGen = new CustomBool(7);
        public static CustomBool iscreatedFood = new CustomBool(8);
        public static CustomBool iscreatedHealth = new CustomBool(9);

        public List<CustomBool> ListBools = new List<CustomBool> { iscreatedTravel, iscreatedKids, iscreatedAnimal, iscreatedVehicle, iscreatedBill, iscreatedPersonal, iscreatedHouseGen, iscreatedFood, iscreatedHealth };

        public string TitleContent { get; set;}
        private bool IsExiting { get; set; }

        public static Software main;

        public Software()
        {
            InitializeComponent();
            main = this;

            string privileges;
            List<Button> listbtn = new List<Button>(){ Travel,Kids,Animals,Vehicles,Bill,PersSpen,HousGen,Health,Balancebtn,Optionsbtn};

            privileges = MainWindow.main.CurrentUser.Privileges;
            welcomelabel.Content = "Welcome " + MainWindow.main.CurrentUser.Name + "   (" + privileges + ")";

            CheckPrivs(listbtn,privileges);

            datelabel.Content = String.Format("{0:D}", DateTime.Now);
            menu1 = new List<ButtonMenulist>();
            menu2 = new List<ButtonMenulist>();
            menu3 = new List<ButtonMenulist>();
            menu4 = new List<ButtonMenulist>();
            menu5 = new List<ButtonMenulist>();
            menu6 = new List<ButtonMenulist>();
            menu7 = new List<ButtonMenulist>();
            menu8 = new List<ButtonMenulist>();
            string[] array1 = { "Sport","Shopping","Go out","Hobby"};
            string[] array2 = { "Lease", "Water", "Gas", "Electricity", "Telephony", "Condomin." };
            string[] array3 = { "Food", "Furniture", "Appliance", "Rep. & Ser." };
            string[] array4 = { "Books", "Games"};
            string[] array5 = { "Body Cure", "Medicine", "Medic. Exam" };
            string[] array6 = { "Ticket", "Food & Lod.","Spending"};
            string[] array7 = { "Animal Feed","Health","Accessories" };
            string[] array8 = { "Repair", "Garage", "Purchase", "Propellant"};
            CreateMenu(array1, menu1);
            CreateMenuTooltip(array2, menu2,5,"Condominium");
            CreateMenuTooltip(array3, menu3,3,"Repair & Service");
            CreateMenu(array4, menu4);
            CreateMenuTooltip(array5, menu5,2,"Medical Exam");
            CreateMenuTooltip(array6, menu6,1,"Food & Lodging");
            CreateMenu(array7, menu7);
            CreateMenu(array8, menu8);

            PersSpen.ContextMenu.ItemsSource = menu1;
            Bill.ContextMenu.ItemsSource = menu2;
            HousGen.ContextMenu.ItemsSource = menu3;
            Kids.ContextMenu.ItemsSource = menu4;
            Health.ContextMenu.ItemsSource = menu5;
            Travel.ContextMenu.ItemsSource = menu6;
            Animals.ContextMenu.ItemsSource = menu7;
            Vehicles.ContextMenu.ItemsSource = menu8;
            grid2.Children.Add(new Home());
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { Registry.CurrentUser.DeleteSubKey("CookieBalanceHome"); }
            catch (Exception Exception) { ; }

            Application.Current.MainWindow = this;
            MainWindow main = new MainWindow();
            main.Show();
            IsExiting = true;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(MainWindow))
                    window.Close();
            }
        }

        public void CreateMenu(string[] test, List<ButtonMenulist> prova)
        {
            foreach(string s in test)
            {
                prova.Add(new ButtonMenulist() { NameList = s});
            }
        }
        public void CreateMenuTooltip(string[] test, List<ButtonMenulist> prova, int index, string tool)
        {
            int i = -1;
            foreach (string s in test)
            {
                i++;
                if(i == index)
                {
                    prova.Add(new ButtonMenulist() { NameList = s,Tooltip =tool});                  
                }
                else
                prova.Add(new ButtonMenulist() { NameList = s});
            }
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            if(!Options.iscreated)
            {
                Options opt = new Options();
                opt.Show();
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            PersSpen.ContextMenu.PlacementTarget = sender as UIElement;
            PersSpen.ContextMenu.Visibility = Visibility.Visible;
            PersSpen.ContextMenu.IsOpen = true;
        }

        private void btn1_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btn1_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Bill_Click(object sender, RoutedEventArgs e)
        {
            Bill.ContextMenu.PlacementTarget = sender as UIElement;
            Bill.ContextMenu.Visibility = Visibility.Visible;
            Bill.ContextMenu.IsOpen = true;
        }

        private void Bill_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void HousGen_Click(object sender, RoutedEventArgs e)
        {
            HousGen.ContextMenu.PlacementTarget = sender as UIElement;
            HousGen.ContextMenu.Visibility = Visibility.Visible;
            HousGen.ContextMenu.IsOpen = true;
        }

        private void HousGen_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void HousGen_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Kids_Click(object sender, RoutedEventArgs e)
        {
            Kids.ContextMenu.PlacementTarget = sender as UIElement;
            Kids.ContextMenu.Visibility = Visibility.Visible;
            Kids.ContextMenu.IsOpen = true;
        }

        private void Kids_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Kids_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Health_Click(object sender, RoutedEventArgs e)
        {
            Health.ContextMenu.PlacementTarget = sender as UIElement;
            Health.ContextMenu.Visibility = Visibility.Visible;
            Health.ContextMenu.IsOpen = true;
        }

        private void Health_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Health_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Travel_Click(object sender, RoutedEventArgs e)
        {
            Travel.ContextMenu.PlacementTarget = sender as UIElement;
            Travel.ContextMenu.Visibility = Visibility.Visible;
            Travel.ContextMenu.IsOpen = true;
        }

        private void Travel_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Travel_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Animals_Click(object sender, RoutedEventArgs e)
        {
            Animals.ContextMenu.PlacementTarget = sender as UIElement;
            Animals.ContextMenu.Visibility = Visibility.Visible;
            Animals.ContextMenu.IsOpen = true;
        }

        private void Animals_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Animals_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Bill_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Vehicles_Click(object sender, RoutedEventArgs e)
        {
            Vehicles.ContextMenu.PlacementTarget = sender as UIElement;
            Vehicles.ContextMenu.Visibility = Visibility.Visible;
            Vehicles.ContextMenu.IsOpen = true;
        }

        private void Vehicles_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Vehicles_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            grid2.Children.Clear();
            grid2.Children.Add(new Balance());

            for(int i =0;i< ListBools.Count;i++)
            {
                ListBools[i].Bool = false;
            }
        }

        private void CheckPrivs(List<Button> list, string privs)
        {
            switch (privs)
            {
                case "Admin":
                    break;
                case "User":
                    list[9].IsEnabled = false;
                    break;
                case "Visitor":
                    for(int i = 0; i < list.Count; i ++)
                    {
                        if(i != 8)
                        {
                            list[i].IsEnabled = false;
                            continue;
                        }
                    }
                    break;
                case "Null":
                    for(int i = 0; i < list.Count; i++)
                    {
                        list[i].IsEnabled = false;
                    }
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!IsExiting)
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid2.Children.Clear();
            grid2.Children.Add(new Home());
        }
    }




    public class CustomBool
    {
        public bool Bool { get; set; }
        public int ID { get; set; }

        public CustomBool(int id)
        {
            Bool = false;
            ID = id;
        }
    }

    public class ButtonMenulist
    {
        public string NameList { get; set; }
        public string Tooltip { get; set; }
    }

    public class DataItem : ICloneable
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public List<string> FamilyM { get; set; }
        public int isaccount { get; set; }
        public string KeyID { get; set; }
        public DataItem()
        {
            FamilyM = new List<string>() { "Father", "Mother", "Son", "Daughter", "GrandFather", "GrandMother" };
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
    public class UserItem : ICloneable
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Privileges { get; set; }
        public string Email { get; set; }
        public string KeyID { get; set; }
        public List<string> FamilyM { get; set; }
        public List<string> PrivU { get; set; }
        public UserItem()
        {
            FamilyM = new List<string>() { "Father", "Mother", "Son", "Daughter", "GrandFather", "GrandMother" };
            PrivU = new List<string>() { "Admin", "User", "Visitor", "Null" };
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class CenterItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
    public class LocationItem
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public LocationItem()
        {

        }
    }
    public class ImportItem : ICloneable
    {
        public string Import { get; set; }
        public string Date { get; set; }
        public string KeyID { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Familiar : ICloneable
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string KeyId { get; set; }
        public override string ToString()
        {
            if (this.Role != null)
                return this.Role + ", " + this.Name;
            else
                return this.Name;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class CustomListViewItem
    {
        public string Content { get; set; }
        public bool ViewAll { get; set; }
        public CustomListViewItem()
        {
            ViewAll = false;
        }
    }
}
