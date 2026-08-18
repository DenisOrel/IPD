// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseTableEventArgs
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

public class ImbaseTableEventArgs : EventArgs
{
  private long _tableId;

  public ImbaseTableEventArgs(int tableId) => this._tableId = (long) tableId;

  public long TableId => this._tableId;
}
