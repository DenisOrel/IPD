// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessageComparer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Forums;

public static class UserMessageComparer
{
  public static IComparer<UserMessage> MessageComparer(SortField filed, SortOrder order)
  {
    switch (filed)
    {
      case SortField.Date:
        return (IComparer<UserMessage>) new UserMessageDateComparer(order);
      case SortField.Caption:
        return (IComparer<UserMessage>) new UserMessageCaptionComparer(order);
      case SortField.UserName:
        return (IComparer<UserMessage>) new UserMessageUserComparer(order);
      default:
        return (IComparer<UserMessage>) new UserMessageDateComparer(SortOrder.Descending);
    }
  }
}
