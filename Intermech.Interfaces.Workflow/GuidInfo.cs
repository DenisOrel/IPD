// Decompiled with JetBrains decompiler
// Type: Intermech.GuidInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech;

public class GuidInfo
{
  public readonly Guid Guid;
  public readonly string Name;

  public GuidInfo(Guid guid, string name)
  {
    this.Guid = guid;
    this.Name = name;
  }

  public override string ToString() => this.Name.ToString();

  public override bool Equals(object obj)
  {
    if (!(obj is GuidInfo guidInfo))
      return base.Equals(obj);
    return guidInfo.Guid == this.Guid && guidInfo.Name == this.Name;
  }

  public override int GetHashCode() => (this.Guid.ToString() + this.Name).GetHashCode();
}
