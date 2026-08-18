// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.DbConfidentialResolution
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Localization;
using Intermech.Kernel;
using Intermech.Office.Interfaces;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

internal class DbConfidentialResolution([NotNull] UserSession uSession, [NotNull] DataTable objectParams) : 
  DBResolution(uSession, objectParams),
  IDBResolution,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBGuid,
  IDeletable,
  IDBLifecycleLevel,
  IDBSecurityCollection,
  IDBSecurity,
  IDBLocalizable
{
}
