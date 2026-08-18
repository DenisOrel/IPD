// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureNodePart
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>
/// список назначенных атрибутов
/// для архива (структура архива)
/// </summary>
public class ArchiveStructureNodePart : INodePart, INodeItems, IContextAware, INodeQuerySupport
{
  /// <summary>тип атрибута</summary>
  private const string AttrTypeID = "AttrTypeID";
  /// <summary>id архива, в структуру которого входит атрибут</summary>
  private const string ArcID = "ArcID";
  /// <summary>Владелец данного списка</summary>
  protected object owner;
  /// <summary>Идентификатор выделенного архива</summary>
  protected long arcID;
  /// <summary>Контейнер сервисов</summary>
  protected IServiceProvider services;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="id"> идентификатор архива, труктуру которого надо отобразить</param>
  /// <param name="provider"> Контейнер сервисов</param>
  public ArchiveStructureNodePart(long id, IServiceProvider provider)
  {
    this.arcID = id;
    this.services = provider;
  }

  /// <summary>
  /// Возвращает набор атрибутов указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Набор флагов атрибутов</returns>
  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.None;

  /// <summary>
  /// Возвращает основной интерфейс элемента из пространства навигации
  /// для указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Интерфейс элемента навигации</returns>
  public INode GetChild(INodeID nodeID) => (INode) null;

  /// <summary>
  /// Возвращает адрес дочернего элемента, который может быть использован
  /// в адресной строке.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Адрес дочернего элемента</returns>
  public string GetAddress(INodeID nodeID) => string.Empty;

  /// <summary>
  /// Восстанавливает идентификатор дочернего элемента по указанному
  /// имени из адресной строки. Если найти адресуемый элемент не удается,
  /// то метод должен вернуть null.
  /// </summary>
  /// <param name="address">Адрес дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  public INodeID ParseAddress(string address) => (INodeID) null;

  /// <summary>
  /// Возвращает строковое представление идентификатора, описывающего объект
  /// базы данных.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <returns>Сериализованное представление идентификатора.</returns>
  public PersistentState Serialize(INodeID nodeID)
  {
    ArchiveStructureNodeID archiveStructureNodeId = (ArchiveStructureNodeID) nodeID;
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("AttrTypeID", (object) archiveStructureNodeId.TypeID);
    persistentState.AddValue("ArcID", (object) archiveStructureNodeId.TypeID);
    return persistentState;
  }

  /// <summary>
  /// Восстанавливает унифицированный идентификатор объекта базы данных из
  /// его строкового представления.
  /// </summary>
  /// <param name="persistNodeID">Строковое представление идентификатора</param>
  /// <returns>Унифицированный идентификатор</returns>
  public INodeID Deserialize(PersistentState persistNodeID)
  {
    return (INodeID) new ArchiveStructureNodeID((int) persistNodeID.GetValue("AttrTypeID"), (long) (int) persistNodeID.GetValue("ArcID"));
  }

  /// <summary>
  /// Возвращает данные указанного формата для объекта базы данных с указанным
  /// идентификатором.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор объекта базы данных</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Объект, представляющий данные указанного формата</returns>
  public object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (ArchiveStructureNodeID) ? (object) nodeID : (object) null;
  }

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

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    get => this.services;
    set => this.services = value;
  }

  /// <summary>
  /// Устанавливает или возвращает объект, в состав которого входит эта часть.
  /// </summary>
  public object Owner
  {
    get => this.owner;
    set => this.owner = value;
  }

  /// <summary>
  /// Получить интерфейс объекта-запроса к источнику данных, используемого
  /// для чтения содержимого элементов из пространства навигации
  /// </summary>
  /// <returns>Интерфейс объекта-запроса к источнику данных или null</returns>
  public INodeQuery GetQuery()
  {
    return (INodeQuery) new ArchiveStructureQuery(this.arcID, (INodeQuerySupport) this);
  }

  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    IColumnSchemes service = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
    Guid structureShemeGuid = ArchiveStructureColumnScheme.ArchiveStructureShemeGuid;
    defaultColumns.Add(service.CreateColumn(structureShemeGuid, (object) "F_ATTRIBUTE_ID"));
    defaultColumns.Add(service.CreateColumn(structureShemeGuid, (object) "F_NAME"));
    defaultColumns.Add(service.CreateColumn(structureShemeGuid, (object) "F_SHORT_NAME"));
    defaultColumns.Add(service.CreateColumn(structureShemeGuid, (object) "F_ALIAS"));
    return defaultColumns;
  }

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection supportedColumns = new NodeColumnCollection();
    IColumnSchemes service = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
    Guid structureShemeGuid = ArchiveStructureColumnScheme.ArchiveStructureShemeGuid;
    foreach (string archiveStructureColumn in ConstsHolder.ArchiveStructureColumns)
      supportedColumns.Add(service.CreateColumn(structureShemeGuid, (object) archiveStructureColumn));
    return supportedColumns;
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
    if (!(column.SchemeGuid == ArchiveStructureColumnScheme.ArchiveStructureShemeGuid))
      return (object) null;
    int index = -Convert.ToInt32(column.ID) - 10000;
    return (object) ConstsHolder.ArchiveStructureColumns[index];
  }

  /// <summary>
  /// Возвращает список идентификаторов полей источника данных, значения
  /// которых обязательно должны быть получены в результате выполнения
  /// запроса.
  /// </summary>
  /// <returns>Список идентификаторов полей источника данных</returns>
  public List<object> GetSpecialFields()
  {
    List<object> specialFields = new List<object>();
    if (!specialFields.Contains((object) "F_ATTRIBUTE_ID"))
      specialFields.Add((object) "F_ATTRIBUTE_ID");
    if (!specialFields.Contains((object) "F_NAME"))
      specialFields.Add((object) "F_NAME");
    return specialFields;
  }

  /// <summary>
  /// Создает и возвращает унифицированный идентификатор элемента навигации.
  /// </summary>
  /// <param name="fieldValues">Значения полей, полученных от источника данных</param>
  /// <param name="adapter">Адаптер полей источника данных</param>
  /// <returns>Унифицированный идентификатор элемента навигации</returns>
  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) "F_ATTRIBUTE_ID")]));
    return attributeType == null ? (INodeID) null : (INodeID) new ArchiveStructureNodeID(attributeType.AttributeID, this.arcID);
  }

  /// <summary>
  /// Создает и возвращает идентификатор элемента в источнике данных по
  /// его унифицированному идентификатору.
  /// </summary>
  /// <param name="nodeId">Унифицированный идентификатор элемента навигации</param>
  /// <returns>Идентификатор соответствующего элемента в источнике данных</returns>
  public object CreateRecordId(INodeID nodeId)
  {
    return (object) MetaDataHelper.GetAttributeType(((ArchiveStructureNodeID) nodeId).TypeID);
  }
}
