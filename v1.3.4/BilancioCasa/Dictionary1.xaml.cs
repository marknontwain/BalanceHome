using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Controls;
using System.Threading.Tasks;
using BilancioCasa.Scenario.Bill;
using BilancioCasa.Scenario.House_Gen;
using BilancioCasa.Scenario;
using Microsoft.Win32;
using System.IO;

namespace BilancioCasa
{
    public partial class Dictionary1 : ResourceDictionary
    {

        OpenFileDialog ofd;
        private string Whatcategory { get; set; }
        public Dictionary1()
        {

        }


        public void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (Software.iscreatedBill.Bool)
                CheckNote(b, Bill.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedKids.Bool)
                CheckNote(b, Kids.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedPersonal.Bool)
                CheckNote(b, PersonalActivities.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedFood.Bool)
                CheckNote(b, Food.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedAnimal.Bool)
                CheckNote(b, Animal.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedHealth.Bool)
                CheckNote(b, Health.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedHouseGen.Bool)
                CheckNote(b, HouseGen.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedTravel.Bool)
                CheckNote(b, Travel.main.txt3, Software.main.TitleContent);

            else if (Software.iscreatedVehicle.Bool)
                CheckNote(b, Service.main.txt3, Software.main.TitleContent);

            if (b.Content == "Lease" || b.Content == "Water" || b.Content == "Electricity" || b.Content == "Gas" || b.Content == "Condomin.")
            {
                CreateWindowBill(b.Content.ToString());        
                return;
            }
            else if (b.Content == "Sport" || b.Content == "Shopping" || b.Content == "Go out" || b.Content == "Hobby")
            {
                CreateWindowPersonal(b.Content.ToString());
                if(b.Content == "Shopping")
                    PersonalActivities.main.isshopping = true;
                return;
            }
            else if (b.Content == "Food")
            {
                CreateWindowHouseFood(b.Content.ToString());
                return;
            }
            else if(b.Content == "Health" || b.Content == "Accessories")
            {
                CreateWindowAnimal(b.Content.ToString());
                if (b.Content == "Accessories")
                    Animal.main.isaccessories = true;
                return;
            }
            else if (b.Content == "Furniture" || b.Content == "Appliance" || b.Content == "Rep. & Ser.")
            {
                CreateWindowHouseGen(b.Content.ToString());
                if (b.Content == "Rep. & Ser.")
                    HouseGen.main.isrepandservice = true;
                return;
            }
            else if (b.Content == "Repair" || b.Content == "Garage" || b.Content == "Purchase" || b.Content =="Propellant")
            {
                CreateWindowService(b.Content.ToString());
                return;
            }
            else if ( b.Content == "Animal Feed" || b.Content == "Telephony")
            {
                CreateWindowBill(b.Content.ToString());
                return;
            }
            else if (b.Content == "Body Cure" || b.Content == "Medicine" || b.Content == "Medic. Exam")
            {
                CreateWindowHealth(b.Content.ToString());
                return;
            }
            else if(b.Content =="Ticket" || b.Content == "Food & Lod." || b.Content == "Spending")
            {
                CreateWindowTravel(b.Content.ToString());
                return;
            }
            else if (b.Content == "Books" || b.Content == "Games")
            {
                CreateWindowKids(b.Content.ToString());
                return;
            }
        }

        public void CreateWindowBill(string wat)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Bill());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedBill);

            if (wat != "Lease" && wat != "Animal Feed" && wat != "Condomin.")
            {
                Bill.main.Title.Content += wat;
            }
            else if (wat == "Lease") Bill.main.Title.Content = "House's Lease";
            else if (wat == "Animal Feed") Bill.main.Title.Content = "Animal Feed";
            else if (wat == "Condomin.") Bill.main.Title.Content = "Condominium";

            if(wat != "AnimalFeed")
                Software.main.TitleContent = "billandlease";
            else Software.main.TitleContent = "animals";
        }
        public void CreateWindowPersonal(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new PersonalActivities());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedPersonal);
            PersonalActivities.main.Title.Content = w;

            Software.main.TitleContent = "personalactivities";
        }
        public void CreateWindowHouseFood(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Food());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedFood);

            Food.main.Title.Content = w;

            Software.main.TitleContent = "housegen";
        }
        public void CreateWindowAnimal(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Animal());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedAnimal);

            Animal.main.Title.Content = w;

            Software.main.TitleContent = "animals";
        }
        public void CreateWindowHouseGen(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new HouseGen());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedHouseGen);

            HouseGen.main.Title.Content = w;

            Software.main.TitleContent = "housegen";
        }
        public void CreateWindowHealth(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Health());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedHealth);

            Health.main.Title.Content = w;

            Software.main.TitleContent = "healthcare";
        }
        public void CreateWindowTravel(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Travel());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedTravel);

            Travel.main.Title.Content = w;

            Software.main.TitleContent = "travel";
        }
        public void CreateWindowKids(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Kids());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedKids);

            Kids.main.Title.Content = w;

            Software.main.TitleContent = "children";
        }
        public void CreateWindowService(string w)
        {
            Software.main.grid2.Children.Clear();
            Software.main.grid2.Children.Add(new Service());

            AllFalseExcept(Software.main.ListBools, Software.iscreatedVehicle);

            Service.main.Title.Content = w;

            Software.main.TitleContent = "vehicles";
        }

        public void CheckNote(Button b,TextBox txt,string category)
        {
            if (b.Content == "Recent...")
            {
                OpenRecent(b,category,txt);
                return;
            }
            else if (b.Content == "File")
            {
                OpenNote(b,txt);
                return;
            }
        }
        public void OpenRecent(Button b,string category,TextBox txt)
        {
            ViewNote viewnote = new ViewNote(category,txt);
            viewnote.ShowDialog();
        }
        public void OpenNote(Button b,TextBox txt3)
        {
            ofd = new OpenFileDialog() { Filter = "TXT Files (*.txt)|*txt" };
            var result = ofd.ShowDialog();
            if (result == true)
            {
                txt3.Text = File.ReadAllText(ofd.FileName);
            }
            ofd = null;
            result = null;
        }

        private void AllFalseExcept(List<CustomBool> list,CustomBool mybol)
        {
            mybol.Bool = true;
            for(int i = 0;i < list.Count; i++)
            {
                if (list[i].ID != mybol.ID)
                    list[i].Bool = false;
                else continue;
            }
        }
    }
    

}
