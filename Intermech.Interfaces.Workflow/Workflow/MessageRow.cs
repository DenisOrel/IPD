// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.MessageRow
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

public class MessageRow
{
  public readonly string RemoteProcessName = "";
  public readonly object[] Data;
  public Guid SrcSiteGuid;
  public string SrcSiteName = "";

  public MessageRow(string ParentProcessName, object[] Data)
  {
    this.RemoteProcessName = ParentProcessName;
    this.Data = Data;
  }
}
