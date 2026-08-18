// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.MutableCollection`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
public class MutableCollection<T> : 
  ObservableCollection<T>,
  INotifyCollectionChanged,
  INotifyPropertyChanged,
  IList<T>,
  ICollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IList,
  ICollection
{
  private int _ignoreChangedCounter;

  public MutableCollection()
  {
  }

  public MutableCollection([NotNull] IEnumerable<T> collection)
    : base(Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<T>>(collection, nameof (collection)))
  {
  }

  public MutableCollection([NotNull] List<T> list)
    : base(Intermech.Diagnostics.Check.ArgumentNotNull<List<T>>(list, nameof (list)))
  {
  }

  protected MutableCollection([NotNull] SerializationInfo info, StreamingContext context)
  {
    T[] objArray = info.GetValue<T[]>("Items");
    if (objArray != null && objArray.Length != 0)
    {
      this.StartCollectionQuietCollectionChange();
      try
      {
        foreach (T obj in objArray)
          this.Add(obj);
      }
      finally
      {
        this.FinishCollectionQuietCollectionChange();
      }
    }
    this.WasChanged = info.GetBoolean(nameof (WasChanged));
  }

  public virtual void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Items", (object) this.ToArray<T>(this.Count), typeof (T[]));
    info.AddValue("WasChanged", this.WasChanged);
  }

  protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
  {
    if (this._ignoreChangedCounter != 0)
      return;
    if (!this.WasChanged)
      this.OnCollectionChangedFirstTime(e);
    base.OnCollectionChanged(e);
  }

  public event NotifyCollectionChangedEventHandler CollectionChangedFirstTime;

  protected virtual void OnCollectionChangedFirstTime([NotNull] NotifyCollectionChangedEventArgs e)
  {
    this.WasChanged = true;
    NotifyCollectionChangedEventHandler changedFirstTime = this.CollectionChangedFirstTime;
    if (changedFirstTime == null)
      return;
    changedFirstTime((object) this, e);
  }

  public bool WasChanged { get; set; }

  public void StartCollectionQuietCollectionChange() => ++this._ignoreChangedCounter;

  public void FinishCollectionQuietCollectionChange()
  {
    if (this._ignoreChangedCounter <= 0)
      return;
    --this._ignoreChangedCounter;
  }
}
