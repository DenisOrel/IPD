// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevisionComplectRelation
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

internal class RevisionComplectRelation(UserSession uSession, DataTable relationsTable) : DBRelation(uSession, relationsTable)
{
  protected override int DoDelete(long DeleteMode) => base.DoDelete(DeleteMode);
}
