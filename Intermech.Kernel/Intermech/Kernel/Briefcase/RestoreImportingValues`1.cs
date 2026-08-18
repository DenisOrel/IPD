// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RestoreImportingValues`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal abstract class RestoreImportingValues<TCollectionItem>
{
  protected IUserSession session;
  protected List<IDСorresponds> importingObjectIDs;
  protected ImportEventLog eventLog;

  public RestoreImportingValues(
    IUserSession session,
    List<IDСorresponds> importingObjectIDs,
    ImportEventLog eventLog)
  {
    this.session = session;
    this.importingObjectIDs = importingObjectIDs;
    this.eventLog = eventLog;
  }

  public void RestoreItem(TCollectionItem item)
  {
    this.RestoreItem(item, new BriefcaseImportProgress(OperationType.Importing));
  }

  public void Restore(List<TCollectionItem> collection)
  {
    BriefcaseImportProgress bip = new BriefcaseImportProgress(OperationType.Importing);
    foreach (TCollectionItem collectionItem in collection)
      this.RestoreItem(collectionItem, bip);
  }

  private void RestoreItem(TCollectionItem item, BriefcaseImportProgress bip)
  {
    try
    {
      (this.session as UserSession).StartTransaction();
      this.OnRestore(item, bip);
      (this.session as UserSession).Commit();
    }
    catch
    {
      (this.session as UserSession).Rollback();
      throw;
    }
  }

  protected abstract void OnRestore(TCollectionItem item, BriefcaseImportProgress bip);
}
