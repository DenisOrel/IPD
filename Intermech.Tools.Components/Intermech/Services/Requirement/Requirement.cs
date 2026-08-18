// Decompiled with JetBrains decompiler
// Type: Intermech.Services.Requirement.Requirement
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Services.Requirement;

internal sealed class Requirement
{
  public string Text { get; set; }

  public string[] Refs { get; set; }

  public int Index { get; set; }

  public string Guid { get; set; }
}
