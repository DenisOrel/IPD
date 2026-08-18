// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.IExpertGlobalTable
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces.Expert;

#nullable disable
namespace Intermech.Expert.Server;

public interface IExpertGlobalTable
{
  HybridTableExp Objects { get; }

  HybridTableExp Relations { get; }

  int ObjectIndex(long objId);

  int RelIndex(long relId);

  int ObjByPartIndex(long partId);

  HybridRowExp SavedDataByObjId(long objId);

  HybridRowExp SavedDataByPartId(long partId);

  HybridRowExp[] SavedLinksByProjId(long projId);

  HybridRowExp[] SavedLinksByPartId(long partId);
}
