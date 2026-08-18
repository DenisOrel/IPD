// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.CompositionSortingValue
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel.Services.Compositions.Sorting;

internal class CompositionSortingValue : IComparable, IComparable<CompositionSortingValue>
{
  public const int UnknownValue = -1;
  protected int _projTypeID = -1;
  protected int _relTypeID = -1;
  protected int _partTypeID = -1;
  protected long _sorting = -1;
  protected DataRow _dataRow;
  protected CompositionSortingColumnInfo _columnsInfo;

  private void FillValues(DataRow dataRow)
  {
    if (dataRow == null || this.ColumnsInfo == null)
      return;
    this._projTypeID = Convert.ToInt32(dataRow[this.ColumnsInfo.idx_ProjType]);
    this._relTypeID = Convert.ToInt32(dataRow[this.ColumnsInfo.idx_RelType]);
    this._partTypeID = Convert.ToInt32(dataRow[this.ColumnsInfo.idx_PartType]);
    object obj = dataRow[this.ColumnsInfo.idx_Sorting];
    this._sorting = obj != DBNull.Value ? Convert.ToInt64(obj) : -1L;
  }

  public CompositionSortingValue([NotNull] DataRow dataRow, [NotNull] CompositionSortingColumnInfo columnsInfo)
  {
    this._columnsInfo = columnsInfo;
    this.FillValues(dataRow);
  }

  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  public override int GetHashCode() => this.ProjTypeId ^ this.RelTypeId;

  public CompositionSortingColumnInfo ColumnsInfo
  {
    [DebuggerStepThrough] get => this._columnsInfo;
  }

  public int ProjTypeId
  {
    [DebuggerStepThrough] get => this._projTypeID;
  }

  public int RelTypeId
  {
    [DebuggerStepThrough] get => this._relTypeID;
  }

  public int PartTypeId
  {
    [DebuggerStepThrough] get => this._partTypeID;
  }

  public long Sorting
  {
    [DebuggerStepThrough] get => this._sorting;
  }

  public int CompareTo(object obj) => this.CompareTo(obj as CompositionSortingValue);

  public int CompareTo(CompositionSortingValue other)
  {
    if (other == null)
      return -1;
    int num1 = this.ProjTypeId.CompareTo(other.ProjTypeId);
    if (num1 != 0)
      return num1;
    if (this.ColumnsInfo == null || this.ColumnsInfo.SortingRule == null)
      return this.Sorting.CompareTo(other.Sorting);
    int num2 = this.ColumnsInfo.SortingRule.CompareTo(this.ProjTypeId, this.RelTypeId, other.RelTypeId, this.PartTypeId, other.PartTypeId, true);
    if (num2 == 0)
      num2 = this.Sorting.CompareTo(other.Sorting);
    return num2;
  }
}
