// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.OnlyArticles4DocumentFinder
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class OnlyArticles4DocumentFinder(ArticleSrvService articleSrvService) : 
  Articles4DocumentFinder<long>(articleSrvService)
{
  protected override long GetLastKeyValue(long lastItem) => lastItem;

  protected override long GetResultItem(long objectID, long relationID) => objectID;
}
