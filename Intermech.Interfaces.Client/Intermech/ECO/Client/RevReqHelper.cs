// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevReqHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ECO.Client;

public class RevReqHelper
{
  public static readonly string guidAttrRevNeed = "cad0077a-306c-11d8-b4e9-00304f19f545";
  public static readonly int idAttrRevNeed = 0;
  public static readonly string guidAttrNewRevNeed = "cad01524-306c-11d8-b4e9-00304f19f545";
  public static readonly int idAttrNewRevNeed = 0;
  public static readonly string guidAttrDelWhenExcluded = "cad00073-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_II = "cad00349-306c-11d8-b4e9-00304f19f545";
  public Dictionary<int, Dictionary<int, ReqRevision>> reqRevs;
  public static readonly RevReqHelper Global = new RevReqHelper();

  public event CreateVersionHandler CreateVersion;

  static RevReqHelper()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      RevReqHelper.idAttrNewRevNeed = sessionKeeper.Session.GetAttributeType(new Guid(RevReqHelper.guidAttrNewRevNeed)).AttributeID;
      RevReqHelper.idAttrRevNeed = sessionKeeper.Session.GetAttributeType(new Guid(RevReqHelper.guidAttrRevNeed)).AttributeID;
    }
  }

  private RevReqHelper() => this.reqRevs = new Dictionary<int, Dictionary<int, ReqRevision>>();

  public static bool PluginLoaded()
  {
    return RevReqHelper.Global.CreateVersion.GetInvocationList().Length != 0;
  }

  public bool CanCreateVersion(long objectID, ReqRevision rr)
  {
    Delegate[] invocationList = this.CreateVersion.GetInvocationList();
    if (invocationList.Length == 0)
      return false;
    bool version = true;
    foreach (CreateVersionHandler createVersionHandler in invocationList)
    {
      version = version && createVersionHandler(new List<long>()
      {
        objectID
      }, rr);
      if (!version)
        break;
    }
    return version;
  }

  /// <summary>
  /// Вернуть словарь "тип объекта - настройка" для данного шага ЖЦ
  /// </summary>
  /// <param name="LCId">ИД шага ЖЦ</param>
  /// <returns>Словарь соответствий "тип объекта - настройка"</returns>
  public static Dictionary<int, ReqRevision> GetRRDictionary(int LCId)
  {
    if (RevReqHelper.Global.reqRevs.ContainsKey(LCId))
      return RevReqHelper.Global.reqRevs[LCId];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject containerForLcStep = (session.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForLCStep((object) session.SessionGUID, LCId);
      Dictionary<int, ReqRevision> rrDictionary = (Dictionary<int, ReqRevision>) null;
      if (containerForLcStep != null)
      {
        IDBAttribute attributeById = containerForLcStep.GetAttributeByID(RevReqHelper.idAttrNewRevNeed);
        if (attributeById != null)
          rrDictionary = ReqRevisionClass.LoadAttrValues(attributeById);
      }
      else
        rrDictionary = new Dictionary<int, ReqRevision>();
      RevReqHelper.Global.reqRevs.Add(LCId, rrDictionary);
      return rrDictionary;
    }
  }

  public static ReqRevision GetRevReq(int LCId, int objTypeId)
  {
    Dictionary<int, ReqRevision> rrDictionary = RevReqHelper.GetRRDictionary(LCId);
    if (rrDictionary == null)
      return ReqRevision.NoRevision;
    ReqRevision defaultValue = ReqRevisionClass.GetDefaultValue(objTypeId);
    if (rrDictionary.ContainsKey(objTypeId))
      defaultValue = rrDictionary[objTypeId];
    if (defaultValue != ReqRevision.Inherited)
    {
      if (!rrDictionary.ContainsKey(objTypeId))
        rrDictionary.Add(objTypeId, defaultValue);
      return defaultValue;
    }
    if (rrDictionary.Count > 0)
    {
      while (objTypeId != -1)
      {
        objTypeId = MetaDataHelper.GetObjectTypeParentID(objTypeId);
        ReqRevision revReq;
        if (rrDictionary.ContainsKey(objTypeId))
        {
          revReq = rrDictionary[objTypeId];
        }
        else
        {
          ReqRevisionClass reqRevisionClass = new ReqRevisionClass(LCId, objTypeId, false);
          revReq = reqRevisionClass.Value;
          rrDictionary.Add(objTypeId, reqRevisionClass.Value);
        }
        if (revReq != ReqRevision.Inherited)
          return revReq;
      }
    }
    return ReqRevision.NoRevision;
  }

  public static void SetRevReq(int LCId, int objTypeId, ReqRevision rr)
  {
    Dictionary<int, ReqRevision> dictionary;
    if (RevReqHelper.Global.reqRevs.ContainsKey(LCId))
    {
      dictionary = RevReqHelper.Global.reqRevs[LCId];
      if (dictionary == null)
      {
        dictionary = new Dictionary<int, ReqRevision>();
        RevReqHelper.Global.reqRevs[LCId] = dictionary;
      }
    }
    else
    {
      dictionary = new Dictionary<int, ReqRevision>();
      RevReqHelper.Global.reqRevs.Add(LCId, dictionary);
    }
    if (dictionary.ContainsKey(objTypeId))
      dictionary[objTypeId] = rr;
    else
      dictionary.Add(objTypeId, rr);
  }
}
