// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SpecRelation
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// 
/// </summary>
public class SpecRelation
{
  private long projectId;
  private Guid relationGuid;

  /// <summary>
  /// 
  /// </summary>
  public SpecRelation() => this.projectId = 0L;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="projectId"></param>
  /// <param name="relationGuid"></param>
  public SpecRelation(long projectId, Guid relationGuid)
  {
    this.projectId = projectId;
    this.relationGuid = relationGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  public long ProjectId
  {
    get => this.projectId;
    set => this.projectId = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор связи в базе данных.
  /// </summary>
  public Guid RelationGuid
  {
    get => this.relationGuid;
    set => this.relationGuid = value;
  }
}
