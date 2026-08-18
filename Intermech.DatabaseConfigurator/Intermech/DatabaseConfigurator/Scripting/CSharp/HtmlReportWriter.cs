// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.HtmlReportWriter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.IO;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class HtmlReportWriter
{
  private ScriptCheckerIDCache idCache;

  public HtmlReportWriter(ScriptCheckerIDCache idCache)
  {
    this.idCache = idCache != null ? idCache : throw new ArgumentNullException(nameof (idCache));
  }

  public UpdatedDBObjectInfo CreateOrUpdateReport(
    DateTime creationTime,
    string reportName,
    string reportContent)
  {
    using (new SessionKeeper())
    {
      IDBObject reportByName = this.GetOrCreateReportByName(reportName);
      long num = reportByName.ObjectID;
      bool isCreationMode = reportByName.IsCreationMode;
      reportByName.Attributes.AddAttribute(this.idCache.Name.Id, false).Value = (object) reportName;
      string fileName = this.MakeUniqueDBObjectFileName(reportByName.ID, $"report_{Math.Abs(num)}.html");
      this.CreateOrUpdateDBObjectFile(num, fileName, reportContent, creationTime);
      if (isCreationMode)
      {
        reportByName.CommitCreation(true);
        num = -num;
      }
      return new UpdatedDBObjectInfo(num, this.idCache.HtmlReports.Id, isCreationMode);
    }
  }

  private string MakeUniqueDBObjectFileName(long id, string nonUniqueFileName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetUniqueFileName(nonUniqueFileName, id, sessionKeeper.Session.SessionGUID);
  }

  private void CreateOrUpdateDBObjectFile(
    long dbObjectId,
    string fileName,
    string fileContent,
    DateTime creationTime)
  {
    string tempFileName = Path.GetTempFileName();
    try
    {
      File.WriteAllText(tempFileName, fileContent, Encoding.UTF8);
      new FileInfo(tempFileName).LastAccessTime = creationTime;
      UploadFileInfo[] items = new UploadFileInfo[1]
      {
        new UploadFileInfo(fileName, tempFileName)
      };
      new UploadFilesAction((IDBObjectRef) new DirectDBObjectRef(dbObjectId), (IList<UploadFileInfo>) items).Perform();
    }
    finally
    {
      FileUtils.DeleteFileSilently(tempFileName);
    }
  }

  private IDBObject GetOrCreateReportByName(string reportName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long reportByName = this.FindReportByName(reportName);
      return reportByName == 0L ? sessionKeeper.Session.GetObjectCollection(this.idCache.HtmlReports.Id).Create() : sessionKeeper.Session.GetObject(reportByName, true);
    }
  }

  private long FindReportByName(string reportName)
  {
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
    dbRecordSetParams.RecordCount = 1;
    dbRecordSetParams.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    dbRecordSetParams.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure(this.idCache.Name.Id, RelationalOperators.Equal, (object) reportName, LogicalOperators.NONE, 0, false)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(this.idCache.HtmlReports.Id, dbRecordSetParams);
      return dataTable.Rows.Count != 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
    }
  }
}
