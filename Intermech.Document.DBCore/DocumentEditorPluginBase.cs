// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.DocumentEditorPluginBase
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Search.Interfaces.Signs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Document.DBCore;

public class DocumentEditorPluginBase
{
  public static LoadDocumentFromDBObjectDelegate ImDocumentLoader;

  public static IDBAttribute FindDocumentFileAttribute(IDBObject docObject, int attributeID)
  {
    return attributeID != -1 ? docObject.GetAttributeByID(attributeID) : DocumentEditorPluginBase.FindDocumentFileAttribute(docObject);
  }

  public static IDBAttribute FindDocumentFileAttribute(IDBObject docObject)
  {
    return docObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd9620-306c-11d8-b4e9-00304f19f545")) ?? docObject.GetAttributeByID(docObject.Session.IdentHelper.FileAttributeID);
  }

  public void UpdateCheckSum(
    IUserSession session,
    CheckSumService serv,
    ImDocumentData doc,
    Stream stream,
    bool inThread,
    bool closeThread = false)
  {
    this.UpdateCheckSum(session, serv, doc, (DocumentEditorPluginBase.CheckSumParams) null, stream, inThread, closeThread);
  }

  public void UpdateCheckSum(
    IUserSession session,
    ImDocumentData doc,
    long elementId,
    int attributeId,
    int fileindex,
    bool inThread)
  {
    CheckSumService serv = new CheckSumService();
    this.UpdateCheckSum(session, serv, doc, new DocumentEditorPluginBase.CheckSumParams()
    {
      AttributeId = attributeId,
      FileIndex = fileindex,
      ObjectID = elementId
    }, (Stream) null, inThread);
  }

  protected virtual bool CanUpdateChecksum(ImDocumentData doc) => true;

  private void UpdateCheckSum(
    IUserSession session,
    CheckSumService serv,
    ImDocumentData doc,
    DocumentEditorPluginBase.CheckSumParams tP,
    Stream stream,
    bool inThread,
    bool closeThread = false)
  {
    if (doc.ContainsAttribute(DocumentTreeNode.AttributeName_DocumentHasCheckSum))
    {
      bool result = true;
      if (bool.TryParse(doc.GetAttributeValue(DocumentTreeNode.AttributeName_DocumentHasCheckSum, true), out result) && !result)
        return;
    }
    if (!this.CanUpdateChecksum(doc))
      return;
    if (inThread)
    {
      BackgroundWorker backgroundWorker = new BackgroundWorker();
      backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DocumentEditorPluginBase.bw_RunWorkerCompleted);
      backgroundWorker.DoWork += new DoWorkEventHandler(DocumentEditorPluginBase.bw_DoWork);
      backgroundWorker.RunWorkerAsync((object) new object[6]
      {
        (object) serv,
        (object) doc,
        (object) tP,
        (object) stream,
        (object) closeThread,
        (object) session
      });
    }
    else
    {
      SessionKeeper sessionKeeper = (SessionKeeper) null;
      if (session == null)
      {
        sessionKeeper = new SessionKeeper();
        session = sessionKeeper.Session;
      }
      try
      {
        string attributeValue = "";
        if (tP != null)
          attributeValue = serv.GetChecksum(session, tP.ObjectID, AttributableElements.Object, tP.AttributeId, tP.FileIndex);
        else if (stream != null)
        {
          attributeValue = serv.GetChecksum(stream);
          if (closeThread)
            stream.Dispose();
        }
        doc.SetAttributeValue(DocumentTreeNode.AttributeName_CheckSum, attributeValue, false, false, false);
        doc.RefreshUI();
      }
      finally
      {
        sessionKeeper?.Dispose();
      }
    }
  }

  private static void bw_DoWork(object sender, DoWorkEventArgs e)
  {
    CheckSumService checkSumService = ((Array) e.Argument).GetValue(0) as CheckSumService;
    ImDocumentData imDocumentData = ((Array) e.Argument).GetValue(1) as ImDocumentData;
    DocumentEditorPluginBase.CheckSumParams checkSumParams = ((Array) e.Argument).GetValue(2) as DocumentEditorPluginBase.CheckSumParams;
    Stream stream = ((Array) e.Argument).GetValue(3) as Stream;
    bool flag = (bool) ((Array) e.Argument).GetValue(4);
    IUserSession userSession = (IUserSession) ((Array) e.Argument).GetValue(5);
    SessionKeeper sessionKeeper = (SessionKeeper) null;
    if (userSession == null)
    {
      sessionKeeper = new SessionKeeper();
      IUserSession session = sessionKeeper.Session;
    }
    try
    {
      string attributeValue = "";
      if (checkSumParams != null)
        attributeValue = checkSumService.GetChecksum(sessionKeeper.Session, checkSumParams.ObjectID, AttributableElements.Object, checkSumParams.AttributeId, checkSumParams.FileIndex);
      else if (stream != null)
      {
        attributeValue = checkSumService.GetChecksum(stream);
        if (flag)
          stream.Dispose();
      }
      imDocumentData.SetAttributeValue(DocumentTreeNode.AttributeName_CheckSum, attributeValue, false, false, false);
    }
    finally
    {
      sessionKeeper?.Dispose();
    }
    e.Result = (object) imDocumentData;
  }

  private static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    BackgroundWorker backgroundWorker = sender as BackgroundWorker;
    backgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(DocumentEditorPluginBase.bw_RunWorkerCompleted);
    backgroundWorker.DoWork -= new DoWorkEventHandler(DocumentEditorPluginBase.bw_DoWork);
    (e.Result as ImDocumentData).RefreshUI();
    try
    {
      backgroundWorker.Dispose();
    }
    catch
    {
    }
  }

  public void SetDocumentDBObject(
    ImDocumentData document,
    Guid objectGuid,
    long objectID,
    int objectType,
    string objectCaption,
    int fileAttributeID = -1,
    int fileAttributeIndex = -1)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (fileAttributeIndex != -1)
      document.FileAttributeIndex = fileAttributeIndex;
    if (fileAttributeID != -1)
      document.FileAttributeID = fileAttributeID;
    if (!(document.Reference is ReferenceToDBObjectBase reference1))
      document.Reference = (ReferenceBase) this.CreateSetDocumentDBObjectReference(document, new DBObjectInfo(objectGuid, objectID, objectType, objectCaption));
    else if (reference1.ReferenceType == RefToDBObjectType.rtSelectedObject)
    {
      reference1.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(objectGuid, objectID, objectType, objectCaption), true);
      reference1.PassiveLink = false;
    }
    else
      document.Reference = (ReferenceBase) this.CreateSetDocumentDBObjectReference(document, new DBObjectInfo(objectGuid, objectID, objectType, objectCaption));
    if (document.DocumentTemplate != null)
    {
      if (!(document.DocumentTemplate.Reference is ReferenceToDBObjectBase reference2))
        document.DocumentTemplate.Reference = document.Reference.Clone();
      else if (reference2.ReferenceType == RefToDBObjectType.rtSelectedObject)
        reference2.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(objectGuid, objectID, objectType, objectCaption), true);
      else
        document.DocumentTemplate.Reference = (ReferenceBase) this.CreateSetDocumentDBObjectReference(document, new DBObjectInfo(objectGuid, objectID, objectType, objectCaption));
    }
    this.SetOriginalTemplateGuid(document, objectGuid);
  }

  protected virtual ReferenceToDBObjectCore CreateSetDocumentDBObjectReference(
    ImDocumentData document,
    DBObjectInfo info)
  {
    return new ReferenceToDBObjectCore((DocumentTreeNode) document, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) info, false);
  }

  private void SetOriginalTemplateGuid(ImDocumentData document, Guid objectGuid)
  {
    if (!document.IsTemplate || document.TemplateOwner != null)
      return;
    if (objectGuid != Guid.Empty)
      document.SetAttributeValue(ImDocumentData.AttributeOriginalTemplateGuid, objectGuid.ToString(), false, false, false);
    else
      document.RemoveAttribute(ImDocumentData.AttributeOriginalTemplateGuid, false, false);
  }

  public void SetDocumentDBObject(ImDocumentData document, IDBObject dbObject)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    if (!(document.Reference is ReferenceToDBObjectBase reference1))
      document.Reference = (ReferenceBase) this.CreateSetDocumentDBObjectReference(document, new DBObjectInfo(dbObject.ObjectGUID, dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption));
    else if (reference1.ReferenceType == RefToDBObjectType.rtSelectedObject)
      reference1.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(dbObject.ObjectGUID, dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption), true);
    else
      document.Reference = (ReferenceBase) this.CreateSetDocumentDBObjectReference(document, new DBObjectInfo(dbObject.ObjectGUID, dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption));
    string attributeValue1 = "";
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(DocIDCache.Attr_Designation);
    if (attributeById1 != null)
      attributeValue1 = attributeById1.Description;
    document.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, attributeValue1);
    string attributeValue2 = "";
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(DocIDCache.Attr_Name);
    if (attributeById2 != null)
      attributeValue2 = attributeById2.Description;
    document.SetAttributeValue(DocumentTreeNode.AttributeName_DocName, attributeValue2);
    document.SetAttributeValue(DocumentTreeNode.AttributeName_VersionId, Math.Abs(dbObject.ObjectID).ToString());
    if (document.DocumentTemplate != null)
    {
      if (!(document.DocumentTemplate.Reference is ReferenceToDBObjectBase reference2))
        document.DocumentTemplate.Reference = document.Reference.Clone();
      else if (reference2.ReferenceType == RefToDBObjectType.rtSelectedObject)
        reference2.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(dbObject.ObjectGUID, dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption), true);
      else
        document.DocumentTemplate.Reference = (ReferenceBase) this.CreateSetDocumentDBObjectReference(document, new DBObjectInfo(dbObject.ObjectGUID, dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption));
    }
    this.SetOriginalTemplateGuid(document, dbObject.ObjectGUID);
  }

  public static IDBAttribute FindFileAttributeForSaveImDocument(
    IDBObject docObject,
    out int attrFileId)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd9620-306c-11d8-b4e9-00304f19f545");
    IDBAttribute attributeById = docObject.GetAttributeByID(attributeTypeId);
    if (attributeById != null || DocumentEditorPluginBase.DBObjectIsScanDocument(docObject))
    {
      attrFileId = attributeTypeId;
    }
    else
    {
      attributeById = docObject.GetAttributeByID(docObject.Session.IdentHelper.FileAttributeID);
      attrFileId = docObject.Session.IdentHelper.FileAttributeID;
    }
    return attributeById;
  }

  public static bool DBObjectIsScanDocument(IDBObject docObject)
  {
    IDBAttribute attributeById = docObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd9644-306c-11d8-b4e9-00304f19f545"));
    return attributeById != null && attributeById.AsBoolean;
  }

  public static int FindAnyImDocumentInAttribute(IDBAttribute fileAttribute)
  {
    int extensionInAttribute = DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(fileAttribute, (IList<string>) ImDocumentData.ImDocumentFileExtensions);
    if (extensionInAttribute == -1)
      extensionInAttribute = DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(fileAttribute, (IList<string>) ImDocumentData.OldImDocumentExtensions);
    if (extensionInAttribute == -1)
      extensionInAttribute = DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(fileAttribute, (IList<string>) ImDocumentData.OldBlankExtensions);
    return extensionInAttribute;
  }

  public static int FindImDocumentInAttribute(IDBAttribute fileAttribute)
  {
    return DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(fileAttribute, (IList<string>) ImDocumentData.ImDocumentFileExtensions);
  }

  public static int FindExternalImDocumentInAttribute(IDBAttribute fileAttribute)
  {
    return DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(fileAttribute, (IList<string>) ImDocumentData.ImDocumentExternalFileExtensions);
  }

  public static int FindFirstFileExtensionInAttribute(
    IDBAttribute fileAttribute,
    IList<string> fileExtensions)
  {
    for (int extensionInAttribute = 0; extensionInAttribute < fileAttribute.Values.Length; ++extensionInAttribute)
    {
      string lower = ImDocumentData.GetFileExtensionWithoutDot(fileAttribute.Descriptions[extensionInAttribute]).ToLower();
      if (fileExtensions.Contains(lower))
        return extensionInAttribute;
    }
    return -1;
  }

  private static int GetOrAddAttrFileValueForSaveFile(IDBAttribute attrFile)
  {
    int valueForSaveFile = DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(attrFile, (IList<string>) ImDocumentData.ImDocumentFileExtensions);
    if (valueForSaveFile == -1 && attrFile.ValuesCount > 0)
    {
      attrFile.Index = 0;
      if (attrFile.IsNull)
        valueForSaveFile = attrFile.Index;
    }
    if (valueForSaveFile == -1)
      valueForSaveFile = attrFile.AddValue((object) null);
    return valueForSaveFile;
  }

  public static IDBAttribute GetOrAddAttrFileForSaveImDocument(
    IDBObject docObject,
    int fileAttributeId,
    ref int fileIndex)
  {
    IDBAttribute attrFile = fileAttributeId != -1 ? docObject.GetAttributeByID(fileAttributeId) : DocumentEditorPluginBase.FindFileAttributeForSaveImDocument(docObject, out fileAttributeId);
    if (attrFile == null)
    {
      attrFile = docObject.Attributes.AddAttribute(fileAttributeId, false);
      fileIndex = 0;
    }
    if (fileIndex == -1)
      fileIndex = DocumentEditorPluginBase.GetOrAddAttrFileValueForSaveFile(attrFile);
    return attrFile;
  }

  public static string CheckAndCorrectFileName(
    string fileName,
    long object_FID,
    IUserSession session)
  {
    string filename = fileName;
    if (!string.IsNullOrEmpty(filename) && filename.Length > 249)
      filename = filename.Substring(0, 200);
    string fileName1 = FileNameHelper.ReplaceInvalidFileNameChars(filename);
    if (!string.IsNullOrEmpty(fileName1))
    {
      string extensionWithoutDot = ImDocumentData.GetFileExtensionWithoutDot(fileName1);
      if (!ImDocumentData.IsImDocumentExtension(extensionWithoutDot))
        fileName1 += ".imdx";
      else if (extensionWithoutDot == fileName1)
        fileName1 = "document" + extensionWithoutDot;
    }
    else
      fileName1 = "document.imdx";
    if (!(session.GetCustomService(typeof (IFileNamesService)) is IFileNamesService customService))
      throw new Exception("Сервис IFileNamesService не найден");
    return customService.GetUniqueFileName(fileName1, object_FID, session.SessionGUID);
  }

  public static ImDocumentData LoadDocumentFromDBObject(
    IDBObject docObject,
    int fileIndex = -1,
    bool failIfNotFound = false)
  {
    LoadDocumentFromDBObjectDelegate imDocumentLoader = DocumentEditorPluginBase.ImDocumentLoader;
    return imDocumentLoader == null ? (ImDocumentData) null : imDocumentLoader(docObject, fileIndex, failIfNotFound);
  }

  public static ImDocumentData LoadDocumentFromDBObject(
    IUserSession session,
    long docObjectID,
    int fileIndex = -1,
    bool failIfNotFound = false)
  {
    return DocumentEditorPluginBase.LoadDocumentFromDBObject(session.GetObject(docObjectID, true), fileIndex, failIfNotFound);
  }

  public static void SaveImDocumentObjectFile(
    IDBObject docObject,
    ImDocumentData document,
    string fileName,
    int fileIndex = -1,
    bool isNewDocument = false)
  {
    object initValue = (object) null;
    if (!isNewDocument && document.SaveModificationDate)
    {
      IDBAttribute attributeById = docObject.GetAttributeByID(DocIDCache.Attr_ContentModifyDate);
      if (attributeById != null)
        initValue = attributeById.Value;
    }
    fileName = DocumentEditorPluginBase.CheckAndCorrectFileName(fileName, docObject.ID, docObject.Session);
    if (fileIndex == -1)
      fileIndex = document.FileAttributeIndex;
    IDBAttribute forSaveImDocument = DocumentEditorPluginBase.GetOrAddAttrFileForSaveImDocument(docObject, document.FileAttributeID, ref fileIndex);
    forSaveImDocument.Index = fileIndex;
    DateTime now = DateTime.Now;
    BlobInformation info = new BlobInformation(0L, 0L, now, fileName, ArcMethods.ZLibPacked, string.Empty);
    using (BlobWriterStream blobWriterStream = new BlobWriterStream(forSaveImDocument, 0, info, forSaveImDocument.Session))
    {
      document.SaveToXml((Stream) blobWriterStream);
      blobWriterStream.Commit();
      document.FileSize = new long?(blobWriterStream.Length);
    }
    document.FileAttributeIndex = fileIndex;
    document.FileAttributeID = forSaveImDocument.AttributeID;
    document.FileName = fileName;
    document.FileModifyDate = new DateTime?(now);
    document.SavedDateTime = new DateTime?(new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0));
    List<AttributeValues> list = new List<AttributeValues>();
    if (AttributeCacheHelper.IsEnabledObjectTypeAttribute(DocIDCache.Attr_CheckSum, docObject.ObjectType))
      list.Add(new AttributeValues(DocIDCache.Attr_CheckSum, (object) 0));
    if (AttributeCacheHelper.IsEnabledObjectTypeAttribute(DocIDCache.Attr_Pages, docObject.ObjectType))
    {
      if (document.NeedUpdateLayoutFlag)
        list.Add(new AttributeValues(DocIDCache.Attr_Pages, (object) null));
      else
        list.Add(new AttributeValues(DocIDCache.Attr_Pages, (object) document.PageCount));
    }
    if (MetaDataHelper.IsObjectTypeChildOf(docObject.TypeID, DocIDCache.ObjType_Specification))
      list.Add(new AttributeValues(DocIDCache.Attr_NeedUpdateDoc, (object) false));
    if (!isNewDocument && document.SaveModificationDate)
      list.Add(new AttributeValues(DocIDCache.Attr_ContentModifyDate, initValue));
    if (list.IsEmpty<AttributeValues>())
      return;
    docObject.SetAttributesValues(list.ToArray());
  }

  public static ImDocumentData CreateDocumentFromTemplate(IUserSession session, long templateID)
  {
    return ImDocumentData.CreateDocumentFromTemplate(DocumentEditorPluginBase.LoadDocumentFromDBObject(session, templateID));
  }

  public static ImDocumentData CreateDocumentFromTemplate(IUserSession session, Guid templateGuid)
  {
    return ImDocumentData.CreateDocumentFromTemplate(DocumentEditorPluginBase.LoadDocumentFromDBObject(session.GetObject(templateGuid)));
  }

  public void UpdateDocumentLinks(
    DocumentTreeNode documentOrComplect,
    bool updateInTemplate,
    bool updateDBLink,
    bool updateDocLink,
    bool updateUI,
    bool updateLayout)
  {
    if (documentOrComplect == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateDocumentLinks(documentOrComplect, sessionKeeper.Session, updateInTemplate, updateDBLink, updateDocLink, updateUI, updateLayout);
  }

  public void UpdateDocumentLinks(
    DocumentTreeNode documentOrComplect,
    IUserSession session,
    bool updateInTemplate,
    bool updateDBLink,
    bool updateDocLink,
    bool updateUI,
    bool updateLayout)
  {
    this.UpdateDocumentLinks(documentOrComplect, session, updateInTemplate, updateDBLink, updateDocLink, UpdateReferencesMode.All, updateUI, updateLayout);
  }

  public void UpdateDocumentLinks(
    DocumentTreeNode documentOrComplect,
    IUserSession session,
    bool updateInTemplate,
    bool updateDBLink,
    bool updateDocLink,
    UpdateReferencesMode updateReferencesMode,
    bool updateUI,
    bool updateLayout)
  {
    if (documentOrComplect == null)
      return;
    DocumentsComplect documentsComplect1 = documentOrComplect as DocumentsComplect;
    ImDocumentData imDocumentData = documentOrComplect as ImDocumentData;
    if (documentsComplect1 == null && imDocumentData == null)
      return;
    if (LogManager.CreateLog)
    {
      LogManager.AddLine($"DocumentEditorPlugin.UpdateDocumentLinks(documentOrComplect:{documentOrComplect}, sessionKeeper, " + $"updateInTemplate:{updateInTemplate}, updateDBLink:{updateDBLink}, updateDocLink:{updateDocLink}, " + $"updateUI:{updateDocLink}, updateLayout:{updateLayout}) -START");
      if (imDocumentData != null)
        LogManager.AddLine($"[DocId: {imDocumentData.DBObjectID}]");
      if (documentsComplect1 != null)
        LogManager.AddLine($"[Complect: {documentsComplect1}]");
    }
    SessionKeeper sessionKeeper = (SessionKeeper) null;
    try
    {
      DocumentsComplect documentsComplect2 = imDocumentData == null ? documentsComplect1.GetRootDocumentsComplect() : imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect2 != null)
      {
        documentsComplect2.UpdatePageNumbers((ImDocumentData) null, 1, false, false);
      }
      else
      {
        string attributeValue = (string) null;
        if (imDocumentData != null && imDocumentData.DocumentComplectObjectGuid != Guid.Empty)
        {
          if (session == null)
          {
            sessionKeeper = new SessionKeeper();
            session = sessionKeeper.Session;
          }
          IDBObject dbObject = session.GetObject(imDocumentData.DocumentComplectObjectGuid, false);
          if (dbObject != null)
          {
            IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(DocIDCache.ComplectPageCount_Guid);
            if (attributeByGuid1 != null)
              attributeValue = attributeByGuid1.AsString;
            if (string.IsNullOrEmpty(attributeValue))
            {
              IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(DocIDCache.DocumentPageCount_Guid);
              if (attributeByGuid2 != null)
                attributeValue = attributeByGuid2.AsString;
            }
            if (!string.IsNullOrEmpty(attributeValue))
              imDocumentData.SetAttributeValue(ImDocumentData.AttributePagesComplectCount, attributeValue);
          }
        }
        if (imDocumentData != null && imDocumentData.DBObjectID.IsDefinedId() && session != null)
        {
          IDBObject objectActual = session.GetObjectActual(imDocumentData.DBObjectID, false);
          if (objectActual != null)
          {
            IDBAttribute attributeByGuid3 = objectActual.GetAttributeByGuid(DocIDCache.FirstPageNumberInDocumentComplect_Guid);
            if (attributeByGuid3 != null && attributeByGuid3.Value != null && !(attributeByGuid3.Value is DBNull))
              imDocumentData.AssignStartComplectPageNumber((int) attributeByGuid3.ConvertToInt64());
            if (string.IsNullOrEmpty(attributeValue))
            {
              IDBAttribute attributeByGuid4 = objectActual.GetAttributeByGuid(DocIDCache.ComplectPageCount_Guid);
              if (attributeByGuid4 != null)
                attributeValue = attributeByGuid4.AsString;
              if (!string.IsNullOrEmpty(attributeValue))
                imDocumentData.SetAttributeValue(ImDocumentData.AttributePagesComplectCount, attributeValue);
            }
          }
        }
        imDocumentData?.UpdatePageNumbers((PageData) null, imDocumentData.StartComplectPageNumber, true, false, false);
      }
      this.UpdateDocumentTreeLinks(documentOrComplect, session, (Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>) null, (Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>) null, updateInTemplate, updateDBLink, updateDocLink, updateReferencesMode, updateUI, updateLayout);
    }
    finally
    {
      sessionKeeper?.Dispose();
    }
    LogManager.AddLine("DocumentEditorPlugin.UpdateDocumentLinks() -END");
  }

  protected virtual void CheckAttributeProcessorDictionary(ImDocumentData document, bool resetCache)
  {
  }

  protected void UpdateDocumentTreeLinks(
    DocumentTreeNode parentNode,
    IUserSession session,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool updateInTemplate,
    bool updateDBLink,
    bool updateDocLink,
    UpdateReferencesMode updateReferencesMode,
    bool updateUI,
    bool updateLayout,
    bool resetCache = true)
  {
    ImDocumentData document = parentNode != null ? parentNode as ImDocumentData : throw new ArgumentNullException(nameof (parentNode));
    List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
    if (document != null)
      imDocumentDataList.Add(document);
    if (parentNode is DocumentsComplect documentParent)
    {
      document = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentParent);
      imDocumentDataList = documentParent.GetAllDocuments();
    }
    if (document == null)
      return;
    List<ReferenceToDBObjectBase> referenceToDbObjectBaseList = new List<ReferenceToDBObjectBase>();
    List<ReferenceToDBObjectBase> source1 = new List<ReferenceToDBObjectBase>();
    List<ReferenceToDBObjectAttributeCore> objectAttributeCoreList = new List<ReferenceToDBObjectAttributeCore>();
    List<Guid> guidList = new List<Guid>();
    Dictionary<Guid, DBObjectInfo> dictionary1 = (Dictionary<Guid, DBObjectInfo>) null;
    Dictionary<long, DBObjectInfo> dictionary2 = (Dictionary<long, DBObjectInfo>) null;
    if (document != null)
    {
      lock (document.Signes)
        document.Signes.Clear();
      document.ObjectsInfoGuid.Clear();
      dictionary1 = document.ObjectsInfoGuid;
      document.ObjectsInfoId.Clear();
      dictionary2 = document.ObjectsInfoId;
    }
    if (objAttrCache == null && document != null)
    {
      if (resetCache)
      {
        lock (document.ObjAttrCache)
          document.ObjAttrCache.Clear();
      }
      objAttrCache = document.ObjAttrCache;
      this.CheckAttributeProcessorDictionary(document, resetCache);
    }
    if (objAttrCache == null)
      objAttrCache = new Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>();
    if (relAttrCache == null && document != null)
    {
      if (resetCache)
      {
        lock (document.RelAttrCache)
          document.RelAttrCache.Clear();
      }
      relAttrCache = document.RelAttrCache;
    }
    if (relAttrCache == null && document != null)
      relAttrCache = new Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>();
    foreach (ImDocumentData node in imDocumentDataList)
    {
      node.UpdateReferencesMode = updateReferencesMode;
      ReferenceToDBObjectBase reference1 = node.Reference as ReferenceToDBObjectBase;
      if (updateDBLink && reference1 != null)
        reference1.UpdateLink((object) session, objAttrCache, relAttrCache, false, updateUI, updateLayout);
      else if (updateDocLink && node.Reference != null)
        node.Reference.UpdateLink(updateUI, updateLayout);
      List<DocumentTreeNode> childNodes = DocumentTreeNode.GetChildNodes((DocumentTreeNode) node);
      if (updateInTemplate && node.DocumentTemplate != null)
        childNodes.AddRange((IEnumerable<DocumentTreeNode>) DocumentTreeNode.GetChildNodes((DocumentTreeNode) node.DocumentTemplate));
      foreach (DocumentTreeNode documentTreeNode in childNodes)
      {
        if (documentTreeNode is INodeWithReference nodeWithReference)
        {
          ReferenceToDBObjectBase reference2 = nodeWithReference.Reference as ReferenceToDBObjectBase;
          if (updateDBLink && reference2 != null && session != null)
          {
            if (reference2.CanUpdateReference(node.UpdateReferencesMode))
            {
              referenceToDbObjectBaseList.Add(reference2);
              if (reference2.IsUpdateDBObjectInfoBatch)
              {
                if (reference2.IsReferenceFromDocumentAttribute && !guidList.Contains(reference2.LinkAttributeGuid))
                  guidList.Add(reference2.LinkAttributeGuid);
                source1.Add(reference2);
                if (reference2 is ReferenceToDBObjectAttributeCore objectAttributeCore)
                  objectAttributeCoreList.Add(objectAttributeCore);
              }
            }
          }
          else if (updateDocLink && nodeWithReference.Reference != null && reference2 == null)
            nodeWithReference.Reference.UpdateLink(updateUI, updateLayout);
        }
      }
    }
    if (source1.Count > 0)
    {
      lock (objAttrCache)
      {
        List<DBObjectInfoBase> source2 = new List<DBObjectInfoBase>();
        foreach (ImDocumentData imDocumentData in imDocumentDataList)
        {
          if (imDocumentData.DBObjectInfo != null)
            source2.Add(imDocumentData.DBObjectInfo);
        }
        foreach (IGrouping<int, DBObjectInfoBase> source3 in source2.GroupBy<DBObjectInfoBase, int>((System.Func<DBObjectInfoBase, int>) (x => x.ObjectType)))
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(source3.Key);
          long[] array = source3.Select<DBObjectInfoBase, long>((System.Func<DBObjectInfoBase, long>) (x => x.ObjectID)).ToArray<long>();
          foreach (Guid guid in guidList)
          {
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeId);
            if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
            {
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(-2, RelationalOperators.In, (object) ((IEnumerable<long>) array).ToArray<long>(), LogicalOperators.NONE, 0, true)
              }, new ColumnDescriptor[6]
              {
                new ColumnDescriptor((object) -2, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) -12, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) -7, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) attributeTypeId, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) attributeTypeId, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
                new ColumnDescriptor((object) -50, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
              });
              foreach (DataRow row in (InternalDataCollectionBase) objectCollection.SelectWithLocalObjects(paramSet).Rows)
              {
                Guid key = new Guid(Convert.ToString(row[-12.ToString()]));
                Convert.ToInt64(row[-2.ToString()]);
                Convert.ToInt32(row[-7.ToString()]);
                Convert.ToString(row[-50.ToString()]);
                object obj1 = row[attributeTypeId.ToString()];
                if (obj1 == DBNull.Value)
                  obj1 = (object) null;
                object obj2 = row[guid.ToString()];
                if (obj2 == DBNull.Value)
                  obj2 = (object) null;
                if (!objAttrCache.ContainsKey(key))
                  objAttrCache[key] = new Dictionary<Guid, AttributeValueCache>();
                try
                {
                  long int64 = obj1 != null ? Convert.ToInt64(obj1) : 0L;
                  if (int64.IsDefinedId())
                  {
                    if (!dictionary2.ContainsKey(-int64))
                      dictionary2[-int64] = (DBObjectInfo) null;
                    if (!dictionary2.ContainsKey(int64))
                      dictionary2[int64] = (DBObjectInfo) null;
                    if (!objAttrCache[key].ContainsKey(guid))
                      objAttrCache[key][guid] = new AttributeValueCache()
                      {
                        Id = new long?(int64),
                        Value = obj2
                      };
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
      }
      if (dictionary2.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(-1).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) dictionary2.Keys.ToArray<long>(), LogicalOperators.NONE, 0, true)
        }, new ColumnDescriptor[4]
        {
          new ColumnDescriptor((object) -2, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0),
          new ColumnDescriptor((object) -12, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -50, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
        })).Rows)
        {
          Guid guid;
          ref Guid local = ref guid;
          DataRow dataRow1 = row;
          int num = -12;
          string columnName1 = num.ToString();
          string g = Convert.ToString(dataRow1[columnName1]);
          local = new Guid(g);
          DataRow dataRow2 = row;
          num = -2;
          string columnName2 = num.ToString();
          long int64 = Convert.ToInt64(dataRow2[columnName2]);
          DataRow dataRow3 = row;
          num = -7;
          string columnName3 = num.ToString();
          int int32 = Convert.ToInt32(dataRow3[columnName3]);
          DataRow dataRow4 = row;
          num = -50;
          string columnName4 = num.ToString();
          string objectCaption = Convert.ToString(dataRow4[columnName4]);
          if (!dictionary2.ContainsKey(int64) || dictionary2[int64] == null)
          {
            DBObjectInfo dbObjectInfo = new DBObjectInfo(guid, int64, int32, objectCaption);
            dictionary1[guid] = dbObjectInfo;
            dictionary2[int64] = dbObjectInfo;
          }
          if (int64 < 0L)
          {
            DBObjectInfo dbObjectInfo = new DBObjectInfo(guid, int64, int32, objectCaption);
            dictionary1[guid] = dbObjectInfo;
            dictionary2[-int64] = dbObjectInfo;
          }
        }
      }
      Dictionary<Guid, DBObjectInfoBase> dictionary3 = new Dictionary<Guid, DBObjectInfoBase>();
      foreach (ReferenceToDBObjectBase referenceToDbObjectBase in source1)
      {
        if (referenceToDbObjectBase.DBObjectGuid != Guid.Empty && !referenceToDbObjectBase.IsConnected && !dictionary1.ContainsKey(referenceToDbObjectBase.DBObjectGuid) && !dictionary3.ContainsKey(referenceToDbObjectBase.DBObjectGuid))
          dictionary3[referenceToDbObjectBase.DBObjectGuid] = (DBObjectInfoBase) null;
      }
      if (dictionary3.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(-1).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-12, RelationalOperators.In, (object) dictionary3.Keys.ToArray<Guid>(), LogicalOperators.NONE, 0, true)
        }, new ColumnDescriptor[4]
        {
          new ColumnDescriptor((object) -2, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -12, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -50, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
        })).Rows)
        {
          Guid guid;
          ref Guid local = ref guid;
          DataRow dataRow5 = row;
          int num = -12;
          string columnName5 = num.ToString();
          string g = Convert.ToString(dataRow5[columnName5]);
          local = new Guid(g);
          DataRow dataRow6 = row;
          num = -2;
          string columnName6 = num.ToString();
          long int64 = Convert.ToInt64(dataRow6[columnName6]);
          DataRow dataRow7 = row;
          num = -7;
          string columnName7 = num.ToString();
          int int32 = Convert.ToInt32(dataRow7[columnName7]);
          DataRow dataRow8 = row;
          num = -50;
          string columnName8 = num.ToString();
          string objectCaption = Convert.ToString(dataRow8[columnName8]);
          DBObjectInfo dbObjectInfo = new DBObjectInfo(guid, int64, int32, objectCaption);
          dictionary1[guid] = dbObjectInfo;
          dictionary2[int64] = dbObjectInfo;
        }
      }
    }
    foreach (ReferenceToDBObjectBase referenceToDbObjectBase in source1.Where<ReferenceToDBObjectBase>((System.Func<ReferenceToDBObjectBase, bool>) (r => !r.PassiveLink)))
      referenceToDbObjectBase.UpdateDBObjectInfo((object) session, (string) null);
    Dictionary<Guid, List<DBObjectInfoBase>> dictionary4 = new Dictionary<Guid, List<DBObjectInfoBase>>();
    foreach (ReferenceToDBObjectAttributeCore objectAttributeCore in objectAttributeCoreList)
    {
      if (objectAttributeCore.DBObjectID != -1L)
      {
        Guid attributeGuid = objectAttributeCore.AttributeGuid;
        if (!dictionary4.ContainsKey(attributeGuid))
          dictionary4[attributeGuid] = new List<DBObjectInfoBase>();
        dictionary4[attributeGuid].Add(objectAttributeCore.DBObjectInfo);
      }
    }
    lock (objAttrCache)
    {
      foreach (KeyValuePair<Guid, List<DBObjectInfoBase>> keyValuePair in dictionary4)
      {
        Guid key1 = keyValuePair.Key;
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(key1);
        if (attributeTypeId >= 0)
        {
          MetaDataHelper.GetAttributeTypeName(key1);
          foreach (IGrouping<int, DBObjectInfoBase> source4 in keyValuePair.Value.GroupBy<DBObjectInfoBase, int>((System.Func<DBObjectInfoBase, int>) (x => x.ObjectType)))
          {
            foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(source4.Key).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(-2, RelationalOperators.In, (object) ((IEnumerable<long>) source4.Select<DBObjectInfoBase, long>((System.Func<DBObjectInfoBase, long>) (x => x.ObjectID)).ToArray<long>()).ToArray<long>(), LogicalOperators.NONE, 0, true)
            }, new ColumnDescriptor[3]
            {
              new ColumnDescriptor((object) -2, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
              new ColumnDescriptor((object) -12, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
              new ColumnDescriptor((object) attributeTypeId, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
            })).Rows)
            {
              Guid key2 = new Guid(Convert.ToString(row[-12.ToString()]));
              Convert.ToInt64(row[-2.ToString()]);
              object obj = row[attributeTypeId.ToString()];
              if (obj == DBNull.Value)
                obj = (object) null;
              if (!objAttrCache.ContainsKey(key2))
                objAttrCache[key2] = new Dictionary<Guid, AttributeValueCache>();
              if (!objAttrCache[key2].ContainsKey(key1))
                objAttrCache[key2][key1] = new AttributeValueCache()
                {
                  Value = obj
                };
            }
          }
        }
      }
    }
    foreach (ReferenceToDBObjectBase referenceToDbObjectBase in referenceToDbObjectBaseList)
    {
      if (referenceToDbObjectBase.OwnerDocument == null || referenceToDbObjectBase.CanUpdateReference(referenceToDbObjectBase.OwnerDocument.UpdateReferencesMode))
        referenceToDbObjectBase.UpdateLink((object) session, objAttrCache, relAttrCache, false, updateUI, updateLayout);
    }
  }

  public class CheckSumParams
  {
    public long ObjectID;
    public int FileIndex;
    public int AttributeId;
  }
}
