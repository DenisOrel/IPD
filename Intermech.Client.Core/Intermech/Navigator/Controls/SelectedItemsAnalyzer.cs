
// Type: Intermech.Navigator.Controls.SelectedItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Базовый класс для анализа коллекции выделенных элементов
/// </summary>
public class SelectedItemsAnalyzer : ISelectedItemsAnalyzer
{
  /// <summary>Уникальный идентификатор анализатора</summary>
  protected Guid _guid = Guid.NewGuid();

  /// <summary>Уникальный идентификатор анализатора</summary>
  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this._guid;
  }

  /// <summary>
  /// Выполнить анализирование указанной коллекции элементов, выделенных в окне
  /// </summary>
  /// <param name="sender">Окно, в котором осуществляется выбор элементов</param>
  /// <param name="itemsHost">Служба окна, которая предоставляет коллекцию выделенных элементов</param>
  /// <returns>Результат проверки</returns>
  public virtual SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    return sender == null || itemsHost == null || itemsHost.SelectedItems == null || itemsHost.SelectedItems.Count == 0 ? SelectedItemsAnalyzerResult.Disabled : SelectedItemsAnalyzerResult.Enabled;
  }
}
