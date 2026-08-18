// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ESFolderInfo
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using System.Collections.Concurrent;
using System.Linq;

#nullable disable
namespace Intermech.Expert.Server;

internal class ESFolderInfo
{
  private long folderId;
  private string Name;
  private TempFormula cond;
  private ConcurrentBag<long> parentFolders;

  internal ESFolderInfo(long fId, string Name, TempFormula f)
  {
    this.folderId = fId;
    this.Name = Name;
    this.cond = f;
  }

  internal void AddParentFolder(long folderId)
  {
    if (this.parentFolders == null)
      this.parentFolders = new ConcurrentBag<long>();
    if (this.parentFolders.Contains<long>(folderId))
      return;
    this.parentFolders.Add(folderId);
  }

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals(object obj) => base.Equals(obj);

  public override string ToString() => this.Name;

  public ConcurrentBag<long> ParentFolders => this.parentFolders;

  public TempFormula Cond => this.cond;

  public void CopyParents(ESFolderInfo other) => this.parentFolders = other.parentFolders;
}
