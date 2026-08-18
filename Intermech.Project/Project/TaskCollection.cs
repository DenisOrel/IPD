// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class TaskCollection : Collection<Task>, ISerializable
{
  public TaskCollection()
    : this(false)
  {
  }

  public TaskCollection(bool calcIndexes)
    : base(calcIndexes)
  {
  }

  protected TaskCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    this.EntityType = info.GetType("EntityType");
    this.AddRange((IEnumerable<Task>) info.GetValue<Task[]>("Items"));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    string str = this.EntityType.Assembly.FullName;
    int length = str.IndexOf(',');
    if (length >= 0)
      str = str.Substring(0, length);
    info.AddValue("EntityType", (object) $"{this.EntityType.FullName}, {str}");
    info.AddValue("Items", (object) this.ToArray<Task>(this.Count), typeof (Task[]));
  }

  [CanBeNull]
  public Task FindByObjectID(long id)
  {
    id = Math.Abs(id);
    return this.FirstOrDefault<Task>((Func<Task, bool>) (t => Math.Abs(t.ObjectID) == id));
  }

  [CanBeNull]
  public Task FindByIndexString([NotNull] string indexString)
  {
    return this.FirstOrDefault<Task>((Func<Task, bool>) (t => t.IndexString == indexString));
  }

  [CanBeNull]
  public Task FindByHash(long hash)
  {
    return this.FirstOrDefault<Task>((Func<Task, bool>) (t => (long) t.GetHashCode() == hash));
  }

  public void Assign([NotNull, ItemNotNull] IEnumerable<Task> list)
  {
    this.Clear();
    foreach (Task task in list)
      this.Add(task);
  }

  protected override void AddNewCoreInit([NotNull] Task item)
  {
    base.AddNewCoreInit(item);
    item.Uncommitted = true;
  }
}
