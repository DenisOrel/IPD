// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.DocumentTypeSettingsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;


namespace Intermech.Kernel.Services;

[Serializable]
public class DocumentTypeSettingsService : LongLifeObject, IDocumentTypeSettingsService
{
  private ConcurrentDictionary<int, DocumentTypeSettings> _documentTypeSettings = new ConcurrentDictionary<int, DocumentTypeSettings>();
  private int docsObjectTypeId = -1;
  private int constrDocsObjectTypeId = -1;
  private int docObjTypeSettingsAttributeId = -1;
  private readonly string xmlConfigurationTag = "configuration";
  private readonly string xmlDocumentFileExt = "DocumentFileExt";
  private readonly string xmlAdditionalDocumentFileExts = "AdditionalDocumentFileExts";
  private readonly string xmlOutputObjectTypes = "OutputObjectTypes";
  private readonly string xmlDocumentTypeName = "DocumentTypeName";
  private readonly string xmlDocumentTypeCode = "DocumentTypeCode";
  private readonly string xmlDocumentNameInStamp = "DocumentNameInStamp";
  private readonly string xmlDocumentTypeCodeInDesignation = "DocumentTypeCodeInDesignation";

  public void InitCache(IUserSession session, ContainerService iContainerService)
  {
    KeyValuePair<Guid, long>[] objectTypeContainers = iContainerService.GetObjectTypeContainers();
    List<long> longList = new List<long>(((IEnumerable<KeyValuePair<Guid, long>>) objectTypeContainers).Select<KeyValuePair<Guid, long>, long>((Func<KeyValuePair<Guid, long>, long>) (kvp => kvp.Value)));
    foreach (IDBObject dbObject in session.GetObjects(longList.ToArray(), false))
    {
      IDBObject dBObject = dbObject;
      DocumentTypeSettings documentTypeSettings = this.GetDocumentTypeSettings(dBObject);
      foreach (KeyValuePair<Guid, long> keyValuePair in ((IEnumerable<KeyValuePair<Guid, long>>) objectTypeContainers).Where<KeyValuePair<Guid, long>>((Func<KeyValuePair<Guid, long>, bool>) (kvp => kvp.Value == dBObject.ObjectID)))
      {
        IDBObjectType objectType = session.GetObjectType(keyValuePair.Key, false);
        if (objectType != null)
        {
          this._documentTypeSettings.TryAdd(objectType.ObjectType, documentTypeSettings);
          break;
        }
      }
    }
  }

  private DocumentTypeSettings GetDocumentTypeSettings(IDBObject dBObject)
  {
    DocumentTypeSettings documentTypeSettings = DocumentTypeSettings.CreateDefault();
    IDBAttribute attributeByGuid = dBObject.GetAttributeByGuid(new Guid("cad00626-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null)
    {
      IMemoReader memoReader = attributeByGuid as IMemoReader;
      object obj = (object) null;
      if (memoReader.OpenMemo(0) > 0)
      {
        obj = (object) memoReader.ReadDataBlock();
        memoReader.CloseMemo();
      }
      if (obj != null)
      {
        XmlDocument xmlDocument = new XmlDocument();
        using (StringReader txtReader = new StringReader(new string((char[]) obj)))
          xmlDocument.Load((TextReader) txtReader);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//" + this.xmlConfigurationTag);
        if (xmlNode != null)
        {
          XmlAttribute attribute1 = xmlNode.Attributes[this.xmlDocumentFileExt];
          documentTypeSettings.DocumentFileExt = attribute1 != null ? attribute1.Value : string.Empty;
          XmlAttribute attribute2 = xmlNode.Attributes[this.xmlAdditionalDocumentFileExts];
          documentTypeSettings.AdditionalDocumentFileExts = attribute2 != null ? attribute2.Value : string.Empty;
          XmlAttribute attribute3 = xmlNode.Attributes[this.xmlOutputObjectTypes];
          documentTypeSettings.OutputObjectTypes = attribute3 != null ? attribute3.Value : string.Empty;
          XmlAttribute attribute4 = xmlNode.Attributes[this.xmlDocumentTypeName];
          documentTypeSettings.DocumentTypeName = attribute4 != null ? attribute4.Value : string.Empty;
          XmlAttribute attribute5 = xmlNode.Attributes[this.xmlDocumentTypeCode];
          documentTypeSettings.DocumentTypeCode = attribute5 != null ? attribute5.Value : string.Empty;
          XmlAttribute attribute6 = xmlNode.Attributes[this.xmlDocumentNameInStamp];
          documentTypeSettings.DocumentNameInStamp = attribute6 == null || attribute6.Value == "true";
          XmlAttribute attribute7 = xmlNode.Attributes[this.xmlDocumentTypeCodeInDesignation];
          documentTypeSettings.DocumentTypeCodeInDesignation = attribute7 == null || attribute7.Value == "true";
        }
      }
    }
    return documentTypeSettings;
  }

  public List<string> GetDocSuffixes()
  {
    List<string> docSuffixes = new List<string>();
    foreach (DocumentTypeSettings documentTypeSettings in this._documentTypeSettings.Select<KeyValuePair<int, DocumentTypeSettings>, DocumentTypeSettings>((Func<KeyValuePair<int, DocumentTypeSettings>, DocumentTypeSettings>) (kvp => kvp.Value)))
    {
      string documentTypeCode = documentTypeSettings.DocumentTypeCode;
      if (!string.IsNullOrWhiteSpace(documentTypeCode) && !docSuffixes.Contains(documentTypeCode))
        docSuffixes.Add(documentTypeCode);
    }
    return docSuffixes;
  }

  public DocumentTypeSettings GetSettings(Guid sessionGuid, int documentType)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    DocumentTypeSettings res;
    if (this._documentTypeSettings.ContainsKey(documentType))
    {
      res = this._documentTypeSettings[documentType];
    }
    else
    {
      IDBObject containerForObjectType = (sessionById.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForObjectType((object) sessionById, documentType);
      res = containerForObjectType == null ? DocumentTypeSettings.CreateDefault() : this.GetDocumentTypeSettings(containerForObjectType);
      this._documentTypeSettings[documentType] = res;
    }
    if (res.DocumentTypeName == string.Empty || res.DocumentTypeCode == string.Empty)
    {
      if (documentType != this.GetConstrDocsObjectTypeId(sessionById) && this.InheritedFromConstructorDocuments(sessionById, documentType) && this.GetParentDocumentType(sessionById, documentType) != this.GetConstrDocsObjectTypeId(sessionById))
      {
        this.FillInheritedFields(sessionById, documentType, ref res);
      }
      else
      {
        string str1 = string.Empty;
        string str2 = string.Empty;
        IDBObjectType objectType = sessionById.GetObjectType(documentType);
        if (objectType != null)
        {
          str1 = objectType.ObjectInstanceName;
          str2 = objectType.ObjectTypeShortName;
        }
        if (res.DocumentTypeName == string.Empty)
          res.DocumentTypeName = str1;
        if (res.DocumentTypeCode == string.Empty)
          res.DocumentTypeCode = str2;
      }
    }
    return res;
  }

  private int GetParentDocumentType(IUserSession session, int documentType)
  {
    int parentDocumentType = 0;
    IDBObjectType objectType = session.GetObjectType(documentType, false);
    if (objectType != null)
      parentDocumentType = objectType.ParentTypeID;
    return parentDocumentType;
  }

  public bool InheritedFromDocuments(Guid sessionGuid, int documentType)
  {
    return this.InheritedFromDocuments(UserSession.GetSessionByID(sessionGuid), documentType);
  }

  private bool InheritedFromDocuments(IUserSession session, int documentType)
  {
    bool flag = false;
    IDBObjectType objectType = session.GetObjectType(documentType);
    if (objectType.ObjectType == this.GetDocsObjectTypeId(session))
      return true;
    for (; objectType != null; objectType = session.GetObjectType(objectType.ParentTypeID, false))
    {
      if (objectType.ParentTypeID == this.GetDocsObjectTypeId(session))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public bool InheritedFromConstructorDocuments(Guid sessionGuid, int documentType)
  {
    return this.InheritedFromConstructorDocuments(UserSession.GetSessionByID(sessionGuid), documentType);
  }

  private bool InheritedFromConstructorDocuments(IUserSession session, int documentType)
  {
    bool flag = false;
    IDBObjectType objectType = session.GetObjectType(documentType);
    if (objectType.ObjectType == this.GetConstrDocsObjectTypeId(session))
      return true;
    for (; objectType != null; objectType = session.GetObjectType(objectType.ParentTypeID, false))
    {
      if (objectType.ParentTypeID == this.GetConstrDocsObjectTypeId(session))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public void SetSettings(
    Guid sessionGuid,
    int documentType,
    DocumentTypeSettings documentTypeSettings)
  {
    string Note = string.Empty;
    DocumentTypeSettings settings = this.GetSettings(sessionGuid, documentType);
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBObject containerForObjectType = (sessionById.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForObjectType((object) sessionById, documentType, true);
    IDBAttribute dbAttribute = containerForObjectType.GetAttributeByGuid(new Guid("cad00626-306c-11d8-b4e9-00304f19f545"));
    IDBObjectType objectType = sessionById.GetObjectType(documentType);
    bool flag = false;
    XmlDocument xmlDocument = new XmlDocument();
    if (dbAttribute == null)
    {
      Note = LocalizationHolder.rm.GetString("DocTypeSettingContainerInit");
      dbAttribute = containerForObjectType.Attributes.AddAttribute(this.GetDocObjTypeSettingsAttributeId(sessionById), false);
      DocumentTypeSettings documentTypeSetting = this._documentTypeSettings[documentType];
      XmlElement element = xmlDocument.CreateElement(this.xmlConfigurationTag);
      xmlDocument.AppendChild((XmlNode) element);
      element.SetAttribute(this.xmlDocumentFileExt, documentTypeSettings.DocumentFileExt);
      element.SetAttribute(this.xmlAdditionalDocumentFileExts, documentTypeSettings.AdditionalDocumentFileExts);
      element.SetAttribute(this.xmlOutputObjectTypes, documentTypeSettings.OutputObjectTypes);
      if (documentTypeSettings.DocumentTypeName != settings.DocumentTypeName)
        element.SetAttribute(this.xmlDocumentTypeName, documentTypeSettings.DocumentTypeName);
      else
        element.SetAttribute(this.xmlDocumentTypeName, string.Empty);
      if (documentTypeSettings.DocumentTypeCode != settings.DocumentTypeCode)
        element.SetAttribute(this.xmlDocumentTypeCode, documentTypeSettings.DocumentTypeCode);
      else
        element.SetAttribute(this.xmlDocumentTypeCode, string.Empty);
      element.SetAttribute(this.xmlDocumentNameInStamp, documentTypeSettings.DocumentNameInStamp ? "true" : "false");
      element.SetAttribute(this.xmlDocumentTypeCodeInDesignation, documentTypeSettings.DocumentTypeCodeInDesignation ? "true" : "false");
      flag = true;
    }
    else
    {
      IMemoReader memoReader = dbAttribute as IMemoReader;
      object obj = (object) null;
      if (memoReader.OpenMemo(0) > 0)
      {
        obj = (object) memoReader.ReadDataBlock();
        memoReader.CloseMemo();
      }
      if (obj != null)
      {
        StringReader txtReader = new StringReader(new string((char[]) obj));
        xmlDocument.Load((TextReader) txtReader);
        XmlElement xmlElement = (XmlElement) xmlDocument.SelectSingleNode("//" + this.xmlConfigurationTag);
        if (xmlElement != null)
        {
          if (documentTypeSettings.DocumentFileExt != settings.DocumentFileExt)
          {
            xmlElement.SetAttribute(this.xmlDocumentFileExt, documentTypeSettings.DocumentFileExt);
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingFileExt")};";
            flag = true;
          }
          if (documentTypeSettings.AdditionalDocumentFileExts != settings.AdditionalDocumentFileExts)
          {
            xmlElement.SetAttribute(this.xmlAdditionalDocumentFileExts, documentTypeSettings.AdditionalDocumentFileExts);
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingAdditionalDocumentFileExts")};";
            flag = true;
          }
          if (documentTypeSettings.OutputObjectTypes != settings.OutputObjectTypes)
          {
            xmlElement.SetAttribute(this.xmlOutputObjectTypes, documentTypeSettings.OutputObjectTypes);
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingOutputObjectTypes")};";
            flag = true;
          }
          if (documentTypeSettings.DocumentTypeName != settings.DocumentTypeName)
          {
            xmlElement.SetAttribute(this.xmlDocumentTypeName, documentTypeSettings.DocumentTypeName);
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingDocumentTypeName")};";
            flag = true;
          }
          if (documentTypeSettings.DocumentTypeCode != settings.DocumentTypeCode)
          {
            xmlElement.SetAttribute(this.xmlDocumentTypeCode, documentTypeSettings.DocumentTypeCode);
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingDocumentTypeCode")};";
            flag = true;
          }
          if (documentTypeSettings.DocumentNameInStamp != settings.DocumentNameInStamp)
          {
            xmlElement.SetAttribute(this.xmlDocumentNameInStamp, documentTypeSettings.DocumentNameInStamp ? "true" : "false");
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingDocumentNameInStamp")};";
            flag = true;
          }
          if (documentTypeSettings.DocumentTypeCodeInDesignation != settings.DocumentTypeCodeInDesignation)
          {
            xmlElement.SetAttribute(this.xmlDocumentTypeCodeInDesignation, documentTypeSettings.DocumentTypeCodeInDesignation ? "true" : "false");
            Note = $"{Note}{LocalizationHolder.rm.GetString("DocTypeSettingDocumentTypeCodeInDesignation")};";
            flag = true;
          }
        }
      }
    }
    if (!flag)
      return;
    StringWriter writer = new StringWriter();
    xmlDocument.Save((TextWriter) writer);
    string str = writer.ToString();
    IMemoWriter memoWriter = dbAttribute as IMemoWriter;
    memoWriter.OpenMemo(str.Length);
    memoWriter.WriteDataBlock(str.ToCharArray());
    if (ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
      service.AddEvent(containerForObjectType.ObjectID, 0L, 3, (long) dbAttribute.AttributeID, string.Format(LocalizationHolder.rm.GetString("DocTypeSettingContainer"), (object) objectType.ObjectTypeName, (object) dbAttribute.Name, (object) containerForObjectType.ObjectID), Note, ActionType.Write, EventlogRecordType.AccessGranted, sessionById.UserID, sessionById.ComputerName, sessionById);
    this.ClearCache();
  }

  public int[] GetDocumentTypesByFileExt(Guid sessionGuid, string fileExt)
  {
    fileExt = fileExt.ToUpper();
    List<int> intList = new List<int>();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.GetDocsObjectTypeId(UserSession.GetSessionByID(sessionGuid)));
    for (int index = 0; index < childrenIdRecursive.Count; ++index)
    {
      if (this.GetSettings(sessionGuid, childrenIdRecursive[index]).DocumentFileExt.ToUpper().Equals(fileExt))
        intList.Add(childrenIdRecursive[index]);
    }
    return intList.ToArray();
  }

  public int[] GetDocumentTypesByOutputObjectTypes(
    Guid sessionGuid,
    int[] outputObjectTypes,
    int rootDocumentObjectType)
  {
    List<int> intList = new List<int>();
    if (outputObjectTypes == null || outputObjectTypes.Length == 0 || !this.InheritedFromDocuments(sessionGuid, rootDocumentObjectType))
      return intList.ToArray();
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    List<string> stringList1 = new List<string>();
    for (int index = 0; index < outputObjectTypes.Length; ++index)
    {
      if (sessionById.GetObjectType(outputObjectTypes[index]) is IDBGuid objectType)
        stringList1.Add(objectType.GUID.ToString().ToLower());
    }
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(rootDocumentObjectType);
    if (childrenIdRecursive != null)
    {
      for (int index1 = 0; index1 < childrenIdRecursive.Count; ++index1)
      {
        List<string> stringList2 = new List<string>((IEnumerable<string>) this.GetSettings(sessionGuid, childrenIdRecursive[index1]).OutputObjectTypes.Split(','));
        for (int index2 = 0; index2 < stringList2.Count; ++index2)
          stringList2[index2] = stringList2[index2].ToLower();
        for (int index3 = 0; index3 < stringList1.Count; ++index3)
        {
          if (stringList2.IndexOf(stringList1[index3]) != -1)
          {
            intList.Add(childrenIdRecursive[index1]);
            break;
          }
        }
      }
    }
    return intList.ToArray();
  }

  private void FillInheritedFields(
    IUserSession session,
    int documentType,
    ref DocumentTypeSettings res)
  {
    if (!(res.DocumentTypeName == string.Empty) && !(res.DocumentTypeCode == string.Empty))
      return;
    IDBObjectType objectType = session.GetObjectType(documentType);
    if (objectType == null)
      return;
    int parentTypeId = objectType.ParentTypeID;
    if (parentTypeId == -1)
      return;
    DocumentTypeSettings settings = this.GetSettings(session.SessionGUID, parentTypeId);
    if (res.DocumentTypeName == string.Empty)
      res.DocumentTypeName = settings.DocumentTypeName;
    if (!(res.DocumentTypeCode == string.Empty))
      return;
    res.DocumentTypeCode = settings.DocumentTypeCode;
  }

  private void ClearCache() => this._documentTypeSettings.Clear();

  private int GetDocsObjectTypeId(IUserSession session)
  {
    if (this.docsObjectTypeId == -1)
      this.docsObjectTypeId = session.IdentHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    return this.docsObjectTypeId;
  }

  private int GetConstrDocsObjectTypeId(IUserSession session)
  {
    if (this.constrDocsObjectTypeId == -1)
      this.constrDocsObjectTypeId = session.IdentHelper.GetObjectTypeID("cad0057f-306c-11d8-b4e9-00304f19f545");
    return this.constrDocsObjectTypeId;
  }

  private int GetDocObjTypeSettingsAttributeId(IUserSession session)
  {
    if (this.docObjTypeSettingsAttributeId == -1)
      this.docObjTypeSettingsAttributeId = session.IdentHelper.GetAttributeID("cad00626-306c-11d8-b4e9-00304f19f545");
    return this.docObjTypeSettingsAttributeId;
  }
}
