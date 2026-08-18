// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectGraphVertex
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class DBObjectGraphVertex : IEquatable<DBObjectGraphVertex>, IDBObjectGraphTraitOwner
{
  private long objectId;
  private int objectTypeId;
  private string caption;
  private DBObjectGraphTraitCollection traits;
  private DeferredEventCollection deferredEvents;
  private CopyingSelector copyingSelector;
  private bool isScanned;
  private DBObjectAttributeCollection attributes;
  private DBObjectFileCollection files;
  private DBObjectContent content;

  public DBObjectGraphVertex(long objectId, int objectTypeId, string caption)
  {
    if (Consts.IsUndefinedObjectId(objectId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (objectTypeId));
    if (caption == null)
      throw new ArgumentNullException("Не задан заголовок объекта IPS.", nameof (caption));
    this.objectId = objectId;
    this.objectTypeId = objectTypeId;
    this.caption = caption;
    this.traits = new DBObjectGraphTraitCollection((IDBObjectGraphTraitOwner) this);
    this.deferredEvents = new DeferredEventCollection();
    this.isScanned = false;
    this.copyingSelector = new CopyingSelector();
    this.copyingSelector.IsSelectedChanged += new EventHandler(this.OnIsSelectedChanged);
    this.attributes = new DBObjectAttributeCollection();
    this.attributes.CollectionItemChanged += new EventHandler(this.OnAttributeValueChanged);
    this.files = new DBObjectFileCollection();
    this.content = (DBObjectContent) DBObjectEmptyContent.Instance;
  }

  public long ObjectId
  {
    [DebuggerStepThrough] get => this.objectId;
  }

  public int ObjectTypeId
  {
    [DebuggerStepThrough] get => this.objectTypeId;
  }

  public string Caption
  {
    [DebuggerStepThrough] get => this.caption;
  }

  public DBObjectGraphTraitCollection Traits
  {
    [DebuggerStepThrough] get => this.traits;
  }

  public DeferredEventCollection DeferredEvents
  {
    [DebuggerStepThrough] get => this.deferredEvents;
  }

  public CopyingSelector CopyingSelector
  {
    [DebuggerStepThrough] get => this.copyingSelector;
  }

  public bool IsScanned
  {
    [DebuggerStepThrough] get => this.isScanned;
    [DebuggerStepThrough] set => this.isScanned = value;
  }

  public IList<DBObjectAttributeEntry> Attributes
  {
    [DebuggerStepThrough] get => (IList<DBObjectAttributeEntry>) this.attributes;
  }

  public IList<DBObjectFileEntry> Files
  {
    [DebuggerStepThrough] get => (IList<DBObjectFileEntry>) this.files;
  }

  public DBObjectContent Content
  {
    [DebuggerStepThrough] get => this.content;
    set => this.content = value != null ? value : throw new ArgumentNullException(nameof (value));
  }

  private void OnIsSelectedChanged(object sender, EventArgs e)
  {
    this.DeferredEvents.Add((DeferredEvent) new DBObjectReselectedDeferredEvent(this), (Predicate<DeferredEvent>) (x => x is DBObjectReselectedDeferredEvent reselectedDeferredEvent && reselectedDeferredEvent.DBObjectVertex != this));
  }

  private void OnAttributeValueChanged(object sender, EventArgs e)
  {
    this.DeferredEvents.Add((DeferredEvent) new DBObjectAttributesChangedDeferredEvent(this), (Predicate<DeferredEvent>) (x => x is DBObjectAttributesChangedDeferredEvent changedDeferredEvent && changedDeferredEvent.DBObjectVertex != this));
  }

  public bool Equals(DBObjectGraphVertex other) => other != null && other.ObjectId == this.ObjectId;

  public override bool Equals(object obj)
  {
    return !(obj is DBObjectGraphVertex other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => this.ObjectId.GetHashCode();
}
