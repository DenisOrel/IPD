// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertObject
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

public abstract class ExpertObject : 
  DBObject,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  internal AttribPair[] attribs;
  internal string[] attrGUIDs;
  internal string[] objTypeGUIDs;
  protected long[] objectLinks;
  protected TempFormula cond;
  public bool newCond;
  public bool loaded;
  protected string _Name;
  protected long _Flags;
  internal ExpertObjType _objType;

  public ExpertObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.attribs = (AttribPair[]) null;
    this.attrGUIDs = (string[]) null;
    this.objTypeGUIDs = (string[]) null;
    this.objectLinks = (long[]) null;
    this._Name = "";
  }

  public virtual void Load()
  {
    if (this.loaded)
      return;
    string[] attrSNames = (string[]) null;
    string[] attrLNames = (string[]) null;
    string[] objTypeSNames = (string[]) null;
    string[] objTypeLNames = (string[]) null;
    FieldTypes[] attFTypes = (FieldTypes[]) null;
    bool[] multi = (bool[]) null;
    AttributeValues[] attributesValues = this.GetAttributesValues(GetAttributeValuesModes.None);
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (attributeValues.AttributeID == ExpertConsts.Consts.attrAttrGUIDs && attributeValues.Values[0] != DBNull.Value)
      {
        this.attribs = (AttribPair[]) Array.CreateInstance(typeof (AttribPair), attributeValues.Values.Length);
        this.attrGUIDs = (string[]) Array.CreateInstance(typeof (string), attributeValues.Values.Length);
        this.objTypeGUIDs = (string[]) Array.CreateInstance(typeof (string), attributeValues.Values.Length);
        attrSNames = (string[]) Array.CreateInstance(typeof (string), attributeValues.Values.Length);
        attrLNames = (string[]) Array.CreateInstance(typeof (string), attributeValues.Values.Length);
        objTypeSNames = (string[]) Array.CreateInstance(typeof (string), attributeValues.Values.Length);
        objTypeLNames = (string[]) Array.CreateInstance(typeof (string), attributeValues.Values.Length);
        attFTypes = (FieldTypes[]) Array.CreateInstance(typeof (FieldTypes), attributeValues.Values.Length);
        multi = (bool[]) Array.CreateInstance(typeof (bool), attributeValues.Values.Length);
      }
    }
    foreach (AttributeValues av in attributesValues)
    {
      if (av.AttributeID == ExpertConsts.Consts.attrAttrGUIDs && av.Values[0] != DBNull.Value)
      {
        DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
        for (int index = 0; index < av.Values.Length; ++index)
        {
          string lower = (av.Values[index] as string).Trim().ToLower();
          this.attrGUIDs[index] = lower;
          if (lower != "")
          {
            DataRow[] dataRowArray = table.Select($"F_GUID='{lower}'");
            if (dataRowArray.Length != 0)
            {
              int int32_1 = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
              if (this.attribs != null && this.attribs[index] == null)
                this.attribs[index] = new AttribPair(int32_1);
              attrSNames[index] = Convert.ToString(dataRowArray[0]["F_ALIAS"]).Trim();
              if (attrSNames[index] == "")
                attrSNames[index] = Convert.ToString(dataRowArray[0]["F_SHORT_NAME"]).Trim();
              attrLNames[index] = ExpertConsts.lBrace + Convert.ToString(dataRowArray[0]["F_NAME"]).Trim() + ExpertConsts.rBrace;
              if (attrSNames[index] == "")
                attrSNames[index] = attrLNames[index];
              int int32_2 = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_TYPE"]);
              attFTypes[index] = (FieldTypes) int32_2;
              MultiValueModes int32_3 = (MultiValueModes) Convert.ToInt32(dataRowArray[0]["F_MULTIPLE_VALUED"]);
              multi[index] = int32_3 == MultiValueModes.MultiValues || int32_3 == MultiValueModes.MultiValuesFromList;
            }
            else
            {
              if (this.attribs != null && this.attribs[index] == null)
                this.attribs[index] = new AttribPair(0);
              attrSNames[index] = "";
              attrLNames[index] = LocalizationHolder.rm.GetString("Expert.Server_197");
              attFTypes[index] = FieldTypes.ftUnknown;
              multi[index] = false;
            }
          }
        }
      }
      else if (av.AttributeID == ExpertConsts.Consts.attrObjTypeGUIDs && this.attribs != null)
      {
        DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
        for (int index = 0; index < av.Values.Length; ++index)
        {
          if (index < this.attribs.Length)
          {
            string str = "";
            if (av.Values[index] != DBNull.Value)
              str = (av.Values[index] as string).Trim().ToLower();
            this.objTypeGUIDs[index] = str;
            if (str != "")
            {
              DataRow[] dataRowArray = table.Select($"F_GUID='{str}'");
              if (dataRowArray.Length != 0)
              {
                this.attribs[index].objTypeID = Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]);
                objTypeLNames[index] = Convert.ToString(dataRowArray[0]["F_OBJ_TYPE_NAME"]).Trim();
                objTypeSNames[index] = Convert.ToString(dataRowArray[0]["F_SHORT_NAME"]).Trim();
                if (objTypeSNames[index] == "")
                  objTypeSNames[index] = objTypeLNames[index];
              }
            }
          }
        }
      }
      else if (av.AttributeID == ExpertConsts.Consts._attrObjName && av.Values[0] != DBNull.Value)
        this._Name = Convert.ToString(av.Values[0]);
      else if (av.AttributeID == ExpertConsts.Consts.attrObjLinkIDs && av.Values[0] != DBNull.Value)
      {
        List<long> longList = new List<long>();
        for (int index = 0; index < av.Values.Length; ++index)
        {
          if (av.Values[index] != null && av.Values[index] != DBNull.Value)
            longList.Add(Convert.ToInt64(av.Values[index]));
        }
        this.objectLinks = longList.ToArray();
      }
      else if ((av.AttributeID != ExpertConsts.Consts.attrCondObj || av.Values == null || av.Values[0] == DBNull.Value) && av.AttributeID != ExpertConsts.Consts.attrObjData && av.AttributeID != ExpertConsts.Consts.attrLongBlob)
      {
        if (av.AttributeID == ExpertConsts.Consts.attrFlags)
        {
          if (av.Values != null && av.Values[0] != DBNull.Value)
            this._Flags = Convert.ToInt64(av.Values[0]);
        }
        else if (av.Values != null && av.Values[0] != DBNull.Value)
          this.LoadField(this.UserSession, av);
      }
    }
    if (this.attribs != null)
      this.RegAttrNames(attrSNames, attrLNames, objTypeSNames, objTypeLNames, attFTypes, multi);
    if (this._Name == "")
    {
      IDBAttribute attributeById = this.GetAttributeByID(ExpertConsts.Consts._attrObjName);
      if (attributeById != null)
        this._Name = attributeById.AsString;
    }
    this.ReadBLOB();
    this.ReadCond();
    this.loaded = true;
  }

  protected virtual void RegAttrNames(
    string[] attrSNames,
    string[] attrLNames,
    string[] objTypeSNames,
    string[] objTypeLNames,
    FieldTypes[] attFTypes,
    bool[] multi)
  {
    for (int index = 0; index < this.attribs.Length; ++index)
    {
      if (this.attribs[index] != null && !ExpertServer.es.attNames.ContainsKey(this.attribs[index]))
        ExpertServer.es.attNames.GetOrAdd(this.attribs[index], new PairName(attrSNames[index], attrLNames[index], objTypeSNames[index], objTypeLNames[index], attFTypes[index], multi[index]));
    }
  }

  protected virtual void LoadField(UserSession uSession, AttributeValues av)
  {
  }

  protected virtual void LoadBLOBData(byte[] data)
  {
  }

  protected virtual byte[] SaveBLOBData() => (byte[]) Array.CreateInstance(typeof (byte), 0);

  protected virtual void ReadCond()
  {
    if (!(this.GetAttributeByID(ExpertConsts.Consts.attrCondObj) is IBlobReader attributeById))
      return;
    BlobInformation blobInformation = attributeById.OpenBlob(0);
    if (blobInformation.RealFileSize > 0L)
    {
      try
      {
        byte[] zipScr = attributeById.ReadDataBlock((int) blobInformation.RealFileSize);
        if (zipScr.Length == 0)
          return;
        this.cond = new TempFormula((XmlNode) ZlibHelper.UnpackXmlBuffer(zipScr).DocumentElement);
        this.cond.FixInfixForm((IUserSession) this.UserSession);
      }
      finally
      {
        attributeById.CloseBlob();
      }
    }
    else
      attributeById.CloseBlob();
  }

  protected virtual void WriteCond()
  {
    if (this.cond == null)
      return;
    MemoryStream memoryStream = new MemoryStream();
    XmlTextWriter writer = new XmlTextWriter((Stream) memoryStream, Encoding.UTF8);
    this.cond.WriteToXML(ref writer);
    writer.Flush();
    byte[] data = ZlibHelper.PackBuffer((Stream) memoryStream);
    if (!((this.Attributes.FindByID(ExpertConsts.Consts.attrCondObj) ?? this.Attributes.AddAttribute(ExpertConsts.Consts.attrCondObj, false)) is IBlobWriter blobWriter))
      return;
    BlobInformation blobInfo = new BlobInformation((long) data.Length, (long) data.Length, DateTime.Now, "", ArcMethods.NotPacked, "");
    if (blobWriter.OpenBlob(blobInfo, false))
      blobWriter.WriteDataBlock(data);
    if (ExpertServer.es == null)
      return;
    ExpertServer.es.SetValueToCache<long, TempFormula>(this.ObjectID, this.cond, ExpertServer.es.expertConds);
    ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheTables, this.ObjectID, 0L, this.UserSession.DataManager);
  }

  protected virtual void ReadBLOB()
  {
    bool flag = false;
    IDBAttribute attributeById = this.GetAttributeByID(ExpertConsts.Consts.attrLongBlob);
    if (attributeById == null)
    {
      attributeById = this.GetAttributeByID(ExpertConsts.Consts.attrObjData);
      if (attributeById is IDBShortBlobAttribute shortBlobAttribute)
      {
        this.LoadBLOBData(shortBlobAttribute.GetData());
        flag = true;
      }
    }
    if (flag || !(attributeById is IBlobReader blobReader))
      return;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      this.LoadBLOBData(blobReader.ReadDataBlock((int) blobInformation.RealFileSize));
    }
    finally
    {
      blobReader.CloseBlob();
    }
  }

  protected virtual void WriteBLOB()
  {
    byte[] data = this.SaveBLOBData();
    IDBAttribute dbAttribute;
    if (data.Length > 100000)
    {
      dbAttribute = this.Attributes.AddAttribute(ExpertConsts.Consts.attrLongBlob, false);
    }
    else
    {
      dbAttribute = this.Attributes.AddAttribute(ExpertConsts.Consts.attrObjData, false);
      this.Attributes.FindByID(ExpertConsts.Consts.attrLongBlob)?.Delete(0L);
    }
    if (!(dbAttribute is IBlobWriter blobWriter))
      return;
    BlobInformation blobInfo = new BlobInformation((long) data.Length, (long) data.Length, DateTime.Now, "", ArcMethods.NotPacked, "");
    if (!blobWriter.OpenBlob(blobInfo, false))
      return;
    blobWriter.WriteDataBlock(data);
  }

  protected virtual int GetAttribCount() => 5;

  internal bool CreateAttribute(int attrTypeId, object initValue)
  {
    if (this.Attributes.FindByID(attrTypeId) != null)
      return false;
    this.Attributes.AddAttribute(attrTypeId, true, new object[1]
    {
      initValue
    });
    return true;
  }

  internal bool CreateAttribute(int attrTypeId)
  {
    if (this.Attributes.FindByID(attrTypeId) != null)
      return false;
    this.Attributes.AddAttribute(attrTypeId, true, (object[]) null);
    return true;
  }

  internal void CreateBaseAttribs()
  {
    this.CreateAttribute(ExpertConsts.Consts.attrObjData);
    if (this.ObjectType != ExpertConsts.Consts.objAttrRules && this.ObjectType != ExpertConsts.Consts.objObjRules)
    {
      this.CreateAttribute(ExpertConsts.Consts.attrAttrGUIDs, (object) "");
      this.CreateAttribute(ExpertConsts.Consts.attrObjTypeGUIDs, (object) "");
      this.CreateAttribute(ExpertConsts.Consts.attrObjLinkIDs);
    }
    if (this.ObjectType == ExpertConsts.Consts.objFormula || this.ObjectType == ExpertConsts.Consts.objAttrRules || this.ObjectType == ExpertConsts.Consts.objObjRules)
    {
      this.CreateAttribute(ExpertConsts.Consts.attrResAttrGUID, (object) "");
      this.CreateAttribute(ExpertConsts.Consts.attrResObjTypeGUID, (object) "");
    }
    if (this is ExpertScriptable)
      this.CreateAttribute(ExpertConsts.Consts.attrAttrRoles);
    if (this.ObjectType == ExpertConsts.Consts.objDocScript)
      this.CreateAttribute(ExpertConsts.Consts.attrTemplateLink);
    if (this.ObjectType != ExpertConsts.Consts.objTable)
      return;
    this.CreateAttribute(ExpertConsts.Consts.attrTableEntries, (object) 1);
    this.CreateAttribute(ExpertConsts.Consts.attrTableCols, (object) 5);
    this.CreateAttribute(ExpertConsts.Consts.attrTableRows, (object) 5);
    this.CreateAttribute(ExpertConsts.Consts.attrTableLayers, (object) 5);
    this.CreateAttribute(ExpertConsts.Consts.attrAttrRoles);
  }

  protected virtual AttributeValues[] CreateAttribs()
  {
    this.CreateBaseAttribs();
    AttributeValues[] instance = (AttributeValues[]) Array.CreateInstance(typeof (AttributeValues), this.GetAttribCount());
    instance[0] = new AttributeValues(ExpertConsts.Consts.attrAttrGUIDs, FieldTypes.ftString, MultiValueModes.MultiValues);
    instance[1] = new AttributeValues(ExpertConsts.Consts.attrObjTypeGUIDs, FieldTypes.ftString, MultiValueModes.MultiValues);
    instance[2] = new AttributeValues(ExpertConsts.Consts.attrObjLinkIDs, FieldTypes.ftObjectLink, MultiValueModes.MultiValues);
    instance[3] = new AttributeValues(ExpertConsts.Consts.attrObjData, FieldTypes.ftShortBlob, MultiValueModes.SingleValue);
    instance[4] = new AttributeValues(ExpertConsts.Consts.attrFlags, FieldTypes.ftInteger, MultiValueModes.SingleValue);
    return instance;
  }

  protected virtual AttributeValues[] SaveData()
  {
    AttributeValues[] attribs = this.CreateAttribs();
    for (int index1 = 0; index1 < attribs.Length; ++index1)
    {
      if (attribs[index1].AttributeID == ExpertConsts.Consts._attrObjName)
      {
        if (attribs[index1].Values == null || attribs[index1].Values[0] == DBNull.Value)
          attribs[index1].Values = new object[1]
          {
            (object) this._Name
          };
        else
          attribs[index1].Values[0] = (object) this._Name;
      }
      else if (attribs[index1].AttributeID == ExpertConsts.Consts.attrAttrGUIDs)
      {
        if (this.attrGUIDs.Length == 0)
        {
          attribs[index1].Values = (object[]) null;
        }
        else
        {
          attribs[index1].Values = (object[]) Array.CreateInstance(typeof (string), this.attrGUIDs.Length);
          for (int index2 = 0; index2 < this.attrGUIDs.Length; ++index2)
            attribs[index1].Values[index2] = (object) this.attrGUIDs[index2];
        }
      }
      else if (attribs[index1].AttributeID == ExpertConsts.Consts.attrObjTypeGUIDs)
      {
        if (this.objTypeGUIDs.Length == 0)
        {
          attribs[index1].Values = (object[]) null;
        }
        else
        {
          attribs[index1].Values = (object[]) Array.CreateInstance(typeof (string), this.objTypeGUIDs.Length);
          for (int index3 = 0; index3 < this.objTypeGUIDs.Length; ++index3)
            attribs[index1].Values[index3] = (object) this.objTypeGUIDs[index3];
        }
      }
      else if (attribs[index1].AttributeID == ExpertConsts.Consts.attrObjLinkIDs)
      {
        this.UpdateObjectLinks();
        if (this.objectLinks.Length == 0)
        {
          attribs[index1].Values = (object[]) null;
        }
        else
        {
          attribs[index1].Values = (object[]) Array.CreateInstance(typeof (object), this.objectLinks.Length);
          for (int index4 = 0; index4 < this.objectLinks.Length; ++index4)
            attribs[index1].Values[index4] = (object) this.objectLinks[index4];
        }
      }
      else if (attribs[index1].AttributeID == ExpertConsts.Consts.attrFlags)
      {
        if (attribs[index1].Values == null || attribs[index1].Values[0] == DBNull.Value)
          attribs[index1].Values = new object[1]
          {
            (object) this._Flags
          };
        else
          attribs[index1].Values[0] = (object) this._Flags;
      }
    }
    this.WriteBLOB();
    this.WriteCond();
    return attribs;
  }

  public virtual void Save() => this.SetAttributesValues(this.SaveData(), false, false);

  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  public ExpertObjType ObjType => this._objType;

  public AttribPair[] usedAttrs => this.attribs;

  public string[] attribGUIDs => this.attrGUIDs;

  public string[] objGUIDs => this.objTypeGUIDs;

  public TempFormula Cond
  {
    get => this.cond;
    set => this.cond = value;
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    if (this.ObjectType != MetaDataHelper.GetObjectTypeID(ExpertObjGUIDs.DocScript))
      return;
    this.UserSession.GetObjectType(this.ObjectType);
    if (attribute.AttributeID != MetaDataHelper.GetAttributeTypeID("cadd920c-306c-11d8-b4e9-00304f19f545"))
      return;
    IAttachedSelectionsServerService service = (IAttachedSelectionsServerService) ServerServices.GetService(typeof (IAttachedSelectionsServerService));
    if (attribute.IsNull || attribute.ValuesCount == 0)
      service.OnDeleteObject(this.ObjectID);
    else
      service.OnSetSelections((IDBObject) this, attribute.Values);
  }

  protected override void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    if (this.ObjectType != MetaDataHelper.GetObjectTypeID(ExpertObjGUIDs.DocScript) || attribute.AttributeID != MetaDataHelper.GetAttributeTypeID("cadd920c-306c-11d8-b4e9-00304f19f545") || deletedValue.IntegerValue == 0L)
      return;
    ((IAttachedSelectionsServerService) ServerServices.GetService(typeof (IAttachedSelectionsServerService))).OnDeleteSelection(this.ObjectID, deletedValue.IntegerValue);
  }

  public override int Delete(long DeleteMode)
  {
    int num = base.Delete(DeleteMode);
    ((IAttachedSelectionsServerService) ServerServices.GetService(typeof (IAttachedSelectionsServerService))).OnDeleteObject(this.ObjectID);
    return num;
  }

  protected override void DoDelete()
  {
    base.DoDelete();
    try
    {
      List<GuidPair> resultPairs = ExpertServer.es.GetResultPairs(this);
      if (resultPairs != null)
      {
        for (int index = 0; index < resultPairs.Count; ++index)
        {
          GuidPair guidPair = resultPairs[index];
          long attrRule = ExpertServer.es.GetAttrRule(this.Session, guidPair.objTypeGUID, guidPair.attrGUID);
          if (attrRule != -1L)
            ExpertServer.DeleteLinks((ExpertRules) this.Session.GetObject(attrRule), this.ObjectGUID);
        }
      }
    }
    catch
    {
    }
    try
    {
      if (!(this is ExpertAttrRules expertAttrRules))
        return;
      ExpertServer.es.DelValueFromCache<AttribPair, ScriptTreeNode>(expertAttrRules.Result, ExpertServer.es.attrRules);
    }
    catch
    {
    }
  }

  protected virtual List<long> CollectObjectLinks()
  {
    List<long> longList = new List<long>();
    if (this.Cond != null)
    {
      foreach (long objectLink in this.Cond.objectLinks)
        longList.Add(objectLink);
    }
    return longList;
  }

  protected void UpdateObjectLinks()
  {
    List<long> longList = this.CollectObjectLinks();
    if (this.objectLinks == null || this.objectLinks.Length != longList.Count)
      this.objectLinks = new long[longList.Count];
    for (int index = 0; index < this.objectLinks.Length; ++index)
      this.objectLinks[index] = longList[index];
  }

  public virtual bool FixIdentsComplete(IUserSession ius)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.FixIdentsComplete(ius);
    if (flag)
      this.WriteCond();
    return flag;
  }

  public virtual bool CreateGUIDs(IUserSession ius)
  {
    bool guiDs = false;
    if (this.cond != null)
      guiDs = this.cond.CreateGUIDs(ius);
    if (guiDs)
      this.WriteCond();
    return guiDs;
  }

  public virtual bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    return false;
  }
}
