// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.StructureChanged_EventArgs
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.ECO.Client;

public class StructureChanged_EventArgs : EventArgs
{
  public DocumentTreeNode Node;

  public StructureChanged_EventArgs(DocumentTreeNode node) => this.Node = node;
}
