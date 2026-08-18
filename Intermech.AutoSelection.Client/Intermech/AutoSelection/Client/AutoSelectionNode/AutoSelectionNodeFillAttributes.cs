// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeFillAttributes
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionNodeFillAttributes : AutoSelectionNodeItemFillAttributes
{
  private void InitializeData() => this._type = AutoSelectionNodeType.FillAttributes;

  public AutoSelectionNodeFillAttributes(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this.InitializeData();
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    if (selExecuteStatus != AutoSelExecuteStatus.Applied)
      return selExecuteStatus;
    ObjInfoItem targetObjInfo = asSession.TargetObjInfo;
    ObjInfoItem targetProjInfo = asSession.TargetProjInfo;
    if ((TypedInfoItem) targetObjInfo == (TypedInfoItem) null)
    {
      string data = LocalizationHolder.rm.GetString("AutoSelection.Client_101");
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data);
      return selExecuteStatus;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(targetObjInfo.ObjectID, false);
      if (dbObject == null)
        return selExecuteStatus;
      this.AttributesObjectSetDefault(asSession, dbObject, (List<AutoSelAttrVal>) this._defObjAttrList);
      this.AttributesCalc(asSession, (IDBAttributable) dbObject, (List<AutoSelAttr>) this._calcObjAttrList);
      if ((TypedInfoItem) targetProjInfo != (TypedInfoItem) null)
      {
        IDBRelation relation = session.GetRelation(targetProjInfo.ObjectID, targetObjInfo.ObjectID, true);
        if (relation != null)
        {
          this.AttributesRelationSetDefault(asSession, relation, (List<AutoSelAttrVal>) this._defRelAttrList);
          this.AttributesCalc(asSession, (IDBAttributable) relation, (List<AutoSelAttr>) this._calcRelAttrList);
        }
      }
    }
    return AutoSelExecuteStatus.Applied;
  }
}
