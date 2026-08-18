// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCompositionObject
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Объект состава, используемый для задач формирования и редактирования составов производственных заказов
/// </summary>
[DebuggerDisplay("F_OBJECT_ID: {F_OBJECT_ID}; Caption: {CAPTION}")]
[Serializable]
public class MRPCompositionObject : 
  SimpleCompositionObject,
  ICloneable,
  IMRPTypedObjectRef,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPContext,
  IMRPTypedItem,
  IMRPRelationRef
{
  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  protected bool isNewRelation;
  /// <summary>
  /// Признак изготовления (1 - Собственное, 2 - Покупное, 3 - По кооперации, 4 - Не изготавливать)
  /// </summary>
  protected long isBoughtArticle = 1;
  /// <summary>
  /// Учёт изделий в производстве (0 - Партиями, 1 - Экземплярами)
  /// </summary>
  protected long productionAccountingOfParts;
  /// <summary>Идентификатор изделия</summary>
  protected long articleID;
  /// <summary>Идентификатор версии изделия</summary>
  protected long articleVersionID;
  /// <summary>Количество (связь)</summary>
  protected MeasuredValue quantity;
  /// <summary>
  /// Контейнер сервисов
  /// [Не сериализуется]
  /// </summary>
  [NonSerialized]
  protected IServiceProvider services;
  /// <summary>
  /// Список колонок для запроса в "ядро" для каждого типа связи
  /// </summary>
  protected static Dictionary<int, List<ColumnDescriptor>> columnDescriptorsComposition = new Dictionary<int, List<ColumnDescriptor>>();

  /// <summary>Создать незаполненное описание объекта состава</summary>
  public MRPCompositionObject()
  {
  }

  /// <summary>Создать частично заполненное описание объекта состава</summary>
  /// <param name="projID">Иденификатор версии родительского объекта</param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  public MRPCompositionObject(long projID, long prjLinkID)
    : base(0L, projID, -1, -1, 0L, 0L, string.Empty, 0L, 0L, 0L, ObjectVersionDescriptionOptions.None, Guid.Empty, prjLinkID, projID, -1)
  {
  }

  /// <summary>Создать описание объекта состава</summary>
  /// <param name="_ID">Идентификатор объекта</param>
  /// <param name="_OBJECT_ID">Идентификатор версии объекта</param>
  /// <param name="_OBJECT_TYPE">Идентификатор типа объекта</param>
  /// <param name="_LCSTEP_ID">Шаг жизненного цикла</param>
  /// <param name="_OWNER_ID">Идентификатор владельца объекта</param>
  /// <param name="_CHKOUT_BY">Кем объект взят на изменение</param>
  /// <param name="_CAPTION">Заголовок объекта</param>
  /// <param name="_F_VERSION_ID">Номер версии</param>
  /// <param name="_F_MODIFICATION_ID">Номер группы изменений</param>
  /// <param name="_F_BASE_VERSION">Является ли версия базовой</param>
  /// <param name="_Options">Опции</param>
  /// <param name="_F_LINK_GUID">Уникальный глобальный идентификатор связи</param>
  /// <param name="_F_PRJLINK_ID">Идентификатор связи (уникальный в пределах всей коллекции)</param>
  /// <param name="_F_PROJ_ID">Идентификатор версии родительского объекта</param>
  /// <param name="_F_RELATION_TYPE">Идентификатор типа связи</param>
  /// <param name="articleID">Идентификатор изделия</param>
  /// <param name="articleVersionID">Идентификатор версии изделия</param>
  /// <param name="quantity">Количество (связь)</param>
  /// <param name="isNewRelation">Является ли связь созданной (новой), либо она существующая (значение по умолчанию)</param>
  public MRPCompositionObject(
    long _ID,
    long _OBJECT_ID,
    int _OBJECT_TYPE,
    int _LCSTEP_ID,
    long _OWNER_ID,
    long _CHKOUT_BY,
    string _CAPTION,
    long _F_VERSION_ID,
    long _F_MODIFICATION_ID,
    long _F_BASE_VERSION,
    ObjectVersionDescriptionOptions _Options,
    Guid _F_LINK_GUID,
    long _F_PRJLINK_ID,
    long _F_PROJ_ID,
    int _F_RELATION_TYPE,
    long articleID,
    long articleVersionID,
    MeasuredValue quantity,
    bool isNewRelation)
    : base(_ID, _OBJECT_ID, _OBJECT_TYPE, _LCSTEP_ID, _OWNER_ID, _CHKOUT_BY, _CAPTION, _F_VERSION_ID, _F_MODIFICATION_ID, _F_BASE_VERSION, _Options, _F_LINK_GUID, _F_PRJLINK_ID, _F_PROJ_ID, _F_RELATION_TYPE)
  {
    this.articleID = articleID;
    this.articleVersionID = articleVersionID;
    this.quantity = quantity;
    this.isNewRelation = isNewRelation;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из строки таблицы
  /// </summary>
  /// <param name="row">Строка таблицы с данными</param>
  public MRPCompositionObject(DataRow row) => this.Assign((object) row);

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта
  /// </summary>
  /// <param name="source">Источник информации</param>
  public MRPCompositionObject(object source) => this.Assign(source);

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта
  /// </summary>
  /// <param name="source">Объект-описатель</param>
  public MRPCompositionObject(IDBRelation source) => this.Assign((object) source);

  /// <summary>
  /// Признак изготовления (1 - Собственное, 2 - Покупное, 3 - По кооперации, 4 - Не изготавливать)
  /// </summary>
  public long IsBoughtArticle
  {
    [DebuggerStepThrough] get => this.isBoughtArticle;
    set => this.isBoughtArticle = value;
  }

  /// <summary>
  /// Учёт изделий в производстве (0 - Партиями, 1 - Экземплярами)
  /// </summary>
  public long ProductionAccountingOfParts
  {
    [DebuggerStepThrough] get => this.productionAccountingOfParts;
    set => this.productionAccountingOfParts = value;
  }

  /// <summary>
  /// Идентификатор изделия, на основе которого создана данная связь
  /// </summary>
  public long ArticleID
  {
    [DebuggerStepThrough] get => this.articleID;
    set => this.articleID = value;
  }

  /// <summary>
  /// Идентификатор версии изделия, на основе которого создана данная связь
  /// </summary>
  public long ArticleVersionID
  {
    [DebuggerStepThrough] get => this.articleVersionID;
    set => this.articleVersionID = value;
  }

  /// <summary>Количество (связь)</summary>
  public MeasuredValue Quantity
  {
    [DebuggerStepThrough] get => this.quantity;
    set => this.quantity = value;
  }

  /// <summary>
  /// Получить список колонок, необходимых для получения списка объектов состава
  /// </summary>
  /// <param name="relTypeID">Тип связи, по которому выполняется раскрутка составов</param>
  /// <returns>Список колонок, необходимых для получения списка объектов состава</returns>
  public virtual List<ColumnDescriptor> GetColumnDescriptors(int relTypeID)
  {
    lock (MRPCompositionObject.columnDescriptorsComposition)
    {
      List<ColumnDescriptor> columnDescriptors;
      if (!MRPCompositionObject.columnDescriptorsComposition.ContainsKey(relTypeID))
      {
        columnDescriptors = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) this.GetColumnDescriptors());
        List<int> intList = MetaDataHelper.GetAttribute4RelationTypeList(relTypeID).ConvertAll<int>((Converter<IMSAttribute4RelationType, int>) (attr => attr.AttributeID));
        columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0038f-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0058a-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00622-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cadd92f0-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545");
        int num = attributeTypeId;
        if (intList.IndexOf(num) >= 0)
          columnDescriptors.Add(new ColumnDescriptor((object) attributeTypeId, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        MRPCompositionObject.columnDescriptorsComposition[relTypeID] = columnDescriptors;
      }
      else
        columnDescriptors = MRPCompositionObject.columnDescriptorsComposition[relTypeID];
      return columnDescriptors;
    }
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты полностью идентичны</returns>
  public override bool Equals(object obj)
  {
    return !(obj is MRPCompositionObject compositionObject) ? base.Equals(obj) : Math.Abs(this.F_PRJLINK_ID) == Math.Abs(compositionObject.F_PRJLINK_ID);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => Math.Abs(this.F_PRJLINK_ID).GetHashCode();

  /// <summary>Очистить экземпляр класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.services = (IServiceProvider) null;
    this.isBoughtArticle = 1L;
    this.productionAccountingOfParts = 0L;
    this.articleID = 0L;
    this.articleVersionID = 0L;
    this.quantity = (MeasuredValue) null;
    this.isNewRelation = false;
  }

  /// <summary>Скопировать информацию из указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    switch (source)
    {
      case MRPCompositionObject compositionObject:
        this.services = compositionObject.services;
        this.isBoughtArticle = compositionObject.IsBoughtArticle;
        this.productionAccountingOfParts = compositionObject.ProductionAccountingOfParts;
        this.articleID = compositionObject.ArticleID;
        this.articleVersionID = compositionObject.ArticleVersionID;
        this.quantity = compositionObject.Quantity;
        this.isNewRelation = compositionObject.isNewRelation;
        break;
      case DataRow row:
        this.isBoughtArticle = DataSetProcessor.GetInt64Value(row, "cad0038f-306c-11d8-b4e9-00304f19f545", 1L);
        this.productionAccountingOfParts = DataSetProcessor.GetInt64Value(row, "cad0058a-306c-11d8-b4e9-00304f19f545", 0L);
        this.articleID = DataSetProcessor.GetInt64Value(row, "cad00622-306c-11d8-b4e9-00304f19f545", 0L);
        this.articleVersionID = DataSetProcessor.GetInt64Value(row, "cadd92f0-306c-11d8-b4e9-00304f19f545", 0L);
        this.quantity = DataSetProcessor.GetMeasuredValue(row, "cad00267-306c-11d8-b4e9-00304f19f545", (MeasuredValue) null);
        this.CalcFields();
        break;
    }
  }

  /// <summary>Идентификатор версии объекта</summary>
  long IMRPObjectRef.ObjectID
  {
    [DebuggerStepThrough] get => this.F_OBJECT_ID;
  }

  /// <summary>
  /// Уникальный глобальный идентификатор объекта
  /// [не реализовано]
  /// </summary>
  Guid IMRPGuidItem.Guid
  {
    [DebuggerStepThrough] get => Guid.Empty;
  }

  /// <summary>
  /// Обновить идентификатор версии объекта на указанное значение
  /// </summary>
  /// <param name="newItemID">Новый идентификатор версии</param>
  void IMRPUpdateableItemRef.UpdateItemID(long newItemID) => this.F_OBJECT_ID = newItemID;

  /// <summary>
  /// Контейнер сервисов (контекст, в рамках которого осуществляется некоторое действие)
  /// </summary>
  IServiceProvider IMRPContext.Services
  {
    [DebuggerStepThrough] get => this.services;
    set => this.services = value;
  }

  /// <summary>Идентификатор типа объекта</summary>
  int IMRPTypedItem.TypeID
  {
    [DebuggerStepThrough] get => this.F_OBJECT_TYPE;
  }

  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  bool IMRPRelationRef.IsNewRelation
  {
    [DebuggerStepThrough] get => this.isNewRelation;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  long IMRPRelationRef.ProjectID
  {
    [DebuggerStepThrough] get => this.F_PROJ_ID;
  }

  /// <summary>Идентификатор связи</summary>
  long IMRPRelationRef.PrjLinkID
  {
    [DebuggerStepThrough] get => this.F_PRJLINK_ID;
  }
}
