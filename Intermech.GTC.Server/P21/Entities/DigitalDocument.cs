// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DigitalDocument
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class DigitalDocument(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IDigitalDocument,
  IBaseObject
{
  public static string EntityName = "DIGITAL_DOCUMENT";
  private IDocumentVersion _documentVersion;
  private string _description;
  private string _id;
  private IDocumentLocationProperty[] _commonLocation;
  private IDocumentFormatProperty _representationFromat;
  private IDigitalFile[] _file;
  private IDocumentCreationProperty _creation;
  private IDocumentContentProperty _content;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.DocumentVersion = this.ParamsArr.Length == 9 ? (IDocumentVersion) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.CommonLocation = ((IEnumerable<string>) this.ParamsArr[1].Split(',')).Select<string, IDocumentLocationProperty>((Func<string, IDocumentLocationProperty>) (itemStr => (IDocumentLocationProperty) entityObjects.Get(itemStr))).ToArray<IDocumentLocationProperty>();
    this.Content = (IDocumentContentProperty) entityObjects.Get(this.ParamsArr[3]);
    this.Creation = (IDocumentCreationProperty) entityObjects.Get(this.ParamsArr[4]);
    this.Description = this.ParamsArr[5];
    this.Id = this.ParamsArr[6];
    this.RepresentationFromat = (IDocumentFormatProperty) entityObjects.Get(this.ParamsArr[7]);
    this.File = ((IEnumerable<string>) this.ParamsArr[8].Split(',')).Select<string, IDigitalFile>((Func<string, IDigitalFile>) (itemStr => (IDigitalFile) entityObjects.Get(itemStr))).ToArray<IDigitalFile>();
  }

  public IDocumentVersion DocumentVersion
  {
    get
    {
      this.Used = true;
      return this._documentVersion;
    }
    private set => this._documentVersion = value;
  }

  public string Description
  {
    get
    {
      this.Used = true;
      return this._description;
    }
    private set => this._description = value;
  }

  public string Id
  {
    get
    {
      this.Used = true;
      return this._id;
    }
    private set => this._id = value;
  }

  public IDocumentLocationProperty[] CommonLocation
  {
    get
    {
      this.Used = true;
      return this._commonLocation;
    }
    private set => this._commonLocation = value;
  }

  public IDocumentFormatProperty RepresentationFromat
  {
    get
    {
      this.Used = true;
      return this._representationFromat;
    }
    private set => this._representationFromat = value;
  }

  public IDigitalFile[] File
  {
    get
    {
      this.Used = true;
      return this._file;
    }
    private set => this._file = value;
  }

  public IDocumentCreationProperty Creation
  {
    get
    {
      this.Used = true;
      return this._creation;
    }
    private set => this._creation = value;
  }

  public IDocumentContentProperty Content
  {
    get
    {
      this.Used = true;
      return this._content;
    }
    private set => this._content = value;
  }
}
