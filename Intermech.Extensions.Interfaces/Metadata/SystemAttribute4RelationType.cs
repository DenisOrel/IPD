// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemAttribute4RelationType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public sealed class SystemAttribute4RelationType : SystemAttribute
{
  [NotEmpty]
  public readonly int RelationTypeID;
  [CanBeNull]
  private readonly IMSAttribute4RelationType _descriptor4RelationType;

  public SystemAttribute4RelationType(
    [NotNull] SystemAttribute attribute,
    [NotEmpty] int relationTypeID,
    bool obligatory)
    : base(attribute)
  {
    this.RelationTypeID = relationTypeID;
    this._Found = new bool?(attribute.Found && !Intermech.Check.RelationTypeIdIsEmpty(relationTypeID));
    if (this.Found)
    {
      IMSAttribute4RelationType attribute4RelationType = MetaDataHelperService.Instance.GetAttribute4RelationType(relationTypeID, attribute.ID);
      this._Found = new bool?(attribute4RelationType != null);
      if (this.Found)
        this._descriptor4RelationType = attribute4RelationType;
    }
    if (!obligatory || this.Found)
      return;
    if (!Intermech.Check.RelationTypeIdIsEmpty(relationTypeID))
      throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден у типа связи {MetaDataHelperService.Instance.GetRelationTypeName(relationTypeID)}!");
    throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден!");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IMSAttribute4RelationType GetDescriptor4RelationType()
  {
    if (this.Found)
      return this._descriptor4RelationType;
    if (!Intermech.Check.RelationTypeIdIsEmpty(this.RelationTypeID))
      throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден у типа связи {MetaDataHelperService.Instance.GetRelationTypeName(this.RelationTypeID)}!");
    throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден!");
  }

  [NotNull]
  public IMSAttribute4RelationType Descriptor4RelationType
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDescriptor4RelationType();
  }
}
