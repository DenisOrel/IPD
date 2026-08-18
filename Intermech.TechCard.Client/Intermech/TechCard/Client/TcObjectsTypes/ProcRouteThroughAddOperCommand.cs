// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRouteThroughAddOperCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// 
/// </summary>
internal class ProcRouteThroughAddOperCommand : ProcRouteThroughBaseCommand
{
  /// <summary>Конструктор</summary>
  public ProcRouteThroughAddOperCommand()
    : base("throughAddOperNode")
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<ObjInfoItem> GetOperInfo2LinkList()
  {
    List<long> selectObjIDs = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.OperaciyaID, (IList<ObjInfoItem>) this._unlinkedOperList, LocalizationHolder.rm.GetString("TechCard.Client_335"), LocalizationHolder.rm.GetString(sc_19556.ssp_techcard_19557()));
    return this._unlinkedOperList.Where<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => selectObjIDs.Contains(item.ObjectID))).ToList<ObjInfoItem>();
  }
}
