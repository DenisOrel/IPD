// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ImbaseTableChangedEventArgs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Editors;

public class ImbaseTableChangedEventArgs : EventArgs
{
  private long _tableId;
  private long _linkId;

  public long TableId => this._tableId;

  public long LinkId => this._linkId;

  public ImbaseTableChangedEventArgs(long linkId, long tableId)
  {
    this._linkId = linkId;
    this._tableId = tableId;
  }
}
