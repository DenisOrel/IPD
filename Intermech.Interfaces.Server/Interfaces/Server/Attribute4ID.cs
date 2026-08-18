// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.Attribute4ID
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

[Serializable]
public class Attribute4ID
{
  public int AttributeID { get; set; }

  public int ObjectTypeID { get; set; }

  public int RelationTypeID { get; set; }

  public Attribute4ID(int attributeID)
  {
    this.AttributeID = attributeID;
    this.ObjectTypeID = -1;
    this.RelationTypeID = -1;
  }

  public Attribute4ID(int attributeID, int objectTypeID, int relationTypeID)
  {
    this.AttributeID = attributeID;
    this.ObjectTypeID = objectTypeID;
    this.RelationTypeID = relationTypeID;
  }

  public override int GetHashCode() => this.AttributeID ^ this.ObjectTypeID ^ this.RelationTypeID;

  public override bool Equals(object obj)
  {
    if (!(obj is Attribute4ID))
      return false;
    Attribute4ID attribute4Id = (Attribute4ID) obj;
    return attribute4Id.GetHashCode() == this.GetHashCode() && attribute4Id.AttributeID == this.AttributeID && attribute4Id.ObjectTypeID == this.ObjectTypeID && attribute4Id.RelationTypeID == this.RelationTypeID;
  }
}
