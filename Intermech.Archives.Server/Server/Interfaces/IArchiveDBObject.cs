// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.Interfaces.IArchiveDBObject
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Archives.Server.Interfaces;

public interface IArchiveDBObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  IDBObject ArchivedObject { get; set; }

  IDBSecurity AccessChecker { get; }
}
