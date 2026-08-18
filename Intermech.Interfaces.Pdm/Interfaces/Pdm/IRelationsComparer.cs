// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IRelationsComparer
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс для сравнения связей по указанным атрибутам</summary>
public interface IRelationsComparer
{
  /// <summary>
  /// Уникальный идентификатор класса, реализующего данный интерфейс.
  /// Используется при регистрации/удалении этого класса в сервисе IRelationsCompaperService
  /// </summary>
  Guid ComparerGuid { get; }

  /// <summary>
  /// Возможности указанного интерфейса по сравнению атрибутов
  /// </summary>
  RelationsAttributeComparerCaps Capabilities { get; }

  /// <summary>
  /// Проверить, умеет ли интерфейс выполнять сравнение по указанному атрибуту
  /// </summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>true - интерфейс умеет выполнять сравнение по указанному атрибуту</returns>
  bool CanCompareByAttribute(int attrID);

  /// <summary>
  /// Список атрибутов, по которым данный интерфейс умеет выполнять сравнение.
  /// Пустой список означает то, что интерфейс умеет работать с любыми атрибутами.
  /// Значение Null означает то, что интерфейс не умеет работать ни с одним из атрибутов.
  /// </summary>
  List<int> SupportedAttributes { get; }

  /// <summary>
  /// Сравнить две связи, вернуть true, если они идентичны по указанному атрибуту
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
