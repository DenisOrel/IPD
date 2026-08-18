// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DependencyNotFoundException
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
public class DependencyNotFoundException : 
  ObjectVersionNotFoundException,
  ISerializable,
  IObjectException
{
  public DependencyNotFoundException([NotEmpty] long dependencyID, [CanBeNull] string customMessage = null)
    : base(dependencyID, customMessage)
  {
  }

  public DependencyNotFoundException([NotEmpty] Guid dependencyGuid, [CanBeNull] string customMessage = null)
    : base(dependencyGuid, customMessage)
  {
  }

  protected DependencyNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [CanBeEmpty]
  public long DependencyID => this.ObjectVersionID;

  [CanBeEmpty]
  public Guid DependencyGuid => this.ObjectVersionGuid;

  [SpecialName]
  long IObjectException.get_ObjectID() => this.ObjectID;
}
