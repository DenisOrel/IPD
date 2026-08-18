// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.VersionCacheItem
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class VersionCacheItem
{
  public readonly long ID;
  public readonly long RelationID;

  public VersionCacheItem(long id, long relationID)
  {
    this.ID = id;
    this.RelationID = relationID;
  }

  public override int GetHashCode()
  {
    long num = this.ID;
    int hashCode1 = num.GetHashCode();
    num = this.RelationID;
    int hashCode2 = num.GetHashCode();
    return hashCode1 & hashCode2;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is VersionCacheItem versionCacheItem))
      return base.Equals(obj);
    return this.ID.Equals(versionCacheItem.ID) && this.RelationID.Equals(versionCacheItem.RelationID);
  }
}
