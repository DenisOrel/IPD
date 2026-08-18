// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TablesDisplayService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Imbase.Server;

internal class TablesDisplayService : LongLifeObject, ITablesDisplayService
{
  private Dictionary<Guid, DisplayMode> _displayModeForUser = new Dictionary<Guid, DisplayMode>();
  private Dictionary<Guid, TablesDisplayService.ObjectInfo> _objsInfo = new Dictionary<Guid, TablesDisplayService.ObjectInfo>();
  private const string ROOT_NODE_NAME = "TablesDisplaySettings";
  private const string USERS = "Users";
  private const string USER = "User";
  private const string OBJECTS = "Objects";
  private const string OBJECT = "Object";
  private const string GENERAL_MODE = "GeneralMode";
  private const string PERSONAL_MODE = "PersonalMode";
  private const string ROLE_MODE = "RoleMode";
  private const string ROLE = "Role";
  private const string GUID_ATTR = "Guid";
  private const string DISPLAYMODE_ATTR = "DisplayMode";
  private const string SORTED_COLUMNS = "SortedColumns";
  private const string COLUMN = "Column";
  private const string MODE = "Mode";

  internal TablesDisplayService(IUserSession session)
  {
    try
    {
      this.ParseXML(this.LoadConfiguration(session));
    }
    catch (Exception ex)
    {
      if (!(ServerServices.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      service.WriteString("IMBASE", LocalizationHolder.rm.GetString("Imbase.Server_23"));
      service.WriteString("IMBASE", ex.Message);
    }
  }

  public DisplayMode GetDisplayModeForUser(Guid userGuid)
  {
    return !this._displayModeForUser.ContainsKey(userGuid) ? DisplayMode.GeneralMode : this._displayModeForUser[userGuid];
  }

  public Guid GetSortedColumnGuid(Guid objGuid, Guid userGuid, out string mode)
  {
    Guid sortedColumnGuid = Guid.Empty;
    mode = "ASC";
    if (this._objsInfo.ContainsKey(objGuid))
    {
      string settingsForSortedColumn = this._objsInfo[objGuid].GetSettingsForSortedColumn(userGuid);
      if (!string.IsNullOrEmpty(settingsForSortedColumn))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.InnerXml = settingsForSortedColumn;
        XmlNode firstChild = xmlDocument.FirstChild;
        if (firstChild != null)
        {
          XmlAttribute attribute1 = firstChild.Attributes["Column"];
          if (attribute1 != null)
          {
            sortedColumnGuid = this.GuidFromString(attribute1.Value);
            XmlAttribute attribute2 = firstChild.Attributes["Mode"];
            mode = attribute2 != null ? attribute2.Value : "ASC";
          }
        }
      }
    }
    return sortedColumnGuid;
  }

  public string GetGeneralSettingsForObject(Guid objGuid)
  {
    XmlNode element = (XmlNode) new XmlDocument().CreateElement("GeneralMode");
    try
    {
      if (this._objsInfo.ContainsKey(objGuid))
      {
        string generalSettings = this._objsInfo[objGuid].GetGeneralSettings();
        if (!string.IsNullOrEmpty(generalSettings))
          element.InnerXml = generalSettings;
      }
    }
    catch
    {
    }
    return element.OuterXml;
  }

  public string GetObjectSettingsForRoles(Guid objGuid, List<Guid> roleGuids)
  {
    XmlDocument xmlDocument = new XmlDocument();
    XmlNode element1 = (XmlNode) xmlDocument.CreateElement("RoleMode");
    if (this._objsInfo.ContainsKey(objGuid) && roleGuids != null)
    {
      string empty = string.Empty;
      foreach (Guid roleGuid in roleGuids)
      {
        string str = this._objsInfo.ContainsKey(objGuid) ? this._objsInfo[objGuid].GetSettingsForRole(roleGuid) : string.Empty;
        if (!string.IsNullOrEmpty(str))
        {
          XmlElement element2 = xmlDocument.CreateElement("Role");
          element2.SetAttribute("Guid", roleGuid.ToString());
          element2.InnerXml = str;
          element1.AppendChild((XmlNode) element2);
        }
      }
    }
    if (!element1.HasChildNodes)
    {
      XmlElement element3 = xmlDocument.CreateElement("Role");
      element3.SetAttribute("Guid", roleGuids[0].ToString());
      element1.AppendChild((XmlNode) element3);
    }
    return element1.OuterXml;
  }

  public string GetObjectSettingsForUser(Guid objGuid, Guid userGuid)
  {
    XmlElement element = new XmlDocument().CreateElement("User");
    element.SetAttribute("Guid", userGuid.ToString());
    string str = this._objsInfo.ContainsKey(objGuid) ? this._objsInfo[objGuid].GetSettingsForUser(userGuid) : string.Empty;
    if (!string.IsNullOrEmpty(str))
      element.InnerXml = str;
    return element.OuterXml;
  }

  public void RemoveDisplayModeForUser(Guid userGuid) => this._displayModeForUser.Remove(userGuid);

  public void RemoveObjectSortedColumnForUser(Guid objGuid, Guid userGuid)
  {
    if (!this._objsInfo.ContainsKey(objGuid))
      return;
    TablesDisplayService.ObjectInfo objectInfo = this._objsInfo[objGuid];
    objectInfo.RemoveSortedColumn(userGuid);
    if (!objectInfo.IsEmpty)
      return;
    this._objsInfo.Remove(objGuid);
  }

  public void RemoveObjectSettingsForRoles(Guid objGuid, List<Guid> roleGuids)
  {
    if (!this._objsInfo.ContainsKey(objGuid) || roleGuids == null)
      return;
    TablesDisplayService.ObjectInfo oi = this._objsInfo[objGuid];
    roleGuids.ForEach((Action<Guid>) (x => oi.RemoveSettingsForRole(x)));
    if (!oi.IsEmpty)
      return;
    this._objsInfo.Remove(objGuid);
  }

  public void RemoveObjectSettingsForUsers(Guid objGuid, List<Guid> userGuids)
  {
    if (!this._objsInfo.ContainsKey(objGuid) || userGuids == null)
      return;
    TablesDisplayService.ObjectInfo oi = this._objsInfo[objGuid];
    userGuids.ForEach((Action<Guid>) (x => oi.RemoveSettingsForUser(x)));
    if (!oi.IsEmpty)
      return;
    this._objsInfo.Remove(objGuid);
  }

  public void RemoveSettingsForObject(List<Guid> objGuids)
  {
    if (objGuids == null || objGuids.Count <= 0)
      return;
    objGuids.ForEach((Action<Guid>) (x => this._objsInfo.Remove(x)));
    this.SaveConfiguration();
  }

  public void RemoveSettingsForRole(List<Guid> roleGuids)
  {
    if (roleGuids == null || roleGuids.Count <= 0)
      return;
    foreach (Guid roleGuid in roleGuids)
    {
      if (!(roleGuid == Guid.Empty))
      {
        foreach (TablesDisplayService.ObjectInfo objectInfo in this._objsInfo.Values)
          objectInfo.RemoveSettingsForRole(roleGuid);
      }
    }
    this.SaveConfiguration();
  }

  public void RemoveSettingsForUser(List<Guid> userGuids)
  {
    if (userGuids == null || userGuids.Count <= 0)
      return;
    foreach (Guid userGuid in userGuids)
    {
      if (!(userGuid == Guid.Empty))
      {
        this.RemoveDisplayModeForUser(userGuid);
        foreach (TablesDisplayService.ObjectInfo objectInfo in this._objsInfo.Values)
        {
          objectInfo.RemoveSettingsForUser(userGuid);
          objectInfo.RemoveSortedColumn(userGuid);
        }
      }
    }
    this.SaveConfiguration();
  }

  public void SaveSettingsForObject(
    Guid objGuid,
    Guid userGuid,
    Guid sortedColumn,
    string mode,
    DisplayMode displayMode,
    string gSettings,
    string uSettings,
    string rSettings)
  {
    if (!(objGuid != Guid.Empty))
      return;
    this.SetDisplayModeForUser(userGuid, displayMode);
    XmlDocument xmlDocument = new XmlDocument();
    bool flag = false;
    TablesDisplayService.ObjectInfo objsInfo = this._objsInfo.ContainsKey(objGuid) ? this._objsInfo[objGuid] : new TablesDisplayService.ObjectInfo(objGuid);
    if (!string.IsNullOrEmpty(gSettings))
    {
      xmlDocument.InnerXml = gSettings;
      this.SetGeneralSettingsForObject(objsInfo, xmlDocument.FirstChild);
      flag = true;
    }
    if (!string.IsNullOrEmpty(uSettings))
    {
      xmlDocument.InnerXml = uSettings;
      this.SetObjectSettingsForUser(objsInfo, xmlDocument.FirstChild);
      flag = true;
    }
    if (!string.IsNullOrEmpty(rSettings))
    {
      xmlDocument.InnerXml = rSettings;
      foreach (XmlNode childNode in xmlDocument.FirstChild.ChildNodes)
        this.SetObjectSettingsForRole(objsInfo, childNode);
      flag = true;
    }
    if (sortedColumn != Guid.Empty && !string.IsNullOrEmpty(mode))
    {
      XmlElement element = xmlDocument.CreateElement("User");
      element.SetAttribute("Guid", userGuid.ToString());
      element.SetAttribute("Column", sortedColumn.ToString());
      element.SetAttribute("Mode", mode);
      objsInfo.SetSortedCoumn((XmlNode) element);
    }
    if (!objsInfo.IsEmpty)
      this._objsInfo[objGuid] = objsInfo;
    else
      this._objsInfo.Remove(objGuid);
    if (!flag)
      return;
    this.SaveConfiguration();
  }

  public void CloneSettings(Guid sourceObjGuid, Guid targetObjGuid)
  {
    if (!(sourceObjGuid != Guid.Empty) || !(targetObjGuid != Guid.Empty) || !this._objsInfo.ContainsKey(sourceObjGuid))
      return;
    TablesDisplayService.ObjectInfo objectInfo = this._objsInfo[sourceObjGuid].Clone(targetObjGuid);
    this._objsInfo.Add(targetObjGuid, objectInfo);
    this.SaveConfiguration();
  }

  private string LoadConfiguration(IUserSession session)
  {
    string str = string.Empty;
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("IMBASE.TablesDisplaySettings", out config_info, out config_file, 0L);
    if (config_info.RealFileSize > 0L && config_file != null && config_file.Length != 0)
    {
      lock (this)
      {
        using (MemoryStream inStream = new MemoryStream(config_file))
        {
          inStream.Position = 0L;
          using (MemoryStream memoryStream = new MemoryStream(config_file.Length / 4))
          {
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
            memoryStream.Position = 0L;
            using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
              str = binaryReader.ReadString();
          }
        }
      }
    }
    return str;
  }

  private void ParseXML(string xml)
  {
    if (string.IsNullOrEmpty(xml))
      return;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.InnerXml = xml;
    XmlNode xmlNode1 = xmlDocument.SelectSingleNode($"{"TablesDisplaySettings"}/{"Users"}");
    if (xmlNode1 != null)
    {
      XmlNodeList childNodes = xmlNode1.ChildNodes;
      if (childNodes != null)
      {
        foreach (XmlNode xmlNode2 in childNodes)
        {
          XmlAttribute attribute1 = xmlNode2.Attributes["Guid"];
          if (attribute1 != null)
          {
            Guid userGuid = this.GuidFromString(attribute1.Value);
            XmlAttribute attribute2 = xmlNode2.Attributes["DisplayMode"];
            if (attribute2 != null && !string.IsNullOrEmpty(attribute2.Value))
            {
              DisplayMode result = DisplayMode.GeneralMode;
              if (Enum.TryParse<DisplayMode>(attribute2.Value, out result))
                this.SetDisplayModeForUser(userGuid, result);
            }
          }
        }
      }
    }
    XmlNode xmlNode3 = xmlDocument.SelectSingleNode($"{"TablesDisplaySettings"}/{"Objects"}");
    if (xmlNode3 == null)
      return;
    XmlNodeList childNodes1 = xmlNode3.ChildNodes;
    if (childNodes1 == null)
      return;
    foreach (XmlNode xmlNode4 in childNodes1)
    {
      XmlAttribute attribute = xmlNode4.Attributes["Guid"];
      if (attribute != null)
      {
        Guid guid = this.GuidFromString(attribute.Value);
        if (!(guid == Guid.Empty))
        {
          TablesDisplayService.ObjectInfo objsInfo = new TablesDisplayService.ObjectInfo(guid);
          XmlNode xmlNode5 = xmlNode4.SelectSingleNode("SortedColumns");
          if (xmlNode5 != null)
          {
            XmlNodeList childNodes2 = xmlNode5.ChildNodes;
            if (childNodes2 != null)
            {
              foreach (XmlNode sortedColumnNode in childNodes2)
              {
                if (!(sortedColumnNode.Name != "User"))
                  objsInfo.SetSortedCoumn(sortedColumnNode);
              }
            }
          }
          this.SetGeneralSettingsForObject(objsInfo, xmlNode4.SelectSingleNode("GeneralMode"));
          XmlNode xmlNode6 = xmlNode4.SelectSingleNode("PersonalMode");
          if (xmlNode6 != null)
          {
            XmlNodeList childNodes3 = xmlNode6.ChildNodes;
            if (childNodes3 != null)
            {
              foreach (XmlNode userNode in childNodes3)
                this.SetObjectSettingsForUser(objsInfo, userNode);
            }
          }
          XmlNode xmlNode7 = xmlNode4.SelectSingleNode("RoleMode");
          if (xmlNode7 != null)
          {
            XmlNodeList childNodes4 = xmlNode7.ChildNodes;
            if (childNodes4 != null)
            {
              foreach (XmlNode roleNode in childNodes4)
                this.SetObjectSettingsForRole(objsInfo, roleNode);
            }
          }
          if (!objsInfo.IsEmpty)
            this._objsInfo.Add(guid, objsInfo);
        }
      }
    }
  }

  private Guid GuidFromString(string guid)
  {
    return !GuidHelper.IsGuid(guid) ? Guid.Empty : new Guid(guid);
  }

  private void SetDisplayModeForUser(Guid userGuid, DisplayMode displayMode)
  {
    if (!(userGuid != Guid.Empty))
      return;
    this._displayModeForUser[userGuid] = displayMode;
  }

  private void SetGeneralSettingsForObject(
    TablesDisplayService.ObjectInfo objsInfo,
    XmlNode generalNode)
  {
    if (generalNode == null || !generalNode.HasChildNodes || !(generalNode.Name == "GeneralMode"))
      return;
    objsInfo.SetGeneralSettings(generalNode);
  }

  private void SetObjectSettingsForUser(TablesDisplayService.ObjectInfo objsInfo, XmlNode userNode)
  {
    if (!userNode.HasChildNodes || !(userNode.Name == "User"))
      return;
    objsInfo.SetSettingsForUser(userNode);
  }

  private void SetObjectSettingsForRole(TablesDisplayService.ObjectInfo objsInfo, XmlNode roleNode)
  {
    if (!roleNode.HasChildNodes || !(roleNode.Name == "Role"))
      return;
    objsInfo.SetSettingsForRole(roleNode);
  }

  private void SaveConfiguration()
  {
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1))
      return;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service1.GetSystemSessionTemporaryClone("Imbase.SaveConfiguration");
      if (userSession == null)
        return;
      IDBConfigurations configurations = userSession.Configurations;
      if (configurations == null)
        return;
      string displayInfo = this.GetDisplayInfo();
      if (string.IsNullOrEmpty(displayInfo))
        return;
      IPackedStream service2 = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      using (MemoryStream memoryStream = new MemoryStream(displayInfo.Length))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(displayInfo);
          binaryWriter.Flush();
          memoryStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) memoryStream.Length / 2))
          {
            service2.PackStream((Stream) outStream, (Stream) memoryStream, 9);
            BlobInformation config_info = new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "IMBASE.TablesDisplaySettings", ArcMethods.ZLibPacked, string.Empty);
            configurations.WriteConfigData(config_info, outStream.ToArray(), 0L);
          }
        }
      }
    }
    finally
    {
      userSession?.Logout("Imbase.SaveConfiguration");
    }
  }

  private string GetDisplayInfo()
  {
    XmlDocument xmlDocument1 = new XmlDocument();
    XmlNode element1 = (XmlNode) xmlDocument1.CreateElement("TablesDisplaySettings");
    XmlNode element2 = (XmlNode) xmlDocument1.CreateElement("Users");
    foreach (KeyValuePair<Guid, DisplayMode> keyValuePair in this._displayModeForUser)
    {
      XmlElement element3 = xmlDocument1.CreateElement("User");
      element3.SetAttribute("Guid", keyValuePair.Key.ToString());
      element3.SetAttribute("DisplayMode", keyValuePair.Value.ToString());
      element2.AppendChild((XmlNode) element3);
    }
    if (element2.HasChildNodes)
      element1.AppendChild(element2);
    XmlNode element4 = (XmlNode) xmlDocument1.CreateElement("Objects");
    string empty = string.Empty;
    foreach (KeyValuePair<Guid, TablesDisplayService.ObjectInfo> keyValuePair1 in this._objsInfo)
    {
      XmlElement element5 = xmlDocument1.CreateElement("Object");
      element5.SetAttribute("Guid", keyValuePair1.Key.ToString());
      XmlNode element6 = (XmlNode) xmlDocument1.CreateElement("SortedColumns");
      Dictionary<Guid, string> sortedColumns = keyValuePair1.Value.GetSortedColumns();
      XmlDocument xmlDocument2 = new XmlDocument();
      foreach (KeyValuePair<Guid, string> keyValuePair2 in sortedColumns)
      {
        XmlElement element7 = xmlDocument1.CreateElement("User");
        element7.SetAttribute("Guid", keyValuePair2.Key.ToString());
        xmlDocument2.InnerXml = keyValuePair2.Value;
        XmlAttribute attribute1 = xmlDocument2.FirstChild.Attributes["Column"];
        if (attribute1 != null && !string.IsNullOrEmpty(attribute1.Value))
        {
          element7.SetAttribute("Column", attribute1.Value);
          XmlAttribute attribute2 = xmlDocument2.FirstChild.Attributes["Mode"];
          if (attribute2 != null && !string.IsNullOrEmpty(attribute2.Value))
          {
            element7.SetAttribute("Mode", attribute2.Value);
            element6.AppendChild((XmlNode) element7);
          }
        }
      }
      if (element6.HasChildNodes)
        element5.AppendChild(element6);
      string generalSettings = keyValuePair1.Value.GetGeneralSettings();
      if (!string.IsNullOrEmpty(generalSettings))
      {
        XmlNode element8 = (XmlNode) xmlDocument1.CreateElement("GeneralMode");
        element8.InnerXml = generalSettings;
        element5.AppendChild(element8);
      }
      XmlNode element9 = (XmlNode) xmlDocument1.CreateElement("PersonalMode");
      foreach (KeyValuePair<Guid, string> user in keyValuePair1.Value.GetUsers())
      {
        XmlElement element10 = xmlDocument1.CreateElement("User");
        element10.SetAttribute("Guid", user.Key.ToString());
        element10.InnerXml = user.Value;
        element9.AppendChild((XmlNode) element10);
      }
      if (element9.HasChildNodes)
        element5.AppendChild(element9);
      XmlNode element11 = (XmlNode) xmlDocument1.CreateElement("RoleMode");
      foreach (KeyValuePair<Guid, string> role in keyValuePair1.Value.GetRoles())
      {
        XmlElement element12 = xmlDocument1.CreateElement("Role");
        element12.SetAttribute("Guid", role.Key.ToString());
        element12.InnerXml = role.Value;
        element11.AppendChild((XmlNode) element12);
      }
      if (element11.HasChildNodes)
        element5.AppendChild(element11);
      if (element5.HasChildNodes)
        element4.AppendChild((XmlNode) element5);
    }
    if (element4.HasChildNodes)
      element1.AppendChild(element4);
    return !element1.HasChildNodes ? string.Empty : element1.OuterXml;
  }

  private class ObjectInfo
  {
    private Guid _objGuid = Guid.Empty;
    private Dictionary<Guid, string> _sortedColumns = new Dictionary<Guid, string>();
    private string _general = string.Empty;
    private Dictionary<Guid, string> _userNodes = new Dictionary<Guid, string>();
    private Dictionary<Guid, string> _roleNodes = new Dictionary<Guid, string>();

    internal bool IsEmpty
    {
      get
      {
        return string.IsNullOrEmpty(this._general) && this._userNodes.Count == 0 && this._roleNodes.Count == 0 && this._sortedColumns.Count == 0;
      }
    }

    internal ObjectInfo(Guid objGuid) => this._objGuid = objGuid;

    internal Dictionary<Guid, string> GetSortedColumns() => this._sortedColumns;

    internal void SetSortedCoumn(XmlNode sortedColumnNode)
    {
      XmlAttribute attribute = sortedColumnNode.Attributes["Guid"];
      if (attribute == null || !GuidHelper.IsGuid(attribute.Value))
        return;
      Guid key = new Guid(attribute.Value);
      if (!(key != Guid.Empty))
        return;
      this._sortedColumns[key] = sortedColumnNode.OuterXml;
    }

    internal string GetSettingsForSortedColumn(Guid userGuid)
    {
      return !this._sortedColumns.ContainsKey(userGuid) ? string.Empty : this._sortedColumns[userGuid];
    }

    internal void RemoveSortedColumn(Guid userGuid) => this._sortedColumns.Remove(userGuid);

    internal string GetGeneralSettings() => this._general;

    internal void SetGeneralSettings(XmlNode node) => this._general = node.InnerXml;

    internal Dictionary<Guid, string> GetUsers() => this._userNodes;

    internal void SetSettingsForUser(XmlNode userNode)
    {
      XmlAttribute attribute = userNode.Attributes["Guid"];
      if (attribute == null || !GuidHelper.IsGuid(attribute.Value))
        return;
      Guid key = new Guid(attribute.Value);
      if (!(key != Guid.Empty))
        return;
      this._userNodes[key] = userNode.InnerXml;
    }

    internal string GetSettingsForUser(Guid userGuid)
    {
      return !this._userNodes.ContainsKey(userGuid) ? string.Empty : this._userNodes[userGuid];
    }

    internal void RemoveSettingsForUser(Guid userGuid) => this._userNodes.Remove(userGuid);

    internal Dictionary<Guid, string> GetRoles() => this._roleNodes;

    internal string GetSettingsForRole(Guid roleGuid)
    {
      return !this._roleNodes.ContainsKey(roleGuid) ? string.Empty : this._roleNodes[roleGuid];
    }

    internal void SetSettingsForRole(XmlNode roleNode)
    {
      XmlAttribute attribute = roleNode.Attributes["Guid"];
      if (attribute == null || !GuidHelper.IsGuid(attribute.Value))
        return;
      Guid key = new Guid(attribute.Value);
      if (!(key != Guid.Empty))
        return;
      this._roleNodes[key] = roleNode.InnerXml;
    }

    internal void RemoveSettingsForRole(Guid roleGuid) => this._roleNodes.Remove(roleGuid);

    internal TablesDisplayService.ObjectInfo Clone(Guid objGuid)
    {
      TablesDisplayService.ObjectInfo objectInfo = new TablesDisplayService.ObjectInfo(objGuid);
      if (!string.IsNullOrEmpty(this._general))
        objectInfo._general = this._general;
      foreach (KeyValuePair<Guid, string> sortedColumn in this._sortedColumns)
        objectInfo._sortedColumns.Add(sortedColumn.Key, sortedColumn.Value);
      foreach (KeyValuePair<Guid, string> userNode in this._userNodes)
        objectInfo._userNodes.Add(userNode.Key, userNode.Value);
      foreach (KeyValuePair<Guid, string> roleNode in this._roleNodes)
        objectInfo._roleNodes.Add(roleNode.Key, roleNode.Value);
      return objectInfo;
    }
  }
}
