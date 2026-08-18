
// Type: Intermech.Navigator.DBObjects.AdvRelationsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Queries;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует запрос к базе данных на чтение инфрормации об объектах из
/// коллекции связей объектов, т.е. позволяет прочитать значения атрибутов не
/// только объектов, но и связей. Результаты запроса возвращаются в
/// унифицированном формате, воспринимаемом навигатором, т.е. для каждого
/// объекта предоставляется его идентификатор, поддерживающий интерфейс INodeID,
/// и значения указанных виртуальных колонок.
/// </summary>
public class AdvRelationsQuery : RelatedObjectsQuery
{
  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  private List<long> _contexts;

  /// <summary>Отключить все возможные фильтрации состава плагинами</summary>
  /// <param name="paramsSet">Параметры запроса в базу данных</param>
  public static void BlockPluginFiltrations(ref DBRecordSetParams paramsSet)
  {
    IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
    paramsSet.Tags = service == null || service.Filtration.Tags == null ? new HybridDictionary(0, true) : service.Filtration.Tags;
    paramsSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true;
    paramsSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
    paramsSet.Tags[(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"] = (object) false;
    paramsSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true;
  }

  /// <summary>
  /// Конструктор запроса, в результате выполнения которого будет прочитана
  /// информация о всех объектах, связанных с указанным объектом заданным
  /// типом связи и удовлетворяющих указанным условиям.
  /// </summary>
  /// <param name="support"></param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  public AdvRelationsQuery(
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
    AdvRelationsDescriptor.CorrectStatics();
    this.relTypeId = relTypeId >= 0 ? relTypeId : AdvRelationsDescriptor.ProjectRelationTypeID;
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
    AdvRelationsQuery.BlockPluginFiltrations(ref queryParams);
    if (this._contexts != null && queryParams.Tags != null)
      queryParams.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) this._contexts;
    return queryParams;
  }
}
