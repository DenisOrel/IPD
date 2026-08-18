// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.DocScript
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class DocScript : 
  ExpertScriptable,
  IExpertDocScript,
  IExpertScriptable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  protected long templateID = -1;
  protected Guid docTypeGuid = Guid.Empty;
  protected string docTypeName = "";

  public DocScript(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.objType = ExpertScriptType.DocScript;
  }

  protected override void LoadField(UserSession uSession, AttributeValues av)
  {
    base.LoadField(uSession, av);
    if (av.AttributeID != ExpertConsts.Consts.attrTemplateLink)
      return;
    IDBAttribute attributeById = this.GetAttributeByID(ExpertConsts.Consts.attrTemplateLink);
    if (attributeById == null)
      return;
    if (attributeById is IDBObjectLinkAttribute)
      this.templateID = (attributeById as IDBObjectLinkAttribute).DBObject.ObjectID;
    else
      this.templateID = attributeById.AsInteger;
  }

  protected override int GetAttribCount() => base.GetAttribCount() + 3;

  protected override AttributeValues[] CreateAttribs()
  {
    AttributeValues[] attribs = base.CreateAttribs();
    int attribCount = base.GetAttribCount();
    attribs[attribCount] = new AttributeValues(ExpertConsts.Consts.attrTemplateLink, FieldTypes.ftObjectLink, MultiValueModes.SingleValue);
    attribs[attribCount + 1] = new AttributeValues(ExpertConsts.Consts.attrGenDocType, FieldTypes.ftGuid, MultiValueModes.SingleValue);
    attribs[attribCount + 2] = new AttributeValues(ExpertConsts.Consts.attrGenDocName, FieldTypes.ftString, MultiValueModes.SingleValue);
    return attribs;
  }

  protected override AttributeValues[] SaveData()
  {
    AttributeValues[] attributeValuesArray = base.SaveData();
    for (int index = 0; index < attributeValuesArray.Length; ++index)
    {
      if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrTemplateLink)
      {
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) this.templateID
          };
        else
          attributeValuesArray[index].Values[0] = (object) this.templateID;
      }
      else if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrGenDocType)
      {
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) this.docTypeGuid
          };
        else
          attributeValuesArray[index].Values[0] = (object) this.docTypeGuid;
      }
      else if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrGenDocName)
      {
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) this.docTypeName
          };
        else
          attributeValuesArray[index].Values[0] = (object) this.docTypeName;
      }
    }
    return attributeValuesArray;
  }

  public long TemplateId
  {
    get => this.templateID;
    set => this.templateID = value;
  }

  public Guid DocTypeGuid
  {
    get => this.docTypeGuid;
    set => this.docTypeGuid = value;
  }

  public string DocTypeName
  {
    get => this.docTypeName;
    set => this.docTypeName = value;
  }
}
