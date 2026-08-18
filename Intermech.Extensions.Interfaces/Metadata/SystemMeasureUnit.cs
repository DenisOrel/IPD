// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemMeasureUnit
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public sealed class SystemMeasureUnit : IpsMetadataObject
{
  [CanBeNull]
  private MeasureDescriptor _descriptor;

  public SystemMeasureUnit(
    [NotEmpty] long measureID,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(measureID, guid, holderType, obligatory, idPropertyName)
  {
    this._Found = new bool?(true);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private MeasureDescriptor GetDescriptor()
  {
    if (!this.Found)
      throw new MeasureNotFoundException(this.Guid, $"{this.FullPropertyName}: Единица измерения с Guid={this.Guid} не найдена!");
    if (this._descriptor == null)
      this._descriptor = MeasureHelper.Instance.FindDescriptor(this.ID);
    return this._descriptor;
  }

  [NotNull]
  public MeasureDescriptor Descriptor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDescriptor();
  }

  public double K
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.K;
  }

  public long PhysicalQuantityID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.PhysicalQuantityID;
  }

  [NotNull]
  public string LongName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.LongName;
  }

  [NotNull]
  public string ShortName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.ShortName;
  }

  public bool IsDefault
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.IsDefault;
  }
}
