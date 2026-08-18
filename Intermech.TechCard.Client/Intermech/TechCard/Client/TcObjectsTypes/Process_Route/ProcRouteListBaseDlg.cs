// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Process_Route.ProcRouteListBaseDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Process_Route;

/// <summary>Base class for proc route dialog</summary>
public abstract class ProcRouteListBaseDlg : Form
{
  /// <summary>
  /// 
  /// </summary>
  private bool _multiSelect;
  /// <summary>Article object ids</summary>
  private readonly List<long> _objArtList;
  /// <summary>Children (for proc route) object</summary>
  protected readonly long _objChildID;
  /// <summary>Child object type - MUST be defined in derived class</summary>
  protected int _objChildTypeID = -1;
  /// <summary>Proc view list control</summary>
  private ProcRouteListControl _viewPrl;
  /// <summary>Selected objects list (by default)</summary>
  protected readonly List<long> _procRouteIDList;

  /// <summary>Initialize class data</summary>
  private void InitializeData()
  {
    if (this._objArtList == null || this._objArtList.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ProcRouteHelper.GetDefaultProcRouteForArticles((IList<long>) this._objArtList, sessionKeeper.Session);
  }

  /// <summary>Initialize custom controls</summary>
  private void InitializeControls()
  {
    this._viewPrl = new ProcRouteListControl(this._objArtList, this._multiSelect);
    this._viewPrl.CreateCustomNodeEvent += new EventHandler<NodeEventArgs>(this.DoCreateNode);
    this._viewPrl.pnlButtons.Visible = true;
    this._viewPrl.Parent = (Control) this;
    this._viewPrl.Dock = DockStyle.Fill;
    this._viewPrl.BringToFront();
    this._viewPrl.Show();
    this.CancelButton = (IButtonControl) this._viewPrl.btnCancel;
    this.AcceptButton = (IButtonControl) this._viewPrl.btnApply;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Size = new Size(420, 520);
    this.FormBorderStyle = FormBorderStyle.Sizable;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service != null)
      this.Icon = service.GetIcon(4, TechCardConsts.ObjectTypes.ProcRoutingID);
    this.Text = LocalizationHolder.rm.GetString("TechCard.Client_209");
    if (this._objArtList.Count == 1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.Text = $"{this.Text}{TechCardConsts.Utils.GetObjectString(this._objArtList[0], sessionKeeper.Session)}\"";
    }
    this.LoadSettings(true);
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected virtual void LoadSettings(bool loadFormPosition)
  {
    string name = typeof (ProcRouteListBaseDlg).ToString();
    if (loadFormPosition)
      TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.All);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name);
    if (this._viewPrl == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this._viewPrl._tolcProcRouteList);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected virtual void SaveSettings(bool saveFormPosition)
  {
    string name = typeof (ProcRouteListBaseDlg).ToString();
    if (saveFormPosition)
      TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.All);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this._viewPrl._tolcProcRouteList);
  }

  /// <summary>Load class data</summary>
  private void LoadData() => this._viewPrl.LoadData();

  /// <summary>Get child objects for proc route</summary>
  /// <param name="objChildTypeId"></param>
  /// <param name="procRouteObjId"></param>
  /// <returns></returns>
  protected static List<long> LoadChildObjects(int objChildTypeId, long procRouteObjId)
  {
    List<long> longList = new List<long>();
    if (objChildTypeId == -1)
      return longList;
    int relationTypeId = MetaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechRelationGuid);
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objChildTypeId);
    if (!childrenIdRecursive.Contains(objChildTypeId))
      childrenIdRecursive.Add(objChildTypeId);
    ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long projId = procRouteObjId;
      IUserSession session = sessionKeeper.Session;
      int[] relations = new int[1]{ relationTypeId };
      ConditionStructure[] conditions = conditionStructureArray;
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in TechCardUtils.GetChildSostavTree(projId, session, (IEnumerable<int>) relations, false, conditions))
      {
        long partId = sostavTreeItem.PartID;
        if (partId != 0L)
          longList.Add(partId);
      }
    }
    return longList;
  }

  /// <summary>Constructor</summary>
  /// <param name="objArtList">Ид. версий изделия</param>
  /// <param name="objChildId">Ид. версии дочернего объекта</param>
  /// <param name="procRouteId">Selected proc routes</param>
  protected ProcRouteListBaseDlg(List<long> objArtList, long objChildId, long[] procRouteId)
  {
    this._objArtList = new List<long>((IEnumerable<long>) objArtList);
    this._objChildID = objChildId;
    this._procRouteIDList = procRouteId != null ? new List<long>((IEnumerable<long>) procRouteId) : new List<long>();
    this.InitializeData();
    this.InitializeControls();
    this.LoadData();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.SaveSettings(true);
    base.Dispose(disposing);
  }

  /// <summary>Show dialog</summary>
  /// <returns></returns>
  public new virtual DialogResult ShowDialog()
  {
    int num = (int) base.ShowDialog();
    int dialogResult = (int) this.DialogResult;
    this.SaveSettings(true);
    return (DialogResult) dialogResult;
  }

  /// <summary>Show dialog</summary>
  /// <param name="objArtId"></param>
  /// <param name="objChildId"></param>
  /// <param name="procRouteDlgType">Class derived from ProcRouteListBaseDlg</param>
  /// <param name="procRouteId"></param>
  /// <returns></returns>
  public static bool ShowDialog(
    long objArtId,
    long objChildId,
    System.Type procRouteDlgType,
    ref long procRouteId)
  {
    long[] procRouteId1 = new long[1]{ procRouteId };
    int num = ProcRouteListBaseDlg.ShowDialog(objArtId, objChildId, procRouteDlgType, ref procRouteId1, false) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (procRouteId1.Length == 0)
      return num != 0;
    procRouteId = procRouteId1[0];
    return num != 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtId"></param>
  /// <param name="objChildId"></param>
  /// <param name="procRouteDlgType">Class derived from ProcRouteListBaseDlg</param>
  /// <param name="procRouteId"></param>
  /// <returns></returns>
  /// <param name="multiSelect"></param>
  public static bool ShowDialog(
    long objArtId,
    long objChildId,
    System.Type procRouteDlgType,
    ref long[] procRouteId,
    bool multiSelect)
  {
    if (objArtId == 0L || objChildId == -1L)
      return false;
    return ProcRouteListBaseDlg.ShowDialog(new List<long>()
    {
      objArtId
    }, objChildId, procRouteDlgType, ref procRouteId, multiSelect);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtList"></param>
  /// <param name="objChildId"></param>
  /// <param name="procRouteDlgType">Class derived from ProcRouteListBaseDlg</param>
  /// <param name="procRouteId">Selected items</param>
  /// <returns></returns>
  /// <param name="multiSelect"></param>
  public static bool ShowDialog(
    List<long> objArtList,
    long objChildId,
    System.Type procRouteDlgType,
    ref long[] procRouteId,
    bool multiSelect)
  {
    Dictionary<long, long> procRoute2ArtList;
    if (!ProcRouteListBaseDlg.ShowDialog(objArtList, objChildId, procRouteDlgType, procRouteId, multiSelect, out procRoute2ArtList))
      return false;
    List<long> longList = new List<long>((IEnumerable<long>) procRoute2ArtList.Keys);
    procRouteId = longList.ToArray();
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtList"></param>
  /// <param name="objChildId"></param>
  /// <param name="procRouteDlgType">Class derived from ProcRouteListBaseDlg</param>
  /// <param name="procRouteId">Items to select</param>
  /// <param name="procRoute2ArtList">Selected proc routes with info about their articles</param>
  /// <returns></returns>
  /// <param name="multiSelect"></param>
  public static bool ShowDialog(
    List<long> objArtList,
    long objChildId,
    System.Type procRouteDlgType,
    long[] procRouteId,
    bool multiSelect,
    out Dictionary<long, long> procRoute2ArtList)
  {
    procRoute2ArtList = (Dictionary<long, long>) null;
    System.Type c = typeof (ProcRouteListBaseDlg);
    if (!procRouteDlgType.IsSubclassOf(c))
      return false;
    if (!(Activator.CreateInstance(procRouteDlgType, (object) objArtList, (object) objChildId, (object) procRouteId) is ProcRouteListBaseDlg instance))
      return false;
    instance.MultiSelect = multiSelect;
    int num = (int) instance.ShowDialog();
    if (instance.DialogResult != DialogResult.OK)
      return false;
    procRoute2ArtList = instance._viewPrl.ProcRoute2ArtIDs;
    return true;
  }

  /// <summary>Create node event</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void DoCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
    IDBTypedObjectID dbTypedObjId;
    if (node == null || navigatorTreeView == null || TechcardClientControlsUtils.GetObjectInfo(node, out dbTypedObjId) || dbTypedObjId == null)
      return;
    long objectId = dbTypedObjId.ObjectID;
    List<long> longList = ProcRouteListBaseDlg.LoadChildObjects(this._objChildTypeID, objectId);
    if (longList != null && longList.Count > 0)
      node.CheckState = CheckState.Indeterminate;
    else
      node.CheckState = this._procRouteIDList == null || !this._procRouteIDList.Contains(objectId) ? CheckState.Unchecked : CheckState.Checked;
  }

  /// <summary>Multi select mode</summary>
  public bool MultiSelect
  {
    get => this._multiSelect;
    set
    {
      if (this._multiSelect == value)
        return;
      this._multiSelect = value;
      this._viewPrl._multiSelect = value;
    }
  }

  /// <summary>Мо по умолчанию для ДСЕ</summary>
  /// <param name="objArtId">Ид. версии изделия</param>
  /// <param name="session">Пользов. сессия</param>
  /// <returns></returns>
  [Obsolete("Use ProcRouteHelper instead", true)]
  public static long GetProcRouteDefault(long objArtId, IUserSession session)
  {
    return ProcRouteHelper.GetDefaultProcRouteForArticle(objArtId, session);
  }

  /// <summary>
  /// Получение информации об контекста изделия в текущем окне
  /// </summary>
  /// <param name="artObjId"></param>
  /// <param name="projArtObjId"></param>
  /// <param name="zakazObjId"></param>
  /// <returns></returns>
  [Obsolete("Use ProcRouteHelper instead", true)]
  public static bool GetArticleContextInfo(
    long artObjId,
    out long projArtObjId,
    out long zakazObjId)
  {
    return ProcRouteHelper.GetArticleContextInfo(artObjId, out projArtObjId, out zakazObjId);
  }

  /// <summary>Мо по умолчанию для ДСЕ</summary>
  /// <param name="objArtList">Ид. версий изделий</param>
  /// <param name="session"></param>
  /// <returns></returns>
  [Obsolete("Use ProcRouteHelper instead", true)]
  public static Dictionary<long, long> GetProcRouteDefault(
    List<long> objArtList,
    IUserSession session)
  {
    return ProcRouteHelper.GetDefaultProcRouteForArticles((IList<long>) objArtList, session);
  }

  /// <summary>Получения изделий для МО</summary>
  /// <param name="procRouteId">Ид. версии МО</param>
  /// <param name="session">Польз. сессия</param>
  /// <returns></returns>
  [Obsolete("Use ProcRouteHelper instead", true)]
  public static List<long> GetArticlesForProcRoute(long procRouteId, IUserSession session)
  {
    return ProcRouteHelper.GetArticlesForProcRoute(procRouteId, session);
  }
}
