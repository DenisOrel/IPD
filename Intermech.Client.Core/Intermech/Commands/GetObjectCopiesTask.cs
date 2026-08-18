
// Type: Intermech.Commands.GetObjectCopiesTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Commands;

public sealed class GetObjectCopiesTask : HistoryTask
{
  private int categoryType;
  private ActionType actionId;
  private List<long> prevObjects;
  private List<long> currObjects;

  public GetObjectCopiesTask(int categoryType, ActionType actionId)
  {
    this.categoryType = categoryType;
    this.actionId = actionId;
    this.prevObjects = new List<long>();
    this.currObjects = new List<long>();
  }

  public List<long> PreviousObjectVersions => this.prevObjects;

  public List<long> CurrentObjectVersions => this.currObjects;

  protected override void DoProcessModifications(List<CategoryValue> modificationList)
  {
    bool flag = true;
    for (int index = 0; index < modificationList.Count; ++index)
    {
      CategoryValue modification = modificationList[index];
      if (modification.CategoryType == this.categoryType && modification.ActionID == this.actionId)
      {
        (flag ? this.prevObjects : this.currObjects).Add(modification.CategoryID);
        flag = !flag;
      }
    }
    if (flag)
      return;
    this.prevObjects.RemoveAt(this.prevObjects.Count - 1);
  }
}
