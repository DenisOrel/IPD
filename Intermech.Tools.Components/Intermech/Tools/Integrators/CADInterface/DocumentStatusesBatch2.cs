// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentStatusesBatch2
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Kernel.Search;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class DocumentStatusesBatch2 : DocumentStatusesBatch
{
  public void GetStatuses(
    string[] pDocFullPaths,
    out EDocumentStatus[] pStatuses,
    out string[] pCheckedOutBy,
    out DateTime[] pLastModified)
  {
    DocumentStatuesResult2 documentStatuesResult2 = (DocumentStatuesResult2) this.PerformBatch((IList<string>) pDocFullPaths);
    pStatuses = documentStatuesResult2.StatusArray;
    pCheckedOutBy = documentStatuesResult2.UserNamesArray;
    pLastModified = documentStatuesResult2.LastModified;
  }

  protected override object CreateResultObject(int length)
  {
    return (object) new DocumentStatuesResult2(length);
  }

  protected override DataTable GetTable(List<long> objectIds)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objectIds.ToArray(), LogicalOperators.NONE, 0, true)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      paramSet.Columns = new object[4]
      {
        (object) -2,
        (object) -6,
        (object) sessionKeeper.Session.IdentHelper.FileAttributeID,
        (object) sessionKeeper.Session.IdentHelper.FileAttributeID
      };
      paramSet.Contents = new ColumnContents[4]
      {
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.Date,
        ColumnContents.String
      };
      return sessionKeeper.Session.GetObjectCollection(-1).Select(paramSet);
    }
  }

  protected override void SetResultForObject(object result, DataRow row, List<int> posList)
  {
    DocumentStatuesResult2 result1 = (DocumentStatuesResult2) result;
    long int64 = Convert.ToInt64(row[1]);
    DateTime dateTime = Convert.ToDateTime(row[2]);
    string str = Convert.ToString(row[3]);
    if (!string.IsNullOrEmpty(str))
      str = Path.Combine(this.fileVault.WorkArea.AreaPath, str);
    if (int64 != 0L && int64 != -1L)
    {
      if (int64 == this.currentUserId)
        this.SetStatus(result1, posList, EDocumentStatus.DS_CheckedOut, this.currentUserName, dateTime, str);
      else
        this.SetStatus(result1, posList, EDocumentStatus.DS_CheckedOutByDifferentUser, this.userNamesCache.GetUserName(int64), dateTime, str);
    }
    else
      this.SetStatus(result1, posList, EDocumentStatus.DS_CheckedIn, string.Empty, dateTime, str);
  }

  protected override void SetResultForUnknownFile(object result, int index)
  {
    this.SetStatus((DocumentStatuesResult2) result, index, EDocumentStatus.DS_Unknown, string.Empty, DateTime.MinValue);
  }

  protected override bool SupportsFastCheckedOutInfo(FileOrigin origin) => false;

  protected override void SetResultForCheckedOutObject(object result, string fullPath, int index)
  {
    throw new NotSupportedException();
  }

  private void SetStatus(
    DocumentStatuesResult2 result,
    int pos,
    EDocumentStatus status,
    string userName,
    DateTime lastModified)
  {
    result.StatusArray[pos] = status;
    result.UserNamesArray[pos] = userName;
    result.LastModified[pos] = lastModified;
  }

  private void SetStatus(
    DocumentStatuesResult2 result,
    List<int> posList,
    EDocumentStatus status,
    string userName,
    DateTime lastModified,
    string masterFileName)
  {
    for (int index = 0; index < posList.Count; ++index)
    {
      int pos = posList[index];
      EDocumentStatus edocumentStatus = masterFileName == null || PathUtils.IsSamePath(masterFileName, this.filenames[pos]) ? status : EDocumentStatus.DS_Auxiliary;
      result.StatusArray[pos] = edocumentStatus;
      result.UserNamesArray[pos] = userName;
      result.LastModified[pos] = lastModified;
    }
  }
}
