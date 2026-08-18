// Decompiled with JetBrains decompiler
// Type: Intermech.Services.Requirement.RequirementExternalData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Services.Requirement;

internal sealed class RequirementExternalData
{
  public RequirementExternalData() => this.Requirements = new List<Intermech.Services.Requirement.Requirement>();

  public Guid AnchorGuid { get; set; }

  public List<Intermech.Services.Requirement.Requirement> Requirements { get; set; }
}
