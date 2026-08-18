// Decompiled with JetBrains decompiler
// Type: Intermech.Ldap.SynchronizeDirectoryService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Xml;


namespace Intermech.Ldap;

public class SynchronizeDirectoryService
{
  [Obsolete("Следует использовать ReadSyncSettings(Guid sessionGUID, out bool multiDomainSyncEnabled...", false)]
  public static int ReadSyncSettings(
    Guid sessionGUID,
    out string catalogName,
    out List<string> exclusionUserSIDs)
  {
    HybridDictionary catalogsAndExclusionUsers = new HybridDictionary();
    exclusionUserSIDs = new List<string>();
    int num = SynchronizeDirectoryService.ReadSyncSettings(sessionGUID, out catalogName, out catalogsAndExclusionUsers);
    if (num < 0 || !catalogsAndExclusionUsers.Contains((object) catalogName))
      return num;
    exclusionUserSIDs = catalogsAndExclusionUsers[(object) catalogName] as List<string>;
    return num;
  }

  public static int ReadSyncSettings(
    Guid sessionGUID,
    out string defaultCatalog,
    out HybridDictionary catalogsAndExclusionUsers)
  {
    defaultCatalog = string.Empty;
    catalogsAndExclusionUsers = new HybridDictionary();
    string string4Xml;
    int num = SynchronizeDirectoryService.ReadConfigString4Xml(sessionGUID, out string4Xml);
    if (num != 0)
      return num;
    if (!string.IsNullOrEmpty(string4Xml))
    {
      XmlDocument doc = new XmlDocument();
      doc.InnerXml = string4Xml;
      SynchronizeDirectoryService.ParseConfigXml(doc, out defaultCatalog, out catalogsAndExclusionUsers);
    }
    return 0;
  }

  [Obsolete("Следует использовать WriteSyncSettings(Guid sessionGUID, bool multiDomainSyncEnabled...", false)]
  public static int WriteSyncSettings(
    Guid sessionGUID,
    string catalogName,
    List<string> exclusionUsers,
    bool withSync)
  {
    int num = 0;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (sessionById == null)
      return -1;
    IDBConfigurations configurations = sessionById.Configurations;
    if (configurations == null)
      return -2;
    string outerXml = SynchronizeDirectoryService.GetConfigXml(catalogName, exclusionUsers).OuterXml;
    using (MemoryStream output = new MemoryStream(outerXml.Length))
    {
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
      {
        binaryWriter.Write(outerXml);
        binaryWriter.Flush();
        BlobInformation config_info = new BlobInformation(output.Length, output.Length, DateTime.Now, LdapConsts.ConfigName, ArcMethods.NotPacked, string.Empty);
        configurations.WriteConfigData(config_info, output.ToArray(), 0L);
      }
    }
    if (withSync)
      num = SynchronizeDirectoryService.SynchronizeDirectory(sessionGUID);
    return num;
  }

  public static int WriteSyncSettings(
    Guid sessionGUID,
    string defaultCatalog,
    HybridDictionary catalogsAndExclusionUsers,
    bool withSync)
  {
    int num = 0;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (sessionById == null)
      return -1;
    IDBConfigurations configurations = sessionById.Configurations;
    if (configurations == null)
      return -2;
    string outerXml = SynchronizeDirectoryService.GetConfigXml(defaultCatalog, catalogsAndExclusionUsers).OuterXml;
    using (MemoryStream output = new MemoryStream(outerXml.Length))
    {
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
      {
        binaryWriter.Write(outerXml);
        binaryWriter.Flush();
        BlobInformation config_info = new BlobInformation(output.Length, output.Length, DateTime.Now, LdapConsts.ConfigName, ArcMethods.NotPacked, string.Empty);
        configurations.WriteConfigData(config_info, output.ToArray(), 0L);
      }
    }
    if (withSync)
      num = SynchronizeDirectoryService.SynchronizeDirectory(sessionGUID);
    return num;
  }

  private static int ReadConfigString4Xml(Guid sessionGUID, out string string4Xml)
  {
    string4Xml = (string) null;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (sessionById == null)
      return -1;
    IDBConfigurations configurations = sessionById.Configurations;
    if (configurations == null)
      return -2;
    BlobInformation config_info;
    byte[] config_file;
    configurations.LoadConfigData(LdapConsts.ConfigName, out config_info, out config_file, 0L);
    if (config_info.RealFileSize == 0L || config_file == null || config_file.Length == 0)
      return -3;
    using (MemoryStream input = new MemoryStream(config_file))
    {
      input.Position = 0L;
      using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        string4Xml = binaryReader.ReadString();
    }
    return 0;
  }

  private static void ParseConfigXml(
    XmlDocument doc,
    out string defaultCatalog,
    out HybridDictionary catalogsAndExclusionUsers)
  {
    defaultCatalog = string.Empty;
    catalogsAndExclusionUsers = new HybridDictionary();
    bool flag1 = false;
    if (!(doc.SelectSingleNode(LdapConsts.xmlConfiguration) is XmlElement xmlElement))
      return;
    if (xmlElement.Attributes[LdapConsts.xmlDefaultCatalog] != null)
      defaultCatalog = xmlElement.Attributes[LdapConsts.xmlDefaultCatalog].Value;
    else
      flag1 = true;
    bool flag2 = false;
    for (int i1 = 0; i1 < xmlElement.ChildNodes.Count; ++i1)
    {
      if (xmlElement.ChildNodes[i1].Name == LdapConsts.xmlCatalog)
      {
        string key = xmlElement.ChildNodes[i1].Attributes[LdapConsts.xmlName].Value;
        List<string> exclusionUserSIDs = new List<string>();
        if (flag1 && !flag2)
        {
          defaultCatalog = key;
          flag2 = true;
          SynchronizeDirectoryService.CollectSIDs((XmlNode) (doc.SelectSingleNode($"{LdapConsts.xmlConfiguration}/{LdapConsts.xmlExclusions}") as XmlElement), exclusionUserSIDs);
        }
        if (!flag1)
        {
          for (int i2 = 0; i2 < xmlElement.ChildNodes[i1].ChildNodes.Count; ++i2)
          {
            if (xmlElement.ChildNodes[i1].ChildNodes[i2].Name == LdapConsts.xmlExclusions)
            {
              SynchronizeDirectoryService.CollectSIDs(xmlElement.ChildNodes[i1].ChildNodes[i2], exclusionUserSIDs);
              break;
            }
          }
        }
        catalogsAndExclusionUsers.Add((object) key, (object) exclusionUserSIDs);
      }
    }
  }

  private static void CollectSIDs(XmlNode elementExclusions, List<string> exclusionUserSIDs)
  {
    if (elementExclusions == null)
      return;
    for (int i = 0; i < elementExclusions.ChildNodes.Count; ++i)
    {
      if (elementExclusions.ChildNodes[i].Name == LdapConsts.xmlUser)
      {
        XmlAttribute attribute = elementExclusions.ChildNodes[i].Attributes[LdapConsts.xmlSID];
        if (attribute != null)
          exclusionUserSIDs.Add(attribute.Value);
      }
    }
  }

  [Obsolete("Следует использовать XmlDocument GetConfigXml(bool multiDomainSyncEnabled, string defaultCatalog...", false)]
  private static XmlDocument GetConfigXml(string catalogName, List<string> exclusionUsers)
  {
    XmlDocument configXml = new XmlDocument();
    XmlElement element1 = configXml.CreateElement(LdapConsts.xmlConfiguration);
    configXml.AppendChild((XmlNode) element1);
    XmlElement element2 = configXml.CreateElement(LdapConsts.xmlCatalog);
    XmlAttribute attribute1 = configXml.CreateAttribute(LdapConsts.xmlName);
    attribute1.Value = catalogName;
    element2.Attributes.Append(attribute1);
    element1.AppendChild((XmlNode) element2);
    XmlElement element3 = configXml.CreateElement(LdapConsts.xmlExclusions);
    for (int index = 0; index < exclusionUsers.Count; ++index)
    {
      XmlElement element4 = configXml.CreateElement(LdapConsts.xmlUser);
      XmlAttribute attribute2 = configXml.CreateAttribute(LdapConsts.xmlSID);
      attribute2.Value = exclusionUsers[index];
      element4.Attributes.Append(attribute2);
      element3.AppendChild((XmlNode) element4);
    }
    element1.AppendChild((XmlNode) element3);
    return configXml;
  }

  private static XmlDocument GetConfigXml(
    string defaultCatalog,
    HybridDictionary catalogsAndExclusionUsers)
  {
    XmlDocument configXml = new XmlDocument();
    XmlElement element1 = configXml.CreateElement(LdapConsts.xmlConfiguration);
    XmlAttribute attribute1 = configXml.CreateAttribute(LdapConsts.xmlDefaultCatalog);
    attribute1.Value = defaultCatalog;
    element1.Attributes.Append(attribute1);
    configXml.AppendChild((XmlNode) element1);
    foreach (DictionaryEntry andExclusionUser in catalogsAndExclusionUsers)
    {
      XmlElement element2 = configXml.CreateElement(LdapConsts.xmlCatalog);
      XmlAttribute attribute2 = configXml.CreateAttribute(LdapConsts.xmlName);
      attribute2.Value = andExclusionUser.Key.ToString();
      element2.Attributes.Append(attribute2);
      XmlElement element3 = configXml.CreateElement(LdapConsts.xmlExclusions);
      if (andExclusionUser.Value != null && andExclusionUser.Value is List<string>)
      {
        List<string> stringList = (List<string>) andExclusionUser.Value;
        for (int index = 0; index < stringList.Count; ++index)
        {
          XmlElement element4 = configXml.CreateElement(LdapConsts.xmlUser);
          XmlAttribute attribute3 = configXml.CreateAttribute(LdapConsts.xmlSID);
          attribute3.Value = stringList[index];
          element4.Attributes.Append(attribute3);
          element3.AppendChild((XmlNode) element4);
        }
      }
      element2.AppendChild((XmlNode) element3);
      element1.AppendChild((XmlNode) element2);
    }
    return configXml;
  }

  public static int SynchronizeDirectory(Guid sessionGUID)
  {
    int num1 = 0;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (sessionById == null)
      return -1;
    string defaultCatalog = string.Empty;
    HybridDictionary catalogsAndExclusionUsers = new HybridDictionary();
    if (SynchronizeDirectoryService.ReadSyncSettings(sessionGUID, out defaultCatalog, out catalogsAndExclusionUsers) != 0)
    {
      (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, LocalizationHolder.rm.GetString("SyncConfigNotFoundEvent"), ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
      return -2;
    }
    (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncConfigReadEvent"), (object) "Список каталогов для синхронизации"), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
    foreach (DictionaryEntry dictionaryEntry in catalogsAndExclusionUsers)
    {
      string domainName = dictionaryEntry.Key.ToString();
      int num2 = SynchronizeDirectoryService.SynchronizeDirectory(sessionGUID, domainName, defaultCatalog, catalogsAndExclusionUsers);
      if (num2 != 0 && num1 == 0)
        num1 = num2;
    }
    return num1;
  }

  public static int SynchronizeDirectory(Guid sessionGUID, string domainName)
  {
    return SynchronizeDirectoryService.SynchronizeDirectory(sessionGUID, domainName, (string) null, (HybridDictionary) null);
  }

  private static string AssignLoginPostfix(string _selectedDomain, string _defaultDomain)
  {
    return !_defaultDomain.Equals(_selectedDomain, StringComparison.InvariantCultureIgnoreCase) ? "@" + _selectedDomain : string.Empty;
  }

  private static string TrimAtSign(string keyString)
  {
    int length = keyString.IndexOf('@');
    if (length != -1)
      keyString = keyString.Substring(0, length);
    return keyString;
  }

  public static int SynchronizeDirectory(
    Guid sessionGUID,
    string domainName,
    string defaultCatalogCached,
    HybridDictionary catalogsAndExclusionUsersCached)
  {
    int num1 = 0;
    string empty1 = string.Empty;
    List<string> stringList1 = new List<string>();
    HybridDictionary users = (HybridDictionary) null;
    LdapHolder ldapHolder = new LdapHolder();
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (sessionById == null)
      return -1;
    (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, LocalizationHolder.rm.GetString("SyncStartEvent"), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
    string defaultCatalog = defaultCatalogCached;
    HybridDictionary catalogsAndExclusionUsers = catalogsAndExclusionUsersCached;
    if (defaultCatalog == null || catalogsAndExclusionUsers == null)
    {
      if (SynchronizeDirectoryService.ReadSyncSettings(sessionGUID, out defaultCatalog, out catalogsAndExclusionUsers) != 0)
      {
        (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, LocalizationHolder.rm.GetString("SyncConfigNotFoundEvent"), ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
        return -2;
      }
      (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncConfigReadEvent"), (object) empty1), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
    }
    string str1 = domainName;
    bool flag1 = str1.Equals(defaultCatalog, StringComparison.CurrentCultureIgnoreCase);
    string str2 = SynchronizeDirectoryService.AssignLoginPostfix(str1, defaultCatalog);
    bool flag2 = false;
    foreach (DictionaryEntry dictionaryEntry in catalogsAndExclusionUsers)
    {
      if (dictionaryEntry.Key.ToString().Equals(domainName, StringComparison.CurrentCultureIgnoreCase))
      {
        stringList1 = dictionaryEntry.Value as List<string>;
        flag2 = true;
        break;
      }
    }
    if (!flag2)
    {
      (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, $"{LocalizationHolder.rm.GetString("SyncConfigNotFoundEvent")} -> {str1}", ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
      return -5;
    }
    if (stringList1 == null)
      stringList1 = new List<string>();
    if (SynchronizeDirectoryService.ReadDBUsers(sessionGUID, out users) != 0)
    {
      (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, LocalizationHolder.rm.GetString("SyncCantReadDBUsersListEvent"), ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
      return -3;
    }
    (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, LocalizationHolder.rm.GetString("SyncDBUsersReadEvent"), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
    try
    {
      if (!ldapHolder.ReadDirectory(str1, true))
      {
        (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncCantReadLdapUsersListEvent"), (object) str1), ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
        return -4;
      }
      (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncLdapUsersReadEvent"), (object) str1), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
    }
    catch (Exception ex)
    {
      (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, $"{string.Format(LocalizationHolder.rm.GetString("SyncCantReadLdapUsersListEvent"), (object) str1)} {ex.Message}", ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
      return -6;
    }
    List<string> stringList2 = new List<string>();
    foreach (DictionaryEntry hdUser in ldapHolder.hdUsers)
    {
      if (stringList1.IndexOf(((HybridDictionary) hdUser.Value)[(object) LdapConsts.ADObjectSID].ToString()) == -1)
        stringList2.Add(hdUser.Key.ToString());
    }
    List<string> stringList3 = new List<string>();
    List<string> stringList4 = new List<string>();
    foreach (DictionaryEntry dictionaryEntry in users)
    {
      string str3 = dictionaryEntry.Key.ToString();
      long dbUserID = (long) ((HybridDictionary) dictionaryEntry.Value)[(object) LdapConsts.DBID];
      ((HybridDictionary) dictionaryEntry.Value)[(object) LdapConsts.ADSAMAccountName].ToString();
      string str4 = ((HybridDictionary) dictionaryEntry.Value)[(object) LdapConsts.ADObjectSID].ToString();
      ((HybridDictionary) dictionaryEntry.Value)[(object) LdapConsts.ADDisplayName].ToString();
      if (str4 != string.Empty)
      {
        if (stringList1.IndexOf(str4) != -1)
        {
          SynchronizeDirectoryService.MoveUserToDeletedGroup(sessionById, dbUserID);
          (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncMovedToDeletedGroupEvent"), (object) str3), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
        }
        else
        {
          bool flag3 = false;
          string empty2 = string.Empty;
          foreach (DictionaryEntry hdUser in ldapHolder.hdUsers)
          {
            if (((HybridDictionary) hdUser.Value)[(object) LdapConsts.ADObjectSID].ToString() == str4)
            {
              flag3 = true;
              empty2 = hdUser.Key.ToString();
              break;
            }
          }
          if (flag3)
          {
            stringList2.Remove(empty2);
            HybridDictionary hdUser = (HybridDictionary) ldapHolder.hdUsers[(object) empty2];
            if (!str3.Equals(empty2 + str2, StringComparison.CurrentCultureIgnoreCase) && users.Contains((object) (empty2 + str2)))
            {
              (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncDoubleConflictEvent"), (object) str3, (object) empty2), ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
            }
            else
            {
              if (!str3.Equals(empty2 + str2, StringComparison.CurrentCultureIgnoreCase))
              {
                stringList3.Add(str3);
                stringList4.Add(empty2 + str2);
              }
              ((HybridDictionary) users[(object) str3])[(object) LdapConsts.ADSAMAccountName] = (object) (hdUser[(object) LdapConsts.ADSAMAccountName].ToString() + str2);
              ((HybridDictionary) users[(object) str3])[(object) LdapConsts.ADDisplayName] = (object) hdUser[(object) LdapConsts.ADDisplayName].ToString();
              SynchronizeDirectoryService.WriteDBUser(sessionById, (HybridDictionary) users[(object) str3], (SearchResult) hdUser[(object) LdapConsts._SearchResult_], false);
            }
          }
          else
          {
            SynchronizeDirectoryService.MoveUserToDeletedGroup(sessionById, dbUserID);
            (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncMovedToDeletedGroupEvent"), (object) str3), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
          }
        }
      }
      else
      {
        int num2 = str3.IndexOf('@');
        if (flag1 && num2 == -1 || !flag1 && num2 != -1 && str3.Substring(num2 + 1).Equals(domainName, StringComparison.CurrentCultureIgnoreCase))
        {
          if (ldapHolder.hdUsers.Contains((object) SynchronizeDirectoryService.TrimAtSign(str3)))
          {
            string key = SynchronizeDirectoryService.TrimAtSign(str3);
            stringList2.Remove(key);
            if (stringList1.IndexOf(((HybridDictionary) ldapHolder.hdUsers[(object) key])[(object) LdapConsts.ADObjectSID].ToString()) != -1)
            {
              SynchronizeDirectoryService.MoveUserToLocalGroup(sessionById, dbUserID);
            }
            else
            {
              HybridDictionary hdUser = (HybridDictionary) ldapHolder.hdUsers[(object) key];
              ((HybridDictionary) users[(object) str3])[(object) LdapConsts.ADObjectSID] = (object) hdUser[(object) LdapConsts.ADObjectSID].ToString();
              ((HybridDictionary) users[(object) str3])[(object) LdapConsts.ADDisplayName] = (object) hdUser[(object) LdapConsts.ADDisplayName].ToString();
              SynchronizeDirectoryService.WriteDBUser(sessionById, (HybridDictionary) users[(object) str3], (SearchResult) hdUser[(object) LdapConsts._SearchResult_], false);
            }
          }
          else
            SynchronizeDirectoryService.MoveUserToLocalGroup(sessionById, dbUserID);
        }
      }
    }
    for (int index = 0; index < stringList2.Count; ++index)
    {
      HybridDictionary hdUser = (HybridDictionary) ldapHolder.hdUsers[(object) stringList2[index]];
      string str5 = hdUser[(object) LdapConsts.ADSAMAccountName].ToString();
      hdUser[(object) LdapConsts.ADSAMAccountName] = (object) (hdUser[(object) LdapConsts.ADSAMAccountName].ToString() + str2);
      try
      {
        string str6 = hdUser[(object) LdapConsts.ADSAMAccountName].ToString();
        string str7 = hdUser[(object) LdapConsts.ADDisplayName].ToString();
        string str8 = hdUser[(object) LdapConsts.ADObjectSID].ToString();
        IDBObjectCollection objectCollection = sessionById.GetObjectCollection(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
        if (objectCollection != null)
        {
          IDBObject dbObject = objectCollection.Create();
          if (dbObject != null)
          {
            long objectId = dbObject.ObjectID;
            IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid1 != null)
              attributeByGuid1.AsString = str7;
            IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid2 != null)
              attributeByGuid2.AsString = str6;
            IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(new Guid("cadd93c1-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid3 != null)
              attributeByGuid3.AsString = str8;
            try
            {
              dbObject.CheckIn();
              hdUser[(object) LdapConsts.DBID] = (object) Math.Abs(objectId);
              SynchronizeDirectoryService.MoveUserToNewGroup(sessionById, Math.Abs(objectId));
              SynchronizeDirectoryService.WriteDBUser(sessionById, hdUser, (SearchResult) hdUser[(object) LdapConsts._SearchResult_], true);
              (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncAddedAndMovedToNewGroupEvent"), (object) str6), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
            }
            catch (Exception ex)
            {
              (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, string.Format(LocalizationHolder.rm.GetString("SyncProcessErrorEvent"), (object) str6, (object) ex.Message), ActionType.DirectorySyncronization, EventlogRecordType.Error, sessionById.UserID, sessionById.ComputerName, sessionById);
            }
          }
        }
      }
      finally
      {
        hdUser[(object) LdapConsts.ADSAMAccountName] = (object) str5;
      }
    }
    (sessionById as UserSession).EventLogHelper.AddEvent(0L, 0L, 0, 0L, string.Empty, LocalizationHolder.rm.GetString("SyncEndEvent"), ActionType.DirectorySyncronization, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, sessionById);
    return num1;
  }

  private static bool WriteDBUser(
    IUserSession session,
    HybridDictionary dbUser,
    SearchResult sr,
    bool srOnly)
  {
    long objectID = (long) dbUser[(object) LdapConsts.DBID];
    string str1 = dbUser[(object) LdapConsts.ADSAMAccountName].ToString();
    string str2 = dbUser[(object) LdapConsts.ADDisplayName].ToString();
    string str3 = dbUser[(object) LdapConsts.ADObjectSID].ToString();
    IDBObject iDBObject = session.GetObject(objectID);
    if (iDBObject != null)
    {
      if (!srOnly)
      {
        IDBAttribute attributeByGuid1 = iDBObject.GetAttributeByGuid(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid1 != null)
          attributeByGuid1.AsString = str2;
        IDBAttribute attributeByGuid2 = iDBObject.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid2 != null)
          attributeByGuid2.AsString = str1;
        IDBAttribute attributeByGuid3 = iDBObject.GetAttributeByGuid(new Guid("cadd93c1-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid3 != null)
          attributeByGuid3.AsString = str3;
      }
      if (sr != null)
      {
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultMail], new Guid("cad002de-306c-11d8-b4e9-00304f19f545"));
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultTelephoneNumber], new Guid("cad002da-306c-11d8-b4e9-00304f19f545"));
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultPhysicalDeliveryOfficeName], new Guid("cad002db-306c-11d8-b4e9-00304f19f545"));
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultHomePhone], new Guid("cad002dd-306c-11d8-b4e9-00304f19f545"));
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultMobilePhone], new Guid("cad015df-306c-11d8-b4e9-00304f19f545"));
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultHomePostalAddress], new Guid("cad002dc-306c-11d8-b4e9-00304f19f545"));
        SynchronizeDirectoryService.WriteSRAttribute(iDBObject, sr.Properties[LdapConsts.SearchResultPostalAddress], new Guid("cad015dd-306c-11d8-b4e9-00304f19f545"));
      }
    }
    return true;
  }

  private static void WriteSRAttribute(
    IDBObject iDBObject,
    ResultPropertyValueCollection res,
    Guid attrGuid)
  {
    if (res == null || res.Count <= 0 || res[0] == null || !(res[0].ToString().Trim() != string.Empty))
      return;
    IDBAttribute dbAttribute = iDBObject.GetAttributeByGuid(attrGuid) ?? iDBObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeType(attrGuid).AttributeID, false);
    if (dbAttribute == null)
      return;
    dbAttribute.AsString = res[0].ToString();
  }

  private static void MoveUserToLocalGroup(IUserSession session, long dbUserID)
  {
    int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"));
    IDBObject dbObject1 = session.GetObject(new Guid("cadd93f0-306c-11d8-b4e9-00304f19f545"));
    if (dbObject1 == null)
      return;
    IDBObject dbObject2 = session.GetObject(new Guid("cadd93ee-306c-11d8-b4e9-00304f19f545"));
    if (dbObject2 == null)
      return;
    IDBObject dbObject3 = session.GetObject(dbUserID);
    if (dbObject3 == null)
      return;
    session.GetRelation(dbObject2.ObjectID, dbObject3.ID, relationTypeId)?.Delete(0L);
    session.GetRelation(dbObject1.ObjectID, dbObject3.ID, relationTypeId)?.Delete(0L);
  }

  private static void MoveUserToDeletedGroup(IUserSession session, long dbUserID)
  {
    int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"));
    IDBObject dbObject1 = session.GetObject(new Guid("cadd93f0-306c-11d8-b4e9-00304f19f545"));
    if (dbObject1 == null)
      return;
    IDBObject dbObject2 = session.GetObject(new Guid("cadd93ee-306c-11d8-b4e9-00304f19f545"));
    if (dbObject2 == null)
      return;
    IDBObject dbObject3 = session.GetObject(dbUserID);
    if (dbObject3 == null)
      return;
    session.GetRelation(dbObject2.ObjectID, dbObject3.ID, relationTypeId)?.Delete(0L);
    if (session.GetRelation(dbObject1.ObjectID, dbObject3.ID, relationTypeId) != null)
      return;
    session.GetRelationCollection(relationTypeId)?.Create(dbObject1.ObjectID, dbObject3.ObjectID);
  }

  private static void MoveUserToNewGroup(IUserSession session, long newID)
  {
    int relationType = session.GetRelationType(new Guid("cad00022-306c-11d8-b4e9-00304f19f545")).RelationType;
    IDBObject dbObject1 = session.GetObject(new Guid("cadd93f0-306c-11d8-b4e9-00304f19f545"));
    if (dbObject1 == null)
      return;
    IDBObject dbObject2 = session.GetObject(new Guid("cadd93ee-306c-11d8-b4e9-00304f19f545"));
    if (dbObject2 == null)
      return;
    IDBObject dbObject3 = session.GetObject(newID);
    if (dbObject3 == null)
      return;
    session.GetRelation(dbObject1.ObjectID, dbObject3.ID, relationType)?.Delete(0L);
    if (session.GetRelation(dbObject2.ObjectID, dbObject3.ID, relationType) != null)
      return;
    session.GetRelationCollection(relationType)?.Create(dbObject2.ObjectID, dbObject3.ObjectID);
  }

  public static int ReadDBUsers(Guid sessionGUID, out HybridDictionary users)
  {
    users = new HybridDictionary();
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    IDBObjectCollection objectCollection = sessionById.GetObjectCollection(sessionById.IdentHelper.UsersTypeID);
    if (objectCollection != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(new DBRecordSetParams((ConditionStructure[]) null, new object[5]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION,
        (object) new Guid("cad00018-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cad0001d-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cadd93c1-306c-11d8-b4e9-00304f19f545")
      })).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        string str1 = Convert.ToString(row[1]);
        string str2 = Convert.ToString(row[2]);
        Convert.ToString(row[3]);
        string str3 = Convert.ToString(row[4]);
        users[(object) str2.ToUpper()] = (object) new HybridDictionary()
        {
          [(object) LdapConsts.DBID] = (object) int64,
          [(object) LdapConsts.ADSAMAccountName] = (object) str2,
          [(object) LdapConsts.ADDisplayName] = (object) str1,
          [(object) LdapConsts.ADObjectSID] = (object) str3
        };
      }
    }
    return 0;
  }
}
