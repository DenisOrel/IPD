// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.XmlFileProcessor
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.ExternalSystemIntegration.Server.Helpers;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class XmlFileProcessor
{
  private string _FileName;
  private IUserSession _Session;
  private IXMLParser _XMLParser;
  private Dictionary<long, string> _ResponceSchemesData;
  private XmlDocument _XmlDocument;

  public XmlFileProcessor(
    IUserSession ASession,
    IXMLParser AXMLParser,
    Dictionary<long, string> AResponceSchemesData,
    string AFileName)
  {
    this._Session = ASession;
    this._XMLParser = AXMLParser;
    this._FileName = AFileName;
    this._XmlDocument = new XmlDocument();
    this._XmlDocument.Load(AFileName);
    this._ResponceSchemesData = AResponceSchemesData;
  }

  public bool ProcessFile()
  {
    bool flag = false;
    long responceSchemeObjectId = this.GetResponceSchemeObjectID();
    if (responceSchemeObjectId != 0L)
    {
      Dictionary<int, string> attributesList = this.GetAttributesList(responceSchemeObjectId);
      long[] numArray1 = new long[0];
      string empty = string.Empty;
      long[] numArray2 = !attributesList.TryGetValue(Const.RequestIDAttrTypeID, out empty) ? this.GetResponceConfigs(responceSchemeObjectId) : this.GetResponceConfigs(responceSchemeObjectId, empty);
      if (numArray2.Length == 0)
      {
        HelperMethods.WriteErrorMsg(this._Session.SessionGUID, $"{this._FileName} : {LocalizationHolder.rm.GetString("ExtInt_23")}");
        return flag;
      }
      IResponceObject responceObject = this._Session.GetObjectCollection(Const.ResponceObjTypeID).Create() as IResponceObject;
      responceObject.ConfigElementLink = numArray2;
      foreach (KeyValuePair<int, string> keyValuePair in attributesList)
      {
        IDBAttribute dbAttribute = (IDBAttribute) null;
        try
        {
          dbAttribute = responceObject.Attributes.AddAttribute(keyValuePair.Key, false);
          dbAttribute.AsString = keyValuePair.Value;
        }
        catch (Exception ex)
        {
          throw new Exception(dbAttribute != null ? string.Format(LocalizationHolder.rm.GetString("ExtInt_24"), (object) keyValuePair.Value, (object) dbAttribute.Name, (object) ex.Message) : ex.Message);
        }
      }
      responceObject.Status = Convert.ToInt64((object) StatusEnum.Wait);
      responceObject.CommitCreation(true);
      return true;
    }
    HelperMethods.WriteErrorMsg(this._Session.SessionGUID, $"{this._FileName} : {LocalizationHolder.rm.GetString("ExtInt_22")}");
    return flag;
  }

  private long[] GetResponceConfigs(long AResponceSchemeID)
  {
    long[] numArray = new long[0];
    return this._Session.GetObjectCollection(Const.ResponceConfigObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Const.ResponceSchemeLinkAttrTypeID, RelationalOperators.Equal, (object) AResponceSchemeID, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "Responce schemes")).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))).ToArray<long>();
  }

  private long[] GetResponceConfigs(long AResponceSchemeID, string requestID)
  {
    long[] responceConfigs = new long[0];
    foreach (DataRow row1 in (InternalDataCollectionBase) this._Session.GetObjectCollection(Const.RequestObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Const.RequestIDAttrTypeID, RelationalOperators.Equal, (object) requestID, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "Requests objs")).Rows)
    {
      if (this._Session.GetObject(Convert.ToInt64(row1[0]), false) is IRequestObject requestObject)
      {
        IDBObject dbObject = this._Session.GetObject(requestObject.SourceObjectLink, false);
        if (dbObject != null)
        {
          Guid linkObjGuidByTypeId = this.GetLinkObjGuidByTypeID(dbObject.ObjectType);
          if (linkObjGuidByTypeId != Guid.Empty)
            responceConfigs = this._Session.GetObjectCollection(Const.ResponceConfigObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
            {
              new ConditionStructure(Const.ResponceSchemeLinkAttrTypeID, RelationalOperators.Equal, (object) AResponceSchemeID, LogicalOperators.AND, 0, false),
              new ConditionStructure(Const.LinkObjectAttrTypeID, RelationalOperators.Equal, (object) linkObjGuidByTypeId, LogicalOperators.NONE, 0, false)
            }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "Responce schemes")).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))).ToArray<long>();
        }
      }
    }
    return responceConfigs;
  }

  private Guid GetLinkObjGuidByTypeID(int sourceObjType)
  {
    Guid linkObjGuidByTypeId = Guid.Empty;
    DataTable dataTable = this._Session.GetObjectCollection(Const.TypeSettingItemObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Const.ObjectTypeIDAttrTypeID, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeGuid(sourceObjType), LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) -2,
      (object) Const.LinkObjectAttrTypeID
    }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "MyObjects"));
    if (dataTable.Rows.Count > 0)
      linkObjGuidByTypeId = new Guid(dataTable.Rows[0][1].ToString());
    return linkObjGuidByTypeId;
  }

  private long GetResponceSchemeObjectID()
  {
    long responceSchemeObjectId = 0;
    foreach (KeyValuePair<long, string> keyValuePair in this._ResponceSchemesData)
    {
      if (this._XMLParser.CompareNodes(keyValuePair.Value, this._XmlDocument.OuterXml))
      {
        responceSchemeObjectId = keyValuePair.Key;
        return responceSchemeObjectId;
      }
      HelperMethods.WriteCompareErrorMsg(this._Session.SessionGUID, string.Format(LocalizationHolder.rm.GetString("ExtInt_21"), (object) this._FileName, (object) keyValuePair.Key, (object) this._XMLParser.CompareErrorMessage));
    }
    return responceSchemeObjectId;
  }

  private Dictionary<int, string> GetAttributesList(long ASchemeID)
  {
    Dictionary<int, string> attributesList = new Dictionary<int, string>();
    string AEtalonNode;
    if (this._ResponceSchemesData.TryGetValue(ASchemeID, out AEtalonNode))
      attributesList = this._XMLParser.ExtractAttributeFromNodes(this._Session.SessionGUID, AEtalonNode, this._XmlDocument.OuterXml);
    return attributesList;
  }
}
