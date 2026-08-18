// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemAttribute
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public class SystemAttribute : IpsMetadataEntityType
{
  [CanBeNull]
  private readonly IMSAttributeType _descriptor;
  private ColumnContents? _defaultColumnContent;
  private ColumnDescriptor? _column;
  private ColumnDescriptor? _objectColumn;
  private ColumnDescriptor? _relationColumn;

  private protected SystemAttribute([NotNull] SystemAttribute systemAttribute)
    : base(systemAttribute._id, systemAttribute.Guid, systemAttribute._HolderType, systemAttribute.Obligatory, systemAttribute._IdPropertyName)
  {
    this._descriptor = systemAttribute._descriptor;
    this._Found = new bool?(systemAttribute.Found);
  }

  public SystemAttribute(
    [CanBeEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
    this._Found = new bool?(!Intermech.Check.AttributeIdIsEmpty(id));
    if (this.Found)
      this._descriptor = MetaDataHelperService.Instance.GetAttributeType(id);
    else if (obligatory)
      throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден!");
  }

  protected internal SystemAttribute(
    [NotEmpty] ObligatoryObjectAttributes id,
    [NotNull] Type holderType,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, holderType, idPropertyName)
  {
    this._descriptor = MetaDataHelperService.Instance.GetAttributeType((int) id);
    this._Found = new bool?(true);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IMSAttributeType GetDescriptor()
  {
    if (!this.Found)
      throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден!");
    return this._descriptor;
  }

  public ColumnContents DefaultColumnContent
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (!this._defaultColumnContent.HasValue)
        this._defaultColumnContent = new ColumnContents?(this.Descriptor.GetDefaultContent());
      return this._defaultColumnContent.Value;
    }
  }

  public ColumnDescriptor Column
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._column ?? (this._column = new ColumnDescriptor?(this.GetColumn())).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ColumnDescriptor GetColumn(
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source,
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    int num = content.HasValue ? 1 : 0;
    return new ColumnDescriptor((object) this.ID, source, (ColumnContents) ((int) content ?? (int) this.DefaultColumnContent), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ColumnDescriptor GetColumn(
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content = ColumnContents.Text,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return this.GetColumn(source, new ColumnContents?(content), mapping, sort, orderByID);
  }

  public ColumnDescriptor ObjectColumn
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._objectColumn ?? (this._objectColumn = new ColumnDescriptor?(this.GetObjectColumn())).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ColumnDescriptor GetObjectColumn(
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return this.GetColumn(AttributeSourceTypes.Object, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ColumnDescriptor GetObjectColumn(
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return this.GetColumn(AttributeSourceTypes.Object, new ColumnContents?(content), mapping, sort, orderByID);
  }

  public ColumnDescriptor RelationColumn
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._relationColumn ?? (this._relationColumn = new ColumnDescriptor?(this.GetObjectColumn())).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ColumnDescriptor GetRelationColumn(
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents? content = null,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return this.GetColumn(AttributeSourceTypes.Relation, content, mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ColumnDescriptor GetRelationColumn(
    [ValueProvider("Intermech.Extensions.DB.Content")] ColumnContents content,
    [ValueProvider("Intermech.Extensions.DB.Source")] AttributeSourceTypes source = AttributeSourceTypes.Auto,
    [ValueProvider("Intermech.Extensions.DB.Mapping")] ColumnNameMapping mapping = ColumnNameMapping.Default,
    [ValueProvider("Intermech.Extensions.DB.Sort")] SortOrders sort = SortOrders.NONE,
    int orderByID = 0)
  {
    return this.GetColumn(AttributeSourceTypes.Relation, new ColumnContents?(content), mapping, sort, orderByID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator ColumnDescriptor([NotNull] SystemAttribute attributeType)
  {
    return attributeType.Column;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator ColumnDescriptor[]([NotNull] SystemAttribute attributeType)
  {
    return new ColumnDescriptor[1]{ attributeType.Column };
  }

  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator string([NotNull] SystemAttribute attributeType)
  {
    return attributeType.Name;
  }

  [NotNull]
  public IMSAttributeType Descriptor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDescriptor();
  }

  [NotNull]
  [NotWhitespace]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.Name;
  }

  [NotNull]
  [NotWhitespace]
  public string ShortName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.ShortName;
  }
}
