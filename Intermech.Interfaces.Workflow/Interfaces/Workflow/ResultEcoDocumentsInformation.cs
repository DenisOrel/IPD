// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.ResultEcoDocumentsInformation
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Workflow;

[Serializable]
public class ResultEcoDocumentsInformation
{
  public long ObjectID { get; set; }

  public long ID { get; set; }

  public int ObjectType { get; set; }

  public long CheckOutBy { get; set; }
}
