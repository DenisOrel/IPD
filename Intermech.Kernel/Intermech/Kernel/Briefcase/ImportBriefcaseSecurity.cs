// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportBriefcaseSecurity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportBriefcaseSecurity : ImportBriefcaseBase
{
  private string _briefcasePath;
  private Guid _briefcase;

  public ImportBriefcaseSecurity(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent,
    Guid briefcase,
    string briefcasePath)
    : base(session, eventLog, setImportProgressEvent, briefcase, briefcasePath)
  {
    this._briefcase = briefcase;
    this._briefcasePath = briefcasePath;
  }

  public bool Import(
    List<IDСorresponds> importingObjectIDs,
    BriefcaseImportProperties importProperties)
  {
    BriefcaseImportProgress briefcaseImportProgress = new BriefcaseImportProgress(OperationType.ImportingSecurity);
    try
    {
      this.SetImportProgress(this._briefcase, briefcaseImportProgress);
      DataSet[] dataSetArray = BriefcaseProcs.ReadMetaDataXML(this._briefcasePath);
      DataSet dataSet1 = (DataSet) null;
      ArrayList importingSecObj = new ArrayList();
      Dictionary<long, string> importingLCSteps = new Dictionary<long, string>();
      if (importProperties.IsSinhronized)
      {
        dataSet1 = new DataSet("SECURITY_METADATA");
        dataSet1.ReadXmlSchema(Path.Combine(this._briefcasePath, "MetadataSecurity.xsd"));
        int num = (int) dataSet1.ReadXml(Path.Combine(this._briefcasePath, "MetadataSecurity.xml"));
      }
      DataSet dataSet2 = new DataSet("SECURITY_DATA");
      dataSet2.ReadXmlSchema(Path.Combine(this._briefcasePath, "ObjSecurity.xsd"));
      int num1 = (int) dataSet2.ReadXml(Path.Combine(this._briefcasePath, "ObjSecurity.xml"));
      int num2 = 0;
      double ExportSteps = 0.0;
      DataRow[] secRows1 = (DataRow[]) null;
      DataRow[] secRows2 = (DataRow[]) null;
      if (importProperties.IsSinhronized && dataSet1 != null && dataSet1.Tables != null && dataSet1.Tables[BriefcaseConsts.XmlSecurityRecordTag] != null)
      {
        secRows1 = dataSet1.Tables[BriefcaseConsts.XmlSecurityRecordTag].Select();
        num2 += secRows1.Length;
      }
      if (dataSet2 != null && dataSet2.Tables != null && dataSet2.Tables[BriefcaseConsts.XmlSecurityRecordTag] != null)
      {
        secRows2 = dataSet2.Tables[BriefcaseConsts.XmlSecurityRecordTag].Select();
        num2 += secRows2.Length;
      }
      double OneStep = num2 > 0 ? 100.0 / (double) num2 : 0.0;
      if (secRows1 != null && !this.ImportSecurityItems(secRows1, dataSetArray[0], importingObjectIDs, briefcaseImportProgress, ref ExportSteps, OneStep, importingSecObj, importingLCSteps) || secRows2 != null && !this.ImportSecurityItems(secRows2, dataSetArray[0], importingObjectIDs, briefcaseImportProgress, ref ExportSteps, OneStep, importingSecObj, importingLCSteps))
        return false;
      briefcaseImportProgress.Percent = 100;
      this.SetImportProgress(this._briefcase, briefcaseImportProgress);
      return true;
    }
    catch (Exception ex)
    {
      briefcaseImportProgress.ErrorException = ex;
      briefcaseImportProgress.Operation = OperationType.Error;
      this.SetImportProgress(this._briefcase, briefcaseImportProgress);
      this.eventLog.AddToTrace(ex.Message);
      return false;
    }
  }

  private bool ImportSecurityItems(
    DataRow[] secRows,
    DataSet metaData,
    List<IDСorresponds> importingObjectIDs,
    BriefcaseImportProgress bip,
    ref double ExportSteps,
    double OneStep,
    ArrayList importingSecObj,
    Dictionary<long, string> importingLCSteps)
  {
    foreach (DataRow secRow in secRows)
    {
      ImportSecurity importSecurity = new ImportSecurity(this.session, secRow, metaData, importingObjectIDs, importingSecObj, importingLCSteps, ImportItemOptions.None);
      if (!importSecurity.IsValid)
      {
        if (importSecurity.ErrorException != null)
          this.eventLog.AddToTrace($"{importSecurity.UniIdentifiler} : {importSecurity.ErrorException.Message}");
      }
      else
      {
        if (!importSecurity.Import())
        {
          if (importSecurity.ErrorException != null)
          {
            bip.ErrorException = importSecurity.ErrorException;
            this.eventLog.AddToTrace($"{importSecurity.ErrorException.Message}: {importSecurity.ErrorException.InnerException.Message}");
          }
          else
            bip.ErrorException = new Exception(string.Format(BriefcaseConsts.logObjectNotImported, (object) importSecurity.UniIdentifiler));
          this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectNotImported, (object) importSecurity.UniIdentifiler));
          bip.Operation = OperationType.Error;
          this.SetImportProgress(this._briefcase, bip);
          return false;
        }
        if (importSecurity.Log.Count > 0)
        {
          foreach (string eventString in importSecurity.Log)
            this.eventLog.AddToTrace(eventString);
        }
        this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectImported, (object) importSecurity.UniIdentifiler));
      }
      ExportSteps += OneStep;
      bip.Percent = (int) Math.Floor(ExportSteps);
      this.SetImportProgress(this._briefcase, bip);
    }
    return true;
  }
}
