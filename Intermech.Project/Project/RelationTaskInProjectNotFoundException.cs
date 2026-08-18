// Decompiled with JetBrains decompiler
// Type: Intermech.Project.RelationTaskInProjectNotFoundException
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class RelationTaskInProjectNotFoundException : RelationNotFoundException, ISerializable
{
  public RelationTaskInProjectNotFoundException()
    : this(new RelationNotFoundException.Params())
  {
  }

  public RelationTaskInProjectNotFoundException([NotEmpty] long relationID, [CanBeNull] string customMessage = null)
    : this(new RelationNotFoundException.Params(new long?(relationID)), customMessage)
  {
  }

  public RelationTaskInProjectNotFoundException([NotEmpty] Guid relationGuid, [CanBeNull] string customMessage = null)
    : this(new RelationNotFoundException.Params(guid: new Guid?(relationGuid)), customMessage)
  {
  }

  public RelationTaskInProjectNotFoundException([NotNull] string relationName, [CanBeNull] string customMessage)
    : this(new RelationNotFoundException.Params(name: relationName), customMessage)
  {
  }

  public RelationTaskInProjectNotFoundException(
    in RelationNotFoundException.Params relationParams,
    [CanBeNull] string customMessage = null)
    : base(in relationParams, customMessage)
  {
  }

  protected RelationTaskInProjectNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
