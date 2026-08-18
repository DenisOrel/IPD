// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeItems
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Persistence;
using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс для работы с дочерними элементами пространства навигации
/// </summary>
public interface INodeItems
{
  /// <summary>
  /// Возвращает набор атрибутов указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Набор флагов атрибутов</returns>
  ContentAttributes GetAttributesOf(INodeID nodeID);

  /// <summary>
  /// Возвращает основной интерфейс элемента из пространства навигации
  /// для указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Интерфейс элемента навигации</returns>
  INode GetChild(INodeID nodeID);

  /// <summary>
  /// Возвращает адрес дочернего элемента, который может быть использован
  /// в адресной строке.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Адрес дочернего элемента</returns>
  string GetAddress(INodeID nodeID);

  /// <summary>
  /// Восстанавливает идентификатор дочернего элемента по указанному
  /// имени из адресной строки. Если найти адресуемый элемент не удается,
  /// то метод должен вернуть null.
  /// </summary>
  /// <param name="address">Адрес дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  INodeID ParseAddress(string address);

  /// <summary>Сериализует идентификатор дочернего элемента.</summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <returns>Сериализованное представление идентификатора.</returns>
  PersistentState Serialize(INodeID nodeID);

  /// <summary>
  /// Восстанавливает идентификатор дочернего элемента из
  /// сериализованного представления. Проверять наличие этого элемента
  /// не нужно.
  /// </summary>
  /// <param name="persistNodeID">Сериализованное представление идентификатора элемента.</param>
  /// <returns>Идентификатор дочернего элемента.</returns>
  INodeID Deserialize(PersistentState persistNodeID);

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  object GetData(INodeID nodeID, Type dataFormat);

  /// <summary>
  /// Возвращает данные в указанном формате для каждого дочернего элемента
  /// из коллекции. Если формат не поддерживается, то соответствующий
  /// элемент результата будет содержать null.
  /// </summary>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Массив объектов указанного типа.</returns>
  object[] GetData(NodeIDCollection nodeIDs, Type dataFormat);

  /// <summary>
  /// Возвращает анализатора, который поможет визуальному элементу обработать
  /// событие обновления.
  /// </summary>
  /// <param name="capabilities">Сведения о возможностях визуального элемента.</param>
  /// <param name="sender">Объект, отправивший событие обновления.</param>
  /// <param name="e">Параметры события обновления.</param>
  /// <returns>Анализатор изменений.</returns>
  IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e);

  /// <summary>
  /// Возвращает сервис указанного типа или null, если он не реализован.
  /// </summary>
  /// <param name="service">Тип сервиса</param>
  /// <returns>Сервис</returns>
  object GetService(Type service);
}
