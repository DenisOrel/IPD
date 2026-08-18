// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class RequestObject : 
  DBObject,
  IRequestObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IStatus
{
  public RequestObject(UserSession uSession)
    : base(uSession)
  {
  }

  public RequestObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public override void DoAfterCreate()
  {
    base.DoAfterCreate();
    string str = "";
    if (this.Session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService)
      str = customService.Info.Code.ToString();
    this.RequestID = str.Length > 0 ? str + Math.Abs(this.ObjectID).ToString() : Math.Abs(this.ObjectID).ToString();
  }

  public string RequestID
  {
    get => this.GetRequestID();
    private set => this.SetGetRequestID(value);
  }

  public string RequestCaption
  {
    get => this.GetRequestCaption();
    set => this.SetRequestCaption(value);
  }

  public long Status
  {
    get => this.GetStatus();
    set => this.SetStatus(value);
  }

  public long ConfigElementLink
  {
    get => this.GetConfigElementLink();
    set => this.SetConfigElementLink(value);
  }

  public long SourceObjectLink
  {
    get => this.GetSourceObjectLink();
    set => this.SetSourceObjectLink(value);
  }

  protected virtual string GetRequestID()
  {
    string requestId = "";
    IDBAttribute attributeById = this.GetAttributeByID(Const.RequestIDAttrTypeID);
    if (attributeById != null)
      requestId = attributeById.AsString;
    return requestId;
  }

  protected virtual void SetGetRequestID(string value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.RequestIDAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.AsString = value;
  }

  protected virtual string GetRequestCaption()
  {
    string requestCaption = string.Empty;
    IDBAttribute attributeById = this.GetAttributeByID(Const.NameAttrTypeID);
    if (attributeById != null)
      requestCaption = attributeById.AsString;
    return requestCaption;
  }

  protected virtual void SetRequestCaption(string value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.NameAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.AsString = value;
  }

  protected virtual long GetStatus()
  {
    long status = 0;
    IDBAttribute attributeById = this.GetAttributeByID(Const.StatusAttrTypeID);
    if (attributeById != null)
      status = attributeById.AsInteger;
    return status;
  }

  protected virtual void SetStatus(long value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.StatusAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.Value = (object) value;
  }

  protected virtual long GetConfigElementLink()
  {
    long configElementLink = 0;
    IDBAttribute attributeById = this.GetAttributeByID(Const.ConfigElementLinkTypeID);
    if (attributeById != null)
      configElementLink = attributeById.AsInteger;
    return configElementLink;
  }

  protected virtual void SetConfigElementLink(long value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.ConfigElementLinkTypeID);
    if (attributeById == null)
      return;
    attributeById.Value = (object) value;
  }

  protected virtual long GetSourceObjectLink()
  {
    long sourceObjectLink = 0;
    IDBAttribute attributeById = this.GetAttributeByID(Const.SourceObjectLinkAttrID);
    if (attributeById != null)
      sourceObjectLink = attributeById.AsInteger;
    return sourceObjectLink;
  }

  protected virtual void SetSourceObjectLink(long value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.SourceObjectLinkAttrID);
    if (attributeById == null)
      return;
    attributeById.Value = (object) value;
  }
}
