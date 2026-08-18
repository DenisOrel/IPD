// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CalcContext
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

public class CalcContext
{
  private Dictionary<string, string> _recordRefMap = new Dictionary<string, string>();
  private List<int> _recordRefColumns = new List<int>();
  private long _linkId;

  public CalcContext(long linkId) => this._linkId = linkId;

  public string GetMapValue(string value)
  {
    string str;
    return this._recordRefMap.TryGetValue(value, out str) ? str : value;
  }

  public void AddRefColumn(int colId)
  {
    if (this._recordRefColumns.Contains(colId))
      return;
    this._recordRefColumns.Add(colId);
  }

  public void SetRecordsMap(Dictionary<string, string> value) => this._recordRefMap = value;

  public bool IsMapped(int colIndex) => this._recordRefColumns.Contains(colIndex);

  public bool HasColumns => this._recordRefColumns.Count > 0;

  public List<int> ColumnsList => this._recordRefColumns;

  public long LinkId
  {
    get => this._linkId;
    set => this._linkId = value;
  }
}
