// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.AttribPair
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Expert;

/// <summary>Attribute ID and ObjectTypeId - used as HashTable key</summary>
[Serializable]
public class AttribPair : ICloneable
{
  public int attribID = -1;
  public int objTypeID = -1;

  public AttribPair(int att, int obj)
  {
    this.attribID = att;
    this.objTypeID = obj;
  }

  public AttribPair(int att) => this.attribID = att;

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (!(obj.GetType() == typeof (AttribPair)))
      return base.Equals(obj);
    return this.attribID == ((AttribPair) obj).attribID && this.objTypeID == ((AttribPair) obj).objTypeID;
  }

  public override string ToString() => $"AttrID={this.attribID}, ObjTypeId={this.objTypeID}";

  public override int GetHashCode() => this.attribID ^ this.objTypeID;

  public object Clone() => (object) new AttribPair(this.attribID, this.objTypeID);
}
