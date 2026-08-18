
// Type: Intermech.Client.Core.FormDesigner.Controls.SavedColumnsSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Класс с сохраненными настройками для колонок ChildrenView.
/// </summary>
internal class SavedColumnsSettings
{
  /// <summary>Колонки и их ширины.</summary>
  private Dictionary<int, int> columnsWidth = new Dictionary<int, int>();

  /// <summary>Запомнить ширину указанной колонки.</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <param name="width">Ширина колонки</param>
  internal void SetColumnsWidth(int columnID, int width) => this.columnsWidth.Add(columnID, width);

  /// <summary>Получить ширину колонки по ее идентификатору.</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Ширина колонки</returns>
  internal int GetColumnsWidth(int columnID)
  {
    int columnsWidth = -1;
    if (!this.columnsWidth.TryGetValue(columnID, out columnsWidth))
      columnsWidth = -1;
    return columnsWidth;
  }
}
