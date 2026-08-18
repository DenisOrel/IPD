
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.DataGridViewCalendarColumn
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

public class DataGridViewCalendarColumn : DataGridViewColumn
{
  /// <summary>Конструктор.</summary>
  public DataGridViewCalendarColumn()
    : base((DataGridViewCell) new DataGridViewCalendarCell())
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public override DataGridViewCell CellTemplate
  {
    get => base.CellTemplate;
    set
    {
      base.CellTemplate = value == null || value.GetType().IsAssignableFrom(typeof (DataGridViewCalendarCell)) ? value : throw new InvalidCastException("Must be a CalendarCell");
    }
  }

  public event EventHandler ClouseUp;

  internal void OnClouseUp(object cell)
  {
    if (this.ClouseUp == null)
      return;
    this.ClouseUp(cell, EventArgs.Empty);
  }
}
