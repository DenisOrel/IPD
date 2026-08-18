
// Type: Intermech.ExceptionHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;


namespace Intermech
{
    public sealed class ExceptionHelper
    {
      private static ApplicationServiceRef<IExceptionHandlerService> exceptionServiceRef = new ApplicationServiceRef<IExceptionHandlerService>();

      /// <summary>
      /// Возвращает или задает ссылку на сервис обработки исключительных ситуаций.
      /// </summary>
      public static IExceptionHandlerService ExceptionService
      {
        [DebuggerStepThrough] get => ExceptionHelper.exceptionServiceRef.Value;
        [DebuggerStepThrough] set => ExceptionHelper.exceptionServiceRef.Value = value;
      }

      [Obsolete("Use the method ExceptionServices.GetExtendedStackTrace(exception) instead of this method.", true)]
      public static string ShowExtendedStackTrace(Exception exception)
      {
        return ExceptionServices.GetExtendedStackTrace(exception);
      }

      public static IXMLSettingsStorage ExceptionToXML(Exception exc, IPluginManager pluginManager)
      {
        XMLSettingsStorage xml = new XMLSettingsStorage();
        xml.document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />");
        XmlNode parentNode1 = xml.AddNode((XmlNode) xml.document.DocumentElement, "Exception");
        XmlNode xmlNode1 = xml.AddNode(parentNode1, "ExceptionText");
        XmlNode xmlNode2 = xml.AddNode(parentNode1, "ExceptionStack");
        XmlNode xmlNode3 = xml.AddNode(parentNode1, "ExceptionSource");
        xmlNode1.InnerText = exc.Message;
        xmlNode2.InnerText = ExceptionServices.GetExtendedStackTrace(exc);
        string source = exc.Source;
        xmlNode3.InnerText = source;
        FileInfo fileInfo = new FileInfo(typeof (XMLSettingsStorage).Assembly.Location);
        object[] customAttributes1 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyVersionString), true);
        object[] customAttributes2 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildDate), true);
        object[] customAttributes3 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildTime), true);
        object[] customAttributes4 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildGuid), true);
        AssemblyVersionString assemblyVersionString = customAttributes1 == null || customAttributes1.Length == 0 ? (AssemblyVersionString) null : customAttributes1[0] as AssemblyVersionString;
        AssemblyBuildDate assemblyBuildDate = customAttributes2 == null || customAttributes2.Length == 0 ? (AssemblyBuildDate) null : customAttributes2[0] as AssemblyBuildDate;
        AssemblyBuildTime assemblyBuildTime = customAttributes3 == null || customAttributes3.Length == 0 ? (AssemblyBuildTime) null : customAttributes3[0] as AssemblyBuildTime;
        AssemblyBuildGuid assemblyBuildGuid = customAttributes4 == null || customAttributes4.Length == 0 ? (AssemblyBuildGuid) null : customAttributes4[0] as AssemblyBuildGuid;
        if (assemblyVersionString != null)
          xml.SetAttributeValue((XmlNode) xml.document.DocumentElement, "Build", assemblyVersionString.Description);
        if (assemblyBuildDate != null)
          xml.SetAttributeValue((XmlNode) xml.document.DocumentElement, "BuildDate", assemblyBuildDate.Description);
        if (assemblyBuildTime != null)
          xml.SetAttributeValue((XmlNode) xml.document.DocumentElement, "BuildTime", assemblyBuildTime.Description);
        if (assemblyBuildGuid != null)
          xml.SetAttributeValue((XmlNode) xml.document.DocumentElement, "BuildGuid", assemblyBuildGuid.Description);
        try
        {
          if (pluginManager != null)
          {
            XmlNode parentNode2 = xml.AddNode((XmlNode) xml.document.DocumentElement, "Plugins");
            foreach (IPlugin plugin in (IEnumerable<IPlugin>) pluginManager.Plugins)
            {
              foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
              {
                XmlNode node = xml.AddNode(parentNode2, "Plugin");
                string location = plugin.Location;
                string str = package.GetType().Assembly.GetName().Version.ToString();
                xml.SetAttributeValue(node, "name", package.Name);
                xml.SetAttributeValue(node, "version", str);
                xml.SetAttributeValue(node, "location", location);
              }
            }
          }
        }
        catch
        {
        }
        return (IXMLSettingsStorage) xml;
      }
    }
}
