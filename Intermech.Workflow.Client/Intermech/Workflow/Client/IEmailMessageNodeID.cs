// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.IEmailMessageNodeID
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

#nullable disable
namespace Intermech.Workflow.Client;

internal interface IEmailMessageNodeID
{
  string MessageID { get; }

  string InReplyTo { get; }

  long OfficeDocID { get; }
}
