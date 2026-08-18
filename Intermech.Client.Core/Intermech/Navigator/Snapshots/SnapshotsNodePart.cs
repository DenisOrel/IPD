
// Type: Intermech.Navigator.Snapshots.SnapshotsNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Snapshots;

public class SnapshotsNodePart : INodePart, INodeItems, IContextAware, INodeQuerySupport
{
  /// <summary>
  /// Идентификатор объекта, итерации которого хотим увидеть
  /// </summary>
  private long id;
  /// <summary>
  /// Идентификатор версии объекта, итерации которого хотим увидеть
  /// </summary>
  private long objectId;
  /// <summary>Владелец данного списка</summary>
  protected object owner;
  /// <summary>Контейнер сервисов</summary>
  protected IServiceProvider services;
  private const string SNAPSHOTNODEID = "SnapshotNodeID";
  private const string OBJECTID = "ObjectID";
  private const string ID = "ID";
  private const string LCSTEP = "LCSTEP";
  private const string VERSIONID = "VERSIONID";
  private const string OBJTYPE = "OBJTYPE";
  private const string OWNERID = "OWNERID";
  private const string MODIFYDATE = "MODIFYDATE";
  private const string LEVELID = "LEVELID";
  private const string OBJCREATE = "OBJCREATE";
  private const string PROJID = "PROJID";
  private const string MODIFID = "MODIFID";
  private const string CAPTION = "CAPTION";
  private const string SITEID = "SITEID";
  private const string NOTE = "NOTE";
  private const string USERID = "USERID";
  private const string SNAPDATE = "SNAPDATE";

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="id"></param>
  /// <param name="objectId"></param>
  /// <param name="services"></param>
  public SnapshotsNodePart(long id, long objectId, IServiceProvider services)
  {
    this.id = id;
    this.objectId = objectId;
    this.services = services;
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
    SnapshotsNodeID snapshotsNodeId = (SnapshotsNodeID) nodeID;
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("SnapshotNodeID", (object) snapshotsNodeId.SnapshotID);
    persistentState.AddValue("ObjectID", (object) snapshotsNodeId.ObjectID);
    persistentState.AddValue("ID", (object) snapshotsNodeId.ID);
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
    long snapshotID = (long) persistNodeID.GetValue("SnapshotNodeID");
    long num1 = (long) persistNodeID.GetValue("ObjectID");
    long num2 = (long) persistNodeID.GetValue("ID");
    int num3 = (int) persistNodeID.GetValue("OBJTYPE");
    string str = persistNodeID.GetValue("NOTE").ToString();
    long num4 = (long) persistNodeID.GetValue("USERID");
    DateTime dateTime = (DateTime) persistNodeID.GetValue("SNAPDATE");
    long objectID = num1;
    long id = num2;
    int objType = num3;
    string name = str;
    long userID = num4;
    DateTime snapDate = dateTime;
    return (INodeID) new SnapshotsNodeID(snapshotID, objectID, id, objType, name, userID, snapDate);
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
    return dataFormat == typeof (SnapshotsNodeID) ? (object) nodeID : (object) null;
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
    return (INodeQuery) new SnapshotsQuery(this.objectId, this.id, (INodeQuerySupport) this);
  }

  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public NodeColumnCollection GetDefaultColumns() => SnapshotConsts.SnapshotGridColumns();

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return SnapshotConsts.SnapshotGridColumns();
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
  public object MapColumnToField(NodeColumn column) => column.ID;

  /// <summary>
  /// Возвращает список идентификаторов полей источника данных, значения
  /// которых обязательно должны быть получены в результате выполнения
  /// запроса.
  /// </summary>
  /// <returns>Список идентификаторов полей источника данных</returns>
  public List<object> GetSpecialFields()
  {
    List<object> specialFields = new List<object>();
    if (!specialFields.Contains((object) SnapshotConsts.SNAPSHOT_ID))
      specialFields.Add((object) SnapshotConsts.SNAPSHOT_ID);
    if (!specialFields.Contains((object) ObligatoryObjectAttributes.F_OBJECT_ID))
      specialFields.Add((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    if (!specialFields.Contains((object) ObligatoryObjectAttributes.F_ID))
      specialFields.Add((object) ObligatoryObjectAttributes.F_ID);
    if (!specialFields.Contains((object) SnapshotConsts.SNAPSHOT_DATE))
      specialFields.Add((object) SnapshotConsts.SNAPSHOT_DATE);
    if (!specialFields.Contains((object) ObligatoryObjectAttributes.F_OBJECT_TYPE))
      specialFields.Add((object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
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
    int fieldIndex1 = adapter.GetFieldIndex((object) SnapshotConsts.SNAPSHOT_ID);
    long int64_1 = Convert.ToInt64(fieldValues[fieldIndex1]);
    int fieldIndex2 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    long int64_2 = Convert.ToInt64(fieldValues[fieldIndex2]);
    int fieldIndex3 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_ID);
    long int64_3 = Convert.ToInt64(fieldValues[fieldIndex3]);
    int fieldIndex4 = adapter.GetFieldIndex((object) SnapshotConsts.SNAPSHOT_DATE);
    DateTime dateTime = Convert.ToDateTime(fieldValues[fieldIndex4]);
    int fieldIndex5 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
    int num = fieldIndex5 != -1 ? Convert.ToInt32(fieldValues[fieldIndex5]) : -1;
    int fieldIndex6 = adapter.GetFieldIndex((object) SnapshotConsts.F_NAME);
    string str = fieldIndex6 != -1 ? fieldValues[fieldIndex6].ToString() : string.Empty;
    int fieldIndex7 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_USER_ID);
    long int64_4 = fieldIndex7 != -1 ? Convert.ToInt64(fieldValues[fieldIndex7]) : 0L;
    long objectID = int64_2;
    long id = int64_3;
    int objType = num;
    string name = str;
    long userID = int64_4;
    DateTime snapDate = dateTime;
    return (INodeID) new SnapshotsNodeID(int64_1, objectID, id, objType, name, userID, snapDate);
  }

  /// <summary>
  /// Создает и возвращает идентификатор элемента в источнике данных по
  /// его унифицированному идентификатору.
  /// </summary>
  /// <param name="nodeId">Унифицированный идентификатор элемента навигации</param>
  /// <returns>Идентификатор соответствующего элемента в источнике данных</returns>
  public object CreateRecordId(INodeID nodeId) => (object) ((SnapshotsNodeID) nodeId).SnapshotID;
}
