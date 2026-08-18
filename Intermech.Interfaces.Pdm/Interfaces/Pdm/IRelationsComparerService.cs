// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IRelationsComparerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Серверная служба, позволяющая выполнить сравнение указанных связей по ряду критериев.
/// Для каждого критерия создаётся свой класс-сравниватель (IRelationsComparer).
/// Связи признаются идентичными, если все сравниватели вернут true при
/// выполнении их сравнения между собой.
/// </summary>
public interface IRelationsComparerService
{
  /// <summary>
  /// Зарегистрировать класс для сравнения связей между собой
  /// </summary>
  /// <param name="relationsComparer">Класс для сравнения связей между собой</param>
  void RegisterRelationsComparer(IRelationsComparer relationsComparer);

  /// <summary>
  /// Удалить из внутренней коллекции ссылку на класс для сравнения связей между собой
  /// </summary>
  /// <param name="relationsComparer">Класс для сравнения связей между собой</param>
  void UnregisterRelationsComparer(IRelationsComparer relationsComparer);

  /// <summary>
  /// Удалить из внутренней коллекции ссылку на класс для сравнения связей между собой
  /// </summary>
  /// <param name="relationsComparerGuid">Уникальный идентификатор класса для сравнения связей между собой</param>
  void UnregisterRelationsComparer(Guid relationsComparerGuid);

  /// <summary>
  /// Получить список анализаторов для указанного атрибута.
  /// Анализаторы упорядочены по их приоритету работы с атрибутом.
  /// </summary>
  /// <param name="attr">Идентификатор атрибута</param>
  /// <returns>Список анализаторов для указанного атрибута</returns>
  List<IRelationsComparer> GetAttributeComparers(int attr);

  /// <summary>
  /// Сравнить две связи, вернуть true, если они идентичны по указанному атрибуту.
  /// Связи признаются идентичными, если все сравниватели, зарегистрированные в службе,
  /// вернут true во время сравнивания этих связей.
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется сравнение</param>
  /// <param name="attrID">Идентификатор атрибута, по которому требуется выполнить сравнение</param>
  /// <param name="prjLinkID1">Идентификатор первой сравниваемой связи</param>
  /// <param name="prjLinkID2">Идентификатор второй сравниваемой связи</param>
  /// <returns>true, если обе связи идентичны по указанному атрибуту</returns>
  bool EqualsTo(IUserSession session, int attrID, long prjLinkID1, long prjLinkID2);

  /// <summary>
  /// Сравнить две связи, вернуть true, если они идентичны по указанным атрибутам
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется сравнение</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов, по которым требуется выполнить сравнение</param>
  /// <param name="prjLinkID1">Идентификатор первой сравниваемой связи</param>
  /// <param name="prjLinkID2">Идентификатор второй сравниваемой связи</param>
  /// <returns>true, если обе связи идентичны по указанным атрибутам</returns>
  bool EqualsTo(IUserSession session, List<int> attrIDs, long prjLinkID1, long prjLinkID2);

  /// <summary>
  /// Сравнить две связи, вернуть true, если они идентичны по указанному атрибуту
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется сравнение</param>
  /// <param name="attrID">Идентификатор атрибута, по которому требуется выполнить сравнение</param>
  /// <param name="prjLinkID1">Идентификатор первой сравниваемой связи</param>
  /// <param name="prjLinkID2">Идентификатор второй сравниваемой связи</param>
  /// <param name="row1">Строка с данными из таблицы состава для первой связи</param>
  /// <param name="row2">Строка с данными из таблицы состава для второй связи</param>
  /// <param name="useSubstAttrs">Если true, то в таблице колонки заполнены согласно списку из SubstituteObjects</param>
  /// <returns>true, если обе связи идентичны по указанному атрибуту</returns>
  bool EqualsTo(
    IUserSession session,
    int attrID,
    long prjLinkID1,
    long prjLinkID2,
    DataRow row1,
    DataRow row2,
    bool useSubstAttrs);

  /// <summary>
  /// Сравнить две связи, вернуть true, если они идентичны по указанным атрибутам.
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется сравнение</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов, по которым требуется выполнить сравнение</param>
  /// <param name="prjLinkID1">Идентификатор первой сравниваемой связи</param>
  /// <param name="prjLinkID2">Идентификатор второй сравниваемой связи</param>
  /// <param name="row1">Строка с данными из таблицы состава для первой связи</param>
  /// <param name="row2">Строка с данными из таблицы состава для второй связи</param>
  /// <param name="useSubstAttrs">Если true, то в таблице колонки заполнены согласно списку из SubstituteObjects</param>
  /// <returns>true, если обе связи идентичны по указанным атрибутам</returns>
  bool EqualsTo(
    IUserSession session,
    List<int> attrIDs,
    long prjLinkID1,
    long prjLinkID2,
    DataRow row1,
    DataRow row2,
    bool useSubstAttrs);
}
