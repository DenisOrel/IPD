// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Settings.ResponceObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Settings;

public class ResponceObject : 
  DBObject,
  IResponceObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IStatus
{
  public ResponceObject(UserSession uSession)
    : base(uSession)
  {
  }

  public ResponceObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public string ResponceID
  {
    get => this.GetResponceID();
    private set => this.SetResponceID(value);
  }

  public string RequestID
  {
    get => this.GetRequestID();
    private set => this.SetGetRequestID(value);
  }

  public long Status
  {
    get => this.GetStatus();
    set => this.SetStatus(value);
  }

  public long[] ConfigElementLink
  {
    get => this.GetConfigElementLink();
    set => this.SetConfigElementLink(value);
  }

  public long[] DestinationObjectsLink
  {
    get => this.GetDestinationObjectsLink();
    set => this.SetDestinationObjectsLink(value);
  }

  protected string GetResponceID()
  {
    string responceId = "";
    IDBAttribute attributeById = this.GetAttributeByID(Const.ResponceIDAttrTypeID);
    if (attributeById != null)
      responceId = attributeById.AsString;
    return responceId;
  }

  protected void SetResponceID(string value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.ResponceIDAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.AsString = value;
  }

  protected string GetRequestID()
  {
    string requestId = "";
    IDBAttribute attributeById = this.GetAttributeByID(Const.RequestIDAttrTypeID);
    if (attributeById != null)
      requestId = attributeById.AsString;
    return requestId;
  }

  protected void SetGetRequestID(string value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.RequestIDAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.AsString = value;
  }

  protected long GetStatus()
  {
    long status = 0;
    IDBAttribute attributeById = this.GetAttributeByID(Const.StatusAttrTypeID);
    if (attributeById != null)
      status = attributeById.AsInteger;
    return status;
  }

  protected void SetStatus(long value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.StatusAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.Value = (object) value;
  }

  protected virtual long[] GetConfigElementLink()
  {
    long[] configElementLink = new long[0];
    IDBAttribute attributeById = this.GetAttributeByID(Const.ConfigElementLinkTypeID);
    if (attributeById != null)
      configElementLink = Array.ConvertAll<object, long>(attributeById.Values, new Converter<object, long>(this.ObjectToLong));
    return configElementLink;
  }

  protected virtual void SetConfigElementLink(long[] value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.ConfigElementLinkTypeID);
    if (attributeById == null || value.Length == 0)
      return;
    attributeById.Values = Array.ConvertAll<long, object>(value, new Converter<long, object>(ResponceObject.LongToObject));
  }

  private long[] GetDestinationObjectsLink()
  {
    long[] destinationObjectsLink = new long[0];
    IDBAttribute attributeById = this.GetAttributeByID(Const.DestinationObjectLinkAttrID);
    if (attributeById != null)
      destinationObjectsLink = Array.ConvertAll<object, long>(attributeById.Values, new Converter<object, long>(Convert.ToInt64));
    return destinationObjectsLink;
  }

  private void SetDestinationObjectsLink(long[] value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.DestinationObjectLinkAttrID);
    if (attributeById == null || value.Length == 0)
      return;
    attributeById.Values = Array.ConvertAll<long, object>(value, new Converter<long, object>(ResponceObject.LongToObject));
  }

  public static object LongToObject(long value) => (object) value;

  private long ObjectToLong(object input)
  {
    long result;
    long.TryParse(input.ToString(), out result);
    return result;
  }
}
