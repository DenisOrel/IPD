// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.GuidPair
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Attribute GUID and ObjectTypeGUID - used as HashTable key
/// </summary>
[Serializable]
public class GuidPair : ICloneable
{
  public Guid attrGUID = Guid.Empty;
  public Guid objTypeGUID = Guid.Empty;

  public GuidPair(string att, string obj)
  {
    this.attrGUID = new Guid(att);
    if (obj == null || obj == "")
      this.objTypeGUID = Guid.Empty;
    else
      this.objTypeGUID = new Guid(obj);
  }

  public GuidPair(string att) => this.attrGUID = new Guid(att);

  public GuidPair(Guid att, Guid obj)
  {
    this.attrGUID = att;
    this.objTypeGUID = obj;
  }

  public GuidPair(Guid att) => this.attrGUID = att;

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (!(obj.GetType() == typeof (GuidPair)))
      return base.Equals(obj);
    return this.attrGUID.Equals(((GuidPair) obj).attrGUID) && this.objTypeGUID.Equals(((GuidPair) obj).objTypeGUID);
  }

  public override string ToString() => $"AttrID={this.attrGUID}, ObjTypeId={this.objTypeGUID}";

  public override int GetHashCode() => this.attrGUID.GetHashCode() ^ this.objTypeGUID.GetHashCode();

  public object Clone() => (object) new GuidPair(this.attrGUID, this.objTypeGUID);
}
