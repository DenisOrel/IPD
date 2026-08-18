// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseExporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Threading;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

public class BriefcaseExporter : DBSessionable
{
  protected int dataBlockSize = Consts.DefaultBlobBlockSize;
  private static readonly string logFromSubscriberString = LocalizationHolder.rm.GetString("Intermech.Briefcase_41");
  private IUserSession session;
  private IServerBriefcase iServerBriefcase;
  private bool isOpened;
  public SetExportProgressHandler SetExportProgressEvent;
  private Guid briefcaseGuid;
  private BriefcaseExportStructure briefcaseExportStructure;
  private BriefcaseAttributes briefcaseAttributes = new BriefcaseAttributes(string.Empty, string.Empty, 0, DateTime.MinValue, DateTime.MinValue, false, false, Guid.Empty);
  private BriefcaseExportProgress bep;
  private Exception lastException;
  private AccessListHolder _accessListHolder;
  private int storageTypeId;
  private List<long> storageList = new List<long>();
  private int physTypeId;
  private bool allPhysIdProcessed;
  private List<long> physIdList = new List<long>();
  private List<bool> physIdProcessedList = new List<bool>();
  private DataSet metadataDataSet;
  private Hashtable objectVerCache = new Hashtable();
  private Hashtable objectCache = new Hashtable();
  private Hashtable objectIDtoBaseVerCache = new Hashtable();
  private Hashtable relTypesApplCache = new Hashtable();
  private BriefcaseLog log = new BriefcaseLog();
  private static string F_ATTR = nameof (F_ATTR);
  private static string F_RELTYPE = nameof (F_RELTYPE);
  private static string F_OBJTYPE = nameof (F_OBJTYPE);
  private static string F_INLIST = nameof (F_INLIST);
  private static string F_OBJVER = nameof (F_OBJVER);
  private static string F_GUID = nameof (F_GUID);
  private static string F_CUSTOM = nameof (F_CUSTOM);
  private DataTable objectVerUsability;
  private HybridDictionary exportSchemaPerObjectType = new HybridDictionary();
  private bool subjectAreaSecurityFirstWriteDone;
  private bool languageSecurityFirstWriteDone;
  private ExportHashHolder exportHolder = new ExportHashHolder();
  private ExportHashHolder exportQueueHolder = new ExportHashHolder();
  private string briefcaseFolder = string.Empty;
  private XmlTextWriter briefcaseMetadataExportListXML;
  private FileStream briefcaseMetadataExportListStream;
  private XmlTextWriter briefcaseObjectsXML;
  private FileStream briefcaseObjectsStream;
  private XmlTextWriter briefcaseRelationsXML;
  private FileStream briefcaseRelationsStream;
  private XmlTextWriter briefcaseObjAttributesXML;
  private FileStream briefcaseObjAttributesStream;
  private XmlTextWriter briefcaseRelAttributesXML;
  private FileStream briefcaseRelAttributesStream;
  private XmlTextWriter briefcaseObjLCStepsXML;
  private FileStream briefcaseObjLCStepsStream;
  private XmlTextWriter briefcaseContextsXML;
  private FileStream briefcaseContextsStream;
  private XmlTextWriter briefcaseMetadataSecurityXML;
  private FileStream briefcaseMetadataSecurityStream;
  private XmlTextWriter briefcaseObjSecurityXML;
  private FileStream briefcaseObjSecurityStream;
  private string exportVersionsRuleIdSource;
  private bool IsExportVersionsRuleIdActiveASSIGNED;
  private object IsExportVersionsRuleIdActiveVALUE;
  private VersionsRule IsExportVersionsRuleIdActiveRULE;
  private long exportVersionsRuleIdObject = -1;
  private bool IsAllObjectVersionExportRuleActiveASSIGNED;
  private bool IsAllObjectVersionExportRuleActiveVALUE;

  private AccessListHolder accessListHolder
  {
    get
    {
      if (this._accessListHolder == null)
        this._accessListHolder = new AccessListHolder();
      return this._accessListHolder;
    }
    set => this._accessListHolder = value;
  }

  private string BriefcaseFolder => this.briefcaseFolder;

  private string ExportLogFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}export.log";
  }

  private string ShortBlobBriefcaseFolder
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ShortBlob";
  }

  private string BlobBriefcaseFolder
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Blob";
  }

  private string MemoBriefcaseFolder
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Memo";
  }

  private string BriefcaseConfig
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}BriefcaseConfig.xml";
  }

  private string BriefcaseMetadataExportListFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}MetadataExportList.xml";
  }

  private string BriefcaseMetadataExportListSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}MetadataExportList.xsd";
  }

  private string BriefcaseMetadataFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Metadata.xml";
  }

  private string BriefcaseMetadataSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Metadata.xsd";
  }

  private string BriefcaseObjectsFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Objects.xml";
  }

  private string BriefcaseObjectsSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Objects.xsd";
  }

  private string BriefcaseObjAttributesFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ObjAttributes.xml";
  }

  private string BriefcaseObjAttributesSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ObjAttributes.xsd";
  }

  private string BriefcaseObjLCStepsFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ObjLcSteps.xml";
  }

  private string BriefcaseObjLCStepsSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ObjLcSteps.xsd";
  }

  private string BriefcaseContextsFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Contexts.xml";
  }

  private string BriefcaseContextsSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Contexts.xsd";
  }

  private string BriefcaseExportContentFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ExportContent.xml";
  }

  private string BriefcaseExportContentSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ExportContent.xsd";
  }

  private string BriefcaseRelationsFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Relations.xml";
  }

  private string BriefcaseRelationsSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}Relations.xsd";
  }

  private string BriefcaseRelAttributesFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}RelAttributes.xml";
  }

  private string BriefcaseRelAttributesSchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}RelAttributes.xsd";
  }

  private string BriefcaseMetadataSecurityFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}MetadataSecurity.xml";
  }

  private string BriefcaseMetadataSecuritySchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}MetadataSecurity.xsd";
  }

  private string BriefcaseObjSecurityFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ObjSecurity.xml";
  }

  private string BriefcaseObjSecuritySchemaFileName
  {
    get => $"{this.briefcaseFolder}{Path.DirectorySeparatorChar.ToString()}ObjSecurity.xsd";
  }

  internal BriefcaseExporter(
    UserSession userSession,
    Guid numOfBriefcase,
    BriefcaseExportStructure bes)
    : base(userSession)
  {
    this.InitSecurityOptions(14, 0L);
    this.CheckAccess(ActionType.Export, false, true);
    this.briefcaseGuid = numOfBriefcase;
    this.briefcaseExportStructure = bes;
    this.briefcaseFolder = bes.ExportProperties.ServerFolder;
  }

  public void Exporting()
  {
    new Thread(new ThreadStart(this.ExportData))
    {
      IsBackground = true
    }.Start();
  }

  private void ExportData()
  {
    this.session = this.UserSession.Clone(true, "Briefcase.ExportData");
    try
    {
      this.bep = new BriefcaseExportProgress(ExportOperationType.Idle);
      this.bep.Percent = 0;
      this.SetExportProgress(this.briefcaseGuid, this.bep);
      this.OpenBriefcase(true);
      (this.session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).Load((object) this.session.SessionGUID);
      this.iServerBriefcase = this.session.GetBriefcase();
      DateTime systemModifyDate = this.iServerBriefcase.SystemModifyDate(this.session.SessionGUID);
      if (this.ExportProc(this.iServerBriefcase, systemModifyDate))
      {
        this.briefcaseAttributes.LastSystemUpdate = systemModifyDate;
        if (this.CloseBriefcase(true))
        {
          this.bep.Percent = 100;
          this.bep.Operation = ExportOperationType.Finished;
        }
        else
        {
          this.bep.Operation = ExportOperationType.Error;
          this.bep.ErrorException = this.lastException;
        }
      }
      else
      {
        this.CloseBriefcase(false);
        this.bep.Operation = ExportOperationType.Error;
        this.bep.ErrorException = this.lastException;
      }
      this.SetExportProgress(this.briefcaseGuid, this.bep);
    }
    catch (Exception ex)
    {
      this.CloseBriefcase(false);
      this.bep.Operation = ExportOperationType.Error;
      this.bep.ErrorException = ex;
      this.SetExportProgress(this.briefcaseGuid, this.bep);
    }
    finally
    {
      this.session.Logout("Briefcase.ExportData");
      this.session = (IUserSession) null;
    }
  }

  private bool CreateBriefcaseSubfolder(string folderName)
  {
    if (!Directory.Exists(folderName))
    {
      try
      {
        Directory.CreateDirectory(folderName);
      }
      catch (Exception ex)
      {
        this.lastException = ex;
        this.log.WriteString("Error_003", $"{LocalizationHolder.rm.GetString("Intermech.Briefcase_45") + folderName}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
        this.bep.Operation = ExportOperationType.Error;
        this.bep.ErrorException = ex;
        this.SetExportProgress(this.briefcaseGuid, this.bep);
        return false;
      }
    }
    return true;
  }

  private void ChangeBriefcaseContent(List<ExportAttribute> exportAttrs)
  {
    XmlTextWriter xmlWriter = (XmlTextWriter) null;
    FileStream fileStream = (FileStream) null;
    BriefcaseStatics.WriteExportContentXMLSchema(this.BriefcaseExportContentSchemaFileName);
    xmlWriter?.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseExportContentFileName, out fileStream, out xmlWriter, BriefcaseConsts.XmlExportContentDatasetName);
    foreach (ExportAttribute exportAttr in exportAttrs)
      BriefcaseProcs.WriteToExportContentXml(this.session, xmlWriter, exportAttr);
    BriefcaseProcs.CloseXML(ref fileStream, ref xmlWriter);
  }

  private DataTable CreateObjVerUsabilityDataTable()
  {
    DataTable usabilityDataTable = new DataTable();
    usabilityDataTable.Columns.AddRange(new DataColumn[7]
    {
      new DataColumn(BriefcaseExporter.F_ATTR, typeof (int)),
      new DataColumn(BriefcaseExporter.F_RELTYPE, typeof (int)),
      new DataColumn(BriefcaseExporter.F_OBJTYPE, typeof (int)),
      new DataColumn(BriefcaseExporter.F_INLIST, typeof (int)),
      new DataColumn(BriefcaseExporter.F_OBJVER, typeof (long)),
      new DataColumn(BriefcaseExporter.F_GUID, typeof (Guid)),
      new DataColumn(BriefcaseExporter.F_CUSTOM, typeof (string))
    });
    return usabilityDataTable;
  }

  private bool OpenBriefcase(bool lForExport)
  {
    if (lForExport)
    {
      if (this.log != null)
        this.log.CloseLog();
      if (Directory.Exists(this.briefcaseFolder))
      {
        if (!BriefcaseProcs.DeleteBriefcase(this.briefcaseFolder, false, out this.lastException))
          return false;
      }
      else
        Directory.CreateDirectory(this.briefcaseFolder);
      BriefcaseProcs.WriteBriefcaseAttributes(this.BriefcaseConfig, this.briefcaseAttributes);
    }
    this.lastException = (Exception) null;
    if (!this.log.OpenLog(this.ExportLogFileName, true))
    {
      string str = LocalizationHolder.rm.GetString("Intermech.Briefcase_48") + this.ExportLogFileName;
      return false;
    }
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_49"));
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_50"), LogFlags.DATE);
    if (!this.CreateBriefcaseSubfolder(this.ShortBlobBriefcaseFolder) || !this.CreateBriefcaseSubfolder(this.BlobBriefcaseFolder) || !this.CreateBriefcaseSubfolder(this.MemoBriefcaseFolder))
      return false;
    this.briefcaseAttributes = BriefcaseProcs.ReadBriefcaseAttributes(this.BriefcaseConfig);
    this.exportHolder.ClearData();
    this.exportQueueHolder.ClearData();
    this.exportSchemaPerObjectType.Clear();
    this.objectVerCache.Clear();
    this.objectCache.Clear();
    this.objectIDtoBaseVerCache.Clear();
    this.relTypesApplCache.Clear();
    this.objectVerUsability = this.CreateObjVerUsabilityDataTable();
    this.metadataDataSet = (DataSet) null;
    this.IsExportVersionsRuleIdActiveASSIGNED = false;
    this.IsExportVersionsRuleIdActiveVALUE = (object) null;
    this.IsExportVersionsRuleIdActiveRULE = (VersionsRule) null;
    this.IsAllObjectVersionExportRuleActiveASSIGNED = false;
    this.IsAllObjectVersionExportRuleActiveVALUE = false;
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_51"), LogFlags.DATE);
    BriefcaseStatics.WriteMetadataExportListXMLSchema(this.BriefcaseMetadataExportListSchemaFileName);
    if (this.briefcaseMetadataExportListXML != null)
      this.briefcaseMetadataExportListXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseMetadataExportListFileName, out this.briefcaseMetadataExportListStream, out this.briefcaseMetadataExportListXML, BriefcaseConsts.XmlMetadataExportListDatasetName);
    BriefcaseStatics.WriteObjectsXMLSchema(this.BriefcaseObjectsSchemaFileName);
    if (this.briefcaseObjectsXML != null)
      this.briefcaseObjectsXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseObjectsFileName, out this.briefcaseObjectsStream, out this.briefcaseObjectsXML, BriefcaseConsts.XmlObjectsDatasetName);
    BriefcaseStatics.WriteRelationsXMLSchema(this.BriefcaseRelationsSchemaFileName);
    if (this.briefcaseRelationsXML != null)
      this.briefcaseRelationsXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseRelationsFileName, out this.briefcaseRelationsStream, out this.briefcaseRelationsXML, BriefcaseConsts.XmlRelationsDatasetName);
    BriefcaseStatics.WriteObjAttributesXMLSchema(this.BriefcaseObjAttributesSchemaFileName);
    if (this.briefcaseObjAttributesXML != null)
      this.briefcaseObjAttributesXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseObjAttributesFileName, out this.briefcaseObjAttributesStream, out this.briefcaseObjAttributesXML, BriefcaseConsts.XmlObjAttributesDatasetName);
    BriefcaseStatics.WriteRelAttributesXMLSchema(this.BriefcaseRelAttributesSchemaFileName);
    if (this.briefcaseRelAttributesXML != null)
      this.briefcaseRelAttributesXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseRelAttributesFileName, out this.briefcaseRelAttributesStream, out this.briefcaseRelAttributesXML, BriefcaseConsts.XmlRelAttributesDatasetName);
    BriefcaseStatics.WriteObjLCStepsXMLSchema(this.BriefcaseObjLCStepsSchemaFileName);
    if (this.briefcaseObjLCStepsXML != null)
      this.briefcaseObjLCStepsXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseObjLCStepsFileName, out this.briefcaseObjLCStepsStream, out this.briefcaseObjLCStepsXML, BriefcaseConsts.XmlObjLCStepsDatasetName);
    BriefcaseStatics.WriteContextsXMLSchema(this.BriefcaseContextsSchemaFileName);
    if (this.briefcaseContextsXML != null)
      this.briefcaseContextsXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseContextsFileName, out this.briefcaseContextsStream, out this.briefcaseContextsXML, BriefcaseConsts.XmlContextsDatasetName);
    BriefcaseStatics.WriteMetadataSecurityXMLSchema(this.BriefcaseMetadataSecuritySchemaFileName);
    if (this.briefcaseMetadataSecurityXML != null)
      this.briefcaseMetadataSecurityXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseMetadataSecurityFileName, out this.briefcaseMetadataSecurityStream, out this.briefcaseMetadataSecurityXML, BriefcaseConsts.XmlMetadataSecurityDatasetName);
    BriefcaseStatics.WriteObjSecurityXMLSchema(this.BriefcaseObjSecuritySchemaFileName);
    if (this.briefcaseObjSecurityXML != null)
      this.briefcaseObjSecurityXML.Close();
    BriefcaseProcs.OpenXML(this.BriefcaseObjSecurityFileName, out this.briefcaseObjSecurityStream, out this.briefcaseObjSecurityXML, BriefcaseConsts.XmlObjSecurityDatasetName);
    this.ChangeBriefcaseContent(this.briefcaseExportStructure.ExportProperties.ExportAttributes);
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_52"), LogFlags.DATE);
    this.isOpened = true;
    return true;
  }

  private bool CloseBriefcase(bool normal)
  {
    if (!this.isOpened)
      return false;
    if (normal && !this.FlushObjectsQueue())
      normal = false;
    this.exportHolder.ClearData();
    this.exportQueueHolder.ClearData();
    this.exportSchemaPerObjectType.Clear();
    this.objectVerCache.Clear();
    this.objectCache.Clear();
    this.objectIDtoBaseVerCache.Clear();
    this.relTypesApplCache.Clear();
    this.objectVerUsability = (DataTable) null;
    this.metadataDataSet = (DataSet) null;
    this.IsExportVersionsRuleIdActiveASSIGNED = false;
    this.IsExportVersionsRuleIdActiveVALUE = (object) null;
    this.IsExportVersionsRuleIdActiveRULE = (VersionsRule) null;
    this.IsAllObjectVersionExportRuleActiveASSIGNED = false;
    this.IsAllObjectVersionExportRuleActiveVALUE = false;
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_53"), LogFlags.DATE);
    this.CloseAllXML();
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_54"), LogFlags.DATE);
    if (this.lastException != null)
    {
      this.log.WriteString($"{LocalizationHolder.rm.GetString("Intermech.Briefcase_LastException")} {this.lastException.Message}", LogFlags.INFO);
      this.log.WriteString("Stacktrace:", LogFlags.INFO);
      this.log.WriteString("=============", LogFlags.INFO);
      this.log.WriteString(this.lastException.StackTrace, LogFlags.INFO);
      this.log.WriteString("=============", LogFlags.INFO);
    }
    if (normal)
    {
      this.briefcaseAttributes.Closed = true;
      this.briefcaseAttributes.ExportDate = DateTime.Now;
      BriefcaseProcs.WriteBriefcaseAttributes(this.BriefcaseConfig, this.briefcaseAttributes);
      this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_55"), LogFlags.DATE);
      this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_56"), LogFlags.DATE);
    }
    this.log.CloseLog();
    this.isOpened = false;
    return normal;
  }

  private void CloseAllXML()
  {
    BriefcaseProcs.CloseXML(ref this.briefcaseMetadataExportListStream, ref this.briefcaseMetadataExportListXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseObjectsStream, ref this.briefcaseObjectsXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseRelationsStream, ref this.briefcaseRelationsXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseObjAttributesStream, ref this.briefcaseObjAttributesXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseRelAttributesStream, ref this.briefcaseRelAttributesXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseObjLCStepsStream, ref this.briefcaseObjLCStepsXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseContextsStream, ref this.briefcaseContextsXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseMetadataSecurityStream, ref this.briefcaseMetadataSecurityXML);
    BriefcaseProcs.CloseXML(ref this.briefcaseObjSecurityStream, ref this.briefcaseObjSecurityXML);
  }

  private bool ExportProc(IServerBriefcase iServerBriefcase, DateTime systemModifyDate)
  {
    this.CheckExportAttributes();
    this.InitExclusions(this.session);
    this.InitPhysIdList(this.session);
    if (this.briefcaseAttributes.LastSystemUpdate != systemModifyDate)
    {
      this.metadataDataSet = iServerBriefcase.GetDataset(this.session.SessionGUID, new string[1]
      {
        "SYSTEM"
      }, (this.briefcaseExportStructure.ExportProperties.IncludeLocalization ? 1 : 0) != 0);
      this.metadataDataSet.DataSetName = BriefcaseConsts.XmlMetadataDatasetName;
      this.bep.Operation = ExportOperationType.CheckingMetadata;
      ++this.bep.Percent;
      this.SetExportProgress(this.briefcaseGuid, this.bep);
      if (!this.PreprocessExportMetadata(this.metadataDataSet))
        throw new Exception(LocalizationHolder.rm.GetString("Intermech.Briefcase_58"));
      this.metadataDataSet.WriteXmlSchema(this.BriefcaseMetadataSchemaFileName);
      this.metadataDataSet.WriteXml(this.BriefcaseMetadataFileName);
    }
    this.bep.Operation = ExportOperationType.ExportingMetaData;
    ++this.bep.Percent;
    this.SetExportProgress(this.briefcaseGuid, this.bep);
    bool flag = this.ExpandMetadata();
    this.bep.Operation = ExportOperationType.Exporting;
    ++this.bep.Percent;
    this.SetExportProgress(this.briefcaseGuid, this.bep);
    if (flag)
      flag = this.ExpandObjectVersions();
    return flag;
  }

  private void InitExclusions(IUserSession session)
  {
    this.storageTypeId = MetaDataHelper.GetObjectType(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).ObjectTypeID;
    this.storageList.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) session.ObjectsSelect(this.storageTypeId, new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    })).Rows)
      this.storageList.Add(Convert.ToInt64(row[0]));
    this.storageList.Sort();
  }

  private void InitPhysIdList(IUserSession session)
  {
    this.physTypeId = MetaDataHelper.GetObjectType(new Guid("cad00048-306c-11d8-b4e9-00304f19f545")).ObjectTypeID;
    this.allPhysIdProcessed = false;
    this.physIdList.Clear();
    this.physIdProcessedList.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) session.ObjectsSelect(this.physTypeId, new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    })).Rows)
    {
      this.physIdList.Add(Convert.ToInt64(row[0]));
      this.physIdProcessedList.Add(false);
    }
    this.physIdList.Sort();
  }

  private void CheckExportAttributes()
  {
    this.bep.Operation = ExportOperationType.CheckingData;
    ++this.bep.Percent;
    this.SetExportProgress(this.briefcaseGuid, this.bep);
    ArrayList arrayList1 = new ArrayList();
    ExportAttribute exportAttribute;
    for (int index = 0; index < this.briefcaseExportStructure.ExportProperties.ExportAttributes.Count; ++index)
    {
      exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index];
      if (exportAttribute.Identifiers == null)
      {
        ArrayList arrayList2 = arrayList1;
        exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index];
        // ISSUE: variable of a boxed type
        __Boxed<int> category1 = (System.ValueType) exportAttribute.Category;
        if (arrayList2.IndexOf((object) category1) == -1)
        {
          ArrayList arrayList3 = arrayList1;
          exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index];
          // ISSUE: variable of a boxed type
          __Boxed<int> category2 = (System.ValueType) exportAttribute.Category;
          arrayList3.Add((object) category2);
        }
      }
    }
    int index1 = 0;
    while (index1 < this.briefcaseExportStructure.ExportProperties.ExportAttributes.Count)
    {
      exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
      if (exportAttribute.Identifiers != null)
      {
        ArrayList arrayList4 = arrayList1;
        exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
        // ISSUE: variable of a boxed type
        __Boxed<int> category = (System.ValueType) exportAttribute.Category;
        if (arrayList4.IndexOf((object) category) != -1)
        {
          this.briefcaseExportStructure.ExportProperties.ExportAttributes.RemoveAt(index1);
          continue;
        }
      }
      ++index1;
    }
  }

  private bool PreprocessExportMetadata(DataSet ds)
  {
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.Briefcase_99"), LogFlags.DATE);
    DataSetProcessor.CreatePrimaryKeys(ds);
    DataTable table1 = ds.Tables["IMS_ATTRIBUTES"];
    ds.Tables["IMS_POSSIBLE_VALUES"].Columns.Add("F_INTEGERGUID", typeof (string));
    ds.Tables["IMS_ATTRIBUTES"].Columns.Add("F_SIZEGUID", typeof (string));
    ds.Tables["IMS_ATTRIBUTES"].Columns.Add("F_DEFAULTGUID", typeof (string));
    ds.Tables["IMS_ATTR4OBJ_TYPES"].Columns.Add("F_DEFAULTGUID", typeof (string));
    ds.Tables["IMS_ATTR4RELATION_TYPES"].Columns.Add("F_DEFAULTGUID", typeof (string));
    DataTable table2 = ds.Tables["IMS_POSSIBLE_VALUES"];
    if (table2 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) table2.Rows)
      {
        if (row["F_INTEGER_VALUE"] != DBNull.Value)
        {
          DataRow dataRow = table1.Rows.Find((object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
          int int32 = Convert.ToInt32(dataRow["F_ATTRIBUTE_TYPE"]);
          if (dataRow != null && (int32 == 8 || int32 == 17))
          {
            long int64 = Convert.ToInt64(row["F_INTEGER_VALUE"]);
            long verId = int64;
            if (int32 == 17 && !this.GetBaseObjVerByID(int64, out verId, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre003a") + $" ID='{int64}'"))
            {
              this.log.WriteString("Error_088", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_100a"), (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]).ToString(), (object) Convert.ToString(dataRow["F_NAME"]), (object) int64.ToString()), LogFlags.DATE);
              return false;
            }
            if (this.storageList.BinarySearch(verId) >= 0)
            {
              row["F_INTEGER_VALUE"] = (object) DBNull.Value;
            }
            else
            {
              object guid = (object) null;
              if (!this.GetObjVerGuid(verId, out guid, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre003") + $" versionid='{verId}'"))
              {
                this.log.WriteString("Error_032", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_100"), (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]).ToString(), (object) Convert.ToString(dataRow["F_NAME"]), (object) verId.ToString()), LogFlags.DATE);
                return false;
              }
              if (guid != null)
              {
                row["F_INTEGERGUID"] = (object) guid.ToString();
                row.AcceptChanges();
                this.AddObjVerToUsability(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]), Convert.ToInt32(row["F_RELATION_TYPE"]), Convert.ToInt32(row["F_INLIST_ID"]), verId, guid, (string) null);
              }
            }
          }
        }
      }
    }
    DataTable table3 = ds.Tables["IMS_ATTRIBUTES"];
    if (table3 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) table3.Rows)
      {
        Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        FieldTypes int32 = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
        if ((int32 == FieldTypes.ftMeasured || int32 == FieldTypes.ftExternalLink) && row["F_SIZE_TYPE"] != DBNull.Value)
        {
          long int64 = Convert.ToInt64(row["F_SIZE_TYPE"]);
          if (int32 == FieldTypes.ftMeasured && int64 != -1L || int32 == FieldTypes.ftExternalLink)
          {
            if (this.storageList.BinarySearch(int64) >= 0)
            {
              row["F_SIZE_TYPE"] = (object) DBNull.Value;
              row.AcceptChanges();
            }
            else
            {
              object guid = (object) null;
              string str = int32 == FieldTypes.ftMeasured ? LocalizationHolder.rm.GetString("Intermech.Briefcase_101") : LocalizationHolder.rm.GetString("Intermech.Briefcase_102");
              if (!this.GetObjVerGuid(int64, out guid, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre004"), (object) int64) + str))
              {
                this.log.WriteString("Error_033", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_103") + str, (object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]).ToString(), (object) Convert.ToString(row["F_NAME"]), (object) int64.ToString()), LogFlags.DATE);
                return false;
              }
              if (guid != null)
              {
                row["F_SIZEGUID"] = (object) guid.ToString();
                row.AcceptChanges();
                this.AddObjVerToUsability(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), -1, -1, 0, int64, guid, (string) null);
              }
            }
          }
        }
        if (int32 == FieldTypes.ftObjectLink || int32 == FieldTypes.ftObjectLinkByID || int32 == FieldTypes.ftExternalLink)
        {
          if (row["F_DEFAULT_VALUE"].ToString() != string.Empty)
          {
            bool flag = false;
            long id = 0;
            long verId = 0;
            try
            {
              id = Convert.ToInt64(row["F_DEFAULT_VALUE"]);
              verId = id;
            }
            catch (FormatException ex)
            {
              if (Convert.ToString(row["F_DEFAULT_VALUE"]) == Consts.CurrentUserFunction)
                flag = true;
              else
                throw;
            }
            if (!flag)
            {
              if (int32 == FieldTypes.ftObjectLinkByID && !this.GetBaseObjVerByID(id, out verId, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre005a") + $" ID='{id}'"))
              {
                this.log.WriteString("Error_089", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_104a"), (object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]).ToString(), (object) Convert.ToString(row["F_NAME"]), (object) id.ToString()), LogFlags.DATE);
                return false;
              }
              if (this.storageList.BinarySearch(verId) >= 0)
              {
                row["F_DEFAULT_VALUE"] = (object) DBNull.Value;
                row.AcceptChanges();
              }
              else
              {
                object guid = (object) null;
                if (!this.GetObjVerGuid(verId, out guid, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre005") + $" versionid='{verId}'"))
                {
                  this.log.WriteString("Error_034", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_104"), (object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]).ToString(), (object) Convert.ToString(row["F_NAME"]), (object) verId.ToString()), LogFlags.DATE);
                  return false;
                }
                if (guid != null)
                {
                  row["F_DEFAULTGUID"] = (object) guid.ToString();
                  row.AcceptChanges();
                  this.AddObjVerToUsability(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), -1, -1, 0, verId, guid, (string) null);
                }
              }
            }
          }
          if (!this.ConvertDefault4Types(ds, "IMS_ATTR4OBJ_TYPES", row) || !this.ConvertDefault4Types(ds, "IMS_ATTR4RELATION_TYPES", row))
            return false;
        }
      }
    }
    this.log.WriteString(LocalizationHolder.rm.GetString("Intermech.EndMetadataPreprocess"), LogFlags.DATE);
    return true;
  }

  private bool ExpandMetadata()
  {
    double num1 = (double) (21 / this.briefcaseExportStructure.ExportProperties.ExportAttributes.Count);
    double percent = (double) this.bep.Percent;
    for (int index1 = 0; index1 < this.briefcaseExportStructure.ExportProperties.ExportAttributes.Count; ++index1)
    {
      ExportAttribute exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
      int category1 = exportAttribute.Category;
      if (BriefcaseConsts.IsMetadataCategory(category1))
      {
        exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
        if (exportAttribute.Identifiers == null)
        {
          object[] metadataCategory = BriefcaseConsts.GetRootByMetadataCategory(category1, this.metadataDataSet);
          for (int index2 = 0; index2 < metadataCategory.Length; ++index2)
          {
            if (!this.ExportMetadataPrim(category1, metadataCategory[index2], ExportDirection.Down, LocalizationHolder.rm.GetString("Intermech.Briefcase_59")))
            {
              string str = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_155"), (object) Consts.GetCategoryName(category1), metadataCategory[index2]);
              this.log.WriteString("Error_042", str, LogFlags.DATE);
              throw new Exception(str);
            }
          }
        }
        else
        {
          int index3 = 0;
          while (true)
          {
            int num2 = index3;
            exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
            int length = exportAttribute.Identifiers.Length;
            if (num2 < length)
            {
              int category2 = category1;
              exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
              object identifier = exportAttribute.Identifiers[index3];
              object[] objArray = Array.Empty<object>();
              if (this.ExportMetadataPrim(category2, identifier, ExportDirection.Down, "", objArray))
                ++index3;
              else
                break;
            }
            else
              goto label_13;
          }
          string format = LocalizationHolder.rm.GetString("Intermech.Briefcase_155");
          string categoryName = Consts.GetCategoryName(category1);
          exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
          object identifier1 = exportAttribute.Identifiers[index3];
          string str = string.Format(format, (object) categoryName, identifier1);
          this.log.WriteString("Error_043", str, LogFlags.DATE);
          throw new Exception(str);
        }
      }
label_13:
      percent += num1;
      this.bep.Percent = Convert.ToInt32(percent);
      this.bep.Operation = ExportOperationType.ExportingMetaData;
      this.SetExportProgress(this.briefcaseGuid, this.bep);
    }
    this.briefcaseMetadataExportListXML.Flush();
    return true;
  }

  private bool ExportMetadataPrim(
    int category,
    object id,
    ExportDirection direction,
    string logComment,
    params object[] arg)
  {
    string format = LocalizationHolder.rm.GetString("Intermech.Briefcase_CategoryAndIDError");
    if (this.briefcaseExportStructure.ExportProperties.ExportSecurity && category == 16 /*0x10*/ && arg != null && arg.Length != 0 && arg[0] is CategoryDescriptorPrim && ((CategoryDescriptorPrim) arg[0]).Category == 4)
    {
      if (!this.ExportMetadataPrim(category, id, direction, logComment))
      {
        this.log.WriteString("Error_053", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_LCSchemaError"), id), LogFlags.DATE);
        return false;
      }
      CategoryDescriptorPrim categoryDescriptorPrim = (CategoryDescriptorPrim) arg[0];
      List<int> intList = (List<int>) this.exportSchemaPerObjectType[(object) Convert.ToInt32(id)];
      if (intList == null || intList.IndexOf(Convert.ToInt32(categoryDescriptorPrim.Id)) == -1)
      {
        if (intList == null)
        {
          intList = new List<int>();
          this.exportSchemaPerObjectType[id] = (object) intList;
        }
        intList.Add(Convert.ToInt32(categoryDescriptorPrim.Id));
        foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_LC_STEPS"].Select($"F_SCHEMA_ID={id.ToString()} and F_DELETED=0"))
        {
          int int32 = Convert.ToInt32(dataRow["F_LC_STEP"]);
          if (this.session.GetLifecycleStep(int32, Convert.ToInt32(categoryDescriptorPrim.Id)) is IDBSecurity lifecycleStep && !this.ExportSecurityPrim(lifecycleStep, 7, (object) int32, (object) Convert.ToInt32(categoryDescriptorPrim.Id), string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_156"), categoryDescriptorPrim.Id)))
          {
            string str = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_60"), (object) int32, categoryDescriptorPrim.Id);
            this.log.WriteString("Error_044", str, LogFlags.DATE);
            throw new Exception(str);
          }
        }
      }
      return true;
    }
    switch (category)
    {
      case 3:
        if (Convert.ToInt32(id) <= 0)
          return true;
        break;
      case 4:
        if (Convert.ToInt32(id) == -1)
          return true;
        break;
      case 6:
        if (Convert.ToInt32(id) == -1)
          return true;
        break;
      case 7:
        if (Convert.ToInt32(id) == 0)
          return true;
        break;
      case 8:
        if (Convert.ToInt32(id) == 0)
          return true;
        break;
      case 9:
        if (id.ToString().Trim() == string.Empty)
          return true;
        break;
      case 11:
        string str1 = id.ToString().Trim();
        if (str1 == string.Empty)
          return true;
        if (str1.Length > 1)
        {
          for (int index = 0; index < str1.Length; ++index)
          {
            if (!this.ExportMetadataPrim(category, (object) str1[index].ToString(), direction, ""))
            {
              string str2 = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_157"), (object) Consts.GetCategoryName(category), (object) str1[index]);
              this.log.WriteString("Error_045", str2, LogFlags.DATE);
              throw new Exception(str2);
            }
          }
          return true;
        }
        break;
      case 16 /*0x10*/:
        if (Convert.ToInt32(id) == 0)
          return true;
        break;
    }
    if (this.exportHolder.GetExternalId(category, id) != null)
      return true;
    this.log.WriteString($"{string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_MetadataPrim_Start"), (object) Consts.GetCategoryName(category), id)} {logComment}", LogFlags.INFO | LogFlags.DATE);
    DataRow row = (DataRow) null;
    object metadataExternalId = BriefcaseConsts.GetMetadataExternalId(category, id, this.metadataDataSet, out row);
    if (metadataExternalId == null || row == null)
    {
      string str3 = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_61"), (object) Consts.GetCategoryName(category), id);
      this.log.WriteString("Error_041", str3, LogFlags.DATE);
      throw new Exception(str3);
    }
    string empty1 = string.Empty;
    string s;
    if (this.briefcaseExportStructure.ExportProperties.ExpandedLog)
    {
      string empty2 = string.Empty;
      string metadataNameByRow = BriefcaseConsts.GetMetadataNameByRow(category, row);
      s = string.Format($"{LocalizationHolder.rm.GetString("Intermech.Briefcase_MetadataInList")} {logComment}", (object) Consts.GetCategoryName(category), id, (object) metadataNameByRow, metadataExternalId, (object) direction);
    }
    else
      s = string.Format($"{LocalizationHolder.rm.GetString("Intermech.Briefcase_158")} {logComment}", (object) Consts.GetCategoryName(category), id, metadataExternalId, (object) direction);
    this.log.WriteString(s, LogFlags.INFO | LogFlags.DATE);
    this.exportHolder.AssignExternalId(category, id, metadataExternalId);
    BriefcaseProcs.WriteToMetadataExportListXml(this.briefcaseMetadataExportListXML, new MetadataRecord(category, id, metadataExternalId));
    if (this.briefcaseExportStructure.ExportProperties.ExportSecurity)
    {
      bool flag = true;
      if (category == 11)
      {
        if (this.subjectAreaSecurityFirstWriteDone)
          flag = false;
        else
          this.subjectAreaSecurityFirstWriteDone = true;
      }
      if (category == 9)
      {
        if (this.languageSecurityFirstWriteDone)
          flag = false;
        else
          this.languageSecurityFirstWriteDone = true;
      }
      if (flag && !this.ExportSecurity(category, id))
      {
        string str4 = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_62"), (object) Consts.GetCategoryName(category), id);
        this.log.WriteString("Error_046", str4, LogFlags.DATE);
        throw new Exception(str4);
      }
    }
    ArrayList arrayList = new ArrayList((ICollection) (this.iServerBriefcase as ICategoryExportManager).GetRegisteredCategoryExport(category));
    for (int index1 = 0; index1 < arrayList.Count; ++index1)
    {
      long[] linkedObjectVersions;
      try
      {
        linkedObjectVersions = ((ICategoryExport) arrayList[index1]).GetLinkedObjectVersions(this.session, category, id);
      }
      catch (Exception ex)
      {
        string str5 = string.Format(LocalizationHolder.rm.GetString("BriefcaseExceptionFromSubscriber"), (object) category, id, (object) ((ICategoryExport) arrayList[index1]).ExporterName);
        this.log.WriteString("Error_086", str5, LogFlags.DATE);
        throw new Exception(str5);
      }
      if (linkedObjectVersions != null)
      {
        for (int index2 = 0; index2 < linkedObjectVersions.Length; ++index2)
        {
          if (!this.PutObjectVersionOnQueue(linkedObjectVersions[index2], BriefcaseExporter.logFromSubscriberString))
          {
            string str6 = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) linkedObjectVersions[index2]);
            this.log.WriteString("Error_047", str6, LogFlags.DATE);
            throw new Exception(str6);
          }
        }
      }
    }
    switch (category)
    {
      case 3:
        if (!this.ExportMetadataPrim(11, (object) Convert.ToString(row["F_AREA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_161"), id)))
        {
          this.log.WriteString("Error_057", $"{string.Format(format, (object) 11, (object) Convert.ToString(row["F_AREA_ID"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(9, (object) Convert.ToString(row["F_LANGUAGE_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_161"), id)))
        {
          this.log.WriteString("Error_058", $"{string.Format(format, (object) 9, (object) Convert.ToString(row["F_LANGUAGE_ID"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(8, (object) Convert.ToInt32(row["F_LEVEL_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_161"), id)))
        {
          this.log.WriteString("Error_059", $"{string.Format(format, (object) 8, (object) Convert.ToInt32(row["F_LEVEL_ID"]))} {logComment}");
          return false;
        }
        FieldTypes int32_1 = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
        if ((int32_1 == FieldTypes.ftObjectLink || int32_1 == FieldTypes.ftObjectLinkByID) && !this.ExportMetadataPrim(4, (object) Convert.ToInt32(row["F_SIZE_TYPE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_161"), id) + LocalizationHolder.rm.GetString("Intermech.Briefcase_191")))
        {
          this.log.WriteString("Error_060", $"{string.Format(format, (object) 4, (object) Convert.ToInt32(row["F_SIZE_TYPE"]))} {logComment}");
          return false;
        }
        int index3;
        if (int32_1 == FieldTypes.ftMeasured && !this.allPhysIdProcessed)
        {
          DataTable table = this.metadataDataSet.Tables["IMS_MD_EXTENSIONS"];
          string str7 = id.ToString();
          index3 = 1;
          string str8 = index3.ToString();
          string filterExpression = $"F_ATTRIBUTE_ID={str7} and F_PARAM_NAME='MU_PHYSICAL_ID' and F_CATEGORY_TYPE={str8}";
          DataRow[] dataRowArray1 = table.Select(filterExpression);
          if (dataRowArray1.Length == 0)
          {
            for (int index4 = 0; index4 < this.physIdList.Count; ++index4)
            {
              if (!this.physIdProcessedList[index4])
              {
                IDBObject iDBObject = this.session.GetObject(this.physIdList[index4]);
                if (iDBObject == null || !this.ExportObjectVerPrim(iDBObject, false))
                  return false;
                this.physIdProcessedList[index4] = true;
              }
            }
            this.allPhysIdProcessed = true;
          }
          else
          {
            DataRow[] dataRowArray2 = dataRowArray1;
            for (index3 = 0; index3 < dataRowArray2.Length; ++index3)
            {
              long int64 = Convert.ToInt64(dataRowArray2[index3]["F_VALUE"]);
              int index5 = this.physIdList.IndexOf(int64);
              if (index5 != -1 && !this.physIdProcessedList[index5])
              {
                IDBObject iDBObject = this.session.GetObject(int64);
                if (iDBObject == null || !this.ExportObjectVerPrim(iDBObject, false))
                  return false;
                this.physIdProcessedList[index5] = true;
              }
            }
          }
        }
        DataRow[] dataRowArray3 = this.objectVerUsability.Select($"{BriefcaseExporter.F_ATTR}={id.ToString()} and {BriefcaseExporter.F_OBJTYPE}=-1 and {BriefcaseExporter.F_RELTYPE}=-1");
        for (index3 = 0; index3 < dataRowArray3.Length; ++index3)
        {
          long int64 = Convert.ToInt64(dataRowArray3[index3][BriefcaseExporter.F_OBJVER]);
          if (!this.PutObjectVersionOnQueue(int64, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_PossibleValue4Attribute"), id)))
          {
            this.log.WriteString("Error_050", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) int64), LogFlags.DATE);
            return false;
          }
        }
        int int32_2 = row["F_MASTER_ID"] != DBNull.Value ? Convert.ToInt32(row["F_MASTER_ID"]) : 0;
        if (int32_2 != 0 && !this.ExportMetadataPrim(3, (object) int32_2, ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_162"), id)))
        {
          this.log.WriteString("Error_061", $"{string.Format(format, (object) 3, (object) int32_2)} {logComment}");
          return false;
        }
        int int32_3 = row["F_SOURCE_ID"] != DBNull.Value ? Convert.ToInt32(row["F_SOURCE_ID"]) : 0;
        if (int32_3 != 0 && !this.ExportMetadataPrim(3, (object) int32_3, ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_163"), id)))
        {
          this.log.WriteString("Error_062", $"{string.Format(format, (object) 3, (object) int32_3)} {logComment}");
          return false;
        }
        DataRow[] dataRowArray4 = this.metadataDataSet.Tables["IMS_FORMULA_ATTRS"].Select($"F_FORMULA_ID={id.ToString()} and F_OBJECT_TYPE=-1 and F_RELATION_TYPE=-1");
        for (index3 = 0; index3 < dataRowArray4.Length; ++index3)
        {
          DataRow dataRow = dataRowArray4[index3];
          if (!this.ExportMetadataPrim(3, (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_164"), id)))
          {
            this.log.WriteString("Error_063", $"{string.Format(format, (object) 3, (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]))} {logComment}");
            return false;
          }
        }
        break;
      case 4:
        if (!this.ExportMetadataPrim(11, (object) Convert.ToString(row["F_AREA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_168"), id)))
        {
          this.log.WriteString("Error_068", $"{string.Format(format, (object) 11, (object) Convert.ToString(row["F_AREA_ID"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(6, (object) Convert.ToInt32(row["F_DEFAULT_RELATION"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_169"), id)))
        {
          this.log.WriteString("Error_069", $"{string.Format(format, (object) 6, (object) Convert.ToInt32(row["F_DEFAULT_RELATION"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(3, (object) Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_170"), id)))
        {
          this.log.WriteString("Error_070", $"{string.Format(format, (object) 3, (object) Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(16 /*0x10*/, (object) Convert.ToInt32(row["F_SCHEMA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_171"), id), (object) new CategoryDescriptorPrim(4, id)))
        {
          this.log.WriteString("Error_071", $"{string.Format(format, (object) 16 /*0x10*/, (object) Convert.ToInt32(row["F_SCHEMA_ID"]))} {logComment}");
          return false;
        }
        DataTable objectVerUsability1 = this.objectVerUsability;
        string filterExpression1 = $"{BriefcaseExporter.F_OBJTYPE}={id.ToString()} and {BriefcaseExporter.F_RELTYPE}=-1";
        foreach (DataRow dataRow in objectVerUsability1.Select(filterExpression1))
        {
          long int64 = Convert.ToInt64(dataRow[BriefcaseExporter.F_OBJVER]);
          if (!this.PutObjectVersionOnQueue(int64, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_PossibleValue4Attribute4ObjType"), (object) Convert.ToString(dataRow[BriefcaseExporter.F_ATTR]), id)))
          {
            this.log.WriteString("Error_051", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) int64), LogFlags.DATE);
            return false;
          }
        }
        foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_ATTR4OBJ_TYPES"].Select("F_OBJECT_TYPE=" + id.ToString()))
        {
          if (!this.ExportMetadataPrim(3, (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_172"), id)))
          {
            this.log.WriteString("Error_072", $"{string.Format(format, (object) 3, (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]))} {logComment}");
            return false;
          }
          if (!this.ExportMetadataPrim(8, (object) Convert.ToInt32(dataRow["F_LEVEL_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_173"), id)))
          {
            this.log.WriteString("Error_073", $"{string.Format(format, (object) 8, (object) Convert.ToInt32(dataRow["F_LEVEL_ID"]))} {logComment}");
            return false;
          }
        }
        foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_TYPES_APPLICABILITY"].Select("F_OBJECT_TYPE=" + id.ToString()))
        {
          if (!this.ExportMetadataPrim(6, (object) Convert.ToInt32(dataRow["F_RELATION_TYPE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_174"), id)))
          {
            this.log.WriteString("Error_074", $"{string.Format(format, (object) 6, (object) Convert.ToInt32(dataRow["F_RELATION_TYPE"]))} {logComment}");
            return false;
          }
          if (!this.ExportMetadataPrim(4, (object) Convert.ToInt32(dataRow["F_INOBJECT_TYPE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_192"), (object) Convert.ToInt32(dataRow["F_RELATION_TYPE"]).ToString()) + string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_193"), id)))
          {
            this.log.WriteString("Error_075", $"{string.Format(format, (object) 4, (object) Convert.ToInt32(dataRow["F_INOBJECT_TYPE"]))} {logComment}");
            return false;
          }
        }
        foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_TYPES_APPLICABILITY"].Select("F_INOBJECT_TYPE=" + id.ToString()))
        {
          if (!this.ExportMetadataPrim(6, (object) Convert.ToInt32(dataRow["F_RELATION_TYPE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_174"), id)))
          {
            this.log.WriteString("Error_076", $"{string.Format(format, (object) 6, (object) Convert.ToInt32(dataRow["F_RELATION_TYPE"]))} {logComment}");
            return false;
          }
          if (!this.ExportMetadataPrim(4, (object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_194"), (object) Convert.ToInt32(dataRow["F_RELATION_TYPE"])) + string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_193"), id)))
          {
            this.log.WriteString("Error_077", $"{string.Format(format, (object) 4, (object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"]))} {logComment}");
            return false;
          }
        }
        DataRow[] dataRowArray5 = this.metadataDataSet.Tables["IMS_OBJTYPES_TREE"].Select("F_OBJECT_TYPE=" + id.ToString());
        if (dataRowArray5.Length != 0 && Convert.ToInt32(dataRowArray5[0]["F_PARENT_ID"]) >= 0 && !this.ExportMetadataPrim(4, (object) Convert.ToInt32(dataRowArray5[0]["F_PARENT_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_175"), id)))
        {
          this.log.WriteString("Error_078", $"{string.Format(format, (object) 4, (object) Convert.ToInt32(dataRowArray5[0]["F_PARENT_ID"]))} {logComment}");
          return false;
        }
        if (direction == ExportDirection.Down)
        {
          foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_OBJTYPES_TREE"].Select("F_PARENT_ID=" + id.ToString()))
          {
            if (!this.ExportMetadataPrim(4, (object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), ExportDirection.Down, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_176"), id)))
            {
              this.log.WriteString("Error_079", $"{string.Format(format, (object) 4, (object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"]))} {logComment}");
              return false;
            }
          }
        }
        IEnumerator enumerator1 = this.session.ObjectsSelect(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, metadataExternalId, LogicalOperators.OR, 0),
          new ConditionStructure(new Guid("cad00922-306c-11d8-b4e9-00304f19f545"), RelationalOperators.EndString, (object) metadataExternalId.ToString(), LogicalOperators.NONE, 0)
        }, new object[1]{ (object) -2 })).Rows.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            IDBObject dbObject = this.session.GetObject(Convert.ToInt64(((DataRow) enumerator1.Current)[0]));
            if (dbObject != null)
            {
              long id1 = dbObject.ID;
              if (!this.PutObjectVersionOnQueue(dbObject.ObjectID, LocalizationHolder.rm.GetString("Intermech.Briefcase_64")))
              {
                string str9 = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_65"), (object) id1);
                this.log.WriteString("Error_049", str9, LogFlags.DATE);
                throw new Exception(str9);
              }
            }
          }
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      case 6:
        if (!this.ExportMetadataPrim(11, (object) Convert.ToString(row["F_AREA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_177"), id)))
        {
          this.log.WriteString("Error_080", $"{string.Format(format, (object) 11, (object) Convert.ToString(row["F_AREA_ID"]))} {logComment}");
          return false;
        }
        DataTable objectVerUsability2 = this.objectVerUsability;
        string filterExpression2 = $"{BriefcaseExporter.F_OBJTYPE}=-1 and {BriefcaseExporter.F_RELTYPE}={id.ToString()}";
        foreach (DataRow dataRow in objectVerUsability2.Select(filterExpression2))
        {
          long int64 = Convert.ToInt64(dataRow[BriefcaseExporter.F_OBJVER]);
          if (!this.PutObjectVersionOnQueue(int64, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_PossibleValue4Attribute4RelType"), (object) Convert.ToString(dataRow[BriefcaseExporter.F_ATTR]), id)))
          {
            this.log.WriteString("Error_052", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) int64), LogFlags.DATE);
            return false;
          }
        }
        foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_ATTR4RELATION_TYPES"].Select("F_RELATION_TYPE=" + id.ToString()))
        {
          if (!this.ExportMetadataPrim(3, (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_178"), id)))
          {
            this.log.WriteString("Error_081", $"{string.Format(format, (object) 3, (object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]))} {logComment}");
            return false;
          }
        }
        break;
      case 7:
        if (!this.ExportMetadataPrim(8, (object) Convert.ToInt32(row["F_LEVEL_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_167"), id)))
        {
          this.log.WriteString("Error_065", $"{string.Format(format, (object) 8, (object) Convert.ToInt32(row["F_LEVEL_ID"]))} {logComment}");
          return false;
        }
        if (Convert.ToInt32(row["F_OBJECT_TYPE"]) != 0 && !this.ExportMetadataPrim(4, (object) Convert.ToInt32(row["F_OBJECT_TYPE"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_167"), id)))
        {
          this.log.WriteString("Error_066", $"{string.Format(format, (object) 4, (object) Convert.ToInt32(row["F_OBJECT_TYPE"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(16 /*0x10*/, (object) Convert.ToInt32(row["F_SCHEMA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_167"), id)))
        {
          this.log.WriteString("Error_067", $"{string.Format(format, (object) 16 /*0x10*/, (object) Convert.ToInt32(row["F_SCHEMA_ID"]))} {logComment}");
          return false;
        }
        IEnumerator enumerator2 = this.session.ObjectsSelect(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(new Guid("cad0014c-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, metadataExternalId, LogicalOperators.OR, 0),
          new ConditionStructure(new Guid("cad00922-306c-11d8-b4e9-00304f19f545"), RelationalOperators.StartString, (object) metadataExternalId.ToString(), LogicalOperators.NONE, 0)
        }, new object[1]{ (object) -2 })).Rows.GetEnumerator();
        try
        {
          while (enumerator2.MoveNext())
          {
            IDBObject dbObject = this.session.GetObject(Convert.ToInt64(((DataRow) enumerator2.Current)[0]));
            if (dbObject != null)
            {
              long id2 = dbObject.ID;
              if (!this.PutObjectVersionOnQueue(dbObject.ObjectID, LocalizationHolder.rm.GetString("Intermech.Briefcase_64")))
              {
                string str10 = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_65"), (object) id2);
                this.log.WriteString("Error_048", str10, LogFlags.DATE);
                throw new Exception(str10);
              }
            }
          }
          break;
        }
        finally
        {
          if (enumerator2 is IDisposable disposable)
            disposable.Dispose();
        }
      case 8:
        if (!this.ExportMetadataPrim(11, (object) Convert.ToString(row["F_AREA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_166"), id)))
        {
          this.log.WriteString("Error_064", $"{string.Format(format, (object) 11, (object) Convert.ToString(row["F_AREA_ID"]))} {logComment}");
          return false;
        }
        break;
      case 12:
        if (!this.ExportMetadataPrim(11, (object) Convert.ToString(row["F_AREA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_159"), id)))
        {
          this.log.WriteString("Error_054", $"{string.Format(format, (object) 11, (object) Convert.ToString(row["F_AREA_ID"]))} {logComment}");
          return false;
        }
        if (!this.ExportMetadataPrim(9, (object) Convert.ToString(row["F_LANGUAGE_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_159"), id)))
        {
          this.log.WriteString("Error_055", $"{string.Format(format, (object) 9, (object) Convert.ToString(row["F_LANGUAGE_ID"]))} {logComment}");
          return false;
        }
        if (direction == ExportDirection.Down)
        {
          foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_ATTR_IN_GROUPS"].Select("F_GROUP_ID=" + id.ToString()))
          {
            int int32_4 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
            if (int32_4 >= 0 && !this.ExportMetadataPrim(3, (object) int32_4, ExportDirection.Down, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_160"), id)))
            {
              this.log.WriteString("Error_056", $"{string.Format(format, (object) 3, (object) int32_4)} {logComment}");
              return false;
            }
          }
          break;
        }
        break;
      case 16 /*0x10*/:
        if (!this.ExportMetadataPrim(11, (object) Convert.ToString(row["F_AREA_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_179"), id)))
        {
          this.log.WriteString("Error_082", $"{string.Format(format, (object) 11, (object) Convert.ToString(row["F_AREA_ID"]))} {logComment}");
          return false;
        }
        foreach (DataRow dataRow in this.metadataDataSet.Tables["IMS_LC_STEPS"].Select($"F_SCHEMA_ID={id.ToString()} and F_DELETED=0"))
        {
          if (!this.ExportMetadataPrim(7, (object) Convert.ToInt32(dataRow["F_LC_STEP"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_180"), id)))
          {
            this.log.WriteString("Error_083", $"{string.Format(format, (object) 7, (object) Convert.ToInt32(dataRow["F_LC_STEP"]))} {logComment}");
            return false;
          }
          if (!this.ExportMetadataPrim(8, (object) Convert.ToInt32(dataRow["F_LEVEL_ID"]), ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_181"), id)))
          {
            this.log.WriteString("Error_084", $"{string.Format(format, (object) 8, (object) Convert.ToInt32(dataRow["F_LEVEL_ID"]))} {logComment}");
            return false;
          }
        }
        break;
    }
    this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_MetadataPrim_End"), (object) Consts.GetCategoryName(category), id), LogFlags.INFO | LogFlags.DATE);
    return true;
  }

  private bool ExportSecurityPrim(IDBSecurity security, int category, object id)
  {
    return this.ExportSecurityPrim(security, category, id, (object) null, "");
  }

  private bool ExportSecurityPrim(
    IDBSecurity security,
    int category,
    object id,
    object contextObject,
    string logComment)
  {
    if (security == null)
    {
      this.log.WriteString("Error_012", LocalizationHolder.rm.GetString("Intermech.Briefcase_66"), LogFlags.DATE);
      return false;
    }
    DataRow[] accessList;
    try
    {
      accessList = this.accessListHolder.GetAccessList(this.session, security, category, id, contextObject);
    }
    catch (Exception ex)
    {
      this.lastException = ex;
      this.log.WriteString("Error_013", $"{ex.Message}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
      return false;
    }
    if (accessList != null)
    {
      if (accessList.Length != 0)
        this.log.WriteString(string.Format($"{LocalizationHolder.rm.GetString("Intermech.Briefcase_182")} {logComment}", (object) Consts.GetCategoryName(category), id), LogFlags.INFO | LogFlags.DATE);
      foreach (DataRow dataRow in accessList)
      {
        int int32_1 = Convert.ToInt32(dataRow["F_CATEGORY_TYPE"]);
        if (int32_1 == 2)
        {
          this.log.WriteString("Error_014", LocalizationHolder.rm.GetString("Intermech.Briefcase_67"), LogFlags.DATE);
          return false;
        }
        object guid1 = (object) null;
        long int64_1 = Convert.ToInt64(dataRow["F_USER_ID"]);
        if (!this.GetObjVerGuid(int64_1, out guid1, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre001") + $" userid='{int64_1}'"))
        {
          this.log.WriteString("Error_015", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_68"), (object) int64_1), LogFlags.DATE);
          return false;
        }
        if (!this.PutObjectVersionOnQueue(int64_1, LocalizationHolder.rm.GetString("Intermech.Briefcase_69")))
        {
          this.log.WriteString("Error_016", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) int64_1), LogFlags.DATE);
          return false;
        }
        object guid2 = (object) null;
        long int64_2 = Convert.ToInt64(dataRow["F_OWNER_ID"]);
        if (!this.GetObjVerGuid(int64_2, out guid2, $"{LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre001")}  ownerid={(object) int64_2}"))
        {
          this.log.WriteString("Error_017", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_68"), (object) int64_2), LogFlags.DATE);
          return false;
        }
        if (int64_2 != 0L && !this.PutObjectVersionOnQueue(int64_2, LocalizationHolder.rm.GetString("Intermech.Briefcase_70")))
        {
          this.log.WriteString("Error_018", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) int64_2), LogFlags.DATE);
          return false;
        }
        SecurityRecord sr;
        ref SecurityRecord local = ref sr;
        long int64_3 = Convert.ToInt64(dataRow["F_CATEGORY_ID"]);
        int categoryType = int32_1;
        int int32_2 = Convert.ToInt32(dataRow["F_RIGHT_ID"]);
        object userId = guid1;
        int int32_3 = Convert.ToInt32(dataRow["F_RIGHT_TYPE"]);
        object ownerId = guid2;
        DateTime dateTime;
        object universalTime1;
        if (dataRow["F_BEGIN_DATE"] == DBNull.Value)
        {
          universalTime1 = dataRow["F_BEGIN_DATE"];
        }
        else
        {
          dateTime = Convert.ToDateTime(dataRow["F_BEGIN_DATE"]);
          universalTime1 = (object) dateTime.ToUniversalTime();
        }
        object universalTime2;
        if (dataRow["F_END_DATE"] == DBNull.Value)
        {
          universalTime2 = dataRow["F_END_DATE"];
        }
        else
        {
          dateTime = Convert.ToDateTime(dataRow["F_END_DATE"]);
          universalTime2 = (object) dateTime.ToUniversalTime();
        }
        local = new SecurityRecord(int64_3, categoryType, int32_2, userId, int32_3, ownerId, universalTime1, universalTime2);
        BriefcaseProcs.WriteToSecurityXml(int32_1 != 1 ? this.briefcaseMetadataSecurityXML : this.briefcaseObjSecurityXML, sr);
      }
      if (accessList.Length != 0)
        this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_183"), (object) Consts.GetCategoryName(category), id), LogFlags.INFO | LogFlags.DATE);
    }
    return true;
  }

  private bool ExportSecurity(int category, object id)
  {
    IDBSecurity security = (IDBSecurity) null;
    switch (category)
    {
      case 1:
        security = this.session.GetObject(Convert.ToInt64(id)) as IDBSecurity;
        break;
      case 3:
        security = this.session.GetAttributeType(Convert.ToInt32(id)) as IDBSecurity;
        break;
      case 4:
        security = this.session.GetObjectType(Convert.ToInt32(id)) as IDBSecurity;
        break;
      case 6:
        security = this.session.GetRelationType(Convert.ToInt32(id)) as IDBSecurity;
        break;
      case 7:
        security = this.session.GetLifecycleStep(Convert.ToInt32(id)) as IDBSecurity;
        break;
      case 8:
        security = this.session.GetLifecycleLevel(Convert.ToInt32(id)) as IDBSecurity;
        break;
      case 9:
        security = this.session.GetLanguageCollection() as IDBSecurity;
        break;
      case 11:
        security = this.session.GetSubjectAreaCollection() as IDBSecurity;
        break;
      case 12:
        security = this.session.GetAttributesGroup(Convert.ToInt32(id)) as IDBSecurity;
        break;
      case 16 /*0x10*/:
        security = this.session.GetLCSchema(Convert.ToInt32(id)) as IDBSecurity;
        break;
    }
    bool flag = false;
    if (security != null)
      flag = this.ExportSecurityPrim(security, category, id);
    return flag;
  }

  private bool PutObjectVersionOnQueue(long id, string logComment)
  {
    if (this.exportHolder.GetExternalId(1, (object) id) == null && this.exportQueueHolder.GetExternalId(1, (object) id) == null)
    {
      object guid;
      if (this.GetObjVerGuid(id, out guid, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre002") + $" versionid='{id}'") && guid != null)
      {
        string empty = string.Empty;
        string s;
        if (this.briefcaseExportStructure.ExportProperties.ExpandedLog)
        {
          string str = string.Empty;
          try
          {
            QuickObjectInfo objectInfo = this.session.GetObjectInfo(id);
            if (!objectInfo.Empty)
              str = objectInfo.Caption;
          }
          catch
          {
          }
          s = string.Format($"{LocalizationHolder.rm.GetString("Intermech.Briefcase_QueueAdded")} {logComment}", (object) id, (object) str);
        }
        else
          s = string.Format($"{LocalizationHolder.rm.GetString("Intermech.Briefcase_71")} {logComment}", (object) id);
        this.log.WriteString(s, LogFlags.INFO | LogFlags.DATE);
        this.exportQueueHolder.AssignExternalId(1, (object) id, guid);
      }
      else if (id != 0L)
        return false;
    }
    return true;
  }

  private ArrayList GetRelApplicabilityList(IDBObject iDBObject)
  {
    ArrayList applicabilityList = (ArrayList) this.relTypesApplCache[(object) iDBObject.ObjectType];
    if (applicabilityList == null)
    {
      applicabilityList = new ArrayList();
      DataTable applicabilitiesList = this.session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, iDBObject.ObjectType);
      if (applicabilitiesList != null && applicabilitiesList.Rows.Count > 0)
      {
        foreach (DataRow dataRow in applicabilitiesList.Select("", "F_RELATION_TYPE ASC"))
        {
          int int32 = Convert.ToInt32(dataRow["F_RELATION_TYPE"]);
          if (applicabilityList.Count == 0 || (int) applicabilityList[applicabilityList.Count - 1] != int32)
            applicabilityList.Add((object) int32);
        }
      }
      this.relTypesApplCache[(object) iDBObject.ObjectType] = (object) applicabilityList;
    }
    return applicabilityList;
  }

  private bool ExpandObjectVersions()
  {
    if (!this.FlushObjectsQueue())
      return false;
    double num1 = (double) (48 /*0x30*/ / this.briefcaseExportStructure.ExportProperties.ExportAttributes.Count);
    double num2 = 50.0;
    for (int index1 = 0; index1 < this.briefcaseExportStructure.ExportProperties.ExportAttributes.Count; ++index1)
    {
      ExportAttribute exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
      if (exportAttribute.Category == 1)
      {
        exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
        if (exportAttribute.Identifiers == null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) this.session.ObjectsSelect(-1, new DBRecordSetParams(new ConditionStructure[0], new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          })).Rows)
          {
            IDBObject iDBObject = this.session.GetObject(Convert.ToInt64(row["F_OBJECT_ID"]));
            if (iDBObject == null || !this.ExportObjectVerPrim(iDBObject, false))
              return false;
            num2 += num1;
            this.bep.Percent = Convert.ToInt32(num2);
            this.bep.Operation = ExportOperationType.Exporting;
            this.SetExportProgress(this.briefcaseGuid, this.bep);
          }
        }
        else
        {
          int index2 = 0;
          while (true)
          {
            int num3 = index2;
            exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
            int length = exportAttribute.Identifiers.Length;
            if (num3 < length)
            {
              exportAttribute = this.briefcaseExportStructure.ExportProperties.ExportAttributes[index1];
              long int64 = Convert.ToInt64(exportAttribute.Identifiers[index2]);
              if (this.IsAllObjectVersionExportRuleActive())
              {
                IDBObject dbObject = this.session.GetObject(int64);
                if (dbObject != null)
                {
                  long id = dbObject.ID;
                  List<long> objectVersions = this.session.GetObjectVersions(id);
                  for (int index3 = 0; index3 < objectVersions.Count; ++index3)
                    this.PutObjectVersionOnQueue(objectVersions[index3], string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_ExpandVersions"), (object) int64, (object) id));
                }
              }
              else
                this.PutObjectVersionOnQueue(int64, LocalizationHolder.rm.GetString("Intermech.Briefcase_86"));
              ++index2;
            }
            else
              break;
          }
          if (!this.FlushObjectsQueue())
            return false;
        }
      }
    }
    return this.FlushObjectsQueue();
  }

  private bool ExportObjectVerPrim(IDBObject iDBObject, bool inFlushQueue)
  {
    long objectId1 = iDBObject.ObjectID;
    using (new RemoteLock((object) iDBObject))
    {
      if (objectId1 >= 0L)
      {
        if (this.exportHolder.GetExternalId(1, (object) objectId1) != null)
          return true;
        this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_72"), (object) iDBObject.ObjectID, (object) iDBObject.ObjectGUID, (object) iDBObject.ID, (object) iDBObject.GUID), LogFlags.INFO | LogFlags.DATE);
        if (!this.MarkObjectVersionAsProcessed(iDBObject))
          return false;
        object guid1 = (object) null;
        if (!this.GetObjVerGuid(iDBObject.OwnerID, out guid1, $"{LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre001")} ownerid={(object) iDBObject.OwnerID}"))
          return false;
        this.PutObjectVersionOnQueue(iDBObject.OwnerID, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_73"), (object) objectId1));
        object guid2 = (object) null;
        if (iDBObject.ProjectID > 0L)
        {
          if (!this.GetObjVerGuid(iDBObject.ProjectID, out guid2, $"{LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre001")} projectid={(object) iDBObject.ProjectID}"))
            return false;
          this.PutObjectVersionOnQueue(iDBObject.ProjectID, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_74"), (object) objectId1));
        }
        if (iDBObject.CreatorID > 0L)
          this.PutObjectVersionOnQueue(iDBObject.CreatorID, $"создатель для версии объекта '{objectId1}'");
        if (!this.ExportMetadataPrim(7, (object) iDBObject.LCStep, ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_184"), (object) objectId1)) || !this.ExportMetadataPrim(4, (object) iDBObject.ObjectType, ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_185"), (object) objectId1)) || !this.ExportMetadataPrim(8, (object) (iDBObject as IDBLifecycleLevel).LevelID, ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_186"), (object) objectId1)))
          return false;
        long parentVersionId = this.IsAllObjectVersionExportRuleActive() ? iDBObject.ParentVersionID : -1L;
        BriefcaseProcs.WriteToObjectsXml(this.briefcaseObjectsXML, new ObjectRecord(iDBObject.ObjectID, (object) iDBObject.ObjectGUID, iDBObject.ID, (object) iDBObject.GUID, iDBObject.LCStep, iDBObject.VersionID, parentVersionId, 0L, (object) null, iDBObject.ObjectVerType, iDBObject.ObjectType, iDBObject.OwnerID, guid1, iDBObject.CreateDate.ToUniversalTime(), (iDBObject as IDBLifecycleLevel).LevelID, iDBObject.CreateDate.ToUniversalTime(), iDBObject.Caption, iDBObject.ProjectID, guid2, iDBObject.AccessLevel, iDBObject.IsBaseVersion, iDBObject.SiteID, iDBObject.ModificationID, (object) null, iDBObject.CreatorID));
        if (this.briefcaseExportStructure.ExportProperties.ExportSecurity && !this.ExportSecurityPrim(iDBObject as IDBSecurity, 1, (object) objectId1))
        {
          this.log.WriteString("Error_019", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_75"), (object) objectId1), LogFlags.DATE);
          return false;
        }
        using (DataTable lcHistory = iDBObject.GetLCHistory())
        {
          if (lcHistory != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) lcHistory.Rows)
            {
              int int32 = Convert.ToInt32(row["F_LC_STEP"]);
              IDBLifecycleStep lifecycleStep = this.session.GetLifecycleStep(int32, false);
              if (lifecycleStep != null && !lifecycleStep.IsDeleted)
                BriefcaseProcs.WriteToObjLCStepsXml(this.briefcaseObjLCStepsXML, new LCStepRecord(iDBObject.ObjectID, int32, Convert.ToDateTime(row["F_START_DATE"])));
            }
          }
        }
        if (MetaDataHelper.IsObjectTypeEditingContext(iDBObject.ObjectType))
        {
          long modificationID = 0;
          List<long> objectIDs = (List<long>) null;
          if (ContextHelper.GetContextContents(this.session as UserSession, iDBObject.ObjectID, out modificationID, out objectIDs) && objectIDs != null && objectIDs.Count > 0)
            BriefcaseProcs.WriteToContextsXml(this.briefcaseContextsXML, new ContextRecord(iDBObject.ObjectID, modificationID, objectIDs));
        }
        ArrayList arrayList1 = new ArrayList((ICollection) (this.iServerBriefcase as ICategoryExportManager).GetRegisteredCategoryExport(1));
        for (int index1 = 0; index1 < arrayList1.Count; ++index1)
        {
          long[] linkedObjectVersions;
          try
          {
            linkedObjectVersions = ((ICategoryExport) arrayList1[index1]).GetLinkedObjectVersions(this.session, 1, (object) objectId1);
          }
          catch (Exception ex)
          {
            this.log.WriteString("Error_085", string.Format(LocalizationHolder.rm.GetString("BriefcaseExceptionFromSubscriber"), (object) 1, (object) objectId1, (object) ((ICategoryExport) arrayList1[index1]).ExporterName), LogFlags.DATE);
            this.lastException = ex;
            return false;
          }
          if (linkedObjectVersions != null)
          {
            for (int index2 = 0; index2 < linkedObjectVersions.Length; ++index2)
            {
              if (!this.PutObjectVersionOnQueue(linkedObjectVersions[index2], BriefcaseExporter.logFromSubscriberString))
                return false;
            }
          }
        }
        using (DataTable attributesDataTable = iDBObject.Attributes.GetAttributesDataTable())
        {
          if (attributesDataTable.ExtendedProperties[(object) "Hidden"] != null)
            this.log.WriteString("Warning_001", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_76"), (object) iDBObject.ObjectID, (object) iDBObject.ObjectGUID), LogFlags.DATE);
          foreach (DataRow row in (InternalDataCollectionBase) attributesDataTable.Rows)
          {
            object rec = (object) null;
            this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_78pre"), (object) iDBObject.ObjectID, (object) iDBObject.ObjectGUID, (object) Convert.ToInt32(row["F_ATTRIBUTE_ID"])), LogFlags.INFO | LogFlags.DATE);
            long objectId2 = iDBObject.ObjectID;
            Guid objectGuid = iDBObject.ObjectGUID;
            if (!this.ExportAttribute4Attributable(AttributableElements.Object, (object) iDBObject, row, ref rec))
            {
              this.log.WriteString("Error_020", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_77"), (object) objectId2, (object) objectGuid, (object) Convert.ToString(row["F_ATTRIBUTE_ID"])), LogFlags.DATE);
              return false;
            }
            string empty = string.Empty;
            string s;
            if (this.briefcaseExportStructure.ExportProperties.ExpandedLog)
            {
              string str = string.Empty;
              try
              {
                IDBAttributeType attributeType = this.session.GetAttributeType(((AttributeRecord) rec).AttributeId);
                if (attributeType != null)
                  str = attributeType.Name;
              }
              catch
              {
              }
              s = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_AttributeAdded"), (object) iDBObject.ObjectID, (object) iDBObject.ObjectGUID, (object) ((AttributeRecord) rec).AttributeId, (object) str);
            }
            else
              s = string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_78"), (object) iDBObject.ObjectID, (object) iDBObject.ObjectGUID, (object) ((AttributeRecord) rec).AttributeId);
            this.log.WriteString(s, LogFlags.INFO | LogFlags.DATE);
            BriefcaseProcs.WriteToObjAttributesXml(this.briefcaseObjAttributesXML, (AttributeRecord) rec);
          }
        }
        ArrayList applicabilityList = this.GetRelApplicabilityList(iDBObject);
        if (applicabilityList.Count > 0)
        {
          ArrayList arrayList2;
          if (this.briefcaseExportStructure.ExportProperties.ExportRelationTypes == null)
          {
            arrayList2 = applicabilityList;
          }
          else
          {
            arrayList2 = new ArrayList();
            for (int index = 0; index < this.briefcaseExportStructure.ExportProperties.ExportRelationTypes.Count; ++index)
            {
              int exportRelationType = (int) this.briefcaseExportStructure.ExportProperties.ExportRelationTypes[index];
              if (applicabilityList.IndexOf((object) exportRelationType) != -1)
                arrayList2.Add((object) exportRelationType);
            }
          }
          for (int index = 0; index < arrayList2.Count; ++index)
          {
            if (!this.ExportMetadataPrim(6, (object) (int) arrayList2[index], ExportDirection.Up, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_187"), (object) objectId1)))
              return false;
            IDBRelationCollection relationCollection = this.session.GetRelationCollection((int) arrayList2[index]);
            if (relationCollection != null)
            {
              relationCollection.LocalTypesMode = true;
              VersionsRule rule = (VersionsRule) null;
              switch (this.ExportVersionsRuleIdActive(this.session, out rule))
              {
                case Guid _:
                  relationCollection.FiltrationOwnerID = this.exportVersionsRuleIdSource;
                  break;
                case long _:
                  relationCollection.FiltrationRule = rule;
                  break;
              }
              DBRecordSetParams paramsSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
              {
                (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
                (object) ObligatoryObjectAttributes.F_OBJECT_ID
              }, new object[1]
              {
                (object) ObligatoryObjectAttributes.F_PRJLINK_ID
              }, new SortOrders[1]{ SortOrders.ASC });
              FiltrationHelper.BlockPluginFiltrations(ref paramsSet, (HybridDictionary) null);
              long num = -1;
              foreach (DataRow row1 in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramsSet, objectId1).Rows)
              {
                long int64_1 = Convert.ToInt64(row1[0]);
                long int64_2 = Convert.ToInt64(row1[1]);
                if (num == -1L || num != int64_1)
                {
                  num = int64_1;
                  IDBRelation relation;
                  try
                  {
                    relation = this.session.GetRelation(num);
                    if (relation == null)
                      return false;
                  }
                  catch (Exception ex)
                  {
                    this.lastException = ex;
                    return false;
                  }
                  object guid3 = (object) null;
                  if (!this.GetObjGuid(relation.PartID, int64_2, out guid3) || guid3 == null)
                    return false;
                  if (relation.CreatorID > 0L)
                    this.PutObjectVersionOnQueue(relation.CreatorID, $"создатель для связи '{(relation as IDBGuid).GUID}'");
                  object createDate = relation.CreateDate == DateTime.MinValue ? (object) null : (object) relation.CreateDate;
                  RelationRecord rr = new RelationRecord(num, (object) (relation as IDBGuid).GUID, (object) iDBObject.ObjectGUID, guid3, Convert.ToInt32(relation.RelationType), createDate, relation.CreatorID);
                  this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_79"), (object) num, (object) iDBObject.ObjectGUID, guid3, (object) 0), LogFlags.INFO | LogFlags.DATE);
                  BriefcaseProcs.WriteToRelationsXml(this.briefcaseRelationsXML, rr);
                  using (DataTable attributesDataTable = relation.Attributes.GetAttributesDataTable())
                  {
                    if (attributesDataTable.ExtendedProperties[(object) "Hidden"] != null)
                      this.log.WriteString("Warning_002", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_80"), (object) relation.RelationID, (object) relation.PartID, (object) relation.ProjID), LogFlags.DATE);
                    foreach (DataRow row2 in (InternalDataCollectionBase) attributesDataTable.Rows)
                    {
                      object rec = (object) null;
                      this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_82pre"), (object) relation.RelationID, (object) relation.PartID, (object) relation.ProjID, (object) Convert.ToInt32(row2["F_ATTRIBUTE_ID"])), LogFlags.INFO | LogFlags.DATE);
                      long relationId = relation.RelationID;
                      long partId = relation.PartID;
                      long projId = relation.ProjID;
                      if (!this.ExportAttribute4Attributable(AttributableElements.Relation, (object) relation, row2, ref rec))
                      {
                        this.log.WriteString("Error_021", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_81"), (object) relationId, (object) partId, (object) projId, (object) ((AttributeRecord) rec).AttributeId), LogFlags.DATE);
                        return false;
                      }
                      this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_82"), (object) relation.RelationID, (object) relation.PartID, (object) relation.ProjID, (object) ((AttributeRecord) rec).AttributeId), LogFlags.INFO | LogFlags.DATE);
                      BriefcaseProcs.WriteToRelAttributesXml(this.briefcaseRelAttributesXML, (AttributeRecord) rec);
                    }
                  }
                }
                if (!this.PutObjectVersionOnQueue(int64_2, string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_83"), (object) objectId1)))
                  return false;
              }
            }
          }
        }
        this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_85"), (object) iDBObject.ObjectID, (object) iDBObject.ObjectGUID, (object) iDBObject.ID, (object) iDBObject.GUID), LogFlags.INFO | LogFlags.DATE);
      }
      else
      {
        this.log.WriteString(string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_WorkCopyNotExported"), (object) -iDBObject.ObjectID, (object) iDBObject.ObjectGUID, (object) iDBObject.ID, (object) iDBObject.GUID), LogFlags.INFO | LogFlags.DATE);
        this.PutObjectVersionOnQueue(-iDBObject.ObjectID, LocalizationHolder.rm.GetString("Intermech.Briefcase_84"));
      }
    }
    return inFlushQueue || this.FlushObjectsQueue();
  }

  public bool FlushObjectsQueue()
  {
    long num = 0;
    while (this.exportQueueHolder[1].Count > 0)
    {
      IEnumerator enumerator = this.exportQueueHolder[1].Keys.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
          num = (long) enumerator.Current;
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
      IDBObject iDBObject = this.session.GetObject(num);
      using (new RemoteLock((object) iDBObject))
      {
        if (num < 0L)
        {
          if (!this.MarkObjectVersionAsProcessed(iDBObject))
            return false;
          this.PutObjectVersionOnQueue(-num, LocalizationHolder.rm.GetString("Intermech.Briefcase_84"));
        }
        else if (this.exportHolder.GetExternalId(1, (object) num) != null)
        {
          if (!this.MarkObjectVersionAsProcessed(iDBObject))
            return false;
        }
        else
        {
          if (iDBObject != null)
          {
            long objectId = iDBObject.ObjectID;
          }
          if (iDBObject != null)
          {
            if (this.ExportObjectVerPrim(iDBObject, true))
              continue;
          }
          return false;
        }
      }
    }
    return true;
  }

  private bool MarkObjectVersionAsProcessed(IDBObject iDBObject)
  {
    long objectId = iDBObject.ObjectID;
    object externalId = this.exportQueueHolder.GetExternalId(1, (object) objectId);
    if (externalId != null)
    {
      this.exportQueueHolder.RemoveExternalId(1, (object) objectId);
    }
    else
    {
      externalId = (object) iDBObject.ObjectGUID;
      this.objectVerCache[(object) iDBObject.ObjectID] = externalId;
    }
    this.exportHolder.AssignExternalId(1, (object) objectId, externalId);
    return true;
  }

  private bool ExportRelationPrim(IDBRelation iDBRelation) => true;

  private bool ExportAttribute4Attributable(
    AttributableElements kind,
    object interf,
    DataRow dr,
    ref object rec)
  {
    IDBAttribute aIDBAttribute = (IDBAttribute) null;
    try
    {
      if (kind != AttributableElements.Object && kind != AttributableElements.Relation)
        return false;
      bool flag1 = kind == AttributableElements.Object;
      int int32_1 = Convert.ToInt32(dr["F_ATTRIBUTE_ID"]);
      long num = flag1 ? Convert.ToInt64(dr["F_OBJECT_ID"]) : Convert.ToInt64(dr["F_PRJLINK_ID"]);
      int int32_2 = Convert.ToInt32(dr["F_INLIST_ID"]);
      if (!this.ExportMetadataPrim(3, (object) int32_1, ExportDirection.Up, flag1 ? LocalizationHolder.rm.GetString("Intermech.Briefcase_188") : LocalizationHolder.rm.GetString("Intermech.Briefcase_189")))
        return false;
      object attrValueCurrent1 = (object) null;
      if (dr["F_INTEGER_VALUE"] != DBNull.Value)
        attrValueCurrent1 = (object) Convert.ToInt64(dr["F_INTEGER_VALUE"]);
      object attrValueCurrent2 = (object) null;
      if (dr["F_DOUBLE_VALUE"] != DBNull.Value)
        attrValueCurrent2 = (object) Convert.ToDouble(dr["F_DOUBLE_VALUE"]);
      object attrValueCurrent3 = (object) null;
      if (dr["F_STRING_VALUE"] != DBNull.Value)
        attrValueCurrent3 = (object) Convert.ToString(dr["F_STRING_VALUE"]);
      object attrValueCurrent4 = (object) null;
      if (dr["F_DATE_VALUE"] != DBNull.Value)
        attrValueCurrent4 = (object) Convert.ToDateTime(dr["F_DATE_VALUE"]);
      object integerGuid = (object) null;
      object doubleGuid = (object) null;
      object fileSize = (object) null;
      object arcMethod = (object) null;
      object fileNote = (object) null;
      object fileType = (object) null;
      object fileAuthor = (object) null;
      DataRow[] dataRowArray = this.metadataDataSet.Tables["IMS_ATTRIBUTES"].Select("F_ATTRIBUTE_ID=" + int32_1.ToString());
      if (dataRowArray == null || dataRowArray.Length == 0)
        return false;
      FieldTypes int32_3 = (FieldTypes) Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_TYPE"]);
      ArrayList arrayList = new ArrayList((ICollection) (this.iServerBriefcase as ICategoryExportManager).GetRegisteredCategoryExport(3));
      switch (int32_3)
      {
        case FieldTypes.ftString:
        case FieldTypes.ftGuid:
          object attrValueOriginal1 = attrValueCurrent3;
          for (int index = 0; index < arrayList.Count; ++index)
          {
            if (!this.ProcessExportAttributes(((ICategoryExport) arrayList[index]).GetLinkedDataByAttribute(this.session, kind, num, interf as IDBAttributable, int32_1, attrValueOriginal1, ref attrValueCurrent3), BriefcaseExporter.logFromSubscriberString))
              return false;
          }
          break;
        case FieldTypes.ftInteger:
        case FieldTypes.ftBoolean:
          object attrValueOriginal2 = attrValueCurrent1;
          for (int index = 0; index < arrayList.Count; ++index)
          {
            if (!this.ProcessExportAttributes(((ICategoryExport) arrayList[index]).GetLinkedDataByAttribute(this.session, kind, num, interf as IDBAttributable, int32_1, attrValueOriginal2, ref attrValueCurrent1), BriefcaseExporter.logFromSubscriberString))
              return false;
          }
          break;
        case FieldTypes.ftDouble:
          object attrValueOriginal3 = attrValueCurrent2;
          for (int index = 0; index < arrayList.Count; ++index)
          {
            if (!this.ProcessExportAttributes(((ICategoryExport) arrayList[index]).GetLinkedDataByAttribute(this.session, kind, num, interf as IDBAttributable, int32_1, attrValueOriginal3, ref attrValueCurrent2), BriefcaseExporter.logFromSubscriberString))
              return false;
          }
          break;
        case FieldTypes.ftDateTime:
          object attrValueOriginal4 = attrValueCurrent4;
          for (int index = 0; index < arrayList.Count; ++index)
          {
            if (!this.ProcessExportAttributes(((ICategoryExport) arrayList[index]).GetLinkedDataByAttribute(this.session, kind, num, interf as IDBAttributable, int32_1, attrValueOriginal4, ref attrValueCurrent4), BriefcaseExporter.logFromSubscriberString))
              return false;
          }
          break;
        case FieldTypes.ftShortBlob:
        case FieldTypes.ftFile:
        case FieldTypes.ftMemo:
        case FieldTypes.ftBlob:
          string folderByFieldType = this.GetDataFolderByFieldType(int32_3);
          if (folderByFieldType == string.Empty)
            return false;
          if (attrValueCurrent1 != null)
          {
            if (aIDBAttribute == null)
              aIDBAttribute = (interf as IDBAttributable).GetAttributeByID(int32_1);
            if (aIDBAttribute == null)
              return false;
            using (new RemoteLock((object) aIDBAttribute))
            {
              aIDBAttribute.Index = int32_2;
              if (int32_3 != FieldTypes.ftMemo)
              {
                string blobFileName = BriefcaseBlobs.GetBlobFileName(num, int32_1, (long) attrValueCurrent1, folderByFieldType, false);
                if (blobFileName == string.Empty)
                  return false;
                bool flag2 = false;
                if (int32_3 == FieldTypes.ftShortBlob)
                {
                  for (int index = 0; index < arrayList.Count; ++index)
                    flag2 = flag2 || ((ICategoryExport) arrayList[index]).ProcessShortBlobs;
                }
                if (int32_3 == FieldTypes.ftShortBlob & flag2)
                {
                  MemoryStream memoryStream = new MemoryStream();
                  try
                  {
                    BlobProcReader blobProcReader = new BlobProcReader(aIDBAttribute, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                    blobProcReader.ReadData(this.session);
                    for (int index = 0; index < arrayList.Count; ++index)
                    {
                      if (((ICategoryExport) arrayList[index]).ProcessShortBlobs)
                      {
                        object attrValueCurrent5 = (object) memoryStream;
                        ExportAttribute[] linkedDataByAttribute = ((ICategoryExport) arrayList[index]).GetLinkedDataByAttribute(this.session, kind, num, interf as IDBAttributable, int32_1, (object) memoryStream, ref attrValueCurrent5);
                        if (attrValueCurrent5 is MemoryStream && !memoryStream.Equals(attrValueCurrent5))
                        {
                          memoryStream.Close();
                          memoryStream = attrValueCurrent5 as MemoryStream;
                        }
                        if (!this.ProcessExportAttributes(linkedDataByAttribute, BriefcaseExporter.logFromSubscriberString))
                          return false;
                      }
                    }
                    BlobProcWriter blobProcWriter = new BlobProcWriter(aIDBAttribute, 0, blobProcReader.BlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                    blobProcWriter.WriteData(this.session, true);
                    Exception exception = (Exception) null;
                    if (blobProcWriter.VirtualStream != null)
                    {
                      try
                      {
                        blobProcWriter.VirtualStream.Position = 0L;
                        byte[] numArray = new byte[blobProcWriter.VirtualStream.Length];
                        blobProcWriter.VirtualStream.Read(numArray, 0, Convert.ToInt32(blobProcWriter.VirtualStream.Length));
                        if (!BriefcaseBlobs.WriteBlob(blobFileName, folderByFieldType, numArray, out exception))
                        {
                          this.log.WriteString("Error_040", LocalizationHolder.rm.GetString("Intermech.Briefcase_87") + (object) exception != null ? exception.Message : string.Empty);
                          return false;
                        }
                      }
                      finally
                      {
                        blobProcWriter.VirtualStream.Close();
                      }
                    }
                    fileSize = (object) blobProcWriter.BlobInformation.RealFileSize;
                    arcMethod = (object) (int) blobProcWriter.BlobInformation.ArcMethod;
                    fileNote = (object) blobProcWriter.BlobInformation.Note;
                    break;
                  }
                  finally
                  {
                    memoryStream?.Close();
                  }
                }
                else
                {
                  if (!(aIDBAttribute is IBlobReader br))
                    return false;
                  Exception exception = (Exception) null;
                  BlobInformation bi;
                  if (!BriefcaseBlobs.WriteBlob(blobFileName, folderByFieldType, br, this.dataBlockSize, out bi, out exception))
                  {
                    this.log.WriteString("Error_022", LocalizationHolder.rm.GetString("Intermech.Briefcase_88") + (object) exception != null ? exception.Message : string.Empty);
                    return false;
                  }
                  fileSize = (object) bi.RealFileSize;
                  arcMethod = (object) (int) bi.ArcMethod;
                  fileNote = (object) bi.Note;
                  if (int32_3 != FieldTypes.ftShortBlob)
                  {
                    fileType = (object) bi.FileType;
                    fileAuthor = (object) bi.Author;
                    if (fileAuthor != null)
                    {
                      object guid;
                      fileAuthor = !this.GetObjVerGuid(Convert.ToInt64(fileAuthor), out guid, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre001")) || guid == null ? (object) null : (object) guid.ToString();
                      break;
                    }
                    break;
                  }
                  break;
                }
              }
              else
              {
                string memoFileName = BriefcaseBlobs.GetMemoFileName(num, int32_1, (long) attrValueCurrent1, folderByFieldType, false);
                if (memoFileName == string.Empty || !(aIDBAttribute is IMemoReader memoReader))
                  return false;
                object attrValueCurrent6 = (object) null;
                try
                {
                  if (memoReader.OpenMemo(0) > 0)
                  {
                    attrValueCurrent6 = (object) memoReader.ReadDataBlock();
                    memoReader.CloseMemo();
                  }
                }
                catch (Exception ex)
                {
                  this.lastException = ex;
                  return false;
                }
                char[] attrValueOriginal5 = attrValueCurrent6 != null ? (char[]) ((Array) attrValueCurrent6).Clone() : (char[]) null;
                for (int index = 0; index < arrayList.Count; ++index)
                {
                  if (!this.ProcessExportAttributes(((ICategoryExport) arrayList[index]).GetLinkedDataByAttribute(this.session, kind, num, interf as IDBAttributable, int32_1, (object) attrValueOriginal5, ref attrValueCurrent6), BriefcaseExporter.logFromSubscriberString))
                    return false;
                }
                Exception exception = (Exception) null;
                if (!BriefcaseBlobs.WriteMemo(memoFileName, folderByFieldType, (char[]) attrValueCurrent6, out exception))
                {
                  this.log.WriteString("Error_023", LocalizationHolder.rm.GetString("Intermech.Briefcase_89") + (object) exception != null ? exception.Message : string.Empty);
                  return false;
                }
                fileSize = (object) (attrValueCurrent6 != null ? ((char[]) attrValueCurrent6).Length : 0);
                arcMethod = (object) ArcMethods.NotPacked;
                fileNote = (object) null;
                break;
              }
            }
          }
          else
            break;
        case FieldTypes.ftExternalLink:
          if (attrValueCurrent2 != null && !this.PutObjectVersionOnQueue(Convert.ToInt64(attrValueCurrent2), LocalizationHolder.rm.GetString("Intermech.Briefcase_90")))
            return false;
          break;
        case FieldTypes.ftObjectLink:
        case FieldTypes.ftObjectLinkByID:
          if (attrValueCurrent1 != null)
          {
            long verId = (long) attrValueCurrent1;
            if (int32_3 == FieldTypes.ftObjectLinkByID && !this.GetBaseObjVerByID((long) attrValueCurrent1, out verId, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre003b") + $" ID='{(long) attrValueCurrent1}'"))
            {
              this.log.WriteString("Error_090", string.Format(LocalizationHolder.rm.GetString("BriefcaseVerIdbyIDError"), (object) ((long) attrValueCurrent1).ToString()), LogFlags.DATE);
              return false;
            }
            if (this.storageList.BinarySearch(verId) >= 0)
            {
              dr["F_INTEGER_VALUE"] = (object) DBNull.Value;
              dr["F_STRING_VALUE"] = (object) DBNull.Value;
              dr.AcceptChanges();
              attrValueCurrent1 = (object) null;
              attrValueCurrent3 = (object) null;
              break;
            }
            if (!this.PutObjectVersionOnQueue(verId, LocalizationHolder.rm.GetString("Intermech.Briefcase_91")))
              return false;
            break;
          }
          break;
        case FieldTypes.ftMeasured:
          if (attrValueCurrent1 != null)
          {
            long id = (long) attrValueCurrent1;
            if (id != 0L && !this.PutObjectVersionOnQueue(id, LocalizationHolder.rm.GetString("Intermech.Briefcase_92")))
              return false;
            if (attrValueCurrent3 != null)
            {
              MeasuredValue measuredValue;
              try
              {
                measuredValue = MeasureHelper.ConvertToMeasuredValue(attrValueCurrent3.ToString());
              }
              catch (FormatException ex)
              {
                measuredValue = (MeasuredValue) null;
                attrValueCurrent3 = (object) null;
              }
              if (measuredValue != null && measuredValue.MeasureID != id && !this.PutObjectVersionOnQueue(measuredValue.MeasureID, LocalizationHolder.rm.GetString("Intermech.Briefcase_93")))
                return false;
              break;
            }
            break;
          }
          break;
      }
      rec = (object) new AttributeRecord(int32_1, num, int32_2, attrValueCurrent1, integerGuid, attrValueCurrent2, doubleGuid, attrValueCurrent3, attrValueCurrent4, fileSize, arcMethod, fileNote, (string) null, fileType, fileAuthor);
    }
    catch (Exception ex)
    {
      this.lastException = ex;
      return false;
    }
    return true;
  }

  private bool ProcessExportAttributes(ExportAttribute[] ea, string logCommonComment)
  {
    if (ea == null || ea.Length == 0)
      return true;
    foreach (ExportAttribute exportAttribute in ea)
    {
      if (exportAttribute.Identifiers != null)
      {
        if (BriefcaseConsts.IsElementCategory(exportAttribute.Category))
        {
          for (int index = 0; index < exportAttribute.Identifiers.Length; ++index)
          {
            if (exportAttribute.Category == 1)
            {
              IDBObject dbObject;
              try
              {
                dbObject = this.session.GetObject((long) exportAttribute.Identifiers[index]);
              }
              catch (Exception ex)
              {
                this.lastException = ex;
                this.log.WriteString("Error_024", $"{ex.Message}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
                return false;
              }
              if (!this.PutObjectVersionOnQueue(dbObject.ObjectID, logCommonComment))
              {
                this.log.WriteString("Error_084", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_63"), (object) dbObject.ObjectID), LogFlags.DATE);
                return false;
              }
            }
            if (exportAttribute.Category == 5)
            {
              IDBRelation relation;
              try
              {
                relation = this.session.GetRelation((long) exportAttribute.Identifiers[index]);
              }
              catch (Exception ex)
              {
                this.lastException = ex;
                this.log.WriteString("Error_025", $"{ex.Message}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
                return false;
              }
              if (!this.ExportRelationPrim(relation))
                return false;
            }
          }
        }
        else if (BriefcaseConsts.IsMetadataCategory(exportAttribute.Category))
        {
          for (int index = 0; index < exportAttribute.Identifiers.Length; ++index)
          {
            if (!this.ExportMetadataPrim(exportAttribute.Category, exportAttribute.Identifiers[index], ExportDirection.Up, LocalizationHolder.rm.GetString("Intermech.Briefcase_190")))
              return false;
          }
        }
      }
    }
    return true;
  }

  private object ExportVersionsRuleIdActive(IUserSession session, out VersionsRule rule)
  {
    if (this.IsExportVersionsRuleIdActiveASSIGNED)
    {
      rule = this.IsExportVersionsRuleIdActiveRULE;
      return this.IsExportVersionsRuleIdActiveVALUE;
    }
    rule = (VersionsRule) null;
    this.IsExportVersionsRuleIdActiveVALUE = (object) null;
    this.IsExportVersionsRuleIdActiveRULE = (VersionsRule) null;
    if (this.exportVersionsRuleIdSource != null)
    {
      if (GuidHelper.IsGuid(this.exportVersionsRuleIdSource))
        this.IsExportVersionsRuleIdActiveVALUE = (object) new Guid(this.exportVersionsRuleIdSource);
      long result = -1;
      if (long.TryParse(this.exportVersionsRuleIdSource, out result))
      {
        IDBObject RuleObject = session.GetObject(result);
        VersionsRule versionsRule = (VersionsRule) null;
        if (RuleObject != null)
        {
          versionsRule = new VersionsRule();
          versionsRule.LoadFromObject(session, RuleObject);
        }
        this.IsExportVersionsRuleIdActiveVALUE = (object) result;
        this.IsExportVersionsRuleIdActiveRULE = versionsRule;
        rule = versionsRule;
      }
    }
    this.IsExportVersionsRuleIdActiveASSIGNED = true;
    return this.IsExportVersionsRuleIdActiveVALUE;
  }

  private bool IsAllObjectVersionExportRuleActive()
  {
    if (this.IsAllObjectVersionExportRuleActiveASSIGNED)
      return this.IsAllObjectVersionExportRuleActiveVALUE;
    this.IsAllObjectVersionExportRuleActiveVALUE = false;
    if (GuidHelper.IsGuid(this.exportVersionsRuleIdSource))
    {
      if (new Guid(this.exportVersionsRuleIdSource).Equals(new Guid("cad001e3-306c-11d8-b4e9-00304f19f545")))
        this.IsAllObjectVersionExportRuleActiveVALUE = true;
    }
    else
    {
      long result = -1;
      if (long.TryParse(this.exportVersionsRuleIdSource, out result))
      {
        if (this.exportVersionsRuleIdObject == -1L)
        {
          IDBObject dbObject = this.session.GetObject(new Guid("cad001e3-306c-11d8-b4e9-00304f19f545"), false);
          if (dbObject != null)
            this.exportVersionsRuleIdObject = dbObject.ObjectID;
        }
        if (this.exportVersionsRuleIdObject != -1L && this.exportVersionsRuleIdObject == result)
          this.IsAllObjectVersionExportRuleActiveVALUE = true;
      }
    }
    this.IsAllObjectVersionExportRuleActiveASSIGNED = true;
    return this.IsAllObjectVersionExportRuleActiveVALUE;
  }

  public string GetDataFolderByFieldType(FieldTypes ft)
  {
    string folderByFieldType = string.Empty;
    switch (ft)
    {
      case FieldTypes.ftShortBlob:
        folderByFieldType = this.ShortBlobBriefcaseFolder;
        break;
      case FieldTypes.ftFile:
      case FieldTypes.ftBlob:
        folderByFieldType = this.BlobBriefcaseFolder;
        break;
      case FieldTypes.ftMemo:
        folderByFieldType = this.MemoBriefcaseFolder;
        break;
    }
    return folderByFieldType;
  }

  private bool GetBaseObjVerByID(long id, out long verId, string logPreComment)
  {
    verId = 0L;
    if (id == 0L)
      return true;
    if (this.objectIDtoBaseVerCache.ContainsKey((object) id))
    {
      verId = (long) this.objectIDtoBaseVerCache[(object) id];
    }
    else
    {
      try
      {
        this.log.WriteString(logPreComment, LogFlags.DATE);
        IDBObject objectBaseVersionById = this.session.GetObjectBaseVersionByID(id, true);
        verId = objectBaseVersionById.ObjectID;
        this.objectIDtoBaseVerCache[(object) id] = (object) verId;
      }
      catch (Exception ex)
      {
        this.lastException = ex;
        this.log.WriteString("Error_087", $"{ex.Message}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
        return false;
      }
    }
    return true;
  }

  private bool GetObjVerGuid(long id, out object guid, string logPreComment)
  {
    guid = (object) null;
    if (id == 0L)
      return true;
    guid = this.objectVerCache[(object) id];
    if (guid == null)
    {
      IDBObject dbObject;
      try
      {
        this.log.WriteString(logPreComment, LogFlags.DATE);
        dbObject = this.session.GetObject(id);
      }
      catch (Exception ex)
      {
        this.lastException = ex;
        this.log.WriteString("Error_026", $"{ex.Message}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
        return false;
      }
      if (dbObject != null)
      {
        guid = (object) dbObject.ObjectGUID;
        if (guid != null)
        {
          this.objectVerCache[(object) id] = guid;
          this.objectCache[(object) dbObject.ID] = (object) dbObject.GUID;
        }
        else
        {
          this.log.WriteString("Error_027", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_94"), (object) id) + LocalizationHolder.rm.GetString("Intermech.Briefcase_95"), LogFlags.DATE);
          return false;
        }
      }
      else
      {
        this.log.WriteString("Error_028", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_96"), (object) id) + LocalizationHolder.rm.GetString("Intermech.Briefcase_97"), LogFlags.DATE);
        return false;
      }
    }
    return true;
  }

  private bool ConvertDefault4Types(DataSet ds, string tableName, DataRow dr)
  {
    FieldTypes int32 = (FieldTypes) Convert.ToInt32(dr["F_ATTRIBUTE_TYPE"]);
    if (int32 == FieldTypes.ftObjectLink || int32 == FieldTypes.ftObjectLink || int32 == FieldTypes.ftMeasured || int32 == FieldTypes.ftExternalLink)
    {
      DataRow[] dataRowArray = ds.Tables[tableName].Select("F_ATTRIBUTE_ID=" + Convert.ToString(dr["F_ATTRIBUTE_ID"]));
      if (dataRowArray != null)
      {
        foreach (DataRow dataRow in dataRowArray)
        {
          if (dataRow["F_DEFAULT_VALUE"].ToString() != string.Empty)
          {
            int num1 = -1;
            int num2 = -1;
            if (tableName == "IMS_ATTR4OBJ_TYPES")
              num1 = Convert.ToInt32(dataRow["F_OBJECT_TYPE"]);
            if (tableName == "IMS_ATTR4RELATION_TYPES")
              num2 = Convert.ToInt32(dataRow["F_RELATION_TYPE"]);
            bool flag = false;
            long id = 0;
            try
            {
              id = Convert.ToInt64(dataRow["F_DEFAULT_VALUE"]);
            }
            catch (FormatException ex)
            {
              if (Convert.ToString(dataRow["F_DEFAULT_VALUE"]) == Consts.CurrentUserFunction)
                flag = true;
              else
                throw;
            }
            if (!flag)
            {
              long verId = id;
              if (int32 == FieldTypes.ftObjectLinkByID && !this.GetBaseObjVerByID(id, out verId, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre005a") + $" ID='{id}'"))
              {
                this.log.WriteString("Error_091", string.Format(LocalizationHolder.rm.GetString("BriefcaseVerIdbyIDError"), (object) id.ToString()), LogFlags.DATE);
                return false;
              }
              if (this.storageList.BinarySearch(verId) >= 0)
              {
                dataRow["F_DEFAULT_VALUE"] = (object) DBNull.Value;
                dataRow.AcceptChanges();
              }
              else
              {
                object guid = (object) null;
                if (!this.GetObjVerGuid(verId, out guid, LocalizationHolder.rm.GetString("Intermech.Briefcase_Pre005") + $" versionid='{verId}'"))
                {
                  string str1 = "";
                  if (num1 != -1 || num2 != -1)
                  {
                    string str2 = "";
                    if (num1 != -1)
                    {
                      try
                      {
                        IDBObjectType objectType = this.session.GetObjectType(num1);
                        if (objectType != null)
                          str2 = objectType.ObjectTypeName;
                      }
                      catch (Exception ex)
                      {
                        this.lastException = ex;
                        this.log.WriteString("Error_035", $"{string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_105"), (object) num1)} [{ex.Message}]. StackTrace: {ex.StackTrace}", LogFlags.DATE);
                        return false;
                      }
                      str1 += string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_106"), (object) num1, (object) str2);
                    }
                    if (num2 != -1)
                    {
                      try
                      {
                        if (this.session.GetObjectType(num2) is IDBRelationType objectType)
                          str2 = objectType.Description;
                      }
                      catch (Exception ex)
                      {
                        this.lastException = ex;
                        this.log.WriteString("Error_036", $"{string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_107"), (object) num2)} [{ex.Message}]. StackTrace: {ex.StackTrace}", LogFlags.DATE);
                        return false;
                      }
                      str1 += string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_108"), (object) num2, (object) str2);
                    }
                  }
                  this.log.WriteString("Error_037", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_109") + str1 + LocalizationHolder.rm.GetString("Intermech.Briefcase_110"), (object) Convert.ToInt32(dr["F_ATTRIBUTE_ID"]).ToString(), (object) Convert.ToString(dr["F_NAME"]), (object) verId.ToString()), LogFlags.DATE);
                  return false;
                }
                if (guid != null)
                {
                  dataRow["F_DEFAULTGUID"] = (object) guid.ToString();
                  dataRow.AcceptChanges();
                  this.AddObjVerToUsability(Convert.ToInt32(dr["F_ATTRIBUTE_ID"]), num1, num2, 0, verId, guid, (string) null);
                }
              }
            }
          }
        }
      }
    }
    return true;
  }

  private bool GetObjGuid(long id, long anyVersionId, out object guid)
  {
    guid = (object) null;
    if (id == 0L)
      return true;
    guid = this.objectCache[(object) id];
    if (guid == null && anyVersionId != 0L)
    {
      IDBObject dbObject;
      try
      {
        dbObject = this.session.GetObject(anyVersionId);
      }
      catch (Exception ex)
      {
        this.lastException = ex;
        this.log.WriteString("Error_029", $"{ex.Message}. StackTrace: {ex.StackTrace}", LogFlags.DATE);
        return false;
      }
      if (dbObject != null)
      {
        this.objectVerCache[(object) anyVersionId] = (object) dbObject.ObjectGUID;
        guid = (object) dbObject.GUID;
        if (guid != null)
        {
          this.objectCache[(object) id] = guid;
        }
        else
        {
          this.log.WriteString("Error_030", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_98"), (object) id) + LocalizationHolder.rm.GetString("Intermech.Briefcase_95"), LogFlags.DATE);
          return false;
        }
      }
      else
      {
        this.log.WriteString("Error_031", string.Format(LocalizationHolder.rm.GetString("Intermech.Briefcase_96"), (object) anyVersionId) + LocalizationHolder.rm.GetString("Intermech.Briefcase_97"), LogFlags.DATE);
        return false;
      }
    }
    return true;
  }

  private void AddObjVerToUsability(
    int attrId,
    int objType,
    int relType,
    int inlist,
    long id,
    object guid,
    string custom)
  {
    this.objectVerUsability.Rows.Add((object) attrId, (object) objType, (object) relType, (object) inlist, (object) id, guid, (object) custom);
  }

  private void SetExportProgress(Guid briefcase, BriefcaseExportProgress bep)
  {
    if (this.SetExportProgressEvent == null)
      return;
    if (bep.Percent > 100)
      bep.Percent = 100;
    this.SetExportProgressEvent((object) this, briefcase, bep);
  }
}
