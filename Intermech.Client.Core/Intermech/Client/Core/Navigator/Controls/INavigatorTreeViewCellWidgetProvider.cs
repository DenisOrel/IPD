
// Type: Intermech.Client.Core.Navigator.Controls.INavigatorTreeViewCellWidgetProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Navigator.Controls;


namespace Intermech.Client.Core.Navigator.Controls;

/// <summary>
/// Интерфейс позволяющий переопределить отображение ячейки дерева навигатора
/// </summary>
public interface INavigatorTreeViewCellWidgetProvider
{
  /// <summary>Получение виджета для отображения</summary>
  /// <param name="rowWidget">Строка</param>
  /// <param name="column">Колонка</param>
  CellWidget GetCellWidget(NavigatorTreeView treeView, RowWidget rowWidget, Column column);
}
