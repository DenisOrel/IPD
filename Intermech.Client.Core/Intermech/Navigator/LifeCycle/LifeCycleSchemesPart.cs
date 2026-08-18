
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemesPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Список схем жизненных циклов</summary>
public class LifeCycleSchemesPart : IContextAware, INodeItems, INodePart, INodeQuerySupport
{
  /// <summary>Контейнер сервисов</summary>
  protected IServiceProvider services;
  /// <summary>Владелец данного списка</summary>
  protected object owner;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="services">Контейнер сервисов</param>
  public LifeCycleSchemesPart(IServiceProvider services) => this.services = services;

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    [DebuggerStepThrough] set => this.services = value;
  }

  /// <summary>
  /// Возвращает набор атрибутов указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Набор флагов атрибутов</returns>
  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.Folder;

  /// <summary>
  /// Возвращает основной интерфейс элемента из пространства навигации
  /// для указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Интерфейс элемента навигации</returns>
  public INode GetChild(INodeID nodeID)
  {
    LifeCycleSchemeNodeID nodeID1 = nodeID as LifeCycleSchemeNodeID;
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    if (nodeID1 == null)
      return (INode) null;
    return service.GetNode((INodeID) nodeID1, (object) nodeID1.id);
  }

  /// <summary>
  /// Возвращает адрес дочернего элемента, который может быть использован
  /// в адресной строке.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Адрес дочернего элемента</returns>
  public string GetAddress(INodeID nodeID)
  {
    return nodeID is LifeCycleSchemeNodeID cycleSchemeNodeId ? MetaDataHelper.GetLCSchemaName(cycleSchemeNodeId.id) : string.Empty;
  }

  /// <summary>
  /// Восстанавливает идентификатор дочернего элемента по указанному
  /// имени из адресной строки. Если найти адресуемый элемент не удается,
  /// то метод должен вернуть null.
  /// </summary>
  /// <param name="address">Адрес дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  public INodeID ParseAddress(string address)
  {
    List<IMSLifeCycleScheme> lcSchemesList = MetaDataHelper.GetLCSchemesList();
    for (int index = 0; index < lcSchemesList.Count; ++index)
    {
      if (lcSchemesList[index].Name == address)
        return (INodeID) new LifeCycleSchemeNodeID(lcSchemesList[index].SchemaID);
    }
    return (INodeID) null;
  }

  /// <summary>Сериализует идентификатор дочернего элемента.</summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <returns>Сериализованное представление идентификатора.</returns>
  public PersistentState Serialize(INodeID nodeID)
  {
    if (!(nodeID is LifeCycleSchemeNodeID cycleSchemeNodeId))
      return (PersistentState) null;
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("id", (object) cycleSchemeNodeId.id);
    return persistentState;
  }

  /// <summary>
  /// Восстанавливает идентификатор дочернего элемента из
  /// сериализованного представления. Проверять наличие этого элемента
  /// не нужно.
  /// </summary>
  /// <param name="persistNodeID">Сериализованное представление идентификатора элемента.</param>
  /// <returns>Идентификатор дочернего элемента.</returns>
  public INodeID Deserialize(PersistentState persistNodeID)
  {
    if (persistNodeID == null)
      return (INodeID) null;
    object obj = persistNodeID.GetValue("id");
    return obj != null && obj is int id ? (INodeID) new LifeCycleSchemeNodeID(id) : (INodeID) null;
  }

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public object GetData(INodeID nodeID, Type dataFormat) => (object) null;

  /// <summary>
  /// Возвращает данные в указанном формате для каждого дочернего элемента
  /// из коллекции. Если формат не поддерживается, то соответствующий
  /// элемент результата будет содержать null.
  /// </summary>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Массив объектов указанного типа.</returns>
  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    for (int index = 0; index < data.Length; ++index)
      data[index] = this.GetData(nodeIDs[index], dataFormat);
    return data;
  }

  /// <summary>
  /// Возвращает анализатора, который поможет визуальному элементу обработать
  /// событие обновления.
  /// </summary>
  /// <param name="capabilities">Сведения о возможностях визуального элемента.</param>
  /// <param name="sender">Объект, отправивший событие обновления.</param>
  /// <param name="e">Параметры события обновления.</param>
  /// <returns>Анализатор изменений.</returns>
  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return (IUpdateAnalyser) null;
  }

  /// <summary>
  /// Возвращает сервис указанного типа или null, если он не реализован.
  /// </summary>
  /// <param name="service">Тип сервиса</param>
  /// <returns>Сервис</returns>
  public object GetService(Type service) => (object) null;

  /// <summary>
  /// Устанавливает или возвращает объект, в состав которого входит эта часть.
  /// </summary>
  public object Owner
  {
    [DebuggerStepThrough] get => this.owner;
    [DebuggerStepThrough] set => this.owner = value;
  }

  /// <summary>
  /// Получить интерфейс объекта-запроса к источнику данных, используемого
  /// для чтения содержимого элементов из пространства навигации
  /// </summary>
  /// <returns>Интерфейс объекта-запроса к источнику данных или null</returns>
  public INodeQuery GetQuery()
  {
    if (this.Owner is IContextAware owner)
    {
      IServiceProvider services = owner.Services;
    }
    return (INodeQuery) new LifeCycleSchemesQuery((INodeQuerySupport) this);
  }

  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public NodeColumnCollection GetDefaultColumns()
  {
    return new NodeColumnCollection()
    {
      (ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes).CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) LifeCycleSchemesQuery.CAPTION)
    };
  }

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return new NodeColumnCollection()
    {
      (ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes).CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) LifeCycleSchemesQuery.CAPTION)
    };
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (String.Empty)
  /// </summary>
  /// <returns>Список поддерживаемых названий наборов колонок</returns>
  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  /// <summary>
  /// Возвращает идентификатор поля источника данных для указанной
  /// виртуальной колонки. Если данная колонка не поддерживается, то
  /// метод должен вернуть null.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <returns>Идентификатор поля источника данных</returns>
  public object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && (column.ID.Equals((object) LifeCycleSchemesQuery.CAPTION) || column.ID.Equals((object) LifeCycleSchemesQuery.ncImsScheme)) || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid ? column.ID : (object) null;
  }

  /// <summary>
  /// Возвращает список идентификаторов полей источника данных, значения
  /// которых обязательно должны быть получены в результате выполнения
  /// запроса.
  /// </summary>
  /// <returns>Список идентификаторов полей источника данных</returns>
  public List<object> GetSpecialFields()
  {
    return new List<object>()
    {
      (object) LifeCycleSchemesQuery.ncImsScheme
    };
  }

  /// <summary>
  /// Создает и возвращает унифицированный идентификатор элемента навигации.
  /// </summary>
  /// <param name="fieldValues">Значения полей, полученных от источника данных</param>
  /// <param name="adapter">Адаптер полей источника данных</param>
  /// <returns>Унифицированный идентификатор элемента навигации</returns>
  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    return !(fieldValues[adapter.GetFieldIndex((object) LifeCycleSchemesQuery.ncImsScheme)] is IMSLifeCycleScheme fieldValue) ? (INodeID) null : (INodeID) new LifeCycleSchemeNodeID(fieldValue.SchemaID);
  }

  /// <summary>
  /// Создает и возвращает идентификатор элемента в источнике данных по
  /// его унифицированному идентификатору.
  /// </summary>
  /// <param name="nodeId">Унифицированный идентификатор элемента навигации</param>
  /// <returns>Идентификатор соответствующего элемента в источнике данных</returns>
  public object CreateRecordId(INodeID nodeId)
  {
    return (object) MetaDataHelper.GetLCSchema(((LifeCycleSchemeNodeID) nodeId).id);
  }
}
