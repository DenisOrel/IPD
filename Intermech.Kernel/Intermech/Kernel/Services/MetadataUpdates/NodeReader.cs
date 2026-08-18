// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.NodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class NodeReader
{
  protected XmlNode rootNode;
  protected IUserSession session;
  protected string directory;
  protected IObligatoryObjectsRegistryService obligatoryObjects;
  protected Guid GUID;
  protected IEventLogHelper eventHelper;
  protected readonly IPropertyFactory propertyFactory;

  public NodeReader(
    XmlNode node,
    IUserSession userSession,
    IEventLogHelper eHelper,
    string curDirectory,
    IObligatoryObjectsRegistryService obligatoryObjects,
    Guid guid,
    IPropertyFactory propertyFactory)
  {
    this.rootNode = node;
    this.session = userSession;
    this.directory = curDirectory;
    this.obligatoryObjects = obligatoryObjects;
    this.eventHelper = eHelper;
    this.GUID = guid;
    this.propertyFactory = propertyFactory;
  }

  public void Read()
  {
    this.propertyFactory.Directory = this.directory;
    this.propertyFactory.Read(this.session, this.rootNode);
    int categoryID;
    object id;
    this.OnRead(out categoryID, out id);
    this.RegisterObligatoryObject(categoryID, id);
  }

  protected abstract void OnRead(out int categoryID, out object id);

  private void RegisterObligatoryObject(int categoryID, object id)
  {
    this.obligatoryObjects.RegisterObligatoryObject(categoryID, id);
    List<ObligatoryElementKey> obligatoryElements = this.propertyFactory.ObligatoryElements;
    if (obligatoryElements == null)
      return;
    foreach (ObligatoryElementKey elementKey in obligatoryElements)
      this.obligatoryObjects.RegisterObligatoryObjectElement(categoryID, id, elementKey);
  }

  protected Dictionary<string, List<MetadataExtension>> GetExtensions(
    IUserSession session,
    XmlNode propNode)
  {
    Dictionary<string, List<MetadataExtension>> extensions = new Dictionary<string, List<MetadataExtension>>(propNode.ChildNodes.Count);
    foreach (XmlNode childNode in propNode.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Extension")
      {
        string str = childNode.Attributes["ParamName"].Value;
        List<MetadataExtension> metadataExtensionList;
        if (!extensions.TryGetValue(str, out metadataExtensionList))
        {
          metadataExtensionList = new List<MetadataExtension>();
          extensions.Add(str, metadataExtensionList);
        }
        int int32 = Convert.ToInt32(childNode.Attributes["CategoryType"].Value);
        string languageId = childNode.Attributes["Value"].Value;
        int num;
        switch (int32)
        {
          case 1:
            if (GuidHelper.IsGuid(languageId))
            {
              languageId = session.GetObjectInfo(new Guid(languageId)).ObjectID.ToString();
              break;
            }
            break;
          case 3:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetAttributeTypeID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 4:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetObjectTypeID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 6:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetRelationTypeID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 7:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetLCStepID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 8:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetLCLevelID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 9:
            if (GuidHelper.IsGuid(languageId))
            {
              languageId = session.GetLanguage(new Guid(languageId)).LanguageID;
              break;
            }
            break;
          case 11:
            if (GuidHelper.IsGuid(languageId))
            {
              languageId = session.GetSubjectAreaType(new Guid(languageId)).AreaID.ToString();
              break;
            }
            break;
          case 16 /*0x10*/:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetLCSchemaID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
        }
        metadataExtensionList.Add(new MetadataExtension(str, Convert.ToInt32(childNode.Attributes["CategoryType"].Value), Convert.ToInt32(childNode.Attributes["InListID"].Value), languageId));
      }
    }
    return extensions;
  }

  protected void SetExtensions(
    DBMetadataExtensions dbExt,
    Dictionary<string, List<MetadataExtension>> extensions)
  {
    foreach (KeyValuePair<string, List<MetadataExtension>> keyValuePair in extensions)
    {
      if (keyValuePair.Value.Count == 1)
      {
        dbExt.SetMDValue(keyValuePair.Value[0].ParamName, keyValuePair.Value[0].CategoryType, keyValuePair.Value[0].Value);
      }
      else
      {
        string[] valuesList = new string[keyValuePair.Value.Count];
        for (int index = 0; index < keyValuePair.Value.Count; ++index)
          valuesList[index] = keyValuePair.Value[index].Value;
        dbExt.SetMDValues(keyValuePair.Value[0].ParamName, keyValuePair.Value[0].CategoryType, valuesList);
      }
    }
  }

  protected void SetAccess(
    IDBSecurity seсurity,
    List<UpdateScriptAccessRight> avs,
    int categoryType,
    long categoryID)
  {
    if (avs == null || avs.Count == 0)
      return;
    DataTable accessList = seсurity.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _);
    for (int index = 0; index < avs.Count; ++index)
    {
      UpdateScriptAccessRight av = avs[index];
      DataRow row = accessList.NewRow();
      row["F_RIGHT_TYPE"] = (object) av.RightType;
      row["F_RIGHT_ID"] = (object) av.RightID;
      QuickObjectInfo objectInfo1 = this.session.GetObjectInfo(av.UserID);
      row["F_USER_ID"] = (object) objectInfo1.ObjectID;
      QuickObjectInfo objectInfo2 = this.session.GetObjectInfo(av.OwnerID);
      row["F_OWNER_ID"] = (object) objectInfo2.ObjectID;
      row["F_CATEGORY_TYPE"] = (object) categoryType;
      row["F_CATEGORY_ID"] = (object) categoryID;
      row["F_PARENT_KEY"] = (object) -1;
      row["F_KEY"] = (object) index;
      accessList.Rows.Add(row);
    }
    accessList.AcceptChanges();
    seсurity.SetAccess(accessList);
  }
}
