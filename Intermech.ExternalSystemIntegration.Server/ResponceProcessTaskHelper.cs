// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.ResponceProcessTaskHelper
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.Extensions;
using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.ExternalSystemIntegration.Server.Helpers;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class ResponceProcessTaskHelper
{
  private IUserSession _Session;
  private IXMLParser _XMLParser;
  private Dictionary<long, string> _ResponceSchemesData;

  public ResponceProcessTaskHelper(IUserSession ASession)
  {
    this._Session = ASession;
    this._XMLParser = ServerServices.GetService(typeof (IXMLParser)) as IXMLParser;
    this._ResponceSchemesData = this.GetResponceSchemesData(this.GetResponceSchemeIDs());
  }

  public bool ProcessTask()
  {
    this.ProcessFiles();
    this.ProcessResponceObjs();
    return true;
  }

  private long[] GetResponceSchemeIDs()
  {
    long[] responceSchemeIds = new long[0];
    DataTable source = this._Session.GetObjectCollection(Const.ResponceConfigObjTypeGuid).Select(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) Const.ResponceSchemeLinkAttrTypeID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Name, SortOrders.NONE, 0)
    }));
    if (source.Rows.Count > 0)
      responceSchemeIds = source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))).Distinct<long>().ToArray<long>();
    return responceSchemeIds;
  }

  private Dictionary<long, string> GetResponceSchemesData(long[] AResponceSchemeIDs)
  {
    Dictionary<long, string> responceSchemesData = new Dictionary<long, string>();
    foreach (long aresponceSchemeId in AResponceSchemeIDs)
    {
      if (this._Session.GetObject(aresponceSchemeId, false) is IResponceSchemeObject responceSchemeObject)
      {
        string schemeData = responceSchemeObject.SchemeData;
        if (schemeData.Length > 0)
          responceSchemesData.Add(aresponceSchemeId, schemeData);
      }
    }
    return responceSchemesData;
  }

  private void ProcessFiles()
  {
    string inputFiles = (ServerServices.GetService(typeof (ICommonSettingsHolder)) as ICommonSettingsHolder).InputFiles;
    if (Directory.Exists(inputFiles))
    {
      foreach (string file in Directory.GetFiles(inputFiles, "*.xml", SearchOption.TopDirectoryOnly))
      {
        try
        {
          this.ResponceProcessing(file);
        }
        catch (Exception ex)
        {
          HelperMethods.WriteErrorMsg(this._Session.SessionGUID, $"{file} : {ex.Message}");
          string errorFiles = (ServerServices.GetService(typeof (ICommonSettingsHolder)) as ICommonSettingsHolder).ErrorFiles;
          if (Directory.Exists(errorFiles))
          {
            string destFileName = Path.Combine(errorFiles, Path.GetFileName(file));
            File.Move(file, destFileName);
          }
          else
            HelperMethods.WriteErrorMsg(this._Session.SessionGUID, LocalizationHolder.rm.GetString("ExtInt_2"));
        }
      }
    }
    else
      HelperMethods.WriteErrorMsg(this._Session.SessionGUID, LocalizationHolder.rm.GetString("ExtInt_1"));
  }

  private void ResponceProcessing(string fileName)
  {
    if (!new XmlFileProcessor(this._Session, this._XMLParser, this._ResponceSchemesData, fileName).ProcessFile())
      return;
    string doneFiles = (ServerServices.GetService(typeof (ICommonSettingsHolder)) as ICommonSettingsHolder).DoneFiles;
    if (Directory.Exists(doneFiles))
    {
      string destFileName = Path.Combine(doneFiles, Path.GetFileName(fileName));
      File.Move(fileName, destFileName);
    }
    else
      HelperMethods.WriteErrorMsg(this._Session.SessionGUID, LocalizationHolder.rm.GetString("ExtInt_3"));
  }

  private void ProcessResponceObjs()
  {
    foreach (long responceObjId in this.GetResponceObjIDs())
    {
      try
      {
        this.ResponceObjectProcessing(responceObjId);
      }
      catch (Exception ex)
      {
        HelperMethods.WriteErrorMsg(this._Session.SessionGUID, string.Format(LocalizationHolder.rm.GetString("ExtInt_4"), (object) responceObjId, (object) ex.Message));
      }
    }
  }

  private void ResponceObjectProcessing(long responceID)
  {
    long[] numArray = new long[0];
    List<long> longList = new List<long>();
    IResponceObject responceObject = this._Session.GetObject(responceID, true) as IResponceObject;
    try
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      responceObject = responceObject.CheckOut() as IResponceObject;
      HelperMethods.SetObjectStatus(this._Session.SessionGUID, responceObject.ObjectID, StatusEnum.Work);
      foreach (long objectID1 in responceObject.ConfigElementLink)
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        try
        {
          IResponceConfigObject responceConfigObject = objectID1 != 0L ? this._Session.GetObject(objectID1, true) as IResponceConfigObject : throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_15"), (object) objectID1));
          int finderId = responceConfigObject.FinderID;
          Tuple<int, int>[] array = ((IEnumerable<string>) responceConfigObject.AttributeComprasion).Where<string>((System.Func<string, bool>) (attrPairStr => !string.IsNullOrEmpty(attrPairStr))).Select<string, string[]>((System.Func<string, string[]>) (attrPairStr => attrPairStr.Split(new char[1]
          {
            '='
          }, StringSplitOptions.RemoveEmptyEntries))).Where<string[]>((System.Func<string[], bool>) (attrPairStr => attrPairStr.Length == 2)).Select<string[], Tuple<int, int>>((System.Func<string[], Tuple<int, int>>) (attrPair => new Tuple<int, int>(int.Parse(attrPair[0]), int.Parse(attrPair[1])))).ToArray<Tuple<int, int>>(responceConfigObject.AttributeComprasion.Length);
          if (finderId != Const.RequestIDAttrTypeID)
          {
            int objectTypeId = MetaDataHelper.GetObjectTypeID((this._Session.GetObject(responceConfigObject.ObjTypeSettingItemObjectID, true) as IObjTypeSettingItemObject).ObjTypeGUID);
            if (objectTypeId == 0)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_14"), (object) responceConfigObject.ObjectID, (object) responceConfigObject.Caption));
            foreach (long objectID2 in this.GetObjsForUpdate(responceObject, finderId, objectTypeId))
            {
              try
              {
                IDBObject destinationObject = this._Session.GetObject(objectID2, true);
                try
                {
                  if (destinationObject.CheckoutBy != 0L)
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_12"), (object) destinationObject.CheckoutBy));
                  this.SetAttrsToDestinationObject(responceObject, destinationObject, array);
                }
                catch (Exception ex)
                {
                  stringBuilder2.AppendLine(string.Format(LocalizationHolder.rm.GetString("ExtInt_13"), (object) destinationObject.ObjectID, (object) destinationObject.Caption, (object) ex.Message));
                }
                finally
                {
                  longList.Add(destinationObject.ObjectID);
                }
              }
              catch (Exception ex)
              {
                stringBuilder2.AppendLine(ex.Message);
              }
            }
          }
          else
          {
            foreach (Tuple<long, long> tuple in ((IEnumerable<long>) this.GetRequestIDs(responceObject, finderId)).SelectNotNull<long, IRequestObject>((System.Func<long, IRequestObject>) (requestID => this._Session.GetObject(requestID) as IRequestObject)).Select<IRequestObject, Tuple<long, long>>((System.Func<IRequestObject, Tuple<long, long>>) (requestObj => new Tuple<long, long>(requestObj.ObjectID, requestObj.SourceObjectLink))).Where<Tuple<long, long>>((System.Func<Tuple<long, long>, bool>) (requestObjID => requestObjID.Item1 != 0L && requestObjID.Item2 != 0L && this._Session.GetObject(requestObjID.Item2, false) != null && this._Session.GetObject(requestObjID.Item2).CheckoutBy == 0L)).ToArray<Tuple<long, long>>())
            {
              try
              {
                IDBObject destinationObject = this._Session.GetObject(tuple.Item2, true);
                try
                {
                  if (destinationObject.CheckoutBy != 0L)
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_12"), (object) destinationObject.CheckoutBy));
                  this.SetAttrsToDestinationObject(responceObject, destinationObject, array);
                  HelperMethods.SetObjectStatus(this._Session.SessionGUID, tuple.Item1, StatusEnum.ResponceRecive);
                }
                catch (Exception ex)
                {
                  stringBuilder2.AppendLine(string.Format(LocalizationHolder.rm.GetString("ExtInt_13"), (object) destinationObject.ObjectID, (object) destinationObject.Caption, (object) ex.Message));
                }
                finally
                {
                  longList.Add(destinationObject.ObjectID);
                }
              }
              catch (Exception ex)
              {
                stringBuilder2.AppendLine(ex.Message);
              }
            }
          }
          if (stringBuilder2.Length != 0)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_16"), (object) responceConfigObject.ObjectID, (object) responceConfigObject.Caption, (object) stringBuilder2.ToString()));
        }
        catch (Exception ex)
        {
          stringBuilder1.AppendLine(ex.Message);
        }
      }
      if (stringBuilder1.Length != 0)
        throw new Exception(stringBuilder1.ToString());
      HelperMethods.SetObjectStatus(this._Session.SessionGUID, responceObject.ObjectID, StatusEnum.Done);
    }
    catch (Exception ex)
    {
      HelperMethods.SetObjectStatus(this._Session.SessionGUID, responceObject.ObjectID, StatusEnum.Error);
      HelperMethods.AddErrorText(this._Session.SessionGUID, responceObject.ObjectID, ex.Message);
    }
    finally
    {
      responceObject.DestinationObjectsLink = longList.ToArray();
      responceObject.CheckIn();
    }
  }

  private long[] GetRequestIDs(IResponceObject responceObject, int finderAttrID)
  {
    long[] numArray = new long[0];
    long int64 = Convert.ToInt64(responceObject.Attributes.FindByID(finderAttrID).Value);
    return this._Session.GetObjectCollection(Const.RequestObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(finderAttrID, RelationalOperators.Equal, (object) int64, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "Request objects for update")).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))).ToArray<long>();
  }

  private long[] GetObjsForUpdate(IResponceObject responceObject, int finderAttrID, int objTypeID)
  {
    long[] objsForUpdate = new long[0];
    IDBObjectCollection objectCollection = this._Session.GetObjectCollection(objTypeID);
    IDBAttribute byId = responceObject.Attributes.FindByID(finderAttrID);
    if (byId != null)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(finderAttrID, RelationalOperators.Equal, byId.Value, LogicalOperators.NONE, 0, false)
      }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "Objects for update");
      objsForUpdate = objectCollection.Select(paramSet).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))).ToArray<long>();
    }
    return objsForUpdate;
  }

  private void SetAttrsToDestinationObject(
    IResponceObject sourceObject,
    IDBObject destinationObject,
    Tuple<int, int>[] attrPairIDs)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Tuple<int, int> attrPairId in attrPairIDs)
    {
      try
      {
        IDBAttribute byId = sourceObject.Attributes.FindByID(attrPairId.Item1);
        if (byId == null)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_8"), (object) attrPairId.Item1));
        IDBAttribute dbAttribute = destinationObject.Attributes.AddAttribute(attrPairId.Item2, false);
        if (dbAttribute == null)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_9"), (object) attrPairId.Item2));
        if (dbAttribute.AttributeType.IsContent)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_10"), (object) MetaDataHelper.GetObjectTypeName(destinationObject.ObjectType), (object) dbAttribute.Name));
        if ((dbAttribute.AttributeType.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("ExtInt_10"), (object) MetaDataHelper.GetObjectTypeName(destinationObject.ObjectType), (object) dbAttribute.Name));
        dbAttribute.Value = byId.Value;
      }
      catch (Exception ex)
      {
        stringBuilder.AppendLine(ex.Message);
      }
    }
    if (stringBuilder.Length > 0)
      throw new Exception(stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString());
  }

  private long[] GetResponceObjIDs()
  {
    long[] numArray = new long[0];
    return this._Session.GetObjectCollection(Const.ResponceObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Const.StatusAttrTypeID, RelationalOperators.Equal, (object) Convert.ToInt64((object) StatusEnum.Wait), LogicalOperators.AND, 0, false),
      new ConditionStructure(-6, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "MyObjects")).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))).ToArray<long>();
  }
}
