// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDBRelationCollectionService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDBRelationCollectionService
{
  IDBRelationCollection GetRelationCollection(IUserSession uSession, int relationType);

  IDBRelationCollection GetRelationCollection(
    IUserSession uSession,
    int relationType,
    string FiltrationOwnerID);

  IDBRelationCollection GetRelationCollection(
    IUserSession uSession,
    int relationType,
    VersionsRule rule);
}
