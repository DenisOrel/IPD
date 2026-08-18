// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelConfigurationParser
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBModelConfigurationParser
{
  private static readonly Guid ObjectIdAttributeGuid = new Guid("CAD00029-306C-11D8-B4E9-00304F19F545");
  private static readonly Guid RelationIdAttributeGuid = new Guid("CAD00033-306C-11D8-B4E9-00304F19F545");
  private static readonly Guid RelationGuidAttributeGuid = new Guid("CAD00344-306C-11D8-B4E9-00304F19F545");
  private static IDictionary<string, DataPropertyMapping> emptyAttributeMappings = (IDictionary<string, DataPropertyMapping>) new Dictionary<string, DataPropertyMapping>(0);
  private DataPropertyHelper dataPropertyHelper;
  private List<DBObjectEntityParserData> dbObjectTypes;
  private List<DBRelationEntityParserData> dbRelationTypes;
  private List<DBEntityParserData> allEntityTypes;

  public DBModelConfigurationParser()
  {
    this.dataPropertyHelper = DataPropertyHelper.DefaultInstance;
  }

  public DBModelConfigurationBuilderResult ParseModel(
    ICollection<DBObjectEntityBuilder> dbObjectBuilders,
    ICollection<DBRelationEntityBuilder> dbRelationBuilders)
  {
    if (dbObjectBuilders == null)
      throw new ArgumentNullException(nameof (dbObjectBuilders));
    if (dbRelationBuilders == null)
      throw new ArgumentNullException(nameof (dbRelationBuilders));
    try
    {
      this.Initialize(dbObjectBuilders, dbRelationBuilders);
      return this.ParseInternal();
    }
    finally
    {
      this.Cleanup();
    }
  }

  private void Initialize(
    ICollection<DBObjectEntityBuilder> dbObjectBuilders,
    ICollection<DBRelationEntityBuilder> dbRelationBuilders)
  {
    int capacity = dbObjectBuilders.Count + dbRelationBuilders.Count;
    this.dbObjectTypes = new List<DBObjectEntityParserData>(capacity);
    this.dbRelationTypes = new List<DBRelationEntityParserData>(capacity);
    this.allEntityTypes = new List<DBEntityParserData>(capacity);
    foreach (DBObjectEntityBuilder dbObjectBuilder in (IEnumerable<DBObjectEntityBuilder>) dbObjectBuilders)
      this.dbObjectTypes.Add(new DBObjectEntityParserData(dbObjectBuilder.EntityType));
    foreach (DBRelationEntityBuilder dbRelationBuilder in (IEnumerable<DBRelationEntityBuilder>) dbRelationBuilders)
      this.dbRelationTypes.Add(new DBRelationEntityParserData(dbRelationBuilder.ChildOccurenceType));
    this.allEntityTypes.AddRange((IEnumerable<DBEntityParserData>) this.dbObjectTypes);
    this.allEntityTypes.AddRange((IEnumerable<DBEntityParserData>) this.dbRelationTypes);
  }

  private void Cleanup()
  {
    this.dbObjectTypes = (List<DBObjectEntityParserData>) null;
    this.dbRelationTypes = (List<DBRelationEntityParserData>) null;
    this.allEntityTypes = (List<DBEntityParserData>) null;
  }

  private DBModelConfigurationBuilderResult ParseInternal()
  {
    foreach (DBEntityParserData allEntityType in this.allEntityTypes)
    {
      this.InitializeEntityTypeData(allEntityType);
      this.ParseEntityType(allEntityType);
      this.ParseDataProperties(allEntityType);
      this.CheckForDuplicateDataPropertyMappings(allEntityType);
      this.ParseKeyProperty(allEntityType);
    }
    this.BeginParsingNavigationProperties();
    this.EndParsingNavigationProperties();
    this.CheckForIncompleteNavigationProperties();
    this.CheckForIncompatibleNavigationPropertyTypes();
    this.CheckForDuplicateDBObjectTypeMappings();
    this.CheckForInvalidProperties();
    this.MapToDatabase();
    return this.CreateTypeDescriptors();
  }

  private void InitializeEntityTypeData(DBEntityParserData entityTypeData)
  {
    entityTypeData.MappablePropertiesParserData = this.FindMappableProperties(entityTypeData.EntityType);
    entityTypeData.DataPropertiesParserData = new Dictionary<string, DataPropertyParserData>(entityTypeData.MappablePropertiesParserData.Count);
    entityTypeData.DataPropertiesDescriptors = new Dictionary<string, DataPropertyDescriptor>(entityTypeData.MappablePropertiesParserData.Count);
    switch (entityTypeData.EntityKind)
    {
      case DBEntityKind.Object:
        DBObjectEntityParserData entityParserData = (DBObjectEntityParserData) entityTypeData;
        entityParserData.DataPropertiesMappings = new Dictionary<string, DataPropertyMapping>(entityTypeData.MappablePropertiesParserData.Count);
        entityParserData.NavigationPropertiesParserData = new Dictionary<string, DBObjectNavigationPropertyParserData>();
        entityParserData.NavigationPropertiesDescriptors = new Dictionary<string, NavigationPropertyDescriptor>();
        entityParserData.NavigationPropertiesMappings = new Dictionary<string, DBObjectNavigationPropertyMapping>();
        break;
      case DBEntityKind.Relation:
        ((DBRelationEntityParserData) entityTypeData).NavigationPropertiesParserData = new Dictionary<string, DBRelationNavigationPropertyParserData>();
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityTypeData.EntityKind);
    }
  }

  private List<ExtendedEntityPropertyInfo> FindMappableProperties(Type entityType)
  {
    PropertyInfo[] properties = entityType.GetProperties();
    List<ExtendedEntityPropertyInfo> mappableProperties = new List<ExtendedEntityPropertyInfo>(properties.Length);
    foreach (PropertyInfo propertyInfo in properties)
    {
      if (!propertyInfo.IsDefined(typeof (NotMappedAttribute), true))
        mappableProperties.Add(new ExtendedEntityPropertyInfo(propertyInfo));
    }
    return mappableProperties;
  }

  private void ParseEntityType(DBEntityParserData entityTypeData)
  {
    switch (entityTypeData.EntityKind)
    {
      case DBEntityKind.Object:
        this.ParseEntityType((DBObjectEntityParserData) entityTypeData);
        break;
      case DBEntityKind.Relation:
        this.ParseEntityType((DBRelationEntityParserData) entityTypeData);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityTypeData.EntityKind);
    }
  }

  private void ParseEntityType(DBObjectEntityParserData entityTypeData)
  {
    entityTypeData.DBObjectTypeGuid = ((this.IsValidDBObjectTypeDeclaration(entityTypeData.EntityType) ? entityTypeData.ReflectionInfo.GetAnnotationAttribute<DBObjectTypeAttribute>(false) : throw new EntityTypeConfigurationException(4, entityTypeData.EntityType, $"Указанный тип доменного объекта '{entityTypeData.EntityType}' не является допустимым. Тип должен быть неабстрактным, не-generic классом.")) ?? throw new EntityTypeConfigurationException(5, entityTypeData.EntityType, $"Тип доменных объектов '{entityTypeData.EntityType}' должен иметь атрибут '{typeof (DBObjectTypeAttribute)}', указывающий соответствующий тип объектов IPS.")).Guid;
    ConstructorInfo constructor = entityTypeData.EntityType.GetConstructor(Type.EmptyTypes);
    if (constructor == (ConstructorInfo) null || !constructor.IsPublic)
      throw new EntityTypeConfigurationException(7, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' отсутствует открытый конструктор по умолчанию.");
  }

  private bool IsValidDBObjectTypeDeclaration(Type entityType)
  {
    return entityType.IsClass && !entityType.IsAbstract && !entityType.IsGenericTypeDefinition;
  }

  private void ParseEntityType(DBRelationEntityParserData entityTypeData)
  {
    ConstructorInfo constructor = entityTypeData.EntityType.GetConstructor(Type.EmptyTypes);
    if (constructor == (ConstructorInfo) null || !constructor.IsPublic)
      throw new EntityTypeConfigurationException(14, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' отсутствует открытый конструктор по умолчанию.");
  }

  private void ParseDataProperties(DBEntityParserData entityTypeData)
  {
    foreach (ExtendedEntityPropertyInfo propertyInfo in CollectionUtils.ExtractAsList<ExtendedEntityPropertyInfo>((IList<ExtendedEntityPropertyInfo>) entityTypeData.MappablePropertiesParserData, new Predicate<ExtendedEntityPropertyInfo>(this.DataPropertyPredicate)))
    {
      string name = propertyInfo.Name;
      entityTypeData.DataPropertiesParserData.Add(name, new DataPropertyParserData(propertyInfo)
      {
        DBAttributeGuid = (propertyInfo.GetAnnotationAttribute<DBAttributeTypeAttribute>(true) ?? throw new EntityTypeConfigurationException(8, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' на свойстве '{name}' должен быть атрибут '{typeof (DBAttributeTypeAttribute)}', указывающий соответствующий тип атрибутов IPS.")).Guid
      });
    }
  }

  private void CheckForDuplicateDataPropertyMappings(DBEntityParserData entityTypeData)
  {
    List<Tuple<string, Guid>> tupleList = new List<Tuple<string, Guid>>(entityTypeData.DataPropertiesParserData.Count);
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in entityTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyData = keyValuePair.Value;
      Tuple<string, Guid> tuple = tupleList.Find((Predicate<Tuple<string, Guid>>) (item => item.Item2 == propertyData.DBAttributeGuid));
      if (tuple != null)
        throw new EntityTypeConfigurationException(9, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' свойства '{tuple.Item1}' и '{propertyData.Name}' отображаются в один и тот же тип атрибутов IPS. Проверьте значения атрибута '{typeof (DBAttributeTypeAttribute)}' у указанных свойств.");
      tupleList.Add(Tuple.Create<string, Guid>(propertyData.Name, propertyData.DBAttributeGuid));
    }
  }

  private bool DataPropertyPredicate(ExtendedEntityPropertyInfo propertyInfo)
  {
    return !propertyInfo.Definition.IsContainer && this.dataPropertyHelper.IsAllowedDataPropertyType(propertyInfo.BasicInfo.PropertyType);
  }

  private void ParseKeyProperty(DBEntityParserData entityTypeData)
  {
    switch (entityTypeData.EntityKind)
    {
      case DBEntityKind.Object:
        this.ParseDBObjectKeyProperty((DBObjectEntityParserData) entityTypeData);
        break;
      case DBEntityKind.Relation:
        this.ParseDBRelationKeyProperty((DBRelationEntityParserData) entityTypeData);
        this.ParseDBRelationGuidProperty((DBRelationEntityParserData) entityTypeData);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityTypeData.EntityKind);
    }
  }

  private void ParseDBObjectKeyProperty(DBObjectEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in entityTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyParserData = keyValuePair.Value;
      if (propertyParserData.DBAttributeGuid == DBModelConfigurationParser.ObjectIdAttributeGuid)
      {
        entityTypeData.KeyProperty = propertyParserData;
        break;
      }
    }
    if (entityTypeData.KeyProperty == null)
      throw new EntityTypeConfigurationException(11, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' не указано ключевое свойство. Укажите одно из свойств с помощью атрибута '{typeof (DBAttributeTypeAttribute)}' с значением '{"CAD00029-306C-11D8-B4E9-00304F19F545"}'.");
  }

  private void ParseDBRelationKeyProperty(DBRelationEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in entityTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyParserData = keyValuePair.Value;
      if (propertyParserData.DBAttributeGuid == DBModelConfigurationParser.RelationIdAttributeGuid)
      {
        entityTypeData.KeyProperty = propertyParserData;
        break;
      }
    }
    if (entityTypeData.KeyProperty == null)
      throw new EntityTypeConfigurationException(11, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' не указано основной ключевое свойство, содержащее ID объекта. Укажите одно из свойств с помощью атрибута '{typeof (DBAttributeTypeAttribute)}' с значением '{"CAD00033-306C-11D8-B4E9-00304F19F545"}'.");
  }

  private void ParseDBRelationGuidProperty(DBRelationEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in entityTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyParserData = keyValuePair.Value;
      if (propertyParserData.DBAttributeGuid == DBModelConfigurationParser.RelationGuidAttributeGuid)
      {
        entityTypeData.GuidProperty = propertyParserData;
        break;
      }
    }
    if (entityTypeData.GuidProperty == null)
      throw new EntityTypeConfigurationException(11, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' не указано дополнительное ключевое свойство, содержащее GUID объекта. Укажите одно из свойств с помощью атрибута '{typeof (DBAttributeTypeAttribute)}' с значением '{"CAD00344-306C-11D8-B4E9-00304F19F545"}'.");
  }

  private void BeginParsingNavigationProperties()
  {
    foreach (DBEntityParserData allEntityType in this.allEntityTypes)
    {
      switch (allEntityType.EntityKind)
      {
        case DBEntityKind.Object:
          this.BeginParsingNavigationProperties((DBObjectEntityParserData) allEntityType);
          continue;
        case DBEntityKind.Relation:
          this.BeginParsingNavigationProperties((DBRelationEntityParserData) allEntityType);
          continue;
        default:
          throw new NotSupportedEnumException((Enum) allEntityType.EntityKind);
      }
    }
  }

  private void BeginParsingNavigationProperties(DBObjectEntityParserData entityTypeData)
  {
    foreach (ExtendedEntityPropertyInfo propertyInfo in CollectionUtils.ExtractAsList<ExtendedEntityPropertyInfo>((IList<ExtendedEntityPropertyInfo>) entityTypeData.MappablePropertiesParserData, new Predicate<ExtendedEntityPropertyInfo>(this.DBObjectNavigationPropertyPredicate)))
    {
      EntityPropertyDefinition definition = propertyInfo.Definition;
      DBRelationTypeAttribute annotationAttribute1 = propertyInfo.GetAnnotationAttribute<DBRelationTypeAttribute>(true);
      if (annotationAttribute1 != null)
      {
        DBObjectNavigationPropertyParserData propertyData = new DBObjectNavigationPropertyParserData(propertyInfo);
        propertyData.IsRelationStart = true;
        propertyData.DBRelationTypeGuid = annotationAttribute1.Guid;
        this.DetectRelationStartComplexity(propertyData);
        this.DetectRelationStartInverseEntityType(entityTypeData, propertyData);
        InversePropertyAttribute annotationAttribute2 = propertyInfo.GetAnnotationAttribute<InversePropertyAttribute>(true);
        if (annotationAttribute2 != null)
          propertyData.InverseTypePropertyName = annotationAttribute2.PropertyName;
        propertyData.IsCompleteDefinition = true;
        entityTypeData.NavigationPropertiesParserData.Add(propertyInfo.Name, propertyData);
      }
      else
      {
        DBObjectNavigationPropertyParserData propertyParserData = new DBObjectNavigationPropertyParserData(propertyInfo);
        entityTypeData.NavigationPropertiesParserData.Add(propertyInfo.Name, propertyParserData);
      }
    }
  }

  private bool DBObjectNavigationPropertyPredicate(ExtendedEntityPropertyInfo propertyInfo)
  {
    EntityPropertyDefinition definition = propertyInfo.Definition;
    return this.IsAllowedNavigationPropertyType(propertyInfo.BasicInfo.PropertyType, definition.ContainerItemType);
  }

  private void DetectRelationStartComplexity(DBObjectNavigationPropertyParserData propertyData)
  {
    Type containerItemType = propertyData.Definition.ContainerItemType;
    if (this.IsDBRelationType(containerItemType))
    {
      propertyData.IsComplex = true;
      propertyData.ChildOccurenceType = containerItemType;
    }
    else
    {
      propertyData.IsComplex = false;
      propertyData.ChildOccurenceType = (Type) null;
    }
  }

  private void DetectRelationStartInverseEntityType(
    DBObjectEntityParserData entityTypeData,
    DBObjectNavigationPropertyParserData propertyData)
  {
    if (propertyData.IsComplex)
    {
      Type entityType = (propertyData.ReflectionInfo.GetAnnotationAttribute<InverseEntityAttribute>(true) ?? throw new EntityTypeConfigurationException(3, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' на навигационном свойстве '{propertyData.Name}' отсутствует атрибут '{typeof (InverseEntityAttribute)}'. Этот атрибут необходим, так как используется объект-связка.")).EntityType;
      propertyData.InverseEntityType = this.IsDBObjectType(entityType) ? entityType : throw new EntityTypeConfigurationException(3, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' на навигационном свойстве '{propertyData.Name}' атрибут '{typeof (InverseEntityAttribute)}' должен указывать на тип доменного объекта.");
    }
    else
      propertyData.InverseEntityType = propertyData.Definition.ContainerItemType;
  }

  private void BeginParsingNavigationProperties(DBRelationEntityParserData entityTypeData)
  {
    List<ExtendedEntityPropertyInfo> asList = CollectionUtils.ExtractAsList<ExtendedEntityPropertyInfo>((IList<ExtendedEntityPropertyInfo>) entityTypeData.MappablePropertiesParserData, new Predicate<ExtendedEntityPropertyInfo>(this.DBRelationNavigationPropertyPredicate));
    this.ParseRelationStartProperty(entityTypeData, asList);
    this.ParseRelationEndProperty(entityTypeData, asList);
    if (asList.Count == 0)
      return;
    entityTypeData.MappablePropertiesParserData.AddRange((IEnumerable<ExtendedEntityPropertyInfo>) asList);
  }

  private bool DBRelationNavigationPropertyPredicate(ExtendedEntityPropertyInfo propertyInfo)
  {
    EntityPropertyDefinition definition = propertyInfo.Definition;
    return !definition.IsContainer && this.IsAllowedNavigationPropertyType(propertyInfo.BasicInfo.PropertyType, definition.ContainerItemType) && (propertyInfo.GetAnnotationAttribute<DBRelationStartAttribute>(true) != null || propertyInfo.GetAnnotationAttribute<DBRelationEndAttribute>(true) != null);
  }

  private void ParseRelationStartProperty(
    DBRelationEntityParserData entityTypeData,
    List<ExtendedEntityPropertyInfo> navigationProperties)
  {
    List<ExtendedEntityPropertyInfo> all = navigationProperties.FindAll((Predicate<ExtendedEntityPropertyInfo>) (item => item.GetAnnotationAttribute<DBRelationStartAttribute>(true) != null));
    if (all.Count == 0)
      throw new EntityTypeConfigurationException(2, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' отсутствует навигационное свойство, указывающее на начало связи и помеченное атрибутом '{typeof (DBRelationStartAttribute)}'.");
    ExtendedEntityPropertyInfo propertyInfo = all.Count == 1 ? all[0] : throw new EntityTypeConfigurationException(2, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' найдены навигационные свойства '{all[0].Name}' и '{all[1].Name}', указывающие на начало связи. Тип обязан содержать только одно такое свойство.");
    navigationProperties.Remove(propertyInfo);
    DBRelationNavigationPropertyParserData propertyParserData = new DBRelationNavigationPropertyParserData(propertyInfo);
    entityTypeData.NavigationPropertiesParserData.Add(propertyInfo.Name, propertyParserData);
    entityTypeData.RelationStartProperty = propertyParserData;
  }

  private void ParseRelationEndProperty(
    DBRelationEntityParserData entityTypeData,
    List<ExtendedEntityPropertyInfo> navigationProperties)
  {
    List<ExtendedEntityPropertyInfo> all = navigationProperties.FindAll((Predicate<ExtendedEntityPropertyInfo>) (item => item.GetAnnotationAttribute<DBRelationEndAttribute>(true) != null));
    if (all.Count == 0)
      throw new EntityTypeConfigurationException(2, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' отсутствует навигационное свойство, указывающее на окончание связи и помеченное атрибутом '{typeof (DBRelationEndAttribute)}'.");
    ExtendedEntityPropertyInfo propertyInfo = all.Count == 1 ? all[0] : throw new EntityTypeConfigurationException(2, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' найдены навигационные свойства '{all[0].Name}' и '{all[1].Name}', указывающие на окончание связи. Тип обязан содержать только одно такое свойство.");
    navigationProperties.Remove(propertyInfo);
    DBRelationNavigationPropertyParserData propertyParserData = new DBRelationNavigationPropertyParserData(propertyInfo);
    entityTypeData.NavigationPropertiesParserData.Add(propertyInfo.Name, propertyParserData);
    entityTypeData.RelationEndProperty = propertyParserData;
  }

  private void EndParsingNavigationProperties()
  {
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      this.EndParsingNavigationProperties(dbObjectType);
  }

  private void EndParsingNavigationProperties(DBObjectEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DBObjectNavigationPropertyParserData> keyValuePair in entityTypeData.NavigationPropertiesParserData)
    {
      DBObjectNavigationPropertyParserData propertyParserData1 = keyValuePair.Value;
      if (propertyParserData1.IsRelationStart && !string.IsNullOrEmpty(propertyParserData1.InverseTypePropertyName))
      {
        DBObjectNavigationPropertyParserData propertyParserData2 = this.GetDBObjectTypeData(propertyParserData1.InverseEntityType).NavigationPropertiesParserData[propertyParserData1.InverseTypePropertyName];
        propertyParserData2.DBRelationTypeGuid = propertyParserData1.DBRelationTypeGuid;
        propertyParserData2.IsComplex = propertyParserData1.IsComplex;
        propertyParserData2.ChildOccurenceType = propertyParserData1.ChildOccurenceType;
        propertyParserData2.InverseTypePropertyName = propertyParserData1.Name;
        propertyParserData2.InverseEntityType = entityTypeData.EntityType;
        propertyParserData2.IsCompleteDefinition = true;
      }
    }
  }

  private void CheckForIncompleteNavigationProperties()
  {
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
    {
      foreach (KeyValuePair<string, DBObjectNavigationPropertyParserData> keyValuePair in dbObjectType.NavigationPropertiesParserData)
      {
        DBObjectNavigationPropertyParserData propertyParserData = keyValuePair.Value;
        if (!propertyParserData.IsCompleteDefinition)
          throw new EntityTypeConfigurationException(13, dbObjectType.EntityType, $"У типа доменных объектов '{dbObjectType.EntityType}' некорректно определено навигационное свойство '{propertyParserData.Name}'. Либо отсутствует атрибут '{typeof (DBRelationTypeAttribute)}', если это начало связи, либо отсутствует атрибут '{typeof (InversePropertyAttribute)}' у парного свойства, если это окончание связи.");
      }
    }
  }

  private void CheckForIncompatibleNavigationPropertyTypes()
  {
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      this.CheckForIncompatibleNavigationPropertyTypes(dbObjectType);
  }

  private void CheckForIncompatibleNavigationPropertyTypes(DBObjectEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DBObjectNavigationPropertyParserData> keyValuePair in entityTypeData.NavigationPropertiesParserData)
    {
      DBObjectNavigationPropertyParserData propertyParserData = keyValuePair.Value;
      if (propertyParserData.IsComplex)
      {
        Type childOccurenceType = propertyParserData.ChildOccurenceType;
        DBRelationEntityParserData relationTypeData = this.GetDBRelationTypeData(childOccurenceType);
        if (propertyParserData.IsRelationStart)
        {
          if (!relationTypeData.RelationStartProperty.Definition.PropertyType.IsAssignableFrom(entityTypeData.EntityType))
            throw new EntityTypeConfigurationException(15, childOccurenceType, $"У типа доменных объектов '{childOccurenceType}' навигационное свойство '{relationTypeData.RelationStartProperty.Name}' должно иметь тип, совместимый с '{entityTypeData.EntityType}'.");
        }
        else
        {
          if (!propertyParserData.Definition.ContainerItemType.IsAssignableFrom(propertyParserData.InverseEntityType))
            throw new EntityTypeConfigurationException(15, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' навигационное свойство '{propertyParserData.Name}' должно иметь тип, совместимый с '{propertyParserData.InverseEntityType}'.");
          if (!relationTypeData.RelationEndProperty.Definition.PropertyType.IsAssignableFrom(entityTypeData.EntityType))
            throw new EntityTypeConfigurationException(15, childOccurenceType, $"У типа доменных объектов '{childOccurenceType}' навигационное свойство '{relationTypeData.RelationEndProperty.Name}' должно иметь тип, совместимый с '{entityTypeData.EntityType}'.");
        }
      }
      else if (!propertyParserData.IsRelationStart && !propertyParserData.Definition.ContainerItemType.IsAssignableFrom(propertyParserData.InverseEntityType))
        throw new EntityTypeConfigurationException(15, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' навигационное свойство '{propertyParserData.Name}' должно иметь тип, совместимый с '{propertyParserData.InverseEntityType}'.");
    }
  }

  private void CheckForInvalidProperties()
  {
    foreach (DBEntityParserData allEntityType in this.allEntityTypes)
    {
      if (allEntityType.MappablePropertiesParserData.Count != 0)
      {
        ExtendedEntityPropertyInfo entityPropertyInfo = allEntityType.MappablePropertiesParserData[0];
        throw new EntityTypeConfigurationException(3, allEntityType.EntityType, $"У типа доменных объектов '{allEntityType.EntityType}' найдено некорректное свойство '{entityPropertyInfo.Name}'. Невозможно определить способ отображения свойства в базу данных IPS.");
      }
    }
  }

  private void CheckForDuplicateDBObjectTypeMappings()
  {
    DuplicateDBObjectTypeMappingCheck typeMappingCheck = new DuplicateDBObjectTypeMappingCheck();
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      typeMappingCheck.AddDBObjectType(dbObjectType.EntityType, dbObjectType.DBObjectTypeGuid);
    typeMappingCheck.Perform();
  }

  private void MapToDatabase()
  {
    foreach (DBEntityParserData allEntityType in this.allEntityTypes)
      this.CreateDataPropertyDescriptors(allEntityType);
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      this.CreateNavigationPropertyDescriptors(dbObjectType);
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      this.MapDBObjectEntityType(dbObjectType);
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      this.BeginMappingNavigationProperties(dbObjectType);
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
      this.EndMappingNavigationProperties(dbObjectType);
  }

  private void CreateDataPropertyDescriptors(DBEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in entityTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyParserData = keyValuePair.Value;
      propertyParserData.Descriptor = new DataPropertyDescriptor(propertyParserData.ReflectionInfo.Definition, propertyParserData.ReflectionInfo.BasicInfo);
      entityTypeData.DataPropertiesDescriptors.Add(propertyParserData.Name, propertyParserData.Descriptor);
    }
  }

  private void CreateNavigationPropertyDescriptors(DBObjectEntityParserData entityTypeData)
  {
    foreach (KeyValuePair<string, DBObjectNavigationPropertyParserData> keyValuePair in entityTypeData.NavigationPropertiesParserData)
    {
      DBObjectNavigationPropertyParserData propertyParserData = keyValuePair.Value;
      NavigationPropertyDescriptor propertyDescriptor = new NavigationPropertyDescriptor(propertyParserData.Definition, propertyParserData.ReflectionInfo.BasicInfo);
      entityTypeData.NavigationPropertiesDescriptors.Add(propertyParserData.Name, propertyDescriptor);
    }
  }

  private void MapDBObjectEntityType(DBObjectEntityParserData entityTypeData)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(entityTypeData.DBObjectTypeGuid, true);
      ArrayList objsTreeList = new ArrayList();
      objectType.FillChildrenList(objsTreeList);
      bool flag = objsTreeList.Count == 1 && (int) objsTreeList[0] == objectType.ObjectType;
      entityTypeData.DBObjectType = new DBObjectTypeMapping(entityTypeData.DBObjectTypeGuid);
      entityTypeData.DBObjectType.Id = objectType.ObjectType;
      entityTypeData.DBObjectType.Name = objectType.ObjectTypeName;
      entityTypeData.DBObjectType.IsLocalType = objectType.IsLocalType;
      entityTypeData.DBObjectType.IsLeafType = flag;
      this.MapDBObjectDataProperties(entityTypeData, sessionKeeper.Session, objectType);
    }
  }

  private void MapDBObjectDataProperties(
    DBObjectEntityParserData entityTypeData,
    IUserSession session,
    IDBObjectType dbObjectType)
  {
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in entityTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyData = keyValuePair.Value;
      propertyData.LanguageInfo = this.CreateDataPropertyLanguageInfo(propertyData.ReflectionInfo);
      IDBAttributeType attributeType = session.GetAttributeType(propertyData.DBAttributeGuid, true);
      Type propertyValueType = this.GetPropertyValueType(propertyData.ReflectionInfo, propertyData.LanguageInfo);
      Type dbValueType = this.GetDBValueType(propertyData, session, attributeType);
      if (propertyValueType != dbValueType)
        throw new EntityTypeConfigurationException(10, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' свойство '{propertyData.Name}' имеет тип значения '{propertyData.ReflectionInfo.Definition.PropertyType}', не совпадающий с типом соответствующиего атрибута IPS. Правильный тип свойства должен быть '{dbValueType}'.");
      bool isContent;
      bool flag1;
      bool flag2;
      bool flag3;
      bool flag4;
      if (attributeType.AttributeType == FieldTypes.ftSystem)
      {
        int options = (int) attributeType.Options;
        isContent = attributeType.IsContent;
        flag1 = (options & 64 /*0x40*/) == 0;
        flag2 = false;
        flag3 = false;
        flag4 = (options & 8) == 0;
      }
      else
      {
        Attribute4ObjectTypeProperties propertiesStructure = ((IDBAttributeType4Object) dbObjectType.Attributes.GetAttributeByID(attributeType.AttributeID, true)).Attribute4ObjectPropertiesStructure;
        AttributeOptions options = propertiesStructure.Options;
        isContent = propertiesStructure.IsContent;
        flag1 = (options & AttributeOptions.ModifyInBase) == AttributeOptions.None;
        flag2 = propertiesStructure.RequiredMode == RequiredModes.Manual;
        flag3 = propertiesStructure.RequiredMode != RequiredModes.AutoRequired;
        flag4 = (options & AttributeOptions.DisableNulls) == AttributeOptions.None;
      }
      DataPropertyMapping propertyMapping = new DataPropertyMapping(propertyData.Descriptor, propertyData.LanguageInfo, propertyData.DBAttributeGuid)
      {
        Id = attributeType.AttributeID,
        Name = attributeType.Name,
        DBFieldType = this.GetDBFieldType(attributeType),
        IsContent = isContent,
        IsCheckoutRequired = flag1,
        IsManuallyCreated = flag2,
        IsDeletable = flag3,
        AllowDBNull = flag4
      };
      propertyMapping.ValueLoadParameters = this.CreateValueLoadParameters(propertyMapping, propertyValueType);
      propertyMapping.ValueSaveParameters = this.CreateValueSaveParameters(propertyMapping);
      entityTypeData.DataPropertiesMappings.Add(propertyData.Name, propertyMapping);
    }
  }

  private DataPropertyLanguageInfo CreateDataPropertyLanguageInfo(
    ExtendedEntityPropertyInfo propertyInfo)
  {
    Type propertyType = propertyInfo.Definition.PropertyType;
    if (this.IsNullableValueType(propertyType))
      return new DataPropertyLanguageInfo(true, false, (object) null);
    if (!propertyType.IsClass)
      return new DataPropertyLanguageInfo(false, false, (object) null);
    bool hasEmptyValue = false;
    object emptyValue = (object) null;
    if (propertyType == typeof (string))
    {
      hasEmptyValue = true;
      emptyValue = (object) string.Empty;
    }
    else if (propertyType == typeof (DBFileValue))
    {
      hasEmptyValue = true;
      emptyValue = (object) DBFileValue.Empty;
    }
    return new DataPropertyLanguageInfo(true, hasEmptyValue, emptyValue);
  }

  private Type GetPropertyValueType(
    ExtendedEntityPropertyInfo propertyInfo,
    DataPropertyLanguageInfo languageInfo)
  {
    Type propertyType = propertyInfo.Definition.PropertyType;
    return languageInfo.IsNullable && this.IsNullableValueType(propertyType) ? propertyType.GetGenericArguments()[0] : propertyType;
  }

  private bool IsNullableValueType(Type propertyType)
  {
    return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof (Nullable<>);
  }

  private FieldTypes GetDBFieldType(IDBAttributeType dbAttrType)
  {
    return dbAttrType.AttributeType == FieldTypes.ftSystem ? ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) dbAttrType.AttributeID) : dbAttrType.AttributeType;
  }

  private Type GetDBValueType(
    DataPropertyParserData propertyData,
    IUserSession session,
    IDBAttributeType dbAttributeType)
  {
    if (dbAttributeType.AttributeID == -7)
      return typeof (int);
    if (dbAttributeType.AttributeID == -23)
      return typeof (int);
    if (dbAttributeType.AttributeType == FieldTypes.ftFile)
      return typeof (DBFileValue);
    Type dataType = DBAttributeHelper.TryGetDataType(dbAttributeType);
    return dataType != (Type) null ? dataType : throw new ModelMappingException($"У типа доменных объектов '{propertyData.ReflectionInfo.BasicInfo.DeclaringType}' свойство '{propertyData.Name}' отображено в атрибут IPS '{dbAttributeType.Name}' (GUID = {propertyData.DBAttributeGuid:B}), для которого не удалось определить тип значений в базе данных.");
  }

  private DataPropertyLoadParameters CreateValueLoadParameters(
    DataPropertyMapping propertyMapping,
    Type propertyValueType)
  {
    DataPropertyLoadParameters valueLoadParameters = new DataPropertyLoadParameters();
    if (propertyMapping.PropertyDescriptor.Definition.PropertyType == typeof (string))
    {
      if (propertyMapping.CanBeDBNull)
      {
        valueLoadParameters.DBNullLoadMode = DBNullLoadMode.EmptyValue;
        valueLoadParameters.DBNullEquivalent = propertyMapping.LanguageInfo.EmptyValue;
      }
      valueLoadParameters.MeaningfulValueType = propertyValueType;
    }
    else if (propertyMapping.IsFileOrBlob)
    {
      if (propertyMapping.CanBeDBNull)
      {
        valueLoadParameters.DBNullLoadMode = DBNullLoadMode.EmptyValue;
        valueLoadParameters.DBNullEquivalent = propertyMapping.LanguageInfo.EmptyValue;
      }
      valueLoadParameters.MeaningfulValueType = propertyValueType;
    }
    else if (propertyMapping.LanguageInfo.IsNullable)
    {
      if (propertyMapping.CanBeDBNull)
      {
        valueLoadParameters.DBNullLoadMode = DBNullLoadMode.NullValue;
        valueLoadParameters.DBNullEquivalent = (object) null;
      }
      valueLoadParameters.MeaningfulValueType = propertyValueType;
    }
    else
    {
      if (propertyMapping.CanBeDBNull)
      {
        valueLoadParameters.DBNullLoadMode = DBNullLoadMode.DefaultValue;
        valueLoadParameters.DBNullEquivalent = this.dataPropertyHelper.GetDefaultValueForValueType(propertyValueType);
      }
      valueLoadParameters.MeaningfulValueType = propertyValueType;
    }
    if (propertyMapping.IsObligatory)
      valueLoadParameters.KeyEntityLoadMode = GetAttributeValuesModes.IncludeObligatoryAttributes;
    else if (propertyMapping.IsFileOrBlob)
      valueLoadParameters.KeyEntityLoadMode = GetAttributeValuesModes.IncludeBlobs | GetAttributeValuesModes.BlobIdentifier;
    if (propertyMapping.IsFileOrBlob)
      valueLoadParameters.BatchLoadMode = ColumnContents.ID;
    valueLoadParameters.Freeze();
    return valueLoadParameters;
  }

  private DataPropertySaveParameters CreateValueSaveParameters(DataPropertyMapping propertyMapping)
  {
    bool ignoreNullValueOnCreate = propertyMapping.CanBeDBNull && (propertyMapping.AllowDBNull || propertyMapping.IsManuallyCreated);
    bool removeNullValueOnUpdate = propertyMapping.CanBeDBNull && !propertyMapping.AllowDBNull;
    if (propertyMapping.PropertyDescriptor.Definition.PropertyType == typeof (string))
      return new DataPropertySaveParameters(propertyMapping.CanBeDBNull ? DBNullSaveMode.DBNull : DBNullSaveMode.NotSupported, ignoreNullValueOnCreate, removeNullValueOnUpdate);
    if (propertyMapping.IsFileOrBlob)
      return new DataPropertySaveParameters(propertyMapping.CanBeDBNull ? DBNullSaveMode.DBNull : DBNullSaveMode.NotSupported, ignoreNullValueOnCreate, removeNullValueOnUpdate);
    return propertyMapping.LanguageInfo.IsNullable ? new DataPropertySaveParameters(propertyMapping.CanBeDBNull ? DBNullSaveMode.DBNull : DBNullSaveMode.NotSupported, ignoreNullValueOnCreate, removeNullValueOnUpdate) : new DataPropertySaveParameters(DBNullSaveMode.NotApplicable, false, false);
  }

  private void BeginMappingNavigationProperties(DBObjectEntityParserData entityTypeData)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (KeyValuePair<string, DBObjectNavigationPropertyParserData> keyValuePair in entityTypeData.NavigationPropertiesParserData)
      {
        DBObjectNavigationPropertyParserData navigationPropertyData = keyValuePair.Value;
        if (navigationPropertyData.IsRelationStart)
        {
          NavigationPropertyDescriptor propertiesDescriptor = entityTypeData.NavigationPropertiesDescriptors[navigationPropertyData.Name];
          IDBRelationType relationType = sessionKeeper.Session.GetRelationType(navigationPropertyData.DBRelationTypeGuid, true);
          DBObjectNavigationPropertyMapping navigationPropertyMapping = new DBObjectNavigationPropertyMapping(propertiesDescriptor)
          {
            IsRelationStart = navigationPropertyData.IsRelationStart,
            IsComplex = navigationPropertyData.IsComplex,
            DBRelationType = new DBRelationTypeMapping(navigationPropertyData.DBRelationTypeGuid)
          };
          navigationPropertyMapping.DBRelationType.Id = relationType.RelationType;
          navigationPropertyMapping.DBRelationType.Name = relationType.Description;
          navigationPropertyMapping.DBRelationAttributes = navigationPropertyMapping.IsComplex ? new DataPropertyMappings(navigationPropertyData.ChildOccurenceType, (IDictionary<string, DataPropertyMapping>) this.MapChildOccurenceDataProperties(navigationPropertyData, sessionKeeper.Session, relationType)) : new DataPropertyMappings(typeof (SimpleOccurence), DBModelConfigurationParser.emptyAttributeMappings);
          navigationPropertyMapping.DBRelationApplicabilities = new DBRelationApplicabilityMappings((IDictionary<Tuple<int, int>, DBRelationApplicabilityMapping>) this.GetNavigationPropertyApplicabilities(entityTypeData, navigationPropertyData, navigationPropertyMapping));
          if (navigationPropertyMapping.DBRelationApplicabilities.Count == 0)
            throw new EntityTypeConfigurationException(3, entityTypeData.EntityType, $"У типа доменных объектов '{entityTypeData.EntityType}' навигационное свойство '{navigationPropertyData.Name}' не является корректным. В базе данных такие связи между объектами недопустимы.");
          entityTypeData.NavigationPropertiesMappings.Add(navigationPropertyData.Name, navigationPropertyMapping);
        }
      }
    }
  }

  private void EndMappingNavigationProperties(DBObjectEntityParserData entityTypeData)
  {
    using (new SessionKeeper())
    {
      foreach (KeyValuePair<string, DBObjectNavigationPropertyParserData> keyValuePair in entityTypeData.NavigationPropertiesParserData)
      {
        DBObjectNavigationPropertyParserData propertyParserData1 = keyValuePair.Value;
        if (propertyParserData1.IsRelationStart && !string.IsNullOrEmpty(propertyParserData1.InverseTypePropertyName))
        {
          DBObjectNavigationPropertyMapping propertiesMapping = entityTypeData.NavigationPropertiesMappings[propertyParserData1.Name];
          DBObjectEntityParserData dbObjectTypeData = this.GetDBObjectTypeData(propertyParserData1.InverseEntityType);
          DBObjectNavigationPropertyParserData propertyParserData2 = dbObjectTypeData.NavigationPropertiesParserData[propertyParserData1.InverseTypePropertyName];
          dbObjectTypeData.NavigationPropertiesMappings.Add(propertyParserData2.Name, new DBObjectNavigationPropertyMapping(dbObjectTypeData.NavigationPropertiesDescriptors[propertyParserData2.Name])
          {
            IsRelationStart = false,
            IsComplex = propertyParserData1.IsComplex,
            DBRelationType = propertiesMapping.DBRelationType,
            DBRelationAttributes = propertiesMapping.DBRelationAttributes,
            DBRelationApplicabilities = propertiesMapping.DBRelationApplicabilities
          });
        }
      }
    }
  }

  private Dictionary<Tuple<int, int>, DBRelationApplicabilityMapping> GetNavigationPropertyApplicabilities(
    DBObjectEntityParserData parentDBObjectData,
    DBObjectNavigationPropertyParserData navigationPropertyData,
    DBObjectNavigationPropertyMapping navigationPropertyMapping)
  {
    Dictionary<Tuple<int, int>, DBRelationApplicabilityMapping> propertyApplicabilities = new Dictionary<Tuple<int, int>, DBRelationApplicabilityMapping>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType1 = sessionKeeper.Session.GetObjectType(parentDBObjectData.DBObjectType.Id, true);
      ArrayList arrayList1 = new ArrayList();
      ArrayList objsTreeList1 = arrayList1;
      objectType1.FillChildrenList(objsTreeList1);
      IDBObjectType objectType2 = sessionKeeper.Session.GetObjectType(this.GetDBObjectTypeData(navigationPropertyData.InverseEntityType).DBObjectType.Id, true);
      ArrayList arrayList2 = new ArrayList();
      ArrayList objsTreeList2 = arrayList2;
      objectType2.FillChildrenList(objsTreeList2);
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      foreach (int inObjectType in arrayList1)
      {
        foreach (int objectType3 in arrayList2)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(navigationPropertyMapping.DBRelationType.Id, objectType3, inObjectType);
          if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
          {
            DBRelationApplicabilityMapping applicabilityMapping = new DBRelationApplicabilityMapping();
            applicabilityMapping.ParentObjectTypeId = inObjectType;
            applicabilityMapping.ChildObjectTypeId = objectType3;
            applicabilityMapping.IsContent = applicability.IsContent;
            applicabilityMapping.Freeze();
            propertyApplicabilities.Add(Tuple.Create<int, int>(inObjectType, objectType3), applicabilityMapping);
          }
        }
      }
    }
    return propertyApplicabilities;
  }

  private Dictionary<string, DataPropertyMapping> MapChildOccurenceDataProperties(
    DBObjectNavigationPropertyParserData navigationPropertyData,
    IUserSession session,
    IDBRelationType dbRelationType)
  {
    Type childOccurenceType = navigationPropertyData.ChildOccurenceType;
    DBRelationEntityParserData relationTypeData = this.GetDBRelationTypeData(childOccurenceType);
    Dictionary<string, DataPropertyMapping> dictionary = new Dictionary<string, DataPropertyMapping>(relationTypeData.DataPropertiesParserData.Count);
    foreach (KeyValuePair<string, DataPropertyParserData> keyValuePair in relationTypeData.DataPropertiesParserData)
    {
      DataPropertyParserData propertyData = keyValuePair.Value;
      propertyData.LanguageInfo = this.CreateDataPropertyLanguageInfo(propertyData.ReflectionInfo);
      IDBAttributeType attributeType = session.GetAttributeType(propertyData.DBAttributeGuid, true);
      Type propertyValueType = this.GetPropertyValueType(propertyData.ReflectionInfo, propertyData.LanguageInfo);
      Type dbValueType = this.GetDBValueType(propertyData, session, attributeType);
      if (propertyValueType != dbValueType)
        throw new EntityTypeConfigurationException(10, childOccurenceType, $"У типа доменных объектов '{childOccurenceType}' свойство '{propertyData.Name}' имеет тип значения '{propertyData.ReflectionInfo.Definition.PropertyType}', не соответствующий типу атрибута IPS.");
      bool isContent;
      bool flag1;
      bool flag2;
      bool flag3;
      bool flag4;
      if (attributeType.AttributeType == FieldTypes.ftSystem)
      {
        int options = (int) attributeType.Options;
        isContent = attributeType.IsContent;
        flag1 = (options & 64 /*0x40*/) == 0;
        flag2 = false;
        flag3 = false;
        flag4 = (options & 8) == 0;
      }
      else
      {
        Attribute4RelationTypeProperties propertiesStructure = ((IDBAttributeType4Relation) dbRelationType.Attributes.GetAttributeByID(attributeType.AttributeID, true)).Attribute4RelationPropertiesStructure;
        AttributeOptions options = propertiesStructure.Options;
        isContent = propertiesStructure.IsContent;
        flag1 = (options & AttributeOptions.ModifyInBase) == AttributeOptions.None;
        flag2 = propertiesStructure.RequiredMode == RequiredModes.Manual;
        flag3 = propertiesStructure.RequiredMode != RequiredModes.AutoRequired;
        flag4 = (options & AttributeOptions.DisableNulls) == AttributeOptions.None;
      }
      DataPropertyMapping propertyMapping = new DataPropertyMapping(propertyData.Descriptor, propertyData.LanguageInfo, propertyData.DBAttributeGuid)
      {
        Id = attributeType.AttributeID,
        Name = attributeType.Name,
        DBFieldType = this.GetDBFieldType(attributeType),
        IsContent = isContent,
        IsCheckoutRequired = flag1,
        IsManuallyCreated = flag2,
        IsDeletable = flag3,
        AllowDBNull = flag4
      };
      propertyMapping.ValueLoadParameters = this.CreateValueLoadParameters(propertyMapping, propertyValueType);
      propertyMapping.ValueSaveParameters = this.CreateValueSaveParameters(propertyMapping);
      dictionary.Add(propertyData.Name, propertyMapping);
    }
    return dictionary;
  }

  private DBModelConfigurationBuilderResult CreateTypeDescriptors()
  {
    DBModelConfigurationBuilderResult typeDescriptors = new DBModelConfigurationBuilderResult(this.dbObjectTypes.Count + this.dbRelationTypes.Count);
    foreach (DBObjectEntityParserData dbObjectType in this.dbObjectTypes)
    {
      DBObjectEntityTypeDescriptor entityTypeDescriptor = new DBObjectEntityTypeDescriptor(dbObjectType.EntityType)
      {
        DBObjectType = dbObjectType.DBObjectType,
        DataProperties = new DataPropertyDescriptors(dbObjectType.EntityType, (IDictionary<string, DataPropertyDescriptor>) dbObjectType.DataPropertiesDescriptors),
        DataPropertiesMappings = new DataPropertyMappings(dbObjectType.EntityType, (IDictionary<string, DataPropertyMapping>) dbObjectType.DataPropertiesMappings)
      };
      entityTypeDescriptor.KeyProperty = entityTypeDescriptor.DataProperties.AsDictionary[dbObjectType.KeyProperty.Name];
      entityTypeDescriptor.NavigationProperties = new NavigationPropertyDescriptors(dbObjectType.EntityType, (IDictionary<string, NavigationPropertyDescriptor>) dbObjectType.NavigationPropertiesDescriptors);
      entityTypeDescriptor.NavigationPropertiesMappings = new DBObjectNavigationPropertyMappings(dbObjectType.EntityType, (IDictionary<string, DBObjectNavigationPropertyMapping>) dbObjectType.NavigationPropertiesMappings);
      entityTypeDescriptor.Initialize();
      typeDescriptors.InternalDescriptors.Add((DBEntityTypeDescriptor) entityTypeDescriptor);
      EntityChangeTrackerDescriptor trackerDescriptor = new EntityChangeTrackerDescriptor(dbObjectType.EntityType);
      trackerDescriptor.DataProperties = entityTypeDescriptor.DataProperties.AsDictionary;
      trackerDescriptor.NavigationProperties = entityTypeDescriptor.NavigationProperties.AsDictionary;
      trackerDescriptor.Initialize();
      typeDescriptors.ChangeTrackerDescriptors.Add(trackerDescriptor);
    }
    foreach (DBRelationEntityParserData dbRelationType in this.dbRelationTypes)
    {
      DBRelationEntityTypeDescriptor entityTypeDescriptor = new DBRelationEntityTypeDescriptor(dbRelationType.EntityType);
      entityTypeDescriptor.DataProperties = new DataPropertyDescriptors(dbRelationType.EntityType, (IDictionary<string, DataPropertyDescriptor>) dbRelationType.DataPropertiesDescriptors);
      entityTypeDescriptor.KeyProperty = new DataPropertyDescriptor(dbRelationType.KeyProperty.ReflectionInfo.Definition, dbRelationType.KeyProperty.ReflectionInfo.BasicInfo);
      entityTypeDescriptor.GuidProperty = new DataPropertyDescriptor(dbRelationType.GuidProperty.ReflectionInfo.Definition, dbRelationType.GuidProperty.ReflectionInfo.BasicInfo);
      entityTypeDescriptor.RelationStartProperty = new NavigationPropertyDescriptor(dbRelationType.RelationStartProperty.Definition, dbRelationType.RelationStartProperty.ReflectionInfo.BasicInfo);
      entityTypeDescriptor.RelationEndProperty = new NavigationPropertyDescriptor(dbRelationType.RelationEndProperty.Definition, dbRelationType.RelationEndProperty.ReflectionInfo.BasicInfo);
      entityTypeDescriptor.Initialize();
      typeDescriptors.InternalDescriptors.Add((DBEntityTypeDescriptor) entityTypeDescriptor);
      EntityChangeTrackerDescriptor trackerDescriptor = new EntityChangeTrackerDescriptor(dbRelationType.EntityType);
      trackerDescriptor.DataProperties = entityTypeDescriptor.DataProperties.AsDictionary;
      trackerDescriptor.NavigationProperties = this.FromSingleNavigationProperty(dbRelationType.EntityType, entityTypeDescriptor.RelationEndProperty).AsDictionary;
      trackerDescriptor.Initialize();
      typeDescriptors.ChangeTrackerDescriptors.Add(trackerDescriptor);
    }
    return typeDescriptors;
  }

  private NavigationPropertyDescriptors FromSingleNavigationProperty(
    Type entityType,
    NavigationPropertyDescriptor propertyDescriptor)
  {
    return new NavigationPropertyDescriptors(entityType, (IDictionary<string, NavigationPropertyDescriptor>) new Dictionary<string, NavigationPropertyDescriptor>()
    {
      {
        propertyDescriptor.Definition.Name,
        propertyDescriptor
      }
    });
  }

  private bool IsAllowedNavigationPropertyType(Type propertyType, Type itemType)
  {
    if (propertyType == (Type) null)
      throw new ArgumentNullException(nameof (propertyType));
    if (itemType == (Type) null)
      throw new ArgumentNullException(nameof (itemType));
    return this.IsDBObjectType(itemType) || this.IsDBRelationType(itemType);
  }

  private bool IsDBObjectType(Type entityType)
  {
    if (entityType == (Type) null)
      throw new ArgumentNullException(nameof (entityType));
    return this.dbObjectTypes.Exists((Predicate<DBObjectEntityParserData>) (item => item.EntityType == entityType));
  }

  private bool IsDBRelationType(Type entityType)
  {
    if (entityType == (Type) null)
      throw new ArgumentNullException(nameof (entityType));
    return this.dbRelationTypes.Exists((Predicate<DBRelationEntityParserData>) (item => item.EntityType == entityType));
  }

  private DBObjectEntityParserData GetDBObjectTypeData(Type entityType)
  {
    return this.dbObjectTypes.Find((Predicate<DBObjectEntityParserData>) (item => item.EntityType == entityType)) ?? throw new InvalidOperationException();
  }

  private DBRelationEntityParserData GetDBRelationTypeData(Type childOccurenceType)
  {
    return this.dbRelationTypes.Find((Predicate<DBRelationEntityParserData>) (item => item.EntityType == childOccurenceType)) ?? throw new InvalidOperationException();
  }
}
