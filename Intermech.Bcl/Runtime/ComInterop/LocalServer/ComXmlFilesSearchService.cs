
// Type: Intermech.Runtime.ComInterop.LocalServer.ComXmlFilesSearchService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.IO;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Сервис поиска плагинов COM-сервера для приложений с плагинами, в которых плагины самого приложения, содержащие COM-классы, описываются с помощью .com.xml-файлов.
    /// </summary>
    internal sealed class ComXmlFilesSearchService
    {
      private const string dllExtension = ".dll";
      private const string exeExtension = ".exe";
      private const string tlbExtension = ".tlb";
      private string hostPath;

      /// <summary>Создает объект.</summary>
      /// <param name="hostPath">Абсолютный путь к исполняемому файлу приложения COM-сервера</param>
      public ComXmlFilesSearchService(string hostPath) => this.hostPath = hostPath;

      /// <summary>
      /// Находит плагины для COM-сервера, используя .com.xml-файлы.
      /// </summary>
      /// <param name="errorList">Список ошибок, произошедших при поиске плагинов</param>
      /// <returns>Коллекция описателей плагинов</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="errorList" /> не должен быть равен null</exception>
      public ICollection<ComPluginInfo> FindPlugins(IErrorList errorList)
      {
        if (errorList == null)
          throw new ArgumentNullException(nameof (errorList));
        string withoutExtension = Path.GetFileNameWithoutExtension(this.hostPath);
        List<ComPluginInfo> plugins = new List<ComPluginInfo>();
        foreach (string hostSearchPath in this.GetHostSearchPaths())
        {
          foreach (string file in Directory.GetFiles(hostSearchPath, "*.com.xml"))
          {
            string pluginAssemblyFile = this.FindPluginAssemblyFile(file);
            if (string.IsNullOrEmpty(pluginAssemblyFile))
            {
              errorList.AddWarning(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_UnlinkedComXmlFile, (object) Path.GetFileName(file)));
            }
            else
            {
              ComXmlPluginInfo configurationFile = this.TryParsePluginConfigurationFile(pluginAssemblyFile, file, errorList);
              if (configurationFile != null && string.Compare(configurationFile.HostName, withoutExtension, true, CultureInfo.CurrentUICulture) == 0)
                plugins.Add((ComPluginInfo) configurationFile);
            }
          }
        }
        return (ICollection<ComPluginInfo>) plugins;
      }

      private List<string> GetHostSearchPaths()
      {
        List<string> hostSearchPaths = new List<string>();
        string directoryName = Path.GetDirectoryName(Path.GetFullPath(this.hostPath));
        if (Directory.Exists(directoryName))
          hostSearchPaths.Add(directoryName);
        string str = this.hostPath + ".config";
        if (File.Exists(str))
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.Load(str);
          XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
          nsmgr.AddNamespace("ms", "urn:schemas-microsoft-com:asm.v1");
          XmlNode xmlNode = xmlDocument.SelectSingleNode("/configuration/runtime/ms:assemblyBinding/ms:probing/@privatePath", nsmgr);
          if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.Value))
            hostSearchPaths.AddRange((IEnumerable<string>) this.ParsePrivateBinPath(xmlNode.Value, directoryName, new Predicate<string>(Directory.Exists)));
        }
        if (!string.IsNullOrEmpty(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath))
          hostSearchPaths.AddRange((IEnumerable<string>) this.ParsePrivateBinPath(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, directoryName, new Predicate<string>(Directory.Exists)));
        return hostSearchPaths;
      }

      private List<string> ParsePrivateBinPath(
        string privateBinPathList,
        string basePath,
        Predicate<string> filter)
      {
        List<string> privateBinPath = new List<string>();
        string str1 = privateBinPathList;
        char[] chArray = new char[1]{ Path.PathSeparator };
        foreach (string path2 in str1.Split(chArray))
        {
          if (!string.IsNullOrEmpty(path2))
          {
            string str2 = Path.Combine(basePath, path2);
            if (filter(str2))
              privateBinPath.Add(str2);
          }
        }
        return privateBinPath;
      }

      private string FindPluginAssemblyFile(string extensionCfgPath)
      {
        string directoryName = Path.GetDirectoryName(extensionCfgPath);
        string withoutExtension = Path.GetFileNameWithoutExtension(extensionCfgPath);
        string path1 = Path.Combine(directoryName, Path.ChangeExtension(withoutExtension, ".dll"));
        if (File.Exists(path1))
          return path1;
        string path2 = Path.Combine(directoryName, Path.ChangeExtension(withoutExtension, ".exe"));
        return File.Exists(path2) ? path2 : (string) null;
      }

      private ComXmlPluginInfo TryParsePluginConfigurationFile(
        string pluginAssemblyFile,
        string pluginConfigurationFile,
        IErrorList errorList)
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          xmlDocument.Load(pluginConfigurationFile);
        }
        catch (XmlException ex)
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            stringBuilder.AppendFormat(ComServerResources.SR_BadComXmlFileFormat, (object) Path.GetFileName(pluginConfigurationFile));
            stringBuilder.Append(' ');
            stringBuilder.Append(ex.Message);
            errorList.AddError(stringBuilder.ToString());
            return (ComXmlPluginInfo) null;
          }
        }
        string str = xmlDocument.SelectSingleNode("/COM/Host/@name")?.Value.Trim();
        if (string.IsNullOrEmpty(str))
        {
          errorList.AddError(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_HostApplicationNameIsNotSpecified, (object) Path.GetFileName(pluginConfigurationFile), (object) Path.GetFileName(pluginAssemblyFile)));
          return (ComXmlPluginInfo) null;
        }
        if (str.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
          str = Path.GetFileNameWithoutExtension(str);
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/COM/TypeLib/@name");
        PathCollection typeLibPathList = new PathCollection(xmlNodeList.Count);
        foreach (XmlNode xmlNode in xmlNodeList)
        {
          string path1 = xmlNode?.Value.Trim();
          if (!string.IsNullOrEmpty(path1))
          {
            if (path1.EndsWith(".tlb", StringComparison.CurrentCultureIgnoreCase))
              path1 = Path.GetFileNameWithoutExtension(path1);
            string path2 = Path.Combine(Path.GetDirectoryName(pluginAssemblyFile), path1 + ".tlb");
            if (File.Exists(path2))
            {
              typeLibPathList.Add(path2);
            }
            else
            {
              string path3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path1 + ".tlb");
              if (File.Exists(path3))
                typeLibPathList.Add(path3);
            }
          }
        }
        return new ComXmlPluginInfo(pluginAssemblyFile, (ICollection<string>) typeLibPathList, str);
      }
    }
}
