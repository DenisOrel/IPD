
// Type: Intermech.Navigator.Controls.ObjectsToSelectItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Класс для анализа элементов пространства навигации и их отметки,
/// если они содержат указанные версии объектов
/// </summary>
public class ObjectsToSelectItemsAnalyzer : ToSelectItemsAnalyzer
{
  /// <summary>
  /// Список идентификаторов версий объектов, которые требуется отметить
  /// </summary>
  protected List<long> objects;

  /// <summary>
  /// Создать анализатор, добавить в список для отметки указанную версию объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта, который надо выбрать в окне</param>
  public ObjectsToSelectItemsAnalyzer(long objectID)
  {
    this.objects = new List<long>();
    this.objects.Add(objectID);
  }

  /// <summary>
  /// Создать анализатор, добавить в список для отметки указанные версии объектов
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов, которые надо выбрать в окне</param>
  public ObjectsToSelectItemsAnalyzer(IList<long> objectIDs)
  {
    this.objects = new List<long>((IEnumerable<long>) objectIDs);
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
  public override ToSelectItemsAnalyzerResult Analyze(
    Control sender,
    System.IServiceProvider services,
    INode handler,
    INodeID item,
    int index)
  {
    return this.objects == null || this.objects.Count == 0 || sender == null || handler == null || item == null || index < 0 || (!(handler.GetData(item, typeof (IDBObjectID)) is IDBObjectID data) ? 0 : (this.objects.IndexOf(data.Value) >= 0 ? 1 : 0)) == 0 ? ToSelectItemsAnalyzerResult.Skip : ToSelectItemsAnalyzerResult.Select;
  }
}
