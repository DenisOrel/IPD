// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestObjectHelperService
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.ExternalSystemIntegration.Server.Helpers;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class RequestObjectHelperService : LongLifeObject, IRequestObjectHelperService
{
  private static readonly char[] charSeparators = new char[1]
  {
    '='
  };

  public void AssignAttributes(long RequestObjectID, long SourceObjectID, Guid SessionGUID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(SessionGUID);
    IDBObject dbObject = sessionById.GetObject(SourceObjectID, false);
    if (dbObject == null)
      return;
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(dbObject.ObjectType);
    DataTable dataTable = sessionById.GetObjectCollection(Const.TypeSettingItemObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Const.ObjectTypeIDAttrTypeID, RelationalOperators.Equal, (object) objectTypeGuid, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "MyObjects"));
    if (dataTable.Rows.Count <= 0)
      return;
    long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
    if (int64 == 0L || !(sessionById.GetObject(int64) is IObjTypeSettingItemObject settingItemObject) || settingItemObject.RequestConfigs.Length == 0)
      return;
    this.AssignAttributes(RequestObjectID, SourceObjectID, settingItemObject.RequestConfigs[0], SessionGUID);
  }

  public void AssignAttributes(
    long RequestObjectID,
    long SourceObjectID,
    long ConfigObjectID,
    Guid SessionGUID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(SessionGUID);
    IRequestObject requestObj = sessionById.GetObject(RequestObjectID) as IRequestObject;
    IDBObject sourceObj = sessionById.GetObject(SourceObjectID, false);
    IConfigObject configObject = sessionById.GetObject(ConfigObjectID, false) as IConfigObject;
    if (requestObj == null || sourceObj == null || configObject == null)
      return;
    Tuple<int, int>[] array = ((IEnumerable<string>) configObject.AttributeComprasion).Where<string>((System.Func<string, bool>) (attPairStr => !string.IsNullOrEmpty(attPairStr))).Select<string, string[]>((System.Func<string, string[]>) (attrPairStr => attrPairStr.Split(RequestObjectHelperService.charSeparators, StringSplitOptions.RemoveEmptyEntries))).Where<string[]>((System.Func<string[], bool>) (attrPairArr => attrPairArr.Length == 2)).Select<string[], Tuple<int, int>>((System.Func<string[], Tuple<int, int>>) (attrPairArr => new Tuple<int, int>(int.Parse(attrPairArr[0]), int.Parse(attrPairArr[1])))).Where<Tuple<int, int>>((System.Func<Tuple<int, int>, bool>) (attTuple => attTuple.Item1 != 0 && attTuple.Item2 != 0)).ToArray<Tuple<int, int>>();
    try
    {
      requestObj.RequestCaption = sourceObj.Caption;
      requestObj.SourceObjectLink = SourceObjectID;
      requestObj.ConfigElementLink = ConfigObjectID;
      foreach (Tuple<IDBAttribute, IDBAttribute> tuple in ((IEnumerable<Tuple<int, int>>) array).Select<Tuple<int, int>, Tuple<IDBAttribute, IDBAttribute>>((System.Func<Tuple<int, int>, Tuple<IDBAttribute, IDBAttribute>>) (attrIDsTuple => new Tuple<IDBAttribute, IDBAttribute>(sourceObj.Attributes.FindByID(attrIDsTuple.Item1), requestObj.Attributes.AddAttribute(attrIDsTuple.Item2, false)))).Where<Tuple<IDBAttribute, IDBAttribute>>((System.Func<Tuple<IDBAttribute, IDBAttribute>, bool>) (attrPairCheck => attrPairCheck.Item1 != null && attrPairCheck.Item2 != null)).ToArray<Tuple<IDBAttribute, IDBAttribute>>())
        tuple.Item2.Value = tuple.Item1.Value;
    }
    catch (Exception ex)
    {
      HelperMethods.WriteErrorMsg(SessionGUID, ex.Message);
      throw ex;
    }
  }
}
