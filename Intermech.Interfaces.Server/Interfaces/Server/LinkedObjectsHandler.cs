// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.LinkedObjectsHandler
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public abstract class LinkedObjectsHandler
{
  private bool changed;
  private DateTime lastModifyObjectTypes;

  public LinkedObjectsHandler()
  {
    this.changed = true;
    this.lastModifyObjectTypes = DateTime.MinValue;
    MetaDataHelperService.Instance.OnCacheReloaded += new MetaDataHelperEventHandler(this.OnCacheReloaded);
  }

  private void OnCacheReloaded(object sender, EventArgs e)
  {
    if (this.changed)
      return;
    this.changed = true;
  }

  protected abstract void OnReloadTypes();

  private void ReloadTypes(bool force)
  {
    if (!force && !(MetaDataHelper.LastObjectsSyncTime != this.lastModifyObjectTypes))
      return;
    this.OnReloadTypes();
    this.lastModifyObjectTypes = MetaDataHelper.LastObjectsSyncTime;
  }

  public bool IsTypesChanged(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    return this.changed;
  }

  public void UpdateHandleAndOutputTypes(IUserSession session, bool force)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    this.ReloadTypes(force);
    this.changed = false;
  }
}
