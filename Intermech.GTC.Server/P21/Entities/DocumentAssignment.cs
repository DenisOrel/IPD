// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DocumentAssignment
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class DocumentAssignment(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IDocumentAssignment,
  IBaseObject
{
  public static string EntityName = "DOCUMENT_ASSIGNMENT";
  private string _role;
  private IItemDefinition _isAssignedTo;
  private IDocumentVersion _assignedDocument;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssignedDocument = this.ParamsArr.Length == 3 ? (IDocumentVersion) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.IsAssignedTo = (IItemDefinition) entityObjects.Get(this.ParamsArr[1]);
    this.Role = this.ParamsArr[2];
  }

  public string Role
  {
    get
    {
      this.Used = true;
      return this._role;
    }
    private set => this._role = value;
  }

  public IItemDefinition IsAssignedTo
  {
    get
    {
      this.Used = true;
      return this._isAssignedTo;
    }
    private set => this._isAssignedTo = value;
  }

  public IDocumentVersion AssignedDocument
  {
    get
    {
      this.Used = true;
      return this._assignedDocument;
    }
    private set => this._assignedDocument = value;
  }
}
