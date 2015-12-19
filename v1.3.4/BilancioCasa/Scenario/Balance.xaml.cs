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
    /// Logica di interazione per Balance.xaml
    /// </summary>
    public partial class Balance : UserControl
    {
        database db;
        DateTime today = DateTime.Today;
        List<RadioButton> list;
        public static Balance main;
        private string travelquery { get; set;}
        private string kidquery { get; set; }
        private string animalquery { get; set; }
        private string vehicquery { get; set; }
        private string bilandlquery { get; set; }
        private string spendpsquery { get; set; }
        private string housegenquery { get; set; }
        private string healthcarequery { get; set; }

        private string TitleWindow { get; set; }

        public Balance()
        {
            db = new database();
            InitializeComponent();
            main = this;

            list = new List<RadioButton>(){rd2,rd4,rd6,rd8,rd12,rd14,rd16}; // bl here

            if (!db.HaveFamily())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].IsEnabled = false;
                }
                list = new List<RadioButton> { rd1, rd3, rd5, rd7,rd11, rd13, rd15 }; //bl here
                for (int i = 0; i< list.Count;i ++)
                {
                    list[i].IsChecked = true;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            db.RetriveFamily(new List<ComboBox> { travelcombo1, kidcombo2, personalcombo6, healthcombo8 });
            db.RetriveCenter(animalcombo3, "Health");
            db.RetriveCenter(vehiclecombo4, new List<string>{"Repair","Garage","Purchase","Propellant"});
            db.RetriveCenter(housegencombo7, new List<string> { "Food", "Rep. & Ser." });
            db.RetriveCenter(healthcombo8, new List<string> { "Body Cure", "Medic. Exam" });
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo1.SelectedIndex == 0)
                db.Period("Today", new List<Label> { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10 }, today);

            else if (combo1.SelectedIndex == 1) 
                db.Period("Last Week", new List<Label> { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10 }, today);

            else if (combo1.SelectedIndex == 2)
                db.Period("Last Month", new List<Label> { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10 }, today);
        
            else if (combo1.SelectedIndex == 3)
                db.Period("Last 4 Months", new List<Label> { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10 }, today);
         
            else if (combo1.SelectedIndex == 4)
                db.Period("Last 8 Months", new List<Label> { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10 }, today);

            else if (combo1.SelectedIndex == 5)
                db.Period("Last Year", new List<Label> { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10 }, today);
                
        }
        #region PeriodCombobox 

        private string MakePeriod(database db, ComboBox box, List<Label> lab, string query, List<double> dob)
        {
            if (box.SelectedIndex == 0)
            {
                query = db.PeriodBillGen("Today", lab, query, dob, today);
            }
            else if (box.SelectedIndex == 1)
            {
                query = db.PeriodBillGen("Last Week", lab, query, dob, today);
            }
            else if (box.SelectedIndex == 2)
            {
                query = db.PeriodBillGen("Last Month", lab, query, dob, today);
            }
            else if (box.SelectedIndex == 3)
            {
                query = db.PeriodBillGen("Last 4 Months", lab, query, dob, today);
            }
            else if (box.SelectedIndex == 4)
            {
                query = db.PeriodBillGen("Last 8 Months", lab, query, dob, today);
            }
            else if (box.SelectedIndex == 5)
            {
                query = db.PeriodBillGen("Last Year", lab, query, dob, today);
            }
            return query;
        }
        private string MakePeriod(database db, ComboBox box, List<Label> lab, string query, List<double> dob,int index)
        {
            if (box.SelectedIndex == 0)
            {
                query = db.PeriodBillGen("Today", lab, query, dob, today,1);
            }
            else if (box.SelectedIndex == 1)
            {
                query = db.PeriodBillGen("Last Week", lab, query, dob, today,1);
            }
            else if (box.SelectedIndex == 2)
            {
                query = db.PeriodBillGen("Last Month", lab, query, dob, today,1);
            }
            else if (box.SelectedIndex == 3)
            {
                query = db.PeriodBillGen("Last 4 Months", lab, query, dob, today,1);
            }
            else if (box.SelectedIndex == 4)
            {
                query = db.PeriodBillGen("Last 8 Months", lab, query, dob, today,1);
            }
            else if (box.SelectedIndex == 5)
            {
                query = db.PeriodBillGen("Last Year", lab, query, dob, today,1);
            }
            return query;
        }
        #endregion


        #region FIRST TRAVEL
        private void combo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double a = 0;
            double x = 0;
            double c = 0;

            if (rd1.IsChecked == true)
            {
                string query = "SELECT bill FROM travel WHERE category='Ticket';";
                query += "SELECT bill FROM travel WHERE category='Food & Lod.';";
                query += "SELECT bill FROM travel WHERE category='Spending';";
                travelquery=MakePeriod(db, travelcombo2, new List<Label> { lbltrav1, lbltrav2, lbltrav3, lbltrav4 }, query, new List<double> { a, x, c });
            }
            else if (rd2.IsChecked == true && travelcombo1.SelectedIndex > -1)
            {
                string persconc = (travelcombo1.SelectedItem as Familiar).KeyId.ToString();
                if (persconc == "Me")
                {
                    persconc = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                }
                string query = db.PrepareQuery("SELECT bill FROM travel WHERE category='Ticket' and personconcerned='%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM travel WHERE category='Food & Lod.' and personconcerned='%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM travel WHERE category='Spending' and personconcerned='%s';", persconc);
                travelquery = MakePeriod(db, travelcombo2, new List<Label> { lbltrav1, lbltrav2, lbltrav3, lbltrav4 }, query, new List<double> { a, x, c });
            }        
        }
        private void rd1_Checked(object sender, RoutedEventArgs e)
        {
            if (travelcombo2.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM travel WHERE category='Ticket';";
                query += "SELECT bill FROM travel WHERE category='Food & Lod.';";
                query += "SELECT bill FROM travel WHERE category='Spending';";

                double a = 0;
                double x = 0;
                double c = 0;
                travelquery = MakePeriod(db, travelcombo2, new List<Label> { lbltrav1, lbltrav2, lbltrav3, lbltrav4 }, query, new List<double> { a, x, c });
            }

            if (travelcombo1.IsEnabled)
            {
                travelcombo1.IsEnabled = false;
                travelcombo1.SelectedIndex = -1;
            }
        }
        private void comb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (travelcombo2.SelectedIndex > -1 && rd2.IsChecked == true)
            {
                string persconc = (travelcombo1.SelectedItem as Familiar).KeyId.ToString();
                if (persconc == "Me")
                {
                    persconc = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                }
                string query = db.PrepareQuery("SELECT bill FROM travel WHERE category='Ticket' and personconcerned='%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM travel WHERE category='Food & Lod.' and personconcerned='%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM travel WHERE category='Spending' and personconcerned='%s';", persconc);

                double a = 0;
                double x = 0;
                double c = 0;
                travelquery = MakePeriod(db, travelcombo2, new List<Label> { lbltrav1, lbltrav2, lbltrav3, lbltrav4 }, query, new List<double> { a, x, c });
            }
        }
        private void rd2_Checked(object sender, RoutedEventArgs e)
        {
            travelcombo1.IsEnabled = true;
        }
        #endregion   


        #region SECOND CHILDREN
        private void combo3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double a = 0;
            double x = 0;

            if (rd3.IsChecked == true)
            {
                string query = "SELECT bill FROM children WHERE category='Books';";
                query += "SELECT bill FROM children WHERE category='Games';";

                kidquery=MakePeriod(db, kidcombo3, new List<Label> { lblchild1, lblchild2, lblchild3 }, query, new List<double> { a, x });
            }
            else if (rd4.IsChecked == true && kidcombo2.SelectedIndex > -1)
            {
                string persconc = (kidcombo2.SelectedItem as Familiar).KeyId.ToString();
                if (persconc == "Me")
                {
                    persconc = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                }
                string query = db.PrepareQuery("SELECT bill FROM children WHERE category='Books' and personconcerned='%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM children WHERE category='Games' and personconcerned='%s';", persconc);
                kidquery=MakePeriod(db, kidcombo3, new List<Label> { lblchild1, lblchild2, lblchild3 }, query, new List<double> { a, x });
            }
        }
        private void rd3_Checked(object sender, RoutedEventArgs e)
        {
            if (kidcombo3.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM children WHERE category='Books';";
                query += "SELECT bill FROM children WHERE category='Games';";

                double a = 0;
                double x = 0;

                kidquery=MakePeriod(db, kidcombo3, new List<Label> { lblchild1, lblchild2, lblchild3 }, query, new List<double> { a, x });
            }

            if (kidcombo2.IsEnabled)
            {
                kidcombo2.IsEnabled = false;
                kidcombo2.SelectedIndex = -1;
            }
        }
        private void comb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kidcombo3.SelectedIndex > -1 && rd4.IsChecked == true)
            {
                string persconc = (kidcombo2.SelectedItem as Familiar).KeyId.ToString();
                if (persconc == "Me")
                {
                    persconc = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                }
                string query = db.PrepareQuery("SELECT bill FROM children WHERE category='Books' and personconcerned='%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM children WHERE category='Games' and personconcerned='%s';", persconc);

                double a = 0;
                double x = 0;
                kidquery=MakePeriod(db, kidcombo3, new List<Label> { lblchild1, lblchild2, lblchild3 }, query, new List<double> { a, x });
            }
        }
        private void rd4_Checked(object sender, RoutedEventArgs e)
        {
            kidcombo2.IsEnabled = true;
        }

        #endregion


        #region THREE ANIMALS
        private void combo4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(rd5.IsChecked == true)
            {
                string query = "SELECT bill FROM Animals WHERE category='Animal Feed';";
                query += "SELECT bill FROM Animals WHERE category='Health';";
                query += "SELECT bill FROM Animals WHERE category='Accessories';";

                double a = 0;
                double x = 0;
                double c = 0;

                animalquery=MakePeriod(db, animalcombo4, new List<Label> { lblanim1, lblanim2, lblanim3, lblanim4 }, query, new List<double> { a, x, c });
            }
            else if(rd6.IsChecked == true && animalcombo3.SelectedIndex >-1)
            {
                string query = "SELECT bill FROM Animals WHERE category='Health';";

                string center = (animalcombo3.SelectedItem as CenterItem).Name;
                string retcenter= "SELECT keyid FROM centers where category='Health' and name='"+center+"';";

                query = db.MakeCenter(retcenter, query, "animals", "Health");
                double a = 0;
                double x = 0;
                double c = 0;
                animalquery=MakePeriod(db, animalcombo4, new List<Label> { lblanim1, lblanim2, lblanim3, lblanim4 }, query, new List<double> { a, x, c },1);
            }
        }
        private void rd5_Checked(object sender, RoutedEventArgs e)
        {
            if (animalcombo4.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM Animals WHERE category='Animal Feed';";
                query += "SELECT bill FROM Animals WHERE category='Health';";
                query += "SELECT bill FROM Animals WHERE category='Accessories';";

                double a = 0;
                double x = 0;
                double c = 0;

                animalquery = MakePeriod(db, animalcombo4, new List<Label> { lblanim1, lblanim2, lblanim3, lblanim4 }, query, new List<double> { a, x, c });
            }

            if (animalcombo3.IsEnabled)
            {
                animalcombo3.IsEnabled = false;
                animalcombo3.SelectedIndex = -1;
            }
        }
        private void comb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (animalcombo4.SelectedIndex > -1 && rd6.IsChecked == true)
            {
                string query = "SELECT bill FROM Animals WHERE category='Health';";

                string center = (animalcombo3.SelectedItem as CenterItem).Name;
                string retcenter = "SELECT keyid FROM centers where category='Health' and name='" + center + "';";

                query = db.MakeCenter(retcenter, query, "animals", "Health");

                double a = 0;
                double x = 0;
                double c = 0;
                animalquery = MakePeriod(db, animalcombo4, new List<Label> { lblanim1, lblanim2, lblanim3, lblanim4 }, query, new List<double> { a, x, c }, 1);
            }
        }
        private void rd6_Checked(object sender, RoutedEventArgs e)
        {
            animalcombo3.IsEnabled = true;
        }
        #endregion


        #region FOUR VEHICLES
        private void combo5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(rd7.IsChecked == true)
            {
                string query = "SELECT bill FROM vehicles WHERE category='Repair';";
                query += "SELECT bill FROM vehicles WHERE category='Garage';";
                query += "SELECT bill FROM vehicles WHERE category='Purchase';";
                query += "SELECT bill FROM vehicles WHERE category='Propellant';";

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;

                vehicquery=MakePeriod(db, vehiclecombo5, new List<Label> { lblvehic1, lblvehic2, lblvehic3, lblvehic4, lblvehic5 }, query, new List<double> { a, x, c, d });    
            }
            else if (rd8.IsChecked == true && vehiclecombo4.SelectedIndex > -1)
            {
                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;
                string center = (vehiclecombo4.SelectedItem as CenterItem).Name;
                string category = (vehiclecombo4.SelectedItem as CenterItem).Category;

                string query = "SELECT bill FROM vehicles WHERE category='" + category + "';";
                string retcenter = "SELECT keyid FROM centers where category='" + category + "' and name='" + center + "';";
                query = db.MakeCenter(retcenter, query, "vehicles", category);

                vehicquery = MakePeriod(db, vehiclecombo5, new List<Label> { lblvehic1, lblvehic2, lblvehic3, lblvehic4, lblvehic5 }, query, new List<double> { a, x, c, d });
            }
        }
        private void rd7_Checked(object sender, RoutedEventArgs e)
        {
            if (vehiclecombo5.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM vehicles WHERE category='Repair';";
                query += "SELECT bill FROM vehicles WHERE category='Garage';";
                query += "SELECT bill FROM vehicles WHERE category='Purchase';";
                query += "SELECT bill FROM vehicles WHERE category='Propellant';";

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;

                vehicquery = MakePeriod(db, vehiclecombo5, new List<Label> { lblvehic1, lblvehic2, lblvehic3, lblvehic4, lblvehic5 }, query, new List<double> { a, x, c, d });
            }

            if (vehiclecombo4.IsEnabled)
            {
                vehiclecombo4.IsEnabled = false;
                vehiclecombo4.SelectedIndex = -1;
            }
        }
        private void comb4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vehiclecombo5.SelectedIndex > -1 && rd8.IsChecked == true)
            {
                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;

                string center = (vehiclecombo4.SelectedItem as CenterItem).Name;
                string category = (vehiclecombo4.SelectedItem as CenterItem).Category;

                string query ="SELECT bill FROM vehicles WHERE category='"+category+"';";
                string retcenter = "SELECT keyid FROM centers where category='" + category + "' and name='" + center + "';";
                query = db.MakeCenter(retcenter, query, "vehicles", category);

                vehicquery = MakePeriod(db, vehiclecombo5, new List<Label> { lblvehic1, lblvehic2, lblvehic3, lblvehic4, lblvehic5 }, query, new List<double> { a, x, c, d });
            }
        }
        private void rd8_Checked(object sender, RoutedEventArgs e)
        {
            vehiclecombo4.IsEnabled = true;
        }
        #endregion


        #region FIVE BILL AND LEASE
        private void combo6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query = "SELECT bill FROM billandlease WHERE category='House Lease';";
            query += "SELECT bill FROM billandlease WHERE category='Bill Water';";
            query += "SELECT bill FROM billandlease WHERE category='Bill Gas';";
            query += "SELECT bill FROM billandlease WHERE category='Bill Electricity';";
            query += "SELECT bill FROM billandlease WHERE category='Bill Telephony';";
            query += "SELECT bill FROM billandlease WHERE category='Condominium';";

            double a = 0;
            double x = 0;
            double c = 0;
            double d = 0;
            double y = 0;
            double t = 0;

            bilandlquery=MakePeriod(db, billandlcombo6, new List<Label> { lblbill1, lblbill2, lblbill3, lblbill4, lblbill5, lblbill6, lblbill7 }, query, new List<double> { a, x, c, d, y, t });
        }
        private void rd9_Checked(object sender, RoutedEventArgs e)
        {
            if (billandlcombo6.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM billandlease WHERE category='House Lease';";
                query += "SELECT bill FROM billandlease WHERE category='Bill Water';";
                query += "SELECT bill FROM billandlease WHERE category='Bill Gas';";
                query += "SELECT bill FROM billandlease WHERE category='Bill Electricity';";
                query += "SELECT bill FROM billandlease WHERE category='Bill Telephony';";
                query += "SELECT bill FROM billandlease WHERE category='Condominium';";

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;
                double y = 0;
                double t = 0;

                bilandlquery = MakePeriod(db, billandlcombo6, new List<Label> { lblbill1, lblbill2, lblbill3, lblbill4, lblbill5, lblbill6, lblbill7 }, query, new List<double> { a, x, c, d, y, t });
            }
        }
        #endregion

       
        #region SIX PERSONALACTIVITIES
        private void combo7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double a = 0;
            double x = 0;
            double c = 0;
            double d = 0;

            if (rd11.IsChecked == true)
            {
                string query = "SELECT bill FROM personalactivities WHERE category='Sport';";
                query += "SELECT bill FROM personalactivities WHERE category='Shopping';";
                query += "SELECT bill FROM personalactivities WHERE category='Go out';";
                query += "SELECT bill FROM personalactivities WHERE category='Hobby';";

                spendpsquery=MakePeriod(db, personalcombo7, new List<Label> { lblpers1, lblpers2, lblpers3, lblpers4, lblpers5 }, query, new List<double> { a, x, c, d });
            }
            else if (rd12.IsChecked == true && personalcombo6.SelectedIndex > -1)
            {
                string persconc = (personalcombo6.SelectedItem as Familiar).KeyId.ToString();
                if (persconc == "Me")
                {
                    persconc = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                }
                string query = db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Sport' and personconcerned = '%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Shopping' and personconcerned = '%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Go Out' and personconcerned = '%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Hobby' and personconcerned = '%s';", persconc);

                spendpsquery = MakePeriod(db, personalcombo7, new List<Label> { lblpers1, lblpers2, lblpers3, lblpers4, lblpers5 }, query, new List<double> { a, x, c, d });
            }
        }
        private void rd11_Checked(object sender, RoutedEventArgs e)
        {
            if (personalcombo7.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM personalactivities WHERE category='Sport';";
                query += "SELECT bill FROM personalactivities WHERE category='Shopping';";
                query += "SELECT bill FROM personalactivities WHERE category='Go out';";
                query += "SELECT bill FROM personalactivities WHERE category='Hobby';";

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;

                spendpsquery = MakePeriod(db, personalcombo7, new List<Label> { lblpers1, lblpers2, lblpers3, lblpers4, lblpers5 }, query, new List<double> { a, x, c, d });
            }

            if (personalcombo6.IsEnabled)
            {
                personalcombo6.IsEnabled = false;
                personalcombo6.SelectedIndex = -1;
            }
        }
        private void comb6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (personalcombo7.SelectedIndex > -1 && rd12.IsChecked == true)
            {
                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;

                string persconc = (personalcombo6.SelectedItem as Familiar).KeyId.ToString();
                if (persconc == "Me")
                {
                    persconc = MainWindow.main.CurrentUser.Role + ", " + MainWindow.main.CurrentUser.Name;
                }
                string query = db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Sport' and personconcerned = '%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Shopping' and personconcerned = '%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Go out' and personconcerned = '%s';", persconc);
                query += db.PrepareQuery("SELECT bill FROM personalactivities WHERE category='Hobby' and personconcerned = '%s';", persconc);

                spendpsquery = MakePeriod(db, personalcombo7, new List<Label> { lblpers1, lblpers2, lblpers3, lblpers4, lblpers5 }, query, new List<double> { a, x, c, d });
            }
        }
        private void rd12_Checked(object sender, RoutedEventArgs e)
        {
            personalcombo6.IsEnabled = true;
        }
        #endregion


        #region SEVEN HOUSEGEN
        private void combo8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(rd13.IsChecked == true)
            {
                string query = "SELECT bill FROM housegen WHERE category='Food';";
                query += "SELECT bill FROM housegen WHERE category='Furniture';";
                query += "SELECT bill FROM housegen WHERE category='Appliance';";
                query += "SELECT bill FROM housegen WHERE category='Rep. & Ser.';";

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;

                housegenquery=MakePeriod(db, housegencombo8, new List<Label> { lblgen1, lblgen2, lblgen3, lblgen4, lblgen5 }, query, new List<double> { a, x, c, d });
            }
            else if (rd14.IsChecked == true && housegencombo7.SelectedIndex > -1)
            {
                string center = (housegencombo7.SelectedItem as CenterItem).Name;
                string category = (housegencombo7.SelectedItem as CenterItem).Category;

                string query = "SELECT bill FROM housegen WHERE category='"+category+"';";
                string retcenter = "SELECT keyid FROM centers where category='" + category + "' and name='" + center + "';";
                query = db.MakeCenter(retcenter, query, "housegen", category);

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;
                housegenquery = MakePeriod(db, housegencombo8, new List<Label> { lblgen1, lblgen2, lblgen3, lblgen4, lblgen5 }, query, new List<double> { a, x, c, d });
            }
        }
        private void rd13_Checked(object sender, RoutedEventArgs e)
        {
            if (housegencombo8.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM housegen WHERE category='Food';";
                query += "SELECT bill FROM housegen WHERE category='Furniture';";
                query += "SELECT bill FROM housegen WHERE category='Appliance';";
                query += "SELECT bill FROM housegen WHERE category='Rep. & Ser.';";

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;
                housegenquery = MakePeriod(db, housegencombo8, new List<Label> { lblgen1, lblgen2, lblgen3, lblgen4, lblgen5 }, query, new List<double> { a, x, c, d });
            }

            if (housegencombo7.IsEnabled)
            {
                housegencombo7.IsEnabled = false;
                housegencombo7.SelectedIndex = -1;
            }
        }
        private void rd14_Checked(object sender, RoutedEventArgs e)
        {
            housegencombo7.IsEnabled = true;
        }
        private void comb7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (housegencombo8.SelectedIndex > -1 && rd14.IsChecked == true)
            {
                string center = (housegencombo7.SelectedItem as CenterItem).Name;
                string category = (housegencombo7.SelectedItem as CenterItem).Category;

                string query = "SELECT bill FROM housegen WHERE category='" + category + "';";
                string retcenter = "SELECT keyid FROM centers where category='" + category + "' and name='" + center + "';";
                query = db.MakeCenter(retcenter, query, "housegen", category);

                double a = 0;
                double x = 0;
                double c = 0;
                double d = 0;
                housegenquery = MakePeriod(db, housegencombo8, new List<Label> { lblgen1, lblgen2, lblgen3, lblgen4, lblgen5 }, query, new List<double> { a, x, c, d });
            }
        }

        #endregion


        #region EIGHT HEALTHCARE
        private void combo9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double a = 0;
            double x = 0;
            double c = 0;

            if (rd15.IsChecked == true)
            {
                string query = "SELECT bill FROM healthcare WHERE category='Body Cure';";
                query += "SELECT bill FROM healthcare WHERE category='Medicine';";
                query += "SELECT bill FROM healthcare WHERE category='Medic. Exam';";

                healthcarequery = MakePeriod(db, healthcombo9, new List<Label> { lblhealth1, lblhealth2, lblhealth3, lblhealth4 }, query, new List<double> { a, x, c });
            }

            else if (rd16.IsChecked == true && healthcombo8.SelectedIndex > -1)
            {
                string query = "";

                if (healthcombo8.SelectedItem is Familiar)
                {
                    string persconc = (healthcombo8.SelectedItem as Familiar).KeyId;
                    query = db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Body Cure' and personconcerned='%s';", persconc);
                    query += db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Medicine' and personconcerned='%s';", persconc);
                    query += db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Medic. Exam' and personconcerned='%s';", persconc);
                }
                else if (healthcombo8.SelectedItem is CenterItem)
                {
                    string center = (healthcombo8.SelectedItem as CenterItem).Name;
                    string category = (healthcombo8.SelectedItem as CenterItem).Category;

                    query = "SELECT bill FROM healthcare WHERE category='" + category + "';";

                    string retcenter = "SELECT keyid FROM centers where category='" + category + "' and name='" + center + "';";
                    query = db.MakeCenter(retcenter, query, "healthcare", category);
                }

                healthcarequery = MakePeriod(db, healthcombo9, new List<Label> { lblhealth1, lblhealth2, lblhealth3, lblhealth4 }, query, new List<double> { a, x, c });
            }
        }
        private void rd15_Checked(object sender, RoutedEventArgs e)
        {
            if (healthcombo9.SelectedIndex > -1)
            {
                string query = "SELECT bill FROM healthcare WHERE category='Body Cure';";
                query += "SELECT bill FROM healthcare WHERE category='Medicine';";
                query += "SELECT bill FROM healthcare WHERE category='Medic. Exam';";

                double a = 0;
                double x = 0;
                double c = 0;

                healthcarequery = MakePeriod(db, healthcombo9, new List<Label> { lblhealth1, lblhealth2, lblhealth3, lblhealth4 }, query, new List<double> { a, x, c });
            }

            if (healthcombo8.IsEnabled)
            {
                healthcombo8.IsEnabled = false;
                healthcombo8.SelectedIndex = -1;
            }
        }
        private void comb8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (healthcombo9.SelectedIndex > -1 && rd16.IsChecked == true)
            {
                double a = 0;
                double x = 0;
                double c = 0;
                string query = "";

                if (healthcombo8.SelectedItem is Familiar)
                {
                    string persconc = (healthcombo8.SelectedItem as Familiar).KeyId;
                    query = db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Body Cure' and personconcerned='%s';", persconc);
                    query += db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Medicine' and personconcerned='%s';", persconc);
                    query += db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Medic. Exam' and personconcerned='%s';", persconc);
                }
                else if (healthcombo8.SelectedItem is CenterItem)
                {
                    string center = (healthcombo8.SelectedItem as CenterItem).Name;
                    string category = (healthcombo8.SelectedItem as CenterItem).Category;
                    query = db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Body Cure';");
                    query += db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Medicine';");
                    query += db.PrepareQuery("SELECT bill FROM healthcare WHERE category='Medic. Exam';");

                    string retcenter = "SELECT keyid FROM centers where category='" + category + "' and name='" + center + "';";

                    query = db.MakeCenter(retcenter, query, "healthcare", category);
                }
                healthcarequery = MakePeriod(db, healthcombo9, new List<Label> { lblhealth1, lblhealth2, lblhealth3, lblhealth4 }, query, new List<double> { a, x, c });
            }
        }
        private void rd16_Checked(object sender, RoutedEventArgs e)
        {
            healthcombo8.IsEnabled = true;
        }
        #endregion



        private void MakeDetails(Label label1, RadioButton ax, RadioButton ay, List<string> Fields,string whatquery, string typebill, string category)
        {
            if (label1.Content != null && label1.Content.ToString() != "   - 0,00")
            {
                if (ax.IsChecked == true || ay.IsChecked == true)
                {
                    foreach (string str in IntDetails.ListOfWindows)
                    {
                        if (str != category)
                            continue;
                        else return;
                    }
                    using (Details dt = new Details(db, SplitQuery(whatquery, category, Fields), typebill, category,TitleWindow,label1))
                    {
                        IntDetails.ListOfWindows.Add(category);
                    }
                }
            }
        }
        private void MakeDetails(Label label1, List<string> Fields,string whatquery, string typebill, string category)
        {
            if (label1.Content != null && label1.Content.ToString() != "   - 0,00")
            {
                foreach (string str in IntDetails.ListOfWindows)
                {
                    if (str != category)
                        continue;
                    else return;
                }
                using (Details dt = new Details(db, SplitQuery(whatquery, category, Fields), typebill, category, TitleWindow,label1))
                {
                    IntDetails.ListOfWindows.Add(category);
                }
            }
        }
        private string SplitQuery(string query, string cat, List<string> Fields)
        {
            string thisquery="";
            string[] splits = query.Split(';');

            List<string> listquery = new List<string>();
            for(int i = 0;i<splits.Count()-1;i++)
            {
                if(splits[i].Contains(cat))
                {
                    thisquery = splits[i];
                    string[] addfields = thisquery.Split(new string[]{" FROM"},StringSplitOptions.None);
                    for (int a = 0; a < Fields.Count;a++ )
                    {
                       if(a != Fields.Count-1)
                         addfields[0] += ","+Fields[a];
                       else addfields[0] += ","+Fields[a]+" FROM "+addfields[1];
                    }
                    listquery.Add(addfields[0]);
                }
            }
            thisquery ="";
            for(int b=0;b<listquery.Count;b++)
            {
                thisquery += listquery[b]+";";
            }
            return thisquery;
        }



        #region DETAILS
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Travel  (Ticket)";
            MakeDetails(lbltrav1, rd1, rd2, new List<string> { "date", "note", "valute", "personconcerned", "fromcity", "tocity", "category", "keyid" }, travelquery, "travel", "Ticket");
        }
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Travel  (Food & Lodge)";
            MakeDetails(lbltrav2, rd1, rd2, new List<string> { "date", "note", "valute", "personconcerned", "fromcity", "tocity", "category", "keyid" }, travelquery, "travel", "Food & Lod.");
        }
        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Travel  (Spending)";
            MakeDetails(lbltrav3, rd1, rd2, new List<string> { "date", "note", "valute", "personconcerned", "fromcity", "tocity", "category", "keyid" }, travelquery, "travel", "Spending");
        }
        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Children  (Books)";
            MakeDetails(lblchild1, rd3, rd4, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, kidquery, "children", "Books");
        }
        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Children  (Games)";
            MakeDetails(lblchild2, rd3, rd4, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, kidquery, "children", "Games");
        }
        private void TextBlock_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Animals  (Animal Feed)";
            MakeDetails(lblanim1, rd5, rd6, new List<string> { "date", "note", "valute", "category", "keyid" }, animalquery, "animals", "Animal Feed");
        }
        private void TextBlock_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Animals  (Health)";
            MakeDetails(lblanim2, rd5, rd6, new List<string> { "date", "note", "valute", "category", "keyid" }, animalquery, "animals", "Health");
        }
        private void TextBlock_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Animals  (Accessories)";
            MakeDetails(lblanim3, rd5, rd6, new List<string> { "date", "note", "valute", "category", "keyid" }, animalquery, "animals", "Accessories");
        }
        private void TextBlock_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Vehicles  (Repair)";
            MakeDetails(lblvehic1, rd7, rd8, new List<string> { "date", "note", "valute", "category", "keyid" }, vehicquery, "vehicles", "Repair");
        }
        private void TextBlock_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Vehicles  (Garage)";
            MakeDetails(lblvehic2, rd7, rd8, new List<string> { "date", "note", "valute", "category", "keyid" }, vehicquery, "vehicles", "Garage");
        }
        private void TextBlock_MouseLeftButtonDown_10(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Vehicles  (Purchase)";
            MakeDetails(lblvehic3, rd7, rd8, new List<string> { "date", "note", "valute", "category", "keyid" }, vehicquery, "vehicles", "Purchase");
        }
        private void TextBlock_MouseLeftButtonDown_11(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Vehicles  (Propellant)";
            MakeDetails(lblvehic4, rd7, rd8, new List<string> { "date", "note", "valute", "category", "keyid" }, vehicquery, "vehicles", "Propellant");
        }
        private void TextBlock_MouseLeftButtonDown_12(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Bill & Lease  (House Lease)";
            MakeDetails(lblbill1, new List<string> { "date", "note", "valute", "category", "keyid" }, bilandlquery, "billandlease", "House Lease");
        }
        private void TextBlock_MouseLeftButtonDown_13(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Bill & Lease  (Bill Water)";
            MakeDetails(lblbill2, new List<string> { "date", "note", "valute", "category", "keyid" }, bilandlquery, "billandlease", "Bill Water");
        }
        private void TextBlock_MouseLeftButtonDown_14(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Bill & Lease  (Bill Gas)";
            MakeDetails(lblbill3, new List<string> { "date", "note", "valute", "category", "keyid" }, bilandlquery, "billandlease", "Bill Gas");
        }
        private void TextBlock_MouseLeftButtonDown_15(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Bill & Lease  (Bill Electricity)";
            MakeDetails(lblbill4, new List<string> { "date", "note", "valute", "category", "keyid" }, bilandlquery, "billandlease", "Bill Electricity");
        }
        private void TextBlock_MouseLeftButtonDown_16(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Bill & Lease  (Bill Telephony)";
            MakeDetails(lblbill5, new List<string> { "date", "note", "valute", "category", "keyid" }, bilandlquery, "billandlease", "Bill Telephony");
        }
        private void TextBlock_MouseLeftButtonDown_17(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Bill & Lease  (Condominium)";
            MakeDetails(lblbill6, new List<string> { "date", "note", "valute", "category", "keyid" }, bilandlquery, "billandlease", "Condominium");
        }
        private void TextBlock_MouseLeftButtonDown_18(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Personal Spending  (Sport)";
            MakeDetails(lblpers1, rd11, rd12, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, spendpsquery, "personalactivities", "Sport");
        }
        private void TextBlock_MouseLeftButtonDown_19(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Personal Spending  (Shopping)";
            MakeDetails(lblpers2, rd11, rd12, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, spendpsquery, "personalactivities", "Shopping");
        }
        private void TextBlock_MouseLeftButtonDown_20(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Personal Spending  (Go out)";
            MakeDetails(lblpers3, rd11, rd12, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, spendpsquery, "personalactivities", "Go out");
        }
        private void TextBlock_MouseLeftButtonDown_21(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Personal Spending  (Hobby)";
            MakeDetails(lblpers4, rd11, rd12, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, spendpsquery, "personalactivities", "Hobby");
        }
        private void TextBlock_MouseLeftButtonDown_22(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "House General  (Food)";
            MakeDetails(lblgen1, rd13, rd14, new List<string> { "date", "note", "valute", "category", "keyid" }, housegenquery, "housegen", "Food");
        }
        private void TextBlock_MouseLeftButtonDown_23(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "House General  (Furniture)";
            MakeDetails(lblgen2, rd13, rd14, new List<string> { "date", "note", "valute", "category", "keyid" }, housegenquery, "housegen", "Furniture");
        }
        private void TextBlock_MouseLeftButtonDown_24(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "House General  (Appliance)";
            MakeDetails(lblgen3, rd13, rd14, new List<string> { "date", "note", "valute", "category", "keyid" }, housegenquery, "housegen", "Appliance");
        }
        private void TextBlock_MouseLeftButtonDown_25(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "House General  (Repair & Service)";
            MakeDetails(lblgen4, rd13, rd14, new List<string> { "date", "note", "valute", "category", "keyid" }, housegenquery, "housegen", "Rep. & Ser.");
        }
        private void TextBlock_MouseLeftButtonDown_26(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "Health Care  (Body Cure)";
            MakeDetails(lblhealth1, rd15, rd16, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, healthcarequery, "healthcare", "Body Cure");
        }   
        private void TextBlock_MouseLeftButtonDown_27(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "House General  (Medicine)";
            MakeDetails(lblhealth2, rd15, rd16, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, healthcarequery, "healthcare", "Medicine");
        }
        private void TextBlock_MouseLeftButtonDown_28(object sender, MouseButtonEventArgs e)
        {
            TitleWindow = "House General  (Medical Exam)";
            MakeDetails(lblhealth3, rd15, rd16, new List<string> { "date", "note", "valute", "personconcerned", "category", "keyid" }, healthcarequery, "healthcare", "Medic. Exam");
        }
        #endregion


    }
}
