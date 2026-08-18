
// Type: Intermech.Navigator.Controls.ObjectListFiltrationHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Сервис позволяет временно сохранять выбранный фильтр списка объектов
/// </summary>
internal class ObjectListFiltrationHolder : IObjectListFiltrationHolder
{
  /// <summary>
  /// Идентификаторы выборок, по которым будут выполняться фильтрации
  /// в разных закладках "Навигатора"
  /// </summary>
  private long _selectionID;

  /// <summary>
  /// Идентификатор выборки, по которой будет выполняться фильтрация.
  /// -1 - фильтрация отключена
  /// </summary>
  long IObjectListFiltrationHolder.SelectionID
  {
    [DebuggerStepThrough] get => this._selectionID;
    set => this._selectionID = value;
  }
}
