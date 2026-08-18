// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDBTimedEvents
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDBTimedEvents
{
  int AddEvent(TimedEventProperties properties, IDbManager db);

  void DeleteEventID(int eventID, IDbManager db);

  void RegisterService(object timedService);

  int FindEvent(Guid serviceGuid, int intInfo, long objectID, IDbManager db);

  IUserSession GetSystemSessionPermanentClone(string sessionName);

  IUserSession GetSystemSessionTemporaryClone(string sessionName);

  void UnlockEventsQueue();

  void AddToTrace(string message, bool always);
}
