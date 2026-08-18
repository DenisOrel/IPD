// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertFormula
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertFormula(UserSession uSession, DataTable objectsTable) : 
  ExpertFormulable(uSession, objectsTable),
  IExpertFormula,
  IExpertFormulable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  private AttribPair result;
  internal string resAttrGUID = "";
  internal string resObjTypeGUID = "";
  private string resShortName = "";
  internal int resAttrID = -1;
  internal string resAttrSName = "";
  internal string resAttrLName = "";
  internal FieldTypes attrFT = FieldTypes.ftString;
  internal bool multis;
  internal int resObjTypeID = -1;
  internal string resObjTypeSName = "";
  internal string resObjTypeLName = "";

  private void RegisterRes(
    int resAttrID,
    int resObjTypeID,
    string resAttrSName,
    string resAttrLName,
    string resObjTypeSName,
    string resObjTypeLName,
    FieldTypes ft,
    bool multis)
  {
    this.result = new AttribPair(resAttrID, resObjTypeID);
    PairName pairName;
    if (!ExpertServer.es.attNames.ContainsKey(this.result))
    {
      pairName = new PairName(resAttrSName, resAttrLName, resObjTypeSName, resObjTypeLName, ft, multis);
      ExpertServer.es.attNames.GetOrAdd(this.result, pairName);
    }
    else
      pairName = ExpertServer.es.attNames[this.result];
    this.resShortName = pairName.ShortName;
  }

  protected override void LoadField(UserSession uSession, AttributeValues av)
  {
    if (av.AttributeID == ExpertConsts.Consts.attrResType)
    {
      string str = Convert.ToString(av.Values[0]);
      for (int index = 0; index < ExpertConsts.AsgnResTypes.Length; ++index)
      {
        if (str == ExpertConsts.AsgnResTypes[index])
        {
          this.resType = (DataType) index;
          break;
        }
      }
    }
    if (av.AttributeID == ExpertConsts.Consts.attrResAttrGUID)
    {
      DataTable table = uSession.DBCache.GetTable("IMS_ATTRIBUTES");
      this.resAttrGUID = Convert.ToString(av.Values[0]).Trim().ToLower();
      string filterExpression = $"F_GUID='{this.resAttrGUID}'";
      DataRow[] dataRowArray = table.Select(filterExpression);
      if (dataRowArray.Length != 0)
      {
        this.resAttrID = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
        this.resAttrSName = Convert.ToString(dataRowArray[0]["F_ALIAS"]).Trim();
        if (this.resAttrSName == "")
          this.resAttrSName = Convert.ToString(dataRowArray[0]["F_SHORT_NAME"]).Trim();
        this.resAttrLName = ExpertConsts.lBrace + Convert.ToString(dataRowArray[0]["F_NAME"]).Trim() + ExpertConsts.rBrace;
        if (this.resAttrSName == "")
          this.resAttrSName = this.resAttrLName;
        this.attrFT = (FieldTypes) Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_TYPE"]);
        MultiValueModes int32 = (MultiValueModes) Convert.ToInt32(dataRowArray[0]["F_MULTIPLE_VALUED"]);
        this.multis = int32 == MultiValueModes.MultiValues || int32 == MultiValueModes.MultiValuesFromList;
      }
      if (this.resObjTypeID != -1)
        this.RegisterRes(this.resAttrID, this.resObjTypeID, this.resAttrSName, this.resAttrLName, this.resObjTypeSName, this.resObjTypeLName, this.attrFT, this.multis);
    }
    if (av.AttributeID != ExpertConsts.Consts.attrResObjTypeGUID)
      return;
    DataTable table1 = uSession.DBCache.GetTable("IMS_OBJECT_TYPES");
    this.resObjTypeGUID = Convert.ToString(av.Values[0]).Trim().ToLower();
    if (this.resObjTypeGUID != "")
    {
      DataRow[] dataRowArray = table1.Select($"F_GUID='{this.resObjTypeGUID}'");
      if (dataRowArray.Length != 0)
      {
        this.resObjTypeID = Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]);
        this.resObjTypeLName = Convert.ToString(dataRowArray[0]["F_OBJ_NAME"]).Trim();
        this.resObjTypeSName = Convert.ToString(dataRowArray[0]["F_SHORT_NAME"]).Trim();
      }
    }
    if (this.resAttrID == -1)
      return;
    this.RegisterRes(this.resAttrID, this.resObjTypeID, this.resAttrSName, this.resAttrLName, this.resObjTypeSName, this.resObjTypeLName, this.attrFT, this.multis);
  }

  protected override int GetAttribCount() => base.GetAttribCount() + 3;

  protected override AttributeValues[] CreateAttribs()
  {
    AttributeValues[] attribs = base.CreateAttribs();
    int attribCount = base.GetAttribCount();
    attribs[attribCount] = new AttributeValues(ExpertConsts.Consts.attrResAttrGUID, FieldTypes.ftString, MultiValueModes.SingleValue);
    attribs[attribCount + 1] = new AttributeValues(ExpertConsts.Consts.attrResObjTypeGUID, FieldTypes.ftString, MultiValueModes.SingleValue);
    attribs[attribCount + 2] = new AttributeValues(ExpertConsts.Consts.attrResType, FieldTypes.ftString, MultiValueModes.SingleValue);
    return attribs;
  }

  protected override AttributeValues[] SaveData()
  {
    AttributeValues[] attributeValuesArray = base.SaveData();
    for (int index = 0; index < attributeValuesArray.Length; ++index)
    {
      if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrResAttrGUID)
      {
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) this.resAttrGUID
          };
        else
          attributeValuesArray[index].Values[0] = (object) this.resAttrGUID;
      }
      if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrResObjTypeGUID)
      {
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) this.resObjTypeGUID
          };
        else
          attributeValuesArray[index].Values[0] = (object) this.resObjTypeGUID;
      }
      if (attributeValuesArray[index].AttributeID == ExpertConsts.Consts.attrResType)
      {
        string asgnResType = ExpertConsts.AsgnResTypes[(int) this.resType];
        if (attributeValuesArray[index].Values == null || attributeValuesArray[index].Values[0] == DBNull.Value)
          attributeValuesArray[index].Values = new object[1]
          {
            (object) asgnResType
          };
        else
          attributeValuesArray[index].Values[0] = (object) asgnResType;
      }
    }
    return attributeValuesArray;
  }

  public AttribPair Result
  {
    get => this.result;
    set => this.result = value;
  }

  public string resAttrGuid
  {
    get => this.resAttrGUID;
    set => this.resAttrGUID = value;
  }

  public string resObjTypeGuid
  {
    get => this.resObjTypeGUID;
    set => this.resObjTypeGUID = value;
  }

  public string resName => this.resShortName;

  public override bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    bool flag1 = false;
    if (this.resAttrGUID == fromAttribute.GUID.ToString())
    {
      this.resAttrGUID = toAttribute.GUID.ToString();
      IDBAttribute attributeById = this.GetAttributeByID(ExpertConsts.Consts.attrResAttrGUID);
      if (attributeById != null)
        attributeById.Value = (object) this.resAttrGUID;
      flag1 = true;
    }
    TempFormula tempFormula = this.GetTempFormula();
    bool flag2 = tempFormula.PerformAttrChange(fromAttribute, toAttribute) | flag1;
    if (flag2)
      this.UpdateObject(tempFormula);
    return flag2;
  }

  public override void SetTempFormula(TempFormula tf)
  {
    base.SetTempFormula(tf);
    ExpertServer.ExpertFormulaInfo val = new ExpertServer.ExpertFormulaInfo(tf, this.resAttrGuid, this.resObjTypeGuid);
    ExpertServer.es.SetValueToCache<long, ExpertServer.ExpertFormulaInfo>(this.ObjectID, val, ExpertServer.es.expertFormulae);
    ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheFormula, this.ObjectID, 0L, this.UserSession.DataManager);
  }
}
