
// Type: Intermech.Interfaces.InformationCollector.OSInformation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Microsoft.Win32;
using System;


namespace Intermech.Interfaces.InformationCollector
{
    public static class OSInformation
    {
      /// <summary>
      ///  получить версию и разрядность Windows
      ///  http://support.microsoft.com/kb/304283
      /// </summary>
      /// <returns></returns>
      public static string GetOSInfo()
      {
        OperatingSystem osVersion = Environment.OSVersion;
        Version version = osVersion.Version;
        string osInfo = "";
        try
        {
          using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false))
          {
            if (registryKey != null)
              osInfo = registryKey.GetValue("ProductName", (object) string.Empty).ToString();
            if (!string.IsNullOrEmpty(osInfo))
            {
              if (!string.IsNullOrEmpty(osVersion.ServicePack))
                osInfo = $"{osInfo} {osVersion.ServicePack}";
              osInfo = $"{osInfo} {(object) OSInformation.GetOSArchitecture()}-bit";
              return osInfo;
            }
          }
        }
        catch
        {
          osInfo = string.Empty;
        }
        if (osVersion.Platform == PlatformID.Win32Windows)
        {
          switch (version.Minor)
          {
            case 0:
              osInfo = "95";
              break;
            case 10:
              osInfo = !(version.Revision.ToString() == "2222A") ? "98" : "98SE";
              break;
            case 90:
              osInfo = "Me";
              break;
          }
        }
        else if (osVersion.Platform == PlatformID.Win32NT)
        {
          switch (version.Major)
          {
            case 3:
              osInfo = "NT 3.51";
              break;
            case 4:
              osInfo = "NT 4.0";
              break;
            case 5:
              if (version.Minor == 0)
              {
                osInfo = "2000";
                break;
              }
              if (version.Minor == 1)
              {
                osInfo = "XP";
                break;
              }
              if (version.Minor == 2)
              {
                osInfo = " 2003";
                break;
              }
              break;
            case 6:
              osInfo = version.Minor != 0 ? (version.Minor != 1 ? (version.Minor != 2 ? "8.1" : "8") : "7") : "Vista";
              break;
            case 10:
              osInfo = "10";
              break;
          }
        }
        if (osInfo != "")
        {
          string str = "Windows " + osInfo;
          if (osVersion.ServicePack != "")
            str = $"{str} {osVersion.ServicePack}";
          osInfo = $"{str} {OSInformation.GetOSArchitecture().ToString()}-bit";
        }
        return osInfo;
      }

      /// <summary>
      /// Even though the physical CPU's architecture may support 64-bit operations,
      /// this environment variable will always return the architecture of the OS.
      /// </summary>
      /// <returns></returns>
      public static int GetOSArchitecture()
      {
        return !Environment.Is64BitOperatingSystem ? 32 /*0x20*/ : 64 /*0x40*/;
      }
    }
}
