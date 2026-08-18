// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DataGridViewCalendarColumn
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class DataGridViewCalendarColumn : DataGridViewColumn
{
  public DataGridViewCalendarColumn()
    : base((DataGridViewCell) new DataGridViewCalendarCell())
  {
  }

  public override DataGridViewCell CellTemplate
  {
    get => base.CellTemplate;
    set
    {
      base.CellTemplate = value == null || value.GetType().IsAssignableFrom(typeof (DataGridViewCalendarCell)) ? value : throw new InvalidCastException("Must be a CalendarCell");
    }
  }
}
