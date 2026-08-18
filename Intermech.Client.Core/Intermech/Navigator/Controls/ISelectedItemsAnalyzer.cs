
// Type: Intermech.Navigator.Controls.ISelectedItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс фильтра, который выполняет анализ указанной коллекции
/// ISelectedItems в окне по выбору объектов
/// и возвращает значение, позволяющее заблокировать кнопку "ОК"
/// </summary>
public interface ISelectedItemsAnalyzer
{
  /// <summary>Уникальный идентификатор анализатора</summary>
  Guid Guid { get; }

  /// <summary>
  /// Выполнить анализирование указанной коллекции элементов, выделенных в окне
  /// </summary>
  /// <param name="sender">Окно, в котором осуществляется выбор элементов</param>
  /// <param name="itemsHost">Служба окна, которая предоставляет коллекцию выделенных элементов</param>
  /// <returns>Результат проверки</returns>
  SelectedItemsAnalyzerResult Analyze(ISelectionWindow sender, ISelectedItemsHost itemsHost);
}
