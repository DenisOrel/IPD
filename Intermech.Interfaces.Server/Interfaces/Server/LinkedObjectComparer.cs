// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.LinkedObjectComparer
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class LinkedObjectComparer : IEqualityComparer<LinkedObject>
{
  public bool Equals(LinkedObject x, LinkedObject y)
  {
    return x.ObjectID == y.ObjectID && x.RelationID == y.RelationID;
  }

  public int GetHashCode(LinkedObject obj)
  {
    return (23 * 31 /*0x1F*/ + obj.ObjectID.GetHashCode()) * 31 /*0x1F*/ + obj.RelationID.GetHashCode();
  }
}
