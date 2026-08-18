// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DigitalFile
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

public class DigitalFile(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IDigitalFile, IBaseObject
{
  public static string EntityName = "DIGITAL_FILE";
  private string _fileId;
  private string _versionId;
  private IExternalFileIdAndLocation[] _externalIdAndLocation;
  private IDocumentFormatProperty _fileFormat;
  private IDocumentCreationProperty _creation;
  private IDocumentContentProperty _content;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Content = this.ParamsArr.Length == 8 ? (IDocumentContentProperty) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Creation = (IDocumentCreationProperty) entityObjects.Get(this.ParamsArr[1]);
    this.ExternalIdAndLocation = ((IEnumerable<string>) this.ParamsArr[3].Split(',')).Select<string, IExternalFileIdAndLocation>((Func<string, IExternalFileIdAndLocation>) (itemStr => (IExternalFileIdAndLocation) entityObjects.Get(itemStr))).ToArray<IExternalFileIdAndLocation>();
    this.FileFormat = (IDocumentFormatProperty) entityObjects.Get(this.ParamsArr[4]);
    this.FileId = this.ParamsArr[5];
    this.VersionId = this.ParamsArr[6];
  }

  public string FileId
  {
    get
    {
      this.Used = true;
      return this._fileId;
    }
    private set => this._fileId = value;
  }

  public string VersionId
  {
    get
    {
      this.Used = true;
      return this._versionId;
    }
    private set => this._versionId = value;
  }

  public IExternalFileIdAndLocation[] ExternalIdAndLocation
  {
    get
    {
      this.Used = true;
      return this._externalIdAndLocation;
    }
    private set => this._externalIdAndLocation = value;
  }

  public IDocumentFormatProperty FileFormat
  {
    get
    {
      this.Used = true;
      return this._fileFormat;
    }
    private set => this._fileFormat = value;
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
