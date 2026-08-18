// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RuntimeSearchScheme
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Схема раскрытия состава, генерируется в рантайме (чтобы сервис ICompositionService не использовал схемы поиска)
/// </summary>
[Serializable]
public sealed class RuntimeSearchScheme
{
  /// <summary>Направление поиска</summary>
  public SearchDirection Direction;
  /// <summary>
  /// Идентификатор выборки, по которой выполняется дополнительная фильтрация результатов раскрутки состава/входимости
  /// </summary>
  public long Selection;
  /// <summary>
  /// Список типов объектов, по которым раскручивается состав/применяемость
  /// </summary>
  public List<int> ObjectTypes = new List<int>(0);
  /// <summary>
  /// Список типов связей, по которым раскручивается состав/применяемость
  /// </summary>
  public List<int> RelationTypes = new List<int>(0);
  /// <summary>
  /// Список атрибутов из различных источников, которые участвуют в раскрутке состава/применяемости
  /// </summary>
  public List<AttributeSource> Attributes = new List<AttributeSource>(0);
  /// <summary>
  /// Список объектов, которые не участвуют в раскрутке состава/применяемости
  /// </summary>
  public List<long> ExcludedObjects = new List<long>(0);
  /// <summary>
  /// Список связей, которые не участвуют в раскрутке состава/применяемости
  /// </summary>
  public List<long> ExcludedRelations = new List<long>(0);
  /// <summary>Опции поиска</summary>
  public SearchOptions Options;

  /// <summary>
  /// Создать экземпляр объекта для раскрутки состава/применяемости
  /// </summary>
  /// <param name="ADirection">Направление поиска</param>
  /// <param name="ASelection">Идентификатор выборки, по которой выполняется дополнительная фильтрация результатов раскрутки состава/входимости</param>
  /// <param name="AnObjectTypes">Массив идентификаторов типов объектов, по которым раскручивается состав/применяемост</param>
  /// <param name="ARelationTypes">Массив идентификаторов типов связей, по которым раскручивается состав/применяемост</param>
  /// <param name="AnAttributes">Список атрибутов из различных источников, по которым раскручивается состав/применяемост</param>
  public RuntimeSearchScheme(
    SearchDirection ADirection,
    long ASelection,
    int[] AnObjectTypes,
    int[] ARelationTypes,
    AttributeSource[] AnAttributes)
    : this(ADirection, ASelection, AnObjectTypes, ARelationTypes, AnAttributes, SearchOptions.None)
  {
  }

  /// <summary>
  /// Создать экземпляр объекта для раскрутки состава/применяемости
  /// </summary>
  /// <param name="ADirection">Направление поиска</param>
  /// <param name="ASelection">Идентификатор выборки, по которой выполняется дополнительная фильтрация результатов раскрутки состава/входимости</param>
  /// <param name="AnObjectTypes">Массив идентификаторов типов объектов, по которым раскручивается состав/применяемост</param>
  /// <param name="ARelationTypes">Массив идентификаторов типов связей, по которым раскручивается состав/применяемост</param>
  /// <param name="AnAttributes">Список атрибутов из различных источников, по которым раскручивается состав/применяемост</param>
  /// <param name="options">Опции поиска</param>
  public RuntimeSearchScheme(
    SearchDirection ADirection,
    long ASelection,
    int[] AnObjectTypes,
    int[] ARelationTypes,
    AttributeSource[] AnAttributes,
    SearchOptions options)
  {
    this.Direction = ADirection;
    this.Selection = ASelection;
    if (AnObjectTypes != null)
    {
      for (int index = 0; index < AnObjectTypes.Length; ++index)
      {
        if (!this.ObjectTypes.Contains(AnObjectTypes[index]))
          this.ObjectTypes.Add(AnObjectTypes[index]);
      }
    }
    if (ARelationTypes != null)
    {
      for (int index = 0; index < ARelationTypes.Length; ++index)
      {
        if (!this.RelationTypes.Contains(ARelationTypes[index]))
          this.RelationTypes.Add(ARelationTypes[index]);
      }
    }
    if (AnAttributes != null)
    {
      for (int index = 0; index < AnAttributes.Length; ++index)
      {
        if (!this.Attributes.Contains(AnAttributes[index]))
          this.Attributes.Add(AnAttributes[index]);
      }
    }
    this.Options = options;
  }

  /// <summary>
  /// Создать экземпляр объекта для раскрутки состава/применяемости
  /// </summary>
  /// <param name="ADirection">Направление поиска</param>
  /// <param name="ASelection">Идентификатор выборки, по которой выполняется дополнительная фильтрация результатов раскрутки состава/входимости</param>
  /// <param name="AnObjectTypes">Массив идентификаторов типов объектов, по которым раскручивается состав/применяемость</param>
  /// <param name="ARelationTypes">Массив идентификаторов типов связей, по которым раскручивается состав/применяемость</param>
  /// <param name="AnAttributes">Список атрибутов из различных источников, по которым раскручивается состав/применяемость</param>
  /// <param name="AnExcludedObjects">Список объектов, которые не участвуют в раскрутке состава/применяемости</param>
  /// <param name="AnExcludedRelations">Список связей, которые не участвуют в раскрутке состава/применяемости</param>
  public RuntimeSearchScheme(
    SearchDirection ADirection,
    long ASelection,
    int[] AnObjectTypes,
    int[] ARelationTypes,
    AttributeSource[] AnAttributes,
    long[] AnExcludedObjects,
    long[] AnExcludedRelations)
    : this(ADirection, ASelection, AnObjectTypes, ARelationTypes, AnAttributes)
  {
    if (AnExcludedObjects != null)
    {
      for (int index = 0; index < AnExcludedObjects.Length; ++index)
      {
        if (!this.ExcludedObjects.Contains(AnExcludedObjects[index]))
          this.ExcludedObjects.Add(AnExcludedObjects[index]);
      }
    }
    if (AnExcludedRelations == null)
      return;
    for (int index = 0; index < AnExcludedRelations.Length; ++index)
    {
      if (!this.ExcludedRelations.Contains(AnExcludedRelations[index]))
        this.ExcludedRelations.Add(AnExcludedRelations[index]);
    }
  }

  /// <summary>Отыскать в схеме атрибут по его ID</summary>
  /// <param name="AttrID">ID атрибута</param>
  /// <returns>Описание найденного атрибута или null</returns>
  public AttributeSource FindAttrByID(int AttrID)
  {
    for (int index = 0; index < this.Attributes.Count; ++index)
    {
      if (this.Attributes[index].ID == AttrID)
        return this.Attributes[index];
    }
    return (AttributeSource) null;
  }

  /// <summary>Отыскать в схеме атрибут по его Guid</summary>
  /// <param name="AttrGUID">Guid атрибута</param>
  /// <returns>Описание найденного атрибута или null</returns>
  public AttributeSource FindAttrByGUID(Guid AttrGUID)
  {
    for (int index = 0; index < this.Attributes.Count; ++index)
    {
      if (this.Attributes[index].GUID == AttrGUID)
        return this.Attributes[index];
    }
    return (AttributeSource) null;
  }

  /// <summary>
  /// Схема раскрутки развёрнутого состава с подсчётом количества.
  /// Запрашиваются следующие атрибуты:
  /// [F_OBJECT_TYPE] [F_PRJLINK_ID] [F_OBJECT_ID] [Количество] [Заголовок].
  /// ВНИМАНИЕ ! НЕ МЕНЯТЬ ПОРЯДОК ПОЛЕЙ ! НЕ УДАЛЯТЬ СУЩЕСТВУЮЩИЕ ПОЛЯ ! ИНАЧЕ БУДЕТ БОЛЬШОЙ ПРИВЕТ ОТ ПЛАГИНА PDM !
  /// </summary>
  /// <param name="session">Сессия, в рамках которой происходит работа со схемой</param>
  /// <param name="ObjTypes">Массив идентификаторов типов объектов, по которым раскручивается состав</param>
  /// <param name="RelTypes">Массив идентификаторов типов связей, по которым раскручивается состав</param>
  /// <returns>Схема раскрутки развёрнутого состава с подсчётом количества</returns>
  public static RuntimeSearchScheme GetCompositionQuantityScheme(
    IUserSession session,
    int[] ObjTypes,
    int[] RelTypes)
  {
    AttributeSource[] AnAttributes = new AttributeSource[5]
    {
      new AttributeSource(-7, new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object),
      new AttributeSource(-20, new Guid("cad00033-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation),
      new AttributeSource(-2, new Guid("cad00029-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object),
      new AttributeSource(session.GetAttributeType(new Guid("cad00267-306c-11d8-b4e9-00304f19f545")).AttributeID, new Guid("cad00267-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation),
      new AttributeSource(-50, new Guid("cad00047-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object)
    };
    return new RuntimeSearchScheme(SearchDirection.RecursiveContains, 0L, ObjTypes, RelTypes, AnAttributes);
  }

  /// <summary>
  /// Получить список описателей колонок для работы с виртуальной схемой раскрытия состава и подсчёта количества.
  /// ВНИМАНИЕ ! НЕ МЕНЯТЬ ПОРЯДОК ПОЛЕЙ ! НЕ УДАЛЯТЬ СУЩЕСТВУЮЩИЕ ПОЛЯ ! ИНАЧЕ БУДЕТ БОЛЬШОЙ ПРИВЕТ ОТ ПЛАГИНА PDM !
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>Описатели колонок</returns>
  public static List<ColumnDescriptor> GetCompositionQuantitySchemeDescriptors(IUserSession session)
  {
    return new List<ColumnDescriptor>(0)
    {
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) session.GetAttributeType(new Guid("cad00267-306c-11d8-b4e9-00304f19f545")).AttributeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
  }
}
