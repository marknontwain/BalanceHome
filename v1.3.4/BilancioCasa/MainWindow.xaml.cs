using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        string username = System.Environment.UserName;
        database db;
        public static MainWindow main;

        bool accesssoftware { get; set; }
        public UserItem CurrentUser { get; set; }
        UserControl1 uc1;
        UserControl2 uc2;
        UserControl3 uc3;
        Signup uc4;
        ForgotPasw uc5;

        public MainWindow() 
        {
            InitializeComponent();
            db = new database();

            if (username.Contains('/') || username.Contains("bin"))
            { MessageBox.Show("Error with Environment variable, attempt to hack software"); return; }

            if (!Directory.Exists("C:\\Users\\" + username + "\\Documents\\BalanceHome"))
                Directory.CreateDirectory("C:\\Users\\" + username + "\\Documents\\BalanceHome");

            if (db.UserExists())
            {
                if (!db.Rememberme(CurrentUser))
                {
                    uc3 = new UserControl3();
                    layout1.Children.Add(uc3);
                }
                else
                {
                    CurrentUser = new UserItem();
                    CurrentUser = db.CurrentUser;
                    Opacity = 0.0;
                    accesssoftware = true;
                }
            }
            else
            {
                uc1 = new UserControl1();
                layout1.Children.Add(uc1);
            }
            main = this;
            db = null;
            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, this.OnMaximizeWindow, this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow, this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow, this.OnCanResizeWindow));
        }
        
        public void CreateSoftware()
        {
            layout1.Children.Clear();
            Software ahua = new Software();
            ahua.Show();
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Close();

        }
        public void Config_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to continue?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result.Equals(MessageBoxResult.Yes))
            {
                layout1.Children.Clear();
                uc1.Dispose();
                uc2 = new UserControl2();
                layout1.Children.Add(uc2);
            }
            else
                Keyboard.ClearFocus();
        }
        public void Config2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to continue?", "Saving...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    db = new database(uc1.nickac.Text, uc1.paswac.Password, uc1.emailbox.Text, uc1.rolebox.Text, uc2.lst1);

                    if (!db.err)
                    {
                        layout1.Children.Clear();
                        uc2.Dispose();
                        uc3 = new UserControl3();
                        layout1.Children.Add(uc3);
                        try { Registry.CurrentUser.DeleteSubKey("CookieBalanceHome"); }
                        catch (Exception Exception) { ; }
                    }
                    else
                    {
                        MessageBox.Show("Erorr Occurred!");
                    }

                    db = null;
                }
                else
                    Keyboard.ClearFocus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Login_Click(object sender, RoutedEventArgs e, string nick, string pasw)
        {
            if (!string.IsNullOrEmpty(nick) && !string.IsNullOrEmpty(pasw))
            {
                TrytoConnect(nick, pasw);

                if (db.found)
                {
                    if (uc3.rememberbox.IsChecked == true)
                    {
                        RegistryKey key;
                        key = Registry.CurrentUser.CreateSubKey("CookieBalanceHome");
                        key.SetValue("Value", CreateMD5(uc3.paswlogin.Password) + "-" + nick);
                        key.Close();
                    }
                    db = null;
                    Software soft = new Software();
                    soft.Show();
                    layout1.Children.Clear();
                    uc3.Dispose();
                    Application.Current.MainWindow = this;
                    Application.Current.MainWindow.Close();
                }
                else Keyboard.ClearFocus();
            }
            else
                Keyboard.ClearFocus();
        }
        public void TrytoConnect(string nick, string pasw)
        {
            CurrentUser = new UserItem();
            db = new database(nick, pasw,CurrentUser);
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            uc2.lst1.Items.Add(uc2.rolebox2.Text +", "+uc2.nameparent.Text);
        }
        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (uc2.lst1.SelectedIndex >= 0)
                uc2.lst1.Items.RemoveAt(uc2.lst1.SelectedIndex);
        }
        public void Button_Click_2(object sender, RoutedEventArgs e,string nick,string pasw,string email,string role)
        {
            db = new database();
            db.CreateAccount(nick,pasw,email,role);
            db = null;
        }
        public void Button_Click_3(object sender, RoutedEventArgs e,string a1,string a2)
        {
            db = new database();
            db.SendNewPassword(a1,a2);
            db = null;
        }
        public void Button_Click_4(object sender, RoutedEventArgs e,string a1,string a2)
        {
            db = new database();
            db.SendUsername(a1,a2);
            db = null;
        }

        public void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            uc2.Dispose();
            uc1 = new UserControl1();
            layout1.Children.Clear();
            layout1.Children.Add(uc1);
        }
        public void Hyperlink_Click2(object sender, RoutedEventArgs e)
        {
            uc3.Dispose(); 
            uc4 = new Signup();
            layout1.Children.Clear();
            layout1.Children.Add(uc4);
        }
        public void Hyperlink_Click3(object sender, RoutedEventArgs e)
        {
            uc4.Dispose();
            uc3 = new UserControl3();
            layout1.Children.Clear();
            layout1.Children.Add(uc3);
        }
        public void Hyperlink_Click4(object sender, RoutedEventArgs e)
        {
            uc3.Dispose();
            uc5 = new ForgotPasw();
            layout1.Children.Clear();
            layout1.Children.Add(uc5);
        }
        public void Hyperlink_Click5(object sender, RoutedEventArgs e)
        {
            uc5.Dispose();
            uc3 = new UserControl3();
            layout1.Children.Clear();
            layout1.Children.Add(uc3);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (accesssoftware)
                CreateSoftware();
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

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }
        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }
        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }
        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
    }
}
