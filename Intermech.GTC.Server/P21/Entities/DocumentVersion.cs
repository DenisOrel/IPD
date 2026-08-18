// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DocumentVersion
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class DocumentVersion(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IDocumentVersion,
  IBaseObject
{
  public static string EntityName = "DOCUMENT_VERSION";
  private IMultiLanguageString _description;
  private string _id;
  private IDocument _associatedDocument;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedDocument = this.ParamsArr.Length == 3 ? (IDocument) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Description = (IMultiLanguageString) entityObjects.Get(this.ParamsArr[1]);
    this.Id = this.ParamsArr[2];
  }

  public IMultiLanguageString Description
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

  public IDocument AssociatedDocument
  {
    get
    {
      this.Used = true;
      return this._associatedDocument;
    }
    private set => this._associatedDocument = value;
  }
}
