// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.OutputViewLogWriter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class OutputViewLogWriter : EventLogWriterBase
{
  private IOutputView outputViewService;
  private string categoryName;

  public OutputViewLogWriter(IOutputView outputViewService, string categoryName)
  {
    if (outputViewService == null)
      throw new ArgumentNullException(nameof (outputViewService));
    if (categoryName == null)
      throw new ArgumentNullException(nameof (categoryName));
    this.outputViewService = outputViewService;
    this.categoryName = categoryName;
  }

  protected override void DoWriteItem(EventLogItem item)
  {
    base.DoWriteItem(item);
    this.InternalWrite(item.MessageText, item.ItemType);
  }

  protected override void DoWriteMessage(string message, EventLogItemType itemType)
  {
    base.DoWriteMessage(message, itemType);
    this.InternalWrite(message, itemType);
  }

  private void InternalWrite(string message, EventLogItemType itemType)
  {
    this.outputViewService.WriteString(this.categoryName, message);
  }
}
