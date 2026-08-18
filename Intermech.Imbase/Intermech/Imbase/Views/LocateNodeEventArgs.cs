// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.LocateNodeEventArgs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Views;

public class LocateNodeEventArgs : EventArgs
{
  private DataTable _dataTable;
  private long _objectId;

  public DataTable DataTable
  {
    get => this._dataTable;
    set => this._dataTable = value;
  }

  public long ObjectId
  {
    get => this._objectId;
    set => this._objectId = value;
  }

  public LocateNodeEventArgs(long objectId, DataTable dt)
  {
    this._dataTable = dt;
    this._objectId = objectId;
  }
}
