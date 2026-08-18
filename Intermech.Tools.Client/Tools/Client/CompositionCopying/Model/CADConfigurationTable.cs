// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADConfigurationTable
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADConfigurationTable
{
  private List<CADConfigurationTableRow> rows;
  private IList<CADConfigurationTableRow> rowsReadOnly;
  private static readonly CADConfigurationTableRow[] emptyRowsArray = new CADConfigurationTableRow[0];

  public CADConfigurationTable()
  {
    this.rows = (List<CADConfigurationTableRow>) null;
    this.rowsReadOnly = (IList<CADConfigurationTableRow>) CADConfigurationTable.emptyRowsArray;
  }

  public void Add(CADConfigurationTableRow row)
  {
    this.PrepareForModification();
    this.rows.Add(row);
  }

  public IList<CADConfigurationTableRow> Rows
  {
    [DebuggerStepThrough] get => this.rowsReadOnly;
  }

  private void PrepareForModification()
  {
    if (this.rows != null)
      return;
    this.rows = new List<CADConfigurationTableRow>();
    this.rowsReadOnly = (IList<CADConfigurationTableRow>) new ReadOnlyListWrapper<CADConfigurationTableRow>((IList<CADConfigurationTableRow>) this.rows);
  }
}
