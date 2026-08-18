
// Type: Intermech.Navigator.Views.ViewsTableEntry
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Views;

/// <summary>Запись таблицы закладок</summary>
internal class ViewsTableEntry
{
  /// <summary>Уровень</summary>
  private int _level;
  /// <summary>
  /// Контейнер информации о закладке, предоставляемой провайдером
  /// </summary>
  private ViewInfo _viewInfo;

  /// <summary>Создать запись таблицы закладок</summary>
  /// <param name="level">Уровень</param>
  /// <param name="viewInfo">Контейнер информации о закладке, предоставляемой провайдером</param>
  public ViewsTableEntry(int level, ViewInfo viewInfo)
  {
    this._level = level;
    this._viewInfo = viewInfo;
  }

  /// <summary>Уровень</summary>
  public int Level => this._level;

  /// <summary>
  /// Контейнер информации о закладке, предоставляемой провайдером
  /// </summary>
  public ViewInfo ViewInfo
  {
    get => this._viewInfo;
    set => this._viewInfo = value;
  }
}
