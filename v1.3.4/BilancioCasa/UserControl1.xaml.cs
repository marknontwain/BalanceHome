﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;


namespace BilancioCasa
{
    /// <summary>
    /// Logica di interazione per UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, IDisposable
    {
        private bool disposed;
        private bool invalid;

        private bool emailcan;
        private bool nickcan;
        private bool passwordcan;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        string redhex = "#FF9999";
        public UserControl1()
        {
            InitializeComponent();
        }

    ~UserControl1()
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
          if(emailcan && nickcan && passwordcan && rolebox.SelectedIndex > -1)
          MainWindow.main.Config_Click(sender, e);
          else
          {
              MessageBox.Show("Please compile all fields correctly", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
              Keyboard.ClearFocus();
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
      private void emailbox_TextChanged(object sender, TextChangedEventArgs e)
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
      private bool PasswordMatch(string xb, string yb)
      {
            if(xb.Equals(yb))
                return true;
            else return false;
      }     
      private void nickac_TextChanged(object sender, TextChangedEventArgs e)
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
              if(!string.IsNullOrEmpty(pasconfirm.Password))
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
      private void pasconfirm_PasswordChanged(object sender, RoutedEventArgs e)
      {
          if(!string.IsNullOrEmpty(pasconfirm.Password))
          {
              if(!PasswordMatch(paswac.Password, pasconfirm.Password))
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
              if(!string.IsNullOrEmpty(paswac.Password))
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

      
    }

}
