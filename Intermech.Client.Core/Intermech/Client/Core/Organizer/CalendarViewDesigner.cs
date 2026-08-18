
// Type: Intermech.Client.Core.Organizer.CalendarViewDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class CalendarViewDesigner : ControlDesigner
{
  /// <summary>Удаление ненужных свойств.</summary>
  /// <param name="properties">Набор свойств</param>
  protected override void PostFilterProperties(IDictionary properties)
  {
    properties.Remove((object) "BackColor");
    properties.Remove((object) "BackgroundImage");
    properties.Remove((object) "BackgroundImageLayout");
    properties.Remove((object) "ContextMenuStrip");
    properties.Remove((object) "ForeColor");
    properties.Remove((object) "RightToLeft");
    properties.Remove((object) "Text");
  }
}
