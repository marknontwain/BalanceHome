using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        database db;
        public static bool iscreated = false;
        List<DataItem> listdataitem { get; set; }
        List<UserItem> listdatauser { get; set; }

        DataItem objedited { get; set; }
        UserItem objediteduser { get; set; }
        ImportItem objeditedimport { get; set; }

        DataItem lastobject { get; set; }
        UserItem lastuser { get; set; }
        ImportItem lastimport { get; set; }

        DataGridColumn dtgridcol { get; set; }
        DataGridColumn dtgridcoluser { get; set; }
        DataGridColumn dtgridcolimport { get; set; }


        private bool isUpdateMode { get; set; }
        private bool isUpdateModeuser { get; set; }
        private bool isUpdateModeimport { get; set; }


        private bool invalid;
        private bool nickcan;
        private bool emailcan;
        private bool passwordcan;
        string redhex = "#FF9999";

        public Options()
        {
            InitializeComponent();
            db = new database();
            iscreated = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (date.GetValue(DatePicker.SelectedDateProperty) != null && (!string.IsNullOrEmpty(txt1.Text) || !string.IsNullOrEmpty(txt2.Text)))
            {
                MessageBoxResult result = MessageBox.Show("Do you want add data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    string money = txt1.Text + "." + txt2.Text;
                    string data = date.Text;
                    string ultdata = Convert.ToDateTime(data).ToString("yyyy-MM-dd");
                    db.CreateEarnings(ultdata, money);
                }
            }
            else
            {
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (remtab.IsSelected)
                {
                    lstbox.ItemsSource = null;
                    db.RetriveFamily(lstbox,listdataitem);
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
        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (accounts.IsSelected)
                {
                    lstboxuser.ItemsSource = null;
                    db.RetriveUsers(lstboxuser);
                }
                else if(myacc.IsSelected)
                {
                    blockuserbox.Text = MainWindow.main.CurrentUser.Name;
                    blockemailbox.Text = MainWindow.main.CurrentUser.Email;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            iscreated = false;
        }

        #region import

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (importbox.SelectedIndex > -1)
            {
                isUpdateModeimport = true;
                if (dtgridcolimport != null)
                {
                    dtgridcolimport.IsReadOnly = false;
                    importbox.Focus();
                    importbox.CurrentCell = new DataGridCellInfo(importbox.Items[importbox.SelectedIndex], importbox.Columns[dtgridcolimport.DisplayIndex]);
                }
                objeditedimport = importbox.SelectedItem as ImportItem;
                lastimport = (ImportItem)(importbox.SelectedItem as ImportItem).Clone();
                importbox.BeginEdit();
            }
        }
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            db.DeleteImport(db, importbox);
        }
        private void lstbox_CellEditEnding2(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (isUpdateModeimport)
            {
                string field = "";
                string value = "";
                string yyDate = "";
                string xxImport = "";
                bool canedit = true;

                List<string> fields = new List<string> { yyDate, xxImport };

                var mytuple = importuple(canedit, field, fields, lstboxuser, e);

                if (mytuple.Item3)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        objeditedimport.Date = mytuple.Item2[0];
                        objeditedimport.Import = mytuple.Item2[1];

                        if (mytuple.Item1 == "date")
                            value = Convert.ToDateTime(objeditedimport.Date).ToString("dd/MM/yyyy");
                        else value = objeditedimport.Import;
                        db.UpdateEarnings(importbox, mytuple.Item1, value, lastimport.KeyID);
                    }
                }
            }
            dtgridcolimport.IsReadOnly = true;
            isUpdateModeimport = false;
        }
        private void importbox_CurrentCellChanged(object sender, EventArgs e)
        {
            if (importbox.CurrentCell.Column != null)
            {
                dtgridcolimport = importbox.CurrentCell.Column;
            }
        }
        private void importbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (isUpdateModeimport)
            {
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;
                    importbox.Focus();
                    dtgridcolimport.IsReadOnly = true;
                }
                else if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
            }
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy") != (importbox.SelectedItem as ImportItem).Date)
            {
                MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    var date = ((DateTime)(((DatePicker)sender).SelectedDate)).ToString("dd/MM/yyyy");
                    db.UpdateEarnings(importbox, "date", date, lastimport.KeyID);
                    objeditedimport.Date = date;
                    dtgridcolimport.IsReadOnly = true;
                }
                else
                {
                    ((DatePicker)sender).Text = (importbox.SelectedItem as ImportItem).Date;
                }
            }
        }
        #endregion


        #region DataGrid1
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(rolebox.SelectedIndex >-1 && !string.IsNullOrEmpty(nameparent.Text))
            {
                MessageBoxResult result = MessageBox.Show("Do you want add familiar?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                    db.AddFamiliar(new string[] { rolebox.Text, nameparent.Text });
            }
            else
                MessageBox.Show("Please compile all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            db.DeleteFamiliar(db, lstbox, listdataitem);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (lstbox.SelectedIndex > -1)
            {
                isUpdateMode = true;
                if(dtgridcol != null)
                {
                    dtgridcol.IsReadOnly = false;
                    lstbox.Focus();
                    lstbox.CurrentCell = new DataGridCellInfo(lstbox.Items[lstbox.SelectedIndex], lstbox.Columns[dtgridcol.DisplayIndex]);
                }
                objedited = lstbox.SelectedItem as DataItem;
                lastobject = (DataItem)(lstbox.SelectedItem as DataItem).Clone();
                lstbox.BeginEdit();
            }
        }
        private void lstbox_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (isUpdateMode ) 
            {
                string field = "";
                string value = "";
                string yyName = "";
                string xxRole = "";
                bool canedit = true;

                List<string> fields = new List<string> { yyName,xxRole};
                
                var mytuple = famtuple(canedit, field, fields, lstboxuser, e);

                if(mytuple.Item3)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                       
                        objedited.Role = mytuple.Item2[0];
                        objedited.Name = mytuple.Item2[1];

                        if (mytuple.Item1 == "role")
                            value = objedited.Role;
                        else value = objedited.Name;
                        db.UpdateFamily(lstbox, listdataitem, mytuple.Item1, value, lastobject.KeyID);
                    }
    
                }
            }
            dtgridcol.IsReadOnly = true;
            isUpdateMode = false;
        }
        private void lstbox_CurrentCellChanged(object sender, EventArgs e)
        {
            if(lstbox.CurrentCell.Column != null)
            {
                dtgridcol = lstbox.CurrentCell.Column;
            }
        }
        private void lstbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (isUpdateMode)
            {
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;
                    lstbox.Focus();
                    dtgridcol.IsReadOnly = true;
                }
                else if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                if (((ComboBox)sender).SelectedItem.ToString() != (lstbox.SelectedItem as DataItem).Role)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var role = ((ComboBox)sender).SelectedItem.ToString();
                        db.UpdateFamily(lstbox, listdataitem, "role", role, lastobject.KeyID);
                        objedited.Role = role;
                        dtgridcol.IsReadOnly = true;
                    }
                    else
                    {
                        ((ComboBox)sender).SelectedItem = (lstbox.SelectedItem as DataItem).Role;
                    }
                }
            }
        }

        #endregion

        private void lstbox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isUpdateMode)
            {
                isUpdateMode = false;
            }
            if(isUpdateModeuser)
            {
                isUpdateModeuser = false;
            }
            if (isUpdateModeimport)
                isUpdateModeimport = false;
        }
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isUpdateMode)
            {
                isUpdateMode = false;
            }
            if (isUpdateModeuser)
            {
                isUpdateModeuser = false;
            }
            if (isUpdateModeimport)
                isUpdateModeimport = false;
        }

        #region DataGrid2

        private void btnremove_Click(object sender, RoutedEventArgs e)
        {
            db.DeleteUser(db, lstboxuser);
        }
        private void btnmodify_Click(object sender, RoutedEventArgs e)
        {
            if (lstboxuser.SelectedIndex > -1)
            {
                isUpdateModeuser = true;

                if (dtgridcoluser != null)
                {
                    dtgridcoluser.IsReadOnly = false;
                    lstboxuser.Focus();
                    lstboxuser.CurrentCell = new DataGridCellInfo(lstboxuser.Items[lstboxuser.SelectedIndex], lstboxuser.Columns[dtgridcoluser.DisplayIndex]);
                }

                objediteduser = lstboxuser.SelectedItem as UserItem;
                lastuser = (UserItem)(lstboxuser.SelectedItem as UserItem).Clone();
                lstboxuser.BeginEdit();
            }
        }
        private void lstboxuser_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (isUpdateModeuser)
            {
                string field = "";
                string value = "";
                string yyName = "";
                string xxRole = "";
                string xxPriv = "";
                string Email = "";
                bool canedit = true;

                List<string> fields = new List<string> { yyName,xxRole,xxPriv,Email};

                var mytuple = perstuple(canedit,field,fields,lstboxuser,e);

                if(mytuple.Item3)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        objediteduser.Name = mytuple.Item2[0];
                        objediteduser.Role = mytuple.Item2[1];
                        objediteduser.Privileges = mytuple.Item2[2];
                        objediteduser.Email = mytuple.Item2[3];

                        if (mytuple.Item1 == "username")
                        {
                            value = objediteduser.Name;
                            MainWindow.main.CurrentUser.Name = value;
                            Software.main.welcomelabel.Content = "Welcome " + MainWindow.main.CurrentUser.Name + "   (" + MainWindow.main.CurrentUser.Privileges + ")";
                        }

                        else if (mytuple.Item1 == "role")
                        {
                            value = objediteduser.Role;
                        }

                        else if (mytuple.Item1 == "privileges")
                        {
                            value = objediteduser.Privileges;
                        }
                        else
                        {
                            value = objediteduser.Email;
                        }
                        db.UpdateUser(lstboxuser, listdatauser, mytuple.Item1, value, lastuser.KeyID);
                    }
                    else
                    {

                    }
                }
            }
            dtgridcoluser.IsReadOnly = true;
            isUpdateModeuser = false;
        }
        private void lstboxuser_CurrentCellChanged(object sender, EventArgs e)
        {
            if (lstboxuser.CurrentCell.Column != null)
            {
                dtgridcoluser = lstboxuser.CurrentCell.Column;
            }
        }
        private void lstboxuser_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (isUpdateModeuser)
            {
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;
                    lstboxuser.Focus();
                    dtgridcoluser.IsReadOnly = true;
                }
                else if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
            }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                if (((ComboBox)sender).SelectedItem.ToString() != (lstboxuser.SelectedItem as UserItem).Role)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        var role = ((ComboBox)sender).SelectedItem.ToString();
                        db.UpdateUser(lstboxuser, listdatauser, "role", role, lastuser.KeyID);
                        objediteduser.Role = role;
                        dtgridcoluser.IsReadOnly = true;
                    }
                    else
                    {
                        ((ComboBox)sender).SelectedItem = (lstboxuser.SelectedItem as UserItem).Role;
                    }
                }
            }
        }
        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                if (((ComboBox)sender).SelectedItem.ToString() != (lstboxuser.SelectedItem as UserItem).Privileges)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        if(Convert.ToInt32((lstboxuser.SelectedItem as UserItem).KeyID) !=1)
                        {
                            var privs = ((ComboBox)sender).SelectedItem.ToString();
                            db.UpdateUser(lstboxuser, listdatauser, "privileges", privs, lastuser.KeyID);
                            objediteduser.Privileges = privs;
                        }
                        else
                        {
                            MessageBox.Show("Can't modify privileges of admin's account.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        dtgridcoluser.IsReadOnly = true;
                    }
                    else
                    {
                        ((ComboBox)sender).SelectedItem = (lstboxuser.SelectedItem as UserItem).Privileges;
                    }
                }
            }
        }
        #endregion


        private Tuple<string, List<string>,bool> famtuple( bool canedit,string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
        {
            for(int i = 0; i<list.Count;i ++)
            {
                FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                if (element2.GetType() == typeof(TextBox))
                {
                    list[i] = ((TextBox)element2).Text;

                    for (int a = 0; a < list.Count; a++)
                    {
                        if (a != i)
                        {
                            if (a == 0)
                                list[a] = lastobject.Role;
                            else if (a == 1)
                                list[a] = lastobject.Name;
                        }
                        else continue;
                    }

                    if (i == 0)
                    {
                       field = "role";
                    }
                    else if (i == 1)
                    {
                        field = "name";
                        foreach (DataItem datx in lstbox.Items)
                        {
                            if (list[i] == datx.Name)
                            {
                                MessageBox.Show("This member already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                canedit = false;
                                break;
                            }
                        }
                    }

                    if (list[i].Contains(" ") || string.IsNullOrEmpty(list[i]))
                    {
                        MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        canedit = false;
                        break;
                    }
                }
            }
            return Tuple.Create(field,list,canedit);
        }
        private Tuple<string, List<string>, bool> importuple(bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
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
                            if (a == 0)
                                list[a] = lastimport.Date;
                            else if (a == 1)
                                list[a] = lastimport.Import;
                        }
                        else continue;
                    }

                    if (i == 0)
                    {
                        field = "date";
                    }
                    else if (i == 1)
                    {
                        field = "import";
                    }

                    if (list[i].Contains(" ") || string.IsNullOrEmpty(list[i]))
                    {
                        MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        canedit = false;
                        break;
                    }
                }
            }
            return Tuple.Create(field, list, canedit);
        }
        private Tuple<string, List<string>, bool> perstuple( bool canedit, string field, List<string> list, DataGrid dt, DataGridCellEditEndingEventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                FrameworkElement element2 = dt.Columns[i].GetCellContent(e.Row);
                if (element2.GetType() == typeof(TextBox))
                {
                    list[i] = ((TextBox)element2).Text;

                    for (int a = 0; a<list.Count; a++ )
                    {
                        if (a != i)
                        {
                            if (a == 0)
                                list[a] = lastuser.Name;
                            else if (a == 1)
                                list[a] = lastuser.Role;
                            else if (a == 2)
                                list[a] = lastuser.Privileges;
                            else if (a == 3)
                                list[a] = lastuser.Email;
                        }
                        else continue;
                    }

                    if (i == 0)
                    {
                        field = "username";
                        foreach (UserItem datx in lstboxuser.Items)
                        {
                            if (list[i] == datx.Name)
                            {
                                MessageBox.Show("This name is already used.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                canedit = false;
                                break;
                            }
                        }
                    }
                    else if (i == 1)
                        field = "role";
                    else if (i == 2)
                        field = "privileges";
                    else if (i == 3)
                    {
                        field = "email";
                        foreach (UserItem datx in lstboxuser.Items)
                        {
                            if (list[i] == datx.Email)
                            {
                                MessageBox.Show("This email is already used.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                canedit = false;
                                break;
                            }
                        }
                    }

                    if (field != "email" && (list[i].Contains(" ") || string.IsNullOrEmpty(list[i])))
                    {
                        MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        canedit = false;
                        break;
                    }
                    else if (field == "email" && (list[i].Contains(" ") || string.IsNullOrEmpty(list[i]) || !IsValidEmail(list[i])))
                    {
                        MessageBox.Show("Incorrect format email.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        canedit = false;
                        break;
                    }
                }
            }
            return Tuple.Create(field, list, canedit);
        }
    
        
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        #region AccountSettings
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (nickcan)
            {
                MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db.UpdateAccount(MainWindow.main.CurrentUser, "username", nickac.Text);
                    blockuserbox.Text = nickac.Text;
                    Software.main.welcomelabel.Content = "Welcome " + MainWindow.main.CurrentUser.Name + "   (" + MainWindow.main.CurrentUser.Privileges + ")";
                }
            }
            else
                MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            nickac.Text = "";
        }

        private void newuser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nickac.Text))
            {
                if (nickac.Text.Length < 4)
                {
                    nickac.ClearValue(Border.BorderBrushProperty);
                    nickac.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    nickac.ToolTip = "Username must contains at least 4 characters";
                    nickcan = false;
                }
                else
                {
                    nickac.ClearValue(Border.BorderBrushProperty);
                    nickac.ToolTip = null;
                    nickcan = true;
                }
            }
            else
            {
                nickac.ClearValue(Border.BorderBrushProperty);
                nickac.ToolTip = null;
                nickcan = false;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (emailcan)
            {
                MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db.UpdateAccount(MainWindow.main.CurrentUser, "email", emailbox.Text);
                    blockemailbox.Text = emailbox.Text;
                }
            }
            else
                MessageBox.Show("Insert valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            blockemailbox.Text = "";
        }
        private void emailac_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(emailbox.Text))
            {
                if (!IsValidEmail(emailbox.Text))
                {
                    emailbox.ClearValue(Border.BorderBrushProperty);
                    emailbox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    emailbox.ToolTip = "Incorrect format email. Example: test@domain.com";
                    emailcan = false;
                }
                else
                {
                    emailbox.ClearValue(Border.BorderBrushProperty);
                    emailbox.ToolTip = null;
                    emailcan = true;
                }
            }
            else
            {
                emailbox.ClearValue(Border.BorderBrushProperty);
                emailbox.ToolTip = null;
                emailcan = false;
            }
        }
        private void paswconfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(pasconfirm.Password))
            {
                if (!PasswordMatch(paswac.Password, pasconfirm.Password))
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;

                    paswac.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    paswac.ToolTip = "Password doesn't match";
                    pasconfirm.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    pasconfirm.ToolTip = "Password doesn't match";

                    passwordcan = false;
                }
                else
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;

                    passwordcan = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(paswac.Password))
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;

                    paswac.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    paswac.ToolTip = "Password doesn't match";
                    pasconfirm.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    pasconfirm.ToolTip = "Password doesn't match";
                }
                else
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;
                }

                passwordcan = false;
            }
        }
        private void paswac_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(paswac.Password))
            {
                if (!PasswordMatch(paswac.Password, pasconfirm.Password))
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;

                    paswac.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    paswac.ToolTip = "Password doesn't match";
                    pasconfirm.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    pasconfirm.ToolTip = "Password doesn't match";

                    passwordcan = false;
                }
                else
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;

                    passwordcan = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(pasconfirm.Password))
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;

                    paswac.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    paswac.ToolTip = "Password doesn't match";
                    pasconfirm.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(redhex));
                    pasconfirm.ToolTip = "Password doesn't match";
                }
                else
                {
                    paswac.ClearValue(Border.BorderBrushProperty);
                    pasconfirm.ClearValue(Border.BorderBrushProperty);
                    paswac.ToolTip = null;
                    pasconfirm.ToolTip = null;
                }

                passwordcan = false;
            }
        }
        private bool PasswordMatch(string xb, string yb)
        {
            if (xb.Equals(yb))
                return true;
            else return false;
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (passwordcan && oldpasw.Password.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want update data?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db.UpdatePaswAccount(MainWindow.main.CurrentUser, "password", paswac.Password,oldpasw.Password);
                }
            }
            else
                MessageBox.Show("Insert valid password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            paswac.Password = "";
            pasconfirm.Password = "";
        }
        #endregion



        #region Center
        private void rd1_Checked(object sender, RoutedEventArgs e)
        {
            db.RetriveCenter(centergrid,new List<string>{"Health"});
        }
        private void rd2_Checked(object sender, RoutedEventArgs e)
        {
            db.RetriveCenter(centergrid,new List<string>{"Repair","Garage","Propellant","Purchase"});
        }
        private void rd3_Checked(object sender, RoutedEventArgs e)
        {
            db.RetriveCenter(centergrid, new List<string>{"Food","Rep. & Ser."});
        }
        private void rd4_Checked(object sender, RoutedEventArgs e)
        {
            db.RetriveCenter(centergrid, new List<string>{"Body Cure","Medic. Exam"});
        }
        private void deletecenter_Click(object sender, RoutedEventArgs e)
        {
            if (rd1.IsChecked == true)
                db.DeleteCenter(centergrid, new List<string> { "Health" });
            else if (rd2.IsChecked == true)
                db.DeleteCenter(centergrid, new List<string> { "Repair", "Garage", "Propellant", "Purchase" });
            else if (rd3.IsChecked == true)
                db.DeleteCenter(centergrid, new List<string>{"Food","Rep. & Ser."});
            else if (rd4.IsChecked == true)
                db.DeleteCenter(centergrid, new List<string>{"Body Cure","Medic. Exam"});
        }

        private void centertab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.Source is TabControl)
            {
                if (locationtb.IsSelected)
                {
                    Keyboard.ClearFocus();
                    db.RetriveLocation(locationgridfrom, locationgridto);
                }
            }
        }
        private void delocation_Click(object sender, RoutedEventArgs e)
        {
            db.DeleteLocation(new List<DataGrid> { locationgridfrom,locationgridto});
            locationgridfrom.SelectedIndex = -1;
            locationgridto.SelectedIndex = -1;
        }
        #endregion

        private void TabControl_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            if(e.Source is TabControl)
            {
                if(remearnings.IsSelected)
                {
                    Keyboard.ClearFocus();
                    db.RetriveImports(importbox);
                }
            }
        }
    }





}
