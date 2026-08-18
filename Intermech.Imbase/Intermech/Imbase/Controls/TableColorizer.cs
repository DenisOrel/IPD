// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.TableColorizer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class TableColorizer : ITableViewColorizer
{
  internal static TableColorizer Instance = new TableColorizer();

  public event TableView.ColorizeRowsEventHandler ColorizeRows;

  internal Dictionary<long, Color> GetColorsForRows(
    IUserSession session,
    AttributeTypeProperties[] properties,
    DataTable dataTable)
  {
    Dictionary<long, Color> colorsForRows = (Dictionary<long, Color>) null;
    if (this.ColorizeRows != null)
    {
      foreach (TableView.ColorizeRowsEventHandler invocation in this.ColorizeRows.GetInvocationList())
      {
        colorsForRows = invocation(session, properties, dataTable);
        if (colorsForRows != null)
          break;
      }
    }
    return colorsForRows;
  }
}
