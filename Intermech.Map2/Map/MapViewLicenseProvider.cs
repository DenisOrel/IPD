// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapViewLicenseProvider
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    internal sealed class MapViewLicenseProvider : LicenseProvider
    {
      private static bool err;
      private static readonly string GONAME = "MapDiagram";

      static MapViewLicenseProvider() => MapViewLicenseProvider.err = false;

      private int Dispose(string keystring, bool run)
      {
        int num1 = 0;
        if (keystring.Length == 0)
          return 0;
        try
        {
          byte[] buffer1 = Convert.FromBase64String(keystring);
          byte[] numArray = new byte[16 /*0x10*/]
          {
            (byte) 33,
            (byte) 83,
            (byte) 27,
            (byte) 95,
            (byte) 28,
            (byte) 84,
            (byte) 197,
            (byte) 169,
            (byte) 39,
            (byte) 93,
            (byte) 75,
            (byte) 105,
            (byte) 82,
            (byte) 97,
            (byte) 49,
            (byte) 44
          };
          CryptoStream cryptoStream = new CryptoStream((Stream) new MemoryStream(buffer1), new RijndaelManaged().CreateDecryptor(numArray, numArray), CryptoStreamMode.Read);
          byte[] bytes = new byte[4096 /*0x1000*/];
          byte[] buffer2 = bytes;
          int length1 = bytes.Length;
          int num2 = cryptoStream.Read(buffer2, 0, length1);
          Decoder decoder = new UTF8Encoding().GetDecoder();
          char[] chars1 = new char[decoder.GetCharCount(bytes, 0, num2)];
          int chars2 = decoder.GetChars(bytes, 0, num2, chars1, 0);
          string[] strArray = new string(chars1, 0, chars2).Split('|');
          int length2 = strArray.Length;
          DateTime today = DateTime.Today;
          CultureInfo currentCulture = CultureInfo.CurrentCulture;
          if (run)
          {
            string strA1 = length2 > 0 ? strArray[0] : "";
            string strA2 = length2 > 1 ? strArray[1] : "";
            int num3 = length2 > 2 ? this.ParseInt(strArray[2]) : -1;
            int num4 = length2 > 3 ? this.ParseInt(strArray[3]) : -1;
            int num5 = length2 > 9 ? this.ParseInt(strArray[9]) : 7;
            string str = length2 > 10 ? strArray[10] : "E";
            if (length2 < 11)
              return 0;
            int year = length2 > 11 ? this.ParseInt(strArray[11]) : 9999;
            int month = length2 > 12 ? this.ParseInt(strArray[12]) : 1;
            int day = length2 > 13 ? this.ParseInt(strArray[13]) : 1;
            int num6 = length2 > 14 ? this.ParseInt(strArray[14]) : 360;
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();
            if (strA2.Length > 0 && string.Compare(strA2, name.Name, true, currentCulture) != 0 || num3 >= 0 && (name.Version.Major > num3 || num4 >= 0 && name.Version.Major == num3 && name.Version.Minor > num4) || str[0] == 'E' || str[0] == 'R' && (MapView.myVersionAssembly == (Assembly) null || string.Compare(strA1, MapView.myVersionAssembly.GetName().Name, true, currentCulture) != 0))
              return 0;
            DateTime dateTime = new DateTime(year, month, day);
            if (today.AddDays((double) num6) <= dateTime)
              return 4;
            if (today.AddDays(7.0) <= dateTime)
              return 6;
            return today.AddDays((double) -num5) <= dateTime ? 5 : 0;
          }
          string strA = length2 > 1 ? strArray[1] : "";
          int num7 = length2 > 2 ? this.ParseInt(strArray[2]) : -1;
          int num8 = length2 > 3 ? this.ParseInt(strArray[3]) : -1;
          string strB1 = length2 > 4 ? strArray[4] : "";
          string strB2 = length2 > 5 ? strArray[5] : "";
          DateTime dateTime1 = new DateTime(length2 > 6 ? this.ParseInt(strArray[6]) : 1, length2 > 7 ? this.ParseInt(strArray[7]) : 1, length2 > 8 ? this.ParseInt(strArray[8]) : 1);
          int num9 = length2 > 9 ? this.ParseInt(strArray[9]) : 7;
          string str1 = length2 > 10 ? strArray[10] : "E";
          int num10 = length2 > 14 ? this.ParseInt(strArray[14]) : 360;
          AssemblyName name1 = Assembly.GetExecutingAssembly().GetName();
          if (strA.Length > 0 && string.Compare(strA, name1.Name, true, currentCulture) != 0 || num7 >= 0 && (name1.Version.Major > num7 || num8 >= 0 && name1.Version.Major == num7 && name1.Version.Minor > num8) || strB1.Length > 0 && string.Compare(Environment.MachineName, strB1, true, currentCulture) != 0 || strB2.Length > 0 && string.Compare(Environment.UserName, strB2, true, currentCulture) != 0)
            return 0;
          if (str1[0] == 'B')
          {
            if (today.AddDays((double) num10) <= dateTime1)
              return 4;
            if (today.AddDays(7.0) <= dateTime1)
              return 6;
            return today.AddDays((double) -num9) <= dateTime1 ? 5 : 0;
          }
          if (today.AddDays(7.0) <= dateTime1)
            return 2;
          if (today.AddDays((double) -num9) > dateTime1)
            return 0;
          num1 = 1;
        }
        catch (Exception ex)
        {
        }
        return num1;
      }

      public override License GetLicense(
        LicenseContext context,
        System.Type type,
        object instance,
        bool allowExceptions)
      {
        string str1 = $"\nEnvironment info:\nversion: {MapView.VersionName}\n";
        string str2;
        if (Assembly.GetEntryAssembly() != (Assembly) null)
        {
          string str3 = str1 + "entry: ";
          string str4;
          try
          {
            str4 = str3 + Assembly.GetEntryAssembly().FullName;
          }
          catch (SecurityException ex)
          {
            str4 = str3 + "?fn?";
          }
          object obj = (object) null;
          try
          {
            obj = (object) Assembly.GetEntryAssembly().EntryPoint;
          }
          catch (SecurityException ex)
          {
            str4 += "?ep?";
          }
          str2 = obj == null ? str4 + ", no entry point\n" : str4 + ", has entry point\n";
        }
        else
          str2 = str1 + "null entry\n";
        int c = 0;
        try
        {
          string str5;
          try
          {
            str5 = context.GetSavedLicenseKey(type, (Assembly) null);
          }
          catch (SecurityException ex)
          {
            str5 = (string) null;
            str2 = $"{str2}\n{ex.ToString()}";
          }
          str2 = str5 == null ? str2 + "null key\n" : $"{str2}key: {str5}\n";
          if (str5 == null || str5.Length == 0)
          {
            try
            {
              foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
              {
                str2 = $"{str2}{assembly.FullName}\n";
                try
                {
                  str5 = context.GetSavedLicenseKey(type, assembly);
                }
                catch (SecurityException ex)
                {
                  str5 = (string) null;
                }
                if (str5 != null)
                {
                  if (str5.Length > 0)
                    break;
                }
              }
            }
            catch (SecurityException ex)
            {
            }
            str2 = $"{str2}{MapView.myVersionName}\n";
            str2 = !(MapView.myVersionAssembly != (Assembly) null) ? str2 + "null licensed assembly\n" : $"{str2}{MapView.myVersionAssembly.GetName().Name}\n";
            if (MapView.myVersionName.Length > 24)
              str5 = MapView.myVersionName;
          }
          if (str5 != null && str5.Length > 0)
          {
            c = this.Dispose(str5, true);
            if (c == 4)
              return (License) new MapViewLicenseProvider.MapViewLicense(str5, c);
          }
          else
          {
            string str6 = "";
            RegistryKey registryKey = (RegistryKey) null;
            try
            {
              registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Northwoods Software\\Go.NET");
            }
            catch (SecurityException ex)
            {
              str2 = $"{str2}\n{ex.ToString()}";
            }
            if (registryKey == null)
            {
              try
              {
                registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Northwoods Software\\GoDiagram");
              }
              catch (SecurityException ex)
              {
              }
            }
            if (registryKey != null)
            {
              string name = type.Assembly.GetName().Name;
              object inArray = (object) null;
              try
              {
                inArray = registryKey.GetValue(name);
              }
              catch (SecurityException ex)
              {
              }
              if (inArray != null && inArray is byte[])
                str6 = Convert.ToBase64String((byte[]) inArray);
            }
            c = this.Dispose(str6, false);
            if (c == 0 && registryKey != null)
            {
              str6 = "";
              string name = type.Assembly.GetName().Name + " eval";
              object inArray = (object) null;
              try
              {
                inArray = registryKey.GetValue(name);
              }
              catch (SecurityException ex)
              {
              }
              if (inArray != null && inArray is byte[])
                str6 = Convert.ToBase64String((byte[]) inArray);
              c = this.Dispose(str6, false);
            }
            if (c >= 4 && context.UsageMode == LicenseUsageMode.Designtime)
              context.SetSavedLicenseKey(type, str6);
            if (c != 4)
            {
              if (context.UsageMode != LicenseUsageMode.Designtime)
                goto label_52;
            }
            return (License) new MapViewLicenseProvider.MapViewLicense(str6, c);
          }
        }
        catch (Exception ex)
        {
          str2 = $"{str2}\n{ex.ToString()}";
        }
    label_52:
        switch (c & 3)
        {
          case 1:
            string str7 = c > 4 ? "beta" : "evaluation";
            string str8 = $"Built using {MapViewLicenseProvider.GONAME} for .NET Windows Forms {this.StringFloat(MapView.Version)}{Environment.NewLine}Copyright © Northwoods Software, 1998-2004.  All Rights Reserved.{Environment.NewLine}This {str7} copy of {MapViewLicenseProvider.GONAME} is about to expire.{Environment.NewLine}{Environment.NewLine}DO NOT DISTRIBUTE OR DEPLOY THIS SOFTWARE.{Environment.NewLine}";
            if (c < 4)
              str8 = $"{str8}{Environment.NewLine}Please purchase a license at {Environment.NewLine}";
            if (SystemInformation.UserInteractive)
            {
              int num1 = (int) MessageBox.Show(str8, type.Name + " License Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
              Console.WriteLine(type.Name + " License Check");
              Console.WriteLine(str8);
            }
            return (License) new MapViewLicenseProvider.MapViewLicense(str8, c);
          case 2:
            string str9 = c > 4 ? "beta" : "evaluation";
            string str10 = $"Built using {MapViewLicenseProvider.GONAME} for .NET Windows Forms {this.StringFloat(MapView.Version)}{Environment.NewLine}Copyright © Northwoods Software, 1998-2004.  All Rights Reserved.{Environment.NewLine}This software is licensed for a limited {str9} period.{Environment.NewLine}{Environment.NewLine}DO NOT DISTRIBUTE OR DEPLOY THIS SOFTWARE.{Environment.NewLine}";
            if (c < 4)
              str10 = $"{str10}{Environment.NewLine}Please purchase a license at {Environment.NewLine}";
            if (SystemInformation.UserInteractive)
            {
              if (!MapViewLicenseProvider.err)
              {
                MapViewLicenseProvider.err = true;
                int num2 = (int) MessageBox.Show(str10, type.Name + " License Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              }
            }
            else
            {
              Console.WriteLine(type.Name + " License Check");
              Console.WriteLine(str10);
            }
            return (License) new MapViewLicenseProvider.MapViewLicense(str10, c);
          default:
            string str11 = $"Built using {MapViewLicenseProvider.GONAME} for .NET Windows Forms {this.StringFloat(MapView.Version)}{Environment.NewLine}Copyright © Northwoods Software, 1998-2004.  All Rights Reserved.{Environment.NewLine}The license for this copy of {MapViewLicenseProvider.GONAME} is invalid or has expired.{Environment.NewLine}{Environment.NewLine}Please purchase a license at {Environment.NewLine}If you have already purchased a {MapViewLicenseProvider.GONAME} development license,{Environment.NewLine}  have you requested an Unlock Code for your development machine by running the GoDiagram LicenseManager?{Environment.NewLine}If you have already entered an Unlock Code in the LicenseManager,{Environment.NewLine}  did you link license objects into your application via the Microsoft license compiler?{Environment.NewLine}  (Make sure the needed components and correct VERSION are listed in the LICENSES.LICX file,{Environment.NewLine}   and that the LICENSES.LICX file is part of your EXECUTABLE's project, not in a DLL.){Environment.NewLine}" + str2;
            if (SystemInformation.UserInteractive)
            {
              int num3 = (int) MessageBox.Show(str11, type.Name + " License Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
              Console.WriteLine(type.Name + " License Check");
              Console.WriteLine(str11);
            }
            if (allowExceptions)
              throw new LicenseException(type, instance, str11);
            return (License) null;
        }
      }

      private int ParseInt(string s) => int.Parse(s, (IFormatProvider) NumberFormatInfo.InvariantInfo);

      private string StringFloat(float f)
      {
        return f.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
      }

      [Serializable]
      internal sealed class MapViewLicense : License, ISerializable
      {
        [NonSerialized]
        internal SolidBrush myBrush;
        [NonSerialized]
        internal Font myFont;
        private string myKey;
        internal Random myRandom;

        internal MapViewLicense(SerializationInfo info, StreamingContext context)
        {
          this.myRandom = (Random) null;
          this.myFont = (Font) null;
          this.myBrush = (SolidBrush) null;
          this.myKey = (string) null;
          this.myRandom = (Random) info.GetValue(nameof (myRandom), typeof (Random));
          this.myKey = (string) info.GetValue(nameof (myKey), typeof (string));
        }

        internal MapViewLicense(string key, int c)
        {
          this.myRandom = (Random) null;
          this.myFont = (Font) null;
          this.myBrush = (SolidBrush) null;
          this.myKey = (string) null;
          this.myKey = key;
          this.myRandom = new Random();
          this.myBrush = new SolidBrush(System.Drawing.Color.FromArgb((int) byte.MaxValue, Math.Min((int) byte.MaxValue, Math.Max(0, 80 /*0x50*/ + this.myRandom.Next(100))), Math.Min((int) byte.MaxValue, Math.Max(0, 80 /*0x50*/ + this.myRandom.Next(100))), Math.Min((int) byte.MaxValue, Math.Max(0, 80 /*0x50*/ + this.myRandom.Next(100))) & -8 | c));
          this.myFont = new Font("Microsoft Sans Serif", (float) (8 + this.myRandom.Next(4)));
        }

        public override void Dispose()
        {
          if (this.myFont != null)
          {
            this.myFont.Dispose();
            this.myFont = (Font) null;
          }
          if (this.myBrush == null)
            return;
          this.myBrush.Dispose();
          this.myBrush = (SolidBrush) null;
        }

        internal void Dispose(MapView view)
        {
          Rectangle clipRectangle = view.myPaintEventArgs.ClipRectangle;
          view.myGraphics.DrawImage((Image) view.myBuffer, clipRectangle, clipRectangle, GraphicsUnit.Pixel);
          if (((int) this.myBrush.Color.B & 7) >= 4)
            return;
          view.myGraphics.DrawString(MapViewLicenseProvider.GONAME + ", for evaluation only\r\nwww.intermech.ru", this.myFont, (Brush) this.myBrush, 10f, 10f);
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
          info.AddValue("myRandom", (object) this.myRandom);
          info.AddValue("myKey", (object) this.myKey);
        }

        public override string LicenseKey => this.myKey;
      }
    }
}
