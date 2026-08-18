// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.PendingLink
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.ECO.Client;

public class PendingLink : ICloneable
{
  public long ID;
  public long verID;
  public Guid verGuid = Guid.Empty;
  private string _design = "";
  private string _invNum = "";
  public Guid mainGuid = Guid.Empty;
  public List<ObjInfo> auxObjects;
  public int objType = -1;
  public string verStr = "";
  public ECOGoal ecoGoal;
  public int stepID;
  private bool _needDelete;
  public long relId;
  public HidingType hideType;
  public bool LockMove;

  public string design
  {
    get => this._invNum != "" ? $"{this._design}   инв.№{this._invNum}" : this._design;
  }

  public void SetDesign(IDBObject dbObject)
  {
    if (dbObject == null)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_Designation);
    this._design = attributeById == null ? "" : attributeById.Description;
    if (Intermech.ECO.Client.ECO.invNumTemplate == null || !(this._design == "") && !ECOPlugin.plugin.eps.Current.PlaceInvNum)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    string str1 = Intermech.ECO.Client.ECO.invNumTemplate;
    do
    {
      int num1 = str1.IndexOf('[');
      if (num1 >= 0)
      {
        int num2 = str1.IndexOf(']', num1);
        if (num2 >= 0)
        {
          string str2 = str1.Substring(num1 + 1, num2 - num1 - 1);
          try
          {
            int int32 = Convert.ToInt32(str2);
            object[] valuesById = dbObject.GetValuesByID(int32, false);
            if (valuesById != null && valuesById.Length != 0 && valuesById[0] != DBNull.Value)
            {
              stringBuilder.Append(str1.Substring(0, num1) + Convert.ToString(valuesById[0]));
            }
            else
            {
              if (this._design != "")
                return;
              stringBuilder.Append(str1.Substring(0, num1));
            }
          }
          catch
          {
            stringBuilder.Append(str1.Substring(0, num1));
          }
          str1 = str1.Substring(num2 + 1);
        }
        else
          break;
      }
      else
        break;
    }
    while (str1 != "");
    if (str1 != "")
      stringBuilder.Append(str1);
    this._design = stringBuilder.ToString();
  }

  public bool needDelete
  {
    get => this._needDelete;
    set => this._needDelete = value;
  }

  public PendingLink(ECOGoal goal, int lcStepId)
  {
    this.ecoGoal = goal;
    this.stepID = lcStepId;
  }

  public PendingLink(long Id, string aver, ECOGoal goal)
  {
    this.verID = Math.Abs(Id);
    this.verStr = aver;
    this.ecoGoal = goal;
    this.stepID = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(Id, false);
      this.InitVars(dbObject);
      this.SetDesign(dbObject);
    }
  }

  public PendingLink(long aobjID, string aver, ECOGoal goal, int sID)
  {
    this.verID = Math.Abs(aobjID);
    this.verStr = aver;
    this.ecoGoal = goal;
    this.stepID = sID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(aobjID, false);
      this.InitVars(dbObject);
      this.SetDesign(dbObject);
    }
  }

  public PendingLink(long aobjID, string aver, ECOGoal goal, string des)
  {
    this.verID = Math.Abs(aobjID);
    this.verStr = aver;
    this.ecoGoal = goal;
    this._design = des;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(aobjID, false);
      this.InitVars(dbObject);
      this.SetDesign(dbObject);
    }
  }

  public PendingLink(Guid vGuid, string aver, ECOGoal goal)
  {
    this.verGuid = vGuid;
    this.verStr = aver;
    this.ecoGoal = goal;
    this.stepID = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.verGuid, false);
      this.InitVars(dbObject);
      this.SetDesign(dbObject);
    }
  }

  public PendingLink(Guid vGuid, string aver, ECOGoal goal, int sID)
  {
    this.verGuid = vGuid;
    this.verStr = aver;
    this.ecoGoal = goal;
    this.stepID = sID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.verGuid, false);
      this.InitVars(dbObject);
      this.SetDesign(dbObject);
    }
  }

  public PendingLink(Guid vGuid, string aver, ECOGoal goal, string des)
  {
    this.verGuid = vGuid;
    this.verStr = aver;
    this.ecoGoal = goal;
    this._design = des;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.verGuid, false);
      this.InitVars(dbObject);
      this.SetDesign(dbObject);
    }
  }

  public PendingLink(long aobjID, Guid vGuid, string aver, ECOGoal goal, string des)
  {
    this.verID = Math.Abs(aobjID);
    this.verGuid = vGuid;
    this.verStr = aver;
    this.ecoGoal = goal;
    this._design = des;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.SetDesign(sessionKeeper.Session.GetObject(this.verGuid, false));
  }

  public static string GetDesignByGuid(Guid g)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(g);
      IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(objectInfo.ObjectID, false) ?? sessionKeeper.Session.GetObjectActualCopy(-objectInfo.ObjectID, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
        if (attributeById != null)
          return attributeById.AsString;
      }
    }
    return "";
  }

  public static void InitVars(IDBObject idbO, ref long Id, ref Guid g, ref string design)
  {
    if (idbO == null)
      return;
    if (Id == 0L)
      Id = Math.Abs(idbO.ObjectID);
    if (g.Equals(Guid.Empty))
      g = idbO.ObjectGUID;
    if (!(design == ""))
      return;
    IDBAttribute attributeById = idbO.GetAttributeByID(RevHelper.idAttrDesign);
    if (attributeById == null)
      return;
    design = attributeById.AsString;
  }

  public static string GetDesignById(long Id)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(Id, false) ?? sessionKeeper.Session.GetObjectActualCopy(-Id, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
        if (attributeById != null)
          return attributeById.AsString;
      }
    }
    return "";
  }

  public static int GetObjTypeById(long Id)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo1 = sessionKeeper.Session.GetObjectInfo(Id);
      if (!objectInfo1.Empty)
        return objectInfo1.ObjectTypeID;
      if (Id > 0L)
      {
        QuickObjectInfo objectInfo2 = sessionKeeper.Session.GetObjectInfo(-Id);
        if (!objectInfo2.Empty)
          return objectInfo2.ObjectTypeID;
      }
    }
    return -1;
  }

  public void UpdateDesign()
  {
    if (this._design != null && !(this._design == ""))
      return;
    this._design = PendingLink.GetDesignById(this.verID);
  }

  public void UpdateObjType()
  {
    if (this.objType != -1)
      return;
    this.objType = PendingLink.GetObjTypeById(this.verID);
  }

  public bool IsAuxObject(Guid g)
  {
    if (this.auxObjects == null)
      return false;
    foreach (ObjInfo auxObject in this.auxObjects)
    {
      if (auxObject.verGuid.Equals(g))
        return true;
    }
    return false;
  }

  public void AddManyAuxObjects(List<long> objList)
  {
    if (this.auxObjects == null)
      this.auxObjects = new List<ObjInfo>();
    objList.ForEach((Action<long>) (Id => this.auxObjects.Add(new ObjInfo(Id))));
  }

  public void AddAuxObject(Guid g)
  {
    if (this.auxObjects == null)
    {
      this.auxObjects = new List<ObjInfo>();
      this.auxObjects.Add(new ObjInfo(g));
    }
    else
      this.auxObjects.Add(new ObjInfo(g));
  }

  public bool HasThisObject(Guid g) => this.verGuid.Equals(g) || this.IsAuxObject(g);

  public bool IsAuxObject(long Id)
  {
    if (this.auxObjects == null)
      return false;
    foreach (ObjInfo auxObject in this.auxObjects)
    {
      if (Math.Abs(auxObject.verId) == Math.Abs(Id))
        return true;
    }
    return false;
  }

  public void AddAuxObject(long Id)
  {
    if (this.auxObjects == null)
    {
      this.auxObjects = new List<ObjInfo>();
      this.auxObjects.Add(new ObjInfo(Id));
    }
    else
      this.auxObjects.Add(new ObjInfo(Id));
  }

  public bool HasThisObject(long Id)
  {
    return Math.Abs(this.verID) == Math.Abs(Id) || this.IsAuxObject(Id);
  }

  public void InitVars(IDBObject idbO)
  {
    if (idbO == null)
      return;
    if (this.verID == 0L)
      this.verID = Math.Abs(idbO.ObjectID);
    if (this.verGuid.Equals(Guid.Empty))
      this.verGuid = idbO.ObjectGUID;
    if (!(this.design == "") || Intermech.ECO.Client.ECO.invNumTemplate != null && ECOPlugin.plugin.eps.Current.PlaceInvNum)
      return;
    IDBAttribute attributeById = idbO.GetAttributeByID(RevHelper.idAttrDesign);
    if (attributeById == null)
      return;
    this._design = attributeById.AsString;
  }

  public long UpdateRelId(IUserSession ius, long ecoId)
  {
    if (this.relId == 0L)
      this.relId = RevHelper.GetRevRelation(ecoId, this.verID);
    return this.relId;
  }

  public object Clone()
  {
    PendingLink pendingLink = new PendingLink(this.ecoGoal, this.stepID);
    pendingLink._design = this._design;
    pendingLink._invNum = this._invNum;
    pendingLink.needDelete = this.needDelete;
    pendingLink.verGuid = this.verGuid;
    pendingLink.verID = this.verID;
    pendingLink.verStr = this.verStr;
    if (this.auxObjects != null)
    {
      pendingLink.auxObjects = new List<ObjInfo>();
      foreach (ObjInfo auxObject in this.auxObjects)
        pendingLink.auxObjects.Add((ObjInfo) auxObject.Clone());
    }
    return (object) pendingLink;
  }
}
