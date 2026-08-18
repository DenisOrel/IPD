// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorContext
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using Intermech.Localization;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Текущий контекст конфигуратора составов IPS.
/// Хранит текущие значения опций и другую информацию.
/// 
/// Информация хранится в атрибуте "Контекст конфигуратора составов",
/// назначаемому конфигурируемым типам связей/объектов. Кроме того, контекст может храниться
/// независимо от связи (назначается версии родительского объекта и типу связи)
/// </summary>
[DebuggerDisplay("Key: [{Key}]; OptionsValues: {OptionsValues.Count}; ObjectsOptions: {ObjectsOptions.Count}")]
[Serializable]
public sealed class PdmConfiguratorContext : ICloneable, IAssignable, IStoreable
{
  /// <summary>Генератор уникальных номеров</summary>
  [NonSerialized]
  private static long _index = 0;
  /// <summary>Уникальный номер контекста</summary>
  [NonSerialized]
  public long Handle;
  /// <summary>Контейнер сервисов</summary>
  [NonSerialized]
  public ServiceContainer Services = new ServiceContainer();
  /// <summary>
  /// Ключ родительского контекста конфигуратора составов IPS
  /// </summary>
  private RelationPair _parentKey;
  /// <summary>
  /// Словарик для хранения значений опций в текущем контексте.
  /// [Guid опции] =&gt; [ID значения опции]
  /// </summary>
  public Dictionary<Guid, string> OptionsValues = new Dictionary<Guid, string>();
  /// <summary>Ключ, к которому привязан контекст</summary>
  public RelationPair Key = new RelationPair();
  /// <summary>Дата и время последней модификации контекста</summary>
  public DateTime ModifiedAt = DateTime.UtcNow;
  /// <summary>
  /// Вид контекста конфигуратора составов
  /// (поле зависит от того, что указано в Key)
  /// </summary>
  public PdmContextType ContextType = PdmContextType.Unknown;
  /// <summary>
  /// Опции, назначенные объектам, к которым относится контекст конфигуратора составов
  /// (если контекст принадлежит связи, то это опции дочернего объекта, если контекст для
  /// родительского объекта конфигурируемого типа, то опции принадлежат этому объекту,
  /// если контекст для комплектации, то опции принадлежат дочерним объектам состава комплектации)
  /// </summary>
  public List<ObjectOptionsHolder> ObjectsOptions = new List<ObjectOptionsHolder>();
  /// <summary>
  /// Кэш контекстов конфигуратора составов IPS, с которым связан контекст
  /// </summary>
  [NonSerialized]
  private PdmConfiguratorContextsCache _contextsCache;
  /// <summary>Экземпляр класса для сравнения опций по их Guid</summary>
  private static PdmConfiguratorContext.OptionCategoriesComparer occ = new PdmConfiguratorContext.OptionCategoriesComparer();

  /// <summary>Кэш, которому принадлежит текущий контекст</summary>
  public PdmConfiguratorContextsCache ContextsCache
  {
    get => this._contextsCache;
    set => this._contextsCache = value;
  }

  /// <summary>Родительский контекст конфигуратора составов IPS</summary>
  public PdmConfiguratorContext ParentContext
  {
    get
    {
      if (this._parentKey == null || this._parentKey.Empty || this.CheckCycle(this._parentKey))
        return (PdmConfiguratorContext) null;
      PdmConfiguratorContextsCache contextsCache = this.ContextsCache;
      if (contextsCache == null)
        return (PdmConfiguratorContext) null;
      PdmConfiguratorContext configuratorContext = contextsCache[this._parentKey];
      return configuratorContext == this ? (PdmConfiguratorContext) null : configuratorContext;
    }
  }

  /// <summary>
  /// Ключ родительского контекста конфигуратора составов IPS
  /// </summary>
  public RelationPair ParentKey
  {
    [DebuggerStepThrough] get => this._parentKey;
    set
    {
      if (value != null && !this.CanBeParent(value))
        return;
      this._parentKey = value;
    }
  }

  /// <summary>Создать пустой контекст конфигуратора составов IPS</summary>
  /// <param name="contextsCache">Ссылка на кэш-владелец</param>
  public PdmConfiguratorContext(PdmConfiguratorContextsCache contextsCache)
  {
    ++PdmConfiguratorContext._index;
    this.Handle = PdmConfiguratorContext._index;
    this.ContextsCache = contextsCache;
  }

  /// <summary>
  /// Создать контекст конфигуратора составов IPS и заполнить его информацией
  /// из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник (string, PdmConfiguratorContext, IDBAttributable, ObjectOptionsHolder)</param>
  public PdmConfiguratorContext(object source)
  {
    ++PdmConfiguratorContext._index;
    this.Handle = PdmConfiguratorContext._index;
    this.Assign(source);
  }

  /// <summary>
  /// Обновить дату и время модификации содержимого контекста
  /// </summary>
  public void Touch()
  {
    lock (this.OptionsValues)
      this.ModifiedAt = DateTime.UtcNow;
  }

  /// <summary>
  /// Получить ID значения опции, назначенное контексту. Если в контексте значение опции не назначено,
  /// будет выполнен поиск в родительском контексте. Если ничего не будет найдено, свойство вернёт
  /// значение Guid.Empty
  /// </summary>
  /// <param name="option">Guid опции, значение которой требуется найти в контексте конфигуратора составов IPS</param>
  /// <returns>Собственное, либо унаследованное значение опции, либо String.Empty, если значение не найдено</returns>
  public string this[Guid option]
  {
    get
    {
      lock (this.OptionsValues)
      {
        if (this.OptionsValues.ContainsKey(option))
        {
          if (!string.IsNullOrEmpty(this.OptionsValues[option]))
            return this.OptionsValues[option];
        }
      }
      return this.ParentContext == null ? string.Empty : this.ParentContext[option];
    }
    set
    {
      try
      {
        if (option == Guid.Empty)
          return;
        lock (this.OptionsValues)
        {
          if (value == string.Empty && this.OptionsValues.ContainsKey(option))
            this.OptionsValues.Remove(option);
          else
            this.OptionsValues[option] = value;
        }
      }
      finally
      {
        this.Touch();
      }
    }
  }

  /// <summary>
  /// Получить ID значения опции, назначенное контексту. Если в контексте значение опции не назначено,
  /// будет выполнен поиск в родительском контексте. Если ничего не будет найдено, свойство вернёт
  /// значение String.Empty
  /// </summary>
  /// <param name="optionGuid">Guid опции (в виде строки), значение которой требуется найти в контексте конфигуратора составов IPS</param>
  /// <returns>Собственное, либо унаследованное значение опции, либо String.Empty, если значение не найдено</returns>
  public string this[string optionGuid]
  {
    get
    {
      Guid guid = new Guid(optionGuid);
      lock (this.OptionsValues)
      {
        if (this.OptionsValues.ContainsKey(guid))
        {
          if (!string.IsNullOrEmpty(this.OptionsValues[guid]))
            return this.OptionsValues[guid];
        }
      }
      return this.ParentContext == null ? string.Empty : this.ParentContext[guid];
    }
    set
    {
      try
      {
        Guid key = new Guid(optionGuid);
        if (key == Guid.Empty)
          return;
        lock (this.OptionsValues)
        {
          if (value == string.Empty && this.OptionsValues.ContainsKey(key))
            this.OptionsValues.Remove(key);
          else
            this.OptionsValues[key] = value;
        }
      }
      finally
      {
        this.Touch();
      }
    }
  }

  /// <summary>Назначить порядковый номер значения опции в контексте</summary>
  /// <param name="option">Идентификатор версии объекта опции</param>
  /// <returns>Собственное значение опции</returns>
  public int this[long option]
  {
    set
    {
      try
      {
        OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(option);
        if (option1 == null)
          return;
        lock (this.OptionsValues)
        {
          OptionValue optionValue = option1.OptionValues.Count <= value || value < 0 ? (OptionValue) null : option1.OptionValues[value];
          if (optionValue == null)
            this.OptionsValues.Remove(option1.OptionGuid);
          else
            this.OptionsValues[option1.OptionGuid] = optionValue.ID;
        }
      }
      finally
      {
        this.Touch();
      }
    }
  }

  /// <summary>
  /// Метод позволяет получить значения опций из контекста и всех его родительских контекстов
  /// </summary>
  /// <returns></returns>
  public Dictionary<Guid, string> ExpandContext()
  {
    Dictionary<Guid, string> dictionary = (Dictionary<Guid, string>) null;
    lock (this.OptionsValues)
      dictionary = new Dictionary<Guid, string>((IDictionary<Guid, string>) this.OptionsValues);
    for (PdmConfiguratorContext parentContext = this.ParentContext; parentContext != null; parentContext = parentContext.ParentContext)
    {
      if (parentContext.OptionsValues != null)
      {
        lock (parentContext.OptionsValues)
        {
          foreach (KeyValuePair<Guid, string> optionsValue in parentContext.OptionsValues)
          {
            if (!dictionary.ContainsKey(optionsValue.Key) && !string.IsNullOrEmpty(optionsValue.Value))
              dictionary.Add(optionsValue.Key, optionsValue.Value);
          }
        }
      }
    }
    return dictionary;
  }

  /// <summary>
  /// Выполнить указанный оператор по отношению к значениям опций из кэша
  /// </summary>
  /// <param name="option1Guid">Guid первой опции</param>
  /// <param name="value1ID">Guid первого значения</param>
  /// <param name="option2Guid">Guid второй опции</param>
  /// <param name="value2ID">Guid второго значения</param>
  /// <param name="operat">Оператор для сравнения значений</param>
  /// <returns>Результат выполнения оператора, либо исключение, если какое-то из значений не совместимо
  /// по типу данных с другим значением, либо не найдено</returns>
  public PdmConfiguratorResult CacheEqualsValues(
    Guid option1Guid,
    string value1ID,
    Guid option2Guid,
    string value2ID,
    Operator operat)
  {
    if (string.IsNullOrEmpty(value1ID) || string.IsNullOrEmpty(value2ID))
      return PdmConfiguratorResult.False;
    int num = PdmConfiguratorCache.CacheCompareValues(option1Guid, value1ID, option2Guid, value2ID);
    switch (operat)
    {
      case Operator.Less:
        return Helper.Bool2PdmConfiguratorResult(num < 0);
      case Operator.LessEquals:
        return Helper.Bool2PdmConfiguratorResult(num <= 0);
      case Operator.Equals:
        return Helper.Bool2PdmConfiguratorResult(num == 0);
      case Operator.GreaterEquals:
        return Helper.Bool2PdmConfiguratorResult(num >= 0);
      case Operator.Greater:
        return Helper.Bool2PdmConfiguratorResult(num > 0);
      case Operator.NotEquals:
        return Helper.Bool2PdmConfiguratorResult(num != 0);
      default:
        return PdmConfiguratorResult.False;
    }
  }

  public IEnumerable<PdmConfiguratorContext> GetAncestorsAndSelf()
  {
    yield return this;
    for (PdmConfiguratorContext parentContext = this.ParentContext; parentContext != null; parentContext = parentContext.ParentContext)
      yield return parentContext;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>
  /// Очистить содержимое контекста конфигуратора составов IPS
  /// </summary>
  public void Clear()
  {
    lock (this.OptionsValues)
    {
      this.OptionsValues.Clear();
      this.ContextType = PdmContextType.Unknown;
    }
    this.Touch();
  }

  /// <summary>Очистить ключи контекста</summary>
  public void ClearKeys()
  {
    if (this.Key != null)
      this.Key.Clear();
    if (this.ParentKey == null)
      return;
    this.ParentKey.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта</summary>
  /// <param name="source">Объект-источник (PdmConfiguratorContext, IDBConfiguratorOption)</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case null:
        break;
      case string _:
        this.FromString((string) source);
        break;
      case PdmConfiguratorContext configuratorContext:
        lock (this.OptionsValues)
        {
          this.OptionsValues = new Dictionary<Guid, string>((IDictionary<Guid, string>) configuratorContext.OptionsValues);
          this.Key.Assign((object) configuratorContext.Key);
          this.ParentKey = configuratorContext.ParentKey == null || configuratorContext.ParentKey.Empty ? (RelationPair) null : new RelationPair((object) configuratorContext.ParentKey);
          this.ContextType = configuratorContext.ContextType;
          this.ContextsCache = configuratorContext.ContextsCache;
          this.Services = configuratorContext.Services;
          if (configuratorContext.ObjectsOptions.Count > 0)
            this.ObjectsOptions.Clear();
          for (int index = 0; index < configuratorContext.ObjectsOptions.Count; ++index)
            this.ObjectsOptions.Add(configuratorContext.ObjectsOptions[index].Clone() as ObjectOptionsHolder);
          this.SyncOptionsList(false);
          break;
        }
      case ObjectOptionsHolder objectOptionsHolder1:
        lock (this.OptionsValues)
        {
          this.ObjectsOptions.Add(objectOptionsHolder1);
          this.CorrectOptionValues();
          break;
        }
      default:
        IDBRelation dbRelation = source as IDBRelation;
        IDBObject source1 = source as IDBObject;
        if ((dbRelation == null || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(dbRelation.RelationType)) && source1 != null && MetaDataHelper.IsPdmConfigurableObjectType(source1.ObjectType) && !MetaDataHelper.IsPdmContextableObjectType(source1.ObjectType))
        {
          lock (this.OptionsValues)
          {
            PdmConfiguratorContext source2 = (PdmConfiguratorContext) null;
            if (source2 != null)
              this.Assign((object) source2);
            if (this.ObjectsOptions.Count == 0)
              this.ObjectsOptions.Add(PdmConfiguratorObjectOptionsCache.GetObjectOptions(this.Key.F_PROJ_ID) ?? new ObjectOptionsHolder((object) source1));
            this.ContextType = PdmContextType.ConfigurableObject;
            this.CorrectOptionValues();
          }
          if (this.ContextsCache == null || this.Key.Empty)
            break;
          this.ContextsCache[this.Key] = this.Clone() as PdmConfiguratorContext;
          break;
        }
        if (dbRelation != null && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(dbRelation.RelationType))
        {
          lock (this.OptionsValues)
          {
            IDBAttribute attributeById = dbRelation.GetAttributeByID(Consts.attributeConfiguratorContextID);
            if (attributeById != null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              if (attributeById.ValuesCount == 1)
              {
                stringBuilder.Append(DataSetProcessor.GetStringValue(attributeById.Value, string.Empty));
              }
              else
              {
                object[] values = attributeById.Values;
                if (values != null)
                {
                  for (int index = 0; index < values.Length; ++index)
                    stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
                }
              }
              this.FromString(stringBuilder.ToString());
              this.ContextType = PdmContextType.ContextRelation;
            }
            ObjectOptionsHolder objectOptionsHolder = PdmConfiguratorObjectOptionsCache.GetObjectOptions(this.Key.F_PROJ_ID) ?? new ObjectOptionsHolder((object) source1);
            if (objectOptionsHolder.ObjectID == 0L)
              objectOptionsHolder.Assign((object) dbRelation.Session.GetObject(this.Key.F_PROJ_ID, false));
            this.ObjectsOptions.Add(objectOptionsHolder);
            this.CorrectOptionValues();
          }
          if (this.ContextsCache == null || this.Key.Empty)
            break;
          this.ContextsCache[this.Key] = this.Clone() as PdmConfiguratorContext;
          break;
        }
        if (this.Key.Empty || source1 == null || !MetaDataHelper.IsPdmContextableObjectType(source1.ObjectType))
          break;
        lock (this.OptionsValues)
        {
          PdmConfiguratorContext source3 = (PdmConfiguratorContext) null;
          if (source3 != null)
          {
            this.Assign((object) source3);
          }
          else
          {
            IDBAttribute attributeById = source1.GetAttributeByID(Consts.attributeConfiguratorContextID);
            if (attributeById != null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              if (attributeById.ValuesCount == 1)
              {
                stringBuilder.Append(DataSetProcessor.GetStringValue(attributeById.Value, string.Empty));
              }
              else
              {
                object[] values = attributeById.Values;
                if (values != null)
                {
                  for (int index = 0; index < values.Length; ++index)
                    stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
                }
              }
              this.FromString(stringBuilder.ToString());
              this.ContextType = PdmContextType.ContextObject;
            }
            if (this.Key.F_PROJ_ID != 0L && this.Key.F_PROJ_ID != this.Key.TOP_OBJECT_ID)
              this.ObjectsOptions.AddRange((IEnumerable<ObjectOptionsHolder>) PdmConfiguratorObjectOptionsCache.CacheLoadObjectsOptions(source1.Session, this.Key.F_PROJ_ID, MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545"), "cad005aa-306c-11d8-b4e9-00304f19f545"));
            else
              this.ObjectsOptions.Add(PdmConfiguratorObjectOptionsCache.GetObjectOptions(this.Key.TOP_OBJECT_ID) ?? new ObjectOptionsHolder((object) source1));
          }
          this.CorrectOptionValues();
        }
        if (this.ContextsCache == null || this.Key.Empty)
          break;
        this.ContextsCache[this.Key] = (PdmConfiguratorContext) this.Clone();
        break;
    }
  }

  /// <summary>
  /// Выполнить проверку контекста перед сохранением, выдать исключение, если есть ошибки
  /// </summary>
  private void BeforeSave()
  {
    IUserSession service1 = this.Services.GetService(typeof (IUserSession)) as IUserSession;
    IDBRelation service2 = this.Services.GetService(typeof (object)) as IDBRelation;
    int childType = -1;
    if (service2 != null)
      childType = service1.GetObjectInfo(service2.ProjID).ObjectTypeID;
    bool flag = this.Key != null && !this.Key.Empty && childType != -1 && MetaDataHelper.IsObjectTypeChildOf(childType, MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545"));
    foreach (KeyValuePair<Guid, string> optionsValue in this.OptionsValues)
    {
      if ((flag || this.IsObligatoryOption(optionsValue.Key)) && string.IsNullOrEmpty(optionsValue.Value))
      {
        OptionHolder orLoadOption = PdmConfiguratorCache.CacheFindOrLoadOption(service1, optionsValue.Key);
        string message = orLoadOption != null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_46"), (object) orLoadOption.OptionCaption) : LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_47");
        if (flag && !this.IsObligatoryOption(optionsValue.Key))
          message = orLoadOption != null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_48"), (object) orLoadOption.OptionCaption) : LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_49");
        throw new PdmConfiguratorExeption(message);
      }
    }
  }

  /// <summary>
  /// Откорректировать значения опций, если в контексте есть опции
  /// </summary>
  private void CorrectOptionValues()
  {
    IUserSession service = this.Services.GetService(typeof (IUserSession)) as IUserSession;
    if (this.ObjectsOptions.Count > 0)
    {
      for (int index1 = 0; index1 < this.ObjectsOptions.Count; ++index1)
      {
        ObjectOptionsHolder objectsOption = this.ObjectsOptions[index1];
        for (int index2 = 0; index2 < objectsOption.Options.Count; ++index2)
        {
          long option = objectsOption.Options[index2];
          if (PdmConfiguratorCache.CacheFindOption(option) == null && service != null)
            PdmConfiguratorCache.CacheAddOption(service, option);
        }
      }
    }
    this.SyncOptionsList(false);
  }

  /// <summary>Загрузить информацию из объекта базы данных</summary>
  /// <param name="obj">Объект-источник</param>
  /// <returns>true - информация загружена успешно, false - были ошибки</returns>
  public bool LoadFromObject(IDBAttributable obj)
  {
    this.Assign((object) obj);
    return true;
  }

  /// <summary>Записать информацию в указанный объект базы данных</summary>
  /// <param name="obj">Объект-назначение</param>
  /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
  public bool SaveToObject(IDBAttributable obj)
  {
    bool flag = false;
    if (obj == null)
      return flag;
    IDBObject dbObject = obj as IDBObject;
    IDBRelation dbRelation = obj as IDBRelation;
    if (dbObject == null && dbRelation == null)
      return flag;
    IMSAttribute4 imsAttribute4 = dbObject != null ? (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, Consts.attributeConfiguratorContextID) : (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(dbRelation.RelationType, Consts.attributeConfiguratorContextID);
    if (imsAttribute4 == null)
      return false;
    List<string> stringList = (List<string>) null;
    lock (this.OptionsValues)
    {
      this.CorrectOptionValues();
      this.BeforeSave();
      stringList = StringsHelper.SplitString(this.ToLongString(Consts.attributeConfiguratorContextID).ToString(), this.GetCompositionConfiguratorContextAttributeMaxValueLength());
    }
    IDBAttribute dbAttribute = obj.GetAttributeByID(Consts.attributeConfiguratorContextID);
    try
    {
      (obj.Session.GetCustomService(typeof (IPdmConfiguratorService)) as IPdmConfiguratorService)[(object) obj.Session.SessionGUID, this.Key] = this;
      if (dbAttribute != null)
      {
        if (this.OptionsValues.Count == 0 || stringList.Count == 0)
        {
          if (imsAttribute4.Required == RequiredModes.Manual)
            dbAttribute.Delete(0L);
          flag = true;
          return flag;
        }
      }
      else
      {
        if (this.OptionsValues.Count == 0 || stringList.Count == 0)
          return flag;
        if (imsAttribute4.Required == RequiredModes.Manual)
          dbAttribute = obj.Attributes.AddAttribute(Consts.attributeConfiguratorContextID, false);
      }
      if (dbAttribute == null)
        return flag;
      dbAttribute.Values = (object[]) stringList.ToArray();
      flag = true;
      return flag;
    }
    finally
    {
      if (flag && this.ContextsCache != null && !this.Key.Empty)
        this.ContextsCache[this.Key] = this.Clone() as PdmConfiguratorContext;
    }
  }

  /// <summary>
  /// Заполнить экземпляр класса информацией из кодированной строки
  /// </summary>
  /// <param name="val">Кодированная строка</param>
  private void FromString(string val)
  {
    lock (this.OptionsValues)
    {
      this.OptionsValues.Clear();
      if (string.IsNullOrEmpty(val))
        return;
      string[] strArray = val.Split(Helper.Splitter, StringSplitOptions.RemoveEmptyEntries);
      if (strArray == null || strArray.Length == 0)
        return;
      Guid key = Guid.Empty;
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (key != Guid.Empty)
        {
          string str = strArray[index];
          if (!string.IsNullOrEmpty(str))
            this.OptionsValues[key] = str;
          key = Guid.Empty;
        }
        else
        {
          string str = strArray[index];
          if (!string.IsNullOrEmpty(str) && GuidHelper.IsGuid(str))
            key = new Guid(str);
        }
      }
    }
  }

  /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
  /// <returns>Значение экземпляра класса в виде строки</returns>
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    lock (this.OptionsValues)
    {
      int num = 0;
      foreach (KeyValuePair<Guid, string> optionsValue in this.OptionsValues)
      {
        if (!string.IsNullOrEmpty(optionsValue.Value))
        {
          stringBuilder.Append(optionsValue.Key.ToString());
          stringBuilder.Append(Helper.SplitterChar);
          stringBuilder.Append(this.CorrectValue(optionsValue.Value));
          if (num < this.OptionsValues.Count - 1)
            stringBuilder.Append(Helper.SplitterChar);
          ++num;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public string ToLongString(int attributeTypeID)
  {
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
      throw new ArgumentException();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.ToString());
    if (stringBuilder.Length == 0)
      return string.Empty;
    stringBuilder.Insert(0, Helper.SplitterChar);
    int num = stringBuilder.Length + 1;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeID);
    stringBuilder.Insert(0, (long) num <= attributeType.SizeType ? "0" : "1");
    return stringBuilder.ToString();
  }

  /// <summary>Заменить в строке все символы '|' на ' '</summary>
  /// <param name="val">Корректируемая строка</param>
  /// <returns>Откорректированная строка</returns>
  private string CorrectValue(string val) => val.Replace(Helper.SplitterChar, ' ');

  private int GetCompositionConfiguratorContextAttributeMaxValueLength()
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Consts.attributeConfiguratorContextID);
    return attributeType == null ? 0 : (int) attributeType.SizeType;
  }

  private bool CanBeParent(RelationPair relationPair)
  {
    if (this.ContextsCache != null && relationPair != null)
    {
      PdmConfiguratorContext configuratorContext = this.ContextsCache[relationPair];
      if (configuratorContext != null && configuratorContext.GetAncestorsAndSelf().Select<PdmConfiguratorContext, RelationPair>((Func<PdmConfiguratorContext, RelationPair>) (o => o.Key)).Contains<RelationPair>(this.Key))
        return false;
    }
    return true;
  }

  private bool CheckCycle(RelationPair relationPair)
  {
    if (this.ContextsCache != null && relationPair != null)
    {
      List<RelationPair> relationPairList = new List<RelationPair>();
      for (PdmConfiguratorContext parentContext = this.ContextsCache[relationPair]; parentContext != null; parentContext = parentContext.ParentContext)
      {
        if (relationPairList.Contains(parentContext.Key))
          return true;
        relationPairList.Add(parentContext.Key);
      }
    }
    return false;
  }

  /// <summary>
  /// Получить список опций, используемых в контексте, отсортированных по категориям
  /// (по именам категорий в алфавитном порядке, а затем )
  /// </summary>
  /// <returns>Отсортированный список опций</returns>
  public List<Guid> GetSortedOptionsList()
  {
    lock (this.OptionsValues)
    {
      List<Guid> sortedOptionsList = new List<Guid>(this.OptionsValues.Count);
      foreach (KeyValuePair<Guid, string> optionsValue in this.OptionsValues)
        sortedOptionsList.Add(optionsValue.Key);
      sortedOptionsList.Sort((IComparer<Guid>) PdmConfiguratorContext.occ);
      return sortedOptionsList;
    }
  }

  /// <summary>
  /// Составить список значений указанной опции, которые доступны во всех объектах контекста
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Список значений указанной опции, которые доступны во всех объектах контекста</returns>
  public List<string> GetOptionVisibleValues(Guid option)
  {
    List<string> optionVisibleValues = new List<string>();
    if (option == Guid.Empty)
      return optionVisibleValues;
    OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(option);
    if (option1 == null)
      return optionVisibleValues;
    lock (this.OptionsValues)
    {
      for (int index = 0; index < option1.OptionValues.Count; ++index)
        optionVisibleValues.Add(option1.OptionValues[index].ID);
      for (int index1 = 0; index1 < this.ObjectsOptions.Count; ++index1)
      {
        if (optionVisibleValues.Count != 0)
        {
          ObjectOptionsHolder objectsOption = this.ObjectsOptions[index1];
          if ((objectsOption.VisibleOptionValues.Items.ContainsKey(option) ? objectsOption.VisibleOptionValues.Items[option] : (List<string>) null) != null)
          {
            for (int index2 = optionVisibleValues.Count - 1; index2 >= 0; --index2)
            {
              if (!objectsOption.VisibleOptionValues.GetVisibleOptionValue(option, optionVisibleValues[index2]))
                optionVisibleValues.RemoveAt(index2);
            }
          }
        }
        else
          break;
      }
    }
    return optionVisibleValues;
  }

  public string GetOptionValue(Guid optionGuid)
  {
    for (PdmConfiguratorContext configuratorContext = this; configuratorContext != null; configuratorContext = configuratorContext.ParentContext)
    {
      string optionValue = (string) null;
      if (configuratorContext.OptionsValues != null && configuratorContext.OptionsValues.TryGetValue(optionGuid, out optionValue))
        return optionValue;
    }
    return (string) null;
  }

  /// <summary>
  /// Проверить, является ли указанная опция обязательной для заполнения в каком-либо объекте контекста
  /// </summary>
  /// <param name="option">Проверяемая опция</param>
  /// <returns>Обязательность заполнения указанной опции</returns>
  public bool IsObligatoryOption(Guid option)
  {
    if (option == Guid.Empty)
      return false;
    lock (this.OptionsValues)
    {
      for (int index = 0; index < this.ObjectsOptions.Count; ++index)
      {
        if (this.ObjectsOptions[index].VisibleOptionValues.GetObligatoryOption(option))
          return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Метод позволяет синхронизировать список опций OptionsValues с коллекцией ObjectsOptions
  /// </summary>
  /// <param name="autoClear">При необходимости удалить из контекста все значения опций, если с контекстом не связан ни один объект</param>
  public void SyncOptionsList(bool autoClear)
  {
    if (this.ObjectsOptions.Count == 0 && this.OptionsValues.Count > 0)
      return;
    List<Guid> guidList = new List<Guid>();
    Dictionary<Guid, bool> dictionary = new Dictionary<Guid, bool>();
    List<long> longList = new List<long>();
    lock (this.OptionsValues)
    {
      for (int index1 = 0; index1 < this.ObjectsOptions.Count; ++index1)
      {
        ObjectOptionsHolder objectsOption = this.ObjectsOptions[index1];
        if (objectsOption.Options.Count != 0)
        {
          for (int index2 = 0; index2 < objectsOption.Options.Count; ++index2)
          {
            longList.Add(objectsOption.Options[index2]);
            Guid optionGuid = PdmConfiguratorCache.CacheFindOptionGuid(objectsOption.Options[index2]);
            if (optionGuid != Guid.Empty && !dictionary.ContainsKey(optionGuid))
              dictionary[optionGuid] = false;
            if (!(optionGuid == Guid.Empty) && !this.OptionsValues.ContainsKey(optionGuid) && (!dictionary.ContainsKey(optionGuid) || !dictionary[optionGuid]))
            {
              this.OptionsValues.Add(optionGuid, objectsOption.VisibleOptionValues.GetDefaultOptionValue(optionGuid) ?? string.Empty);
              dictionary[optionGuid] = true;
            }
          }
          foreach (KeyValuePair<Guid, string> optionsValue in this.OptionsValues)
          {
            if (!dictionary.ContainsKey(optionsValue.Key))
              guidList.Add(optionsValue.Key);
          }
          for (int index3 = 0; index3 < guidList.Count; ++index3)
            this.OptionsValues.Remove(guidList[index3]);
        }
      }
      if (!(longList.Count == 0 & autoClear))
        return;
      this.OptionsValues.Clear();
    }
  }

  /// <summary>
  /// Вспомогательный класс для сравнения опций по их категориям
  /// </summary>
  private class OptionCategoriesComparer : IComparer<Guid>
  {
    /// <summary>Сравнитель строк</summary>
    private static StringComparer sc = StringComparer.Create(CultureInfo.InvariantCulture, true);

    /// <summary>Сравнить две опции по их категориям</summary>
    /// <param name="x">Опция первая</param>
    /// <param name="y">Опция вторая</param>
    /// <returns></returns>
    public int Compare(Guid x, Guid y)
    {
      OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(x);
      OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(y);
      if (option1 == null || option2 == null)
        return 0;
      OptionObjectDescription objectDescription1 = PdmConfiguratorCache.CategoriesCache.ContainsKey(option1.OptionCategory) ? PdmConfiguratorCache.CategoriesCache[option1.OptionCategory] : (OptionObjectDescription) null;
      OptionObjectDescription objectDescription2 = PdmConfiguratorCache.CategoriesCache.ContainsKey(option2.OptionCategory) ? PdmConfiguratorCache.CategoriesCache[option2.OptionCategory] : (OptionObjectDescription) null;
      if (objectDescription1 == null || objectDescription2 == null)
        return 0;
      int num = PdmConfiguratorContext.OptionCategoriesComparer.sc.Compare(objectDescription1.CAPTION, objectDescription2.CAPTION);
      if (num == 0)
        num = PdmConfiguratorContext.OptionCategoriesComparer.sc.Compare(option1.OptionCaption, option2.OptionCaption);
      return num;
    }
  }
}
