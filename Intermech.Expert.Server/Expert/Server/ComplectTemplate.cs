// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ComplectTemplate
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class ComplectTemplate : 
  ExpertScriptable,
  IComplectTemplate,
  IExpertScriptable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  private string _objTypeGUID = "";

  public ComplectTemplate(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.objType = ExpertScriptType.ComplectTemplate;
  }

  public string ObjTypeGuid
  {
    get => this._objTypeGUID;
    set => this._objTypeGUID = value;
  }

  protected override int GetAttribCount() => base.GetAttribCount() + 1;

  protected override AttributeValues[] CreateAttribs()
  {
    AttributeValues[] attribs = base.CreateAttribs();
    attribs[base.GetAttribCount()] = new AttributeValues(ExpertConsts.Consts.attrResObjTypeGUID, FieldTypes.ftString, MultiValueModes.SingleValue);
    return attribs;
  }

  protected override AttributeValues[] SaveData()
  {
    AttributeValues[] attributeValuesArray = base.SaveData();
    for (int index = 0; index < attributeValuesArray.Length; ++index)
    {
      if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrResObjTypeGUID)
      {
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) this._objTypeGUID
          };
        else
          attributeValuesArray[index].Values[0] = (object) this._objTypeGUID;
      }
    }
    return attributeValuesArray;
  }
}
