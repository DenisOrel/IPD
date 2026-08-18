// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.VisDataObject
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

public class VisDataObject(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable), ISchemeSaveLoad
{
  protected override void DoDelete()
  {
    base.DoDelete();
    ServerPDMPlugin.VisCache.DeleteValue(this.ObjectID);
    ServerPDMPlugin.VisCache.AddDeleteEvent(this.ObjectID, this.Session);
  }

  public void SaveScheme(VisSchemeParms scheme)
  {
    scheme.SaveToObject(this.Session);
    ServerPDMPlugin.VisCache.SaveValue(this.ObjectID, scheme);
    ServerPDMPlugin.VisCache.AddUpdateEvent(this.ObjectID, this.Session);
  }

  public VisSchemeParms LoadScheme() => new VisSchemeParms(this.ObjectID, this.Session);
}
