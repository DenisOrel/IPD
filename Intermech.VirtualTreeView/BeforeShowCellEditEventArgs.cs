// Decompiled with JetBrains decompiler
// Type: Intermech.VirtualTreeView.BeforeShowCellEditEventArgs
// Assembly: Intermech.VirtualTreeView, Version=4.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: CFAE8D69-6554-4155-8AB7-42592C2FC48A
// Assembly location: D:\IPS\Client\Intermech.VirtualTreeView.dll

using Infralution.Controls.VirtualTree;
using System;

#nullable disable
namespace Intermech.VirtualTreeView;

public class BeforeShowCellEditEventArgs : EventArgs
{
  private bool _cancel;
  private Column _column;
  private Row _row;

  public BeforeShowCellEditEventArgs(Row row, Column column)
  {
    this._row = row;
    this._column = column;
  }

  public bool Cancel
  {
    get => this._cancel;
    set => this._cancel = value;
  }

  public Column Column => this._column;

  public Row Row => this._row;
}
