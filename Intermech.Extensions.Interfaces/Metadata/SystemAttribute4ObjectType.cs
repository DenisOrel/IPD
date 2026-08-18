// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemAttribute4ObjectType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public sealed class SystemAttribute4ObjectType : SystemAttribute
{
  [NotEmpty]
  public readonly int ObjectTypeID;
  [CanBeNull]
  private readonly IMSAttribute4ObjectType _descriptor4ObjectType;

  public SystemAttribute4ObjectType([NotNull] SystemAttribute attribute, [NotEmpty] int objectTypeID, bool obligatory)
    : base(attribute)
  {
    this.ObjectTypeID = objectTypeID;
    this._Found = new bool?(attribute.Found && !Intermech.Check.ObjectTypeIdIsEmpty(objectTypeID));
    if (this.Found)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelperService.Instance.GetAttribute4ObjectType(objectTypeID, attribute.ID);
      this._Found = new bool?(attribute4ObjectType != null);
      if (this.Found)
        this._descriptor4ObjectType = attribute4ObjectType;
    }
    if (!obligatory || this.Found)
      return;
    if (!Intermech.Check.ObjectTypeIdIsEmpty(this.ObjectTypeID))
      throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден у типа объекта {MetaDataHelperService.Instance.GetObjectTypeName(this.ObjectTypeID)}!");
    throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден!");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IMSAttribute4ObjectType GetDescriptor4ObjectType()
  {
    if (this.Found)
      return this._descriptor4ObjectType;
    if (!Intermech.Check.ObjectTypeIdIsEmpty(this.ObjectTypeID))
      throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден у типа объекта {MetaDataHelperService.Instance.GetObjectTypeName(this.ObjectTypeID)}!");
    throw new AttributeWithGuidNotFoundException(this.Guid, $"{this.FullPropertyName}: Атрибут с Guid={this.Guid} не найден!");
  }

  [NotNull]
  public IMSAttribute4ObjectType Descriptor4ObjectType
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDescriptor4ObjectType();
  }
}
