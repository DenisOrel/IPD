// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRouteThroughBaseCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Localization;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.TcObjectsTypes.Process_Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// 
/// </summary>
internal abstract class ProcRouteThroughBaseCommand : TechCardSelectedItemsCommand
{
  /// <summary>Сквозной МО</summary>
  private ProcRouteThroughObject _throughObject;
  /// <summary>Cписок операций, допускающих добавления в сквозной МО</summary>
  protected List<ObjInfoItem> _unlinkedOperList;

  /// <summary>Перенумерация объектов</summary>
  /// <param name="session"></param>
  /// <param name="relInfoItems"></param>
  private void DoRenumerateObjects(IUserSession session, List<RelInfoItem> relInfoItems)
  {
    if (relInfoItems == null || relInfoItems.Count == 0)
      return;
    ITechNumerationService service = ServiceUtils.GetService<ITechNumerationService>((object) session, false);
    if (service == null)
      return;
    ITechNumerationSession session1 = service.CreateSession(session.SessionGUID);
    if (session1 == null)
      return;
    session1.BeginLogging();
    try
    {
      session1.NumerateObject(relInfoItems[0].RelationID, TechNumerationObjectModes.FirstObj, session.SessionGUID);
    }
    finally
    {
      ITechNumerationLog numerationLog = session1.GetNumerationLog();
      if (numerationLog != null)
      {
        if (numerationLog.ObjectsLog != null && numerationLog.ObjectsLog.Count != 0)
          this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", numerationLog.ObjectsLog, true));
        if (numerationLog.RelationsLog != null && numerationLog.RelationsLog.Count != 0)
          this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", numerationLog.RelationsLog));
      }
      service.DisposeSession(session.SessionGUID);
    }
  }

  /// <summary>
  /// Получение списка операций, требущих добавления к сквозному МО
  /// </summary>
  /// <returns></returns>
  protected abstract List<ObjInfoItem> GetOperInfo2LinkList();

  /// <summary>Конструктор</summary>
  public ProcRouteThroughBaseCommand(string name)
    : base(name)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool LoadCommandInfo()
  {
    if (!base.LoadCommandInfo())
      return false;
    this._throughObject = new ProcRouteThroughObject(this._selectedObjInfo);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._throughObject.GetUnlinkedOperObjList(sessionKeeper.Session, out this._unlinkedOperList);
    if (this._unlinkedOperList != null && this._unlinkedOperList.Count != 0)
      return true;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_19558.ssp_techcard_19559()), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    List<ObjInfoItem> operInfo2LinkList = this.GetOperInfo2LinkList();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<RelInfoItem> relInfoItemList = this._throughObject.LinkOper2ThroughObject(sessionKeeper.Session, operInfo2LinkList);
      this._createdRelInfoList.AddRange((IEnumerable<RelObjInfoItem>) relInfoItemList.Select<RelInfoItem, RelObjInfoItem>((Func<RelInfoItem, RelObjInfoItem>) (item => new RelObjInfoItem(item, this._selectedObjInfo, (ObjInfoItem) null))).ToArray<RelObjInfoItem>());
      this.DoRenumerateObjects(sessionKeeper.Session, relInfoItemList);
      return relInfoItemList.Count > 0;
    }
  }
}
