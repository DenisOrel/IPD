
// Type: Intermech.Interfaces.InformationCollector.IPSInformation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Intermech.Diagnostics;
using Intermech.Interfaces.Plugins;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;


namespace Intermech.Interfaces.InformationCollector
{
    public static class IPSInformation
    {
      /// <summary>
      /// в реестре путь к продуктам интермеха
      /// SOFTWARE\Intermech
      /// </summary>
      private static string INTERMECH_KEY = "SOFTWARE\\Intermech";
      /// <summary>
      /// имя параметра для клиента IPS
      /// IPSHomeClient
      /// </summary>
      private static string IPS_HOME_CLIENT = "IPSHomeClient";
      /// <summary>ключ в логе инсталятора</summary>
      private static string CONFIG_NODE_NAME = "log/ipscli/config";
      /// <summary>ключ для имени организации</summary>
      private static string ORGANIZATION_NAME = "log/ipscli/config/value[@key='IPS_COMPANY_NAME']";
      /// <summary>
      /// 
      /// </summary>
      private static string SETUP_FOLDER = "log/ipscli/config/value[@key='IPS_SETUP_FOLDER']";
      /// <summary>
      /// папка, в которой будут храниться логи клиента и его конфиг
      /// </summary>
      public static string CLIENT_FILES_PATH = "ClientFiles";
      /// <summary>
      /// папка, в которой будут храниться логи сервера и его конфиг
      /// </summary>
      public static string SERVER_FILES_PATH = "ServerFiles";
      /// <summary>
      /// папка, в которой будут храниться скриншот и файлы приатаченные юзером
      /// </summary>
      public static string ATTACH_PATH = "Attach";
      /// <summary>
      /// 50 МБ - максимальный размер логов, пересылаемый в службу поддержки
      /// </summary>
      public static int MAX_LOG_SIZE = 52428800 /*0x03200000*/;
      /// <summary>1 МБ - максимальный размер одного файла с логами</summary>
      public static int MAX_SINGLE_LOG_FILE_SIZE = 1048576 /*0x100000*/;

      /// <summary>Информация о плагинах</summary>
      /// <param name="pluginManager">Ссылку на службу, управляющую модулями расширения</param>
      /// <returns></returns>
      public static InformationNode PluginsInformation(IPluginManager pluginManager)
      {
        InformationNode informationNode = new InformationNode("Plugins");
        if (pluginManager != null)
        {
          foreach (IPlugin plugin in (IEnumerable<IPlugin>) pluginManager.Plugins)
          {
            foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
              informationNode.Add(new InformationNode("Plugin")
              {
                new InformationNode("name", package.Name, NodeType.Attribute),
                new InformationNode("location", plugin.Location, NodeType.Attribute),
                new InformationNode("version", package.GetType().Assembly.GetName().Version.ToString(), NodeType.Attribute)
              });
          }
        }
        return informationNode;
      }

      /// <summary>Версия, дата и время билда.</summary>
      public static InformationNode VersionInformation()
      {
        InformationNode informationNode = new InformationNode("IPSVersion");
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
          informationNode.Add(new InformationNode("Build", assemblyVersionString.Description));
        if (assemblyBuildDate != null)
          informationNode.Add(new InformationNode("BuildDate", assemblyBuildDate.Description));
        if (assemblyBuildTime != null)
          informationNode.Add(new InformationNode("BuildTime", assemblyBuildTime.Description));
        if (assemblyBuildGuid != null)
          informationNode.Add(new InformationNode("BuildGuid", assemblyBuildGuid.Description));
        return informationNode;
      }

      /// <summary>Информация о возникшей ошибке</summary>
      /// <returns></returns>
      public static InformationNode ExceptionInformation(Exception ex)
      {
        InformationNode informationNode = new InformationNode("Exception");
        if (ex == null)
          return informationNode;
        informationNode.Add(new InformationNode("ExceptionText", ex.Message));
        informationNode.Add(new InformationNode("ExceptionStack", ExceptionServices.GetExtendedStackTrace(ex)));
        informationNode.Add(new InformationNode("ExceptionSource", ex.Source));
        return informationNode;
      }

      /// <summary>информация из окна Вывод сервера приложений</summary>
      /// <param name="session"></param>
      /// <returns></returns>
      public static InformationNode ServerOutput(IUserSession session)
      {
        IOutputViewHistory customService = session.GetCustomService(typeof (IOutputViewHistory)) as IOutputViewHistory;
        InformationNode informationNode = new InformationNode(nameof (ServerOutput));
        if (customService != null)
        {
          foreach (Tuple<string, string> tuple in customService.GetOutputHistory())
          {
            string nodeValue = tuple.Item1;
            informationNode.Add(new InformationNode("OutputMessage", tuple.Item2)
            {
              new InformationNode("OutputCategory", nodeValue, NodeType.Attribute)
            });
          }
        }
        return informationNode;
      }

      /// <summary>
      /// получить содержимое секции config в файле SetupCliLog.xml для всех клиентов,
      /// установленных на машине
      /// </summary>
      /// <returns></returns>
      public static InformationNode ClientHomeConfig()
      {
        InformationNode informationNode1 = new InformationNode("IPSHomeClient");
        string directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        Dictionary<string, string> clientHomeList = IPSInformation.GetClientHomeList();
        foreach (string key in clientHomeList.Keys)
        {
          string str1 = clientHomeList[key];
          if (File.Exists(str1))
          {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(str1);
            XmlNode xmlNode = xmlDocument.SelectSingleNode(IPSInformation.CONFIG_NODE_NAME);
            if (xmlNode != null)
            {
              InformationNode informationNode2 = new InformationNode("config");
              informationNode2.Add(new InformationNode("name", key, NodeType.Attribute));
              foreach (XmlNode childNode in xmlNode.ChildNodes)
              {
                InformationNode informationNode3 = new InformationNode("value");
                string nodeValue = childNode.Attributes["key"].Value;
                string str2 = childNode.Attributes["val"].Value;
                informationNode3.Add(new InformationNode("key", nodeValue, NodeType.Attribute));
                informationNode3.Add(new InformationNode("val", str2, NodeType.Attribute));
                if (nodeValue == "IPS_SETUP_FOLDER" && Path.Combine(str2, "Client") == directoryName)
                  informationNode2.Add(new InformationNode("current", "true", NodeType.Attribute));
                informationNode2.Add(informationNode3);
              }
              informationNode1.Add(informationNode2);
            }
          }
        }
        return informationNode1;
      }

      /// <summary>
      /// получить имя организации, указанное в файле SetupCliLog.xml
      /// </summary>
      /// <returns></returns>
      public static string OrganizationName()
      {
        string directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        Dictionary<string, string> clientHomeList = IPSInformation.GetClientHomeList();
        foreach (string key in clientHomeList.Keys)
        {
          string str = clientHomeList[key];
          if (File.Exists(str))
          {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(str);
            XmlNode xmlNode1 = xmlDocument.SelectSingleNode(IPSInformation.SETUP_FOLDER);
            if (xmlNode1 != null && Path.Combine(xmlNode1.Attributes["val"].Value, "Client") == directoryName)
            {
              XmlNode xmlNode2 = xmlDocument.SelectSingleNode(IPSInformation.ORGANIZATION_NAME);
              if (xmlNode2 != null)
                return xmlNode2.Attributes["val"].Value;
            }
          }
        }
        return string.Empty;
      }

      /// <summary>получить список путей к логами установленных ипсов</summary>
      /// <returns></returns>
      private static Dictionary<string, string> GetClientHomeList()
      {
        Dictionary<string, string> clientHomeList = new Dictionary<string, string>();
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(IPSInformation.INTERMECH_KEY, false))
        {
          if (registryKey != null)
          {
            string[] valueNames = registryKey.GetValueNames();
            if (valueNames != null)
            {
              foreach (string str1 in valueNames)
              {
                if (str1.Contains(IPSInformation.IPS_HOME_CLIENT))
                {
                  string str2 = registryKey.GetValue(str1, (object) string.Empty).ToString();
                  clientHomeList.Add(str1, str2);
                }
              }
            }
          }
        }
        return clientHomeList;
      }

      /// <summary>Упаковать все файлы отчёта в архив</summary>
      /// <param name="folderForPack"> папка во всеми файлами отчёта</param>
      /// <param name="zipFilePath">имя файла в котором будет архив</param>
      public static void PackReport(string folderForPack, string zipFilePath)
      {
        List<string> fileList = IPSInformation.GenerateFileList(folderForPack);
        using (FileStream baseOutputStream = new FileStream(zipFilePath, FileMode.OpenOrCreate, FileAccess.Write))
        {
          using (ZipOutputStream destination = new ZipOutputStream((Stream) baseOutputStream))
          {
            destination.SetLevel(9);
            foreach (string path in fileList)
            {
              ZipEntry entry = new ZipEntry(ZipEntry.CleanName(path.Replace(folderForPack, string.Empty)));
              destination.PutNextEntry(entry);
              if (!path.EndsWith("/"))
              {
                using (FileStream source = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                  byte[] buffer = new byte[4096 /*0x1000*/];
                  StreamUtils.Copy((Stream) source, (Stream) destination, buffer);
                }
              }
            }
          }
        }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="folder"></param>
      /// <returns></returns>
      private static List<string> GenerateFileList(string folder)
      {
        List<string> fileList = new List<string>();
        bool flag = true;
        foreach (string file in Directory.GetFiles(folder))
        {
          fileList.Add(file);
          flag = false;
        }
        if (flag && Directory.GetDirectories(folder).Length == 0)
          fileList.Add(folder + "/");
        foreach (string directory in Directory.GetDirectories(folder))
          fileList.AddRange((IEnumerable<string>) IPSInformation.GenerateFileList(directory));
        return fileList;
      }

      /// <summary>получить значение узла в сформированном отчёте</summary>
      /// <param name="node">родительский узел</param>
      /// <param name="nodeName">имя узла, значение которого хотим получить</param>
      /// <returns></returns>
      public static string GetReportNodeValue(InformationNode node, string nodeName)
      {
        foreach (InformationNode informationNode in (List<InformationNode>) node)
        {
          if (informationNode.Type != NodeType.Attribute && !(informationNode.NodeName != nodeName))
            return informationNode.NodeValue;
        }
        return string.Empty;
      }
    }
}
