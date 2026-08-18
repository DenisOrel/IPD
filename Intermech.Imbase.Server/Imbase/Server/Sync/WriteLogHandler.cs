// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.WriteLogHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal delegate void WriteLogHandler(Guid taskGuid, EventType type, string eventText);
