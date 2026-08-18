
// Type: Intermech.Navigator.Controls.ToSelectItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Базовый класс для анализа элементов пространства навигации и их отметки
/// </summary>
public class ToSelectItemsAnalyzer : IToSelectItemsAnalyzer
{
  /// <summary>Уникальный идентификатор анализатора</summary>
  protected Guid _guid = Guid.NewGuid();

  /// <summary>Уникальный идентификатор анализатора</summary>
  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this._guid;
  }

  /// <summary>
  /// Выполнить изучение элемента из указанной коллекции, вернуть результат - надо ли его выделять в контроле или нет
  /// </summary>
  /// <param name="sender">Контрол, в котором осуществляется выбор элементов</param>
  /// <param name="services">Контейнер сервисов контрола окна, предоставляющего анализируемый элемент пространства навигации</param>
  /// <param name="handler">Обработчик указанного элемента, позволяющий получать дополнительные данные для этого элемента</param>
  /// <param name="item">Анализируемый элемент из коллекции пространства навигации</param>
  /// <param name="index">Индекс данного элемента в коллекции</param>
  /// <returns>Результат проверки</returns>
  public virtual ToSelectItemsAnalyzerResult Analyze(
    Control sender,
    System.IServiceProvider services,
    INode handler,
    INodeID item,
    int index)
  {
    if (sender != null && item != null)
      ;
    return ToSelectItemsAnalyzerResult.Skip;
  }
}
