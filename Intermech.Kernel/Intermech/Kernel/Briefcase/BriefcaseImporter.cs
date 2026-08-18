// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class BriefcaseImporter : DBSessionable
{
  public SetImportProgressEventHandler SetImportProgressEvent;
  private Guid _briefcase;
  private BriefcaseImportStructure _bis;
  private readonly string _briefcasePath = string.Empty;
  private int _countObjects;
  private int _countRelations;
  private readonly ImportEventLog _eventLog;

  public BriefcaseImporter(
    UserSession userSession,
    Guid numOfBriefcase,
    BriefcaseImportStructure bis)
    : this(userSession, numOfBriefcase, string.Format(BriefcaseConsts.logImportName, (object) numOfBriefcase))
  {
    this._bis = bis;
    this._briefcasePath = this._bis.ImportProperties.Location.ComputerLocation == BriefcaseLocation.Computer.Server ? this._bis.ImportProperties.Location.Path : Path.Combine(this._bis.ImportProperties.ServerTempFolder, this._briefcase.ToString());
  }

  public BriefcaseImporter(UserSession userSession, string logFileName)
    : this(userSession, Guid.NewGuid(), logFileName)
  {
  }

  public BriefcaseImporter(UserSession userSession, Guid numOfBriefcase, string logFileName)
    : base(userSession)
  {
    this.InitSecurityOptions(14, 0L);
    this.CheckAccess(ActionType.Import, false, true);
    this._eventLog = new ImportEventLog(this.EventHelper, logFileName);
    this._briefcase = numOfBriefcase;
  }

  public void Importing()
  {
    new Thread(new ThreadStart(this.ImportData))
    {
      IsBackground = true
    }.Start();
  }

  private void ImportData()
  {
    string sessionName = $"Briefcase.ImportData_{Guid.NewGuid()}";
    UserSession session = (UserSession) this.UserSession.Clone(true, sessionName);
    try
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(this._briefcasePath);
      int briefcaseIndex = -1;
      if (this._bis.ImportProperties.Location.ComputerLocation == BriefcaseLocation.Computer.Local && !new BriefcaseUnpacker(session, this._eventLog, new SetImportProgressEventHandler(this.SetImportProgress), this._briefcase, this._briefcasePath, this._bis).Unpack())
        return;
      BriefcaseImportProgress importProgress = new BriefcaseImportProgress(OperationType.ImportingMetaData);
      ImportStore store = new ImportStore();
      if (!directoryInfo.Exists)
        throw new KernelExceptionID(98, (object) this._briefcasePath);
      DataSet[] dataSetArray = BriefcaseProcs.ReadMetaDataXML(this._briefcasePath);
      if (dataSetArray == null)
        throw new KernelExceptionID(98, (object) this._briefcasePath);
      IDBLanguageType defaultLanguage = session.DefaultLanguage;
      DataRow[] dataRowArray = dataSetArray[0].Tables["IMS_LANGUAGES"].Select($"{"F_DEFAULT"} = 1");
      if (dataRowArray.Length == 0)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_356"));
      if (dataRowArray.Length > 1)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_358"));
      if (defaultLanguage == null)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_357"));
      bool flag = defaultLanguage.LanguageID.Equals(Convert.ToString(dataRowArray[0]["F_LANGUAGE_ID"]));
      List<IDСorresponds> importingObjectIDs = new List<IDСorresponds>();
      Intermech.Kernel.Briefcase.ImportMetadata importMetadata = new Intermech.Kernel.Briefcase.ImportMetadata(session, this._eventLog, new SetImportProgressEventHandler(this.SetImportProgress));
      if (this._bis.ImportProperties.IsSinhronized && !this._bis.ImportProperties.ObjectsOnly && !importMetadata.Import(dataSetArray[0], dataSetArray[1], this._briefcase, store, IgnoringErrors.None, flag, this._bis.ImportProperties.CreateOnly))
        return;
      if (!this._bis.ImportProperties.ObjectsOnly)
      {
        session.DBCache.ReloadOldTables(session.DataManager);
        (session.IdentHelper as IDHelper).LoadData(session.DataManager);
      }
      this._countObjects = this.ReadCountNodes("Objects.xml", BriefcaseConsts.XmlObjectRecordTag);
      this._countRelations = this.ReadCountNodes("Relations.xml", BriefcaseConsts.XmlRelationRecordTag);
      List<FoundObjectInfo> findObjectsToObject = new List<FoundObjectInfo>();
      List<CheckMetadataLogItem> checkMetadataLogItemList = new CheckImportingData(session, this._eventLog, new SetImportProgressEventHandler(this.SetImportProgress)).Check(this._briefcasePath, this._briefcase, this._countObjects, findObjectsToObject);
      if (checkMetadataLogItemList.Count > 0 && !this._bis.ImportProperties.ObjectsOnly)
      {
        importProgress.CheckErrors = checkMetadataLogItemList;
        importProgress.Operation = OperationType.CheckError;
        this.SetImportProgress(this._briefcase, importProgress);
      }
      else
      {
        IBriefcaseProcesses service = (IBriefcaseProcesses) ServerServices.GetService(typeof (IBriefcaseProcesses));
        if (service != null)
          briefcaseIndex = service.StartImport(this._briefcase);
        try
        {
          if (!new ImportBriefcaseData(session, this._eventLog, new SetImportProgressEventHandler(this.SetImportProgress), this._briefcase, this._briefcasePath).Import(findObjectsToObject, importingObjectIDs, flag, briefcaseIndex, this._bis.ImportProperties, this._countObjects + this._countRelations))
            return;
        }
        finally
        {
          service?.StopImport(this._briefcase);
        }
        if (this._bis.ImportProperties.IsSinhronized || !this._bis.ImportProperties.ObjectsOnly)
        {
          this.SetPossibleValues(session, store.PossibleValuesAttributeType, importingObjectIDs);
          this.SetMeasureValues(session, store.MeasureValueObjectLink, importingObjectIDs);
          this.SetDefaultValues(session, store.DefaultValueObjectLink, importingObjectIDs);
          new RestoreMetaDataExtentions((IUserSession) session, importingObjectIDs, this._eventLog).RestoreItem(dataSetArray);
        }
        if (!this.ImportSecurity((IUserSession) session, importingObjectIDs))
          return;
        if (!this._bis.ImportProperties.ObjectsOnly)
        {
          session.DBCache.ReloadOldTables(session.DataManager);
          (session.IdentHelper as IDHelper).LoadData(session.DataManager);
        }
        importProgress.Percent = 100;
        importProgress.Operation = OperationType.Finished;
        this.SetImportProgress(this._briefcase, importProgress);
      }
    }
    catch (Exception ex)
    {
      this.SetImportProgress(this._briefcase, new BriefcaseImportProgress(OperationType.Error)
      {
        ErrorException = ex
      });
    }
    finally
    {
      this.DeleteTempFolder();
      session.Logout(sessionName);
    }
  }

  public void SetPossibleValues(
    UserSession session,
    List<AttributeTypePossibleValues> possibleValuesAttributeType,
    List<IDСorresponds> importingObjectIDs)
  {
    new RestorePossibleValues((IUserSession) session, importingObjectIDs, this._eventLog).Restore(possibleValuesAttributeType);
  }

  public void SetMeasureValues(
    UserSession session,
    List<SaveImportValues> measureValueObjectLink,
    List<IDСorresponds> importingObjectIDs)
  {
    new ResotreMeasureValues((IUserSession) session, importingObjectIDs, this._eventLog).Restore(measureValueObjectLink);
  }

  public void SetDefaultValues(
    UserSession session,
    List<SaveImportValues> defaultValueObjectLink,
    List<IDСorresponds> importingObjectIDs)
  {
    new RestoreDefaultValues((IUserSession) session, importingObjectIDs, this._eventLog).Restore(defaultValueObjectLink);
  }

  public bool ImportMetadata(
    DataSet metadata,
    DataSet importingList,
    ImportStore store,
    IgnoringErrors ignoringErrors,
    bool langsEquals,
    bool createOnly)
  {
    return new Intermech.Kernel.Briefcase.ImportMetadata(this.UserSession, this._eventLog, new SetImportProgressEventHandler(this.SetImportProgress)).Import(metadata, importingList, this._briefcase, store, ignoringErrors, langsEquals, createOnly);
  }

  private void DeleteTempFolder()
  {
    if (!this._bis.ImportProperties.DeleteTempFolder)
      return;
    DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(this._bis.ImportProperties.ServerTempFolder, this._briefcase.ToString()));
    if (!directoryInfo.Exists)
      return;
    directoryInfo.Delete(true);
  }

  private bool ImportSecurity(IUserSession session, List<IDСorresponds> importingObjectIDs)
  {
    return new ImportBriefcaseSecurity(session as UserSession, this._eventLog, new SetImportProgressEventHandler(this.SetImportProgress), this._briefcase, this._briefcasePath).Import(importingObjectIDs, this._bis.ImportProperties);
  }

  private void SetImportProgress(object sender, SetImportProgressEventArgs e)
  {
    this.SetImportProgress(e.Briefcase, e.ImportProgress);
  }

  private void SetImportProgress(Guid briefcase, BriefcaseImportProgress importProgress)
  {
    if (this.SetImportProgressEvent == null)
      return;
    if (importProgress.Percent > 100)
      importProgress.Percent = 100;
    this.SetImportProgressEvent((object) this, new SetImportProgressEventArgs(briefcase, importProgress));
  }

  private int ReadCountNodes(string xmlFileName, string nodeName)
  {
    int num = 0;
    XmlTextReader xmlTextReader = new XmlTextReader(Path.Combine(this._briefcasePath, xmlFileName));
    try
    {
      while (xmlTextReader.Read())
      {
        if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == nodeName)
          ++num;
      }
      return num;
    }
    finally
    {
      xmlTextReader.Close();
    }
  }
}
