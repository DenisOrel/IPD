// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DocumentFormatProperty
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class DocumentFormatProperty(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IDocumentFormatProperty,
  IBaseObject
{
  public static string EntityName = "DOCUMENT_FORMAT_PROPERTY";
  private string _characterCode;
  private string _dataFormat;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.CharacterCode = this.ParamsArr.Length == 3 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.DataFormat = this.ParamsArr[1];
  }

  public string CharacterCode
  {
    get
    {
      this.Used = true;
      return this._characterCode;
    }
    private set => this._characterCode = value;
  }

  public string DataFormat
  {
    get
    {
      this.Used = true;
      return this._dataFormat;
    }
    private set => this._dataFormat = value;
  }
}
