// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.ResponceConfigObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class ResponceConfigObject : 
  ConfigObject,
  IResponceConfigObject,
  IConfigObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public ResponceConfigObject(UserSession uSession)
    : base(uSession)
  {
  }

  public ResponceConfigObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public int FinderID
  {
    get => this.GetFinderID();
    set => this.SetFinderID(value);
  }

  protected override long GetSchemeTransfLink()
  {
    long schemeTransfLink = 0;
    IDBAttribute byId = this.Attributes.FindByID(Const.ResponceSchemeLinkAttrTypeID);
    if (byId != null)
      schemeTransfLink = byId.AsInteger;
    return schemeTransfLink;
  }

  protected override void SetSchemeTransfLink(long value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.ResponceSchemeLinkAttrTypeID);
    if (byId == null)
      return;
    byId.AsInteger = value;
  }

  protected virtual int GetFinderID()
  {
    int finderId = 0;
    IDBAttribute byId = this.Attributes.FindByID(Const.FinderIDTypeID);
    if (byId != null)
      finderId = (int) byId.AsInteger;
    return finderId;
  }

  protected virtual void SetFinderID(int value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.FinderIDTypeID);
    if (byId == null)
      return;
    byId.Value = (object) value;
  }
}
