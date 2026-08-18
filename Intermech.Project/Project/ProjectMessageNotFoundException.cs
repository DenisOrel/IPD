// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectMessageNotFoundException
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
public class ProjectMessageNotFoundException : 
  ObjectVersionNotFoundException,
  ISerializable,
  IObjectException
{
  public ProjectMessageNotFoundException([NotEmpty] long messageID, [CanBeNull] string customMessage = null)
    : base(messageID, customMessage)
  {
  }

  public ProjectMessageNotFoundException([NotEmpty] Guid messageGuid, [CanBeNull] string customMessage = null)
    : base(messageGuid, customMessage)
  {
  }

  protected ProjectMessageNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [CanBeEmpty]
  public long MessageID => this.ObjectVersionID;

  [CanBeEmpty]
  public Guid MessageGuid => this.ObjectVersionGuid;

  [SpecialName]
  long IObjectException.get_ObjectID() => this.ObjectID;
}
