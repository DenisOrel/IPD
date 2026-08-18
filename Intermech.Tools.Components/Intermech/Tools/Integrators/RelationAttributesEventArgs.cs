// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.RelationAttributesEventArgs
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public class RelationAttributesEventArgs : EventArgs
{
  private SectionEntity project;
  private SectionEntity part;
  private ValueBag relationAtts;

  public RelationAttributesEventArgs(SectionEntity project, SectionEntity part)
  {
    if (project == null)
      throw new ArgumentNullException(nameof (project));
    if (part == null)
      throw new ArgumentNullException(nameof (part));
    this.project = project;
    this.part = part;
    this.relationAtts = new ValueBag();
  }

  public SectionEntity Project => this.project;

  public SectionEntity Part => this.part;

  public ValueBag RelationAttributes => this.relationAtts;
}
