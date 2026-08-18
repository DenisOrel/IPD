
// Type: Intermech.Navigator.Controls.IToSelectItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс фильтра, который выполняет анализ элементов пространства навигации в окне по выбору
/// узлов в дереве и в гриде, и возвращает значения, позволяющее выделять указанные элементы
/// </summary>
public interface IToSelectItemsAnalyzer
{
  /// <summary>Уникальный идентификатор анализатора</summary>
  Guid Guid { get; }

  /// <summary>
  /// Выполнить изучение элемента из указанной коллекции, вернуть результат - надо ли его выделять в контроле или нет
  /// </summary>
  /// <param name="sender">Контрол, в котором осуществляется выбор элементов</param>
  /// <param name="services">Контейнер сервисов контрола окна, предоставляющего анализируемый элемент пространства навигации</param>
  /// <param name="handler">Обработчик указанного элемента, позволяющий получать дополнительные данные для этого элемента</param>
  /// <param name="item">Анализируемый элемент из коллекции пространства навигации</param>
  /// <param name="index">Индекс данного элемента в коллекции</param>
  /// <returns>Результат проверки</returns>
  ToSelectItemsAnalyzerResult Analyze(
    Control sender,
    System.IServiceProvider services,
    INode handler,
    INodeID item,
    int index);
}
