// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.IndexingData
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Kernel;

#nullable disable
namespace Intermech.Imbase.Server;

internal struct IndexingData
{
  public UserSession session;
  public int version;
  public bool needUpdateVersion;
}
