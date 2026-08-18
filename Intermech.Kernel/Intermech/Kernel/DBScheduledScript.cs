// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBScheduledScript
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using System.Data;


namespace Intermech.Kernel;

public class DBScheduledScript(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable)
{
  private void DoAddToCache()
  {
    ServiceUtils.GetService<IScheduledScriptService>((object) ApplicationServices.Container, false)?.RegisterScript(this.Session.SessionGUID, new ScheduledScriptInfo((IDBObject) this));
  }

  private void DoUpdateObjInfo(IDBAttribute attribute)
  {
    ServiceUtils.GetService<IScheduledScriptService>((object) ApplicationServices.Container, false)?.UpdateScript(this.Session.SessionGUID, new ScheduledScriptInfo((IDBObject) this));
  }

  private void DoRemoveFromCache()
  {
    ServiceUtils.GetService<IScheduledScriptService>((object) ApplicationServices.Container, false)?.RemoveScript(this.Session.SessionGUID, new ScheduledScriptInfo((IDBObject) this));
  }

  protected override void AfterSetCaption()
  {
    base.AfterSetCaption();
    this.DoUpdateObjInfo((IDBAttribute) null);
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    this.DoAddToCache();
  }

  protected override void DoDelete()
  {
    base.DoDelete();
    this.DoRemoveFromCache();
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
    this.DoUpdateObjInfo(attribute);
  }

  protected override void DoBeforeDeleteAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoBeforeDeleteAdditionalAttributeValue(attribute);
    this.DoUpdateObjInfo(attribute);
  }
}
