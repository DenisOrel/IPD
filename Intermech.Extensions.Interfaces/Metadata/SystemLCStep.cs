// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemLCStep
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public sealed class SystemLCStep(
  [NotEmpty] Guid guid,
  [NotNull] Type holderType,
  bool obligatory,
  [NotNull, NotWhitespace] string idPropertyName) : IpsMetadataEntityType(guid, holderType, obligatory, idPropertyName), IInitWithSession
{
  [CanBeNull]
  private DBLifecycleStepProperties? _properties;

  [NotNull]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Properties.LCName ?? "Безымянный";
    }
  }

  [NotNull]
  public string Note
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.Note ?? string.Empty;
  }

  public int ObjectTypeID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.ObjectTypeID;
  }

  public int LevelID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.LevelID;
  }

  public bool FirstStep
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.FirstStep;
  }

  public ObjectModifyModes ObjectModifyMode
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Properties.ObjectModifyMode;
  }

  public DBLifecycleStepProperties Properties
  {
    get
    {
      if (!this.Found)
        throw this.LifeCycleStepNotFoundException(this.Guid);
      return this._properties.Value;
    }
  }

  void IInitWithSession.Init([NotNull] IUserSession session)
  {
    IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(this.Guid, false);
    this._Found = new bool?(lifecycleStep != null);
    if (lifecycleStep != null)
    {
      DBLifecycleStepProperties properties = lifecycleStep.Properties;
      this._id = properties.LCStep;
      this._properties = new DBLifecycleStepProperties?(properties);
    }
    else if (this.Obligatory)
      throw this.LifeCycleStepNotFoundException(this.Guid);
  }

  [NotNull]
  private Intermech.Interfaces.LifeCycleStepNotFoundException LifeCycleStepNotFoundException(
    [NotEmpty] Guid guid)
  {
    return new Intermech.Interfaces.LifeCycleStepNotFoundException(this.Guid, $"{this.FullPropertyName}: Шаг ЖЦ с Guid={this.Guid} не найден!");
  }
}
