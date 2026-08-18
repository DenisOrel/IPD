
// Type: Intermech.Navigator.DBObjects.TopObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует запрос к базе данных, возвращающий список объектов заданного типа,
/// не входящих в другие объекты этого типа. Объекты этого класса могут
/// применяться для при создании элементов из пространства навигации,
/// являющихся корнями иерархий однотипных объектов (например,
/// групп пользователей, архивов, катагов IMBASE и др).
/// Результаты запроса возвращаются в унифицированном формате, воспринимаемом
/// навигатором, т.е. для каждого объекта предоставляется его идентификатор,
/// поддерживающий интерфейс INodeID, и значения указанных виртуальных колонок.
/// </summary>
/// <summary>
/// Конструктор запроса, в результате выполнения которого будет прочитана
/// информация о всех объектах указанного типа, находящихся на верщине
/// иерархии и удовлетворяющих указанным условиям.
/// </summary>
/// <param name="support"></param>
/// <param name="objTypeID">Идентификатор типа объектов</param>
/// <param name="conditions">Условия, которым должны удовлетворять читаемые объекты</param>
public class TopObjectsQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions) : ObjectsQuery(support, objTypeID, conditions, (IServiceProvider) null)
{
  /// <summary>
  /// Добавляет к параметрам запроса условия, позволяющие прочитать
  /// информацию об объектах, находящихся на верхнем уровне иерархии.
  /// </summary>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <param name="bookmark">Закладка, определяющая позицию для чтения порции</param>
  /// <param name="count">Количество записей, которое должно быть прочитано</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping);
    queryParams.Conditions = ConditionStructure.Join(this.GetLevelConditions(), queryParams.Conditions);
    return queryParams;
  }

  /// <summary>
  /// Добавляет к параметрам запроса условия, позволяющие прочитать
  /// информацию об объектах, находящихся на верхнем уровне иерархии.
  /// </summary>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <param name="recordIds">?Коллекция унифицированных идентификаторов объектов</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(recordIds, mapping);
    queryParams.Conditions = ConditionStructure.Join(this.GetLevelConditions(), queryParams.Conditions);
    return queryParams;
  }

  /// <summary>
  /// Формирует массив условий запроса, позволяющее прочитать информацию об
  /// объектах, находящихся на верхнем уровне иерархии объектов, указанного
  /// типа.
  /// </summary>
  /// <returns></returns>
  private ConditionStructure[] GetLevelConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.NotEntersInType, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(this._objTypeID).ToArray(), LogicalOperators.NONE, 0, false)
    };
  }

  /// <summary>
  /// Создает и возвращает условие, позволяющее найти объекты, находящиеся
  /// на верхнем уровне иерархии, т.е. не входящие в состав объектов
  /// этого же типа.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Условие запроса к базе данных</returns>
  private ConditionStructure GetLevelCondition(int objTypeID)
  {
    return new ConditionStructure((string) null, RelationalOperators.NotEntersInType, (object) objTypeID, LogicalOperators.NONE, 0, false);
  }
}
