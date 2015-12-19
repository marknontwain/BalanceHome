using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per Details.xaml
    /// </summary>
    public partial class IntDetails : Window
    {
        public static IntDetails main;
        public static List<string> ListOfWindows = new List<string>();

        public database db;
        string fieldx { get; set; }
        string Category { get; set; }
        private int indexcombo { get; set; }
        private Label labeldetail { get; set; }
        public DataGrid datagrid { get; set; }

        TravelDetails alf;
        ChildrenDetails child;
        AnimalsDetails anim;
        VehiclesDetails vehic;
        BLDetails billandl;
        PSDetails spenps;
        HouseGenDetails housegen;
        HealthCareDetails healthcare;

        enum DetailsState { Travel,Children,Animals,Vehicles,BL,PS,Housegen,Healthcare}
        DetailsState currentstate;

        public IntDetails(string typebill, string category,Label label,string query)
        {
            InitializeComponent();
            main = this;
            db = new database();
            fieldx = typebill;
            Category = category;
            labeldetail = label;
            switch (typebill)
            {
                case "travel":
                    {
                        currentstate = DetailsState.Travel;
                        alf= new TravelDetails();
                        db.RetriveDetails(alf.datagrid, query);
                        alf.labeldetail = label;
                        gridintdetails.Children.Add(alf.datagrid);
                    }
                    break;
                case "children":
                    {
                       currentstate = DetailsState.Children;
                       child = new ChildrenDetails();
                       db.RetriveDetails(child.datagrid, query);
                       child.labeldetail = label;
                       gridintdetails.Children.Add(child.datagrid);
                    }
                    break;
                case "animals":
                    {
                        anim =new AnimalsDetails();
                        db.RetriveDetails(anim.datagrid, query);
                        anim.labeldetail = label;
                        gridintdetails.Children.Add(anim.datagrid);
                    }
                    break;
                case "vehicles":
                    {
                        vehic= new VehiclesDetails();
                        db.RetriveDetails(vehic.datagrid, query);
                        vehic.labeldetail = label;
                        gridintdetails.Children.Add(vehic.datagrid);
                    }
                    break;
                case "billandlease":
                    {
                       billandl =new BLDetails();
                       db.RetriveDetails(billandl.datagrid, query);
                       billandl.labeldetail = label;
                       gridintdetails.Children.Add(billandl.datagrid);
                    }
                    break;
                case "personalactivities":
                    {
                        currentstate = DetailsState.PS;
                        spenps = new PSDetails();
                        db.RetriveDetails(spenps.datagrid, query);
                        spenps.labeldetail = label;
                        gridintdetails.Children.Add(spenps.datagrid);

                    }
                    break;
                case "housegen":
                    {
                        housegen=new HouseGenDetails();
                        db.RetriveDetails(housegen.datagrid, query);
                        housegen.labeldetail = label;
                        gridintdetails.Children.Add(housegen.datagrid);
                    }
                    break;
                case "healthcare":
                    {
                        healthcare = new HealthCareDetails();
                        currentstate = DetailsState.Healthcare;
                        db.RetriveDetails(healthcare.datagrid, query);
                        healthcare.labeldetail = label;
                        gridintdetails.Children.Add(healthcare.datagrid);
                    }
                    break;
            }           
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (fieldx)
            {
                case "travel":              
                        alf.DatePicker_SelectedDateChanged(sender,e);
                    break;
                case "children":                  
                        child.DatePicker_SelectedDateChanged(sender, e);
                    break;
                case "animals":                
                        anim.DatePicker_SelectedDateChanged(sender, e);
                    break;
                case "vehicles":
                        vehic.DatePicker_SelectedDateChanged(sender, e); 
                    break;
                case "billandlease":          
                        billandl.DatePicker_SelectedDateChanged(sender, e);
                    break;
                case "personalactivities":
                        spenps.DatePicker_SelectedDateChanged(sender, e);
                    break;
                case "housegen":
                        housegen.DatePicker_SelectedDateChanged(sender, e);
                    break;
                case "healthcare":
                        healthcare.DatePicker_SelectedDateChanged(sender, e);
                    break;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(fieldx)
            {
                case "travel":
                        alf.ComboBox_SelectionChanged(sender, e);
                    break;
                case "children":
                        child.ComboBox_SelectionChanged(sender, e);
                    break;
                case "personalactivities":
                    spenps.ComboBox_SelectionChanged(sender, e);
                    break;
                case "healthcare":
                    healthcare.ComboBox_SelectionChanged(sender, e);
                    break;
            }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            switch (fieldx)
            {
                case "travel":
                        alf.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "children":
                        child.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "animals":
                        anim.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "vehicles":
                        vehic.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "billandlease":
                        billandl.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "personalactivities":
                    spenps.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "housegen":
                    housegen.ComboBox_SelectionChanged_1(sender, e);
                    break;
                case "healthcare":
                    healthcare.ComboBox_SelectionChanged_1(sender, e);
                    break;
            }
        }
        private void Hyperlink_Loaded(object sender, RoutedEventArgs e)
        {
            switch (fieldx)
            {
                case "travel":
                    alf.Hyperlink_Loaded(sender, e);
                    break;
                case "children":
                    child.Hyperlink_Loaded(sender, e);
                    break;
                case "animals":
                    anim.Hyperlink_Loaded(sender, e);
                    break;
                case "vehicles":
                    vehic.Hyperlink_Loaded(sender, e);
                    break;
                case "billandlease":
                    billandl.Hyperlink_Loaded(sender, e);
                    break;
                case "personalactivities":
                    spenps.Hyperlink_Loaded(sender, e);
                    break;
                case "housegen":
                    housegen.Hyperlink_Loaded(sender, e);
                    break;
                case "healthcare":
                    healthcare.Hyperlink_Loaded(sender, e);
                    break;
            }
        }
        private void Hyperlink_Loaded_1(object sender, RoutedEventArgs e)
        {
            switch (fieldx)
            {
                case "animals":
                    anim.Hyperlink_Loaded_1(sender, e);
                    break;
                case "personalactivities":
                    spenps.Hyperlink_Loaded_1(sender, e);
                    break;
                case "housegen":
                    housegen.Hyperlink_Loaded_1(sender, e);
                    break;
                case "healthcare":
                    healthcare.Hyperlink_Loaded_1(sender, e);
                    break;
            }
        }
        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            switch (fieldx)
            {
                case "travel":
                    alf.TextBlock_Loaded(sender, e);
                    break;
                case "children":
                    child.TextBlock_Loaded(sender, e);
                    break;
                case "animals":
                    anim.TextBlock_Loaded(sender, e);
                    break;
                case "vehicles":
                    vehic.TextBlock_Loaded(sender, e);
                    break;
                case "billandlease":
                    billandl.TextBlock_Loaded(sender, e);
                    break;
                case "personalactivities":
                    spenps.TextBlock_Loaded(sender, e);
                    break;
                case "housegen":
                    housegen.TextBlock_Loaded(sender, e);
                    break;
                case "healthcare":
                    healthcare.TextBlock_Loaded(sender, e);
                    break;
            }
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            switch (fieldx)
            {
                case "travel":
                    alf.ComboBox_Loaded(sender, e);
                    break;
                case "children":
                    child.ComboBox_Loaded(sender, e);
                    break;
                case "personalactivities":
                    spenps.ComboBox_Loaded(sender, e);
                    break;
                case "healthcare":
                    healthcare.ComboBox_Loaded(sender, e);
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ListOfWindows.Remove(Category);
        }


        private class TravelDetails
        {
            TravelItem lasttravel { get; set; }
            TravelItem newtravel { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }
            
            public TravelDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                datagrid.CellEditEnding += this.datagrid_CellEditEnding;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }
            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();
                DataGridTemplateColumn persconc = new DataGridTemplateColumn();
                DataGridTextColumn from = new DataGridTextColumn();
                DataGridTextColumn tocity = new DataGridTextColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";
                persconc.Header = "Person";
                from.Header = "From";
                tocity.Header = "To";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                persconc.CellTemplate = IntDetails.main.Resources["pers1"] as DataTemplate;
                persconc.CellEditingTemplate = IntDetails.main.Resources["pers2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                from.Binding = new Binding("From");
                tocity.Binding = new Binding("To");

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;
                persconc.IsReadOnly = true;
                from.IsReadOnly = true;
                tocity.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                dt.Columns.Add(persconc);
                dt.Columns.Add(from);
                dt.Columns.Add(tocity);
                dt.Columns.Add(note);
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as TravelItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateTravel(datagrid, "date", date, lasttravel);
                        newtravel.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as TravelItem).Date;
                }
            }
            public void datagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string From = "";
                    string To = "";
                    bool canedit = true;

                    List<string> fields = new List<string> { null, null, null, null, From, To, null };

                    var mytuple = tuple(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newtravel.From = mytuple.Item2[4];
                            newtravel.To = mytuple.Item2[5];

                            if (mytuple.Item1 == "fromcity")
                                value = newtravel.From;
                            else if (mytuple.Item1 == "tocity")
                                value = newtravel.To;

                            IntDetails.main.db.UpdateTravel(datagrid, mytuple.Item1, value, lasttravel);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false; 
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;              
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                        e.Handled = true;
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newtravel = datagrid.SelectedItem as TravelItem;
                    lasttravel = (TravelItem)(datagrid.SelectedItem as TravelItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as TravelItem);
                    if (IntDetails.main.db.DeletedBillTravel(datagrid))
                    {
                        List<TravelItem> newsource = ((IEnumerable<TravelItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if(newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            private Tuple<string, List<string>, bool> tuple(bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                    if (element2.GetType() == typeof(TextBox))
                    {
                        list[i] = ((TextBox)element2).Text;

                        for (int a = 0; a < list.Count; a++)
                        {
                            if (a != i)
                            {
                                if (a == 4)
                                    list[a] = lasttravel.From;
                                else if (a == 5)
                                    list[a] = lasttravel.To;
                            }
                            else continue;
                        }

                        if (i == 4)
                            field = "fromcity";
                        else if (i == 5)
                            field = "tocity";

                        if ((list[i].Contains(" ") || string.IsNullOrEmpty(list[i])))
                        {
                            MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            canedit = false;
                            break;
                        }
                    }
                }
                return Tuple.Create(field, list, canedit);
            }
            public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as TravelItem).Person.ToString())
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var person = (((ComboBox)sender).SelectedItem as Familiar);
                            var value = person.KeyId;
                            IntDetails.main.db.UpdateTravel(datagrid,"personconcerned", value, lasttravel);
                            newtravel.Person = person;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedIndex = IntDetails.main.indexcombo; 
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as TravelItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();
                            IntDetails.main.db.UpdateTravel(datagrid, "valute", valute, lasttravel);
                            newtravel.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as TravelItem).Valute;
                        }
                    }
                }
            }
            public void ComboBox_Loaded(object sender, RoutedEventArgs e)
            {
                var obj = (datagrid.SelectedItem as TravelItem).Person.ToString();

                for (int i = 0; i < (sender as ComboBox).Items.Count; i++)
                {
                    if ((sender as ComboBox).Items[i].ToString() == obj)
                    {
                        IntDetails.main.indexcombo = (sender as ComboBox).Items.IndexOf((sender as ComboBox).Items[i]);
                        (sender as ComboBox).SelectedItem = (sender as ComboBox).Items[i];
                        break;
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newtravel.Note,dtgridcol,isUpdateMode,"travel",newtravel);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newtravel.Bill,dtgridcol,isUpdateMode,"travel",newtravel);
                xx.ShowDialog();
            }
        }
        private class ChildrenDetails
        {
            KidItem lastkid { get; set; }
            KidItem newkid { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }

            public ChildrenDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                datagrid.CellEditEnding += this.datagrid_CellEditEnding;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }

            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();          
                DataGridTemplateColumn valute = new DataGridTemplateColumn();
                DataGridTemplateColumn persconc = new DataGridTemplateColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";
                persconc.Header = "Person";
                
                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                persconc.CellTemplate = IntDetails.main.Resources["pers1"] as DataTemplate;
                persconc.CellEditingTemplate = IntDetails.main.Resources["pers2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;
                persconc.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                dt.Columns.Add(persconc);
                dt.Columns.Add(note);
            }

            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as KidItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateKid(datagrid, "date", date, lastkid);
                        newkid.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as KidItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null) 
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;              
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newkid = datagrid.SelectedItem as KidItem;
                    lastkid = (KidItem)(datagrid.SelectedItem as KidItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as KidItem);
                    if (IntDetails.main.db.DeletedBillKid(datagrid))
                    {
                        List<KidItem> newsource = ((IEnumerable<KidItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if(newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as KidItem).Person.ToString())
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var person = (((ComboBox)sender).SelectedItem as Familiar);
                            var value = person.KeyId;
                            IntDetails.main.db.UpdateKid(datagrid, "personconcerned", value, lastkid);
                            newkid.Person = person;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedIndex = IntDetails.main.indexcombo;
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as KidItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();
                            IntDetails.main.db.UpdateKid(datagrid, "valute", valute, lastkid);
                            newkid.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as KidItem).Valute;
                        }
                    }
                }
            }
            public void ComboBox_Loaded(object sender, RoutedEventArgs e)
            {
                var obj = (datagrid.SelectedItem as KidItem).Person.ToString();

                for (int i = 0; i < (sender as ComboBox).Items.Count; i++)
                {
                    if ((sender as ComboBox).Items[i].ToString() == obj)
                    {
                        IntDetails.main.indexcombo = (sender as ComboBox).Items.IndexOf((sender as ComboBox).Items[i]);
                        (sender as ComboBox).SelectedItem = (sender as ComboBox).Items[i];
                        break;
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newkid.Note,dtgridcol,isUpdateMode,"children",newkid);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newkid.Bill, dtgridcol, isUpdateMode, "children", newkid);
                xx.ShowDialog();
            }
        }
        private class AnimalsDetails
        {
            AnimalItem lastanim { get; set; }
            AnimalItem newanim { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            private string WhatCategory { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }


            public AnimalsDetails()
            {
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                isUpdateMode = new CustomBool(1);

                WhatCategory = IntDetails.main.Category;
                if (WhatCategory == "Health")
                    datagrid.CellEditEnding += this.datagrid_CellEditEndingCenter;
                else if (WhatCategory == "Animal Feed")
                    datagrid.CellEditEnding += this.datagrid_CellEditEndingFeed;
                else 
                    datagrid.CellEditEnding += this.datagrid_CellEditEndingPrd;

                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }
            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                if (WhatCategory == "Health")
                {
                    dt.Columns.Add(new DataGridTextColumn { Header = "Center", Binding = new Binding("Center"), IsReadOnly = true });
                    dt.Columns.Add(new DataGridTemplateColumn { Header = "Products", IsReadOnly = true, CellTemplate = IntDetails.main.Resources["products1"] as DataTemplate, CellEditingTemplate = IntDetails.main.Resources["products2"] as DataTemplate });
                }
                else if (WhatCategory == "Accessories")
                {
                    dt.Columns.Add(new DataGridTemplateColumn { Header = "Products", IsReadOnly = true, CellTemplate = IntDetails.main.Resources["products1"] as DataTemplate, CellEditingTemplate = IntDetails.main.Resources["products2"] as DataTemplate });
                }
                dt.Columns.Add(note);
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as AnimalItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateAnimal(datagrid, "date", date, lastanim);
                        newanim.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as AnimalItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEndingCenter(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string Center = "";

                    bool canedit = true;

                    List<string> fields = new List<string> { null, null, null,Center,null };

                    var mytuple = tuplecenter(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newanim.Center = mytuple.Item2[3];

                            if (mytuple.Item1 == "center")
                                value = newanim.Center;

                            IntDetails.main.db.UpdateBillCenter(value, lastanim.Category, lastanim.KeyID);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false;
            }
            public void datagrid_CellEditEndingFeed(object sender, DataGridCellEditEndingEventArgs e)
            {
            }
            public void datagrid_CellEditEndingPrd(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string Center = "";

                    bool canedit = true;

                    List<string> fields = new List<string> { null, null, null, null, Center };

                    var mytuple = tuplecenter(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newanim.Date = mytuple.Item2[0];
                            newanim.Bill = mytuple.Item2[2];
                            newanim.Note = mytuple.Item2[3];

                            if (mytuple.Item1 == "center")
                                value = newanim.Center;

                            IntDetails.main.db.UpdateBillCenter(value, lastanim.Category, lastanim.KeyID);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false;
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newanim = datagrid.SelectedItem as AnimalItem;
                    lastanim = (AnimalItem)(datagrid.SelectedItem as AnimalItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as AnimalItem);
                    if (IntDetails.main.db.DeletedBillAnimal(datagrid))
                    {
                        List<AnimalItem> newsource = ((IEnumerable<AnimalItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if (newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            private Tuple<string, List<string>, bool> tuplecenter(bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                    if (element2.GetType() == typeof(TextBox))
                    {
                        list[i] = ((TextBox)element2).Text;

                        for (int a = 0; a < list.Count; a++)
                        {
                            if (a != i)
                            {
                                if (a == 3)
                                    list[a] = lastanim.Center;
                            }
                            else continue;
                        }
                        if (i == 3)
                            field = "center";

                        if ((list[i].Contains(" ") || string.IsNullOrEmpty(list[i])))
                        {
                            MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            canedit = false;
                            break;
                        }
                    }
                }
                return Tuple.Create(field, list, canedit);
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as AnimalItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();

                            IntDetails.main.db.UpdateAnimal(datagrid, "valute", valute, lastanim);

                            newanim.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as AnimalItem).Valute;
                        }
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newanim.Note, dtgridcol, isUpdateMode, "animals", newanim);
                xx.ShowDialog();
            }
            public void Hyperlink_Loaded_1(object sender, RoutedEventArgs e)
            {
                InsertPrd xx = new InsertPrd(newanim.ListPrd,dtgridcol,isUpdateMode,newanim.Category,newanim.KeyID);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newanim.Bill, dtgridcol, isUpdateMode, "animals", newanim);
                xx.ShowDialog();
            }
        }
        private class VehiclesDetails
        {
            VehicleItem lastvehic { get; set; }
            VehicleItem newvehic { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }

            public VehiclesDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };

                datagrid.CellEditEnding += this.datagrid_CellEditEnding;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid,IntDetails.main.Category);
            }
            private void CreateGrid(DataGrid dt,string query)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();
                DataGridTextColumn center = new DataGridTextColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                if (query.Contains("Repair") || query.Contains("Garage"))
                    center.Header = "Garage";
                else if (query.Contains("Purchase"))
                    center.Header = "Purchase";
                else if (query.Contains("Propellant"))
                    center.Header = "Propellant";

                center.Binding = new Binding("Center");

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;
                center.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                dt.Columns.Add(center);
                dt.Columns.Add(note);
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as VehicleItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateVehicle(datagrid, "date", date, lastvehic);
                        newvehic.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as VehicleItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string Center = "";

                    bool canedit = true;

                    List<string> fields = new List<string> { null, null, null, Center,null };

                    var mytuple = tuple(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newvehic.Center = mytuple.Item2[3];

                            if (mytuple.Item1 == "center")
                                value = newvehic.Center;

                            IntDetails.main.db.UpdateBillCenter(value, lastvehic.Category, lastvehic.KeyID);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false;
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newvehic = datagrid.SelectedItem as VehicleItem;
                    lastvehic = (VehicleItem)(datagrid.SelectedItem as VehicleItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as VehicleItem);
                    if (IntDetails.main.db.DeletedBillVehicle(datagrid))
                    {
                        List<VehicleItem> newsource = ((IEnumerable<VehicleItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if (newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            private Tuple<string, List<string>, bool> tuple(bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                    if (element2.GetType() == typeof(TextBox))
                    {
                        list[i] = ((TextBox)element2).Text;

                        for (int a = 0; a < list.Count; a++)
                        {
                            if (a != i)
                            {
                                if (a == 3)
                                    list[a] = lastvehic.Center;
                            }
                            else continue;
                        }

                        if (i == 3)
                            field = "center";

                        if ((list[i].Contains(" ") || string.IsNullOrEmpty(list[i])))
                        {
                            MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            canedit = false;
                            break;
                        }
                    }
                }
                return Tuple.Create(field, list, canedit);
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as VehicleItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();
                            IntDetails.main.db.UpdateVehicle(datagrid, "valute", valute, lastvehic);
                            newvehic.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as VehicleItem).Valute;
                        }
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newvehic.Note, dtgridcol, isUpdateMode, "vehicles", newvehic);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newvehic.Bill, dtgridcol, isUpdateMode, "vehicles", newvehic);
                xx.ShowDialog();
            }
        }  
        private class BLDetails
        {
            BLItem lastbillandl { get; set; }
            BLItem newbillandl { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }

            public BLDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                datagrid.CellEditEnding += this.datagrid_CellEditEnding;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }
            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                dt.Columns.Add(note);
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as BLItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateBillandL(datagrid, "date", date, lastbillandl);
                        newbillandl.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as KidItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newbillandl = datagrid.SelectedItem as BLItem;
                    lastbillandl = (BLItem)(datagrid.SelectedItem as BLItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as BLItem);
                    if (IntDetails.main.db.DeletedBillandL(datagrid))
                    {
                        List<BLItem> newsource = ((IEnumerable<BLItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if (newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as BLItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();
                            IntDetails.main.db.UpdateBillandL(datagrid, "valute", valute, lastbillandl);
                            newbillandl.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as BLItem).Valute;
                        }
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newbillandl.Note, dtgridcol, isUpdateMode, "billandlease", newbillandl);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newbillandl.Bill, dtgridcol, isUpdateMode, "billandlease", newbillandl);
                xx.ShowDialog();
            }
        }
        private class PSDetails
        {
            PSItem lastspenps{ get; set; }
            PSItem newspenps { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            private string WhatCategory { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }

            public PSDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                WhatCategory = IntDetails.main.Category;
                datagrid.CellEditEnding += this.datagrid_CellEditEnding;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }
            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();
                DataGridTemplateColumn persconc = new DataGridTemplateColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";
                persconc.Header = "Person";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                persconc.CellTemplate = IntDetails.main.Resources["pers1"] as DataTemplate;
                persconc.CellEditingTemplate = IntDetails.main.Resources["pers2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;
                persconc.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                dt.Columns.Add(persconc);
                if (WhatCategory == "Shopping")
                {
                    dt.Columns.Add(new DataGridTemplateColumn { Header = "Products", IsReadOnly = true, CellTemplate = IntDetails.main.Resources["products1"] as DataTemplate, CellEditingTemplate = IntDetails.main.Resources["products2"] as DataTemplate });
                }
                dt.Columns.Add(note);
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as PSItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateSpenPS(datagrid, "date", date, lastspenps);
                        newspenps.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as PSItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newspenps = datagrid.SelectedItem as PSItem;
                    lastspenps = (PSItem)(datagrid.SelectedItem as PSItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as PSItem);
                    if (IntDetails.main.db.DeletedSpenPS(datagrid))
                    {
                        List<PSItem> newsource = ((IEnumerable<PSItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if (newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as PSItem).Person.ToString())
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var person = (((ComboBox)sender).SelectedItem as Familiar);
                            var value = person.KeyId;
                            IntDetails.main.db.UpdateSpenPS(datagrid, "personconcerned", value, lastspenps);
                            newspenps.Person = person;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedIndex = IntDetails.main.indexcombo;
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as PSItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();
                            IntDetails.main.db.UpdateSpenPS(datagrid, "valute", valute, lastspenps);
                            newspenps.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as PSItem).Valute;
                        }
                    }
                }
            }
            public void ComboBox_Loaded(object sender, RoutedEventArgs e)
            {
                var obj = (datagrid.SelectedItem as PSItem).Person.ToString();

                for (int i = 0; i < (sender as ComboBox).Items.Count; i++)
                {
                    if ((sender as ComboBox).Items[i].ToString() == obj)
                    {
                        IntDetails.main.indexcombo = (sender as ComboBox).Items.IndexOf((sender as ComboBox).Items[i]);
                        (sender as ComboBox).SelectedItem = (sender as ComboBox).Items[i];
                        break;
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newspenps.Note, dtgridcol, isUpdateMode, "personalactivities", newspenps);
                xx.ShowDialog();
            }
            public void Hyperlink_Loaded_1(object sender, RoutedEventArgs e)
            {
                InsertPrd xx = new InsertPrd(newspenps.ListPrd, dtgridcol, isUpdateMode, newspenps.Category, newspenps.KeyID);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newspenps.Bill, dtgridcol, isUpdateMode, "personalactivities", newspenps);
                xx.ShowDialog();
            }
        }
        private class HouseGenDetails
        {
            HouseItem lastgen { get; set; }
            HouseItem newgen { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            private string WhatCategory { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }

            public HouseGenDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                WhatCategory = IntDetails.main.Category;
                if (WhatCategory == "Food")
                    datagrid.CellEditEnding += this.datagrid_CellEditEndingPrdCenter;
                else if (WhatCategory == "Furniture" || WhatCategory == "Appliance")
                    datagrid.CellEditEnding += this.datagrid_CellEditEndingPrd;
                else datagrid.CellEditEnding += this.datagrid_CellEditEndingCenter;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }
            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                if (WhatCategory == "Food")
                {
                    dt.Columns.Add(new DataGridTextColumn { Header = "Market", Binding = new Binding("Service"), IsReadOnly = true });
                    dt.Columns.Add(new DataGridTemplateColumn { Header = "Products", IsReadOnly = true, CellTemplate = IntDetails.main.Resources["products1"] as DataTemplate, CellEditingTemplate = IntDetails.main.Resources["products2"] as DataTemplate });
                }
                else if (WhatCategory == "Rep. & Ser.")
                {
                    dt.Columns.Add(new DataGridTextColumn { Header = "Service", Binding = new Binding("Service"), IsReadOnly = true });
                }
                else if (WhatCategory == "Furniture" || WhatCategory == "Appliance")
                    dt.Columns.Add(new DataGridTemplateColumn { Header = "Products", IsReadOnly = true, CellTemplate = IntDetails.main.Resources["products1"] as DataTemplate, CellEditingTemplate = IntDetails.main.Resources["products2"] as DataTemplate });

                dt.Columns.Add(note);
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as HouseItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateHouseGen(datagrid, "date", date, lastgen);
                        newgen.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as HouseItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEndingCenter(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string Center = "";

                    bool canedit = true;

                    List<string> fields = new List<string> { null, null, null, Center, null };

                    var mytuple = tuplecenter(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newgen.Service = mytuple.Item2[3];

                            if (mytuple.Item1 == "center")
                                value = newgen.Service;

                            IntDetails.main.db.UpdateBillCenter(value, lastgen.Category, lastgen.KeyID);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false;
            }
            public void datagrid_CellEditEndingPrd(object sender, DataGridCellEditEndingEventArgs e)
            {
            }
            public void datagrid_CellEditEndingPrdCenter(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string Center = "";

                    bool canedit = true;

                    List<string> fields = new List<string> { null, null,null, Center};

                    var mytuple = tuplecenter(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newgen.Service = mytuple.Item2[3];

                            if (mytuple.Item1 == "center")
                                value = newgen.Service;

                            IntDetails.main.db.UpdateBillCenter(value, lastgen.Category, lastgen.KeyID);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false;
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newgen = datagrid.SelectedItem as HouseItem;
                    lastgen = (HouseItem)(datagrid.SelectedItem as HouseItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as HouseItem);
                    if (IntDetails.main.db.DeletedHouseGen(datagrid))
                    {
                        List<HouseItem> newsource = ((IEnumerable<HouseItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if (newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            private Tuple<string, List<string>, bool> tuplecenter(bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                    if (element2.GetType() == typeof(TextBox))
                    {
                        list[i] = ((TextBox)element2).Text;

                        for (int a = 0; a < list.Count; a++)
                        {
                            if (a != i)
                            {
                                if (a == 3)
                                    list[a] = lastgen.Service;
                            }
                            else continue;
                        }

                        if (i == 3)
                            field = "center";

                        if ((list[i].Contains(" ") || string.IsNullOrEmpty(list[i])))
                        {
                            MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            canedit = false;
                            break;
                        }
                    }
                }
                return Tuple.Create(field, list, canedit);
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as HouseItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();

                            IntDetails.main.db.UpdateHouseGen(datagrid, "valute", valute, lastgen);

                            newgen.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as HouseItem).Valute;
                        }
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newgen.Note, dtgridcol, isUpdateMode, "housegen", newgen);
                xx.ShowDialog();
            }
            public void Hyperlink_Loaded_1(object sender, RoutedEventArgs e)
            {
                InsertPrd xx = new InsertPrd(newgen.ListPrd, dtgridcol, isUpdateMode, newgen.Category, newgen.KeyID);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newgen.Bill, dtgridcol, isUpdateMode, "housegen", newgen);
                xx.ShowDialog();
            }
        }
        private class HealthCareDetails
        {
            HealthItem lasthealth { get; set; }
            HealthItem newhealth { get; set; }
            DataGridColumn dtgridcol { get; set; }
            private CustomBool isUpdateMode { get; set; }
            private string WhatCategory { get; set; }
            public DataGrid datagrid { get; set; }
            public Label labeldetail { get; set; }

            public HealthCareDetails()
            {
                isUpdateMode = new CustomBool(1);
                datagrid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false, Margin = new Thickness(0, 75, 0, 10) };
                if (IntDetails.main.Category != "Medicine")
                    datagrid.CellEditEnding += this.datagrid_CellEditEndingCenter;
                datagrid.CurrentCellChanged += this.datagrid_CurrentCellChanged;
                datagrid.PreviewKeyDown += this.datagrid_PreviewKeyDown;
                datagrid.PreviewMouseLeftButtonDown += this.datagrid_PreviewMouseLeftButtonDown;
                IntDetails.main.mdfbtn.Click += Button_Click;
                IntDetails.main.dltbtn.Click += Delete_Click;
                CreateGrid(datagrid);
            }
            private void CreateGrid(DataGrid dt)
            {
                DataGridTemplateColumn date = new DataGridTemplateColumn();
                DataGridTemplateColumn note = new DataGridTemplateColumn();
                DataGridTemplateColumn bill = new DataGridTemplateColumn();
                DataGridTemplateColumn valute = new DataGridTemplateColumn();
                DataGridTemplateColumn person = new DataGridTemplateColumn();

                date.Header = "Date";
                note.Header = "Note";
                bill.Header = "Bill";
                valute.Header = "Valute";
                person.Header = "Person";

                date.CellTemplate = IntDetails.main.Resources["celltemplate"] as DataTemplate;
                date.CellEditingTemplate = IntDetails.main.Resources["edittemplate"] as DataTemplate;
                valute.CellTemplate = IntDetails.main.Resources["valute1"] as DataTemplate;
                valute.CellEditingTemplate = IntDetails.main.Resources["valute2"] as DataTemplate;
                note.CellTemplate = IntDetails.main.Resources["seenote1"] as DataTemplate;
                note.CellEditingTemplate = IntDetails.main.Resources["seenote2"] as DataTemplate;
                person.CellTemplate = IntDetails.main.Resources["pers1"] as DataTemplate;
                person.CellEditingTemplate = IntDetails.main.Resources["pers2"] as DataTemplate;
                bill.CellTemplate = IntDetails.main.Resources["bill1"] as DataTemplate;
                bill.CellEditingTemplate = IntDetails.main.Resources["bill2"] as DataTemplate;

                date.IsReadOnly = true;
                note.IsReadOnly = true;
                bill.IsReadOnly = true;
                valute.IsReadOnly = true;
                person.IsReadOnly = true;

                dt.Columns.Add(date);
                dt.Columns.Add(valute);
                dt.Columns.Add(bill);
                dt.Columns.Add(person);

                if(!IntDetails.main.Category.Contains("Medicine"))
                {
                    dt.Columns.Add(new DataGridTextColumn{Header = "Center",Binding = new Binding("Center"),IsReadOnly=true});
                }
                else
                {
                    dt.Columns.Add(new DataGridTemplateColumn { Header = "Products", IsReadOnly = true, CellTemplate = IntDetails.main.Resources["products1"] as DataTemplate, CellEditingTemplate = IntDetails.main.Resources["products2"] as DataTemplate });
                }

                dt.Columns.Add(note);            
            }
            public void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (datagrid.SelectedItem as HealthItem).Date)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                        IntDetails.main.db.UpdateHealthC(datagrid, "date", date, lasthealth);
                        newhealth.Date = date;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((DatePicker)sender).Text = (datagrid.SelectedItem as HealthItem).Date;
                    }
                }
            }
            public void datagrid_CellEditEndingPrd(object sender, DataGridCellEditEndingEventArgs e)
            {
            }
            public void datagrid_CellEditEndingCenter(object sender, DataGridCellEditEndingEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    string field = "";
                    string value = "";
                    string Center = "";

                    bool canedit = true;

                    List<string> fields = new List<string> {null,null,null,null,Center };

                    var mytuple = tuplecenter(canedit, field, fields, datagrid, e);

                    if (mytuple.Item3)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            newhealth.Center = mytuple.Item2[4];
        
                            if (mytuple.Item1 == "center")
                                value = newhealth.Center;

                            IntDetails.main.db.UpdateBillCenter(value, lasthealth.Category, lasthealth.KeyID);
                        }
                    }
                }
                dtgridcol.IsReadOnly = true;
                isUpdateMode.Bool = false;
            }
            public void datagrid_CurrentCellChanged(object sender, EventArgs e)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                    dtgridcol = ((DataGrid)sender).CurrentCell.Column;
            }
            public void datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (isUpdateMode.Bool)
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        ((DataGrid)sender).Focus();
                        dtgridcol.IsReadOnly = true;
                    }
                    else if (e.Key == Key.Tab)
                    {
                        e.Handled = true;
                    }
                }
            }
            public void datagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (isUpdateMode.Bool)
                    isUpdateMode.Bool = false;
            }
            public void Button_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    isUpdateMode.Bool = true;
                    if (dtgridcol != null)
                    {
                        dtgridcol.IsReadOnly = false;
                        datagrid.Focus();
                        datagrid.CurrentCell = new DataGridCellInfo(datagrid.Items[datagrid.SelectedIndex], datagrid.Columns[dtgridcol.DisplayIndex]);
                    }
                    newhealth = datagrid.SelectedItem as HealthItem;
                    lasthealth = (HealthItem)(datagrid.SelectedItem as HealthItem).Clone();
                    datagrid.BeginEdit();
                }
            }
            public void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (datagrid.SelectedIndex > -1)
                {
                    var item = (datagrid.SelectedItem as HealthItem);
                    if (IntDetails.main.db.DeletedHealthC(datagrid))
                    {
                        List<HealthItem> newsource = ((IEnumerable<HealthItem>)datagrid.ItemsSource).ToList();
                        newsource.Remove(item);
                        datagrid.ItemsSource = newsource;
                        if (newsource.Count == 0)
                        {
                            labeldetail.Content = "   - 0,00";
                            var myWindow = Window.GetWindow(datagrid);
                            myWindow.Close();
                        }
                    }
                }
            }
            private Tuple<string, List<string>, bool> tuplecenter(bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                    if (element2.GetType() == typeof(TextBox))
                    {
                        list[i] = ((TextBox)element2).Text;

                        for (int a = 0; a < list.Count; a++)
                        {
                            if (a != i)
                            {
                                if (a == 4)
                                    list[a] = lasthealth.Center;
                            }
                            else continue;
                        }

                        if (i == 4)
                            field = "center";

                        if ((list[i].Contains(" ") || string.IsNullOrEmpty(list[i])))
                        {
                            MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            canedit = false;
                            break;
                        }
                    }
                }
                return Tuple.Create(field, list, canedit);
            }
            public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as HealthItem).Person.ToString())
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var person = (((ComboBox)sender).SelectedItem as Familiar);
                            var value = person.KeyId;
                            IntDetails.main.db.UpdateHealthC(datagrid, "personconcerned", value, lasthealth);
                            newhealth.Person = person;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedIndex = IntDetails.main.indexcombo;
                        }
                    }
                }
            }
            public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (((ComboBox)sender).SelectedIndex > -1)
                {
                    if (((ComboBox)sender).SelectedItem.ToString() != (datagrid.SelectedItem as HealthItem).Valute)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            var valute = ((ComboBox)sender).SelectedItem.ToString();

                            IntDetails.main.db.UpdateHealthC(datagrid, "valute", valute, lasthealth);

                            newhealth.Valute = valute;
                            dtgridcol.IsReadOnly = true;
                        }
                        else
                        {
                            ((ComboBox)sender).SelectedItem = (datagrid.SelectedItem as HealthItem).Valute;
                        }
                    }
                }
            }
            public void ComboBox_Loaded(object sender, RoutedEventArgs e)
            {
                var obj = (datagrid.SelectedItem as HealthItem).Person.ToString();

                for (int i = 0; i < (sender as ComboBox).Items.Count; i++)
                {
                    if ((sender as ComboBox).Items[i].ToString() == obj)
                    {
                        IntDetails.main.indexcombo = (sender as ComboBox).Items.IndexOf((sender as ComboBox).Items[i]);
                        (sender as ComboBox).SelectedItem = (sender as ComboBox).Items[i];
                        break;
                    }
                }
            }
            public void Hyperlink_Loaded(object sender, RoutedEventArgs e)
            {
                SeeNote xx = new SeeNote(newhealth.Note, dtgridcol, isUpdateMode, "healthcare", newhealth);
                xx.ShowDialog();
            }
            public void Hyperlink_Loaded_1(object sender, RoutedEventArgs e)
            {
                InsertPrd xx = new InsertPrd(newhealth.ListPrd, dtgridcol, isUpdateMode, newhealth.Category, newhealth.KeyID);
                xx.ShowDialog();
            }
            public void TextBlock_Loaded(object sender, RoutedEventArgs e)
            {
                InsertBill xx = new InsertBill(newhealth.Bill, dtgridcol, isUpdateMode, "healthcare", newhealth);
                xx.ShowDialog();
            }
        }


        private bool IsNotValidBill(string bill)
        {
            Char[] goodChars = new Char[2] { ',', '.' };
            for(int i = 0;i<bill.Length;i++)
            {
                if (Char.IsLetter(bill[i]) || Char.IsSymbol(bill[i]) || Char.IsControl(bill[i]) || Char.IsSeparator(bill[i]))
                    return true; 

                else if (Char.IsPunctuation(bill[i]))
                {
                    for (int j = 0; j < goodChars.Count(); j++)
                    {
                        if (bill[i] == goodChars[j])
                        {
                            int index = bill.IndexOf(bill[i]);
                            if (index == 0 || bill.Length - 1 < index + 1)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
