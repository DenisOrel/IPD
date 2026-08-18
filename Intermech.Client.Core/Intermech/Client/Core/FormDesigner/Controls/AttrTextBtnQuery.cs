
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextBtnQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class AttrTextBtnQuery : RelatedObjectsQuery
{
  private int _childObjTypeID = -1;

  /// <summary>
  /// Конструктор запроса, в результате выполнения которого будет прочитана информация о всех объектах, связанных с указанным объектом заданным типом связи и удовлетворяющих указанным условиям.
  /// </summary>
  /// <param name="support">Создающий Part</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="childObjTypeID"></param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  public AttrTextBtnQuery(
    INodeQuerySupport support,
    int objTypeID,
    long objID,
    int childObjTypeID,
    int relTypeID,
    ConditionStructure[] conditions)
    : base(support, objID, objTypeID, RelatedObjectsRole.Composition, relTypeID, conditions)
  {
    this._childObjTypeID = childObjTypeID;
  }

  /// <summary>
  /// Добавляет к параметрам запроса условия, указанные в конструкторе запроса. Этот метод используется при чтении первой/следующей части списка объектов.
  /// </summary>
  /// <param name="bookmark">Закладка, определяющая позицию для чтения порции</param>
  /// <param name="count">Количество записей, которое должно быть прочитано</param>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping);
    ConditionStructure joinedCondition = new ConditionStructure(-7, RelationalOperators.Equal, (object) this._childObjTypeID, LogicalOperators.NONE, 0, false);
    queryParams.Conditions = ConditionStructure.Join(joinedCondition, queryParams.Conditions);
    return queryParams;
  }
}
