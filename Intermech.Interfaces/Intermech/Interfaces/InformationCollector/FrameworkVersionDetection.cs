
// Type: Intermech.Interfaces.InformationCollector.FrameworkVersionDetection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;


namespace Intermech.Interfaces.InformationCollector
{
    /// <summary>
    /// Provides support for determining if a specific version of the .NET
    /// Framework runtime is installed and the service pack level for the
    /// runtime version.
    /// </summary>
    public static class FrameworkVersionDetection
    {
      private const string Netfx10RegKeyName = "Software\\Microsoft\\.NETFramework\\Policy\\v1.0";
      private const string Netfx10RegKeyValue = "3705";
      private const string Netfx10SPxMSIRegKeyName = "Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}";
      private const string Netfx10SPxOCMRegKeyName = "Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}";
      private const string Netfx10SPxRegValueName = "Version";
      private const string Netfx11RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322";
      private const string Netfx20RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727";
      private const string Netfx30RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup";
      private const string Netfx35RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5";
      private const string Netfx40ClientRegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client";
      private const string Netfx40FullRegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full";
      private const string Netfx11PlusRegValueName = "Install";
      private const string Netfx30PlusRegValueName = "InstallSuccess";
      private const string Netfx11PlusSPxRegValueName = "SP";
      private const string Netfx20PlusBuildRegValueName = "Increment";
      private const string Netfx30PlusVersionRegValueName = "Version";
      private const string Netfx35PlusBuildRegValueName = "Build";
      private const int netfx30VersionMajor = 3;
      private const int netfx30VersionMinor = 0;
      private const int netfx30VersionBuild = 4506;
      private const int netfx30VersionRevision = 26;
      private const int netfx35VersionMajor = 3;
      private const int netfx35VersionMinor = 5;
      private const int netfx35VersionBuild = 21022;
      private const int netfx35VersionRevision = 8;
      private const int netfx40VersionMajor = 4;
      private const int netfx40VersionMinor = 0;
      private const int netfx40VersionBuild = 30319;
      private const int netfx40VersionRevision = 0;

      [DllImport("user32.dll", SetLastError = true)]
      internal static extern int GetSystemMetrics(SystemMetric smIndex);

      private static bool GetRegistryValue<T>(
        RegistryHive hive,
        string key,
        string value,
        RegistryValueKind kind,
        out T data)
      {
        bool registryValue = false;
        data = default (T);
        using (RegistryKey registryKey1 = RegistryKey.OpenRemoteBaseKey(hive, string.Empty))
        {
          if (registryKey1 != null)
          {
            using (RegistryKey registryKey2 = registryKey1.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
            {
              if (registryKey2 != null)
              {
                if (registryKey2.GetValueKind(value) == kind)
                {
                  object obj = registryKey2.GetValue(value, (object) null);
                  if (obj != null)
                  {
                    data = (T) Convert.ChangeType(obj, typeof (T), (IFormatProvider) CultureInfo.InvariantCulture);
                    registryValue = true;
                  }
                }
              }
            }
          }
        }
        return registryValue;
      }

      private static bool IsNetfx10Installed()
      {
        string data = string.Empty;
        return FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\.NETFramework\\Policy\\v1.0", "3705", RegistryValueKind.String, out data);
      }

      private static bool IsNetfx11Installed()
      {
        bool flag = false;
        int data = 0;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322", "Install", RegistryValueKind.DWord, out data) && data == 1)
          flag = true;
        return flag;
      }

      private static bool IsNetfx20Installed()
      {
        bool flag = false;
        int data = 0;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727", "Install", RegistryValueKind.DWord, out data) && data == 1)
          flag = true;
        return flag;
      }

      private static bool IsNetfx30Installed()
      {
        bool flag = false;
        int data = 0;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup", "InstallSuccess", RegistryValueKind.DWord, out data) && data == 1)
          flag = true;
        Version exactVersion = FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx30);
        return flag && FrameworkVersionDetection.CheckNetfxBuildNumber(exactVersion, 3, 0, 4506, 26);
      }

      private static bool IsNetfx35Installed()
      {
        bool flag = false;
        int data = 0;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5", "Install", RegistryValueKind.DWord, out data) && data == 1)
          flag = true;
        Version exactVersion = FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx35);
        return flag && FrameworkVersionDetection.CheckNetfxBuildNumber(exactVersion, 3, 5, 21022, 8);
      }

      private static bool IsNetfx40ClientInstalled()
      {
        bool flag = false;
        int data = 0;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client", "Install", RegistryValueKind.DWord, out data) && data == 1)
          flag = true;
        Version exactVersion = FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx40C);
        return flag && FrameworkVersionDetection.CheckNetfxBuildNumber(exactVersion, 4, 0, 30319, 0);
      }

      private static bool IsNetfx40FullInstalled()
      {
        bool flag = false;
        int data = 0;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full", "Install", RegistryValueKind.DWord, out data) && data == 1)
          flag = true;
        Version exactVersion = FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx40F);
        return flag && FrameworkVersionDetection.CheckNetfxBuildNumber(exactVersion, 4, 0, 30319, 0);
      }

      /// <summary>
      /// Retrieves the .NET Framework build number from
      /// the registry and validates that it is not a pre-release version number
      /// </summary>
      /// <param name="registryVersion"></param>
      /// <param name="iRequestedVersionMajor"></param>
      /// <param name="iRequestedVersionMinor"></param>
      /// <param name="iRequestedVersionBuild"></param>
      /// <param name="iRequestedVersionRevision"></param>
      public static bool CheckNetfxBuildNumber(
        Version registryVersion,
        int iRequestedVersionMajor,
        int iRequestedVersionMinor,
        int iRequestedVersionBuild,
        int iRequestedVersionRevision)
      {
        return registryVersion.Major > iRequestedVersionMajor || registryVersion.Major == iRequestedVersionMajor && (registryVersion.Minor > iRequestedVersionMinor || registryVersion.Minor == iRequestedVersionMinor && (registryVersion.Build > iRequestedVersionBuild || registryVersion.Build == iRequestedVersionBuild && (registryVersion.Revision == -1 || registryVersion.Revision >= iRequestedVersionRevision)));
      }

      private static int GetNetfx10SPLevel()
      {
        int result = -1;
        string data;
        if (!FrameworkVersionDetection.IsTabletOrMediaCenter() ? FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}", "Version", RegistryValueKind.String, out data) : FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}", "Version", RegistryValueKind.String, out data))
        {
          int num = data.LastIndexOf(',');
          if (num > 0)
            int.TryParse(data.Substring(num + 1), out result);
        }
        return result;
      }

      private static int GetNetfx11SPLevel()
      {
        int data = 0;
        int netfx11SpLevel = -1;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322", "SP", RegistryValueKind.DWord, out data))
          netfx11SpLevel = data;
        return netfx11SpLevel;
      }

      private static int GetNetfx20SPLevel()
      {
        int data = 0;
        int netfx20SpLevel = -1;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727", "SP", RegistryValueKind.DWord, out data))
          netfx20SpLevel = data;
        return netfx20SpLevel;
      }

      private static int GetNetfx30SPLevel() => -1;

      private static int GetNetfx35SPLevel()
      {
        int data = 0;
        int netfx35SpLevel = -1;
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5", "SP", RegistryValueKind.DWord, out data))
          netfx35SpLevel = data;
        return netfx35SpLevel;
      }

      private static Version GetNetfx10ExactVersion()
      {
        Version netfx10ExactVersion = new Version();
        string data;
        if (!FrameworkVersionDetection.IsTabletOrMediaCenter() ? FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}", "Version", RegistryValueKind.String, out data) : FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}", "Version", RegistryValueKind.String, out data))
        {
          int length = data.LastIndexOf(',');
          if (length > 0)
          {
            string[] strArray = data.Substring(0, length).Split(',');
            if (strArray.Length == 3)
              netfx10ExactVersion = new Version(Convert.ToInt32(strArray[0], (IFormatProvider) NumberFormatInfo.InvariantInfo), Convert.ToInt32(strArray[1], (IFormatProvider) NumberFormatInfo.InvariantInfo), Convert.ToInt32(strArray[2], (IFormatProvider) NumberFormatInfo.InvariantInfo));
          }
        }
        return netfx10ExactVersion;
      }

      private static Version GetNetfx11ExactVersion()
      {
        int data = 0;
        Version netfx11ExactVersion = new Version();
        if (FrameworkVersionDetection.GetRegistryValue<int>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322", "Install", RegistryValueKind.DWord, out data) && data == 1)
        {
          string[] strArray = "Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322".Split(new string[1]
          {
            "NDP\\v"
          }, StringSplitOptions.None);
          if (strArray.Length == 2)
            netfx11ExactVersion = new Version(strArray[1]);
        }
        return netfx11ExactVersion;
      }

      private static Version GetNetfx20ExactVersion()
      {
        string data = string.Empty;
        Version netfx20ExactVersion = new Version();
        if (FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727", "Increment", RegistryValueKind.String, out data) && !string.IsNullOrEmpty(data))
        {
          string[] strArray1 = "Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727".Split(new string[1]
          {
            "NDP\\v"
          }, StringSplitOptions.None);
          if (strArray1.Length == 2)
          {
            string[] strArray2 = strArray1[1].Split('.');
            if (strArray2.Length == 3)
              netfx20ExactVersion = new Version(Convert.ToInt32(strArray2[0], (IFormatProvider) NumberFormatInfo.InvariantInfo), Convert.ToInt32(strArray2[1], (IFormatProvider) NumberFormatInfo.InvariantInfo), Convert.ToInt32(strArray2[2], (IFormatProvider) NumberFormatInfo.InvariantInfo), Convert.ToInt32(data, (IFormatProvider) NumberFormatInfo.InvariantInfo));
          }
        }
        return netfx20ExactVersion;
      }

      private static Version GetNetfx30ExactVersion()
      {
        string data = string.Empty;
        Version netfx30ExactVersion = new Version();
        if (FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup", "Version", RegistryValueKind.String, out data) && !string.IsNullOrEmpty(data))
          netfx30ExactVersion = new Version(data);
        return netfx30ExactVersion;
      }

      private static Version GetNetfx35ExactVersion()
      {
        string data = string.Empty;
        Version netfx35ExactVersion = new Version();
        if (FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5", "Version", RegistryValueKind.String, out data) && !string.IsNullOrEmpty(data))
          netfx35ExactVersion = new Version(data);
        return netfx35ExactVersion;
      }

      private static Version GetNetfx40ClientExactVersion()
      {
        string data = string.Empty;
        Version clientExactVersion = new Version();
        if (FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client", "Version", RegistryValueKind.String, out data) && !string.IsNullOrEmpty(data))
          clientExactVersion = new Version(data);
        return clientExactVersion;
      }

      private static Version GetNetfx40FullExactVersion()
      {
        string data = string.Empty;
        Version fullExactVersion = new Version();
        if (FrameworkVersionDetection.GetRegistryValue<string>(RegistryHive.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full", "Version", RegistryValueKind.String, out data) && !string.IsNullOrEmpty(data))
          fullExactVersion = new Version(data);
        return fullExactVersion;
      }

      private static bool IsTabletOrMediaCenter()
      {
        return FrameworkVersionDetection.GetSystemMetrics(SystemMetric.SM_TABLETPC) != 0 || FrameworkVersionDetection.GetSystemMetrics(SystemMetric.SM_MEDIACENTER) != 0;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="results"></param>
      /// <param name="fVersion"></param>
      private static void FormVersionString(List<string> results, FrameworkVersion fVersion)
      {
        Version exactVersion = FrameworkVersionDetection.GetExactVersion(fVersion);
        int servicePackLevel = FrameworkVersionDetection.GetServicePackLevel(fVersion);
        string str1;
        switch (fVersion)
        {
          case FrameworkVersion.Fx40C:
            str1 = "Client ";
            break;
          case FrameworkVersion.Fx40F:
            str1 = "Full ";
            break;
          default:
            str1 = string.Empty;
            break;
        }
        string str2 = str1 + exactVersion.ToString();
        if (servicePackLevel != -1)
          str2 = $"{str2} SP{(object) servicePackLevel}";
        results.Add(str2);
      }

      /// <summary>найти все установленные версии framework</summary>
      /// <returns></returns>
      public static List<string> SearchFrameworkVersionsInstalled()
      {
        List<string> results = new List<string>();
        bool flag1 = false;
        bool flag2 = false;
        if (FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx10))
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx10);
        if (FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx11))
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx11);
        if (FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx20))
        {
          flag1 = true;
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx20);
        }
        if (flag1 && FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx30))
        {
          flag2 = true;
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx30);
        }
        if (flag1 & flag2 && FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx35))
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx35);
        if (FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx40C))
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx40C);
        if (FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx40F))
          FrameworkVersionDetection.FormVersionString(results, FrameworkVersion.Fx40F);
        return results;
      }

      /// <summary>
      /// Determines if the specified .NET Framework version is installed
      /// on the local computer.
      /// </summary>
      /// <param name="frameworkVersion">One of the
      /// <see cref="T:Intermech.Interfaces.InformationCollector.FrameworkVersion" /> values.</param>
      /// <returns><see langword="true" /> if the specified .NET Framework
      /// version is installed; otherwise <see langword="false" />.</returns>
      public static bool IsInstalled(FrameworkVersion frameworkVersion)
      {
        bool flag = false;
        switch (frameworkVersion)
        {
          case FrameworkVersion.Fx10:
            flag = FrameworkVersionDetection.IsNetfx10Installed();
            break;
          case FrameworkVersion.Fx11:
            flag = FrameworkVersionDetection.IsNetfx11Installed();
            break;
          case FrameworkVersion.Fx20:
            flag = FrameworkVersionDetection.IsNetfx20Installed();
            break;
          case FrameworkVersion.Fx30:
            flag = FrameworkVersionDetection.IsNetfx30Installed();
            break;
          case FrameworkVersion.Fx35:
            flag = FrameworkVersionDetection.IsNetfx35Installed();
            break;
          case FrameworkVersion.Fx40C:
            flag = FrameworkVersionDetection.IsNetfx40ClientInstalled();
            break;
          case FrameworkVersion.Fx40F:
            flag = FrameworkVersionDetection.IsNetfx40FullInstalled();
            break;
        }
        return flag;
      }

      /// <summary>
      /// Retrieves the service pack level for the specified .NET Framework
      /// version.
      /// </summary>
      /// <param name="frameworkVersion">One of the
      /// <see cref="T:Intermech.Interfaces.InformationCollector.FrameworkVersion" /> values.</param>
      /// <returns>An <see cref="T:System.Int32">integer</see> value representing
      /// the service pack level for the specified .NET Framework version. If
      /// the specified .NET Frameowrk version is not found, -1 is returned.
      /// </returns>
      public static int GetServicePackLevel(FrameworkVersion frameworkVersion)
      {
        int servicePackLevel = -1;
        switch (frameworkVersion)
        {
          case FrameworkVersion.Fx10:
            servicePackLevel = FrameworkVersionDetection.GetNetfx10SPLevel();
            break;
          case FrameworkVersion.Fx11:
            servicePackLevel = FrameworkVersionDetection.GetNetfx11SPLevel();
            break;
          case FrameworkVersion.Fx20:
            servicePackLevel = FrameworkVersionDetection.GetNetfx20SPLevel();
            break;
          case FrameworkVersion.Fx30:
            servicePackLevel = FrameworkVersionDetection.GetNetfx30SPLevel();
            break;
          case FrameworkVersion.Fx35:
            servicePackLevel = FrameworkVersionDetection.GetNetfx35SPLevel();
            break;
        }
        return servicePackLevel;
      }

      /// <summary>
      /// Retrieves the exact version number for the specified .NET Framework
      /// version.
      /// </summary>
      /// <param name="frameworkVersion">One of the
      /// <see cref="T:Intermech.Interfaces.InformationCollector.FrameworkVersion" /> values.</param>
      /// <returns>A <see cref="T:System.Version">version</see> representing
      /// the exact version number for the specified .NET Framework version.
      /// If the specified .NET Frameowrk version is not found, a
      /// <see cref="T:System.Version" /> is returned that represents a 0.0.0.0 version
      /// number.
      /// </returns>
      public static Version GetExactVersion(FrameworkVersion frameworkVersion)
      {
        Version exactVersion = new Version();
        switch (frameworkVersion)
        {
          case FrameworkVersion.Fx10:
            exactVersion = FrameworkVersionDetection.GetNetfx10ExactVersion();
            break;
          case FrameworkVersion.Fx11:
            exactVersion = FrameworkVersionDetection.GetNetfx11ExactVersion();
            break;
          case FrameworkVersion.Fx20:
            exactVersion = FrameworkVersionDetection.GetNetfx20ExactVersion();
            break;
          case FrameworkVersion.Fx30:
            exactVersion = FrameworkVersionDetection.GetNetfx30ExactVersion();
            break;
          case FrameworkVersion.Fx35:
            exactVersion = FrameworkVersionDetection.GetNetfx35ExactVersion();
            break;
          case FrameworkVersion.Fx40C:
            exactVersion = FrameworkVersionDetection.GetNetfx40ClientExactVersion();
            break;
          case FrameworkVersion.Fx40F:
            exactVersion = FrameworkVersionDetection.GetNetfx40FullExactVersion();
            break;
        }
        return exactVersion;
      }
    }
}
