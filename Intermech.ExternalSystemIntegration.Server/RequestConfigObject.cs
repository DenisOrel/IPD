// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestConfigObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class RequestConfigObject : 
  ConfigObject,
  IRequestConfigObject,
  IConfigObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public RequestConfigObject(UserSession uSession)
    : base(uSession)
  {
  }

  public RequestConfigObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  protected override long GetSchemeTransfLink()
  {
    long schemeTransfLink = 0;
    IDBAttribute byId = this.Attributes.FindByID(Const.RequestSchemeLinkAttrTypeID);
    if (byId != null)
      schemeTransfLink = byId.AsInteger;
    return schemeTransfLink;
  }

  protected override void SetSchemeTransfLink(long value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.RequestSchemeLinkAttrTypeID);
    if (byId == null)
      return;
    byId.AsInteger = value;
  }

  public bool ShowCard
  {
    get => this.GetShowCard();
    set => this.SetShowCard(value);
  }

  public string FileName
  {
    get => this.GetFileName();
    set => this.SetFileName(value);
  }

  protected virtual bool GetShowCard()
  {
    bool showCard = false;
    IDBAttribute byId = this.Attributes.FindByID(Const.ShowCardAttrTypeID);
    if (byId != null)
      showCard = byId.AsBoolean;
    return showCard;
  }

  protected virtual void SetShowCard(bool value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.ShowCardAttrTypeID);
    if (byId == null)
      return;
    byId.AsBoolean = value;
  }

  protected virtual string GetFileName()
  {
    string fileName = string.Empty;
    IDBAttribute byId = this.Attributes.FindByID(Const.RequestFileNameAttrTypeID);
    if (byId != null)
      fileName = byId.AsString;
    return fileName;
  }

  protected virtual void SetFileName(string value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.RequestFileNameAttrTypeID);
    if (byId == null)
      return;
    byId.AsString = value;
  }
}
