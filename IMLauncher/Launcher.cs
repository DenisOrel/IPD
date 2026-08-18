// Decompiled with JetBrains decompiler
// Type: IMLauncher.Launcher
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using Intermech.Runtime.ComInterop.LocalServer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace IMLauncher;

internal class Launcher
{
  private List<ProgrammInfo> listOfProgramms = new List<ProgrammInfo>();
  private List<string> listOfImBase_net = new List<string>();
  private List<ProgrammInfo> listOfIPS = new List<ProgrammInfo>();
  private List<ProgrammInfo> programmsFromXml = new List<ProgrammInfo>();

  public List<ProgrammInfo> ListOfProgramms => this.listOfProgramms;

  public List<string> ListOfImBase_net => this.listOfImBase_net;

  [DllImport("oleaut32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
  private static extern void RegisterTypeLib(
    System.Runtime.InteropServices.ComTypes.ITypeLib TypeLib,
    string szFullPath,
    string szHelpDirs);

  [DllImport("oleaut32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
  private static extern System.Runtime.InteropServices.ComTypes.ITypeLib LoadTypeLibEx(
    string szFullPath,
    Launcher.REGKIND regKind);

  public Launcher() => this.listOfProgramms.Clear();

  public void StartProcess(string exeName, string libraryVersion, string programmArguments)
  {
    if (!File.Exists(exeName))
    {
      int num = (int) MessageBox.Show("Не удается найти указанный файл: \n" + exeName, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      try
      {
        this.RegisterTypeLib(libraryVersion);
      }
      catch
      {
        if (MessageBox.Show("Не удается зарегистрировать библиотеку – возможно, нет прав доступа \n Продолжить запуск приложения?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
          return;
      }
      Process.Start(new ProcessStartInfo()
      {
        FileName = exeName,
        WorkingDirectory = this.GetWorkingDirectory(exeName),
        UseShellExecute = false,
        Arguments = programmArguments
      });
    }
  }

  private string GetWorkingDirectory(string exeName) => Directory.GetParent(exeName).FullName;

  private bool IsIPSProgramm(string exeName) => new FileInfo(exeName).Name == "IMClient.exe";

  private void ExecuteEnableCom(string exeName)
  {
    string path = exeName.Replace(LauncherConsts.IM_CLIENT_EXE, LauncherConsts.ENABLE_COM_BAT);
    if (!File.Exists(path))
      return;
    string str = exeName.Replace(LauncherConsts.IM_CLIENT_EXE, string.Empty);
    Process.Start(new ProcessStartInfo()
    {
      FileName = path,
      WorkingDirectory = str,
      UseShellExecute = false
    }).WaitForExit();
  }

  public void TurnOnCom(string paths)
  {
    string str = paths;
    string[] separator = new string[1]
    {
      Environment.NewLine
    };
    foreach (string exeName in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      if (this.IsIPSProgramm(exeName))
        this.ExecuteEnableCom(exeName);
    }
  }

  public bool IsComEnabled(string path)
  {
    return StringComparer.CurrentCultureIgnoreCase.Compare(path, ComServer.GetLastRegisteredHostApplication(LauncherConsts.IMCLIENT_ASSEMBLY_GUID)) == 0;
  }

  private void RegisterTypeLib(string libraryVersion)
  {
    string path = libraryVersion;
    if (string.IsNullOrEmpty(path))
      return;
    string fullPath = Path.GetFullPath(path);
    System.Runtime.InteropServices.ComTypes.ITypeLib typeLib = Launcher.LoadTypeLibEx(fullPath, Launcher.REGKIND.REGKIND_NONE);
    try
    {
      Launcher.RegisterTypeLib(typeLib, fullPath, Path.GetDirectoryName(fullPath));
    }
    finally
    {
      Marshal.FinalReleaseComObject((object) typeLib);
    }
  }

  private List<ProgrammInfo> GetCadmechProfiles()
  {
    List<ProgrammInfo> cadmechProfiles = new List<ProgrammInfo>();
    object curVersion = (object) null;
    RegistryKey registryKey1 = Registry.CurrentUser.OpenSubKey("Software\\Autodesk\\AutoCAD\\");
    if (registryKey1 != null)
    {
      string[] subKeyNames = registryKey1.GetSubKeyNames();
      for (int index = 0; index < subKeyNames.Length; ++index)
      {
        if (subKeyNames[index].StartsWith("R1"))
        {
          LauncherConsts.FULL_PATH_TO_AUTO_CAD_VERSION = $"{LauncherConsts.FULL_PATH_TO_AUTO_CAD_VERSION}\\{subKeyNames[index]}";
          LauncherConsts.PATH_TO_AUTO_CAD_VERSION = $"{LauncherConsts.PATH_TO_AUTO_CAD_VERSION}\\{subKeyNames[index]}";
          LauncherConsts.FULL_PATH_TO_AUTO_CAD_EXE = $"{LauncherConsts.FULL_PATH_TO_AUTO_CAD_EXE}\\{subKeyNames[index]}";
          curVersion = Registry.GetValue(LauncherConsts.FULL_PATH_TO_AUTO_CAD_VERSION, LauncherConsts.ACAD_VERSION, (object) null);
          break;
        }
      }
    }
    if (curVersion != null)
    {
      RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey($"{LauncherConsts.PATH_TO_AUTO_CAD_VERSION}\\{curVersion}\\Profiles");
      string imDirectory = this.GetIMDirectory();
      string imLibrary = imDirectory == string.Empty ? string.Empty : imDirectory + LauncherConsts.IM_ANCI_PATH;
      if (registryKey2 != null)
      {
        string[] subKeyNames = registryKey2.GetSubKeyNames();
        if (subKeyNames != null)
        {
          foreach (string profile in subKeyNames)
          {
            if (profile == LauncherConsts.PROFILE_CADMECH)
              cadmechProfiles.Add(new ProgrammInfo(LauncherConsts.CADMECH_NAME, new string[1]
              {
                this.GetCadmechPath(curVersion)
              }, imLibrary, AdditionalInfo.None, this.GetCadmechArguments(curVersion, profile)));
            else if (profile == LauncherConsts.PROFILE_CADMECH_T)
              cadmechProfiles.Add(new ProgrammInfo(LauncherConsts.CADMECH_T_NAME, new string[1]
              {
                this.GetCadmechPath(curVersion)
              }, imLibrary, AdditionalInfo.None, this.GetCadmechArguments(curVersion, profile)));
            else if (profile == LauncherConsts.PROFILE_CADM_IPS)
              cadmechProfiles.Add(new ProgrammInfo(LauncherConsts.CADMECH_IPS_NAME, new string[1]
              {
                this.GetCadmechPath(curVersion)
              }, LauncherConsts.CAD_IM_BASE_LIBRARY, AdditionalInfo.None, this.GetCadmechArguments(curVersion, profile)));
            else if (profile == LauncherConsts.PROFILE_CADM_T_IPS)
              cadmechProfiles.Add(new ProgrammInfo(LauncherConsts.CADMECH_IPS_T_NAME, new string[1]
              {
                this.GetCadmechPath(curVersion)
              }, LauncherConsts.CAD_IM_BASE_LIBRARY, AdditionalInfo.None, this.GetCadmechArguments(curVersion, profile)));
          }
        }
      }
    }
    return cadmechProfiles;
  }

  private string GetCadmechPath(object curVersion)
  {
    object obj = Registry.GetValue($"{LauncherConsts.FULL_PATH_TO_AUTO_CAD_EXE}\\{curVersion.ToString()}", LauncherConsts.ACAD_LOCATION, (object) null);
    return obj != null ? obj.ToString() + LauncherConsts.ACAD_EXE : string.Empty;
  }

  private string GetCadmechArguments(object curVersion, string profile)
  {
    object obj = Registry.GetValue($"{LauncherConsts.FULL_PATH_TO_AUTO_CAD_VERSION}\\{curVersion.ToString()}{LauncherConsts.ACAD_PROFILES}{profile}{LauncherConsts.ACAD_GENERAL}", LauncherConsts.ACAD_TEMPLATE, (object) null);
    return obj != null ? $" /p {profile} /t {obj.ToString()}" : " /p " + profile;
  }

  private List<string> ReadRegistryValueForIPS()
  {
    List<string> stringList = new List<string>();
    try
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(LauncherConsts.IPS_KEY);
      if (registryKey != null)
      {
        string[] valueNames = registryKey.GetValueNames();
        Regex regex = new Regex(LauncherConsts.PATTREN_IPS);
        foreach (string str in valueNames)
        {
          if (regex.IsMatch(str) && registryKey.GetValue(str).ToString() != string.Empty)
            stringList.Add(registryKey.GetValue(str).ToString());
        }
      }
    }
    catch (SecurityException ex)
    {
      int num = (int) MessageBox.Show(LauncherConsts.ERROR_TEXT + ex.Message, LauncherConsts.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return stringList;
    }
    return stringList;
  }

  private void FormPathToIPS()
  {
    this.listOfIPS.Clear();
    foreach (string str in this.ReadRegistryValueForIPS())
    {
      if (File.Exists(str))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(str);
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        Regex regex1 = new Regex(LauncherConsts.PATTREN_IMCLIENT);
        Regex regex2 = new Regex(LauncherConsts.PATTREN_IMIMBASE);
        foreach (XmlNode childNode in xmlDocument.ChildNodes[0].ChildNodes[0].ChildNodes)
        {
          if (childNode.Name == LauncherConsts.COPY_FILE_NODE)
          {
            if (empty1 == string.Empty && regex1.IsMatch(childNode.Attributes[LauncherConsts.NAME_ATTRIBUTE].Value))
              empty1 = childNode.Attributes[LauncherConsts.NAME_ATTRIBUTE].Value;
            if (empty2 == string.Empty && regex2.IsMatch(childNode.Attributes[LauncherConsts.NAME_ATTRIBUTE].Value))
            {
              empty2 = childNode.Attributes[LauncherConsts.NAME_ATTRIBUTE].Value;
              if (!this.listOfImBase_net.Contains(empty2))
                this.listOfImBase_net.Add(empty2);
            }
            if (empty1 != string.Empty)
            {
              if (empty2 != string.Empty)
                break;
            }
          }
        }
        AdditionalInfo info = AdditionalInfo.IMClient;
        if (this.IsComEnabled(empty1))
          info |= AdditionalInfo.Com;
        this.listOfIPS.Add(new ProgrammInfo(LauncherConsts.IPS_NAME, new string[1]
        {
          empty1
        }, empty2, info));
      }
    }
  }

  private object ReadRegistryValueForSearch()
  {
    object obj = (object) null;
    try
    {
      return Registry.GetValue(LauncherConsts.SEARCH_KEY, LauncherConsts.SEARCH_EXENAME, (object) null);
    }
    catch (SecurityException ex)
    {
      int num = (int) MessageBox.Show(LauncherConsts.ERROR_TEXT + ex.Message, LauncherConsts.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return obj;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LauncherConsts.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return obj;
    }
  }

  private string GetIMDirectory()
  {
    object obj;
    try
    {
      obj = Registry.GetValue(LauncherConsts.IM_KEY, LauncherConsts.IM_DIRECTORY, (object) null);
    }
    catch (SecurityException ex)
    {
      int num = (int) MessageBox.Show(LauncherConsts.ERROR_TEXT + ex.Message, LauncherConsts.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return string.Empty;
    }
    if (obj != null)
      return obj.ToString();
    return string.Empty;
  }

  public void MakeListOfProgramm()
  {
    object obj = this.ReadRegistryValueForSearch();
    string imDirectory = this.GetIMDirectory();
    string imLibrary = imDirectory == string.Empty ? string.Empty : imDirectory + LauncherConsts.IM_ANCI_PATH;
    if (obj != null)
      this.listOfProgramms.Add(new ProgrammInfo(LauncherConsts.SEARCH_NAME, new string[1]
      {
        obj.ToString()
      }, imLibrary, AdditionalInfo.None));
    this.FormPathToIPS();
    this.listOfProgramms.AddRange((IEnumerable<ProgrammInfo>) this.listOfIPS);
    this.listOfProgramms.AddRange((IEnumerable<ProgrammInfo>) this.GetCadmechProfiles());
    this.listOfProgramms.AddRange((IEnumerable<ProgrammInfo>) this.ReadsFromXml());
  }

  private List<ProgrammInfo> ReadsFromXml()
  {
    this.programmsFromXml = new List<ProgrammInfo>();
    if (File.Exists(LauncherConsts.XML_CONFIG))
    {
      XMLSettingsStorage xmlSettingsStorage = new XMLSettingsStorage(LauncherConsts.XML_CONFIG);
      foreach (XmlNode childNode1 in xmlSettingsStorage.FindNode((XmlNode) xmlSettingsStorage.document, nameof (Launcher), false).ChildNodes)
      {
        AdditionalInfo info = AdditionalInfo.Custom;
        string attributeValue1 = xmlSettingsStorage.GetAttributeValue(childNode1, LauncherConsts.NAME_ATTRIBUTE, string.Empty);
        List<string> stringList = new List<string>(childNode1.ChildNodes.Count);
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          string attributeValue2 = xmlSettingsStorage.GetAttributeValue(childNode2, LauncherConsts.CONFIG_ATTRIBUTE_PATH, string.Empty);
          if (this.IsIPSProgramm(attributeValue2))
          {
            info |= AdditionalInfo.IMClient;
            if (this.IsComEnabled(attributeValue2))
              info |= AdditionalInfo.Com;
          }
          stringList.Add(attributeValue2);
        }
        this.programmsFromXml.Add(new ProgrammInfo(attributeValue1, stringList.ToArray(), string.Empty, info));
      }
    }
    return this.programmsFromXml;
  }

  public AdditionalInfo AddProgrammToXml(string programmName, string[] programmPaths)
  {
    AdditionalInfo info = AdditionalInfo.Custom;
    XMLSettingsStorage xmlSettingsStorage = new XMLSettingsStorage();
    xmlSettingsStorage.Load(LauncherConsts.XML_CONFIG);
    XmlNode node1 = xmlSettingsStorage.FindNode((XmlNode) xmlSettingsStorage.document, nameof (Launcher), false);
    XmlNode xmlNode = xmlSettingsStorage.AddNode(node1, "pack");
    xmlSettingsStorage.SetAttributeValue(xmlNode, LauncherConsts.NAME_ATTRIBUTE, programmName);
    foreach (string programmPath in programmPaths)
    {
      XmlNode node2 = xmlSettingsStorage.AddNode(xmlNode, "programm");
      xmlSettingsStorage.SetAttributeValue(node2, LauncherConsts.CONFIG_ATTRIBUTE_PATH, programmPath);
      if (this.IsIPSProgramm(programmPath))
      {
        info |= AdditionalInfo.IMClient;
        if (this.IsComEnabled(programmPath))
          info |= AdditionalInfo.Com;
      }
    }
    this.programmsFromXml.Add(new ProgrammInfo(programmName, programmPaths, string.Empty, info));
    xmlSettingsStorage.Save(LauncherConsts.XML_CONFIG);
    return info;
  }

  public void RemoveProgrammFromXml(string programmName, string[] programmPaths)
  {
    List<string> stringList = new List<string>((IEnumerable<string>) programmPaths);
    XMLSettingsStorage xmlSettingsStorage = new XMLSettingsStorage();
    xmlSettingsStorage.Load(LauncherConsts.XML_CONFIG);
    XmlNode node = xmlSettingsStorage.FindNode((XmlNode) xmlSettingsStorage.document, nameof (Launcher), false);
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (xmlSettingsStorage.GetAttributeValue(childNode1, LauncherConsts.NAME_ATTRIBUTE, string.Empty).Equals(programmName))
      {
        bool flag = true;
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          string attributeValue = xmlSettingsStorage.GetAttributeValue(childNode2, LauncherConsts.CONFIG_ATTRIBUTE_PATH, string.Empty);
          if (!stringList.Contains(attributeValue))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          node.RemoveChild(childNode1);
          break;
        }
      }
    }
    xmlSettingsStorage.Save(LauncherConsts.XML_CONFIG);
  }

  private enum REGKIND
  {
    REGKIND_DEFAULT,
    REGKIND_REGISTER,
    REGKIND_NONE,
  }
}
