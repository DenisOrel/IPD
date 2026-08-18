// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectNotFoundException
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
public class ProjectNotFoundException : 
  ObjectVersionNotFoundException,
  ISerializable,
  IObjectException
{
  public ProjectNotFoundException([NotEmpty] long projectID, [CanBeNull] string customMessage = null)
    : base(projectID, customMessage)
  {
  }

  public ProjectNotFoundException([NotEmpty] Guid projectGuid, [CanBeNull] string customMessage = null)
    : base(projectGuid, customMessage)
  {
  }

  protected ProjectNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [CanBeEmpty]
  public long ProjectID => this.ObjectVersionID;

  [CanBeEmpty]
  public Guid ProjectGuid => this.ObjectVersionGuid;

  [SpecialName]
  long IObjectException.get_ObjectID() => this.ObjectID;
}
