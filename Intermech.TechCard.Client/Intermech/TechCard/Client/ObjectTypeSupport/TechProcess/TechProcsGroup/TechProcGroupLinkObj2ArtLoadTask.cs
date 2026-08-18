// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcsGroup.TechProcGroupLinkObj2ArtLoadTask
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.TechCard.Client.BackgroundTask;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcsGroup;

/// <summary>
/// Класс загрузки списка деталей для злемента ГТП (списка привязок)
/// </summary>
internal class TechProcGroupLinkObj2ArtLoadTask : TechCardBackgroundTask
{
  /// <summary>Ид. связи с текущим элементом</summary>
  private RelInfoItem _gtpElemRelInfo;
  /// <summary>Список маршрутов обработки для текущего ГТП</summary>
  private List<ObjInfoItem> _procRouteIDs = new List<ObjInfoItem>();
  /// <summary>ГТП + связи с ЕТП</summary>
  private Gtp2EtpRefData _gtp2EtpRefData;
  /// <summary>Состав ГТП</summary>
  private List<TechCardUtils.SostavTreeItem> _gtpSostavItems;
  /// <summary>Диалог привязки</summary>
  private TechProcGroupLinkObj2ArtDialog _techProcGroupArtDlg;
  /// <summary>
  /// Кеш key = proc route id, value - данные о связях состава ГТП с объектами ЕТП
  /// </summary>
  private Dictionary<ObjInfoItem, List<Gtp2EtpRefObjData>> _procRoute2RefData = new Dictionary<ObjInfoItem, List<Gtp2EtpRefObjData>>();

  /// <summary>Загрузка информации по МО</summary>
  /// <param name="procRouteInfo"></param>
  /// <param name="relGuid2IdCache"></param>
  /// <param name="session"></param>
  private void LoadProcRouteData(
    ObjInfoItem procRouteInfo,
    Dictionary<Guid, RelInfoItem> relGuid2IdCache,
    IUserSession session)
  {
    List<int> intList1 = new List<int>();
    intList1.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.MarshrObrabID));
    intList1.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID));
    List<int> intList2 = new List<int>();
    intList2.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TechProcEdinID));
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      procRouteInfo
    }, (IEnumerable<int>) intList2.ToArray(), (IEnumerable<int>) intList1.ToArray(), (IEnumerable<int>) TechCardConsts.RelTypes.TechAllRelationTypes.ToArray<int>(), ObjInfoDbScheme.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, true, false, -1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
    DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    if (source != null)
      new ObjInfoDbScheme().ParseItems(source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objInfoItemList);
    ObjInfoItem etpRootObjInfo = objInfoItemList.FirstOrDefault<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => this._gtp2EtpRefData.ObjRefIDs.ContainsValue((TypedInfoItem) item)));
    if ((TypedInfoItem) etpRootObjInfo == (TypedInfoItem) null)
      return;
    List<Gtp2EtpRefData> etpRelIdList = TechProcGroupUtils.GetEtpRelIDList(relGuid2IdCache, etpRootObjInfo, session);
    Dictionary<RelInfoItem, Gtp2EtpRefData> dictionary = new Dictionary<RelInfoItem, Gtp2EtpRefData>();
    foreach (Gtp2EtpRefData gtp2EtpRefData in etpRelIdList)
    {
      if (gtp2EtpRefData != null)
        dictionary[gtp2EtpRefData.ItemInfo as RelInfoItem] = gtp2EtpRefData;
    }
    List<Gtp2EtpRefObjData> gtp2EtpRefObjDataList = new List<Gtp2EtpRefObjData>();
    Gtp2EtpRefObjData gtp2EtpRefObjData1 = new Gtp2EtpRefObjData((TypedInfoItem) this._techProcGroupArtDlg.GtpObjectInfo, GtpRefDataType.gritGtpObject, (Dictionary<TypedInfoItem, TypedInfoItem>) null, (TechCardUtils.SostavTreeItem) null);
    gtp2EtpRefObjData1.ObjRefIDs.Add((TypedInfoItem) new RelInfoItem(0L), (TypedInfoItem) etpRootObjInfo);
    gtp2EtpRefObjDataList.Add(gtp2EtpRefObjData1);
    foreach (TechCardUtils.SostavTreeItem gtpSostavItem in this._gtpSostavItems)
    {
      if (gtpSostavItem != null && gtpSostavItem.LinkID != 0L)
      {
        RelInfoItem relInfoItem = new RelInfoItem(gtpSostavItem.LinkID, gtpSostavItem.LinkTypeID);
        if (dictionary.ContainsKey(relInfoItem))
        {
          gtp2EtpRefObjDataList.Add(new Gtp2EtpRefObjData(dictionary[relInfoItem], gtpSostavItem));
        }
        else
        {
          Gtp2EtpRefObjData gtp2EtpRefObjData2 = new Gtp2EtpRefObjData((TypedInfoItem) relInfoItem, GtpRefDataType.gritGtpRelation, (Dictionary<TypedInfoItem, TypedInfoItem>) null, gtpSostavItem);
          gtp2EtpRefObjDataList.Add(gtp2EtpRefObjData2);
        }
      }
    }
    this._procRoute2RefData[procRouteInfo] = gtp2EtpRefObjDataList;
  }

  /// <summary>Обновление статусов дерева</summary>
  /// <param name="procRoute"></param>
  private void UpdateCheckState(object procRoute)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new TechProcGroupLinkObj2ArtLoadTask.InvokeObjCallback(this.UpdateCheckState), procRoute);
    }
    else
    {
      ObjInfoItem key = (ObjInfoItem) null;
      if (procRoute.GetType().Equals(typeof (long)))
        key = new ObjInfoItem((long) procRoute);
      else if (procRoute is ObjInfoItem)
        key = procRoute as ObjInfoItem;
      if ((TypedInfoItem) key == (TypedInfoItem) null || key.ObjectID == 0L || !this._procRoute2RefData.ContainsKey(key))
        return;
      foreach (TreeListNode node1 in this._techProcGroupArtDlg._techProcGroupASV.tlArts.Nodes)
      {
        foreach (TreeListNode node2 in node1.Nodes)
        {
          if (!((TypedInfoItem) ((EtpProcRoute2ArtInfo) node2.Tag).ObjProcRouteInfo != (TypedInfoItem) key))
          {
            CheckState checkState = CheckState.Unchecked;
            foreach (Gtp2EtpRefObjData gtp2EtpRefObjData in this._procRoute2RefData[key])
            {
              if (gtp2EtpRefObjData.ItemInfo.Equals((TypedInfoItem) this._gtpElemRelInfo))
              {
                checkState = gtp2EtpRefObjData.ObjRefIDs.Count > 0 ? CheckState.Checked : CheckState.Unchecked;
                break;
              }
            }
            node2.CheckState = checkState;
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="data"></param>
  private void UpdateRefData(object data)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new TechProcGroupLinkObj2ArtLoadTask.InvokeObjCallback(this.UpdateRefData), data);
    }
    else
    {
      if (!(data is Dictionary<ObjInfoItem, List<Gtp2EtpRefObjData>>))
        return;
      this._techProcGroupArtDlg.ProcRoute2RefData = data as Dictionary<ObjInfoItem, List<Gtp2EtpRefObjData>>;
    }
  }

  /// <summary>Constructor</summary>
  /// <param name="techProcGroupArtDlg"></param>
  public TechProcGroupLinkObj2ArtLoadTask(TechProcGroupLinkObj2ArtDialog techProcGroupArtDlg)
    : base(LocalizationHolder.rm.GetString("TechCard.Client_287"))
  {
    if (techProcGroupArtDlg == null)
      return;
    this._gtpElemRelInfo = techProcGroupArtDlg.GtpElemRelInfo;
    this._gtpSostavItems = techProcGroupArtDlg.GtpSostavItems;
    this._procRouteIDs = techProcGroupArtDlg.ProcRouteIDs;
    this._techProcGroupArtDlg = techProcGroupArtDlg;
    string empty = string.Empty;
    this._name = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_288"), (object) empty);
    this._minValue = 0;
    this._maxValue = this._procRouteIDs.Count;
    this._canTerminate = true;
    this._thread = new Thread(new ThreadStart(((CustomThreadBackgroundTask) this).ThreadProc));
    this.Start();
  }

  /// <summary>Основная процедура потока</summary>
  protected override void CustomThreadProc()
  {
    this._event.WaitOne();
    List<RelInfoItem> relInfoList = new List<RelInfoItem>();
    foreach (TechCardUtils.SostavTreeItem gtpSostavItem in this._gtpSostavItems)
    {
      if (gtpSostavItem != null && gtpSostavItem.LinkID != 0L)
        relInfoList.Add(new RelInfoItem(gtpSostavItem.LinkID, gtpSostavItem.LinkTypeID));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<Guid, RelInfoItem> relationGuid2Id = TechCardUtils.GetRelationGuid2Id(relInfoList, sessionKeeper.Session);
      this._gtp2EtpRefData = TechProcGroupUtils.GetEtpObjIDList(this._techProcGroupArtDlg.GtpObjectInfo, TechCardConsts.ObjectTypes.TechProcEdinGUID, sessionKeeper.Session);
      if (this._gtp2EtpRefData == null || this._gtp2EtpRefData.ObjRefIDs.Count == 0)
        return;
      this.UpdateRefData((object) this._procRoute2RefData);
      for (int index = 0; index < this._procRouteIDs.Count; ++index)
      {
        this.Value = (object) index;
        this._event.WaitOne();
        ObjInfoItem procRouteId = this._procRouteIDs[index];
        if (!((TypedInfoItem) procRouteId == (TypedInfoItem) null) && procRouteId.ObjectID != 0L)
        {
          this.LoadProcRouteData(procRouteId, relationGuid2Id, sessionKeeper.Session);
          this.UpdateCheckState((object) procRouteId);
        }
      }
    }
  }

  /// <summary>Internal delegate for invoke</summary>
  internal delegate void InvokeCallback();

  /// <summary>Internal delegate for invoke</summary>
  /// <param name="data"></param>
  internal delegate void InvokeObjCallback(object data);
}
