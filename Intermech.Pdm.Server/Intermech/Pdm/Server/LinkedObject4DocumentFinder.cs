// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.LinkedObject4DocumentFinder
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces.Server;

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class LinkedObject4DocumentFinder(ArticleSrvService articleSrvService) : 
  Articles4DocumentFinder<LinkedObject>(articleSrvService)
{
  protected override long GetLastKeyValue(LinkedObject lastItem) => lastItem.ObjectID;

  protected override LinkedObject GetResultItem(long objectID, long relationID)
  {
    return new LinkedObject(objectID, relationID);
  }
}
