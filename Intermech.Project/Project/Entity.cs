// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Entity
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Entity : INotifyPropertyChanged, IDeserializationCallback
{
  [NonSerialized]
  private bool _raisePropertyChangedEvents;
  private static long _globalUpdateCounter;
  private bool _modified;
  private int _index = -1;

  [field: NonSerialized]
  public event PropertyChangedEventHandler PropertyChangeCompleted;

  [field: NonSerialized]
  public event PropertyChangedEventHandler PropertyChanged;

  [field: NonSerialized]
  public event PropertyChangedEventHandler PropertyChanging;

  protected Entity()
  {
    this.UseCache = true;
    this.Initialize();
  }

  protected virtual void Initialize() => this._raisePropertyChangedEvents = true;

  public void OnDeserialization([CanBeNull] object sender) => this.Initialize();

  protected internal virtual void OnPropertyChangeCompleted([NotNull, NotEmpty] string property)
  {
    if (!this.RaisePropertyChangedEvents)
      return;
    PropertyChangedEventHandler propertyChangeCompleted = this.PropertyChangeCompleted;
    if (propertyChangeCompleted == null)
      return;
    propertyChangeCompleted((object) this, new PropertyChangedEventArgs(property));
  }

  public static void GlobalBeginUpdate() => ++Entity._globalUpdateCounter;

  public static void GlobalEndUpdate() => --Entity._globalUpdateCounter;

  public static bool InGlobalUpdate => Entity._globalUpdateCounter != 0L;

  internal void FirePropertyChanged([NotNull, NotEmpty] string property)
  {
    if (!this.RaisePropertyChangedEvents)
      return;
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(property));
  }

  internal virtual void OnPropertyChanged([NotNull, NotWhitespace] string property, bool triggerModified)
  {
    if (Entity.InGlobalUpdate)
      return;
    if (triggerModified)
      this.Modified = true;
    this.FirePropertyChanged(property);
  }

  [NotifyPropertyChangedInvocator]
  internal void OnPropertyChanged([NotNull, NotEmpty] string property)
  {
    this.OnPropertyChanged(property, true);
  }

  protected internal void OnPropertyChanging([NotNull, NotEmpty] string property)
  {
    if (!this.RaisePropertyChangedEvents)
      return;
    PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
    if (propertyChanging == null)
      return;
    propertyChanging((object) this, new PropertyChangedEventArgs(property));
  }

  public void ResetBindings()
  {
    foreach (MemberInfo property in this.GetType().GetProperties())
      this.OnPropertyChanged(property.Name, false);
  }

  public bool HasPropertyChangedSubscribers => this.PropertyChanged != null;

  public bool RaisePropertyChangedEvents
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._raisePropertyChangedEvents;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      if (value == this.RaisePropertyChangedEvents)
        return;
      this._raisePropertyChangedEvents = value;
    }
  }

  public bool UseCache { get; set; }

  protected virtual void SetModified(bool value)
  {
    if (this._modified == value)
      return;
    this._modified = value;
    EventHandler modifiedChanged = this.ModifiedChanged;
    if (modifiedChanged == null)
      return;
    modifiedChanged((object) this, (EventArgs) null);
  }

  public virtual bool Modified
  {
    [DebuggerStepThrough] get => this._modified;
    set => this.SetModified(value);
  }

  [field: NonSerialized]
  public event EventHandler ModifiedChanged;

  /// <summary>Заполняется родительской коллекцией, если у неё включено CalcIndexes</summary>
  public int Index
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._index;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] protected internal set
    {
      this._index = value;
    }
  }
}
