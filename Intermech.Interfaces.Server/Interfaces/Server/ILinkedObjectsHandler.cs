// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ILinkedObjectsHandler
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface ILinkedObjectsHandler
{
  List<int> HandleTypes { get; }

  List<int> OutputTypes { get; }

  bool IsTypesChanged(IUserSession session);

  void UpdateHandleAndOutputTypes(IUserSession session, bool force);

  List<LinkedObject> Handle(
    IUserSession session,
    long objectID,
    int objectType,
    string filtrationOwnerID);

  string Name { get; }
}
