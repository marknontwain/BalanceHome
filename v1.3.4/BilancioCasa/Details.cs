using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace BilancioCasa
{
   
    public class Details : IDisposable
    {
        IntDetails wind;

        public Details(database db, string query, string typebill, string category,string TitleWindow,Label label)
        {
            wind = new IntDetails(typebill, category, label,query);
            wind.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wind.KeyTitle.Content = TitleWindow;
            wind.Show();
        }

        ~Details()
        {
            Dispose(false);
        }

        private string MakeTitle(string typebill,string category)
        {
            string mystring = null;

            if (typebill == "billandlease")
                mystring = "Bill & Lease  (" + category + ")";
            else if (typebill == "personalactivities")
                mystring = "Personal Spending  (" + category + ")";
            else if (typebill == "housegen")
                mystring = "House General  (" + category + ")";
            else if (typebill == "healthcare")
                mystring = "Health Care  (" + category + ")";
            else
                mystring = mystring = typebill.First().ToString().ToUpper() + typebill.Substring(1) + "  (" + category + ")";
            return mystring;
        }

        private bool disposed;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    handle.Dispose();
                }
                disposed = true;
            }
        }
    }

    public class TravelItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public Familiar Person { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public List<Familiar> Personlist { get; set; }
        public List<string> Valutelist {get;set;}
        public TravelItem()
        {
            database db = new database();
            Personlist = db.RetriveFamily();
            Valutelist = new List<string>() { "Euros","Dollars","Sterlins" };
            db = null;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class KidItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public Familiar Person { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public List<Familiar> Personlist { get; set; }
        public List<string> Valutelist { get; set; }
        public KidItem()
        {
            database db = new database();
            Personlist = db.RetriveFamily();
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
            db = null;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class AnimalItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public string Center { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public List<string> Valutelist { get; set; }
        public ObservableCollection<Item> ListPrd { get; set; }
        public AnimalItem()
        {
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
            ListPrd = new ObservableCollection<Item>();
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class VehicleItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public string Center { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public List<string> Valutelist { get; set; }
        public VehicleItem()
        {
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class BLItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public List<string> Valutelist { get; set; }
        public BLItem()
        {
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class PSItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public string KeyID { get; set; }
        public string Category { get; set; }
        public List<Familiar> Personlist { get; set; }
        public Familiar Person { get; set; }
        public List<string> Valutelist { get; set; }
        public ObservableCollection<Item> ListPrd { get; set; }
        public string Note { get; set; }
        public PSItem()
        {
            database db = new database();
            Personlist = db.RetriveFamily();
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
            ListPrd = new ObservableCollection<Item>();
            db = null;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class HouseItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public string Service { get; set; }
        public string Note { get; set; }
        public string KeyID { get; set; }
        public string Category { get; set; }
        public List<string> Valutelist { get; set; }
        public ObservableCollection<Item> ListPrd { get; set; }
        public HouseItem()
        {
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
            ListPrd = new ObservableCollection<Item>();
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class HealthItem : ICloneable
    {
        public string Date { get; set; }
        public string Bill { get; set; }
        public string Valute { get; set; }
        public string Center { get; set; }
        public List<Familiar> Personlist { get; set; }
        public Familiar Person { get; set; }
        public List<string> Valutelist { get; set; }
        public ObservableCollection<Item> ListPrd { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string KeyID { get; set; }
        public HealthItem()
        {
            database db = new database();
            Personlist = db.RetriveFamily();
            Valutelist = new List<string>() { "Euros", "Dollars", "Sterlins" };
            ListPrd = new ObservableCollection<Item>();
            db = null;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
