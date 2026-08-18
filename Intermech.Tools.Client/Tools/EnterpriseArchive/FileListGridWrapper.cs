// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.FileListGridWrapper
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class FileListGridWrapper : IFileListView
{
  private readonly iGrid control;

  public FileListGridWrapper(iGrid control)
  {
    this.control = control != null ? control : throw new ArgumentNullException(nameof (control));
    this.control.UniqueKeys = true;
  }

  public void AppendItem(
    string key,
    string name,
    string type,
    long length,
    DateTime lastWriteTime,
    string state)
  {
    if (string.IsNullOrEmpty(key))
      throw new ArgumentException();
    iGRow iGrow = this.control.Rows.Add();
    iGrow.Key = key;
    iGrow.Cells["Name"].Value = (object) name;
    iGrow.Cells["Type"].Value = (object) type;
    iGrow.Cells["Length"].Value = (object) length;
    iGrow.Cells["LastWriteTime"].Value = (object) lastWriteTime;
    iGrow.Cells["State"].Value = (object) state;
  }

  public void UpdateItem(string key, string state)
  {
    iGRow rowByKey = this.FindRowByKey(key);
    if (rowByKey == null)
      return;
    rowByKey.Cells["State"].Value = (object) state;
  }

  public bool ContainsItem(string key) => this.FindRowByKey(key) != null;

  private iGRow FindRowByKey(string key)
  {
    if (string.IsNullOrEmpty(key))
      throw new ArgumentException();
    try
    {
      return this.control.Rows[key];
    }
    catch (ArgumentException ex)
    {
      return (iGRow) null;
    }
  }

  public void ClearItems() => this.control.Rows.Clear();

  public string GetSelectedItem()
  {
    return this.control.SelectedCells.Count <= 0 ? (string) null : this.control.SelectedCells[0].RowKey;
  }

  public void AutoSizeColumns()
  {
    foreach (iGCol col in (IEnumerable) this.control.Cols)
      col.AutoWidth(false);
  }

  public void ReapplySort()
  {
    if (this.control.SortObject.Count <= 0)
      return;
    this.control.Sort();
  }

  public int ItemsCount => this.control.Rows.Count;
}
