using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Net;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.IO;
using System.Globalization;



namespace BilancioCasa
{
    public class database 
    {
        private SQLiteCommand DBPointer;
        private SQLiteConnection DBLink = null;
        private SQLiteDataReader ResultSet;
        string username = System.Environment.UserName;
        public Boolean found = false;
        public Boolean err = false;

        public UserItem CurrentUser { get; set; }
        bool founduser { get; set; }
        bool remember { get; set; }

        public database(string nick, string pasw, string email, string role, ListBox lst1)
        {
            if (CanExecuteQuery())
            {
                string checktables = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name= 'login_details' AND name= 'family';";
                SQLiteDataReader res = this.Query(checktables);
                while (res.Read())
                {
                    if (res.GetInt32(0) == 0)
                    {
                        res.Close();
                        string query = "PRAGMA writable_schema = 1;delete from sqlite_master where type = 'table';PRAGMA writable_schema = 0;";
                        query += "CREATE TABLE IF NOT EXISTS login_details (username TEXT(30), password TEXT(30), email TEXT(40), role TEXT(20), privileges TEXT,keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS family (role TEXT(30), name TEXT(30),isaccount INTEGER not NULL,keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "INSERT INTO family (name,isaccount,keyid) VALUES ('Family','0','0');";
                        query += "CREATE TABLE IF NOT EXISTS billandlease (date DATE, note TEXT, bill FLOAT, category TEXT(20),valute TEXT(15), keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS personalactivities (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), personconcerned INT,keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS vehicles (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS housegen (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS healthcare (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), personconcerned INT,keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS children (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), personconcerned INT,keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS travel (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), personconcerned TEXT, fromcity TEXT, tocity TEXT,keyid INTEGER PRIMARY KEY AUTOINCREMENT); ";
                        query += "CREATE TABLE IF NOT EXISTS animals (date DATE, note TEXT, bill FLOAT, category TEXT(20), valute TEXT(15), keyid INTEGER PRIMARY KEY AUTOINCREMENT);";
                        query += "CREATE TABLE IF NOT EXISTS earnings (date DATE, import FLOAT,keyid INTEGER PRIMARY KEY AUTOINCREMENT);";
                        query += "CREATE TABLE IF NOT EXISTS products (category TEXT(20), name TEXT, bill FLOAT, idbill INTEGER, keyid INTEGER PRIMARY KEY AUTOINCREMENT);";
                        query += "CREATE TABLE IF NOT EXISTS centers (category TEXT(20), name TEXT, keyid INTEGER);";
                        query += "CREATE TABLE IF NOT EXISTS retrivecenters (category TEXT(20), name TEXT, keyid INTEGER PRIMARY KEY AUTOINCREMENT);";
                        HasUpdatedVoid(Query(query));
                        SQLiteDataReader rex = this.Query(this.PrepareQuery("SELECT COUNT(*) FROM login_details WHERE username = '%s' AND email = '%s'", nick, email));
                        while (rex.Read())
                        {
                            if (rex.GetInt32(0) == 0)
                            {
                                rex.Close();
                                if (HasUpdatedBool(Query(this.PrepareQuery("INSERT INTO login_details (username, password, email, role,privileges,keyid) VALUES ('%s','%s','%s', '%s', 'Admin','1')", nick, CreateMD5(pasw), email, role))))
                                    HasUpdatedVoid(Query(PrepareQuery("INSERT INTO family (role, name, isaccount) VALUES ('%s','%s','1');",role,nick)));
                                break;
                            }
                            else
                            {
                                res.Close();
                                break;
                            }
                        }
                        rex.Close();

                        if (lst1.Items.Count > 0)
                        {
                            foreach (string s in lst1.Items)
                            {
                                string[] parts = s.Split(new string[] { ", " }, StringSplitOptions.None);

                                if(parts[0] != role && parts[1] != nick)
                                {
                                    SQLiteDataReader ret = this.Query(this.PrepareQuery("SELECT COUNT(*) FROM family WHERE role = '%s' AND name = '%s'", parts[0], parts[1]));
                                    while (ret.Read())
                                    {
                                        if (ret.GetInt32(0) == 0)
                                        {
                                            ret.Close();
                                            string[] vars = s.Split(new string[] { ", " }, StringSplitOptions.None);
                                            HasUpdatedVoid(Query(this.PrepareQuery("INSERT INTO family (role, name,isaccount) VALUES ('%s','%s','1')", vars[0], vars[1])));
                                            break;
                                        }
                                        else
                                        {
                                            res.Close();
                                            break;
                                        }
                                    }
                                    ret.Close();
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        res.Close();
                        break;
                    }
                }
                res.Close();
            }
        }
        public database()
        {

        }
        public database(string Nick, string pass,UserItem itemuser)
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader res = this.Query(PrepareQuery("SELECT COUNT(*) FROM login_details WHERE username ='%s' AND password = ('%s')", Nick, CreateMD5(pass)));

                while (res.Read())
                {
                    if (res.GetInt32(0) == 1)
                    {
                        MessageBox.Show("Successfully logged in", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        found = true;
                        SQLiteDataReader rex = this.Query(PrepareQuery("SELECT role,privileges,email from login_details where username ='%s' AND password = ('%s')", Nick, CreateMD5(pass)));
                        while (rex.Read())
                        {
                            itemuser.Name = Nick;
                            itemuser.Role = rex["role"].ToString();
                            itemuser.Privileges = rex["privileges"].ToString();
                            itemuser.Email = rex["email"].ToString();
                        }
                        rex.Close();
                        break;
                    }
                    break;
                }
                if (!found)
                    MessageBox.Show("Incorrect username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                res.Close();
            }
        }


        //DA CONTROLLARE DELETEIMPORT
        #region CreateBills
        public void CreateBill(string param, string param2, string param3, string param4,string param5)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into billandlease (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5)))) ;
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreatePersonal(string data, string bill, string category, string note,string valute,string person)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into personalactivities (date, bill, category, note,valute,personconcerned) values ('%s','%s','%s','%s','%s','%s')", data, bill, category, note, valute, person))));
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreatePersonalPrd(string param, string param2, string param3, string param4, string param5, string person,ObservableCollection<Item> listprd)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into personalactivities (date, bill, category, note,valute,personconcerned) values ('%s','%s','%s','%s','%s','%s');", param, param2, param3, param4, param5, person))))
                {
                    if (listprd.Count > 0)
                    {
                        SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                        var id = "";

                        while (rex.Read())
                            id = rex["value"].ToString();
                        rex.Close();

                        for (int i = 0; i < listprd.Count; i++)
                        {
                            HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", param3, listprd[i].name, listprd[i].price,id)));
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }  
        }
        public void CreateVehicle(string param,string param2,string param3,string param4,string param5,string param6)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into vehicles (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))))
                {
                    SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                    var id = "";

                    while (rex.Read())
                        id = rex["value"].ToString();
                    rex.Close();

                    if(HasUpdatedBool(Query(PrepareQuery("insert into centers(category,name,keyid) values('%s','%s','%s')", param3, param6, id))))
                    {
                        rex = this.Query("SELECT COUNT(*) FROM retrivecenters where name='" + param6 + "' and category='" + param3 + "';");
                        while (rex.Read())
                        {
                            if (rex.GetInt32(0) == 0)
                            {
                                HasUpdatedVoid(Query(PrepareQuery("insert into retrivecenters (name,category) values('%s','%s');", param6, param3)));
                            }
                        }
                        rex.Close();
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region Animal
        public void CreateAnimal(string param, string param2, string param3, string param4, string param5)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into animals (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5)))) ;
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }    
        public void CreateAnimalPrd(string param, string param2, string param3, string param4, string param5, ObservableCollection<Item> listprd)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into animals (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))))
                {
                    if (listprd.Count > 0)
                    {
                        SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                        var id = "";

                        while (rex.Read())
                            id = rex["value"].ToString();
                        rex.Close();

                        for (int i = 0; i < listprd.Count; i++)
                        {
                            HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", param3, listprd[i].name, listprd[i].price,id)));
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreateAnimalCenter(string param, string param2, string param3, string param4, string param5, string param6, ObservableCollection<Item> listprd)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into animals (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))))
                {
                    SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                    var id = "";

                    while (rex.Read())
                        id = rex["value"].ToString();

                    rex.Close();

                    if (HasUpdatedBool(Query(PrepareQuery("insert into centers(category,name,keyid) values('%s','%s','%s')", param3, param6, id))))
                    {
                        rex = this.Query("SELECT COUNT(*) FROM retrivecenters where name='"+param6+"' and category='"+param3+"';");
                        while(rex.Read())
                        {
                            if (rex.GetInt32(0) == 0)
                            {
                                HasUpdatedVoid(Query(PrepareQuery("insert into retrivecenters (name,category) values('%s','%s');", param6, param3)));
                            }
                        }
                        rex.Close();

                        if (listprd.Count > 0)
                        {
                            for (int i = 0; i < listprd.Count; i++)
                            {
                                HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", param3, listprd[i].name, listprd[i].price,id)));
                            }
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region Housegen
        public void CreateHouseGenCenter(string param, string param2, string param3, string param4, string param5, string param6)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into housegen (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))))
                {
                    SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                    var id = "";

                    while (rex.Read())
                        id = rex["value"].ToString();
                    rex.Close();
                    if(HasUpdatedBool(Query(PrepareQuery("insert into centers(category,name,keyid) values('%s','%s','%s')", param3, param6, id))))
                    {
                        rex = this.Query("SELECT COUNT(*) FROM retrivecenters where name='" + param6 + "' and category='" + param3 + "';");
                        while (rex.Read())
                        {
                            if (rex.GetInt32(0) == 0)
                            {
                                HasUpdatedVoid(Query(PrepareQuery("insert into retrivecenters (name,category) values('%s','%s');", param6, param3)));
                            }
                        }
                        rex.Close();
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreateFoodGenPrd(string param, string param2, string param3, string param4, string param5, string param6,ObservableCollection<Item>listprd)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into housegen (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))))
                {
                    SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                    var id = "";

                    while (rex.Read())
                        id = rex["value"].ToString();
                    rex.Close();
                    if(HasUpdatedBool(Query(PrepareQuery("insert into centers(category,name,keyid) values('%s','%s','%s')", param3, param6, id))))
                    {
                        rex = this.Query("SELECT COUNT(*) FROM retrivecenters where name='" + param6 + "' and category='" + param3 + "';");
                        while (rex.Read())
                        {
                            if (rex.GetInt32(0) == 0)
                            {
                                HasUpdatedVoid(Query(PrepareQuery("insert into retrivecenters (name,category) values('%s','%s');", param6, param3)));
                            }
                        }
                        rex.Close();
                    }
 
                    if (listprd.Count > 0)
                    {
                        for (int i = 0; i < listprd.Count; i++)
                        {
                            HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", param3, listprd[i].name, listprd[i].price,id)));
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreateHouseGenPrd(string param, string param2, string param3, string param4, string param5, ObservableCollection<Item> listprd)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into housegen (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))))
                {
                    if (listprd.Count > 0)
                    {
                        SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                        var id = "";

                        while (rex.Read())
                            id = rex["value"].ToString();
                        rex.Close();

                        for (int i = 0; i < listprd.Count; i++)
                        {
                            HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", param3, listprd[i].name, listprd[i].price,id)));
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreateHouseGen(string param, string param2, string param3, string param4, string param5)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into housegen (date, bill, category, note,valute) values ('%s','%s','%s','%s','%s')", param, param2, param3, param4, param5))));
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }   
        }
        #endregion

        #region Kids
        public void CreateKids(string param, string param2, string param3, string param4, string param5, string param6)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into children (date, bill, category, note,valute,personconcerned) values ('%s','%s','%s','%s','%s','%s')", param, param2, param3, param4, param5, param6))));
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region Travel
        public void CreateTravel(string param, string param2, string param3, string param4, string param5, string param6,string param7,string param8)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into travel (date, bill, category, note,valute,personconcerned,fromcity,tocity) values ('%s','%s','%s','%s','%s','%s','%s','%s')", param, param2, param3, param4, param5, param6, param7, param8))))
                {
                    SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                    var id = "";

                    while (rex.Read())
                        id = rex["value"].ToString();
                    rex.Close();

                    var subcat = new List<string> { "Fromcity","Tocity",param7,param8};

                    for (int a = 0; a < 2; a++)
                    {
                        if (HasUpdatedBool(Query(PrepareQuery("insert into centers(category,name,keyid) values('%s','%s','%s')", subcat[a], subcat[a + 2], id))))
                        {
                            if (subcat[a + 2] != "")
                            {
                                string query = "SELECT COUNT(*) FROM retrivecenters where name='" + subcat[a + 2] + "' and category='" + subcat[a] + "';";
                                rex = this.Query(query);
                                while (rex.Read())
                                {
                                    if (rex.GetInt32(0) == 0)
                                    {
                                        HasUpdatedVoid(Query(PrepareQuery("insert into retrivecenters (name,category) values('%s','%s');", subcat[a + 2], subcat[a])));
                                    }
                                }
                                rex.Close();
                            }
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreateTravel(string param, string param2, string param3, string param4, string param5, string param6)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into travel (date, bill, category, note,valute,personconcerned) values ('%s','%s','%s','%s','%s','%s')", param, param2, param3, param4, param5, param6)))) ;
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region Health
        public void CreateHealthCenter(string param, string param2, string param3, string param4, string param5, string param6,string param7)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into healthcare (date, bill, category, note, valute, personconcerned) values ('%s','%s','%s','%s','%s','%s')", param, param2, param3, param4, param5, param7))))
                {
                    SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                    var id = "";

                    while (rex.Read())
                        id = rex["value"].ToString();
                    rex.Close();

                    if(HasUpdatedBool(Query(PrepareQuery("insert into centers(category,name,keyid) values('%s','%s','%s')", param3, param6, id))))
                    {
                        rex = this.Query("SELECT COUNT(*) FROM retrivecenters where name='" + param6 + "' and category='" + param3 + "';");
                        while (rex.Read())
                        {
                            if (rex.GetInt32(0) == 0)
                            {
                                HasUpdatedVoid(Query(PrepareQuery("insert into retrivecenters (name,category) values('%s','%s');", param6, param3)));
                            }
                        }
                        rex.Close();
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void CreateHealthPrd(string param, string param2, string param3, string param4, string param5,string param6, ObservableCollection<Item> listprd)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into healthcare (date, bill, category, note,valute,personconcerned) values ('%s','%s','%s','%s','%s','%s')", param, param2, param3, param4, param5, param6))))
                {
                    if (listprd.Count > 0)
                    {
                        SQLiteDataReader rex = this.Query("SELECT last_insert_rowid() as value");
                        var id = "";

                        while (rex.Read())
                            id = rex["value"].ToString();
                        rex.Close();

                        for (int i = 0; i < listprd.Count; i++)
                        {
                            HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", param3, listprd[i].name, listprd[i].price, id)));
                        }
                    }
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }     

        #endregion

        #region Import
        public void CreateEarnings(string param, string param2)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("insert into earnings (date, import) values ('%s','%s')", param, param2))));
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void DeleteImport(database db, DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    if (CanExecuteQuery())
                    {
                        string query = null;

                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            ImportItem it = dt.SelectedItems[i] as ImportItem;
                            var dat = Convert.ToDateTime(it.Date);
                            query += "DELETE FROM earnings WHERE keyid='" + it.KeyID + "';";
                        }
                        SQLiteDataReader rex = this.Query(PrepareQuery(query));
                        rex.Close();
                        dt.ItemsSource = null;

                        db.RetriveImports(dt);
                    }
                }
            }
            else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void UpdateEarnings(DataGrid dt, string field, string value,string keyid)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update earnings set %s='%s' where keyid='%s';", field, value,keyid);
                HasUpdatedVoid(Query(query));
            }
        }
        #endregion

        #endregion


        #region Family
        public void RetriveFamily(ComboBox combo)
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader rex = this.Query("SELECT role,name,keyid FROM family where role is not null");
                while (rex.Read())
                {
                    combo.Items.Add(new Familiar { Role = rex["role"].ToString(), Name = rex["name"].ToString(), KeyId = rex["keyid"].ToString() });
                }
                ScrollViewer sv = new ScrollViewer();
                sv.Content = combo;
                combo.MaxDropDownHeight = 70;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                rex.Close();
            }
        }
        public void RetriveFamily(List<ComboBox> combo)
        {
            if (CanExecuteQuery())
            {
                for (int i = 0; i < combo.Count; i++)
                {
                    SQLiteDataReader rex = this.Query("SELECT role,name,keyid FROM family where role is not null");
                    while (rex.Read())
                    {

                        combo[i].Items.Add(new Familiar { Role = rex["role"].ToString(), Name = rex["name"].ToString(), KeyId = rex["keyid"].ToString() });
                    }
                    ScrollViewer sv = new ScrollViewer();
                    sv.Content = combo[i];
                    combo[i].MaxDropDownHeight = 70;
                    sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                    rex.Close();
                }
            }
        }
        public void RetriveFamily(DataGrid dt,List<DataItem> datalist)
        {
            if (CanExecuteQuery())
            {
                List<DataItem> listx = new List<DataItem>();
                datalist = new List<DataItem>();
                SQLiteDataReader rex = this.Query("SELECT role,name,isaccount,keyid FROM family where role is not null and isaccount = '0'");
                while (rex.Read())
                {
                    datalist.Add(new DataItem { Name = rex["name"].ToString(), Role = rex["role"].ToString(),isaccount=Convert.ToInt32(rex["isaccount"]),KeyID = rex["keyid"].ToString() });
                }
                rex.Close();

                dt.ItemsSource = datalist;
            }
        }
        public List<Familiar> RetriveFamily()
        {
            if (CanExecuteQuery())
            {
                List<Familiar> namesList = new List<Familiar>();
                SQLiteDataReader rex = this.Query("SELECT role,name,keyid FROM family where role is not null");
                while (rex.Read())
                {
                    namesList.Add(new Familiar { Role = rex["role"].ToString(), Name = rex["name"].ToString(), KeyId = rex["keyid"].ToString() });
                }
                namesList.Add(new Familiar { Name = "Family",KeyId="0" });
                rex.Close();
                return namesList;
            }
            return null;
        }
        public void UpdateFamily(DataGrid dt, List<DataItem> datalist,string field,string value,string keyid)
        {
            if (CanExecuteQuery())
            {
                string query = PrepareQuery("update family set %s='%s' where keyid='%s';", field, value, keyid);
                HasUpdatedVoid(Query(query));
            }
        }        
        public void AddFamiliar(string[] parts)
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader ret = this.Query(this.PrepareQuery("SELECT COUNT(*) FROM family WHERE role = '%s' AND name = '%s'", parts[0], parts[1]));
                while (ret.Read())
                {
                    if (ret.GetInt32(0) == 0)
                    {
                        ret.Close();
                        HasUpdatedVoid(Query(this.PrepareQuery("INSERT INTO family (role, name,isaccount) VALUES ('%s','%s','0')", parts[0], parts[1])));
                        break;
                    }
                    else
                    {
                        ret.Close();
                        MessageBox.Show("This member already exists");
                        break;
                    }
                }
                ret.Close();
            }
        }
        public void DeleteFamiliar(database db,DataGrid dt,List<DataItem> datalist)
        {
            if (dt.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    if (CanExecuteQuery())
                    {
                        string query = null;
                        bool userfamily = false;
                        int removed = 0;

                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        { 
                            DataItem it = dt.SelectedItems[i] as DataItem;
                            if (it.isaccount == 0)
                            {
                                query += "DELETE FROM family WHERE keyid='" + it.KeyID + "';";
                                removed++;
                            }
                            else
                                userfamily = true;
                        }
                        if(removed > 0)
                        {
                            SQLiteDataReader rex = this.Query(PrepareQuery(query));
                            rex.Close();
                            dt.ItemsSource = null;
                            db.RetriveFamily(dt, datalist);
                        }
                        if (userfamily)
                            MessageBox.Show("Can't delete familiar with account");
                    }
                }
            }
            else MessageBox.Show("Select one or more familiars");
        }
        public bool HaveFamily()
        {
            int result = 0;
            if (CanExecuteQuery())
            {
                SQLiteDataReader rex = this.Query("SELECT COUNT(*) FROM family");
                try
                {
                    while (rex.Read())
                    {
                        result = rex.GetInt16(0);
                    }
                    rex.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (result > 0)
                return true;
            else return false;
        }

        #endregion


        #region Details
        public void RetriveDetails(DataGrid dt, string query)
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader rex = this.Query(query);
                MakeDetails(rex, dt, query);
                rex.Close();
            }
        }

        private void MakeDetails(SQLiteDataReader rex, DataGrid dt, string query)
        {
            if (query.Contains("Ticket") || query.Contains("Food & Lod.") || query.Contains("Spending"))
            {
                MakeDetailTravel(rex, dt);
            }
            else if (query.Contains("Books") || query.Contains("Games"))
            {
                MakeDetailChildren(rex, dt);
            }
            else if (query.Contains("Animal Feed"))
            {
                MakeDetailAnimalFeed(rex, dt);
            }
            else if (query.Contains("Accessories"))
            {
                MakeDetailAnimAccessories(rex, dt);
            }
            else if (query.Contains("Health"))
            {
                MakeDetailAnimHealth(rex, dt, query);
            }
            else if (query.Contains("Repair") || query.Contains("Garage") || query.Contains("Purchase") || query.Contains("Propellant"))
            {
                MakeDetailVehicle(rex, dt, query);
            }
            else if (query.Contains("Lease") || query.Contains("Water") || query.Contains("Gas") || query.Contains("Electricity") || query.Contains("Telephony") || query.Contains("Condomin."))
            {
                MakeDetailBillandL(rex, dt);
            }
            else if (query.Contains("Sport") || query.Contains("Go out") || query.Contains("Hobby"))
            {
                MakeDetailSpenPS(rex, dt);
            }
            else if (query.Contains("Shopping"))
            {
                MakeDetailSpenPSShopping(rex, dt);
            }
            else if (query.Contains("Furniture") || query.Contains("Appliance"))
            {
                MakeDetailHouseGen(rex, dt);
            }
            else if (query.Contains("Rep. & Ser."))
            {
                MakeDetailHouseGenService(rex, dt, query);
            }
            else if (query.Contains("Food"))
            {
                MakeDetailHouseGenFood(rex,dt,query);
            }
            else if (query.Contains("Body Cure") || query.Contains("Medic. Exam"))
            {
                MakeDetailHealthCenter(rex, dt);
            }
            else if (query.Contains("Medicine"))
            {
                MakeDetailHealthPrd(rex, dt);
            }
        }

        private void MakeDetailTravel(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<TravelItem>();
            Familiar person = null;
            while (rex.Read())
            {
                if (rex["personconcerned"].ToString() != "0")
                {
                    SQLiteDataReader rax = Query("select role,name,keyid from family where keyid ='" + rex["personconcerned"].ToString() + "'");
                    while (rax.Read())
                        person = new Familiar { Role = rax["role"].ToString(), Name = rax["name"].ToString(), KeyId = rax["keyid"].ToString() };
                    rax.Close();
                }
                else
                    person = new Familiar { Name = "Family", KeyId = "0" };

                listx.Add(new TravelItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Person = person, From = rex["fromcity"].ToString(), To = rex["tocity"].ToString(), Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailChildren(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<KidItem>();
            Familiar person = null;
            while (rex.Read())
            {
                if (rex["personconcerned"].ToString() != "0")
                {
                    SQLiteDataReader rax = Query("select role,name,keyid from family where keyid ='" + rex["personconcerned"].ToString() + "'");
                    while (rax.Read())
                        person = new Familiar { Role = rax["role"].ToString(), Name = rax["name"].ToString(), KeyId = rax["keyid"].ToString() };
                    rax.Close();
                }
                else
                    person = new Familiar { Name = "Family", KeyId = "0" };

                listx.Add(new KidItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Person = person, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailAnimalFeed(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<AnimalItem>();
            while (rex.Read())
            {
                listx.Add(new AnimalItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailAnimAccessories(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<AnimalItem>();
            ObservableCollection<Item> Listprd = null;

            while (rex.Read())
            {
                Listprd = new ObservableCollection<Item>();
                SQLiteDataReader rax = Query(PrepareQuery("select name,bill,keyid from products where category='%s' and idbill='%s'", rex["category"].ToString(), rex["keyid"].ToString()));

                while (rax.Read())
                {
                    Listprd.Add(new Item { name = rax["name"].ToString(), price = rax["bill"].ToString(), KeyId = rax["keyid"].ToString(), Category = rex["category"].ToString() });
                }
                rax.Close();

                listx.Add(new AnimalItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString(), ListPrd = Listprd });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailAnimHealth(SQLiteDataReader rex, DataGrid dt, string query)
        {
            var listx = new List<AnimalItem>();
            ObservableCollection<Item> Listprd = null;
            string center = "";

            string[] howmuchtime = query.Split(';');
            for (int i = 0; i < howmuchtime.Count() - 1; i++)
            {
                while (rex.Read())
                {
                    Listprd = new ObservableCollection<Item>();
                    SQLiteDataReader rax = Query("select name from centers where keyid ='" + rex["keyid"].ToString() + "' and category='" + rex["category"].ToString() + "';");
                    while (rax.Read())
                        center = rax["name"].ToString();
                    rax.Close();

                    rax = Query(PrepareQuery("select name,bill,keyid from products where category='%s' and idbill='%s'", rex["category"].ToString(), rex["keyid"].ToString()));
                    while (rax.Read())
                    {
                        Listprd.Add(new Item { name = rax["name"].ToString(), price = rax["bill"].ToString(), KeyId = rax["keyid"].ToString(), Category = rex["category"].ToString() });
                    }
                    rax.Close();

                    listx.Add(new AnimalItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Center = center, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString(), ListPrd = Listprd });
                }
                rex.NextResult();
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailVehicle(SQLiteDataReader rex, DataGrid dt, string query)
        {
            var listx = new List<VehicleItem>();
            string center = "";

            string[] howmuchtime = query.Split(';');
            for (int i = 0; i < howmuchtime.Count() - 1; i++)
            {
                while (rex.Read())
                {
                    SQLiteDataReader rax = Query("select name from centers where keyid ='" + rex["keyid"].ToString() + "' and category='" + rex["category"].ToString() + "';");
                    while (rax.Read())
                        center = rax["name"].ToString();
                    rax.Close();
                    listx.Add(new VehicleItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Center = center, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
                }
                rex.NextResult();
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailBillandL(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<BLItem>();
            while (rex.Read())
            {
                listx.Add(new BLItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailSpenPS(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<PSItem>();
            Familiar person = null;
            while (rex.Read())
            {
                if (rex["personconcerned"].ToString() != "0")
                {
                    SQLiteDataReader rax = Query("select role,name,keyid from family where keyid ='" + rex["personconcerned"].ToString() + "'");
                    while (rax.Read())
                        person = new Familiar { Role = rax["role"].ToString(), Name = rax["name"].ToString(), KeyId = rax["keyid"].ToString() };
                    rax.Close();
                }
                else
                    person = new Familiar { Name = "Family", KeyId = "0" };

                listx.Add(new PSItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Person = person, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailSpenPSShopping(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<PSItem>();
            ObservableCollection<Item> Listprd = null;
            Familiar person = null;
            while (rex.Read())
            {
                Listprd = new ObservableCollection<Item>();
                SQLiteDataReader rax = Query("select role,name,keyid from family where keyid ='" + rex["personconcerned"].ToString() + "'");
                if (rex["personconcerned"].ToString() != "0")
                {
                    while (rax.Read())
                        person = new Familiar { Role = rax["role"].ToString(), Name = rax["name"].ToString(), KeyId = rax["keyid"].ToString() };
                }
                else
                    person = new Familiar { Name = "Family", KeyId = "0" };
                rax.Close();

                rax = Query(PrepareQuery("select name,bill,keyid from products where category='%s' and idbill='%s'", rex["category"].ToString(), rex["keyid"].ToString()));
                while (rax.Read())
                {
                    Listprd.Add(new Item { name = rax["name"].ToString(), price = rax["bill"].ToString(), KeyId = rax["keyid"].ToString(), Category = rex["category"].ToString() });
                }
                rax.Close();

                listx.Add(new PSItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Person = person, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString(),ListPrd = Listprd });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailHouseGen(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<HouseItem>();
            ObservableCollection<Item> Listprd = null;
            while (rex.Read())
            {
                Listprd = new ObservableCollection<Item>();
                SQLiteDataReader rax = Query(PrepareQuery("select name,bill,keyid from products where category='%s' and idbill='%s'", rex["category"].ToString(), rex["keyid"].ToString()));
                while (rax.Read())
                {
                    Listprd.Add(new Item { name = rax["name"].ToString(), price = rax["bill"].ToString(), KeyId = rax["keyid"].ToString(), Category = rex["category"].ToString() });
                }
                rax.Close();
                listx.Add(new HouseItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString(),ListPrd=Listprd });
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailHouseGenService(SQLiteDataReader rex, DataGrid dt, string query)
        {
            var listx = new List<HouseItem>();
            string center = "";

            string[] howmuchtime = query.Split(';');
            for (int i = 0; i < howmuchtime.Count() - 1; i++)
            {
                while (rex.Read())
                {
                    SQLiteDataReader rax = Query("select name from centers where keyid ='" + rex["keyid"].ToString() + "' and category='" + rex["category"].ToString() + "';");
                    while (rax.Read())
                        center = rax["name"].ToString();
                    rax.Close();
                    listx.Add(new HouseItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Service = center, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
                }
                rex.NextResult();
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailHouseGenFood(SQLiteDataReader rex, DataGrid dt, string query)
        {
            var listx = new List<HouseItem>();
            ObservableCollection<Item> Listprd = null;
            string center = "";

            string[] howmuchtime = query.Split(';');
            for (int i = 0; i < howmuchtime.Count() - 1; i++)
            {
                while (rex.Read())
                {
                    Listprd = new ObservableCollection<Item>();
                    SQLiteDataReader rax = Query("select name from centers where keyid ='" + rex["keyid"].ToString() + "' and category='" + rex["category"].ToString() + "';");
                    while (rax.Read())
                        center = rax["name"].ToString();
                    rax.Close();

                    rax = Query(PrepareQuery("select name,bill,keyid from products where category='%s' and idbill='%s'", rex["category"].ToString(), rex["keyid"].ToString()));
                    while (rax.Read())
                    {
                        Listprd.Add(new Item { name = rax["name"].ToString(), price = rax["bill"].ToString(), KeyId = rax["keyid"].ToString(), Category = rex["category"].ToString() });
                    }
                    rax.Close();

                    listx.Add(new HouseItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Service = center, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString(),ListPrd=Listprd });
                }
                rex.NextResult();
            }
            dt.ItemsSource = listx;
        }
        private void MakeDetailHealthCenter(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<HealthItem>();
            Familiar person = null;
            string center = "";
            while (rex.Read())
            {
                SQLiteDataReader rax= Query("select role,name,keyid from family where keyid ='" + rex["personconcerned"].ToString() + "'");;
                if (rex["personconcerned"].ToString() != "0")
                {
                    while (rax.Read())
                        person = new Familiar { Role = rax["role"].ToString(), Name = rax["name"].ToString(), KeyId = rax["keyid"].ToString() };
                }
                else
                    person = new Familiar { Name = "Family", KeyId = "0" };
                rax.Close();

                rax = Query("select name from centers where keyid ='" + rex["keyid"].ToString() + "' and category='" + rex["category"].ToString() + "';");
                while (rax.Read())
                    center = rax["name"].ToString();
                rax.Close();

                listx.Add(new HealthItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Center = center, Person = person, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
                dt.ItemsSource = listx;
            }
        }
        private void MakeDetailHealthPrd(SQLiteDataReader rex, DataGrid dt)
        {
            var listx = new List<HealthItem>();
            ObservableCollection<Item> Listprd = null;
            Familiar person = null;
            while (rex.Read())
            {
                Listprd = new ObservableCollection<Item>();
                SQLiteDataReader rax = Query("select role,name,keyid from family where keyid ='" + rex["personconcerned"].ToString() + "'");
                if (rex["personconcerned"].ToString() != "0")
                {  
                    while (rax.Read())
                        person = new Familiar { Role = rax["role"].ToString(), Name = rax["name"].ToString(), KeyId = rax["keyid"].ToString() };
                }
                else
                    person = new Familiar { Name = "Family", KeyId = "0" };
                rax.Close();

                rax = Query(PrepareQuery("select name,bill,keyid from products where category='%s' and bill='%s'", rex["category"].ToString(), rex["keyid"].ToString()));
                while (rax.Read())
                {
                    Listprd.Add(new Item { name = rax["name"].ToString(), price = rax["bill"].ToString(), KeyId = rax["keyid"].ToString(), Category = rex["category"].ToString() });
                }
                rax.Close();

                listx.Add(new HealthItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Note = rex["note"].ToString(), Valute = rex["valute"].ToString(), Bill = rex["bill"].ToString(), Person = person, Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString(), ListPrd = Listprd });
                dt.ItemsSource = listx;
            }
        }


        #region MakeCenter
        public string MakeCenter(string centerkeyid, string query, string cat, string subcat)
        {
            string[] whatquery = query.Split(';');

            SQLiteDataReader rax = Query(centerkeyid);
            List<string> Fields = new List<string>();

            while (rax.Read())
            {
                Fields.Add(rax["keyid"].ToString());
            }
            if (Fields.Count > 0)
            {
                whatquery[0] += " and keyid ='" + Fields[0] + "';";
                for (int z = 1; z < Fields.Count; z++)
                {
                    whatquery[0] += "SELECT bill FROM " + cat + " WHERE category='" + subcat + "' and keyid='" + Fields[z] + "';";
                }
                query = whatquery[0];
                return query;
            }
            return null;
        }
        #endregion

        #region UpdateAndDelete
        public void UpdateBillCenter(string value, string category, string itemid)
        {
            if (CanExecuteQuery())
            {
                string query = PrepareQuery("update centers set name='%s' where category='%s' and keyid='%s'", value, category, itemid);
                HasUpdatedVoid(Query(query));
            }
        }
        public void DeleteProduct(string category,string id)
        {
            if(CanExecuteQuery())
                HasUpdatedVoid(Query(PrepareQuery("delete from products where category='%s' and keyid='%s';", category,id)));
        }
        public void AddProduct(string category, string id, Item item)
        {
            if (CanExecuteQuery())
                 HasUpdatedVoid(Query(PrepareQuery("insert into products (category,name,bill,idbill) values('%s','%s','%s','%s');", category, item.name, item.price, id)));
        }
        public void UpdateTravel(DataGrid dt, string field, string value, TravelItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");

                string qr = "update travel set %s='%s' where category='%s' and keyid='%s'";
                string query = PrepareQuery(qr, field, value, item.Category,item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedBillTravel(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            TravelItem it = dt.SelectedItems[i] as TravelItem;
                            string query = PrepareQuery("delete from travel where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                query = PrepareQuery("delete from centers where category='Fromcity' and keyid='%s';delete from centers where category='Tocity' and keyid='%s'",it.KeyID, it.KeyID);
                                HasUpdatedVoid(Query(query));
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void UpdateKid(DataGrid dt, string field, string value, KidItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string qr = "update children set %s='%s' where category='%s' and keyid='%s'";
                string query = PrepareQuery(qr, field, value, item.Category,item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedBillKid(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            KidItem it = dt.SelectedItems[i] as KidItem;
                            string query = PrepareQuery("delete from children where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void UpdateAnimal(DataGrid dt, string field, string value, AnimalItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update animals set %s='%s' where category='%s' and keyid='%s'", field, value, item.Category,item.KeyID);
                MessageBox.Show(query);             
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedBillAnimal(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            AnimalItem it = dt.SelectedItems[i] as AnimalItem;
                            string query = PrepareQuery("delete from animals where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                HasUpdatedVoid(Query(PrepareQuery("delete from centers where category='%s' and keyid='%s';", it.Category, it.KeyID)));
                                HasUpdatedVoid(Query(PrepareQuery("delete from products where category='%s' and idbill='%s';", it.Category, it.KeyID)));
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }       
        public void UpdateVehicle(DataGrid dt, string field, string value, VehicleItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update vehicles set %s='%s' where category='%s' and keyid='%s'", field, value, item.Category,item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedBillVehicle(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            VehicleItem it = dt.SelectedItems[i] as VehicleItem;
                            string query = PrepareQuery("delete from vehicles where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                HasUpdatedVoid(Query(PrepareQuery("delete from centers where category='%s' and keyid='%s';", it.Category, it.KeyID)));
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void UpdateBillandL(DataGrid dt, string field, string value, BLItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update billandlease set %s='%s' where category='%s' and keyid='%s'", field, value, item.Category, item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedBillandL(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            BLItem it = dt.SelectedItems[i] as BLItem;
                            string query = PrepareQuery("delete from billandlease where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void UpdateSpenPS(DataGrid dt, string field, string value, PSItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update personalactivities set %s='%s' where category='%s' and keyid='%s'", field, value, item.Category, item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedSpenPS(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            PSItem it = dt.SelectedItems[i] as PSItem;
                            string query = PrepareQuery("delete from personalactivities where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                HasUpdatedVoid(Query(PrepareQuery("delete from products where category='%s' and idbill='%s';", it.Category, it.KeyID)));
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void UpdateHouseGen(DataGrid dt, string field, string value, HouseItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update housegen set %s='%s' where category='%s' and keyid='%s'", field, value, item.Category, item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedHouseGen(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            HouseItem it = dt.SelectedItems[i] as HouseItem;
                            string query = PrepareQuery("delete from housegen where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                HasUpdatedVoid(Query(PrepareQuery("delete from centers where category='%s' and keyid='%s';", it.Category, it.KeyID)));
                                HasUpdatedVoid(Query(PrepareQuery("delete from products where category='%s' and idbill='%s';", it.Category, it.KeyID)));
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void UpdateHealthC(DataGrid dt, string field, string value, HealthItem item)
        {
            if (CanExecuteQuery())
            {
                if (field == "date")
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                
                string query = PrepareQuery("update healthcare set %s='%s' where category='%s' and keyid='%s'", field, value, item.Category, item.KeyID);
                MessageBox.Show(query);
                HasUpdatedVoid(Query(query));
            }
        }
        public bool DeletedHealthC(DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                if (CanExecuteQuery())
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            HealthItem it = dt.SelectedItems[i] as HealthItem;
                            string query = PrepareQuery("delete from healthcare where category='%s' and keyid='%s'", it.Category, it.KeyID);
                            if (HasUpdatedBool(Query(query)))
                            {
                                HasUpdatedVoid(Query(PrepareQuery("delete from centers where category='%s' and keyid='%s';", it.Category, it.KeyID)));
                                HasUpdatedVoid(Query(PrepareQuery("delete from products where category='%s' and idbill='%s';", it.Category, it.KeyID)));
                                MessageBox.Show("Successfully deleted");
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #endregion


        #region Center
        public void RetriveCenter(ComboBox combo,List<string>categorylist)
        {
            if (CanExecuteQuery())
            {
                for(int i =0;i< categorylist.Count;i++)
                {
                    string query = PrepareQuery("select name,category,keyid from retrivecenters where category='%s'", categorylist[i]);
                    SQLiteDataReader rex = this.Query(query);
                    while (rex.Read())
                    {
                        combo.Items.Add(new CenterItem { Name = rex["name"].ToString(), Category = categorylist[i], KeyID = rex["keyid"].ToString() });
                    }
                    rex.Close();
                }
                ScrollViewer sv = new ScrollViewer();
                sv.Content = combo;
                combo.MaxDropDownHeight = 70;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }
        public void RetriveCenter(ComboBox combo,string category)
        {
            if (CanExecuteQuery())
            {
                string query = PrepareQuery("select name,category,keyid from retrivecenters where category='%s'", category);
                SQLiteDataReader rex = this.Query(query);
                while (rex.Read())
                {
                    combo.Items.Add(new CenterItem { Name = rex["name"].ToString(), Category = category, KeyID = rex["keyid"].ToString() });
                }
                rex.Close();

                ScrollViewer sv = new ScrollViewer();
                sv.Content = combo;
                combo.MaxDropDownHeight = 70;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }
        public void RetriveCenter(DataGrid dt,List<string> fromwhere)
        {
            if (CanExecuteQuery())
            {
                List<CenterItem> listx = new List<CenterItem>();
                
                for (int i = 0; i < fromwhere.Count; i++)
                {
                    SQLiteDataReader rex = this.Query("SELECT name,category,keyid FROM retrivecenters where category='"+fromwhere[i]+"';");
                    while (rex.Read())
                    {
                        listx.Add(new CenterItem { Name = rex["name"].ToString(), Category = rex["category"].ToString(), KeyID = rex["keyid"].ToString() });
                    }
                    rex.Close();
                }
                dt.ItemsSource = listx;
            }
        }
        public void DeleteCenter(DataGrid dt, List<string> fromwhere)
        {
            if (dt.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    if (CanExecuteQuery())
                    {
                        string query = null;

                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            CenterItem it = dt.SelectedItems[i] as CenterItem;
                            query += "DELETE FROM retrivecenters WHERE keyid='" + it.KeyID + "';";
                        }
                        SQLiteDataReader rex = this.Query(PrepareQuery(query));
                        rex.Close();
                        dt.ItemsSource = null;
                        RetriveCenter(dt, fromwhere);
                    }
                }         
            }
            else MessageBox.Show("Select one or more centers");
        } 
        public void RetriveImports(DataGrid dt)
        {
            if (CanExecuteQuery())
            {
                List<ImportItem> listx = new List<ImportItem>();
                SQLiteDataReader rex = this.Query("select date,import,keyid from earnings");
                while (rex.Read())
                {
                    listx.Add(new ImportItem { Date = ((DateTime)rex["date"]).ToString("dd/MM/yyyy"), Import = rex["import"].ToString(), KeyID = rex["keyid"].ToString() });
                }
                rex.Close();
                dt.ItemsSource = listx;
            }
        }
        public void RetriveLocation(DataGrid dt,DataGrid at)
        {
            if (CanExecuteQuery())
            {
                List<LocationItem> listx = new List<LocationItem>();
                List<LocationItem> listx2 = new List<LocationItem>();
                SQLiteDataReader rex = this.Query("select name,keyid from retrivecenters where category='Fromcity';");
                while (rex.Read())
                {
                    listx.Add(new LocationItem { Location = rex["name"].ToString(), Category = "Fromcity", KeyID = rex["keyid"].ToString() });
                }
                rex.Close();
                rex = this.Query("select name,keyid from retrivecenters where category='Tocity';");
                while (rex.Read())
                {
                    listx2.Add(new LocationItem { Location = rex["name"].ToString(), Category = "Tocity", KeyID = rex["keyid"].ToString() });
                }
                rex.Close();
                dt.ItemsSource = listx;
                at.ItemsSource = listx2;
            }
        }
        public void DeleteLocation(List<DataGrid> grids)
        {
            for (int b = 0; b < grids.Count; b++)
            {
                if (grids[b].SelectedItems.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        if (CanExecuteQuery())
                        {
                            string query = null;

                            for (int i = 0; i < grids[b].SelectedItems.Count; i++)
                            {
                                LocationItem it = grids[b].SelectedItems[i] as LocationItem;
                                query += "DELETE FROM retrivecenters WHERE keyid='" + it.KeyID + "';";
                            }
                            SQLiteDataReader rex = this.Query(PrepareQuery(query));
                            rex.Close();
                            grids[b].ItemsSource = null;
                            RetriveLocation(grids[0], grids[1]);
                            break;
                        }
                    }
                }
                if(b == 2)
                MessageBox.Show("Select one or more locations");              
            }
        }
        public void RetriveLocation(ComboBox combo, ComboBox combo2, string query)
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader rex = this.Query(query);
                while (rex.Read())
                {
                    combo.Items.Add(rex["name"]);
                }
                rex.NextResult();
                while (rex.Read())
                {
                    combo2.Items.Add(rex["name"]);
                }
                rex.Close();

                ScrollViewer sv = new ScrollViewer();
                sv.Content = combo;
                combo.MaxDropDownHeight = 70;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                ScrollViewer sv2 = new ScrollViewer();
                sv2.Content = combo2;
                combo2.MaxDropDownHeight = 70;
                sv2.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }
        #endregion


        #region LastMonth
        public void Period(string When,List<Label>x,DateTime date)
        {
            if (CanExecuteQuery())
            {
                string query = null;
                string stringtoadd = CalculatePeriod(When, date, query);
                string earnings = CalculatePeriodEarnings(When, date, query);
                query = "SELECT import FROM earnings" + earnings;
                query += "SELECT bill FROM travel" + stringtoadd;
                query += "SELECT bill from children" + stringtoadd;
                query += "SELECT bill from animals" + stringtoadd;
                query += "SELECT bill from vehicles" + stringtoadd;
                query += "SELECT bill from billandlease" + stringtoadd;
                query += "SELECT bill from personalactivities" + stringtoadd;
                query += "SELECT bill from housegen" + stringtoadd;
                query += "SELECT bill from healthcare" + stringtoadd;
                SQLiteDataReader res = this.Query(query);
                double money = 0;
                double travel = 0;
                double children = 0;
                double animals = 0;
                double vehicles = 0;
                double bs = 0;
                double ps = 0;
                double hg = 0;
                double hc = 0;
                List<double> alist = new List<double>() { travel, children, animals, vehicles, bs, ps, hg, hc };

                while (res.Read())
                {
                    money += (double)res["import"];
                }
                res.NextResult();

                for (int i = 0; i < alist.Count; i++)
                {
                    while (res.Read())
                    {
                        alist[i] += (double)res["bill"];
                    }
                    res.NextResult();
                }
                res.Close();

                x[0].Content = "   + " + money.ToString();

                for (int i = 1, a = 0; a < (alist.Count); i++, a++)
                {
                    x[i].Content = "   - " + alist[a].ToString("0.00");
                    money -= alist[a];
                }

                if (money < 0)
                {
                    x[9].Foreground = System.Windows.Media.Brushes.Red;
                    string[] mon = money.ToString("0.00").Split('-');
                    string final = string.Join("- ", mon);
                    x[9].Content = final;
                }
                else
                {
                    x[9].Foreground = System.Windows.Media.Brushes.Green;
                    x[9].Content = "+ " + money;
                }
            }  
        }
        private String CalculatePeriod(string When, DateTime date,string compare)
        {
            string stringtoadd = null;
            string vardate = date.ToString("yyyy-MM-dd");
            string period = null;
            string operators = " where";
            if (compare != null && (compare.Contains("where") || compare.Contains("WHERE")))
                operators = " and";
            switch (When)
            {
                case "Today":
                    stringtoadd = operators + " date ='" + vardate + "';";
                    break;
                case "Last Week":
                    period = date.AddDays(-7).ToString("yyyy-MM-dd");
                    stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                        break;
                case "Last Month":
                        period = date.AddMonths(-1).ToString("yyyy-MM-dd");
                        stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                        break;
                case "Last 4 Months":
                        period = date.AddMonths(-4).ToString("yyyy-MM-dd");
                        stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                        break;
                case "Last 8 Months":
                        period = date.AddMonths(-8).ToString("yyyy-MM-dd");
                        stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                        break;
                case "Last Year":
                        period = date.AddYears(-1).ToString("yyyy-MM-dd");
                        stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                        break;
            }
            return stringtoadd;
        }
        private String CalculatePeriodEarnings(string When, DateTime date, string compare)
        {
            string stringtoadd = null;
            string vardate = date.ToString("yyyy-MM-dd");
            string period = null;
            string operators = " where";
            if (compare != null && (compare.Contains("where") || compare.Contains("WHERE")))
                operators = " and";
            switch (When)
            {
                case "Today":
                case "Last Week":
                case "Last Month":
                    period = date.AddMonths(-1).ToString("yyyy-MM-dd");
                    stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                    break;
                case "Last 4 Months":
                    period = date.AddMonths(-4).ToString("yyyy-MM-dd");
                    stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                    break;
                case "Last 8 Months":
                    period = date.AddMonths(-8).ToString("yyyy-MM-dd");
                    stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                    break;
                case "Last Year":
                    period = date.AddYears(-1).ToString("yyyy-MM-dd");
                    stringtoadd = operators + " date BETWEEN '" + period + "' AND '" + vardate + "';";
                    break;
            }
            return stringtoadd;
        }
        #endregion


        #region BillsPeriod
        public string PeriodBillGen(string When,List<Label>a,string query,List<double> b,DateTime date)
        {
            if (CanExecuteQuery())
            {
                if (query != null)
                {
                    string stringtoadd = CalculatePeriod(When, date, query);
                    string[] newquery = query.Split(new string[] { ";" }, StringSplitOptions.None);

                    for (int i = 0; i < newquery.Count() - 1; i++)
                    {
                        newquery[i] += stringtoadd;
                    }
                    string lastquery = string.Join("", newquery);
                    SQLiteDataReader res = this.Query(lastquery);
                    double tot = 0;

                    try
                    {
                        for (int i = 0; i < b.Count; i++)
                        {
                            while (res.Read())
                            {
                                b[i] += (double)res["bill"];
                            }
                            res.NextResult();
                        }
                        res.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    for (int i = 0; i < b.Count; i++)
                    {
                        a[i].Content = "   - " + b[i].ToString("0.00");
                        tot -= b[i];
                    }
                    a[a.Count - 1].Foreground = System.Windows.Media.Brushes.Red;
                    string[] mon = tot.ToString("0.00").Split('-');
                    string final = string.Join("- ", mon);
                    a[a.Count - 1].Content = final;

                    return lastquery;
                }
                else
                {
                    double tot = 0;
                    for (int i = 0; i < b.Count; i++)
                    {
                        a[i].Content = "   - 0,00";
                        tot -= 0;
                    }
                    a[a.Count - 1].Foreground = System.Windows.Media.Brushes.Red;
                    string[] mon = tot.ToString("0.00").Split('-');
                    string final = string.Join("- ", mon);
                    a[a.Count - 1].Content = final;
                }
            }
            return null;
        }
        public string PeriodBillGen(string When, List<Label> a, string query, List<double> b, DateTime date,int index)
        {
            if (CanExecuteQuery())
            {
                if (query != null)
                {
                    string stringtoadd = CalculatePeriod(When, date, query);
                    string[] newquery = query.Split(new string[] { ";" }, StringSplitOptions.None);

                    for (int i = 0; i < newquery.Count() - 1; i++)
                    {
                        newquery[i] += stringtoadd;
                    }
                    string lastquery = string.Join("", newquery);
                    SQLiteDataReader res = this.Query(lastquery);
                    double tot = 0;

                    string[] howmuchtime = query.Split(';');
                    try
                    {
                        for (int i = 0; i < howmuchtime.Count() - 1; i++)
                        {
                            while (res.Read())
                            {
                                b[index] += (double)res["bill"];
                            }
                            res.NextResult();
                        }

                        res.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    for (int i = 0; i < b.Count; i++)
                    {
                        a[i].Content = "   - " + b[i].ToString("0.00");
                        tot -= b[i];
                    }

                    a[a.Count - 1].Foreground = System.Windows.Media.Brushes.Red;
                    string[] mon = tot.ToString("0.00").Split('-');
                    string final = string.Join("- ", mon);
                    a[a.Count - 1].Content = final;

                    return lastquery;
                }
                else
                {
                    double tot = 0;
                    for (int i = 0; i < b.Count; i++)
                    {
                        a[i].Content = "   - 0,00";
                        tot -= 0;
                    }
                    a[a.Count - 1].Foreground = System.Windows.Media.Brushes.Red;
                    string[] mon = tot.ToString().Split('-');
                    string final = string.Join("- ", mon);
                    a[a.Count - 1].Content = final;
                }
            }
            return null;
        }
        #endregion 


        #region METHODS ACCOUNTS,USERS
        public void RetriveNotes(ListView mylist,string category)
        {
            if(CanExecuteQuery())
            {
                string query = PrepareQuery("SELECT distinct note from %s where note != '' order by note limit 10", category);
                SQLiteDataReader rex = Query(query);
                while (rex.Read())
                {
                    mylist.Items.Add(new CustomListViewItem() { Content = rex["note"].ToString() });
                }
            }
        }
        private string RandPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890.,-_#@";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        private string RetriveUsername(string pasw,string email)
        {
            string usernick = "";

            if (CanExecuteQuery())
            {
                SQLiteDataReader rex = this.Query(this.PrepareQuery("SELECT username FROM login_details WHERE password ='%s' AND email ='%s'", CreateMD5(pasw), email));
                while (rex.Read())
                {
                    usernick= rex["username"].ToString();
                    rex.Close();
                    break;
                }
                rex.Close();
                return usernick;
            }
            return usernick;
        }
        public void CreateAccount(string nick,string pasw,string email,string role)
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader rex = this.Query(this.PrepareQuery("SELECT COUNT(*) FROM login_details WHERE username = '%s' AND email = '%s'", nick, email));
                while (rex.Read())
                {
                    if (rex.GetInt32(0) == 0)
                    {
                        rex.Close();
                        if (HasUpdatedBool(Query(this.PrepareQuery("INSERT INTO login_details (username, password, email, role, privileges) VALUES ('%s','%s','%s', '%s', 'Null')", nick, CreateMD5(pasw), email, role))))
                        {
                            rex = this.Query("SELECT last_insert_rowid() as value");
                            var id = "";

                            while (rex.Read())
                                id = rex["value"].ToString();
                            rex.Close();

                            rex = Query(PrepareQuery("SELECT COUNT(*) FROM family where name='%s' and role='%s'", nick, role));
                            {
                                while (rex.Read())
                                {
                                    if (rex.GetInt32(0) == 0)
                                    {
                                        HasUpdatedVoid(Query(PrepareQuery("INSERT INTO family (role,name,isaccount) VALUES ('%s','%s','%s')", role, nick, id)));
                                    }
                                    else
                                        HasUpdatedVoid(Query(PrepareQuery("update family set isaccount='%s' where role='%s' and name ='%s'",id,role,nick)));
                                }
                                rex.Close();
                            }
                        }
                        MessageBox.Show("Successfully created", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                    }
                    else
                    {
                        rex.Close();
                        MessageBox.Show("This name is already used.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    }
                }
                rex.Close();
            }
        }
        public void SendNewPassword(string nick,string email)
        {
            if (UserExists(nick, email,"password"))
            {
                EmailSend xx = new EmailSend();

                string newpasw = RandPassword(10);

                if(xx.EmailSent(email,newpasw,"password"))
                {
                    NewPassword(nick, email,newpasw);
                }

                xx = null;
            }
        }
        public void SendUsername(string email, string password)
        {
            if(UserExists(email,password,"username"))
            {
                EmailSend xx = new EmailSend();

                string nick = RetriveUsername(password,email);
                if (xx.EmailSent(email, nick, "username")) ;
                xx = null;
            }
        }
        private void NewPassword(string nick,string email,string newpasw)
        {
            if (CanExecuteQuery())
            {
                if (HasUpdatedBool(Query(PrepareQuery("update login_details set password='%s' where username='%s' and email='%s'", CreateMD5(newpasw), nick, email))))
                {
                    MessageBox.Show("Successfully changed password.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else MessageBox.Show("Error occurred while querying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool UserExists(string var1, string var2, string decision)
        {
            if (CanExecuteQuery())
            {
                string query = "";

                if (decision == "password")
                    query = PrepareQuery("SELECT COUNT(*) FROM login_details WHERE username ='%s' AND email = '%s'", var1, var2);

                else if (decision == "username")
                    query = PrepareQuery("SELECT COUNT(*) FROM login_details WHERE email ='%s' AND password = '%s'", var1, CreateMD5(var2));

                SQLiteDataReader ras = this.Query(query);
                while (ras.Read())
                {
                    if (ras.GetInt32(0) == 0)
                    {
                        MessageBox.Show("This user doesn't exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        ras.Close();
                        break;
                    }
                    else
                    {
                        ras.Close();
                        return true;
                    }
                }
            }
            return false;
        }
        public bool UserExists()
        {
            if (CanExecuteQuery())
            {
                SQLiteDataReader ras = this.Query("SELECT COUNT(*) FROM sqlite_master where type='table' and name='login_details'");
                while (ras.Read())
                {
                    if (ras.GetInt32(0) == 0)
                    {
                        ras.Close();
                        break;
                    }
                    else
                    {
                        ras.Close();
                        try
                        {
                            SQLiteDataReader res = this.Query("SELECT COUNT(*) FROM login_details");
                            while (res.Read())
                            {
                                if (res.GetInt32(0) == 0)
                                {
                                    res.Close();
                                    break;
                                }
                                else
                                {
                                    res.Close();
                                    founduser = true;
                                    break;
                                }
                            }
                        }
                        catch (SQLiteException ex)
                        { MessageBox.Show(ex.Message); }
                        break;
                    }
                }
            }
            return founduser;
        }  
        public bool Rememberme(UserItem user)
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey("CookieBalanceHome");
            if (key != null)
            {
                try
                {
                    string[] valuekey = key.GetValue("Value").ToString().Split('-');
                    remember = true;
                    key.Close();
                    this.DBLink = new SQLiteConnection("Data Source=C:\\Users\\" + username + "\\Documents\\BalanceHome\\BalanceHome.sqlite;Version=3;New=False;Compress=True;");
                    try
                    {
                        this.DBLink.Open();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        err = true;
                    }
                    if (!err)
                    {
                        CurrentUser = new UserItem();
                        SQLiteDataReader ras = this.Query(PrepareQuery("SELECT username,role,privileges,email,keyid FROM login_details where password='%s' and username='%s'", valuekey[0], valuekey[1]));
                        while (ras.Read())
                        {
                            CurrentUser.Name = (string)ras["username"];
                            CurrentUser.Role = (string)ras["role"];
                            CurrentUser.Privileges = (string)ras["privileges"];
                            CurrentUser.Email = (string)ras["email"];
                            CurrentUser.KeyID = ras["keyid"].ToString();
                            break;
                        }
                    }
                }
                catch
                {}
            }
            return remember;
        }
        public void RetriveUsers(DataGrid dt)
        {
            if (CanExecuteQuery())
            {
                List<UserItem> listx = new List<UserItem>();
                SQLiteDataReader rex = this.Query("SELECT username,email,role,privileges,keyid FROM login_details");
                while (rex.Read())
                {
                    listx.Add(new UserItem { Name = rex["username"].ToString(), Role = rex["role"].ToString(), Privileges = rex["privileges"].ToString(), Email = rex["email"].ToString(),KeyID=rex["keyid"].ToString() });
                }
                rex.Close();

                dt.ItemsSource = listx;
            }
        }
        public void DeleteUser(database db, DataGrid dt)
        {
            if (dt.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want remove data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    if (CanExecuteQuery())
                    {
                        string query = null;
                        bool adminaccount = false;
                        int removed = 0;

                        for (int i = 0; i < dt.SelectedItems.Count; i++)
                        {
                            UserItem it = dt.SelectedItems[i] as UserItem;
                            if (Int32.Parse(it.KeyID) != 1)
                            {
                                query += "DELETE FROM login_details WHERE keyid='" + it.KeyID + "';";
                                removed++;
                            }
                            else adminaccount = true;
                        }
                        if(removed > 0)
                        {
                            SQLiteDataReader rex = this.Query(PrepareQuery(query));
                            rex.Close();
                            dt.ItemsSource = null;
                            db.RetriveUsers(dt);
                        }
                        if (adminaccount)
                            MessageBox.Show("Can't delete admin's account");
                    }
                }
            }
            else MessageBox.Show("Select one or more accounts");
        }
        public void UpdateUser(DataGrid dt, List<UserItem> datalist, string field, string value, string keyid)
        {
            if (CanExecuteQuery())
            {
                string query = PrepareQuery("update login_details set %s='%s' where keyid='%s'", field, value, keyid);
                if(HasUpdatedBool(Query(query)))
                {
                    if (field == "role")
                        HasUpdatedVoid(Query(PrepareQuery("update family set role='%s' where isaccount='%s'",value,keyid)));
                    else if (field == "username")
                    {
                        HasUpdatedVoid(Query(PrepareQuery("update family set name='%s' where isaccount='%s'", value, keyid)));
                    }
                }
            }
        }
        public void UpdateAccount(UserItem user, string field, string value)
        {
            if (CanExecuteQuery())
            {
                switch (field)
                {
                    case "username":
                        user.Name = value;
                        break;
                    case "email":
                        user.Email = value;
                        break;
                }
                string query = PrepareQuery("update login_details set %s='%s' where keyid='%s'", field, value, user.KeyID);
                HasUpdatedVoid(Query(query));

                MessageBox.Show("Successfully updated account.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            } 
        }
        public void UpdatePaswAccount(UserItem user, string field, string value, string oldvalue)
        {
            if (CanExecuteQuery())
            {
                string query = PrepareQuery("SELECT COUNT(*) FROM login_details WHERE password='%s' AND username = '%s' AND email = '%s'", CreateMD5(oldvalue), user.Name, user.Email);
                SQLiteDataReader rex = Query(query);
                while (rex.Read())
                {
                    if (rex.GetInt32(0) == 0)
                    {
                        rex.Close();
                        MessageBox.Show("Password isn't correct, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    }
                    else
                    {
                        rex.Close();
                        query = PrepareQuery("update login_details set %s='%s' where role='%s' and username='%s' and privileges='%s' and email='%s'", field, CreateMD5(value), user.Role, user.Name, user.Privileges, user.Email);
                        HasUpdatedVoid(Query(query));
                        MessageBox.Show("Successfully updated account.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                    }
                }
            } 
        }
        #endregion


        #region METHODS DB
        public SQLiteDataReader Query(String query)
        {
            if (!this.isConnected())
            {
                MessageBox.Show("Cannot execute query , database not connected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                this.DBPointer = new SQLiteCommand(query, this.DBLink);
                try
                {
                    if (query.Contains("CREATE TABLE"))
                    {
                        this.ResultSet = this.DBPointer.ExecuteReader();
                        this.ResultSet.Close();
                        return null;
                    }
                    else
                    {
                        this.ResultSet = this.DBPointer.ExecuteReader();
                        return this.ResultSet;
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error while performing query: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    this.DBPointer = null;
                }

                return null;
            }
        }
        public String PrepareQuery(String Query, params String[] parameters)
        {
            String[] badChars = new String[2] { "'", "\"" };
            for (int i = 0; i < parameters.Count(); i++)
            {
                for (int j = 0; j < badChars.Count(); j++)
                {
                    if (parameters[i].Contains(badChars[j]))
                    {
                        parameters[i] = parameters[i].Replace(badChars[j], "");
                    }
                }
                if (parameters[i].Contains("\\"))
                {
                    parameters[i] = parameters[i].Replace("\\", "\\\\");
                }
                int pos = Query.IndexOf("%s", 0);
                Query = Query.Remove(pos, 2);
                Query = Query.Insert(pos, parameters[i]);

            }
            return Query;
        }
        public bool HasRow(SQLiteDataReader ResultSet)
        {
            if (ResultSet != null && ResultSet.FieldCount > 0)
            {
                ResultSet.Close();
                return true;
            }
            else if (ResultSet != null)
            {
                ResultSet.Close();
            }
            return false;
        }
        public void HasUpdatedVoid(SQLiteDataReader ResultSet)
        {
            if (ResultSet != null && ResultSet.RecordsAffected > 0)
            {
                ResultSet.Close();
            }
            else if (ResultSet != null)
            {
                ResultSet.Close();
            }
        }
        public bool HasUpdatedBool(SQLiteDataReader ResultSet)
        {
            if (ResultSet != null && ResultSet.RecordsAffected > 0)
            {
                ResultSet.Close();
                return true;
            }
            else if (ResultSet != null)
            {
                ResultSet.Close();
            }
            return false;
        }
        public bool isConnected()
        {
            if (this.DBLink != null && this.DBLink.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CanExecuteQuery()
        {
            Boolean err = false;
            if (username.Contains('/') || username.Contains("bin"))
            { MessageBox.Show("Error with Environment variable, attempt to hack software"); return false; }

            this.DBLink = new SQLiteConnection("Data Source=C:\\Users\\" + username + "\\Documents\\BalanceHome\\BalanceHome.sqlite;Version=3;New=False;Compress=True;");

            try
            {
                this.DBLink.Open();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                err = true;
            }

            if (!err)
            {
                return true;
            }
            return false;
        }
        public string CreateMD5(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion

        private class EmailSend
        {
            public EmailSend()
            {

            }
            public bool EmailSent(string email,string vari, string decision)
            {
                try
                {            
                    using (WebClient client = new WebClient())
                    {
                        var site = "";
                        var values = new NameValueCollection();
                        values["email"] = email;

                        if(decision == "username")
                        {
                            values["username"] = vari;
                            site = "http://napolisourceproject.altervista.org/BalanceHome/recoveruser.php";
                        }

                        else if(decision == "password")
                        {
                            values["password"] = vari;
                            site ="http://napolisourceproject.altervista.org/BalanceHome/recoverpasw.php";
                        }

                        var response = client.UploadValues(site,values);
                        MessageBox.Show("Sent Email", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return false;
            }
        }
    }
}
