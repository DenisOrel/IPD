// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DependencyCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class DependencyCollection : EnhCollection<Dependency>, ISerializable
{
  private readonly bool _isBackDependencies;

  [CanBeNull]
  public Task Task { get; }

  public DependencyCollection([CanBeNull] Task task, bool isBackDependencies)
  {
    this.Task = task;
    this._isBackDependencies = isBackDependencies;
    if (this._isBackDependencies)
      return;
    this.ItemAdding += new EventHandler<ItemEventArgs<Dependency>>(this.DependencyCollection_ItemAdding);
  }

  protected override void OnItemAdded(Dependency item)
  {
    if (!this._isBackDependencies)
    {
      if (item.DependentOfTask != null && !item.DependentOfTask.BackDependencies.Contains(item))
        item.DependentOfTask.BackDependencies.Add(item);
      if (item.ObjectID != 0L)
        this.Task?.Project?.DeletedDependencies.Remove(item);
    }
    base.OnItemAdded(item);
  }

  protected override void OnItemRemoved(Dependency item)
  {
    if (!this._isBackDependencies)
    {
      if (item.DependentOfTask != null)
        item.DependentOfTask.BackDependencies.Remove(item);
      if (item.ObjectID != 0L)
      {
        Intermech.Project.Project project = this.Task?.Project;
        if (project != null && !project.DeletedDependencies.Contains(item))
          project.DeletedDependencies.Add(item);
      }
    }
    base.OnItemRemoved(item);
  }

  private void DependencyCollection_ItemAdding([CanBeNull] object sender, [NotNull] ItemEventArgs<Dependency> e)
  {
    if (this.FindByIndexString(e.Item.IndexString) != null)
      throw new Exception(Localization.GetString("DoubleDependencyNotAllowed", (object) (e.Item.DependentOfTask?.Name ?? string.Empty), (object) this.Task.Name));
  }

  protected DependencyCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this((Task) null, false)
  {
    this.EntityType = info.GetType("EntityType");
    this.Task = info.GetValue<Task>(nameof (Task));
    foreach (Dependency dependency in info.GetValue<Dependency[]>("Items"))
    {
      dependency._Task = this.Task;
      this.Add(dependency);
    }
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    string str = this.EntityType.Assembly.FullName;
    char ch = ',';
    int length = str.IndexOf(ch);
    if (length >= 0)
      str = str.Substring(0, length);
    info.AddValue("EntityType", (object) $"{this.EntityType.FullName}, {str}");
    Dependency[] dependencyArray = new Dependency[this.Count];
    for (int index = 0; index < this.Count; ++index)
    {
      Dependency dependency = this[index];
      if (dependency.DependentOfTask != null)
      {
        dependency._DependentOfTaskHash = (long) dependency.DependentOfTask.GetHashCode();
        dependencyArray[index] = dependency;
      }
    }
    info.AddValue("Items", (object) dependencyArray);
    info.AddValue("Task", (object) this.Task);
  }

  [CanBeNull]
  public Dependency FindByTask([NotNull] Task depTask)
  {
    return this.FirstOrDefault<Dependency>((Func<Dependency, bool>) (d => d.DependentOfTask == depTask));
  }

  [CanBeNull]
  public Dependency FindByIndexString([NotNull] string indexString)
  {
    return this.FirstOrDefault<Dependency>((Func<Dependency, bool>) (d => d.IndexString == indexString));
  }

  [CanBeNull]
  public Dependency Find([NotNull] Predicate<Dependency> match)
  {
    return this.FirstOrDefault<Dependency>((Func<Dependency, bool>) (d => match(d)));
  }

  internal bool AllSaved { get; private set; } = true;

  internal void Save([NotNull] IUserSession session, bool saveAll)
  {
    this.AllSaved = true;
    if (!this.Where<Dependency>((Func<Dependency, bool>) (d => saveAll || d.ObjectID == 0L)).Any<Dependency>((Func<Dependency, bool>) (d => !d.Save(session))))
      return;
    this.AllSaved = false;
  }

  internal void Commit() => this._Modified = false;

  internal void Rollback()
  {
    foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this)
    {
      if (dependency.JustCreated)
        dependency.HackObjectID = 0L;
    }
  }

  public bool HasExternal => this.Any<Dependency>((Func<Dependency, bool>) (d => d.External));
}
