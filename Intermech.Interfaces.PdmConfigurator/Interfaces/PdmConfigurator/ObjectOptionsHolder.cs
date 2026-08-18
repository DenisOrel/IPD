// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ObjectOptionsHolder
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс, позволяющий сохранить содержимое ключевых полей конфигурируемых объектов
/// (опции, назначенные объекту, видимые значения этих опций, условия несовместимости
/// опций)
/// </summary>
[Serializable]
public sealed class ObjectOptionsHolder : ICloneable, IAssignable, IStoreable
{
  /// <summary>Идентификатор версии конфигурируемого объекта</summary>
  public long ObjectID;
  /// <summary>Список опций, назначенных объекту</summary>
  public List<long> Options = new List<long>();
  /// <summary>Список видимых значений опций</summary>
  public VisibleOptionValues VisibleOptionValues = new VisibleOptionValues();
  /// <summary>Коллекция условий несовместимости опций</summary>
  public ObjectIncompatibilitiesCollection Incompatibilities = new ObjectIncompatibilitiesCollection();
  /// <summary>Дата и время последней модификации полей класса</summary>
  public DateTime ModifiedAt = DateTime.UtcNow;
  /// <summary>Список колонок для запроса в "ядро"</summary>
  private static List<ColumnDescriptor> columnDescriptors = new List<ColumnDescriptor>();

  /// <summary>Создать пустой экземпляр класса</summary>
  public ObjectOptionsHolder()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ObjectOptionsHolder(object source) => this.Assign(source);

  /// <summary>
  /// Получить список колонок, необходимых для получения списка объектов
  /// </summary>
  /// <returns>Список колонок, необходимых для получения списка объектов</returns>
  internal static List<ColumnDescriptor> GetSelectColumns()
  {
    if (ObjectOptionsHolder.columnDescriptors.Count != 0)
      return ObjectOptionsHolder.columnDescriptors;
    ObjectOptionsHolder.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    ObjectOptionsHolder.columnDescriptors.Add(new ColumnDescriptor((object) Consts.attributeOptionsLinkID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    ObjectOptionsHolder.columnDescriptors.Add(new ColumnDescriptor((object) Consts.attributeVisibleOptionValuesID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    ObjectOptionsHolder.columnDescriptors.Add(new ColumnDescriptor((object) Consts.attributeOptionsIncompatibilityID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    return ObjectOptionsHolder.columnDescriptors;
  }

  /// <summary>Загрузить информацию об опциях из указанного объекта</summary>
  /// <param name="source">Источник</param>
  internal void InternalLoadOptions(IDBAttributable source)
  {
    this.Options = new List<long>((IEnumerable<long>) ObjectOptionsHolder.LoadOptionsList(source));
  }

  /// <summary>Загрузить информацию об опциях из указанного объекта</summary>
  /// <param name="source">Источник</param>
  /// <returns>Список опций, назначенных объекту, либо пустой список</returns>
  public static List<long> LoadOptionsList(IDBAttributable source)
  {
    List<long> options = new List<long>();
    if (source == null)
      return options;
    IDBAttribute attributeById = source.GetAttributeByID(Consts.attributeOptionsLinkID);
    if (attributeById != null)
    {
      object[] values = attributeById.Values;
      if (values != null && values.Length != 0)
      {
        for (int index = 0; index < values.Length; ++index)
        {
          long int64Value = DataSetProcessor.GetInt64Value(values[index], 0L);
          if (int64Value != 0L && options.IndexOf(int64Value) < 0)
            options.Add(int64Value);
        }
        PdmConfiguratorCache.CacheLoadOptions(attributeById.Session, (IList<long>) options);
      }
    }
    return options;
  }

  /// <summary>Загрузить информацию из строки таблицы данных</summary>
  /// <param name="row">Строка (таблица сформирована с помощью колонок columnDescriptiors)</param>
  internal void InternalLoadFromRow(DataRow row)
  {
    if (row == null || row.Table.Columns.Count < ObjectOptionsHolder.columnDescriptors.Count)
      return;
    this.ObjectID = DataSetProcessor.GetInt64Value(row, "cad00029-306c-11d8-b4e9-00304f19f545", 0L);
    this.ModifiedAt = DateTime.UtcNow;
    string source1 = DataSetProcessor.GetStringValue(row, "cad015a1-306c-11d8-b4e9-00304f19f545", string.Empty);
    if (source1.IndexOf("0|") == 0)
      this.VisibleOptionValues.Assign((object) source1);
    else
      source1 = string.Empty;
    string source2 = DataSetProcessor.GetStringValue(row, "cad015ab-306c-11d8-b4e9-00304f19f545", string.Empty);
    if (source2.IndexOf("0|") == 0)
      this.Incompatibilities.Assign((object) source2);
    else
      source2 = string.Empty;
    if (!(row.Table.ExtendedProperties[(object) "IUserSession"] is IUserSession extendedProperty))
      return;
    IDBObject source3 = extendedProperty.GetObject(this.ObjectID, false);
    if (source3 == null)
      return;
    if (string.IsNullOrEmpty(source1))
      this.VisibleOptionValues.LoadFromObject((IDBAttributable) source3);
    if (string.IsNullOrEmpty(source2))
      this.Incompatibilities.LoadFromObject((IDBAttributable) source3);
    this.InternalLoadOptions((IDBAttributable) source3);
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this)
    {
      this.ObjectID = 0L;
      this.Options.Clear();
      this.VisibleOptionValues.Clear();
      this.Incompatibilities.Clear();
    }
    this.Touch();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case ObjectOptionsHolder objectOptionsHolder:
        lock (this)
        {
          this.ObjectID = objectOptionsHolder.ObjectID;
          this.Options = new List<long>((IEnumerable<long>) objectOptionsHolder.Options);
          this.VisibleOptionValues.Assign((object) objectOptionsHolder.VisibleOptionValues);
          this.Incompatibilities.Assign((object) objectOptionsHolder.Incompatibilities);
        }
        this.ClearVisibleOptionsValuesLists((IUserSession) null);
        break;
      case IDBObject source1:
        lock (this)
        {
          this.ObjectID = source1.ObjectID;
          int count = source1.Attributes.Count;
          this.InternalLoadOptions((IDBAttributable) source1);
          this.VisibleOptionValues.LoadFromObject((IDBAttributable) source1);
          this.Incompatibilities.LoadFromObject((IDBAttributable) source1);
        }
        this.ClearVisibleOptionsValuesLists(source1.Session);
        break;
      case DataRow row:
        this.InternalLoadFromRow(row);
        break;
    }
  }

  /// <summary>Загрузить информацию из объекта/связи базы данных</summary>
  /// <param name="obj">Источник</param>
  /// <returns>true - информация загружена успешно, false - были ошибки</returns>
  public bool LoadFromObject(IDBAttributable obj)
  {
    if (obj == null)
      return false;
    this.Assign((object) obj);
    if (this.ObjectID != 0L)
    {
      int num = !this.Equals((object) PdmConfiguratorObjectOptionsCache.GetObjectOptions(this.ObjectID)) ? 1 : 0;
      PdmConfiguratorObjectOptionsCache.SetObjectOptions(this.ObjectID, this.Clone() as ObjectOptionsHolder);
    }
    return true;
  }

  /// <summary>Записать информацию в указанный элемент базы данных</summary>
  /// <param name="obj">Элемент-назначение</param>
  /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
  public bool SaveToObject(IDBAttributable obj)
  {
    if (obj == null || !(obj is IDBObject dbObject))
      return false;
    lock (this)
    {
      this.RemoveInvalidCriterions();
      try
      {
        this.Incompatibilities.Holder = (object) this;
        this.Incompatibilities.SaveToObject(obj);
      }
      finally
      {
        this.Incompatibilities.Holder = (object) null;
      }
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, Consts.attributeOptionsLinkID);
      if (attribute4ObjectType == null)
        return false;
      IDBAttribute dbAttribute = dbObject.GetAttributeByID(Consts.attributeOptionsLinkID);
      if (dbAttribute != null)
      {
        if (this.Options.Count == 0 && attribute4ObjectType.Required == RequiredModes.Manual)
        {
          dbAttribute.Delete(0L);
          dbAttribute = (IDBAttribute) null;
        }
      }
      else if (attribute4ObjectType.Required == RequiredModes.Manual && this.Options.Count > 0)
        dbAttribute = dbObject.Attributes.AddAttribute(Consts.attributeOptionsLinkID, false);
      if (dbAttribute != null)
      {
        object[] objArray = new object[this.Options.Count];
        for (int index = 0; index < this.Options.Count; ++index)
          objArray[index] = (object) this.Options[index];
        if (objArray.Length != 0)
          dbAttribute.Values = objArray;
        else
          dbAttribute.ClearValues();
      }
      this.ClearVisibleOptionsValuesLists(obj.Session);
      this.VisibleOptionValues.SaveToObject(obj);
    }
    if (this.ObjectID != 0L)
      PdmConfiguratorObjectOptionsCache.SetObjectOptions(this.ObjectID, this.Clone() as ObjectOptionsHolder);
    return true;
  }

  /// <summary>
  /// Обновить дату и время модификации содержимого контекста
  /// </summary>
  public void Touch()
  {
    lock (this)
      this.ModifiedAt = DateTime.UtcNow;
  }

  /// <summary>Заполнить списки видимыми значениями, если они пусты</summary>
  public void FillVisibleOptionsValuesLists()
  {
    lock (this)
    {
      for (int index = 0; index < this.Options.Count; ++index)
      {
        OptionHolder option = PdmConfiguratorCache.CacheFindOption(this.Options[index]);
        this.VisibleOptionValues.AddVisibleValues(option);
        this.VisibleOptionValues.SyncWithOption(option);
      }
    }
  }

  /// <summary>Заполнить списки видимыми значениями, если они пусты</summary>
  /// <param name="session">Сессия</param>
  public void ClearVisibleOptionsValuesLists(IUserSession session)
  {
    lock (this)
    {
      List<Guid> guidList = new List<Guid>();
      foreach (KeyValuePair<Guid, List<string>> keyValuePair in this.VisibleOptionValues.Items)
      {
        OptionHolder orLoadOption = PdmConfiguratorCache.CacheFindOrLoadOption(session, keyValuePair.Key);
        if (orLoadOption != null && this.Options.IndexOf(orLoadOption.OptionObjectID) < 0)
          guidList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < guidList.Count; ++index)
        this.VisibleOptionValues.Items.Remove(guidList[index]);
      guidList.Clear();
      foreach (KeyValuePair<Guid, bool> keyValuePair in this.VisibleOptionValues.Obligatory)
      {
        OptionHolder orLoadOption = PdmConfiguratorCache.CacheFindOrLoadOption(session, keyValuePair.Key);
        if (orLoadOption != null && this.Options.IndexOf(orLoadOption.OptionObjectID) < 0)
          guidList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < guidList.Count; ++index)
        this.VisibleOptionValues.Obligatory.Remove(guidList[index]);
      for (int index = 0; index < this.Options.Count; ++index)
        this.VisibleOptionValues.RemoveVisibleValues(PdmConfiguratorCache.CacheFindOption(this.Options[index]));
      if (this.Options.Count != 0)
        return;
      this.VisibleOptionValues.Clear();
    }
  }

  /// <summary>
  /// Метод позволяет загрузить в кэш конфигуратора информацию об опциях, ссылки на которые есть в текущем объекте
  /// </summary>
  /// <param name="session">Сессия</param>
  public void LoadOptionsToCache(IUserSession session)
  {
    if (session == null)
      return;
    PdmConfiguratorCache.CacheLoadOptions(session, (IList<long>) this.Options);
  }

  /// <summary>Отыскать опции, принадлежащие указанной категории</summary>
  /// <param name="category">Категория</param>
  /// <returns>Опции, принадлежащие указанной категории</returns>
  public List<OptionHolder> FindCategoryOptions(long category)
  {
    List<OptionHolder> categoryOptions = new List<OptionHolder>();
    lock (this)
    {
      foreach (KeyValuePair<long, OptionHolder> keyValuePair in PdmConfiguratorCache.OptionsCacheID)
      {
        if (keyValuePair.Value.OptionCategory == category && this.Options.IndexOf(keyValuePair.Value.OptionObjectID) >= 0)
          categoryOptions.Add(keyValuePair.Value.Clone() as OptionHolder);
      }
    }
    categoryOptions.Sort();
    return categoryOptions;
  }

  /// <summary>
  /// Метод удаляет из коллекции условий несовместимости критерии,
  /// связанные с невидимыми значениями опций, а также с опциями, которых нет в текущем объекте
  /// </summary>
  public void RemoveInvalidCriterions()
  {
    if (this.Incompatibilities.Count == 0)
      return;
    for (int index1 = this.Incompatibilities.Count - 1; index1 >= 0; --index1)
    {
      PdmCriterion incompatibility = this.Incompatibilities[index1] as PdmCriterion;
      if (this.Options.IndexOf(PdmConfiguratorCache.CacheFindOptionID(incompatibility.Option)) < 0)
      {
        this.Incompatibilities.RemoveAt(index1);
      }
      else
      {
        for (int index2 = incompatibility.Items.Count - 1; index2 >= 0; --index2)
        {
          ObjectIncompatibilityCriterion incompatibilityCriterion = incompatibility.Items[index2] as ObjectIncompatibilityCriterion;
          if (!this.VisibleOptionValues.GetVisibleOptionValue(incompatibility.Option, incompatibilityCriterion.Value))
          {
            incompatibility.Items.RemoveAt(index2);
          }
          else
          {
            long optionId1 = PdmConfiguratorCache.CacheFindOptionID(incompatibilityCriterion.Option);
            long optionId2 = PdmConfiguratorCache.CacheFindOptionID(incompatibilityCriterion.OptionConflict);
            if ((optionId1 == 0L || this.Options.IndexOf(optionId1) >= 0) && optionId2 != 0L)
              this.Options.IndexOf(optionId2);
          }
        }
      }
    }
  }

  /// <summary>Добавить опцию в объект</summary>
  /// <param name="optionID">Идентификатор версии объекта опции</param>
  public void AddOption(long optionID)
  {
    lock (this)
    {
      if (optionID == 0L || this.Options.IndexOf(optionID) >= 0)
        return;
      this.Options.Add(optionID);
    }
  }

  /// <summary>Добавить опции в объект</summary>
  /// <param name="optionsID">Идентификаторы версии объекта опции</param>
  public void AddOptions(IList<long> optionsID)
  {
    lock (this)
    {
      if (optionsID == null || optionsID.Count == 0)
        return;
      for (int index = 0; index < optionsID.Count; ++index)
      {
        if (this.Options.IndexOf(optionsID[index]) < 0)
          this.Options.Add(optionsID[index]);
      }
    }
  }

  /// <summary>Удалить опцию из объекта</summary>
  /// <param name="optionID">Идентификатор версии объекта опции</param>
  public void DeleteOption(long optionID)
  {
    lock (this)
    {
      if (optionID == 0L || this.Options.IndexOf(optionID) < 0)
        return;
      this.Options.Remove(optionID);
    }
  }

  /// <summary>Удалить опции из объекта</summary>
  /// <param name="optionsID">Идентификаторы версий объектов опций</param>
  public void DeleteOptions(IList<long> optionsID)
  {
    lock (this)
    {
      if (optionsID == null || optionsID.Count == 0)
        return;
      for (int index = 0; index < optionsID.Count; ++index)
        this.Options.Remove(optionsID[index]);
    }
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (this == obj)
      return true;
    if (!(obj is ObjectOptionsHolder objectOptionsHolder) || this.ObjectID != objectOptionsHolder.ObjectID)
      return false;
    bool flag = Helper.CompareLists((IList) this.Options, (IList) objectOptionsHolder.Options);
    if (flag)
      flag = this.VisibleOptionValues.Equals((object) objectOptionsHolder.VisibleOptionValues);
    if (flag)
      flag = this.Incompatibilities.Equals((object) objectOptionsHolder.Incompatibilities);
    return flag;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this.ObjectID.GetHashCode() << 16 /*0x10*/ ^ this.Options.Count.GetHashCode() << 10 ^ this.VisibleOptionValues.GetHashCode() << 5 ^ this.Incompatibilities.GetHashCode();
  }

  /// <summary>
  /// Метод позволяет извлечь из списка опций объектов информацию об опциях и объектах, которым они назначены
  /// </summary>
  /// <param name="items">Список опций объектов</param>
  /// <param name="excludedOptions">Исключаемые опции</param>
  /// <returns>Информация об опциях (Int4-Key) и объектах (List(Int64)), которым они назначены</returns>
  public static Dictionary<long, List<long>> ExtractOptionsInObjects(
    List<ObjectOptionsHolder> items,
    IList<long> excludedOptions)
  {
    Dictionary<long, List<long>> optionsInObjects = new Dictionary<long, List<long>>();
    if (items == null || items.Count == 0)
      return optionsInObjects;
    for (int index1 = 0; index1 < items.Count; ++index1)
    {
      long objectId = items[index1].ObjectID;
      List<long> options = items[index1].Options;
      if (options != null && options.Count != 0)
      {
        for (int index2 = 0; index2 < options.Count; ++index2)
        {
          long key = options[index2];
          if (excludedOptions == null || excludedOptions.IndexOf(key) < 0)
          {
            List<long> longList = optionsInObjects.ContainsKey(key) ? optionsInObjects[key] : new List<long>();
            if (longList.IndexOf(objectId) < 0)
              longList.Add(objectId);
            optionsInObjects[key] = longList;
          }
        }
      }
    }
    return optionsInObjects;
  }
}
