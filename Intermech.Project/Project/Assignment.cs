// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Assignment
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Assignment : Entity
{
  private double _maxUnits;
  [CanBeNull]
  [NotNullAfter("Load")]
  [NotNullAfter("Resource")]
  private Resource _resource;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  [CanBeNull]
  [NonSerialized]
  internal Task _Task;
  private double _units;
  internal long _PrevRelationID;
  [NonSerialized]
  protected long _RelationID;
  private bool _isChief;

  private Assignment()
  {
    this._units = 1.0;
    this._maxUnits = 1.0;
  }

  public Assignment([NotNull] Task task)
    : this()
  {
    this._Task = task;
  }

  public Assignment([NotNull] Resource resource)
    : this()
  {
    this.Resource = resource;
  }

  public Assignment([NotNull] Resource resource, double units)
    : this(resource)
  {
    this.Units = units;
  }

  public Assignment([NotNull] Resource resource, double units, double maxUnits)
    : this(resource, units)
  {
    this.MaxUnits = maxUnits;
  }

  public virtual void Delete()
  {
    if (this.Task == null)
      return;
    this.Task.Assignments.Remove(this);
  }

  public virtual double MaxUnits
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._maxUnits;
    set
    {
      if (value < this.Units)
        throw new ArgumentOutOfRangeException(nameof (MaxUnits), "MaxUnits value must be greater than or equal to Units.");
      this.OnPropertyChanging(nameof (MaxUnits));
      this._maxUnits = value;
      this.OnPropertyChanged(nameof (MaxUnits));
      this.OnPropertyChangeCompleted(nameof (MaxUnits));
    }
  }

  [CanBeNull]
  public virtual Resource Resource
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._resource;
    set
    {
      Resource resource = this._resource;
      if ((resource != null ? (!resource.Equals((object) value) ? 1 : 0) : 1) == 0)
        return;
      this.OnPropertyChanging(nameof (Resource));
      if (this._RelationID != 0L)
        this._PrevRelationID = this._RelationID;
      this._RelationID = 0L;
      this._resource = value;
      this.OnPropertyChanged(nameof (Resource));
      this.OnPropertyChangeCompleted(nameof (Resource));
    }
  }

  [CanBeNull]
  public virtual object Tag
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._tag;
    set
    {
      if (value == this.Tag)
        return;
      this.OnPropertyChanging(nameof (Tag));
      this._tag = value;
      this.OnPropertyChanged(nameof (Tag));
      this.OnPropertyChangeCompleted(nameof (Tag));
    }
  }

  [CanBeNull]
  public virtual Task Task
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Task;
    set
    {
      if (value == this.Task)
        return;
      if (value != null && this.Resource == null)
        throw new ArgumentException("Cannot set task: resource must be set prior to setting task.", nameof (Task));
      if (this.Task != null)
        this.Task.Assignments.Remove(this);
      this._Task = value;
      if (this.Task == null)
        return;
      this.Task.Assignments.Add(this);
    }
  }

  public virtual double Units
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._units;
    set
    {
      if (this._units == value || value < 0.0)
        return;
      this.OnPropertyChanging(nameof (Units));
      this._units = value;
      this.OnPropertyChanged(nameof (Units));
      this.OnPropertyChangeCompleted(nameof (Units));
      this.MaxUnits = this.Units;
    }
  }

  [NotNull]
  public string UnitsString
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Convert.ToString(this.Units * 100.0, (IFormatProvider) CultureInfo.CurrentCulture) + "%";
    }
    set
    {
      value = value.Replace("%", string.Empty).Trim();
      double result;
      double.TryParse(value, out result);
      this.Units = result / 100.0;
    }
  }

  public long RelationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._RelationID;
  }

  internal long HackRelationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._RelationID = 0L;
    }
  }

  public void Load([NotNull] DataRow row)
  {
    this._RelationID = Convert.ToInt64(row[2]);
    long calendarID = 0;
    if (!DBNull.Value.Equals(row[7]))
      calendarID = Convert.ToInt64(row[7]);
    this._resource = new Resource((ISessionProvider) this.Task, Convert.ToInt64(row[1]), row[3]?.ToString() ?? string.Empty, Convert.ToInt32(row[4]), calendarID);
    if (!DBNull.Value.Equals(row[5]))
      this.Units = Convert.ToDouble(row[5]);
    this._isChief = !DBNull.Value.Equals(row[6]) && Convert.ToBoolean(row[6]);
  }

  internal bool JustCreated { get; private set; }

  internal void Save([NotNull] IUserSession session, [NotEmpty] long projectID)
  {
    if (this._resource == null)
      return;
    if (this._PrevRelationID != 0L)
      this.DeleteRelation(this._PrevRelationID, session);
    this.JustCreated = false;
    IDBRelation relation;
    if (this._RelationID == 0L)
    {
      relation = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.Resources).Create(projectID, this._resource.ObjectID);
      this._RelationID = relation.RelationID;
      this.JustCreated = true;
    }
    else
      relation = session.GetRelation(this._RelationID);
    relation.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ResourceUnits).AsDouble = this.Units;
    IDBAttribute attributeById = relation.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ResourceIsChief);
    if (attributeById != null)
    {
      attributeById.AsBoolean = this.IsChief;
    }
    else
    {
      if (!this.IsChief)
        return;
      relation.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.ResourceIsChief, false, new object[1]
      {
        (object) this.IsChief
      });
    }
  }

  public bool IsUser
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Resource != null && this.Resource.ObjectType == (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User;
    }
  }

  internal void DeleteRelation([NotEmpty] long relationID, [NotNull] IUserSession session)
  {
    if (relationID == 0L)
      return;
    session.GetRelation(relationID, false)?.Delete(0L);
  }

  public bool IsChief
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._isChief;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      if (this._isChief == value)
        return;
      this._isChief = value;
      this.OnPropertyChanged(nameof (IsChief));
    }
  }

  internal long ResourceObjectID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Resource resource = this.Resource;
      return resource == null ? 0L : resource.ObjectID;
    }
  }
}
