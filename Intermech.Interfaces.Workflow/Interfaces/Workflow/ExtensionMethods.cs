// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.ExtensionMethods
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public static class ExtensionMethods
{
  public static bool ReplaceLink(
    this List<ExpressionInfo> expressionInfoList,
    long oldLinkID,
    long newLinkID)
  {
    int index = expressionInfoList.FindIndex((Predicate<ExpressionInfo>) (x => x.LinkID == oldLinkID));
    if (index == -1)
      return false;
    expressionInfoList[index].LinkID = Math.Abs(newLinkID);
    return true;
  }
}
