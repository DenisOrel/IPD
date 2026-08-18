// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesReportHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal static class CaptureChangesReportHelper
{
  private const int TypicalContextSymbols = 32 /*0x20*/;
  internal const string Analysis = "Analysis";
  internal const string Applying = "Applying";
  private const int FileInfoBaseId = 1000;
  private const int FileUploadId = 1001;

  public static void TrySetFormattingHandler(UIReportScope childScope)
  {
    if (childScope == null)
      return;
    childScope.PrepareReport += new EventHandler<UIReportDisplayArgs>(CaptureChangesReportHelper.OnPrepareReport);
  }

  private static void OnPrepareReport(object sender, UIReportDisplayArgs e)
  {
    if (e.ReportItems.Count == 0)
      return;
    CaptureChangesReportHelper.CreateContextGroups(e.ReportItems);
  }

  private static void CreateContextGroups(ICollection<UIReportItem> items)
  {
    List<UIReportItem> uiReportItemList = new List<UIReportItem>((IEnumerable<UIReportItem>) items);
    for (int index1 = 0; index1 < uiReportItemList.Count; ++index1)
    {
      UIReportItem uiReportItem = uiReportItemList[index1];
      int index2 = Array.FindIndex<object>(uiReportItem.Context, new Predicate<object>(CaptureChangesReportHelper.IsGroupOperation));
      if (index2 >= 0)
        index1 += CaptureChangesReportHelper.CreateLineGroup(uiReportItemList, index1, uiReportItem, index2);
    }
    items.Clear();
    items.AddRange<UIReportItem>((IEnumerable<UIReportItem>) uiReportItemList);
  }

  private static int CreateLineGroup(
    List<UIReportItem> target,
    int itemIndex,
    UIReportItem item,
    int displayGroupPos)
  {
    LinkedList<UIReportItem> itemsWithContext = CaptureChangesReportHelper.ExtractItemsWithContext(target, item.Context, displayGroupPos, itemIndex + 1);
    UIReportItem uiReportItem1 = item.Clone();
    uiReportItem1.TraceLevel = TraceLevel.Info;
    uiReportItem1.Id = 0;
    uiReportItem1.Header = string.Empty;
    uiReportItem1.Text = $">>> {CaptureChangesReportHelper.GetGroupName(item, displayGroupPos)}";
    ++item.IndentLevel;
    foreach (UIReportItem uiReportItem2 in itemsWithContext)
      ++uiReportItem2.IndentLevel;
    for (LinkedListNode<UIReportItem> linkedListNode = itemsWithContext.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
      target.Insert(itemIndex + 1, linkedListNode.Value);
    target.Insert(itemIndex, uiReportItem1);
    return itemsWithContext.Count + 1;
  }

  private static string GetGroupName(UIReportItem item, int displayGroupPos)
  {
    string groupText = CaptureChangesReportHelper.GetGroupText(item.Context[displayGroupPos]);
    Queue<string> stringQueue = new Queue<string>(item.Context.Length);
    for (int index = displayGroupPos + 1; index < item.Context.Length; ++index)
    {
      string groupContextText = CaptureChangesReportHelper.GetGroupContextText(item.Context[index]);
      if (!string.IsNullOrEmpty(groupContextText))
        stringQueue.Enqueue(groupContextText);
    }
    StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/ + groupText.Length + stringQueue.Count * 32 /*0x20*/);
    stringBuilder.Append(groupText);
    if (stringQueue.Count > 0)
    {
      stringBuilder.Append(' ');
      stringBuilder.Append("//");
      stringBuilder.Append(' ');
      stringBuilder.Append(LocalizationHolder.rm.GetString("Tools.Components_511"));
      stringBuilder.Append(':');
      stringBuilder.Append(' ');
      stringBuilder.Append(stringQueue.Dequeue());
      while (stringQueue.Count > 0)
      {
        stringBuilder.Append(',');
        stringBuilder.Append(' ');
        stringBuilder.Append(stringQueue.Dequeue());
      }
    }
    return stringBuilder.ToString();
  }

  private static LinkedList<UIReportItem> ExtractItemsWithContext(
    List<UIReportItem> target,
    object[] context,
    int displayGroupPos,
    int index)
  {
    LinkedList<UIReportItem> itemsWithContext = new LinkedList<UIReportItem>();
    while (index < target.Count)
    {
      UIReportItem uiReportItem = target[index];
      bool flag = uiReportItem.Context.Length == context.Length;
      if (flag)
      {
        flag = true;
        for (int index1 = displayGroupPos; index1 < uiReportItem.Context.Length; ++index1)
        {
          if (!object.Equals(uiReportItem.Context[index1], context[index1]))
          {
            flag = false;
            break;
          }
        }
      }
      if (flag)
      {
        itemsWithContext.AddLast(uiReportItem);
        target.RemoveAt(index);
      }
      else
        ++index;
    }
    return itemsWithContext;
  }

  private static bool IsGroupOperation(object logicalOperationId)
  {
    return logicalOperationId is SectionEntity sectionEntity && sectionEntity.Sections.Contains<DisplaySection>();
  }

  private static string GetGroupText(object logicalOperationId)
  {
    return ((SectionEntity) logicalOperationId).Sections.Get<DisplaySection>().QualifiedName;
  }

  private static string GetGroupContextText(object logicalOperationId)
  {
    if (logicalOperationId is string strA)
    {
      if (string.Compare(strA, "Analysis", true) == 0)
        return LocalizationHolder.rm.GetString("Tools.Components_509");
      if (string.Compare(strA, "Applying", true) == 0)
        return LocalizationHolder.rm.GetString("Tools.Components_510");
    }
    return (string) null;
  }

  public static void ReportFileUploadData(ICollection<string> fileNames)
  {
    if (fileNames == null)
      throw new ArgumentNullException(nameof (fileNames));
    if (fileNames.Count == 0)
      return;
    string[] strArray = new string[fileNames.Count];
    fileNames.CopyTo(strArray, 0);
    UIReport.ReportData((object[]) strArray, TraceLevel.Off, 1001);
  }

  public static void ReportFileUploadSummary()
  {
    PathCollection pathCollection = new PathCollection(256 /*0x0100*/);
    foreach (UIReportItem uiReportItem in UIReport.ScanReport())
    {
      if (uiReportItem.Id == 1001)
      {
        foreach (string str in uiReportItem.Data)
          pathCollection.Add(str);
      }
    }
    if (pathCollection.Count <= 0)
      return;
    UIReport.ReportEvent($">>> {LocalizationHolder.rm.GetString("Tools.Components_507")}:");
    UIReport.Indent();
    foreach (string text in (OrderedList<string>) pathCollection)
      UIReport.ReportEvent(text);
    UIReport.Unindent();
  }
}
