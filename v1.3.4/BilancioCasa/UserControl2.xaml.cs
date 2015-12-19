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
    /// Logica di interazione per UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl,IDisposable
    {
        private bool disposed;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public UserControl2()
        {
            InitializeComponent();
        }
        ~UserControl2()
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

      private void Button_Click(object sender, RoutedEventArgs e)
      {
          MainWindow.main.Button_Click_1(sender, e);
      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
          MainWindow.main.Button_Click(sender, e);
      }

      private void Hyperlink_Click(object sender, RoutedEventArgs e)
      {
          MainWindow.main.Hyperlink_Click(sender, e);
      }

      private void Button_Click_2(object sender, RoutedEventArgs e)
      {
          MainWindow.main.Config2_Click(sender, e);
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
    }
}
