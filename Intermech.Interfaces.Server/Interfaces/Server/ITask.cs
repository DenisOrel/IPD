// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ITask
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface ITask
{
  ITransferedObject[] Units { get; set; }

  long UserID { get; set; }

  TaskPriority Priority { get; set; }

  Exception Error { get; set; }

  long TaskID { get; set; }

  Guid UserGuid { get; }

  TaskStatus Status { get; set; }

  string Name { get; set; }

  TaskType Type { get; set; }

  double Percent { get; set; }

  bool Enabled { get; set; }

  string GetIncludesInfo(IUserSession session);

  int LastStepIDCompleted { get; set; }

  byte[] Save(IUserSession session, IDBObject backupObject);

  void Load(IUserSession session, IDBObject backupObject, byte[] bytes);

  void LoadTransferedObjects(BinaryReader reader);

  void BeginTask(IUserSession session, IEventLogHelper eventHelper);

  event TaskStartEventHandler TaskStartEvent;

  event TaskStepCompletedEventHandler TaskStepCompletedEvent;

  event TaskStatusChangedEventHandler TaskStatusChangedEvent;

  event TaskObjectImportedEventHandler TaskObjectImportedEvent;

  event TaskSaveDataEventHandler TaskSaveDataEvent;

  void OnTaskDelete(Guid connectionGuid, IPortalConnector connector);
}
