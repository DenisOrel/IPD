// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EntityRefByKeyBase`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Common;
using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Extensions;

[DebuggerDisplay("Key={Key}")]
[Serializable]
public abstract class EntityRefByKeyBase<TKey, TEntity> : 
  IEntityRefByKeyBase<TKey, TEntity>,
  ISerializable
  where TKey : struct
  where TEntity : class
{
  [CanBeNull]
  private readonly Action<TKey> _actionIfNotFound;
  private bool _loaded;
  [CanBeNull]
  private TEntity _entity;

  [CanBeEmpty]
  public TKey Key { get; }

  protected EntityRefByKeyBase(TKey key, [CanBeNull] Action<TKey> actionIfNotFound = null)
  {
    this.Key = key;
    this._actionIfNotFound = actionIfNotFound;
  }

  protected EntityRefByKeyBase([NotNull] SerializationInfo info, StreamingContext context)
  {
    this.Key = info.GetValue<TKey>(nameof (Key));
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Key", (object) this.Key);
  }

  [CanBeNull]
  public TEntity Entity
  {
    get
    {
      if (this._loaded)
        return this._entity;
      if (!this.KeyIsEmpty)
      {
        this._entity = this.GetEntityByKey();
        if ((object) this._entity != null)
        {
          if (this._entity is WrapperBase<TEntity> entity)
            this._entity = entity.WrappedObject;
        }
        else
        {
          Action<TKey> actionIfNotFound = this._actionIfNotFound;
          if (actionIfNotFound != null)
            actionIfNotFound(this.Key);
        }
      }
      this._loaded = true;
      return this._entity;
    }
  }

  [CanBeNull]
  protected abstract TEntity GetEntityByKey();

  protected virtual bool KeyIsEmpty => false;
}
