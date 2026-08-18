// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.IFormsCacheSynchronizer
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces.Server;

#nullable disable
namespace Intermech.FormDesigner.Server;

public interface IFormsCacheSynchronizer
{
  void AddEvent(string strInfo, IDbManager db);
}
