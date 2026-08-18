
// Type: Intermech.Navigator.Parts.DescriptorsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

/// <summary>
/// Часть элемента пространства навигации, позволяющая отобразить в своём составе элементы по списку их дескрипторов
/// </summary>
public class DescriptorsPart : INodePart, INodeItems
{
  /// <summary>Коллекция дескрипторов</summary>
  protected DescriptorCollection _descriptors;
  /// <summary>Требуется ли сортировать запросы</summary>
  protected bool _sortedQueries;
  /// <summary>Владелец части</summary>
  protected object _owner;
  /// <summary>Интерфейс по обработке колонки "Статусы элемента"</summary>
  private INodeStatusesInfo statusesInfoService;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="descriptors">Список дескрипторов</param>
  public DescriptorsPart(DescriptorCollection descriptors)
    : this(descriptors, true)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, указать, требуется ли сортировка дочерних элементов
  /// </summary>
  /// <param name="descriptors">Список дескрипторов</param>
  /// <param name="sortedQueries">Если true, то дочерние элементы будут участвовать в сортировке</param>
  public DescriptorsPart(DescriptorCollection descriptors, bool sortedQueries)
  {
    this._descriptors = descriptors;
    this._sortedQueries = sortedQueries;
  }

  public DescriptorCollection Descriptors => this._descriptors;

  /// <summary>
  /// Устанавливает или возвращает объект, в состав которого входит эта часть.
  /// </summary>
  public object Owner
  {
    get => this._owner;
    set => this._owner = value;
  }

  /// <summary>
  /// Получить интерфейс объекта-запроса к источнику данных, используемого
  /// для чтения содержимого элементов из пространства навигации
  /// </summary>
  /// <returns>Интерфейс объекта-запроса к источнику данных или null</returns>
  public virtual INodeQuery GetQuery()
  {
    return (INodeQuery) new DescriptorsQuery(this._descriptors, this._sortedQueries);
  }

  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public virtual NodeColumnCollection GetDefaultColumns()
  {
    return new NodeColumnCollection()
    {
      Holder.ColumnSchemes.CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION")
    };
  }

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  public virtual NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return new NodeColumnCollection()
    {
      Holder.ColumnSchemes.CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION")
    };
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (String.Empty)
  /// </summary>
  /// <returns>Список поддерживаемых названий наборов колонок</returns>
  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  /// <summary>
  /// Возвращает набор атрибутов указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Набор флагов атрибутов</returns>
  public ContentAttributes GetAttributesOf(INodeID nodeID)
  {
    IDescriptor descriptor = this.GetDescriptor(nodeID);
    return descriptor != null ? descriptor.GetAttributesOf(nodeID) : ContentAttributes.None;
  }

  /// <summary>
  /// Возвращает основной интерфейс элемента из пространства навигации
  /// для указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Интерфейс элемента навигации</returns>
  public INode GetChild(INodeID nodeID) => this.GetDescriptor(nodeID)?.GetChild(nodeID);

  /// <summary>
  /// Возвращает адрес дочернего элемента, который может быть использован
  /// в адресной строке.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Адрес дочернего элемента</returns>
  public string GetAddress(INodeID nodeID)
  {
    IDescriptor descriptor = this.GetDescriptor(nodeID);
    return descriptor != null ? descriptor.GetAddress(nodeID) : string.Empty;
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
    lock (this._descriptors.SyncRoot)
    {
      for (int index = 0; index < this._descriptors.Count; ++index)
      {
        INodeID address1 = this._descriptors[index].ParseAddress(address);
        if (address1 != null)
        {
          address1.Cookie = (object) new DescriptorCookie(this._descriptors.GetUniqueId(index));
          return address1;
        }
      }
    }
    return (INodeID) null;
  }

  /// <summary>Сериализует идентификатор дочернего элемента.</summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <returns>Сериализованное представление идентификатора.</returns>
  public PersistentState Serialize(INodeID nodeID)
  {
    IDescriptor descriptor = this.GetDescriptor(nodeID);
    if (descriptor != null)
    {
      PersistentState persistentState = descriptor.Serialize(nodeID);
      if (persistentState != null)
      {
        DescriptorCookie cookie = (DescriptorCookie) nodeID.Cookie;
        persistentState.AddValue("DescriptorId", (object) PartGuidMapper.GetGuid(cookie.DescriptorId));
        return persistentState;
      }
    }
    return (PersistentState) null;
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
    int uniqueId = PartGuidMapper.GetUniqueId((Guid) persistNodeID.GetValue("DescriptorId"));
    IDescriptor descriptor = this.GetDescriptor(uniqueId);
    if (descriptor != null)
    {
      INodeID nodeId = descriptor.Deserialize(persistNodeID);
      if (nodeId != null)
      {
        nodeId.Cookie = (object) new DescriptorCookie(uniqueId);
        return nodeId;
      }
    }
    return (INodeID) null;
  }

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public object GetData(INodeID nodeID, Type dataFormat)
  {
    return this.GetDescriptor(nodeID)?.GetData(nodeID, dataFormat);
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
    List<AnalyserSlot> nativeAnalysers = new List<AnalyserSlot>();
    lock (this._descriptors.SyncRoot)
    {
      for (int index = 0; index < this._descriptors.Count; ++index)
      {
        IUpdateAnalyser analyser = this._descriptors[index].GetAnalyser(capabilities, sender, e);
        if (analyser != null)
          nativeAnalysers.Add(new AnalyserSlot(this._descriptors.GetUniqueId(index), analyser));
      }
    }
    return nativeAnalysers.Count <= 0 ? (IUpdateAnalyser) null : (IUpdateAnalyser) new DescriptorsUpdateAnalyser(nativeAnalysers);
  }

  /// <summary>
  /// Возвращает сервис указанного типа или null, если он не реализован.
  /// </summary>
  /// <param name="service">Тип сервиса</param>
  /// <returns>Сервис</returns>
  public object GetService(Type service)
  {
    if (!(service == typeof (INodeStatusesInfo)))
      return (object) null;
    if (this.statusesInfoService == null)
      this.statusesInfoService = (INodeStatusesInfo) (ServicesManager.GetService(typeof (StatusesInfoService)) as StatusesInfoService);
    return (object) this.statusesInfoService;
  }

  /// <summary>
  /// Вернуть дескриптор для указанного идентификатора элемента пространства навигации
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <returns>Дескриптор для указанного идентификатора элемента пространства навигации</returns>
  private IDescriptor GetDescriptor(INodeID nodeID)
  {
    return nodeID.Cookie is DescriptorCookie ? this.GetDescriptor(((DescriptorCookie) nodeID.Cookie).DescriptorId) : (IDescriptor) null;
  }

  /// <summary>Вернуть дескриптор по его идентификатору</summary>
  /// <param name="descriptorId">Идентификатор дескриптора</param>
  /// <returns>Дескриптор по его идентификатору</returns>
  private IDescriptor GetDescriptor(int descriptorId)
  {
    return this._descriptors.FindDescriptor(descriptorId);
  }
}
