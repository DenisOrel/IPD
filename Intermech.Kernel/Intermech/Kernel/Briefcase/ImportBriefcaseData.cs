// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportBriefcaseData
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportBriefcaseData : ImportBriefcaseBase
{
  private ImportingEntityCustomCheckService _checkerService;

  public ImportBriefcaseData(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent,
    Guid briefcase,
    string briefcasePath)
    : base(session, eventLog, setImportProgressEvent, briefcase, briefcasePath)
  {
    this.briefcase = briefcase;
    this.briefcasePath = briefcasePath;
    this._checkerService = (ImportingEntityCustomCheckService) ServerServices.GetService(typeof (IImportingEntityCustomCheckService));
  }

  public bool Import(
    List<FoundObjectInfo> findObjectsToObject,
    List<IDСorresponds> importingObjectIDs,
    bool langEquals,
    int briefcaseIndex,
    BriefcaseImportProperties importProperties,
    int itemsCount)
  {
    BriefcaseImportProgress briefcaseImportProgress = new BriefcaseImportProgress(OperationType.Importing);
    List<long> needRefreshFolderKeyObjects = new List<long>();
    List<Tuple<Guid, Guid>> notImportedObjects = new List<Tuple<Guid, Guid>>();
    try
    {
      this.SetImportProgress(this.briefcase, briefcaseImportProgress);
      DataSet[] metadata = BriefcaseProcs.ReadMetaDataXML(this.briefcasePath);
      int i = 0;
      ArrayList objectLinks = new ArrayList();
      bool throwException = !importProperties.ObjectsOnly || !importProperties.IgnoreErrors;
      if (!this.ReadObjects(metadata, briefcaseImportProgress, importProperties, objectLinks, needRefreshFolderKeyObjects, findObjectsToObject, importingObjectIDs, notImportedObjects, langEquals, briefcaseIndex, throwException, itemsCount, ref i) || !this.ReadEditingContext(importingObjectIDs, briefcaseImportProgress) || !this.ReadRelations(metadata, briefcaseImportProgress, importProperties, objectLinks, notImportedObjects, langEquals, throwException, itemsCount, ref i))
        return false;
      ImportObjectLinks importObjectLinks = new ImportObjectLinks(this.session, objectLinks, importingObjectIDs);
      importObjectLinks.Import();
      if (needRefreshFolderKeyObjects.Count > 0)
        DBClassifier.RebuildKeys((IUserSession) this.session, needRefreshFolderKeyObjects.ToArray(), this.eventLog.LogFileName, false);
      foreach (string eventString in importObjectLinks.Log)
        this.eventLog.AddToTrace(eventString);
      return true;
    }
    catch (Exception ex)
    {
      briefcaseImportProgress.ErrorException = ex;
      briefcaseImportProgress.Operation = OperationType.Error;
      this.SetImportProgress(this.briefcase, briefcaseImportProgress);
      this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logImportRelationUnknError, (object) ex.Message));
      return false;
    }
    finally
    {
      if (importingObjectIDs.Count > 0)
      {
        List<long> processedObjects = new List<long>(importingObjectIDs.Count);
        foreach (IDСorresponds importingObjectId in importingObjectIDs)
        {
          if (!processedObjects.Contains(importingObjectId.HostObjectID))
            processedObjects.Add(importingObjectId.HostObjectID);
        }
        ((ICustomImport) ServerServices.GetService(typeof (ICustomImport)))?.FireAfterImportObjects((object) this, new AfterCustomImportEventArgs((IUserSession) this.session, processedObjects, briefcaseImportProgress.ErrorException));
      }
      notImportedObjects.Clear();
    }
  }

  private bool ReadEditingContext(
    List<IDСorresponds> importingObjectIDs,
    BriefcaseImportProgress bip)
  {
    IDBEditingContextsServerService service = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
    string str1 = Path.Combine(this.briefcasePath, "Contexts.xml");
    if (!File.Exists(str1))
      return true;
    XmlTextReader xmlTextReader = new XmlTextReader(str1);
    try
    {
      while (xmlTextReader.Read())
      {
        if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == BriefcaseConsts.XmlContextsRecordTag)
        {
          long id = Convert.ToInt64(xmlTextReader.GetAttribute(BriefcaseConsts.XmlContextIDAttributeName));
          Convert.ToInt64(xmlTextReader.GetAttribute(BriefcaseConsts.XmlContextModificationIDAttributeName));
          long linkedContextNumber = 0;
          IDСorresponds idСorresponds1 = importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == id));
          if (idСorresponds1 != null)
          {
            IDBObject dbObject = this.session.GetObject(idСorresponds1.HostObjectID);
            IDBAttribute dbAttribute = dbObject.GetAttributeByGuid(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"));
            if (idСorresponds1.IsNew)
            {
              linkedContextNumber = idСorresponds1.HostObjectID;
              if (dbAttribute == null)
                dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad014ff-306c-11d8-b4e9-00304f19f545"), false);
              dbAttribute.Value = (object) linkedContextNumber;
            }
            else
              linkedContextNumber = dbAttribute != null ? dbAttribute.AsInteger : 0L;
          }
          if (linkedContextNumber != 0L)
          {
            string attribute = xmlTextReader.GetAttribute(BriefcaseConsts.XmlContextContentAttributeName);
            if (!string.IsNullOrEmpty(attribute))
            {
              string[] strArray = attribute.Split(';');
              List<long> fIDs = new List<long>();
              List<long> versionIDs = new List<long>();
              foreach (string str2 in strArray)
              {
                string s_id = str2;
                IDСorresponds idСorresponds2 = importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == Convert.ToInt64(s_id)));
                if (idСorresponds2 != null)
                {
                  fIDs.Add(idСorresponds2.HostID);
                  versionIDs.Add(idСorresponds2.HostObjectID);
                }
              }
              if (fIDs.Count > 0)
                service.AddToContext((object) this.session, idСorresponds1.HostObjectID, linkedContextNumber, (IList<long>) fIDs, (IList<long>) versionIDs, true, true);
            }
          }
          else
            this.eventLog.AddToTrace($"Не удалось определить Номер взаимосвязанного контекста для F_OBJECT_ID в портфеле = {id}");
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      bip.ErrorException = new Exception($"{BriefcaseConsts.logContextsUnknError}: {ex.Message}", ex);
      bip.Operation = OperationType.Error;
      this.SetImportProgress(this.briefcase, bip);
      this.eventLog.AddToTrace($"{BriefcaseConsts.logContextsUnknError}: {ex.Message}");
      return false;
    }
    finally
    {
      xmlTextReader.Close();
    }
  }

  private bool ReadRelations(
    DataSet[] metadata,
    BriefcaseImportProgress bip,
    BriefcaseImportProperties importProperties,
    ArrayList objectLinks,
    List<Tuple<Guid, Guid>> notImportedObjects,
    bool langEquals,
    bool throwException,
    int itemsCount,
    ref int i)
  {
    ImportingRelationAttribute relationAttribute = new ImportingRelationAttribute(this.session, this.eventLog, this.setImportProgressEvent);
    AttributeXmlReader attributeXmlReader = new AttributeXmlReader(this.eventLog);
    RelationXmlReader relationXmlReader = new RelationXmlReader(this.eventLog);
    XmlTextReader reader1 = new XmlTextReader(Path.Combine(this.briefcasePath, "Relations.xml"));
    XmlTextReader reader2 = new XmlTextReader(Path.Combine(this.briefcasePath, "RelAttributes.xml"));
    AttributeRecord attributeRecord = new AttributeRecord();
    try
    {
      AttributeRecord attr1 = new AttributeRecord();
      while (reader1.Read())
      {
        if (reader1.NodeType == XmlNodeType.Element && reader1.Name == BriefcaseConsts.XmlRelationRecordTag)
        {
          ImportingRelation importingRelation = new ImportingRelation(relationXmlReader.Read(reader1));
          bool flag = true;
          if (attr1.AttributeId != 0)
          {
            if (attr1.AttributableId == importingRelation.Relation.PrjLinkId)
            {
              if (!relationAttribute.AddAtribute((IUserSession) this.session, (ImportingAttributable) importingRelation, importingRelation.Relation.PrjLinkId, importingRelation.Relation.RelationType, this.briefcasePath, this.briefcase, metadata[0].Tables["IMS_ATTRIBUTES"], metadata[0].Tables["IMS_RELATION_TYPES"], attr1, bip, throwException))
                return false;
            }
            else
              flag = false;
          }
          if (flag)
          {
            while (reader2.Read())
            {
              if (reader2.NodeType == XmlNodeType.Element && reader2.Name == BriefcaseConsts.XmlAttributeRecordTag)
              {
                AttributeRecord attr2 = attributeXmlReader.Read(reader2);
                if (attr2.AttributableId != importingRelation.Relation.PrjLinkId)
                {
                  attr1 = attr2;
                  break;
                }
                if (!relationAttribute.AddAtribute((IUserSession) this.session, (ImportingAttributable) importingRelation, importingRelation.Relation.PrjLinkId, importingRelation.Relation.RelationType, this.briefcasePath, this.briefcase, metadata[0].Tables["IMS_ATTRIBUTES"], metadata[0].Tables["IMS_RELATION_TYPES"], attr2, bip, throwException))
                  return false;
                attr1 = new AttributeRecord();
              }
            }
          }
          if (this._checkerService.CheckImportingRelation((IUserSession) this.session, importingRelation, importProperties, notImportedObjects))
          {
            ImportBriefcaseRelation briefcaseRelation = new ImportBriefcaseRelation(this.session, metadata[0], importingRelation);
            if (briefcaseRelation.Import(langEquals, false) == 0L)
            {
              this.eventLog.AddToTrace(string.Format("{2}{0}: {1}", (object) string.Format(LocalizationHolder.rm.GetString("Kernel_980"), importingRelation.Relation.PrjLinkGuid, importingRelation.Relation.ProjId, importingRelation.Relation.PartId), (object) briefcaseRelation.ErrorException.Message, (object) BriefcaseConsts.logErrorString));
              if (throwException)
              {
                bip.ErrorException = briefcaseRelation.ErrorException;
                bip.Operation = OperationType.Error;
                this.SetImportProgress(this.briefcase, bip);
                foreach (string eventString in briefcaseRelation.Log)
                  this.eventLog.AddToTrace(eventString);
                return false;
              }
            }
            objectLinks.AddRange((ICollection) briefcaseRelation.ObjectLinks);
            foreach (string eventString in briefcaseRelation.Log)
              this.eventLog.AddToTrace(eventString);
          }
          else
            this.eventLog.AddToTrace($"Связь {{{importingRelation.Relation.PrjLinkGuid}}} не импортирована");
          ++i;
          bip.Percent = 100 * i / itemsCount;
          this.SetImportProgress(this.briefcase, bip);
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      bip.ErrorException = new Exception(BriefcaseConsts.logImportRelationUnknError, ex);
      bip.Operation = OperationType.Error;
      this.SetImportProgress(this.briefcase, bip);
      this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logImportRelationUnknError, (object) ex.Message));
      return false;
    }
    finally
    {
      reader1.Close();
      reader2.Close();
    }
  }

  private bool ReadObjects(
    DataSet[] metadata,
    BriefcaseImportProgress bip,
    BriefcaseImportProperties importProperties,
    ArrayList objectLinks,
    List<long> needRefreshFolderKeyObjects,
    List<FoundObjectInfo> findObjectsToObject,
    List<IDСorresponds> importingObjectIDs,
    List<Tuple<Guid, Guid>> notImportedObjects,
    bool langEquals,
    int briefcaseIndex,
    bool throwException,
    int itemsCount,
    ref int i)
  {
    XmlTextReader reader1 = new XmlTextReader(Path.Combine(this.briefcasePath, "Objects.xml"));
    XmlTextReader reader2 = new XmlTextReader(Path.Combine(this.briefcasePath, "ObjAttributes.xml"));
    XmlTextReader reader3 = new XmlTextReader(Path.Combine(this.briefcasePath, "ObjLcSteps.xml"));
    AttributeXmlReader attributeXmlReader = new AttributeXmlReader(this.eventLog);
    LCStepXmlReader lcStepXmlReader = new LCStepXmlReader(this.eventLog);
    ObjectXmlReader objectXmlReader = new ObjectXmlReader(this.eventLog);
    ImportingObjectAttribute importingObjectAttribute = new ImportingObjectAttribute(this.session, this.eventLog, this.setImportProgressEvent);
    AttributeRecord attr1 = new AttributeRecord();
    LCStepRecord step1 = new LCStepRecord();
    List<Guid> briefcaseObjects = BriefcaseContentXmlReader.ReadObjects(Path.Combine(this.briefcasePath, "ExportContent.xml"));
    HashSet<long> createdIDs = new HashSet<long>();
    try
    {
      bool flag1 = true;
      ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
      BriefcaseAttributes briefcaseAttributes = BriefcaseProcs.ReadBriefcaseAttributes(Path.Combine(this.briefcasePath, "BriefcaseConfig.xml"));
      if (briefcaseAttributes.SiteGuid != Guid.Empty && customService.GetSite(briefcaseAttributes.SiteGuid, false) != null)
        flag1 = false;
      while (reader1.Read())
      {
        if (reader1.NodeType == XmlNodeType.Element && reader1.Name == BriefcaseConsts.XmlObjectRecordTag)
        {
          ImportingObject importingObject = new ImportingObject(objectXmlReader.Read(reader1));
          bool flag2 = true;
          if (attr1.AttributeId != 0)
          {
            if (attr1.AttributableId == importingObject.Object.Object_id)
            {
              if (!importingObjectAttribute.AddAtribute((IUserSession) this.session, (ImportingAttributable) importingObject, importingObject.Object.Object_id, importingObject.Object.ObjectType, this.briefcasePath, this.briefcase, metadata[0].Tables["IMS_ATTRIBUTES"], metadata[0].Tables["IMS_OBJECT_TYPES"], attr1, bip, throwException))
                return false;
            }
            else
              flag2 = false;
          }
          if (flag2)
          {
            while (reader2.Read())
            {
              if (reader2.NodeType == XmlNodeType.Element && reader2.Name == BriefcaseConsts.XmlAttributeRecordTag)
              {
                AttributeRecord attr2 = attributeXmlReader.Read(reader2);
                if (attr2.AttributableId != importingObject.Object.Object_id)
                {
                  attr1 = attr2;
                  break;
                }
                if (!importingObjectAttribute.AddAtribute((IUserSession) this.session, (ImportingAttributable) importingObject, importingObject.Object.Object_id, importingObject.Object.ObjectType, this.briefcasePath, this.briefcase, metadata[0].Tables["IMS_ATTRIBUTES"], metadata[0].Tables["IMS_OBJECT_TYPES"], attr2, bip, throwException))
                  return false;
                attr1 = new AttributeRecord();
              }
            }
          }
          bool flag3 = true;
          if (step1.ObjectId != 0L)
          {
            if (step1.ObjectId == importingObject.Object.Object_id)
              importingObject.AddLCStep(step1);
            else
              flag3 = false;
          }
          if (flag3)
          {
            while (reader3.Read())
            {
              if (reader3.NodeType == XmlNodeType.Element && reader3.Name == BriefcaseConsts.XmlObjLCStepsRecordTag)
              {
                LCStepRecord step2 = lcStepXmlReader.Read(reader3);
                if (step2.ObjectId != importingObject.Object.Object_id)
                {
                  step1 = step2;
                  break;
                }
                importingObject.AddLCStep(step2);
                step1 = new LCStepRecord();
              }
            }
          }
          if (flag1 && importingObject.Object.SiteID != string.Empty)
            importingObject.Object.SiteID = string.Empty;
          ImportObject importObject = new ImportObject(this.session, metadata[0], importingObject, findObjectsToObject, importingObjectIDs, createdIDs);
          if (importObject.UnknownType)
          {
            DataRow dataRow = metadata[0].Tables["IMS_OBJECT_TYPES"].Rows.Find((object) importingObject.Object.ObjectType);
            this.eventLog.AddToTrace(string.Format("{2}{0}: {1}", (object) string.Format(LocalizationHolder.rm.GetString("Kernel_338"), importingObject.Object.ObjectGuid), (object) string.Format(BriefcaseConsts.ImportObjectTypeNotFound, dataRow["F_GUID"]), (object) BriefcaseConsts.logErrorString));
            if (throwException)
              return false;
          }
          else if (this._checkerService.CheckImportingObject((IUserSession) this.session, importingObject, importProperties, briefcaseObjects))
          {
            importObject.BriefcaseIndex = briefcaseIndex;
            if (!importObject.Import(langEquals))
            {
              this.eventLog.AddToTrace(string.Format("{2}{0}: {1}", (object) string.Format(LocalizationHolder.rm.GetString("Kernel_338"), importingObject.Object.ObjectGuid), (object) importObject.ErrorException.Message, (object) BriefcaseConsts.logErrorString));
              if (throwException)
              {
                bip.ErrorException = importObject.ErrorException;
                bip.Operation = OperationType.Error;
                this.SetImportProgress(this.briefcase, bip);
                foreach (string eventString in importObject.Log)
                  this.eventLog.AddToTrace(eventString);
                return false;
              }
            }
            objectLinks.AddRange((ICollection) importObject.ObjectLinks);
            if (importObject.NeedRefreshFolderKey != 0L)
              needRefreshFolderKeyObjects.Add(importObject.NeedRefreshFolderKey);
            foreach (string eventString in importObject.Log)
              this.eventLog.AddToTrace(eventString);
          }
          else
          {
            notImportedObjects.Add(new Tuple<Guid, Guid>((Guid) importingObject.Object.ObjectGuid, (Guid) importingObject.Object.IdGuid));
            this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_338"), importingObject.Object.ObjectGuid));
          }
          ++i;
          bip.Percent = 100 * i / itemsCount;
          this.SetImportProgress(this.briefcase, bip);
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      bip.ErrorException = new Exception(BriefcaseConsts.logImportObjectUnknError, ex);
      bip.Operation = OperationType.Error;
      this.SetImportProgress(this.briefcase, bip);
      this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logImportObjectUnknError, (object) ex.Message));
      return false;
    }
    finally
    {
      reader1.Close();
      reader2.Close();
      reader3.Close();
    }
  }
}
