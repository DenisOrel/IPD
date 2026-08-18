// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmAnalyzedOptionObject
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Обработанное описание объекта с опциями</summary>
[DebuggerDisplay("ObjectID: {objectID}; Items.Count: {Items.Count}; Caption: {caption}")]
[Serializable]
public sealed class PdmAnalyzedOptionObject : IAssignable, ICloneable
{
  /// <summary>
  /// Коллекция колонок, необходимых для запроса к базе данных
  /// </summary>
  private static List<ColumnDescriptor> _columns;
  /// <summary>Родительский объект</summary>
  private PdmAnalyzedOptionObjects parent;
  /// <summary>Коллекция дочерних объектов</summary>
  private PdmAnalyzedOptionObjects items;
  /// <summary>Опции объекта</summary>
  private List<long> options;
  /// <summary>Идентификатор объекта, опции которого изучаются</summary>
  private long id;
  /// <summary>
  /// Идентификатор версии объекта (уникальный в пределах всей коллекции), опции которого изучаются
  /// </summary>
  private long objectID;
  /// <summary>Обработан ли объект</summary>
  private bool parsedObject;
  /// <summary>Обработан ли состав объекта</summary>
  private bool parsedComposition;
  /// <summary>Идентификатор типа объекта</summary>
  private int objectType = -1;
  /// <summary>Заголовок объекта</summary>
  private string caption;
  /// <summary>Идентификатор владельца объекта</summary>
  private long ownerID;
  /// <summary>Кем объект взят на изменение</summary>
  private long chkOutByID;
  /// <summary>Шаг жизненного цикла</summary>
  private int lcStepID = -1;
  /// <summary>Номер версии</summary>
  private long versionNo;
  /// <summary>Является ли версия базовой</summary>
  private bool baseVersion;

  /// <summary>Создать пустой экземпляр класса</summary>
  public PdmAnalyzedOptionObject()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public PdmAnalyzedOptionObject(object source) => this.Assign(source);

  /// <summary>
  /// Создать описание обрабатываемого объекта (минимальная версия конструктора)
  /// </summary>
  /// <param name="parent">Родительский объект</param>
  /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
  public PdmAnalyzedOptionObject(PdmAnalyzedOptionObjects parent, long objectID)
    : this(parent, 0L, objectID, false, false, -1, string.Empty, 0L, 0L, -1, 0L, false, (List<long>) null)
  {
  }

  /// <summary>
  /// Создать описание обрабатываемого объекта (полная версия конструктора)
  /// </summary>
  /// <param name="parent">Родительский объект</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
  /// <param name="parsedObject">Обработан ли указанный объект</param>
  /// <param name="parsedComposition">Обработан ли состав объекта</param>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <param name="caption">Заголовок объекта</param>
  /// <param name="ownerID">Идентификатор владельца объекта</param>
  /// <param name="chkOutByID">Кем объект взят на изменение</param>
  /// <param name="lcStepID">Шаг жизненного цикла</param>
  /// <param name="versionNo">Номер версии</param>
  /// <param name="baseVersion">Является ли версия базовой</param>
  /// <param name="options">Опции объекта</param>
  public PdmAnalyzedOptionObject(
    PdmAnalyzedOptionObjects parent,
    long id,
    long objectID,
    bool parsedObject,
    bool parsedComposition,
    int objectType,
    string caption,
    long ownerID,
    long chkOutByID,
    int lcStepID,
    long versionNo,
    bool baseVersion,
    List<long> options)
  {
    this.parent = parent;
    this.id = id;
    this.objectID = objectID;
    this.parsedObject = parsedObject;
    this.parsedComposition = parsedComposition;
    this.objectType = objectType;
    this.caption = caption;
    this.ownerID = ownerID;
    this.chkOutByID = chkOutByID;
    this.lcStepID = lcStepID;
    this.versionNo = versionNo;
    this.baseVersion = baseVersion;
    this.options = options;
    this.items = new PdmAnalyzedOptionObjects(this.Parent);
  }

  /// <summary>Родительский объект</summary>
  public PdmAnalyzedOptionObjects Parent
  {
    [DebuggerStepThrough] get => this.parent;
    set => this.parent = value;
  }

  /// <summary>Коллекция дочерних объектов</summary>
  public PdmAnalyzedOptionObjects Items
  {
    [DebuggerStepThrough] get => this.items;
  }

  /// <summary>Опции объекта</summary>
  public List<long> Options
  {
    [DebuggerStepThrough] get => this.options;
    set => this.options = value;
  }

  /// <summary>Дочернее описание объекта с указанным индексом</summary>
  /// <param name="index">Индекс</param>
  /// <returns>Дочернее описание объекта с указанным индексом</returns>
  public PdmAnalyzedOptionObject this[int index]
  {
    [DebuggerStepThrough] get => this.items[index];
  }

  /// <summary>Количество дочерних описаний объектов</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this.items.Count;
  }

  /// <summary>Идентификатор объекта</summary>
  public long ID
  {
    [DebuggerStepThrough] get => this.id;
    set => this.id = value;
  }

  /// <summary>
  /// Идентификатор версии объекта (уникальный в пределах всей коллекции)
  /// </summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this.objectID;
    set => this.objectID = value;
  }

  /// <summary>Обработан ли указанный объект</summary>
  public bool ParsedObject
  {
    [DebuggerStepThrough] get => this.parsedObject;
    set => this.parsedObject = value;
  }

  /// <summary>Обработан ли состав объекта</summary>
  public bool ParsedComposition
  {
    [DebuggerStepThrough] get => this.parsedComposition;
    set => this.parsedComposition = value;
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjectType
  {
    [DebuggerStepThrough] get => this.objectType;
    set => this.objectType = value;
  }

  /// <summary>Заголовок объекта</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this.caption;
    set => this.caption = value;
  }

  /// <summary>Идентификатор владельца объекта</summary>
  public long OwnerID
  {
    [DebuggerStepThrough] get => this.ownerID;
    set => this.ownerID = value;
  }

  /// <summary>Кем объект взят на изменение</summary>
  public long ChkOutByID
  {
    [DebuggerStepThrough] get => this.chkOutByID;
    set => this.chkOutByID = value;
  }

  /// <summary>Шаг жизненного цикла</summary>
  public int LCStepID
  {
    [DebuggerStepThrough] get => this.lcStepID;
    set => this.lcStepID = value;
  }

  /// <summary>Номер версии объекта</summary>
  public long VersionNo
  {
    [DebuggerStepThrough] get => this.versionNo;
    set => this.versionNo = value;
  }

  /// <summary>Является ли версия базовой</summary>
  public bool BaseVersion
  {
    [DebuggerStepThrough] get => this.baseVersion;
    set => this.baseVersion = value;
  }

  /// <summary>
  /// Отыскать в коллекции описание объекта с указанным идентификатором.
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="objectID">Уникальный в пределах всей коллекции идентификатор версии объекта</param>
  /// <returns>null, если описание объекта не найдено</returns>
  public PdmAnalyzedOptionObject FindObject(long objectID)
  {
    if (objectID == 0L)
      return (PdmAnalyzedOptionObject) null;
    if (objectID == this.objectID)
      return this;
    for (int index = 0; index < this.items.Count; ++index)
    {
      PdmAnalyzedOptionObject analyzedOptionObject = this.items[index].FindObject(objectID);
      if (analyzedOptionObject != null)
        return analyzedOptionObject;
    }
    return (PdmAnalyzedOptionObject) null;
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты полностью идентичны</returns>
  public override bool Equals(object obj)
  {
    return obj is PdmAnalyzedOptionObject analyzedOptionObject && this.objectID == analyzedOptionObject.objectID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.objectID.GetHashCode();

  /// <summary>
  /// Загрузить описание объекта из базы данных (если оно не загружено)
  /// </summary>
  /// <param name="session">Сессия</param>
  public void LoadDescription(IUserSession session)
  {
    if (session == null || this.LCStepID != -1 && this.ObjectType != -1 && this.OwnerID != 0L)
      return;
    this.LoadDescription(session.GetObject(this.ObjectID));
  }

  /// <summary>
  /// Загрузить описание из указанного объекта, если оно ещё не загружено
  /// </summary>
  /// <param name="obj">Объект</param>
  public void LoadDescription(IDBObject obj)
  {
    if (obj == null || this.LCStepID != -1 && this.ObjectType != -1 && this.OwnerID != 0L)
      return;
    this.ID = obj.ID;
    this.Caption = obj.Caption;
    this.LCStepID = obj.LCStep;
    this.ObjectType = obj.ObjectType;
    this.OwnerID = obj.OwnerID;
    this.ChkOutByID = obj.CheckoutBy;
    this.VersionNo = (long) obj.VersionID;
    this.BaseVersion = obj.IsBaseVersion;
    this.Options = ObjectOptionsHolder.LoadOptionsList((IDBAttributable) obj);
    this.ParsedObject = true;
  }

  /// <summary>
  /// Метод возвращает коллекцию колонок, необходимых для запроса к базе данных
  /// </summary>
  /// <returns>Коллекция колонок, необходимых для запроса к базе данных</returns>
  public static List<ColumnDescriptor> GetColumnDescriptors()
  {
    if (PdmAnalyzedOptionObject._columns == null)
    {
      PdmAnalyzedOptionObject._columns = new List<ColumnDescriptor>();
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -8, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -6, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -4, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -5, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -16, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) Consts.attributeOptionsLinkID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
      PdmAnalyzedOptionObject._columns.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    }
    return PdmAnalyzedOptionObject._columns;
  }

  /// <summary>Загрузить описание объекта из строки таблицы</summary>
  /// <param name="session">Сессия</param>
  /// <param name="row">Строка из таблицы, колонки которой сформированы методом GetColumnDescriptors</param>
  public void LoadDescription(IUserSession session, DataRow row)
  {
    this.Clear();
    if (session == null || row == null)
      return;
    this.ObjectID = DataSetProcessor.GetInt64Value(row, 0, 0L);
    this.ObjectType = DataSetProcessor.GetInt32Value(row, 1, -1);
    this.ID = DataSetProcessor.GetInt64Value(row, 2, 0L);
    this.Caption = DataSetProcessor.GetStringValue(row, 3, string.Empty);
    this.OwnerID = DataSetProcessor.GetInt64Value(row, 4, 0L);
    this.ChkOutByID = DataSetProcessor.GetInt64Value(row, 5, 0L);
    this.LCStepID = DataSetProcessor.GetInt32Value(row, 6, -1);
    this.VersionNo = DataSetProcessor.GetInt64Value(row, 7, 0L);
    this.BaseVersion = DataSetProcessor.GetInt64Value(row, 8, 0L) == 1L;
    if (DataSetProcessor.GetInt64Value(row, 9, 0L) != 0L)
      this.Options = ObjectOptionsHolder.LoadOptionsList((IDBAttributable) session.GetObject(this.ObjectID, false));
    this.ParsedObject = true;
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.items.Clear();
    this.caption = string.Empty;
    this.chkOutByID = 0L;
    this.id = 0L;
    this.lcStepID = -1;
    this.objectID = 0L;
    this.ownerID = 0L;
    this.parent = (PdmAnalyzedOptionObjects) null;
    this.parsedObject = false;
    this.parsedComposition = false;
    this.options = (List<long>) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is PdmAnalyzedOptionObject analyzedOptionObject))
      return;
    this.caption = analyzedOptionObject.Caption;
    this.chkOutByID = analyzedOptionObject.ChkOutByID;
    this.id = analyzedOptionObject.ID;
    this.items.Assign((object) analyzedOptionObject.Items);
    this.lcStepID = analyzedOptionObject.LCStepID;
    this.objectID = analyzedOptionObject.ObjectID;
    this.objectType = analyzedOptionObject.ObjectType;
    this.ownerID = analyzedOptionObject.OwnerID;
    this.parent = analyzedOptionObject.Parent;
    this.parsedObject = analyzedOptionObject.ParsedObject;
    this.parsedComposition = analyzedOptionObject.ParsedComposition;
    this.versionNo = analyzedOptionObject.VersionNo;
    this.options = analyzedOptionObject.Options != null ? new List<long>((IEnumerable<long>) analyzedOptionObject.Options) : (List<long>) null;
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone() => (object) new PdmAnalyzedOptionObject((object) this);

  /// <summary>Проверить опции объекта</summary>
  /// <param name="session">Сессия</param>
  /// <param name="options">Параметры</param>
  /// <param name="excludedOptions">Список игнорируемых опций</param>
  public void CheckOptions(
    IUserSession session,
    PdmAnalyzerFlags options,
    IList<long> excludedOptions)
  {
    if (!this.parsedObject || this.options == null || this.options.Count == 0 || session == null || (options & PdmAnalyzerFlags.IgnoreObsoleteOptions) != PdmAnalyzerFlags.IgnoreObsoleteOptions)
      return;
    for (int index = this.options.Count - 1; index >= 0; --index)
    {
      OptionHolder optionHolder = PdmConfiguratorCache.CacheFindOption(this.options[index]) ?? PdmConfiguratorCache.CacheAddOption(session, this.options[index]);
      if (optionHolder == null || (optionHolder.OptionFlags & OptionFlags.Obsolete) == OptionFlags.Obsolete || excludedOptions != null && excludedOptions.IndexOf(this.options[index]) >= 0)
        this.options.RemoveAt(index);
    }
  }
}
