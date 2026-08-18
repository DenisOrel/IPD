// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentStatusesBatch1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class DocumentStatusesBatch1 : DocumentStatusesBatch
{
  public void GetStatuses(
    string[] pDocFullPaths,
    out EDocumentStatus[] pStatuses,
    out string[] pCheckedOutBy)
  {
    DocumentStatuesResult documentStatuesResult = (DocumentStatuesResult) this.PerformBatch((IList<string>) pDocFullPaths);
    pStatuses = documentStatuesResult.StatusArray;
    pCheckedOutBy = documentStatuesResult.UserNamesArray;
  }

  protected override object CreateResultObject(int length)
  {
    return (object) new DocumentStatuesResult(length);
  }

  protected override DataTable GetTable(List<long> objectIds)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[2]
    {
      (object) -2,
      (object) -6
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objectIds.ToArray(), LogicalOperators.NONE, 0, true)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectCollection(-1).Select(paramSet);
  }

  protected override void SetResultForObject(object result, DataRow row, List<int> posList)
  {
    DocumentStatuesResult result1 = (DocumentStatuesResult) result;
    long int64 = Convert.ToInt64(row[1]);
    switch (int64)
    {
      case -1:
      case 0:
        DocumentStatusesBatch1.SetStatus(result1, posList, EDocumentStatus.DS_CheckedIn, string.Empty);
        break;
      default:
        if (int64 == this.currentUserId)
        {
          DocumentStatusesBatch1.SetStatus(result1, posList, EDocumentStatus.DS_CheckedOut, this.currentUserName);
          break;
        }
        DocumentStatusesBatch1.SetStatus(result1, posList, EDocumentStatus.DS_CheckedOutByDifferentUser, this.userNamesCache.GetUserName(int64));
        break;
    }
  }

  protected override void SetResultForUnknownFile(object result, int index)
  {
    DocumentStatusesBatch1.SetStatus((DocumentStatuesResult) result, index, EDocumentStatus.DS_Unknown, string.Empty);
  }

  protected override bool SupportsFastCheckedOutInfo(FileOrigin origin)
  {
    return origin.WorkObject.IsEditableState && origin.WorkObject.ObjectId < 0L;
  }

  protected override void SetResultForCheckedOutObject(object result, string fullPath, int index)
  {
    DocumentStatusesBatch1.SetStatus((DocumentStatuesResult) result, index, EDocumentStatus.DS_CheckedOut, this.currentUserName);
  }

  private static void SetStatus(
    DocumentStatuesResult result,
    List<int> posList,
    EDocumentStatus status,
    string userName)
  {
    for (int index = 0; index < posList.Count; ++index)
    {
      int pos = posList[index];
      result.StatusArray[pos] = status;
      result.UserNamesArray[pos] = userName;
    }
  }

  private static void SetStatus(
    DocumentStatuesResult result,
    int pos,
    EDocumentStatus status,
    string userName)
  {
    result.StatusArray[pos] = status;
    result.UserNamesArray[pos] = userName;
  }
}
