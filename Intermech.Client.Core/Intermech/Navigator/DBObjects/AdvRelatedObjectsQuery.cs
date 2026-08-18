
// Type: Intermech.Navigator.DBObjects.AdvRelatedObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Navigator.VirtualColumns;
using System.Data;


namespace Intermech.Navigator.DBObjects;

/// <summary>Расширение функциональности объекта, выбирающего данные из БД для создания состава объектов, входящих в другой объект</summary>
public class AdvRelatedObjectsQuery : 
  RelatedObjectsQuery,
  IContextAware,
  IFiltrateVersionsLogHolder,
  INodeQuery
{
  /// <summary>Номер колонки с идентификатором связи в полученном наборе данных</summary>
  protected internal int _prjLinkIDColumnNum = -1;
  /// <summary>Номер колонки с гуидом связи в полученном наборе данных</summary>
  protected internal int _prjLinkGuidColumnNum = -1;

  /// <summary>Конструктор запроса, в результате выполнения которого будет прочитана информация о всех объектах, связанных с указанным
  /// объектом заданным типом связи и удовлетворяющих указанным условиям.</summary>
  /// <param name="support"></param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="objTypeID"></param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  public AdvRelatedObjectsQuery(
    [NotNull] INodeQuerySupport support,
    [NotEmpty] long objId,
    [NotEmpty] int objTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int relTypeId,
    [NotNull] ConditionStructure[] conditions)
    : base(support, objId, objTypeID, role, relTypeId, conditions)
  {
  }

  /// <summary>Конструктор запроса, в результате выполнения которого будет прочитана информация о всех объектах, связанных с указанным
  /// объектом заданным типом связи и удовлетворяющих указанным условиям.</summary>
  /// <param name="support"></param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="objTypeID"></param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="parentObjTypeID">Идентификатор родительского типа объектов для типизированного запроса в коллекцию связей</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  public AdvRelatedObjectsQuery(
    [NotNull] INodeQuerySupport support,
    [NotEmpty] long objId,
    [NotEmpty] int objTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int relTypeId,
    [CanBeEmpty] int parentObjTypeID,
    [NotNull] ConditionStructure[] conditions)
    : base(support, objId, objTypeID, role, relTypeId, parentObjTypeID, conditions)
  {
  }

  /// <summary>Возвращает таблицу, содержащую результаты запроса. Базовый класс вызывает этот метод, чтобы получить результаты запроса в
  /// формате источника данных, а затем транслирует их в унифицированный формат, понятный навигатору.</summary>
  /// <param name="queryParams">Параметры запроса к базе данных</param>
  /// <returns>Таблица с значениями атрибутов объектов</returns>
  [CanBeNull]
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    DataTable dataTable = base.GetDataTable(queryParams);
    this.mapping.CheckFieldIndex(ref this._prjLinkIDColumnNum, RelatedPartBase.ncF_PRJLINK_ID, "F_PRJLINK_ID");
    this.mapping.CheckFieldIndex(ref this._prjLinkGuidColumnNum, RelatedPartBase.ncF_PRJ_GUID, "F_PRJ_GUID");
    if (dataTable != null)
      VirtualQueryResultColumn.AddVirtualColumns(dataTable, this.mapping);
    return dataTable;
  }
}
