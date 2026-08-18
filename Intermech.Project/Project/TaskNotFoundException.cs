// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskNotFoundException
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class TaskNotFoundException : ObjectVersionNotFoundException, ISerializable, IObjectException
{
  public TaskNotFoundException([NotEmpty] long taskID, [CanBeNull] string customMessage = null)
    : base(taskID, customMessage)
  {
  }

  public TaskNotFoundException([NotEmpty] Guid taskGuid, [CanBeNull] string customMessage = null)
    : base(taskGuid, customMessage)
  {
  }

  protected TaskNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [CanBeEmpty]
  public long TaskID => this.ObjectVersionID;

  [CanBeEmpty]
  public Guid TaskGuid => this.ObjectVersionGuid;

  [SpecialName]
  long IObjectException.get_ObjectID() => this.ObjectID;
}
