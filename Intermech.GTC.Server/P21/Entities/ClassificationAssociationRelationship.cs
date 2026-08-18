// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ClassificationAssociationRelationship
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ClassificationAssociationRelationship(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IClassificationAssociationRelationship,
  IBaseObject
{
  public static string EntityName = "CLASSIFICATION_ASSOCIATION_RELATIONSHIP";
  private IClassificationAssociation _relating;
  private IClassificationAssociation _related;
  private string _relationshipType;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Relating = this.ParamsArr.Length == 3 ? (IClassificationAssociation) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Related = (IClassificationAssociation) entityObjects.Get(this.ParamsArr[1]);
    this.RelationshipType = this.ParamsArr[2];
  }

  public IClassificationAssociation Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }

  public IClassificationAssociation Related
  {
    get
    {
      this.Used = true;
      return this._related;
    }
    private set => this._related = value;
  }

  public string RelationshipType
  {
    get
    {
      this.Used = true;
      return this._relationshipType;
    }
    private set => this._relationshipType = value;
  }
}
