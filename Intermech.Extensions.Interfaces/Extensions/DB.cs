// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DB
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DB
{
  public static readonly If AttributeExists = new If(RelationalOperators.AttributeExists);
  public static readonly If InFiltrationTable = new If(RelationalOperators.InFiltrationTable);
  public static readonly If InGlobalIndex = new If(RelationalOperators.InGlobalIndex);
  public static readonly If Empty = new If(RelationalOperators.Empty);
  public static readonly If NotEmpty = new If(RelationalOperators.NotEmpty);
  public static readonly If LocalObjectTypes = new If(RelationalOperators.LocalObjectTypes);
  public static readonly If NotExistsOrEmpty = new If(RelationalOperators.NotExistsOrEmpty);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [Intermech.Diagnostics.NotEmpty] int id,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    if (!content.HasValue)
      content = new ColumnContents?((MetaDataHelperService.Instance.GetAttributeType(id) ?? throw new AttributeTypeNotFoundException($"Атрибут ID = {id} не найден в системе!")).GetDefaultContent());
    return new ColumnDescriptor((object) id, source, content.Value, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes id,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute((int) id, source, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [Intermech.Diagnostics.NotEmpty] Guid guid,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    if (!content.HasValue)
      content = new ColumnContents?((MetaDataHelperService.Instance.GetAttributeType(guid) ?? throw new AttributeTypeNotFoundException($"Атрибут GUID = {guid} не найден в системе!")).GetDefaultContent());
    return new ColumnDescriptor((object) guid, source, content.Value, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [NotNull, NotWhitespace] string guidOrName,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    int result1;
    if (int.TryParse(guidOrName, out result1))
      return DB.Attribute(result1, source, content, mapping, sort, orderByID);
    Guid result2;
    if (Guid.TryParse(guidOrName, out result2))
      return DB.Attribute(result2, source, content, mapping, sort, orderByID);
    int attributeByTypeNameId = MetaDataHelperService.Instance.GetAttributeByTypeNameID(guidOrName);
    if (Intermech.Check.AttributeIdIsEmpty(attributeByTypeNameId))
      throw new AttributeTypeNotFoundException($"Тип атрибута \"{guidOrName}\" не найден!");
    return DB.Attribute(attributeByTypeNameId, source, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [Intermech.Diagnostics.NotEmpty] int id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, source, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, source, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [Intermech.Diagnostics.NotEmpty] Guid guid,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guid, source, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor Attribute(
    [NotNull, NotWhitespace] string guidOrName,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guidOrName, source, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [Intermech.Diagnostics.NotEmpty] int id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Object, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes id,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Object, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [Intermech.Diagnostics.NotEmpty] Guid guid,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guid, AttributeSourceTypes.Object, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [NotNull, NotWhitespace] string guidOrName,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guidOrName, AttributeSourceTypes.Object, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [Intermech.Diagnostics.NotEmpty] int id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Object, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Object, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [Intermech.Diagnostics.NotEmpty] Guid guid,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guid, AttributeSourceTypes.Object, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor ObjectAttribute(
    [NotNull, NotWhitespace] string guidOrName,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guidOrName, AttributeSourceTypes.Object, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [Intermech.Diagnostics.NotEmpty] int id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Relation, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes id,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Relation, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [Intermech.Diagnostics.NotEmpty] Guid guid,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guid, AttributeSourceTypes.Relation, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [NotNull, NotWhitespace] string guidOrName,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guidOrName, AttributeSourceTypes.Relation, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [Intermech.Diagnostics.NotEmpty] int id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Relation, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes id,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(id, AttributeSourceTypes.Relation, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [Intermech.Diagnostics.NotEmpty] Guid guid,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guid, AttributeSourceTypes.Relation, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor RelationAttribute(
    [NotNull, NotWhitespace] string guidOrName,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return DB.Attribute(guidOrName, AttributeSourceTypes.Relation, new ColumnContents?(content), mapping, sort, orderByID);
  }

  public static int GetAttributeID([CanBeNull] object value, bool throwExceptionIfCantGet = true)
  {
    if (throwExceptionIfCantGet && value == null)
      Intermech.Diagnostics.Check.ArgumentNotNull<object>((object) null, nameof (value));
    object obj1 = value;
    if (obj1 != null)
    {
      if (obj1 is int attributeId1)
      {
        if (throwExceptionIfCantGet)
          Intermech.Check.AttributeIdNotEmpty(attributeId1, "intValue");
        return attributeId1;
      }
      if (!(obj1 is SystemAttribute systemAttribute))
      {
        object obj2;
        if ((obj2 = obj1) is ObligatoryObjectAttributes)
        {
          ObligatoryObjectAttributes attributeId2 = (ObligatoryObjectAttributes) obj2;
          if (throwExceptionIfCantGet)
            Intermech.Check.AttributeIdNotEmpty((int) attributeId2, "obligatoryObjectAttribute");
          return (int) attributeId2;
        }
        switch (obj1)
        {
          case Guid attrTypeGuid:
            IMSAttributeType attributeType1 = MetaDataHelperService.Instance.GetAttributeType(attrTypeGuid);
            if (attributeType1 != null)
              return attributeType1.AttributeID;
            if (!throwExceptionIfCantGet)
              return 0;
            throw new AttributeTypeNotFoundException($"Атрибут GUID = {attrTypeGuid} не найден в системе!");
          case string str:
            if (string.IsNullOrWhiteSpace(str))
            {
              if (!throwExceptionIfCantGet)
                return 0;
              throw new Exception("empty string as attributeID!");
            }
            int result1;
            if (int.TryParse(str, out result1))
            {
              if (throwExceptionIfCantGet)
                Intermech.Check.AttributeIdNotEmpty(result1, "parsedID");
              return result1;
            }
            Guid result2;
            if (Guid.TryParse(str, out result2))
            {
              IMSAttributeType attributeType2 = MetaDataHelperService.Instance.GetAttributeType(result2);
              if (attributeType2 != null)
                return attributeType2.AttributeID;
              if (!throwExceptionIfCantGet)
                return 0;
              throw new AttributeTypeNotFoundException($"Атрибут GUID = {result2} не найден в системе!");
            }
            int attributeByTypeNameId = MetaDataHelperService.Instance.GetAttributeByTypeNameID(str);
            if (Intermech.Check.AttributeIdIsEmpty(attributeByTypeNameId))
            {
              if (!throwExceptionIfCantGet)
                return 0;
              throw new AttributeTypeNotFoundException($"Атрибут '{str}' не найден в системе!");
            }
            if (throwExceptionIfCantGet)
              Intermech.Check.AttributeIdNotEmpty(attributeByTypeNameId, "attributeID");
            return attributeByTypeNameId;
          case ColumnDescriptor columnDescriptor:
            return DB.GetAttributeID(columnDescriptor.AttributeID, throwExceptionIfCantGet);
        }
      }
      else
      {
        systemAttribute.CheckIsFound();
        return systemAttribute.ID;
      }
    }
    if (throwExceptionIfCantGet)
      throw new AttributeTypeNotFoundException($"Не удаётся найти атрибут по ключу {value} типа {value.GetType().FullName}!");
    return 0;
  }

  public static bool TryGetAttributeID([CanBeNull] object value, out int attributeID)
  {
    if (value == null)
    {
      attributeID = 0;
      return false;
    }
    attributeID = DB.GetAttributeID(value, false);
    return attributeID != 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Column([Intermech.Diagnostics.NotEmpty] in ColumnDescriptor columnDescriptor)
  {
    return new ColumnDescriptor[1]{ columnDescriptor };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Column(
    [Intermech.Diagnostics.NotEmpty] int attributeID,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return new ColumnDescriptor[1]
    {
      DB.Attribute(attributeID, source, content, mapping, sort, orderByID)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Column(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes attributeID,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return new ColumnDescriptor[1]
    {
      DB.Attribute(attributeID, source, content, mapping, sort, orderByID)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Column(
    [Intermech.Diagnostics.NotEmpty] Guid attributeGuid,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return new ColumnDescriptor[1]
    {
      DB.Attribute(attributeGuid, source, content, mapping, sort, orderByID)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Column(
    [NotNull, NotWhitespace] string attributeGuidOrName,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return new ColumnDescriptor[1]
    {
      DB.Attribute(attributeGuidOrName, source, content, mapping, sort, orderByID)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Columns([CanBeNull] params ColumnDescriptor[] values)
  {
    return values == null || values.Length == 0 ? Array.Empty<ColumnDescriptor>() : values;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Columns([CanBeNull, ValueProvider("Intermech.Extensions.DB.ObjAttr"), ValueProvider("Intermech.Extensions.DB.RelAttr")] params int[] values)
  {
    if (values == null || values.Length == 0)
      return Array.Empty<ColumnDescriptor>();
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[values.Length];
    for (int index = 0; index < values.Length; ++index)
    {
      int id = values[index];
      columnDescriptorArray[index] = DB.Attribute(id);
    }
    return columnDescriptorArray;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Columns([CanBeNull] params ObligatoryObjectAttributes[] values)
  {
    if (values == null || values.Length == 0)
      return Array.Empty<ColumnDescriptor>();
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[values.Length];
    for (int index = 0; index < values.Length; ++index)
    {
      ObligatoryObjectAttributes id = values[index];
      columnDescriptorArray[index] = DB.Attribute(id);
    }
    return columnDescriptorArray;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Columns(
    [CanBeNull] params (int AttributeID, AttributeSourceTypes Source)[] values)
  {
    if (values == null || values.Length == 0)
      return Array.Empty<ColumnDescriptor>();
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[values.Length];
    for (int index = 0; index < values.Length; ++index)
    {
      (int AttributeID, AttributeSourceTypes Source) tuple = values[index];
      columnDescriptorArray[index] = DB.Attribute(tuple.AttributeID, tuple.Source);
    }
    return columnDescriptorArray;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] Columns(
    [CanBeNull] params (int AttributeID, AttributeSourceTypes Source, ColumnContents Content)[] values)
  {
    if (values == null || values.Length == 0)
      return Array.Empty<ColumnDescriptor>();
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[values.Length];
    for (int index = 0; index < values.Length; ++index)
    {
      (int AttributeID, AttributeSourceTypes Source, ColumnContents Content) tuple = values[index];
      columnDescriptorArray[index] = DB.Attribute(tuple.AttributeID, tuple.Source, new ColumnContents?(tuple.Content));
    }
    return columnDescriptorArray;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnDescriptor[] ColumnsDescriptors([CanBeNull] params ColumnDescriptor[] values)
  {
    return values == null || values.Length == 0 ? Array.Empty<ColumnDescriptor>() : values;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ColumnDescriptors ColumnsWithSorting([CanBeNull] params ColumnDescriptor[] columns)
  {
    return new DB.ColumnDescriptors(columns);
  }

  [CanBeNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object[] ColumnsObjectArray([CanBeNull] params ColumnDescriptor[] values)
  {
    return values == null || values.Length == 0 ? Array.Empty<object>() : values.Cast<object>().AsArray<object>(values.Length);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [Intermech.Diagnostics.NotEmpty] int attributeID,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes attributeID,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((int) attributeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition([Intermech.Diagnostics.NotEmpty] ColumnDescriptor columnDescriptor, [Intermech.Diagnostics.NotEmpty] If condition)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(columnDescriptor.GetAttributeID(), condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, columnDescriptor.AttributeSource, columnDescriptor.Contents)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [Intermech.Diagnostics.NotEmpty] Guid attributeGuid,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((MetaDataHelperService.Instance.GetObjectType(attributeGuid) ?? throw new ObjectTypeNotFoundException(attributeGuid)).ObjectTypeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [NotNull, NotWhitespace] string attributeGuidOrName,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    Guid result;
    int attributeID;
    if (Guid.TryParse(attributeGuidOrName, out result))
    {
      attributeID = (MetaDataHelperService.Instance.GetObjectType(result) ?? throw new ObjectTypeNotFoundException(result)).ObjectTypeID;
    }
    else
    {
      attributeID = MetaDataHelperService.Instance.GetAttributeByTypeNameID(attributeGuidOrName);
      if (attributeID == -1)
        throw new ObjectTypeNotFoundException(attributeGuidOrName, (string) null);
    }
    return new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [Intermech.Diagnostics.NotEmpty] int attributeID,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes attributeID,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((int) attributeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [Intermech.Diagnostics.NotEmpty] Guid attributeGuid,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((MetaDataHelperService.Instance.GetObjectType(attributeGuid) ?? throw new ObjectTypeNotFoundException(attributeGuid)).ObjectTypeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ConditionStructure[] Condition(
    [NotNull, NotWhitespace] string attributeGuidOrName,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source,
    [Intermech.Diagnostics.NotEmpty] If condition,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text)
  {
    Guid result;
    int attributeID;
    if (Guid.TryParse(attributeGuidOrName, out result))
    {
      attributeID = (MetaDataHelperService.Instance.GetObjectType(result) ?? throw new ObjectTypeNotFoundException(result)).ObjectTypeID;
    }
    else
    {
      attributeID = MetaDataHelperService.Instance.GetAttributeByTypeNameID(attributeGuidOrName);
      if (attributeID == -1)
        throw new ObjectTypeNotFoundException(attributeGuidOrName, (string) null);
    }
    return new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, condition.Operator, condition.Value, condition.Value2, LogicalOperators.NONE, 0, condition.CaseSensitive, source, content)
    };
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup Conditions(
    [NotNull] params (ColumnDescriptor Attribute, If Condition)[] values)
  {
    return values.Length == 0 ? DB.ConditionsGroup.Empty : new DB.ConditionsGroup(LogicalOperators.AND, (IReadOnlyCollection<ConditionStructure>) DB.GetConditionsEnumeration(values.Cast<object>()).ToList<ConditionStructure>(values.Length));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup Conditions([NotNull, ItemNotNull] params object[] values)
  {
    return values.Length == 0 ? DB.ConditionsGroup.Empty : new DB.ConditionsGroup(LogicalOperators.AND, (IReadOnlyCollection<ConditionStructure>) DB.GetConditionsEnumeration((IEnumerable<object>) values).ToList<ConditionStructure>(values.Length));
  }

  [NotNull]
  public static IEnumerable<ConditionStructure> GetConditionsEnumeration(
    [CanBeNull, ItemNotNull] IEnumerable<object> values,
    int groupIndex = 0,
    LogicalOperators logicalOperator = LogicalOperators.AND)
  {
    if (values != null)
    {
      using (IEnumerator<object> enumerator = values.GetEnumerator())
      {
        int nestedGroupIndex = groupIndex + 1;
        int attributeID = 0;
        bool foundNextAttribute = false;
        object nextValue = (object) null;
        while (foundNextAttribute || enumerator.MoveNext())
        {
          object obj1 = !foundNextAttribute ? enumerator.Current : nextValue;
          AttributeSourceTypes source = AttributeSourceTypes.Auto;
          ColumnContents? content = new ColumnContents?();
          object obj2 = obj1;
          switch (obj2)
          {
            case null:
label_41:
              throw new InvalidOperationException($"Unknown condition type {obj1.GetType()}!");
            case ConditionStructure conditionStructure1:
              yield return conditionStructure1;
              continue;
            case ColumnDescriptor columnDescriptor2:
              attributeID = columnDescriptor2.GetAttributeID();
              source = columnDescriptor2.AttributeSource;
              content = new ColumnContents?(columnDescriptor2.Contents);
              break;
            case SystemAttribute systemAttribute:
              systemAttribute.CheckIsFound();
              attributeID = systemAttribute.ID;
              break;
            case int num:
              attributeID = num;
              break;
            default:
              object obj3;
              if ((obj3 = obj2) is ObligatoryObjectAttributes)
              {
                attributeID = (int) obj3;
                break;
              }
              switch (obj2)
              {
                case Guid attrTypeGuid:
                  attributeID = MetaDataHelperService.Instance.GetAttributeTypeID(attrTypeGuid);
                  if (Intermech.Check.AttributeIdIsEmpty(attributeID))
                    throw new AttributeTypeNotFoundException($"Атрибут GUID = {attrTypeGuid} не найден в системе!");
                  break;
                case string str:
                  if (!int.TryParse(str, out attributeID))
                  {
                    Guid result;
                    if (Guid.TryParse(str, out result))
                    {
                      attributeID = MetaDataHelperService.Instance.GetAttributeTypeID(result);
                      if (Intermech.Check.AttributeIdIsEmpty(attributeID))
                        throw new AttributeTypeNotFoundException($"Атрибут GUID = {result} не найден в системе!");
                      break;
                    }
                    attributeID = MetaDataHelperService.Instance.GetAttributeByTypeNameID(str);
                    if (Intermech.Check.AttributeIdIsEmpty(attributeID))
                      throw new AttributeTypeNotFoundException($"Тип атрибута \"{str}\" не найден!");
                    break;
                  }
                  break;
                case DB.ConditionsGroup source1:
                  if (logicalOperator != source1.Operator)
                  {
                    foreach (ConditionStructure conditionStructure in DB.GetConditionsEnumeration(source1.Cast<object>(), nestedGroupIndex + 1, source1.Operator))
                      yield return conditionStructure;
                    break;
                  }
                  foreach (ConditionStructure conditionStructure in DB.GetConditionsEnumeration(source1.Cast<object>(), groupIndex, source1.Operator))
                    yield return conditionStructure;
                  break;
                case (ColumnDescriptor, If) valueTuple1:
                  ColumnDescriptor columnDescriptor1 = valueTuple1.Item1;
                  If if1 = valueTuple1.Item2;
                  yield return new ConditionStructure(columnDescriptor1.GetAttributeID(), if1.Operator, if1.Value, if1.Value2, logicalOperator, groupIndex, if1.CaseSensitive, columnDescriptor1.AttributeSource, columnDescriptor1.Contents);
                  continue;
                case (int, If) valueTuple2:
                  int attributeID1 = valueTuple2.Item1;
                  If if2 = valueTuple2.Item2;
                  yield return new ConditionStructure(attributeID1, if2.Operator, if2.Value, if2.Value2, logicalOperator, groupIndex, if2.CaseSensitive, AttributeSourceTypes.Auto, Attributes.GetDefaultContent(attributeID1));
                  continue;
                case IEnumerable<ConditionStructure> conditionStructures:
                  foreach (ConditionStructure conditionStructure in conditionStructures)
                    yield return conditionStructure;
                  continue;
                case IEnumerable source2:
                  foreach (ConditionStructure conditionStructure in DB.GetConditionsEnumeration(source2.Cast<object>(), groupIndex, logicalOperator))
                    yield return conditionStructure;
                  continue;
                default:
                  goto label_41;
              }
              break;
          }
          RelationalOperators relationalOperator = RelationalOperators.Empty;
          object conditionValue = (object) null;
          object conditionValue2 = (object) null;
          bool caseSensitive = true;
          while (enumerator.MoveNext())
          {
            nextValue = enumerator.Current;
            object obj4 = nextValue;
            if (obj4 != null)
            {
              if (obj4 is If if3)
              {
                relationalOperator = if3.Operator;
                conditionValue = if3.Value;
                conditionValue2 = if3.Value2;
                caseSensitive = if3.CaseSensitive;
                continue;
              }
              object obj5;
              if ((obj5 = obj4) is AttributeSourceTypes)
              {
                source = (AttributeSourceTypes) obj5;
                continue;
              }
              object obj6;
              if ((obj6 = obj4) is ColumnContents)
              {
                content = new ColumnContents?((ColumnContents) obj6);
                continue;
              }
            }
            foundNextAttribute = true;
            break;
          }
          if (!content.HasValue)
            content = new ColumnContents?((MetaDataHelperService.Instance.GetAttributeType(attributeID) ?? throw new AttributeTypeNotFoundException($"Атрибут ID = {attributeID} не найден в системе!")).GetDefaultContent());
          yield return new ConditionStructure(attributeID, relationalOperator, conditionValue, conditionValue2, logicalOperator, groupIndex, caseSensitive, source, content.Value);
          attributeID = 0;
          if (!foundNextAttribute)
            yield break;
          content = new ColumnContents?();
        }
        nextValue = (object) null;
      }
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup And([NotNull, Intermech.Diagnostics.NotEmpty] params ConditionStructure[] conditions)
  {
    return new DB.ConditionsGroup(LogicalOperators.AND, (IReadOnlyCollection<ConditionStructure>) conditions);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup And([NotNull, Intermech.Diagnostics.NotEmpty] params object[] values)
  {
    return new DB.ConditionsGroup(LogicalOperators.AND, (IReadOnlyCollection<ConditionStructure>) DB.GetConditionsEnumeration((IEnumerable<object>) values).ToList<ConditionStructure>(values.Length));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup Or([NotNull, Intermech.Diagnostics.NotEmpty] params ConditionStructure[] conditions)
  {
    return new DB.ConditionsGroup(LogicalOperators.OR, (IReadOnlyCollection<ConditionStructure>) conditions);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup Or([NotNull, Intermech.Diagnostics.NotEmpty] params object[] values)
  {
    return new DB.ConditionsGroup(LogicalOperators.OR, (IReadOnlyCollection<ConditionStructure>) DB.GetConditionsEnumeration((IEnumerable<object>) values).ToList<ConditionStructure>(values.Length));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup Not([NotNull, Intermech.Diagnostics.NotEmpty] params ConditionStructure[] conditions)
  {
    return new DB.ConditionsGroup(LogicalOperators.OR, (IReadOnlyCollection<ConditionStructure>) conditions);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DB.ConditionsGroup Not([NotNull, Intermech.Diagnostics.NotEmpty] params object[] values)
  {
    return new DB.ConditionsGroup(LogicalOperators.NOT, (IReadOnlyCollection<ConditionStructure>) DB.GetConditionsEnumeration((IEnumerable<object>) values).ToList<ConditionStructure>(values.Length));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If EqualTo([NotNull] object value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.Equal, value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotEqualTo([NotNull] object value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.NotEqual, value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If Between([NotNull] object value, [NotNull] object value2)
  {
    return new If(RelationalOperators.Between, value, value2);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If ConsistFrom([NotNull] object value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.ConsistFrom, value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If ConsistFromType([NotNull] object value)
  {
    return new If(RelationalOperators.ConsistFromType, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If StartString([NotNull] string value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.StartString, (object) value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If EndString([NotNull] string value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.EndString, (object) value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotStartString([NotNull] string value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.NotStartString, (object) value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotEndString([NotNull] string value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.NotEndString, (object) value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If EntersIn([NotNull] object value) => new If(RelationalOperators.EntersIn, value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If ExistsInVersionContext([NotNull] object value)
  {
    return new If(RelationalOperators.ExistsInVersionContext, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If EntersInType([NotNull] object value)
  {
    return new If(RelationalOperators.EntersInType, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If Greater([NotNull] object value) => new If(RelationalOperators.Greater, value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If GreaterOrEqual([NotNull] object value)
  {
    return new If(RelationalOperators.GreaterOrEqual, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If In([NotNull] IReadOnlyCollection<long> values)
  {
    if (!(values is long[] numArray))
      numArray = values.ToArray<long>(values.Count);
    return new If(RelationalOperators.In, (object) numArray);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If In([NotNull] params long[] values)
  {
    return new If(RelationalOperators.In, (object) values);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If In([NotNull] IReadOnlyCollection<object> values)
  {
    if (!(values is object[] objArray))
      objArray = values.ToArray<object>(values.Count);
    return new If(RelationalOperators.In, (object) objArray);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotIn([NotNull] IReadOnlyCollection<long> values)
  {
    if (!(values is long[] numArray))
      numArray = values.ToArray<long>(values.Count);
    return new If(RelationalOperators.NotIn, (object) numArray);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotIn([NotNull] params long[] values)
  {
    return new If(RelationalOperators.NotIn, (object) values);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotIn([NotNull] object[] values)
  {
    return new If(RelationalOperators.NotIn, (object) values);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If InLcHistory([NotNull] object value)
  {
    return new If(RelationalOperators.InLCHistory, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If LastNDays(int days) => new If(RelationalOperators.LastNDays, (object) days);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If InSelection([NotNull] object value)
  {
    return new If(RelationalOperators.InSelection, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If Less([NotNull] object value) => new If(RelationalOperators.Less, value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If LessOrEqual([NotNull] object value)
  {
    return new If(RelationalOperators.LessOrEqual, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If Linked([NotNull] object value) => new If(RelationalOperators.Linked, value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotLinked([NotNull] object value)
  {
    return new If(RelationalOperators.NotLinked, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NextNDays(int days) => new If(RelationalOperators.NextNDays, (object) days);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotSubstring([NotNull] string value, bool caseSensitive = true)
  {
    return new If(RelationalOperators.NotSubstring, (object) value, caseSensitive: caseSensitive);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If NotConsistFromType([NotNull] object value)
  {
    return new If(RelationalOperators.NotConsistFromType, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If ObjectTypeFilter([NotNull] int[] objTypes)
  {
    return new If(RelationalOperators.ObjectTypeFilter, (object) objTypes);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If ParentVersionID(long parentVersionID)
  {
    return new If(RelationalOperators.ParentVersionID, (object) parentVersionID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static If StringTemplate([NotNull] string stringTemplate, bool caseSensitive = true)
  {
    return new If(RelationalOperators.StringTemplate, (object) stringTemplate, caseSensitive: caseSensitive);
  }

  public static class Source
  {
    public const AttributeSourceTypes Auto = AttributeSourceTypes.Auto;
    public const AttributeSourceTypes Object = AttributeSourceTypes.Object;
    public const AttributeSourceTypes Relation = AttributeSourceTypes.Relation;
    public const AttributeSourceTypes Events = AttributeSourceTypes.Events;
    public const AttributeSourceTypes History = AttributeSourceTypes.History;
    public const AttributeSourceTypes FileStorage = AttributeSourceTypes.FileStorage;
    public const AttributeSourceTypes Snapshot = AttributeSourceTypes.Snapshot;
    public const AttributeSourceTypes Other = AttributeSourceTypes.Other;
  }

  public static class Content
  {
    public const ColumnContents Text = ColumnContents.Text;
    public const ColumnContents ID = ColumnContents.ID;
    public const ColumnContents Date = ColumnContents.Date;
    public const ColumnContents Value = ColumnContents.Value;
    public const ColumnContents String = ColumnContents.String;
  }

  public static class Mapping
  {
    public const ColumnNameMapping Default = ColumnNameMapping.Default;
    public const ColumnNameMapping ID = ColumnNameMapping.ID;
    public const ColumnNameMapping Guid = ColumnNameMapping.Guid;
    public const ColumnNameMapping Alias = ColumnNameMapping.Alias;
    public const ColumnNameMapping ShortName = ColumnNameMapping.ShortName;
    public const ColumnNameMapping Name = ColumnNameMapping.Name;
    public const ColumnNameMapping FieldName = ColumnNameMapping.FieldName;
    public const ColumnNameMapping Index = ColumnNameMapping.Index;
  }

  public static class Sort
  {
    public const SortOrders NONE = SortOrders.NONE;
    public const SortOrders ASC = SortOrders.ASC;
    public const SortOrders DESC = SortOrders.DESC;
  }

  public static class ObjectAttr
  {
    public static readonly ColumnDescriptor VersionID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJECT_ID);
    public static readonly ColumnDescriptor ObjectID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJECT_ID);
    public static readonly ColumnDescriptor ID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_ID);
    public static readonly ColumnDescriptor LcStepID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_LC_STEP, ColumnContents.ID);
    public static readonly ColumnDescriptor LcStepName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_LC_STEP, ColumnContents.String);
    public static readonly ColumnDescriptor VersionNum = DB.ObjectAttribute(ObligatoryObjectAttributes.F_VERSION_ID);
    public static readonly ColumnDescriptor CheckOutBy = DB.ObjectAttribute(ObligatoryObjectAttributes.F_CHKOUT_BY, ColumnContents.ID);
    public static readonly ColumnDescriptor CheckOutByString = DB.ObjectAttribute(ObligatoryObjectAttributes.F_CHKOUT_BY, ColumnContents.String);
    public static readonly ColumnDescriptor TypeID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJECT_TYPE, ColumnContents.ID);
    public static readonly ColumnDescriptor TypeName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJECT_TYPE, ColumnContents.String);
    public static readonly ColumnDescriptor OwnerID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OWNER_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor OwnerName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OWNER_ID, ColumnContents.String);
    public static readonly ColumnDescriptor LevelID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_LEVEL_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor LevelName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_LEVEL_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor ModifyDate = DB.ObjectAttribute(ObligatoryObjectAttributes.F_MODIFY_DATE);
    public static readonly ColumnDescriptor AreaID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_AREA_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor AreaName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_AREA_ID, ColumnContents.String);
    public static readonly ColumnDescriptor VersionGuid = DB.ObjectAttribute(ObligatoryObjectAttributes.F_GUID);
    public static readonly ColumnDescriptor Created = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJ_CREATE);
    public static readonly ColumnDescriptor ProjectID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_PROJECT_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor ProjectName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_PROJECT_ID, ColumnContents.String);
    public static readonly ColumnDescriptor ModificationID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_MODIFICATION_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor ModificationName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_MODIFICATION_ID, ColumnContents.String);
    public static readonly ColumnDescriptor BaseVersion = DB.ObjectAttribute(ObligatoryObjectAttributes.F_BASE_VERSION);
    public static readonly ColumnDescriptor SiteID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_SITE_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor GUID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJ_GUID);
    public static readonly ColumnDescriptor VersionType = DB.ObjectAttribute(ObligatoryObjectAttributes.F_OBJECT_VER_TYPE);
    public static readonly ColumnDescriptor Caption = DB.ObjectAttribute(ObligatoryObjectAttributes.CAPTION, ColumnContents.String);
    public static readonly ColumnDescriptor Access = DB.ObjectAttribute(ObligatoryObjectAttributes.F_ACCESS);
    public static readonly ColumnDescriptor CreatorID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_CREATOR_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor CreatorName = DB.ObjectAttribute(ObligatoryObjectAttributes.F_CREATOR_ID, ColumnContents.String);
    public static readonly ColumnDescriptor ParentObjectVersionID = DB.ObjectAttribute(ObligatoryObjectAttributes.F_PARENT_OBJECT_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor VersionsCount = DB.ObjectAttribute(ObligatoryObjectAttributes.F_VERSIONS_COUNT);
    public static readonly ColumnDescriptor RefsCount = DB.ObjectAttribute(ObligatoryObjectAttributes.F_REFERENCE_COUNT);
    public static readonly ColumnDescriptor NestedInCount = DB.ObjectAttribute(ObligatoryObjectAttributes.F_RELATIONS_COUNT);
    public static readonly ColumnDescriptor LcStepChanged = DB.ObjectAttribute(ObligatoryObjectAttributes.F_LCSTEP_DATE);
  }

  public static class RelationAttr
  {
    public static readonly ColumnDescriptor PrjLinkID = DB.RelationAttribute(ObligatoryObjectAttributes.F_PRJLINK_ID);
    public static readonly ColumnDescriptor ProjID = DB.RelationAttribute(ObligatoryObjectAttributes.F_PROJ_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor ProjName = DB.RelationAttribute(ObligatoryObjectAttributes.F_PROJ_ID, ColumnContents.String);
    public static readonly ColumnDescriptor PartID = DB.RelationAttribute(ObligatoryObjectAttributes.F_PART_ID, ColumnContents.ID);
    public static readonly ColumnDescriptor PartName = DB.RelationAttribute(ObligatoryObjectAttributes.F_PART_ID, ColumnContents.String);
    public static readonly ColumnDescriptor TypeID = DB.RelationAttribute(ObligatoryObjectAttributes.F_RELATION_TYPE, ColumnContents.ID);
    public static readonly ColumnDescriptor TypeName = DB.RelationAttribute(ObligatoryObjectAttributes.F_RELATION_TYPE, ColumnContents.String);
    public static readonly ColumnDescriptor FromDate = DB.RelationAttribute(ObligatoryObjectAttributes.F_CREATE_DATE);
    public static readonly ColumnDescriptor Guid = DB.RelationAttribute(ObligatoryObjectAttributes.F_PRJ_GUID);
    public static readonly ColumnDescriptor CreatorID = DB.RelationAttribute(ObligatoryObjectAttributes.F_REL_CREATOR, ColumnContents.ID);
    public static readonly ColumnDescriptor CreatorName = DB.RelationAttribute(ObligatoryObjectAttributes.F_REL_CREATOR, ColumnContents.String);
  }

  public class ColumnDescriptors : List<ColumnDescriptor>
  {
    private int _orderIndex;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ColumnDescriptors([CanBeNull] ColumnDescriptor[] columns)
    {
      if (columns == null || columns.Length == 0)
        return;
      if (this.Capacity < columns.Length)
        this.Capacity = columns.Length;
      this.AddRange((IEnumerable<ColumnDescriptor>) columns);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessParam(
      [NotNull] object attributeID,
      [NotNull] DB.ColumnDescriptors.ProcessColumnDescriptor action)
    {
      int index = this.IndexOfFirst<ColumnDescriptor>((Predicate<ColumnDescriptor>) (column => object.Equals(column.AttributeID, attributeID)));
      if (index < 0)
        return;
      ColumnDescriptor column1 = this[index];
      action(ref column1);
      this[index] = column1;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors OrderBy([Intermech.Diagnostics.NotEmpty] int attributeID, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      this.ProcessParam((object) attributeID, (DB.ColumnDescriptors.ProcessColumnDescriptor) ((ref ColumnDescriptor column) =>
      {
        column.Sort = sortOrder;
        column.OrderByID = this._orderIndex++;
      }));
      return this;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors OrderBy([Intermech.Diagnostics.NotEmpty] in ColumnDescriptor columnDescriptor, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      this.ProcessParam(columnDescriptor.AttributeID, (DB.ColumnDescriptors.ProcessColumnDescriptor) ((ref ColumnDescriptor column) =>
      {
        column.Sort = sortOrder;
        column.OrderByID = this._orderIndex++;
      }));
      return this;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors OrderBy(
      [Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes attributeID,
      [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      return this.OrderBy((int) attributeID, sortOrder);
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors OrderBy([Intermech.Diagnostics.NotEmpty] Guid attributeGuid, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      this.ProcessParam((object) attributeGuid, (DB.ColumnDescriptors.ProcessColumnDescriptor) ((ref ColumnDescriptor column) =>
      {
        column.Sort = sortOrder;
        column.OrderByID = this._orderIndex++;
      }));
      return this;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors OrderBy([NotNull, NotWhitespace] string attributeGuidOrName, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      Guid result;
      if (Guid.TryParse(attributeGuidOrName, out result))
        return this.OrderBy(result, sortOrder);
      int attributeByTypeNameId = MetaDataHelperService.Instance.GetAttributeByTypeNameID(attributeGuidOrName);
      return !Intermech.Check.AttributeIdIsEmpty(attributeByTypeNameId) ? this.OrderBy(attributeByTypeNameId, sortOrder) : throw new AttributeTypeNotFoundException($"Тип атрибута \"{attributeGuidOrName}\" не найден!");
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors ThenBy([Intermech.Diagnostics.NotEmpty] int attributeID, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      this.ProcessParam((object) attributeID, (DB.ColumnDescriptors.ProcessColumnDescriptor) ((ref ColumnDescriptor column) =>
      {
        column.Sort = sortOrder;
        column.OrderByID = this._orderIndex++;
      }));
      return this;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors ThenBy([Intermech.Diagnostics.NotEmpty] in ColumnDescriptor columnDescriptor, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      this.ProcessParam(columnDescriptor.AttributeID, (DB.ColumnDescriptors.ProcessColumnDescriptor) ((ref ColumnDescriptor column) =>
      {
        column.Sort = sortOrder;
        column.OrderByID = this._orderIndex++;
      }));
      return this;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors ThenBy([Intermech.Diagnostics.NotEmpty] ObligatoryObjectAttributes attributeID, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      return this.OrderBy((int) attributeID, sortOrder);
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors ThenBy([Intermech.Diagnostics.NotEmpty] Guid attributeGuid, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      this.ProcessParam((object) attributeGuid, (DB.ColumnDescriptors.ProcessColumnDescriptor) ((ref ColumnDescriptor column) =>
      {
        column.Sort = sortOrder;
        column.OrderByID = this._orderIndex++;
      }));
      return this;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DB.ColumnDescriptors ThenBy([NotNull, NotWhitespace] string attributeGuidOrName, [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sortOrder)
    {
      Guid result;
      if (Guid.TryParse(attributeGuidOrName, out result))
        return this.OrderBy(result, sortOrder);
      int attributeByTypeNameId = MetaDataHelperService.Instance.GetAttributeByTypeNameID(attributeGuidOrName);
      return !Intermech.Check.AttributeIdIsEmpty(attributeByTypeNameId) ? this.OrderBy(attributeByTypeNameId, sortOrder) : throw new AttributeTypeNotFoundException($"Тип атрибута \"{attributeGuidOrName}\" не найден!");
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator object[]([CanBeNull] DB.ColumnDescriptors columns)
    {
      return columns == null || columns.Count <= 0 ? Array.Empty<object>() : columns.Select<ColumnDescriptor, object>((Func<ColumnDescriptor, object>) (column => (object) column)).ToArray<object>(columns.Count);
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ColumnDescriptor[]([CanBeNull] DB.ColumnDescriptors columns)
    {
      return columns == null || columns.Count <= 0 ? Array.Empty<ColumnDescriptor>() : columns.ToArray<ColumnDescriptor>(columns.Count);
    }

    private delegate void ProcessColumnDescriptor(ref ColumnDescriptor column);
  }

  public class ConditionsGroup : List<ConditionStructure>
  {
    public readonly LogicalOperators Operator;
    [NotNull]
    public static readonly DB.ConditionsGroup Empty = new DB.ConditionsGroup(LogicalOperators.AND, (IReadOnlyCollection<ConditionStructure>) Array.Empty<ConditionStructure>());

    public ConditionsGroup(
      LogicalOperators @operator,
      [NotNull] IReadOnlyCollection<ConditionStructure> conditions)
      : base(conditions.Count)
    {
      this.Operator = @operator;
      this.AddRange((IEnumerable<ConditionStructure>) conditions);
    }

    [NotNull]
    public static implicit operator ConditionStructure[]([NotNull] DB.ConditionsGroup conditionsGroup)
    {
      return conditionsGroup.ToArray<ConditionStructure>(conditionsGroup.Count);
    }
  }
}
