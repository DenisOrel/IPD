// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemLCLevel
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public sealed class SystemLCLevel : IpsMetadataEntityType
{
  [CanBeNull]
  private readonly IMSLifeCycleLevel _properties;

  public SystemLCLevel(
    [NotEmpty] Guid guid,
    [CanBeNull] IMSLifeCycleLevel properties,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(guid, holderType, obligatory, idPropertyName)
  {
    this._Found = new bool?(properties != null);
    if (this.Found)
    {
      this._id = properties.LevelID;
      this._properties = properties;
    }
    else if (obligatory)
      throw this.LifeCycleLevelNotFoundException(this.Guid);
  }

  [NotNull]
  public IMSLifeCycleLevel Properties
  {
    get
    {
      return this.Found && this._properties != null ? this._properties : throw this.LifeCycleLevelNotFoundException(this.Guid);
    }
  }

  [NotNull]
  private Intermech.Interfaces.LifeCycleLevelNotFoundException LifeCycleLevelNotFoundException(
    [NotEmpty] Guid guid)
  {
    return new Intermech.Interfaces.LifeCycleLevelNotFoundException(this.Guid, $"{this.FullPropertyName}: Уровень продвижения с Guid={this.Guid} не найден!");
  }

  [NotNull]
  private string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.Name ?? string.Empty;
  }

  [NotNull]
  private string AreaID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Properties.AreaID ?? string.Empty;
    }
  }

  private bool IsDefault
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.Default;
  }

  private bool IsFrozen
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.IsFrozen;
  }
}
