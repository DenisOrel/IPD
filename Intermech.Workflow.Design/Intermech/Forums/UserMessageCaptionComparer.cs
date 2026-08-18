// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessageCaptionComparer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Forums;

public class UserMessageCaptionComparer : IComparer<UserMessage>
{
  private SortOrder order = SortOrder.Descending;

  public UserMessageCaptionComparer(SortOrder order) => this.order = order;

  public int Compare(UserMessage x, UserMessage y)
  {
    string caption1 = x.Caption;
    string caption2 = y.Caption;
    return this.order != SortOrder.Ascending ? caption2.CompareTo(caption1) : caption1.CompareTo(caption2);
  }
}
