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
    /// Logica di interazione per UserControl3.xaml
    /// </summary>
    public partial class UserControl3 : UserControl, IDisposable
    {
        private bool disposed;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public UserControl3()
        {
            InitializeComponent();
        }
        ~UserControl3()
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
        MainWindow.main.Login_Click(sender, e, userlogin.Text, paswlogin.Password);
    }

    private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            MainWindow.main.DragMove();
        }
        catch (Exception ex)
        {
            ;
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.main.Hyperlink_Click2(sender, e);
    }

    private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
    {
        MainWindow.main.Hyperlink_Click4(sender, e);
    }

  }
}
