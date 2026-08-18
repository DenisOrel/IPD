
// Type: Intermech.Controls.Grid.IEmbeddedControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace Intermech.Controls.Grid;

/// <summary>
/// Interface you must include for a control to be activated embedded useable
/// </summary>
public interface IEmbeddedControl
{
  /// <summary>item this control is embedded in</summary>
  ListItem Item { get; set; }

  /// <summary>Sub item this control is embedded in</summary>
  ListSubItem SubItem { get; set; }

  /// <summary>Parent control</summary>
  ListGrid ListControl { get; set; }

  /// <summary>
  /// This returns the current text output as entered into the control right now
  /// </summary>
  /// <returns></returns>
  string ReturnText();

  /// <summary>Called when the control is loaded</summary>
  /// <param name="item"></param>
  /// <param name="subItem"></param>
  /// <param name="listctrl"></param>
  /// <returns></returns>
  bool Load(ListItem item, ListSubItem subItem, ListGrid listctrl);

  /// <summary>Called when control is being destructed</summary>
  void Unload();
}
