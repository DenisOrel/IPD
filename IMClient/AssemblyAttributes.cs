
// Type: IMClient.AssemblyAttributes




using Intermech.Interfaces;
using System.IO;
using System.Reflection;


namespace IMClient
{
    public static class AssemblyAttributes
    {
      public static string IPSVersion
      {
        get
        {
          object[] customAttributes = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyVersionString), true);
          AssemblyVersionString assemblyVersionString = customAttributes == null || customAttributes.Length == 0 ? (AssemblyVersionString) null : customAttributes[0] as AssemblyVersionString;
          return assemblyVersionString == null ? AssemblyAttributes.AssemblyVersion : assemblyVersionString.Description;
        }
      }

      public static string IPSServicePack
      {
        get
        {
          string ipsVersion = AssemblyAttributes.IPSVersion;
          if (string.IsNullOrEmpty(ipsVersion))
            return string.Empty;
          string[] strArray = ipsVersion.Split('.');
          return strArray != null && strArray.Length == 4 && DataSetProcessor.GetInt64Value((object) strArray[2], 0L) > 0L ? strArray[2] : string.Empty;
        }
      }

      public static string IPSBuildDate
      {
        get
        {
          object[] customAttributes = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildDate), true);
          AssemblyBuildDate assemblyBuildDate = customAttributes == null || customAttributes.Length == 0 ? (AssemblyBuildDate) null : customAttributes[0] as AssemblyBuildDate;
          return assemblyBuildDate == null ? "01.04.2009" : assemblyBuildDate.Description;
        }
      }

      public static string IPSBuildTime
      {
        get
        {
          object[] customAttributes = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildTime), true);
          AssemblyBuildTime assemblyBuildTime = customAttributes == null || customAttributes.Length == 0 ? (AssemblyBuildTime) null : customAttributes[0] as AssemblyBuildTime;
          return assemblyBuildTime == null ? "10:40:00" : assemblyBuildTime.Description;
        }
      }

      public static string IPSBuildGuid
      {
        get
        {
          object[] customAttributes = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildGuid), true);
          AssemblyBuildGuid assemblyBuildGuid = customAttributes == null || customAttributes.Length == 0 ? (AssemblyBuildGuid) null : customAttributes[0] as AssemblyBuildGuid;
          return assemblyBuildGuid == null ? "{7D2A4E6E-C93E-42C0-B563-AEF3D520EA82}" : assemblyBuildGuid.Description;
        }
      }

      public static string AssemblyTitle
      {
        get
        {
          object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
          if (customAttributes.Length != 0)
          {
            AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute) customAttributes[0];
            if (assemblyTitleAttribute.Title != "")
              return assemblyTitleAttribute.Title;
          }
          return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }
      }

      public static string AssemblyVersion
      {
        get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }

      public static string AssemblyDescription
      {
        get
        {
          object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
          return customAttributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
        }
      }

      public static string AssemblyProduct
      {
        get
        {
          object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
          return customAttributes.Length == 0 ? "" : ((AssemblyProductAttribute) customAttributes[0]).Product;
        }
      }

      public static string AssemblyCopyright
      {
        get
        {
          object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
          return customAttributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
        }
      }

      public static string AssemblyCompany
      {
        get
        {
          object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
          return customAttributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) customAttributes[0]).Company;
        }
      }
    }
}
