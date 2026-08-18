// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Queries.TechCompositionQuery
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Queries;

/// <summary>Techcard composition query</summary>
public class TechCompositionQuery : RelatedObjectsQuery, IFiltrationClass
{
  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  private readonly List<long> _contexts;

  /// <summary>
  /// Конструктор запроса, в результате выполнения которого будет прочитана
  /// информация о всех объектах, связанных с указанным объектом заданным
  /// типом связи и удовлетворяющих указанным условиям.
  /// </summary>
  /// <param name="objTypeId"></param>
  /// <param name="support"></param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  public TechCompositionQuery(
    INodeQuerySupport support,
    long objId,
    int objTypeId,
    RelatedObjectsRole role,
    int relTypeId,
    ConditionStructure[] conditions,
    string filtrationOwnerID,
    List<long> contexts)
    : base(support, objId, objTypeId, role, relTypeId, conditions)
  {
    this.filtrationOwnerID = filtrationOwnerID != string.Empty ? filtrationOwnerID : "cad001e2-306c-11d8-b4e9-00304f19f545";
    if (contexts == null || contexts.Count <= 0)
      return;
    this._contexts = contexts;
  }

  /// <summary>
  /// Добавляет к параметрам запроса условия, указанные в конструкторе
  /// запроса. Этот метод используется при чтении первой/следующей части
  /// списка объектов.
  /// </summary>
  /// <param name="aMapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <param name="bookmark">Закладка, определяющая позицию для чтения порции</param>
  /// <param name="count">Количество записей, которое должно быть прочитано</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping aMapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, aMapping);
    if (this._contexts != null && queryParams.Tags != null)
      queryParams.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) this._contexts;
    return queryParams;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  /// <returns></returns>
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    return base.GetDataTable(queryParams);
  }

  /// <summary>Ключ настроек фильтрации</summary>
  public string FiltrationOwnerID
  {
    [DebuggerStepThrough] get => this.filtrationOwnerID;
  }
}
