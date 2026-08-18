// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessageDateComparer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Forums;

public class UserMessageDateComparer : IComparer<UserMessage>
{
  private SortOrder order = SortOrder.Descending;

  public UserMessageDateComparer(SortOrder order) => this.order = order;

  public int Compare(UserMessage x, UserMessage y)
  {
    DateTime date1 = x.Date;
    DateTime date2 = y.Date;
    return this.order != SortOrder.Ascending ? date2.CompareTo(date1) : date1.CompareTo(date2);
  }
}
