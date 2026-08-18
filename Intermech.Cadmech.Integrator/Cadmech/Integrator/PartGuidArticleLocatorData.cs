// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PartGuidArticleLocatorData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class PartGuidArticleLocatorData : IPartGuidArticleLocatorData
{
  private Guid partGuid;

  public PartGuidArticleLocatorData(Guid partGuid) => this.partGuid = partGuid;

  public Guid GetPartGuid() => this.partGuid;
}
