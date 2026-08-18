// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.BeforeCreateRelationHandler
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public delegate void BeforeCreateRelationHandler(
  IDBObject parentObject,
  long childID,
  DateTime beginDate,
  long prjlinkID,
  IUserSession session,
  IDBRelationCollection relations,
  DataTable versionsTable);
