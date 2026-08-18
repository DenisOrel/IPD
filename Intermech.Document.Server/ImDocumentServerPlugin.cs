// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Server.ImDocumentServerPlugin
// Assembly: Intermech.Document.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F658B856-4DF9-439D-954C-249051C853FF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Document.Server.dll

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.GlobalIndex;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Document.Server;

[Serializable]
public class ImDocumentServerPlugin : DocumentEditorPluginBase, IPackage
{
  private static ImDocumentServerPlugin instance;
  private IServiceProvider serviceProvider;
  private IEventLogHelper eventLogHelper;
  [NonSerialized]
  private CombineAttributesHelper combineAttributesHelper;

  public static ImDocumentServerPlugin Instance
  {
    get
    {
      if (ImDocumentServerPlugin.instance == null)
        ImDocumentServerPlugin.instance = new ImDocumentServerPlugin();
      return ImDocumentServerPlugin.instance;
    }
  }

  public void Load(IServiceProvider serviceProvider)
  {
    serviceProvider.GetService<IPluginManager>().LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    LogManager.FileName = "ImDocBaseServer.log";
    if (serviceProvider.GetService(typeof (IGlobalIndexService)) is IGlobalIndexService service)
    {
      ImDocumentConverter converter = new ImDocumentConverter();
      service.RegisterFileConverter((IIndexerFileConverter) converter);
    }
    this.serviceProvider = serviceProvider;
    this.eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this.eventLogHelper.AfterCombineAttributesEvent += new CombineAttributesHandler(this.eventLogHelper_AfterCombineAttributesEvent);
    this.eventLogHelper.BeforeCombineAttributesEvent += new CombineAttributesHandler(this.eventLogHelper_BeforeCombineAttributesEvent);
    Type type1 = typeof (ReferenceToDBObjectCore);
    DocumentTreeNode.TypeNameDictionary[(object) type1.Name] = (object) type1;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToDBObjectBase.XmlTypeName] = (object) type1;
    int index1 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToDBObjectBase));
    if (index1 == -1)
      ReferenceBase.ReferenceClassList.Add(type1);
    else
      ReferenceBase.ReferenceClassList[index1] = type1;
    DocumentTreeNode.TypeConstructorDictionary[(object) type1.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectCore.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToDBObjectBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectCore.EmptyConstructor);
    Type type2 = typeof (ReferenceToDBObjectBase);
    DocumentTreeNode.TypeNameDictionary[(object) type2.Name] = (object) typeof (ReferenceToDBObjectCore);
    DocumentTreeNode.TypeConstructorDictionary[(object) type2.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectCore.EmptyConstructor);
    Type type3 = typeof (ReferenceToDBObjectAttributeCore);
    DocumentTreeNode.TypeNameDictionary[(object) type3.Name] = (object) type3;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToDBObjectAttributeBase.XmlTypeName] = (object) type3;
    int index2 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToDBObjectAttributeBase));
    if (index2 == -1)
      ReferenceBase.ReferenceClassList.Add(type3);
    else
      ReferenceBase.ReferenceClassList[index2] = type3;
    DocumentTreeNode.TypeConstructorDictionary[(object) type3.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeCore.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToDBObjectAttributeBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeCore.EmptyConstructor);
    Type type4 = typeof (ReferenceToDBObjectAttributeBase);
    DocumentTreeNode.TypeNameDictionary[(object) type4.Name] = (object) typeof (ReferenceToDBObjectAttributeCore);
    DocumentTreeNode.TypeConstructorDictionary[(object) type4.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeCore.EmptyConstructorActiveLink);
    Type type5 = typeof (ReferenceToSignCore);
    DocumentTreeNode.TypeNameDictionary[(object) type5.Name] = (object) type5;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) type5;
    int index3 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToSignBase));
    if (index3 == -1)
      ReferenceBase.ReferenceClassList.Add(type5);
    else
      ReferenceBase.ReferenceClassList[index3] = type5;
    DocumentTreeNode.TypeConstructorDictionary[(object) type5.Name] = (object) new EmptyConstructorDelegate(ReferenceToSignCore.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToSignCore.EmptyConstructor);
    Type type6 = typeof (ReferenceToSignBase);
    DocumentTreeNode.TypeNameDictionary[(object) type6.Name] = (object) typeof (ReferenceToSignCore);
    DocumentTreeNode.TypeConstructorDictionary[(object) type6.Name] = (object) new EmptyConstructorDelegate(ReferenceToSignCore.EmptyConstructorActiveLink);
    Type type7 = typeof (ReferenceToGraphicsCore);
    DocumentTreeNode.TypeNameDictionary[(object) type7.Name] = (object) type7;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToGraphicsBase.XmlTypeName] = (object) type7;
    int index4 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToGraphicsBase));
    if (index4 == -1)
      ReferenceBase.ReferenceClassList.Add(type7);
    else
      ReferenceBase.ReferenceClassList[index4] = type7;
    DocumentTreeNode.TypeConstructorDictionary[(object) type7.Name] = (object) new EmptyConstructorDelegate(ReferenceToGraphicsCore.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToGraphicsBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToGraphicsCore.EmptyConstructor);
    DocumentEditorPluginBase.ImDocumentLoader = new LoadDocumentFromDBObjectDelegate(ImDocumentServerPlugin.LoadDocumentFromDBObjectCore);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.ImDocumentConfig.LoadConfiguration(sessionKeeper.Session);
  }

  public ImDocumentConfigBase ImDocumentConfig
  {
    [DebuggerStepThrough] get => ImDocumentConfigBase.Instance;
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
  }

  public static void UpdateDocumentDBObject(
    IUserSession session,
    ImDocumentData document,
    long docObjID,
    bool updateDocumentLinks)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (!docObjID.IsDefinedId())
      return;
    IDBObject objectActual = session.GetObjectActual(docObjID, false);
    if (objectActual == null)
      return;
    ImDocumentServerPlugin.Instance.SetDocumentDBObject(document, objectActual);
    if (!updateDocumentLinks)
      return;
    ImDocumentServerPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) document, session, true, true, false, false, false);
  }

  public static ImDocumentData LoadDocumentFromDBObject(
    IUserSession session,
    long docObjectID,
    bool updateDoc,
    UpdateReferencesMode updateReferencesMode,
    int fileIndex = -1,
    bool failIfNotFound = false)
  {
    ImDocumentData imDocumentData = ImDocumentServerPlugin.LoadDocumentFromDBObjectCore(session.GetObject(docObjectID, true), fileIndex, failIfNotFound);
    if (imDocumentData != null & updateDoc)
    {
      if (updateReferencesMode.HasFlag((Enum) UpdateReferencesMode.Checksum))
        ImDocumentServerPlugin.Instance.UpdateCheckSum((IUserSession) null, imDocumentData, docObjectID, -1, fileIndex, false);
      ImDocumentServerPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) imDocumentData, session, true, true, false, updateReferencesMode, false, false);
    }
    return imDocumentData;
  }

  public static ImDocumentData LoadDocumentFromDBObjectCore(
    IDBObject docObject,
    int fileIndex = -1,
    bool failIfNotFound = false)
  {
    if (docObject == null)
      throw new ArgumentNullException(nameof (docObject));
    ImDocumentData imDocumentData = (ImDocumentData) null;
    IDBAttribute fileAttribute = (IDBAttribute) null;
    int aIndex = fileIndex;
    try
    {
      fileAttribute = DocumentEditorPluginBase.FindDocumentFileAttribute(docObject);
      if (fileAttribute != null)
      {
        if (aIndex == -1)
          aIndex = DocumentEditorPluginBase.FindImDocumentInAttribute(fileAttribute);
        if (aIndex != -1)
        {
          BlobReaderStream blobReaderStream = new BlobReaderStream(docObject.ObjectID, AttributableElements.Object, fileAttribute.AttributeID, aIndex, 0, docObject.Session);
          string fileName = blobReaderStream.BlobInformation.FileName;
          DateTime modifyDate = blobReaderStream.BlobInformation.ModifyDate;
          long realFileSize = blobReaderStream.BlobInformation.RealFileSize;
          if (realFileSize > 0L)
          {
            XmlReadArgs readArg = new XmlReadArgs()
            {
              FileName = fileName,
              FileSize = realFileSize,
              FileModifyDate = new DateTime?(modifyDate),
              IsTemplate = MetaDataHelper.IsObjectTypeChildOf(docObject.ObjectType, DocIDCache.ObjType_ImDocTemplate),
              IsFormulaLib = MetaDataHelper.IsObjectTypeChildOf(docObject.ObjectType, DocIDCache.ObjType_FormulaLib)
            };
            imDocumentData = ImDocumentData.LoadFromXml((Stream) blobReaderStream, readArg);
          }
          if (imDocumentData != null)
          {
            imDocumentData.FileName = fileName;
            imDocumentData.FileSize = new long?(realFileSize);
            imDocumentData.FileModifyDate = new DateTime?(modifyDate);
            imDocumentData.FileAttributeIndex = aIndex;
          }
        }
      }
    }
    catch (Exception ex)
    {
      throw new ImDocumentException($"При загрузке файла документа из атрибута {fileAttribute?.AttributeType.Name} [{aIndex}] " + $"объекта [{docObject.ObjectID}] '{docObject.Caption}' возникла ошибка:" + Environment.NewLine + ex.Message, ex);
    }
    return !(imDocumentData == null & failIfNotFound) ? imDocumentData : throw new ImDocumentException("Ошибка загрузки файла документа. \r\n" + $"В объекте [{docObject.ObjectID}] '{docObject.Caption}' документ интермех не найден!");
  }

  public void UpdateDocumentDBObject(
    IUserSession session,
    ImDocumentData document,
    long docObjID,
    bool updateDocumentLinks,
    bool updateLayout)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (!docObjID.IsDefinedId())
      return;
    IDBObject objectActual = session.GetObjectActual(docObjID, false);
    if (objectActual == null)
      return;
    this.SetDocumentDBObject(document, objectActual);
    if (!updateDocumentLinks)
      return;
    this.UpdateDocumentLinks((DocumentTreeNode) document, session, true, true, false, false, updateLayout);
  }

  public static void SaveImDocumentObjectFile(
    IUserSession session,
    long docObjectID,
    ImDocumentData document,
    string fileName,
    int fileIndex,
    bool isNewDocument)
  {
    DocumentEditorPluginBase.SaveImDocumentObjectFile(session.GetObject(docObjectID), document, fileName, fileIndex, isNewDocument);
  }

  public void Unload()
  {
    this.serviceProvider = (IServiceProvider) null;
    this.eventLogHelper.AfterCombineAttributesEvent -= new CombineAttributesHandler(this.eventLogHelper_AfterCombineAttributesEvent);
    this.eventLogHelper.BeforeCombineAttributesEvent -= new CombineAttributesHandler(this.eventLogHelper_BeforeCombineAttributesEvent);
    this.eventLogHelper = (IEventLogHelper) null;
    this.combineAttributesHelper = (CombineAttributesHelper) null;
  }

  public string Name => "Серверная часть Редактора документов";

  private void eventLogHelper_BeforeCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    this.combineAttributesHelper = new CombineAttributesHelper();
    this.combineAttributesHelper.BeforeCombineAttributesEvent(fromAttribute, toAttribute, session, combineMode);
  }

  private void eventLogHelper_AfterCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    this.combineAttributesHelper.AfterCombineAttributesEvent(fromAttribute, toAttribute, session, combineMode);
  }
}
