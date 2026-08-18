
// Type: Intermech.Interfaces.InformationCollector.IISVersionDetection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Microsoft.Win32;


namespace Intermech.Interfaces.InformationCollector
{
    public static class IISVersionDetection
    {
      private const string IISRegKeyName = "Software\\Microsoft\\InetStp";
      private const string IISRegKeyValue = "MajorVersion";
      private const string IISRegKeyMinorVersionValue = "MinorVersion";

      public static string GetIISVersionInstalled()
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\InetStp", false))
        {
          if (registryKey != null)
          {
            int num1 = (int) registryKey.GetValue("MajorVersion", (object) -1);
            int num2 = (int) registryKey.GetValue("MinorVersion", (object) -1);
            if (num1 != -1)
            {
              string versionInstalled = "IIS Version " + (object) num1;
              if (num2 != -1)
                versionInstalled = $"{versionInstalled}.{(object) num1}";
              return versionInstalled;
            }
          }
        }
        return string.Empty;
      }
    }
}
