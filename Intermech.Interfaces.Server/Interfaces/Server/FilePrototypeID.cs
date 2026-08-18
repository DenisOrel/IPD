// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.FilePrototypeID
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

[Serializable]
public class FilePrototypeID
{
  public int AttributeID { get; set; }

  public int ObjectTypeID { get; set; }

  public long UserID { get; set; }

  public FilePrototypeID(int attributeID, int objectTypeID)
  {
    this.AttributeID = attributeID;
    this.ObjectTypeID = objectTypeID;
    this.UserID = 0L;
  }

  public FilePrototypeID(int attributeID, int objectTypeID, long userID)
  {
    this.AttributeID = attributeID;
    this.ObjectTypeID = objectTypeID;
    this.UserID = userID;
  }

  public override int GetHashCode() => this.AttributeID ^ this.ObjectTypeID ^ (int) this.UserID;

  public override bool Equals(object obj)
  {
    if (!(obj is FilePrototypeID))
      return false;
    FilePrototypeID filePrototypeId = (FilePrototypeID) obj;
    return filePrototypeId.GetHashCode() == this.GetHashCode() && filePrototypeId.AttributeID == this.AttributeID && filePrototypeId.ObjectTypeID == this.ObjectTypeID;
  }
}
