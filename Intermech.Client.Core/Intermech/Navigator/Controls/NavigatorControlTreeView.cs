
// Type: Intermech.Navigator.Controls.NavigatorControlTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

public class NavigatorControlTreeView : NavigatorTreeView
{
  /// <summary>Set tree view columns</summary>
  /// <param name="columns">Columns collecrion</param>
  /// <param name="descriptor">Root descriptor (to correct set supported columns)</param>
  public virtual void SetColumns(NodeColumnCollection columns, IDescriptor descriptor)
  {
    this.RootDescriptor = descriptor;
    this.SetColumns(columns);
  }
}
