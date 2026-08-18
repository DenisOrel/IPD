// Decompiled with JetBrains decompiler
// Type: Intermech.IDInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech;

public class IDInfo
{
  public readonly long ID;
  public readonly string Name;

  public IDInfo(long ID, string Name)
  {
    this.ID = ID;
    this.Name = Name;
  }

  public override string ToString() => this.Name.ToString();

  public override bool Equals(object obj)
  {
    if (!(obj is IDInfo idInfo))
      return base.Equals(obj);
    return idInfo.ID == this.ID && idInfo.Name == this.Name;
  }

  public override int GetHashCode() => (this.ID.ToString() + this.Name).GetHashCode();
}
