
// Type: Intermech.Navigator.Parts.CompositeNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.Parts;

/// <summary>
/// Базовый класс для реализации элементов пространства навигации
/// </summary>
public class CompositeNode : INode, INodeItems
{
  /// <summary>Слоты-папки</summary>
  protected List<PartSlot> folderSlots;
  /// <summary>Слоты-не-папки (аналог файлов в файловых системах)</summary>
  protected List<PartSlot> nonFolderSlots;
  /// <summary>Ссылка на сервис статусов "Навигатора"</summary>
  protected INodeStatusesInfo statusesInfoService;
  /// <summary>Идентификатор папки</summary>
  public const int FolderId = 1073741824 /*0x40000000*/;
  /// <summary>ХЕЗ</summary>
  public const int PrecisePartId = 1073741823 /*0x3FFFFFFF*/;
  /// <summary>Тип - папка</summary>
  public const byte FolderPartKind = 1;
  /// <summary>Тип - не-папка</summary>
  private const byte NonFolderPartKind = 0;
  /// <summary>Набор свойств узла</summary>
  protected NodeOptions options;
  /// <summary>В узле содержится только один слот</summary>
  public static readonly Guid SinglePartGuid = new Guid("BA71F66E-AB60-4a32-A6F7-32386773E1DC");
  /// <summary>
  /// Список пустых слотов (если не требуется отображать у узла какой-либо состав)
  /// </summary>
  private static readonly List<PartSlot> EmptySlots = new List<PartSlot>(0);

  /// <summary>
  /// Набор дополнительных свойств, которые присущи указанному элементу пространства навигации
  /// </summary>
  public virtual NodeOptions Options
  {
    [DebuggerStepThrough] get => this.options;
    set => this.options = value;
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого можно
  /// прочитать список дочерних элементов. Если у данного элемента нет
  /// дочерних, то метод вернет null.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип читаемых дочерних элементов</param>
  /// <returns>Интерфейс запроса</returns>
  public virtual INodeQuery GetQuery(ContentType content)
  {
    List<QuerySlot> subQueries = new List<QuerySlot>();
    if ((content & ContentType.Folders) != ContentType.None && this.FolderSlots != null)
    {
      for (int index = 0; index < this.FolderSlots.Count; ++index)
      {
        INodeQuery query = this.FolderSlots[index].Object.GetQuery();
        if (query != null)
          subQueries.Add(new QuerySlot(this.FolderSlots[index].UniqueId, query));
      }
    }
    if ((content & ContentType.NonFolders) != ContentType.None && this.NonFolderSlots != null)
    {
      for (int index = 0; index < this.NonFolderSlots.Count; ++index)
      {
        INodeQuery query = this.NonFolderSlots[index].Object.GetQuery();
        if (query != null)
          subQueries.Add(new QuerySlot(this.NonFolderSlots[index].UniqueId, query));
      }
    }
    return subQueries.Count == 0 ? (INodeQuery) null : this.CreateCompositeQuery(subQueries);
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public virtual NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    NodeColumnCollection defaultColumns1 = new NodeColumnCollection();
    if ((content & ContentType.Folders) != ContentType.None && this.FolderSlots != null)
    {
      for (int index1 = 0; index1 < this.FolderSlots.Count; ++index1)
      {
        NodeColumnCollection defaultColumns2 = this.FolderSlots[index1].Object.GetDefaultColumns();
        for (int index2 = 0; index2 < defaultColumns2.Count; ++index2)
        {
          if (defaultColumns1.Find((object) defaultColumns2[index2]) == null)
            defaultColumns1.Add(defaultColumns2[index2]);
        }
      }
    }
    if ((content & ContentType.NonFolders) != ContentType.None && this.NonFolderSlots != null)
    {
      for (int index3 = 0; index3 < this.NonFolderSlots.Count; ++index3)
      {
        NodeColumnCollection defaultColumns3 = this.NonFolderSlots[index3].Object.GetDefaultColumns();
        for (int index4 = 0; index4 < defaultColumns3.Count; ++index4)
        {
          if (!defaultColumns1.Contains(defaultColumns3[index4]))
            defaultColumns1.Add(defaultColumns3[index4]);
        }
      }
    }
    return defaultColumns1;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public virtual NodeColumnCollection GetSupportedColumns(ContentType content, string ColumnSetName)
  {
    NodeColumnCollection supportedColumns1 = new NodeColumnCollection();
    if ((content & ContentType.Folders) != ContentType.None && this.FolderSlots != null)
    {
      for (int index1 = 0; index1 < this.FolderSlots.Count; ++index1)
      {
        NodeColumnCollection supportedColumns2 = this.FolderSlots[index1].Object.GetSupportedColumns(ColumnSetName);
        for (int index2 = 0; index2 < supportedColumns2.Count; ++index2)
        {
          if (!supportedColumns1.Contains(supportedColumns2[index2]))
            supportedColumns1.Add(supportedColumns2[index2]);
        }
      }
    }
    if ((content & ContentType.NonFolders) != ContentType.None && this.NonFolderSlots != null)
    {
      for (int index3 = 0; index3 < this.NonFolderSlots.Count; ++index3)
      {
        NodeColumnCollection supportedColumns3 = this.NonFolderSlots[index3].Object.GetSupportedColumns(ColumnSetName);
        for (int index4 = 0; index4 < supportedColumns3.Count; ++index4)
        {
          if (!supportedColumns1.Contains(supportedColumns3[index4]))
            supportedColumns1.Add(supportedColumns3[index4]);
        }
      }
    }
    return supportedColumns1;
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (Intermech.Navigator.Consts.NavigatorDefaultColumnSetName)
  /// </summary>
  /// <returns>Список поддерживаемых названий наборов колонок</returns>
  public virtual List<string> GetSupportedColumnSetNames() => (List<string>) null;

  /// <summary>Обновляет внутренние структуры элемента навигации.</summary>
  public virtual void Refresh()
  {
    this.folderSlots = (List<PartSlot>) null;
    this.nonFolderSlots = (List<PartSlot>) null;
  }

  /// <summary>
  /// Возвращает набор атрибутов указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Набор флагов атрибутов</returns>
  public virtual ContentAttributes GetAttributesOf(INodeID nodeID)
  {
    ContentAttributes attributesOf = ContentAttributes.None;
    PartSlot slot = this.FindSlot(nodeID);
    if (slot != null)
    {
      if ((slot.UniqueId & 1073741824 /*0x40000000*/) != 0)
        attributesOf |= ContentAttributes.Folder;
      attributesOf |= slot.Object.GetAttributesOf(nodeID);
    }
    return attributesOf;
  }

  /// <summary>
  /// Возвращает основной интерфейс элемента из пространства навигации
  /// для указанного дочернего элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Интерфейс элемента навигации</returns>
  public virtual INode GetChild(INodeID nodeID)
  {
    PartSlot slot = this.FindSlot(nodeID);
    return slot == null || (slot.UniqueId & 1073741824 /*0x40000000*/) == 0 ? (INode) null : slot.Object.GetChild(nodeID);
  }

  /// <summary>
  /// Возвращает адрес дочернего элемента, который может быть использован
  /// в адресной строке.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента</param>
  /// <returns>Адрес дочернего элемента</returns>
  public string GetAddress(INodeID nodeID)
  {
    PartSlot slot = this.FindSlot(nodeID);
    return slot == null || (slot.UniqueId & 1073741824 /*0x40000000*/) == 0 ? (string) null : slot.Object.GetAddress(nodeID);
  }

  /// <summary>
  /// Восстанавливает идентификатор дочернего элемента по указанному
  /// имени из адресной строки. Если найти адресуемый элемент не удается,
  /// то метод должен вернуть null.
  /// </summary>
  /// <param name="address">Адрес дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  public virtual INodeID ParseAddress(string address)
  {
    if (this.FolderSlots != null)
    {
      for (int index = 0; index < this.FolderSlots.Count; ++index)
      {
        INodeID address1 = this.FolderSlots[index].Object.ParseAddress(address);
        if (address1 != null)
        {
          if (address1.Cookie == null)
            address1.Cookie = (object) new PartCookie();
          ((PartCookie) address1.Cookie).PartId = this.FolderSlots[index].UniqueId;
          return address1;
        }
      }
    }
    return (INodeID) null;
  }

  /// <summary>Сериализует идентификатор дочернего элемента.</summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <returns>Сериализованное представление идентификатора.</returns>
  public virtual PersistentState Serialize(INodeID nodeID)
  {
    PartSlot slot = this.FindSlot(nodeID);
    if (slot != null)
    {
      PersistentState persistentState = slot.Object.Serialize(nodeID);
      if (persistentState != null)
      {
        int partId = ((PartCookie) nodeID.Cookie).PartId;
        int uniqueId = partId & 1073741823 /*0x3FFFFFFF*/;
        persistentState.AddValue("PartKind", (object) (byte) ((partId & 1073741824 /*0x40000000*/) != 0 ? 1 : 0));
        persistentState.AddValue("PartId", (object) PartGuidMapper.GetGuid(uniqueId));
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
  public virtual INodeID Deserialize(PersistentState persistNodeID)
  {
    byte num = (byte) persistNodeID.GetValue("PartKind");
    int partId = PartGuidMapper.GetUniqueId((Guid) persistNodeID.GetValue("PartId")) | (num == (byte) 1 ? 1073741824 /*0x40000000*/ : 0);
    PartSlot slot = this.FindSlot(partId);
    if (slot != null)
    {
      INodeID nodeId = slot.Object.Deserialize(persistNodeID);
      if (nodeId != null)
      {
        if (nodeId.Cookie == null)
          nodeId.Cookie = (object) new PartCookie();
        ((PartCookie) nodeId.Cookie).PartId = partId;
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
  public virtual object GetData(INodeID nodeID, Type dataFormat)
  {
    return this.FindSlot(nodeID)?.Object.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Возвращает данные в указанном формате для каждого дочернего элемента
  /// из коллекции. Если формат не поддерживается, то соответствующий
  /// элемент результата будет содержать null.
  /// </summary>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Массив объектов указанного типа.</returns>
  public virtual object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    for (int index = 0; index < data.Length; ++index)
    {
      PartSlot slot = this.FindSlot(nodeIDs[index]);
      data[index] = slot == null ? (object) null : slot.Object.GetData(nodeIDs[index], dataFormat);
    }
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
  public virtual IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    List<AnalyserSlot> nativeAnalysers = new List<AnalyserSlot>();
    if ((capabilities.ContentType & ContentType.Folders) != ContentType.None && this.FolderSlots != null)
    {
      for (int index = 0; index < this.FolderSlots.Count; ++index)
      {
        IUpdateAnalyser analyser = this.FolderSlots[index].Object.GetAnalyser(capabilities, sender, e);
        if (analyser != null)
          nativeAnalysers.Add(new AnalyserSlot(this.FolderSlots[index].UniqueId, analyser));
      }
    }
    if ((capabilities.ContentType & ContentType.NonFolders) != ContentType.None && this.NonFolderSlots != null)
    {
      for (int index = 0; index < this.NonFolderSlots.Count; ++index)
      {
        IUpdateAnalyser analyser = this.NonFolderSlots[index].Object.GetAnalyser(capabilities, sender, e);
        if (analyser != null)
          nativeAnalysers.Add(new AnalyserSlot(this.NonFolderSlots[index].UniqueId, analyser));
      }
    }
    return nativeAnalysers.Count == 0 ? (IUpdateAnalyser) null : (IUpdateAnalyser) new CompositeUpdateAnalyser(nativeAnalysers);
  }

  /// <summary>
  /// Возвращает сервис указанного типа или null, если он не реализован.
  /// </summary>
  /// <param name="service">Тип сервиса</param>
  /// <returns>Сервис</returns>
  public virtual object GetService(Type service)
  {
    if (!(service == typeof (INodeStatusesInfo)))
      return (object) null;
    if (this.statusesInfoService == null)
    {
      List<StatusesInfoSlot> slots = new List<StatusesInfoSlot>();
      for (int index = 0; index < this.FolderSlots.Count; ++index)
      {
        INodeStatusesInfo service1 = (INodeStatusesInfo) this.FolderSlots[index].Object.GetService(service);
        if (service1 != null)
          slots.Add(new StatusesInfoSlot(this.FolderSlots[index].UniqueId, service1));
      }
      for (int index = 0; index < this.NonFolderSlots.Count; ++index)
      {
        INodeStatusesInfo service2 = (INodeStatusesInfo) this.NonFolderSlots[index].Object.GetService(service);
        if (service2 != null)
          slots.Add(new StatusesInfoSlot(this.NonFolderSlots[index].UniqueId, service2));
      }
      this.statusesInfoService = (INodeStatusesInfo) new CompositeStatusesInfo(slots);
    }
    return (object) this.statusesInfoService;
  }

  /// <summary>Список слотов-папок</summary>
  public List<PartSlot> FolderSlots
  {
    get
    {
      if (this.folderSlots == null)
      {
        this.folderSlots = this.CreateFolderSlots();
        if (this.folderSlots == null)
          this.folderSlots = CompositeNode.EmptySlots;
        for (int index = 0; index < this.folderSlots.Count; ++index)
        {
          this.folderSlots[index].Object.Owner = (object) this;
          this.folderSlots[index].UniqueId |= 1073741824 /*0x40000000*/;
        }
      }
      return this.folderSlots;
    }
  }

  /// <summary>Список слотов-не-папок</summary>
  public List<PartSlot> NonFolderSlots
  {
    get
    {
      if (this.nonFolderSlots == null)
      {
        this.nonFolderSlots = this.CreateNonFolderSlots();
        if (this.nonFolderSlots == null)
          this.nonFolderSlots = CompositeNode.EmptySlots;
        for (int index = 0; index < this.nonFolderSlots.Count; ++index)
          this.nonFolderSlots[index].Object.Owner = (object) this;
      }
      return this.nonFolderSlots;
    }
  }

  /// <summary>Создать список слотов-папок</summary>
  /// <returns>Список слотов-папок</returns>
  protected virtual List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;

  /// <summary>Создать список слотов-не-папок</summary>
  /// <returns>Список слотов-не-папок</returns>
  protected virtual List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;

  /// <summary>
  /// Создает коллекцию слотов частей из части. Предназначено для создания
  /// простых элементов навигации, у которых папки и не-папки реализуются
  /// с помощью одной части.
  /// </summary>
  /// <param name="part">Часть элемента навигации</param>
  /// <returns>Коллекция слотов частей.</returns>
  protected virtual List<PartSlot> SlotsFromSinglePart(INodePart part)
  {
    return part != null ? new List<PartSlot>(1)
    {
      new PartSlot(CompositeNode.SinglePartGuid, part)
    } : throw new ArgumentNullException(sc_4248.ssp_imclient_4249(), LocalizationHolder.rm.GetString("Client.Core_622"));
  }

  /// <summary>Виртуальный метод-конструктор составной Query. Нужен для того, чтобы можно было перекрыть и вернуть потомка</summary>
  /// <param name="subQueries">Список вложенных Query</param>
  /// <returns>Созданная составная Query содержащая в себе переданный список вложенных Query</returns>
  protected virtual INodeQuery CreateCompositeQuery(List<QuerySlot> subQueries)
  {
    return (INodeQuery) new CompositeQuery(subQueries);
  }

  /// <summary>Найти слот для указанного идентификатора узла</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <returns>Слот для указанного идентификатора узла или null</returns>
  private PartSlot FindSlot(INodeID nodeID)
  {
    return nodeID == null || nodeID.Cookie == null ? (PartSlot) null : this.FindSlot(((PartCookie) nodeID.Cookie).PartId);
  }

  /// <summary>
  /// Отыскать слот для указанной части (старший бит - тип - папка или не папка)
  /// </summary>
  /// <param name="partId">Индекс части узла</param>
  /// <returns>Слот для указанной части (папки или не папки)</returns>
  private PartSlot FindSlot(int partId)
  {
    return this.FindSlot((partId & 1073741824 /*0x40000000*/) != 0 ? this.FolderSlots : this.NonFolderSlots, partId);
  }

  /// <summary>Найти слот в указанном списке</summary>
  /// <param name="slots">Список слотов</param>
  /// <param name="partId">Индекс части узла</param>
  /// <returns>Слот для указанной части (папки или не папки)</returns>
  private PartSlot FindSlot(List<PartSlot> slots, int partId)
  {
    if (slots != null)
    {
      for (int index = 0; index < slots.Count; ++index)
      {
        if (slots[index].UniqueId == partId)
          return slots[index];
      }
    }
    return (PartSlot) null;
  }

  protected DescriptorCollection GetSpecialDescriptors() => this.GetSpecialDescriptors(true, true);

  protected DescriptorCollection GetSpecialDescriptors(
    bool enableSelections,
    bool enableClassifiers)
  {
    DescriptorCollection specialDescriptors = new DescriptorCollection();
    if (enableSelections)
    {
      if (UISettings.ShowUnitedSelections)
      {
        specialDescriptors.Add(Intermech.Navigator.Selections.Consts.SelectionsDescriptorGuid, (IDescriptor) new HiveDescriptor(Intermech.Navigator.Selections.Consts.SelectionTypeID, this.GetBinding(BindingType.Selections)));
      }
      else
      {
        specialDescriptors.Add(Intermech.Navigator.Selections.Consts.SelectionsCommonDescriptorGuid, (IDescriptor) new HiveDescriptor(MetaDataHelper.GetObjectTypeID("cad00122-306c-11d8-b4e9-00304f19f545"), this.GetBinding(BindingType.CommonSelections)));
        specialDescriptors.Add(Intermech.Navigator.Selections.Consts.SelectionsPersonalDescriptorGuid, (IDescriptor) new HiveDescriptor(MetaDataHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545"), this.GetBinding(BindingType.PersonalSelections)));
      }
    }
    if (enableClassifiers)
      specialDescriptors.Add(Intermech.Navigator.Selections.Consts.ClassifiersDescriptorGuid, (IDescriptor) new HiveDescriptor(Intermech.Navigator.Selections.Consts.ClassifierTypeID, this.GetBinding(BindingType.Classificators)));
    return specialDescriptors;
  }

  protected virtual ITopBinding GetBinding(BindingType bindingType)
  {
    throw new MissingMethodException();
  }
}
