
// Type: Intermech.Navigator.ContextMenu.FilterActionType
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Описывает, как фильтр будет использовать переданный ему
/// список имен команд
/// </summary>
public enum FilterActionType
{
  /// <summary>
  /// В таблицу команд попадут только те команды, чьи
  /// имена есть в списке
  /// </summary>
  Allow,
  /// <summary>
  /// В таблицу команд не попадут те команды, чьи
  /// имена есть в списке
  /// </summary>
  Suppress,
}
