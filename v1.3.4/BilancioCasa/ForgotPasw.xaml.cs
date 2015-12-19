using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per ForgotPasw.xaml
    /// </summary>
    public partial class ForgotPasw : UserControl, IDisposable
    {
        private bool disposed;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        public ForgotPasw()
        {
            InitializeComponent();
        }

        ~ForgotPasw()
        {
            this.Dispose(false);
        }

        public void Dispose()
         {
            this.Dispose(true);
            GC.SuppressFinalize(this);
         }
        protected virtual void Dispose(bool disposing)
         {
             if (!disposed)
             {
                 if (disposing)
                 {
                     handle.Dispose();
                 }
                 disposed = true;
             }
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow.main.DragMove();
            }
            catch (Exception ex)
            {
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Hyperlink_Click5(sender,e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nickac.Text) && !string.IsNullOrEmpty(emailac.Text))
            {
                MainWindow.main.Button_Click_3(sender, e, nickac.Text, emailac.Text);
            }
            else
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); 
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(emailac2.Text) && !string.IsNullOrEmpty(paswac.Password))
            {
                MainWindow.main.Button_Click_4(sender, e,emailac2.Text, paswac.Password);
            }
            else
                MessageBox.Show("Please fill out default fields", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
