// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.MetaData.ImMetaData
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.Collections;
using Intermech.IpsXmlViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace XmlReaderAPI.MetaData;

/// <summary>Метаданные из файла "Портфеля IPS"</summary>
public sealed class ImMetaData : IImMetaData, IAssignable, IDisposable
{
  /// <summary>
  /// Объект для поток.безопасного доступа к содержимому класса
  /// </summary>
  private readonly object _syncRoot = new object();
  /// <summary>Контейнер сервисов</summary>
  internal readonly AdvancedServiceContainer services = new AdvancedServiceContainer();
  /// <summary>
  /// В данном словарике хранятся соответствия [Guid чего-то] =&gt; [ImGlobals - информация о типе метаданных]
  /// </summary>
  private IDictionary<Guid, ImGlobals> _globalsGuid = (IDictionary<Guid, ImGlobals>) new Dictionary<Guid, ImGlobals>();
  /// <summary>
  /// В данном словарике хранятся краткие описания типов объектов
  /// [ID типа объекта] =&gt; [ImObjectType - описание типа объекта]
  /// </summary>
  private IDictionary<int, IImObjectType> _objectTypes = (IDictionary<int, IImObjectType>) new Dictionary<int, IImObjectType>();
  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа объекта] =&gt; [ID типа объекта]
  /// </summary>
  private IDictionary<Guid, int> _objectsGuid2Id = (IDictionary<Guid, int>) new Dictionary<Guid, int>();
  /// <summary>
  /// В данном словарике хранятся краткие описания типов связей
  /// [ID типа связи] =&gt; [ImRelationType - краткое описание типа связи]
  /// </summary>
  private IDictionary<int, IImRelationType> _relationTypes = (IDictionary<int, IImRelationType>) new Dictionary<int, IImRelationType>();
  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа связи] =&gt; [ID типа связи]
  /// </summary>
  private IDictionary<Guid, int> _relationsGuid2Id = (IDictionary<Guid, int>) new Dictionary<Guid, int>();
  /// <summary>
  /// В данном словарике хранятся краткие описания типов атрибутов
  /// [ID типа атрибута] =&gt; [ImAttributeType - описание типа атрибута]
  /// </summary>
  private IDictionary<int, IImAttributeType> _attrTypes = (IDictionary<int, IImAttributeType>) new Dictionary<int, IImAttributeType>();
  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа атрибута] =&gt; [ID типа атрибута]
  /// </summary>
  private IDictionary<Guid, int> _attrsGuid2Id = (IDictionary<Guid, int>) new Dictionary<Guid, int>();
  /// <summary>
  /// В данном словарике хранятся соответствия имён атрибутов их идентификаторам
  /// [Имя типа атрибута] =&gt; [Int32 идентификатор типа атрибута]
  /// </summary>
  private IDictionary<string, int> _attrNameTypes = (IDictionary<string, int>) new Dictionary<string, int>();

  /// <summary>
  /// Объект для поток.безопасного доступа к содержимому класса
  /// </summary>
  public object SyncRoot
  {
    [DebuggerStepThrough] get => this._syncRoot;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this.services;
  }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid чего-то] =&gt; [ImGlobals - информация о типе метаданных]
  /// </summary>
  public IDictionary<Guid, ImGlobals> GlobalsGuid
  {
    [DebuggerStepThrough] get => this._globalsGuid;
  }

  /// <summary>
  /// В данном словарике хранятся краткие описания типов объектов
  /// [ID типа объекта] =&gt; [ImObjectType - описание типа объекта]
  /// </summary>
  public IDictionary<int, IImObjectType> ObjectTypes
  {
    [DebuggerStepThrough] get => this._objectTypes;
  }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа объекта] =&gt; [ID типа объекта]
  /// </summary>
  public IDictionary<Guid, int> ObjectsGuid2ID
  {
    [DebuggerStepThrough] get => this._objectsGuid2Id;
  }

  /// <summary>
  /// В данном словарике хранятся краткие описания типов связей
  /// [ID типа связи] =&gt; [ImRelationType - краткое описание типа связи]
  /// </summary>
  public IDictionary<int, IImRelationType> RelationTypes
  {
    [DebuggerStepThrough] get => this._relationTypes;
  }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа связи] =&gt; [ID типа связи]
  /// </summary>
  public IDictionary<Guid, int> RelationsGuid2ID
  {
    [DebuggerStepThrough] get => this._relationsGuid2Id;
  }

  /// <summary>
  /// В данном словарике хранятся краткие описания типов атрибутов
  /// [ID типа атрибута] =&gt; [ImAttributeType - описание типа атрибута]
  /// </summary>
  public IDictionary<int, IImAttributeType> AttrTypes
  {
    [DebuggerStepThrough] get => this._attrTypes;
  }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа атрибута] =&gt; [ID типа атрибута]
  /// </summary>
  public IDictionary<Guid, int> AttrsGuid2ID
  {
    [DebuggerStepThrough] get => this._attrsGuid2Id;
  }

  /// <summary>
  /// В данном словарике хранятся соответствия имён атрибутов их идентификаторам
  /// [Имя типа атрибута] =&gt; [Int32 идентификатор типа атрибута]
  /// </summary>
  public IDictionary<string, int> AttrNameTypes
  {
    [DebuggerStepThrough] get => this._attrNameTypes;
  }

  /// <summary>
  /// Создать контейнер метаданных и заполнить его из объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ImMetaData(object source) => this.Assign(source);

  /// <summary>
  /// Создать контейнер метаданных и заполнить его из указанного документа
  /// </summary>
  /// <param name="document">Документ XML</param>
  public ImMetaData(XDocument document) => this.Assign((object) document);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this._syncRoot)
    {
      this.GlobalsGuid.Clear();
      this.ObjectTypes.Clear();
      this.ObjectsGuid2ID.Clear();
      this.RelationTypes.Clear();
      this.RelationsGuid2ID.Clear();
      this.AttrTypes.Clear();
      this.AttrsGuid2ID.Clear();
      this.AttrNameTypes.Clear();
    }
  }

  /// <summary>
  /// Заполнить экземпляр класса информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (source == null || this == source)
      return;
    switch (source)
    {
      case XDocument source1:
        this.AssignFromDocument(source1);
        break;
      case ImMetaData source2:
        this.AssignFromMetaData(source2);
        break;
      case XmlReaderAPI.Kernel.Kernel source3:
        this.AssignFromKernel(source3);
        break;
    }
  }

  /// <summary>
  /// Заполнить экземпляр класса информацией из аналогичного контейнера
  /// </summary>
  /// <param name="source">Контейнер с метаданными</param>
  private void AssignFromMetaData(ImMetaData source)
  {
    if (source == null || this == source)
      return;
    this.Clear();
    lock (this._syncRoot)
    {
      this._globalsGuid = CloneHelper.Clone((object) source.GlobalsGuid) as IDictionary<Guid, ImGlobals>;
      this._objectTypes = CloneHelper.Clone((object) source.ObjectTypes) as IDictionary<int, IImObjectType>;
      this._objectsGuid2Id = CloneHelper.Clone((object) source.ObjectsGuid2ID) as IDictionary<Guid, int>;
      this._relationTypes = CloneHelper.Clone((object) source.RelationTypes) as IDictionary<int, IImRelationType>;
      this._relationsGuid2Id = CloneHelper.Clone((object) source.RelationsGuid2ID) as IDictionary<Guid, int>;
      this._attrTypes = CloneHelper.Clone((object) source.AttrTypes) as IDictionary<int, IImAttributeType>;
      this._attrsGuid2Id = CloneHelper.Clone((object) source.AttrsGuid2ID) as IDictionary<Guid, int>;
      this._attrNameTypes = CloneHelper.Clone((object) source.AttrNameTypes) as IDictionary<string, int>;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeTypes"></param>
  /// <param name="objectTypes"></param>
  /// <param name="relationTypes"></param>
  private void ParseCollections(
    List<IImAttributeType> attributeTypes,
    List<IImObjectType> objectTypes,
    List<IImRelationType> relationTypes)
  {
    lock (this._syncRoot)
    {
      attributeTypes.ForEach((Action<IImAttributeType>) (item =>
      {
        this.AttrTypes[item.F_ATTRIBUTE_ID] = item;
        this.AttrNameTypes[item.F_NAME.ToUpperInvariant()] = item.F_ATTRIBUTE_ID;
        if (!(item.F_GUID != Guid.Empty))
          return;
        this.GlobalsGuid[item.F_GUID] = ImGlobals.IMSAttributeType;
        this.AttrsGuid2ID[item.F_GUID] = item.F_ATTRIBUTE_ID;
      }));
      objectTypes.ForEach((Action<IImObjectType>) (item =>
      {
        this.ObjectTypes[item.F_OBJ_TYPE] = item;
        if (!(item.F_GUID != Guid.Empty))
          return;
        this.GlobalsGuid[item.F_GUID] = ImGlobals.IMSObjectType;
        this.ObjectsGuid2ID[item.F_GUID] = item.F_OBJ_TYPE;
      }));
      relationTypes.ForEach((Action<IImRelationType>) (item =>
      {
        this.RelationTypes[item.F_RELATION_TYPE] = item;
        if (!(item.F_GUID != Guid.Empty))
          return;
        this.GlobalsGuid[item.F_GUID] = ImGlobals.IMSRelationType;
        this.RelationsGuid2ID[item.F_GUID] = item.F_RELATION_TYPE;
      }));
    }
  }

  /// <summary>Заполнить экземпляр класса информацией из базы данных</summary>
  /// <param name="source">База данных</param>
  private void AssignFromKernel(XmlReaderAPI.Kernel.Kernel source)
  {
    if (source == null)
      return;
    this.Clear();
    this.ParseCollections(source.GetAttributeTypes(false), source.GetObjectTypes(false), source.GetRelationTypes(false));
  }

  /// <summary>
  /// Заполнить экземпляр класса информацией из документа XML с метаданными
  /// </summary>
  /// <param name="source">Документ с метаданными</param>
  private void AssignFromDocument(XDocument source)
  {
    if (source == null)
      return;
    this.Clear();
    this.ParseCollections(ImAttributeType.Load(source).ConvertAll<IImAttributeType>((Converter<ImAttributeType, IImAttributeType>) (src => (IImAttributeType) src)), ImObjectType.Load(source).ConvertAll<IImObjectType>((Converter<ImObjectType, IImObjectType>) (src => (IImObjectType) src)), ImRelationType.Load(source).ConvertAll<IImRelationType>((Converter<ImRelationType, IImRelationType>) (src => (IImRelationType) src)));
  }

  /// <summary>
  /// Возвращает идентификатор типа объектов по строковому представлению его глобального идентификатора
  /// </summary>
  /// <param name="guid">Guid типа объекта в виде строки</param>
  public int GetObjectTypeID(string guid) => this.GetObjectTypeID(new Guid(guid));

  /// <summary>Получить по Guid типа объекта его Int32-идентификатор</summary>
  /// <param name="objTypeGuid">Guid типа объекта</param>
  /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
  public int GetObjectTypeID(Guid objTypeGuid)
  {
    lock (this._syncRoot)
    {
      int objectTypeId;
      if (this.ObjectsGuid2ID.TryGetValue(objTypeGuid, out objectTypeId))
        return objectTypeId;
    }
    return 0;
  }

  /// <summary>
  /// Получить по Int32-идентификатору типа объекта его Guid-идентификатор
  /// </summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <returns>Идентификатор типа объекта. Guid.Empty - тип объекта не найден</returns>
  public Guid GetObjectTypeGuid(int objTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.ObjectTypes.ContainsKey(objTypeId))
        return this.ObjectTypes[objTypeId].F_GUID;
    }
    return Guid.Empty;
  }

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе объекта
  /// </summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <returns>true, если тип объекта существует</returns>
  public bool ExistsObjectType(int objTypeId)
  {
    lock (this._syncRoot)
      return this.ObjectTypes.ContainsKey(objTypeId);
  }

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе объекта
  /// </summary>
  /// <param name="objTypeGuid">Guid типа объекта</param>
  /// <returns>true, если тип объекта существует</returns>
  public bool ExistsObjectType(Guid objTypeGuid)
  {
    return this.ExistsObjectType(this.GetObjectTypeID(objTypeGuid));
  }

  /// <summary>Получить краткую информацию о типе объекта</summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <returns>Краткая информация о типе объекта или null</returns>
  public IImObjectType GetObjectType(int objTypeId)
  {
    lock (this._syncRoot)
    {
      IImObjectType imObjectType;
      if (this.ObjectTypes.TryGetValue(objTypeId, out imObjectType))
        return (IImObjectType) (imObjectType.Clone() as ImObjectType);
    }
    return (IImObjectType) null;
  }

  /// <summary>Получить краткую информацию о типе объекта</summary>
  /// <param name="objTypeGuid">Идентификатор типа объекта</param>
  /// <returns>Краткая информация о типе объекта или null</returns>
  public IImObjectType GetObjectType(Guid objTypeGuid)
  {
    return this.GetObjectType(this.GetObjectTypeID(objTypeGuid));
  }

  /// <summary>Получить название типа объектов (например, "Детали")</summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <returns>Название типа объектов (например, "Детали")</returns>
  public string GetObjectTypeName(int objTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.ObjectTypes.ContainsKey(objTypeId))
        return this.ObjectTypes[objTypeId].F_OBJ_TYPE_NAME;
    }
    return string.Empty;
  }

  /// <summary>Получить название типа объектов (например, "Детали")</summary>
  /// <param name="objTypeGuid">Идентификатор типа объекта</param>
  /// <returns>Название типа объектов (например, "Детали")</returns>
  public string GetObjectTypeName(Guid objTypeGuid)
  {
    return this.GetObjectTypeName(this.GetObjectTypeID(objTypeGuid));
  }

  /// <summary>Получить список описаний всех типов объектов</summary>
  /// <returns>Список описаний всех типов объектов</returns>
  public IList<IImObjectType> GetObjectTypesList()
  {
    lock (this._syncRoot)
    {
      IImObjectType[] imObjectTypeArray = new IImObjectType[this.ObjectTypes.Count];
      this.ObjectTypes.Values.CopyTo(imObjectTypeArray, 0);
      List<IImObjectType> objectTypesList = new List<IImObjectType>(imObjectTypeArray.Length);
      objectTypesList.AddRange((IEnumerable<IImObjectType>) imObjectTypeArray);
      return (IList<IImObjectType>) objectTypesList;
    }
  }

  /// <summary>Получить по Guid типа связи его Int32-идентификатор</summary>
  /// <param name="relTypeGuid">Guid типа связи</param>
  /// <returns>Идентификатор типа связи. -1 - тип связи не найден</returns>
  public int GetRelationTypeID(Guid relTypeGuid)
  {
    lock (this._syncRoot)
    {
      if (this.RelationsGuid2ID.ContainsKey(relTypeGuid))
        return this.RelationsGuid2ID[relTypeGuid];
    }
    return 0;
  }

  /// <summary>
  /// Получить по Int32-идентификатору типа связи её Guid-идентификатор
  /// </summary>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <returns>Идентификатор типа связи. Guid.Empty - тип связи не найден</returns>
  public Guid GetRelationTypeGuid(int relTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.RelationTypes.ContainsKey(relTypeId))
        return this.RelationTypes[relTypeId].F_GUID;
    }
    return Guid.Empty;
  }

  /// <summary>
  /// Возвращает идентификатор типа связи по строковому представлению её глобального идентификатора
  /// </summary>
  /// <param name="guid">Guid типа связи в виде строки</param>
  public int GetRelationTypeID(string guid) => this.GetRelationTypeID(new Guid(guid));

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе связи
  /// </summary>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <returns>true, если тип связи существует</returns>
  public bool ExistsRelationType(int relTypeId)
  {
    lock (this._syncRoot)
      return this.RelationTypes.ContainsKey(relTypeId);
  }

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе связи
  /// </summary>
  /// <param name="relTypeGuid">Guid типа связи</param>
  /// <returns>true, если тип связи существует</returns>
  public bool ExistsRelationType(Guid relTypeGuid)
  {
    return this.ExistsRelationType(this.GetRelationTypeID(relTypeGuid));
  }

  /// <summary>Получить краткую информацию о типе связи</summary>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <returns>Краткая информация о типе связи или null</returns>
  public IImRelationType GetRelationType(int relTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.RelationTypes.ContainsKey(relTypeId))
        return this.RelationTypes[relTypeId].Clone() as IImRelationType;
    }
    return (IImRelationType) null;
  }

  /// <summary>Получить краткую информацию о типе связи</summary>
  /// <param name="relTypeGuid">Идентификатор типа связи</param>
  /// <returns>Краткая информация о типе связи или null</returns>
  public IImRelationType GetRelationType(Guid relTypeGuid)
  {
    return this.GetRelationType(this.GetRelationTypeID(relTypeGuid));
  }

  /// <summary>
  /// Получить название типа связи (например, "Проектная связь")
  /// </summary>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <returns>Название типа связи (например, "")</returns>
  public string GetRelationTypeName(int relTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.RelationTypes.ContainsKey(relTypeId))
        return this.RelationTypes[relTypeId].F_TYPE_NAME;
    }
    return string.Empty;
  }

  /// <summary>
  /// Получить название типа связи (например, "Проектная связь")
  /// </summary>
  /// <param name="relTypeGuid">Идентификатор типа связи</param>
  /// <returns>Название типа связи (например, "Проектная связь")</returns>
  public string GetRelationTypeName(Guid relTypeGuid)
  {
    return this.GetRelationTypeName(this.GetRelationTypeID(relTypeGuid));
  }

  /// <summary>Получить список описаний всех типов связей</summary>
  /// <returns>Список описаний всех типов связей</returns>
  public IList<IImRelationType> GetRelationTypesList()
  {
    lock (this._syncRoot)
    {
      IImRelationType[] imRelationTypeArray = new IImRelationType[this.RelationTypes.Count];
      this.RelationTypes.Values.CopyTo(imRelationTypeArray, 0);
      List<IImRelationType> list = ((IEnumerable<IImRelationType>) imRelationTypeArray).ToList<IImRelationType>();
      list.AddRange((IEnumerable<IImRelationType>) imRelationTypeArray);
      return (IList<IImRelationType>) list;
    }
  }

  /// <summary>
  /// Получить по Guid типа атрибута его Int32-идентификатор
  /// </summary>
  /// <param name="attrTypeGuid">Guid типа атрибута</param>
  /// <returns>Идентификатор типа атрибута. -1 - тип атрибута не найден</returns>
  public int GetAttributeTypeID(Guid attrTypeGuid)
  {
    lock (this._syncRoot)
    {
      int attributeTypeId;
      if (this.AttrsGuid2ID.TryGetValue(attrTypeGuid, out attributeTypeId))
        return attributeTypeId;
    }
    return 0;
  }

  /// <summary>
  /// Получить по Int32-идентификатору типа атрибута его Guid-идентификатор
  /// </summary>
  /// <param name="attrTypeId">Идентификатор типа атрибута</param>
  /// <returns>Идентификатор типа атрибута. Guid.Empty - тип атрибута не найден</returns>
  public Guid GetAttributeTypeGuid(int attrTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.AttrTypes.ContainsKey(attrTypeId))
        return this.AttrTypes[attrTypeId].F_GUID;
    }
    return Guid.Empty;
  }

  /// <summary>
  /// Возвращает идентификатор типа атрибута по строковому представлению его глобального идентификатора
  /// </summary>
  /// <param name="guid">Guid типа атрибута в виде строки</param>
  public int GetAttributeTypeID(string guid) => this.GetAttributeTypeID(new Guid(guid));

  /// <summary>
  /// Возвращает идентификатор типа атрибута по его названию
  /// </summary>
  /// <param name="attrName">Название типа атрибута</param>
  public int GetAttributeByTypeNameID(string attrName)
  {
    lock (this._syncRoot)
    {
      int attributeByTypeNameId;
      if (this.AttrNameTypes.TryGetValue(attrName.Trim().ToUpperInvariant(), out attributeByTypeNameId))
        return attributeByTypeNameId;
    }
    return 0;
  }

  /// <summary>Возвращает Guid типа атрибута по его названию</summary>
  /// <param name="attrName">Название типа атрибута</param>
  public Guid GetAttributeByTypeNameGuid(string attrName)
  {
    lock (this._syncRoot)
    {
      int key;
      if (this.AttrNameTypes.TryGetValue(attrName.Trim().ToUpperInvariant(), out key))
      {
        if (this.AttrTypes.ContainsKey(key))
          return this.AttrTypes[key].F_GUID;
      }
    }
    return Guid.Empty;
  }

  /// <summary>Получить список всех типов атрибутов</summary>
  /// <returns>Список всех типов атрибутов</returns>
  public IList<int> GetAttributeTypesIDList()
  {
    lock (this._syncRoot)
    {
      int[] numArray = new int[this.AttrTypes.Count];
      this.AttrTypes.Keys.CopyTo(numArray, 0);
      return (IList<int>) new List<int>((IEnumerable<int>) numArray);
    }
  }

  /// <summary>Получить список Guid всех типов атрибутов</summary>
  /// <returns>Список Guid всех типов атрибутов</returns>
  public IList<Guid> GetAttributeTypesGuidList()
  {
    lock (this._syncRoot)
    {
      List<Guid> attributeTypesGuidList = new List<Guid>(this.AttrTypes.Count);
      foreach (KeyValuePair<int, IImAttributeType> attrType in (IEnumerable<KeyValuePair<int, IImAttributeType>>) this.AttrTypes)
        attributeTypesGuidList.Add(attrType.Value.F_GUID);
      return (IList<Guid>) attributeTypesGuidList;
    }
  }

  /// <summary>Получить список описаний всех типов атрибутов</summary>
  /// <returns>Список описаний всех типов атрибутов</returns>
  public IList<IImAttributeType> GetAttributeTypesList()
  {
    lock (this._syncRoot)
    {
      IImAttributeType[] imAttributeTypeArray = new IImAttributeType[this.AttrTypes.Count];
      this.AttrTypes.Values.CopyTo(imAttributeTypeArray, 0);
      return (IList<IImAttributeType>) ((IEnumerable<IImAttributeType>) imAttributeTypeArray).ToList<IImAttributeType>();
    }
  }

  /// <summary>
  /// Получить Int32-идентификатор типа атрибута по его имени, Guid или числовому идентификатору.
  /// Генерируем исключение, если в метод засунуть объект некорректного типа
  /// </summary>
  /// <param name="attributeId">Имя атрибута, Guid или числовой идентификатор</param>
  /// <returns>Int32-идентификатор или Consts.UnknownIDx32, если тип атрибута не найден</returns>
  public int GetAttributeID(object attributeId)
  {
    switch (attributeId)
    {
      case null:
        return 0;
      case int attributeId1:
        return attributeId1;
      case Guid attrTypeGuid:
        return this.GetAttributeTypeID(attrTypeGuid);
      case string attrName:
        return this.GetAttributeByTypeNameID(attrName);
      default:
        return Convert.ToInt32(attributeId);
    }
  }

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе атрибута
  /// </summary>
  /// <param name="attrTypeId">Идентификатор типа атрибута</param>
  /// <returns>true, если тип атрибута существует</returns>
  public bool ExistsAttributeType(int attrTypeId)
  {
    lock (this._syncRoot)
      return this.AttrTypes.ContainsKey(attrTypeId);
  }

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе атрибута
  /// </summary>
  /// <param name="attrTypeGuid">Guid типа атрибута</param>
  /// <returns>true, если тип атрибута существует</returns>
  public bool ExistsAttributeType(Guid attrTypeGuid)
  {
    return this.ExistsAttributeType(this.GetAttributeTypeID(attrTypeGuid));
  }

  /// <summary>Получить краткую информацию о типе атрибута</summary>
  /// <param name="attrTypeId">Идентификатор типа атрибута</param>
  /// <returns>Краткая информация о типе атрибута или null</returns>
  public IImAttributeType GetAttributeType(int attrTypeId)
  {
    lock (this._syncRoot)
    {
      IImAttributeType imAttributeType;
      if (this.AttrTypes.TryGetValue(attrTypeId, out imAttributeType))
        return imAttributeType.Clone() as IImAttributeType;
    }
    return (IImAttributeType) null;
  }

  /// <summary>Хранятся ли в атрибуте системные данные</summary>
  /// <param name="attrTypeId">Идентификатор типа атрибута</param>
  /// <returns>true, если в атрибуте хранятся системные данные</returns>
  public bool HasAttributeSystemData(int attrTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.AttrTypes.ContainsKey(attrTypeId))
        return this.AttrTypes[attrTypeId].F_ATTRIBUTE_TYPE == 15;
    }
    return false;
  }

  /// <summary>Хранятся ли в атрибуте системные данные</summary>
  /// <param name="attrTypeGuid">Guid типа атрибута</param>
  /// <returns>true, если в атрибуте хранятся системные данные</returns>
  public bool HasAttributeSystemData(Guid attrTypeGuid)
  {
    return this.HasAttributeSystemData(this.GetAttributeTypeID(attrTypeGuid));
  }

  /// <summary>Получить краткую информацию о типе атрибута</summary>
  /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
  /// <returns>Краткая информация о типе атрибута или null</returns>
  public IImAttributeType GetAttributeType(Guid attrTypeGuid)
  {
    return this.GetAttributeType(this.GetAttributeTypeID(attrTypeGuid));
  }

  /// <summary>Получить название типа атрибута</summary>
  /// <param name="attrTypeId">Идентификатор типа атрибута</param>
  /// <returns>Название типа атрибута</returns>
  public string GetAttributeTypeName(int attrTypeId)
  {
    lock (this._syncRoot)
    {
      if (this.AttrTypes.ContainsKey(attrTypeId))
        return this.AttrTypes[attrTypeId].F_NAME;
    }
    return string.Empty;
  }

  /// <summary>Получить название типа атрибута</summary>
  /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
  /// <returns>Название типа атрибута</returns>
  public string GetAttributeTypeName(Guid attrTypeGuid)
  {
    return this.GetAttributeTypeName(this.GetAttributeTypeID(attrTypeGuid));
  }

  /// <summary>
  /// Получить по Guid какого-то элемента метаданных его тип
  /// </summary>
  /// <param name="guid">Guid какого-то элемента метаданных</param>
  /// <returns>Тип метаданных для указанного элемента</returns>
  public ImGlobals GetGlobalsByGuid(Guid guid)
  {
    lock (this._syncRoot)
    {
      if (this.GlobalsGuid.ContainsKey(guid))
        return this.GlobalsGuid[guid];
    }
    return ImGlobals.Unknown;
  }

  /// <summary>Отыскать описание элемента метаданных по его Guid</summary>
  /// <param name="type">Тип метаданных</param>
  /// <param name="guid">Guid элемента метаданных</param>
  /// <returns>Описание элемента метаданных</returns>
  private IDisplayable GetMetaDataDisplayableByGuid(ImGlobals type, Guid guid)
  {
    switch (type)
    {
      case ImGlobals.IMSAttributeType:
        return (IDisplayable) this.GetAttributeType(guid);
      case ImGlobals.IMSObjectType:
        return (IDisplayable) this.GetObjectType(guid);
      case ImGlobals.IMSRelationType:
        return (IDisplayable) this.GetRelationType(guid);
      default:
        return (IDisplayable) null;
    }
  }

  /// <summary>
  /// Получить по Guid какого-то элемента метаданных его описание
  /// </summary>
  /// <param name="guid">Guid какого-то элемента метаданных</param>
  /// <returns>Описание метаданных для указанного элемента</returns>
  public IDisplayable GetDisplayableByGuid(Guid guid)
  {
    if (guid == Guid.Empty)
      return (IDisplayable) null;
    lock (this._syncRoot)
    {
      if (this.GlobalsGuid.ContainsKey(guid))
        return this.GetMetaDataDisplayableByGuid(this.GlobalsGuid[guid], guid);
    }
    return (IDisplayable) null;
  }

  /// <summary>Проверить, есть ли метаданные в документе XML</summary>
  /// <param name="document">Документ XML</param>
  /// <returns>true - секция метаданных найдена в указанном документе</returns>
  public static bool HasMetaDataSection(XDocument document)
  {
    return document?.Element((XName) "METADATABRIEF") != null;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose() => this.services.Dispose();
}
