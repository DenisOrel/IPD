// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Clipboard.TableData
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Clipboard;

internal class TableData : IImbaseTableData, ICutCopy
{
  private DataSet _dataSet;
  private string _caption;
  private long _tableId;
  private long _linkId;
  private bool _cut;
  private static int _tableImageIndex;
  internal List<long> usedKeys;
  internal DataTable createdObjects;

  static TableData()
  {
    if (!(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    TableData._tableImageIndex = service.ImageIndex("imgTableEdit");
  }

  public TableData(DataSet dataSet, long tableId, long linkId, string caption, bool cut)
  {
    this._tableId = tableId;
    this._dataSet = dataSet;
    this._linkId = linkId;
    this._caption = caption;
    this._cut = cut;
  }

  public override bool Equals(object obj)
  {
    return obj is TableData tableData ? tableData._dataSet.Equals((object) this._dataSet) : base.Equals(obj);
  }

  public override int GetHashCode() => this._dataSet.GetHashCode();

  public override string ToString() => this._caption;

  public DataSet DataSet => this._dataSet;

  public long TableId => this._tableId;

  public long LinkId => this._linkId;

  public bool IsCut
  {
    get => this._cut;
    set => this._cut = value;
  }

  public int ImageIndex => TableData._tableImageIndex;
}
