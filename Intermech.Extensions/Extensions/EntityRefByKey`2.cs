// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EntityRefByKey`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Extensions;

public class EntityRefByKey<TKey, TEntity> : EntityRefByKeyBase<TKey, TEntity>
  where TKey : struct
  where TEntity : class
{
  [NotNull]
  private readonly Func<TKey, TEntity> _getEntityByID;

  protected EntityRefByKey(
    TKey key,
    [NotNull] Func<TKey, TEntity> getEntityByID,
    [CanBeNull] Action<TKey> actionIfNotFound = null)
    : base(key, actionIfNotFound)
  {
    this._getEntityByID = getEntityByID;
  }

  [CanBeNull]
  protected override TEntity GetEntityByKey() => this._getEntityByID(this.Key);
}
