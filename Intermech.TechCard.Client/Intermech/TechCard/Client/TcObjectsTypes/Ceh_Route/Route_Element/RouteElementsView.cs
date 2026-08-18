// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Element.RouteElementsView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Ceh_Route;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.Tools.Controls;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Element;

/// <summary>Route template view control</summary>
/// <summary>RouteTemplateView</summary>
public class RouteElementsView : TechCardBaseView
{
  /// <summary>Возможность редактирования свойств объекта</summary>
  private bool _canEdit;
  /// <summary>Контейнер РЭ</summary>
  private CehRouteElementContainer _cehRouteElementContainer;
  /// <summary>TechCard Navigator контрол</summary>
  private TechNavigatorControl _techNavControl;
  /// <summary>Image List</summary>
  /// <remarks>Чтобы каждый раз не лазить в ресурсы - объявим его статическим</remarks>
  private static ImageList _imageList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip toolStrip1;
  private ToolStripButton tsbtnAdd;
  private ToolStripButton tsbtnInsert;
  private ToolStripButton tsbtnApplyUpdates;
  private ToolStripSeparator tsSep1;
  private ToolStripButton tsbtnDelete;
  private ToolStripButton tsbtnClear;
  private ToolStripSeparator tsSep2;
  private ToolStripButton tsbtnFilter;
  private ToolStripSeparator tsmiSep2;
  private ToolStripMenuItem tsmiClear;
  private SplitContainer splContainer;
  private ContextMenuStrip cmsCehRoutes;
  private ToolStripMenuItem tsmiAdd;
  private ToolStripMenuItem tsmiInsert;
  private ToolStripMenuItem tsmiApplyUpdates;
  private ToolStripMenuItem tsmiMove;
  private ToolStripMenuItem tsmiMoveFirst;
  private ToolStripMenuItem tsmiMoveUp;
  private ToolStripMenuItem tsmiMoveDown;
  private ToolStripMenuItem tsmiMoveLast;
  private ToolStripSeparator tsmiSep1;
  private ToolStripMenuItem tsmiDelete;
  private Panel pnlList;
  internal TechCardNavObjListControl tcnolcElemRoutes;
  private ToolStripMenuItem tsmiReplace;

  /// <summary>Инициализация контролов</summary>
  protected override void InitializeCustomControls()
  {
    this.InitializeComponent();
    base.InitializeCustomControls();
    this._techNavControl = new TechNavigatorControl();
    this.pnlList.Controls.Add((Control) this._techNavControl);
    this._techNavControl.Dock = DockStyle.Fill;
    this._techNavControl.BringToFront();
    this._techNavControl.DoubleClick += new TechNavigatorEventHandler(this.TechNavControlDoubleClickEvent);
    this._techNavControl.KeyDown += new KeyEventHandler(this.TechNavControlKeyDownEvent);
    this._techNavControl.Location = new Point(8, 8);
    this._techNavControl.Name = "techNavControl";
    this._techNavControl.ViewsManager.AllowedViews = new string[2]
    {
      "ChildrenView",
      "SelectionViewObject"
    };
    this._techNavControl.TabIndex = 0;
    this.tcnolcElemRoutes.DisableColumnsGrouping = true;
    this.tcnolcElemRoutes.DisableToolBar = false;
    this.tcnolcElemRoutes.DisableHeaderContextMenu = false;
    this.tcnolcElemRoutes.DisableColumnsSorting = true;
    this.tcnolcElemRoutes.DisableManualSortingSetup = true;
    this.tcnolcElemRoutes.ViewContentType = ContentType.Folders;
    if (this.DesignMode)
      return;
    this.tsbtnApplyUpdates.Visible = false;
    if (RouteElementsView._imageList.Images.Count > 0)
      this.tsbtnAdd.Image = RouteElementsView._imageList.Images[0];
    if (RouteElementsView._imageList.Images.Count > 1)
      this.tsbtnInsert.Image = RouteElementsView._imageList.Images[1];
    if (RouteElementsView._imageList.Images.Count > 2)
      this.tsbtnApplyUpdates.Image = RouteElementsView._imageList.Images[2];
    if (RouteElementsView._imageList.Images.Count > 3)
      this.tsbtnDelete.Image = RouteElementsView._imageList.Images[3];
    if (RouteElementsView._imageList.Images.Count <= 4)
      return;
    this.tsbtnClear.Image = RouteElementsView._imageList.Images[4];
  }

  /// <summary>Инициализация сообщений</summary>
  protected override void InitializeCustomMessages()
  {
    base.InitializeCustomMessages();
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_541");
    this._locMessageTxt = LocalizationHolder.rm.GetString("TechCard.Client_151");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  protected override void InitServices(System.IServiceProvider services)
  {
    base.InitServices(services);
  }

  /// <summary>
  /// Загрузка всех возможных шаблонов элементов (заполнение дерева навигатора)
  /// </summary>
  private void LoadElemRoutesAllList()
  {
    this._techNavControl.SelectedItemsChanged -= new EventHandler(this.Routes_WT_SelectedItemsChangedEvent);
    try
    {
      this._techNavControl.RootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(TechCardConsts.ObjectTypes.ElemRouteTemplateId);
    }
    finally
    {
      this._techNavControl.SelectedItemsChanged += new EventHandler(this.Routes_WT_SelectedItemsChangedEvent);
    }
  }

  /// <summary>Загрузка элементов для текущего шаблона</summary>
  /// <param name="needUpdateControls"></param>
  private void LoadElemRoutes4TemplateList(bool needUpdateControls)
  {
    List<long> objIdList = new List<long>();
    Dictionary<int, CehRouteElementClass> dictionary = new Dictionary<int, CehRouteElementClass>();
    int key = 0;
    this._cehRouteElementContainer.RouteElementList.Sort((Comparison<CehRouteElementClass>) ((item1, item2) => item1.OrderID.CompareTo(item2.OrderID)));
    foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) this._cehRouteElementContainer.RouteElementList)
    {
      if (!routeElement.Deleted)
      {
        objIdList.Add(routeElement.ObjID);
        dictionary.Add(key, routeElement);
        ++key;
      }
    }
    List<int> intList = new List<int>();
    CehRouteElementClass rowRouteElemData;
    foreach (int rowIndex in this.tcnolcElemRoutes.SelectedPositions.Values)
    {
      rowRouteElemData = this.GetRowRouteElemData(rowIndex);
      if (rowRouteElemData != null)
      {
        int num = this._cehRouteElementContainer.RouteElementList.IndexOf(rowRouteElemData);
        if (num != -1)
          intList.Add(num);
      }
    }
    this.tcnolcElemRoutes.Grid.BeginUpdate();
    try
    {
      this.tcnolcElemRoutes.LoadData(objIdList, TechCardConsts.ObjectTypes.ElemRouteID, TechObjectListMode.MultiValue);
      this.tcnolcElemRoutes.Activate((IView) null);
    }
    finally
    {
      if (intList.Count > 0)
      {
        this.tcnolcElemRoutes.GridDeselectAll(true);
        foreach (int index in intList)
        {
          if (index != -1 && index < this.tcnolcElemRoutes.Grid.Rows.Count)
          {
            iGRow row = this.tcnolcElemRoutes.Grid.Rows[index];
            this.tcnolcElemRoutes.GridSelectRowCells(row, true);
            this.tcnolcElemRoutes.Grid.CurRow = row;
          }
        }
      }
      this.tcnolcElemRoutes.Grid.EndUpdate();
    }
    for (int index = 0; index < this.tcnolcElemRoutes.Grid.Rows.Count; ++index)
    {
      if (dictionary.TryGetValue(index, out rowRouteElemData))
      {
        ChildrenViewRowData rowData = this.tcnolcElemRoutes.GetRowData(index);
        if (rowData != null)
          rowData.Tag = (object) rowRouteElemData;
      }
    }
    if (!needUpdateControls)
      return;
    this.UpdateControls();
  }

  /// <summary>Add new route element</summary>
  private void AddRouteElem()
  {
    if (!this.CanModify || !this.CanAddRouteElem())
      return;
    this.InsertRouteElem(((INodeView) this.tcnolcElemRoutes).Count);
  }

  /// <summary>Insert new route element</summary>
  /// <param name="index"></param>
  private void InsertRouteElem(int index)
  {
    if (!this.CanModify || !this.CanAddRouteElem())
      return;
    ISelectedItems selectedItems = this._techNavControl.ItemsHost?.SelectedItems;
    if (selectedItems == null)
      return;
    int count = selectedItems.Count;
    if (count == 0)
      return;
    if (index > this.tcnolcElemRoutes.Grid.Rows.Count)
      index = this.tcnolcElemRoutes.Grid.Rows.Count;
    long num1 = 0;
    long num2 = 1000000;
    bool flag = false;
    if (((INodeView) this.tcnolcElemRoutes).Count > 0)
    {
      CehRouteElementClass rowRouteElemData1 = this.GetRowRouteElemData((index < this.tcnolcElemRoutes.Grid.Rows.Count ? this.tcnolcElemRoutes.Grid.Rows[index] : this.tcnolcElemRoutes.Grid.Rows[index - 1]).Index);
      if (rowRouteElemData1 != null)
        num1 = rowRouteElemData1.OrderID;
      if (index == 0)
      {
        num2 = num1 / (long) (count + 1);
        num1 = 0L;
      }
      else if (index < ((INodeView) this.tcnolcElemRoutes).Count)
      {
        long num3 = num1;
        num1 = 0L;
        CehRouteElementClass rowRouteElemData2 = this.GetRowRouteElemData(this.tcnolcElemRoutes.Grid.Rows[index - 1].Index);
        if (rowRouteElemData2 != null)
          num1 = rowRouteElemData2.OrderID;
        long num4 = num1;
        num2 = (num3 - num4) / (long) (count + 1);
      }
      if (num2 == 0L)
      {
        num2 = 1000000L;
        flag = true;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechUtilsService service = ServiceUtils.GetService<ITechUtilsService>((object) sessionKeeper.Session, true);
      TemplRouteClass templRouteClass = new TemplRouteClass(this._cehRouteElementContainer.ObjID, this._cehRouteElementContainer.LinkID);
      for (int index1 = 0; index1 < selectedItems.Count; ++index1)
      {
        if (selectedItems.GetItemData(index1, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          RouteElementsView.BlankCehRouteElemClass cehRouteElemClass1 = new RouteElementsView.BlankCehRouteElemClass();
          cehRouteElemClass1.OrderID = num1 + (long) (index1 + 1) * num2;
          RouteElementsView.BlankCehRouteElemClass cehRouteElemClass2 = cehRouteElemClass1;
          if (MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, TechCardConsts.ObjectTypes.ElemRouteID))
            cehRouteElemClass2.ObjID = itemData.ObjectID;
          else if (MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, TechCardConsts.ObjectTypes.ElemRouteTemplateId))
          {
            IDBObject dbObject = service.CreateObject(TechCardConsts.ObjectTypes.ElemRouteID, itemData.ObjectID, sessionKeeper.Session.SessionGUID);
            cehRouteElemClass2.ObjID = dbObject != null ? dbObject.ObjectID : 0L;
            templRouteClass.RouteElementList.Add((CehRouteElementClass) cehRouteElemClass2);
          }
          if (cehRouteElemClass2.ObjID != 0L)
            this._cehRouteElementContainer.RouteElementList.Insert(index + index1, (CehRouteElementClass) cehRouteElemClass2);
        }
      }
      if (templRouteClass.RouteElementList.Count != 0)
        templRouteClass.SaveData(sessionKeeper.Session);
    }
    if (flag)
    {
      for (int index2 = index + count; index2 < this._cehRouteElementContainer.RouteElementList.Count - 1; ++index2)
      {
        CehRouteElementClass routeElement = this._cehRouteElementContainer.RouteElementList[index2];
        if (routeElement != null)
          routeElement.OrderID = (long) (index2 + 1) * 1000000L;
      }
    }
    this.Modified = true;
    this.LoadElemRoutes4TemplateList(true);
  }

  /// <summary>Remove route element</summary>
  private bool RemoveRouteElem(List<int> rowIdx2Remove)
  {
    if (!this.CanModify || rowIdx2Remove == null || rowIdx2Remove.Count == 0)
      return false;
    foreach (int index in rowIdx2Remove)
    {
      iGRow row = this.tcnolcElemRoutes.Grid.Rows[index];
      if (row != null)
      {
        CehRouteElementClass rowRouteElemData = this.GetRowRouteElemData(row.Index);
        if (rowRouteElemData != null)
        {
          rowRouteElemData.Deleted = true;
          this.Modified = true;
        }
      }
    }
    return true;
  }

  /// <summary>Replace route elem</summary>
  private void ReplaceRouteElem()
  {
    if (!this.CanModify || !this.CanAddRouteElem())
      return;
    List<int> selectedRows = this.GetSelectedRows();
    if (selectedRows == null || selectedRows.Count != 1)
      return;
    ISelectedItems selectedItems = this._techNavControl.ItemsHost?.SelectedItems;
    if (selectedItems == null || selectedItems.Count == 0 || !this.RemoveRouteElem(selectedRows))
      return;
    this.InsertRouteElem(selectedRows.First<int>());
  }

  /// <summary>Clear route element</summary>
  private void ClearRouteElem()
  {
    if (!this.CanModify)
      return;
    foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) this._cehRouteElementContainer.RouteElementList)
    {
      if (routeElement != null)
      {
        routeElement.Deleted = true;
        this.Modified = true;
      }
    }
    this.LoadElemRoutes4TemplateList(true);
  }

  /// <summary>Check edit mode</summary>
  /// <returns></returns>
  private bool CanAddRouteElem()
  {
    if (!(this._techNavControl.ActiveViewPage?.View is ISelectedItemsHost view))
      return false;
    ISelectedItems selectedItems = view.SelectedItems;
    bool flag = selectedItems.Count > 0;
    if (!flag)
      return false;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (!(selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectID == 0L)
      {
        flag = false;
        break;
      }
      if (!MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, TechCardConsts.ObjectTypes.ElemRouteID) && !MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, TechCardConsts.ObjectTypes.ElemRouteTemplateId))
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  /// <summary>Move route elem first</summary>
  private void RouteElemMoveFirst()
  {
    if (!this.CanModify)
      return;
    List<int> selectedRows = this.GetSelectedRows();
    if (selectedRows == null || selectedRows.Count != 1)
      return;
    int sourceIdx = selectedRows[0];
    switch (sourceIdx)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        this.RouteElemMove(sourceIdx, 0);
        break;
    }
  }

  /// <summary>Move route element up</summary>
  private void RouteElemMoveUp()
  {
    if (!this.CanModify)
      return;
    List<int> selectedRows = this.GetSelectedRows();
    if (selectedRows == null || selectedRows.Count != 1)
      return;
    int sourceIdx = selectedRows[0];
    switch (sourceIdx)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        this.RouteElemMove(sourceIdx, sourceIdx - 1);
        break;
    }
  }

  /// <summary>Move route element down</summary>
  private void RouteElemMoveDown()
  {
    if (!this.CanModify)
      return;
    List<int> selectedRows = this.GetSelectedRows();
    if (selectedRows == null || selectedRows.Count != 1)
      return;
    int sourceIdx = selectedRows[0];
    if (sourceIdx == -1 || sourceIdx == ((INodeView) this.tcnolcElemRoutes).Count - 1)
      return;
    this.RouteElemMove(sourceIdx, sourceIdx + 1);
  }

  /// <summary>Move route element last</summary>
  private void RouteElemMoveLast()
  {
    if (!this.CanModify)
      return;
    List<int> selectedRows = this.GetSelectedRows();
    if (selectedRows == null || selectedRows.Count != 1)
      return;
    int sourceIdx = selectedRows[0];
    if (sourceIdx == -1 || sourceIdx == ((INodeView) this.tcnolcElemRoutes).Count - 1)
      return;
    this.RouteElemMove(sourceIdx, ((INodeView) this.tcnolcElemRoutes).Count - 1);
  }

  /// <summary>Move route element</summary>
  /// <param name="sourceIdx"></param>
  /// <param name="destinationIdx"></param>
  private void RouteElemMove(int sourceIdx, int destinationIdx)
  {
    if (!this.CanModify || sourceIdx < 0 || sourceIdx == destinationIdx || destinationIdx >= ((INodeView) this.tcnolcElemRoutes).Count)
      return;
    this.tcnolcElemRoutes.Grid.BeginUpdate();
    try
    {
      int num = sourceIdx < destinationIdx ? 1 : -1;
      for (int index = sourceIdx; index != destinationIdx; index += num)
      {
        iGRow row1 = this.tcnolcElemRoutes.Grid.Rows[index];
        iGRow row2 = this.tcnolcElemRoutes.Grid.Rows[index + num];
        CehRouteElementClass rowRouteElemData1 = this.GetRowRouteElemData(row1.Index);
        CehRouteElementClass rowRouteElemData2 = this.GetRowRouteElemData(row2.Index);
        if (rowRouteElemData1 == null || rowRouteElemData2 == null)
          break;
        long orderId = rowRouteElemData1.OrderID;
        rowRouteElemData1.OrderID = rowRouteElemData2.OrderID;
        rowRouteElemData2.OrderID = orderId;
        row1.Index = index + num;
      }
    }
    finally
    {
      this.tcnolcElemRoutes.Grid.EndUpdate();
      this.Modified = true;
      this.UpdateControls();
    }
  }

  /// <summary>Get selected object's rows indexes</summary>
  /// <returns></returns>
  private List<int> GetSelectedRows()
  {
    List<int> selectedRows = new List<int>();
    Dictionary<INodeID, int> selectedPositions = this.tcnolcElemRoutes.SelectedPositions;
    if (selectedPositions != null && selectedPositions.Count != 0)
    {
      foreach (KeyValuePair<INodeID, int> keyValuePair in selectedPositions)
      {
        if (keyValuePair.Key != null)
          selectedRows.Add(keyValuePair.Value);
      }
    }
    return selectedRows;
  }

  /// <summary>Получение данных РЭ для строки</summary>
  /// <param name="rowIndex"></param>
  /// <returns></returns>
  private CehRouteElementClass GetRowRouteElemData(int rowIndex)
  {
    return this.tcnolcElemRoutes.GetRowData(rowIndex)?.Tag as CehRouteElementClass;
  }

  /// <summary>Загрузка image list из ресурсов</summary>
  /// <param name="forceMode">Режим принудительной загрузки / обновления</param>
  private static void LoadImagesFromResources(bool forceMode)
  {
    if (RouteElementsView._imageList != null && !forceMode)
      return;
    RouteElementsView._imageList = new ImageList();
    string[] strArray = new string[5]
    {
      "Intermech.TechCard.Client.Resources.ElemRouteAdd.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteInsert.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteChange.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteDel.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteClean.bmp"
    };
    foreach (string resourceName in strArray)
      RouteElementsView.LoadImageFromResources(resourceName);
  }

  /// <summary>Загрузка image из ресурсов</summary>
  /// <param name="resourceName">Наименование ресурса</param>
  private static void LoadImageFromResources(string resourceName)
  {
    if (resourceName == string.Empty)
      return;
    Bitmap bitmap = ResourceHolder.LoadImageFromResources(resourceName);
    if (bitmap == null)
      return;
    RouteElementsView._imageList.Images.AddStrip((Image) bitmap);
  }

  /// <summary>Can modifying flag</summary>
  public override bool CanModify => base.CanModify && this._canEdit;

  /// <summary>Загрузить информацию в control</summary>
  protected override void LoadData()
  {
    this._canEdit = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this._objID != 0L)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objID, false);
        if (dbObject != null)
          this._canEdit = dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.CheckoutBy == sessionKeeper.Session.UserID;
      }
      this._cehRouteElementContainer = CehRouteElementFactory.Instance.CreateItem(new ObjInfoItem(this._objID, this._objTypeID));
      this._cehRouteElementContainer.LinkID = this._prjLinkID;
      this._cehRouteElementContainer.LoadData(sessionKeeper.Session);
    }
    this.LoadElemRoutesAllList();
    this.LoadElemRoutes4TemplateList(false);
    base.LoadData();
  }

  /// <summary>Сохранить информацию из контрола</summary>
  /// <param name="sendNotifications">Необходимость отправки уведомлений</param>
  protected override void SaveData(bool sendNotifications = true)
  {
    if (!this.Modified)
      return;
    List<CategoryValue> categoryValueList = (List<CategoryValue>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = false;
      if (sendNotifications && !sessionKeeper.Session.IsStartedLogHistory)
      {
        sessionKeeper.Session.StartLogHistory();
        flag = true;
      }
      try
      {
        this._cehRouteElementContainer.SaveData(sessionKeeper.Session);
        this.UpdateRouteString(sessionKeeper.Session);
      }
      finally
      {
        if (flag)
        {
          sessionKeeper.Session.StopLogHistory();
          categoryValueList = sessionKeeper.Session.GetModificationsHistoryList();
        }
      }
    }
    base.SaveData(sendNotifications);
    if (!sendNotifications || categoryValueList == null || !categoryValueList.Any<CategoryValue>())
      return;
    NotificationQueue notificationQueue = new NotificationQueue();
    foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents((IList<CategoryValue>) categoryValueList))
      notificationQueue.QueueEvent(notificationEvent);
    notificationQueue.FlushQueue();
  }

  /// <summary>Обновление строки расцеховки</summary>
  /// <param name="session"></param>
  private void UpdateRouteString(IUserSession session)
  {
    ICehRouteStringService service = ServiceUtils.GetService<ICehRouteStringService>((object) session, false);
    if (service == null)
      return;
    if (MetaDataHelper.IsObjectTypeChildOf(this._objTypeID, TechCardConsts.ObjectTypes.CehRouteID))
    {
      service.CreateCehRouteString(this._objID, session.SessionGUID);
    }
    else
    {
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID).ToArray(), (object) null, LogicalOperators.NONE, 0, false)
      };
      DataTable parentSostavData = DataHelper.GetParentSostavData(new ObjInfoItem(this._objID, this._objTypeID), session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) null);
      if (parentSostavData == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
        service.CreateCehRouteString(DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L), session.SessionGUID);
    }
  }

  /// <summary>Отмена изменений</summary>
  protected override void CancelChanges()
  {
    if (!this.Modified)
      return;
    CehRouteElementContainer elementContainer = CehRouteElementFactory.Instance.CreateItem(new ObjInfoItem(this._objID, this._objTypeID));
    elementContainer.LinkID = this._prjLinkID;
    foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) this._cehRouteElementContainer.RouteElementList)
    {
      if (routeElement is RouteElementsView.BlankCehRouteElemClass cehRouteElemClass)
      {
        cehRouteElemClass.Deleted = true;
        elementContainer.RouteElementList.Add((CehRouteElementClass) cehRouteElemClass);
      }
    }
    if (elementContainer.RouteElementList.Count != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        elementContainer.SaveData(sessionKeeper.Session);
    }
    this.LoadData();
  }

  /// <summary>Обновить состояние элементов управления закладки</summary>
  protected override void UpdateControls()
  {
    base.UpdateControls();
    this.tsbtnApplyUpdates.Enabled = this.tsmiApplyUpdates.Enabled = this.Modified;
    this.tsbtnAdd.Enabled = this.tsbtnInsert.Enabled = this.tsmiMove.Enabled = this.CanModify;
    bool flag = this.CanModify && ((INodeView) this.tcnolcElemRoutes).Count > 0;
    this.tsbtnClear.Enabled = this.tsmiClear.Enabled = flag;
    this.tsbtnDelete.Enabled = this.tsmiDelete.Enabled = flag;
    ISelectedItems selectedItems = this.tcnolcElemRoutes.SelectedItems;
    this.tsmiMove.Enabled = this.tsmiReplace.Enabled = flag && selectedItems != null && selectedItems.Count == 1;
    this.tsbtnFilter.Enabled = this._cehRouteElementContainer.RouteElementList.Count > 0;
  }

  protected override void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    base.NotificationEventFired(sender, e);
    this.tcnolcElemRoutes.NotificationEventFired(sender, e);
  }

  /// <summary>Конструктор</summary>
  /// <remarks>Добавлен для инициализации изображений</remarks>
  static RouteElementsView() => RouteElementsView.LoadImagesFromResources(false);

  /// <summary>ImageIndex</summary>
  public override int ImageIndex => -1;

  /// <summary>OrderID</summary>
  public override int OrderID => 0;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Routes_WT_SelectedItemsChangedEvent(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsCehRoutes_Opening(object sender, CancelEventArgs e)
  {
    this.UpdateControls();
    if (!this.tsmiMove.Enabled)
      return;
    int num = -1;
    List<int> selectedRows = this.GetSelectedRows();
    if (selectedRows.Count == 1)
      num = selectedRows[0];
    this.tsmiMoveFirst.Enabled = this.tsmiMoveUp.Enabled = num > 0;
    this.tsmiMoveDown.Enabled = this.tsmiMoveLast.Enabled = num != -1 && num < this.tcnolcElemRoutes.Grid.Rows.Count - 1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiAdd_Click(object sender, EventArgs e) => this.AddRouteElem();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiInsert_Click(object sender, EventArgs e)
  {
    if (this.tcnolcElemRoutes.Grid.CurRow == null)
      this.AddRouteElem();
    else
      this.InsertRouteElem(this.tcnolcElemRoutes.Grid.CurRow.Index);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiReplace_Click(object sender, EventArgs e) => this.ReplaceRouteElem();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiDelete_Click(object sender, EventArgs e)
  {
    if (!this.RemoveRouteElem(this.GetSelectedRows()))
      return;
    this.LoadElemRoutes4TemplateList(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiClear_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString(sc_19451.ssp_techcard_19452()), LocalizationHolder.rm.GetString("TechCard.Client_149"), MessageBoxButtons.YesNo) == DialogResult.No)
      return;
    this.ClearRouteElem();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiMoveFirst_Click(object sender, EventArgs e) => this.RouteElemMoveFirst();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiMoveUp_Click(object sender, EventArgs e) => this.RouteElemMoveUp();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiMoveDown_Click(object sender, EventArgs e) => this.RouteElemMoveDown();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiMoveLast_Click(object sender, EventArgs e) => this.RouteElemMoveLast();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiApplyUpdates_Click(object sender, EventArgs e) => this.SaveData(true);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechNavControlDoubleClickEvent(object sender, TechNavigatorEventArgs e)
  {
    if (this._techNavControl.ActiveViewPage == null || !(this._techNavControl.ActiveViewPage.Name == "ChildrenView"))
      return;
    this.AddRouteElem();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechNavControlKeyDownEvent(object sender, KeyEventArgs e)
  {
    if (sender == null || e == null || e.KeyCode != Keys.Return || e.Alt || e.Shift || e.Control)
      return;
    this.AddRouteElem();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RouteElementsView));
    this.splContainer = new SplitContainer();
    this.tcnolcElemRoutes = new TechCardNavObjListControl();
    this.cmsCehRoutes = new ContextMenuStrip(this.components);
    this.tsmiAdd = new ToolStripMenuItem();
    this.tsmiInsert = new ToolStripMenuItem();
    this.tsmiReplace = new ToolStripMenuItem();
    this.tsmiApplyUpdates = new ToolStripMenuItem();
    this.tsmiMove = new ToolStripMenuItem();
    this.tsmiMoveFirst = new ToolStripMenuItem();
    this.tsmiMoveUp = new ToolStripMenuItem();
    this.tsmiMoveDown = new ToolStripMenuItem();
    this.tsmiMoveLast = new ToolStripMenuItem();
    this.tsmiSep1 = new ToolStripSeparator();
    this.tsmiDelete = new ToolStripMenuItem();
    this.tsmiClear = new ToolStripMenuItem();
    this.tsmiSep2 = new ToolStripSeparator();
    this.pnlList = new Panel();
    this.toolStrip1 = new ToolStrip();
    this.tsbtnAdd = new ToolStripButton();
    this.tsbtnInsert = new ToolStripButton();
    this.tsbtnApplyUpdates = new ToolStripButton();
    this.tsSep1 = new ToolStripSeparator();
    this.tsbtnDelete = new ToolStripButton();
    this.tsbtnClear = new ToolStripButton();
    this.tsSep2 = new ToolStripSeparator();
    this.tsbtnFilter = new ToolStripButton();
    this.pnButtons.SuspendLayout();
    this.splContainer.BeginInit();
    this.splContainer.Panel1.SuspendLayout();
    this.splContainer.Panel2.SuspendLayout();
    this.splContainer.SuspendLayout();
    this.cmsCehRoutes.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    componentResourceManager.ApplyResources((object) this.splContainer, "splContainer");
    this.splContainer.Name = "splContainer";
    this.splContainer.Panel1.Controls.Add((Control) this.tcnolcElemRoutes);
    this.splContainer.Panel2.Controls.Add((Control) this.pnlList);
    this.splContainer.Panel2.Controls.Add((Control) this.toolStrip1);
    this.tcnolcElemRoutes.AllowCustomGroupValues = true;
    this.tcnolcElemRoutes.AllowEditing = true;
    this.tcnolcElemRoutes.Control = (object) this.tcnolcElemRoutes;
    this.tcnolcElemRoutes.CustomContextMenuStrip = this.cmsCehRoutes;
    this.tcnolcElemRoutes.DisableColumnsGrouping = true;
    this.tcnolcElemRoutes.DisableColumnsSorting = true;
    this.tcnolcElemRoutes.DisableGroupBox = true;
    this.tcnolcElemRoutes.DisableIMContextMenu = true;
    this.tcnolcElemRoutes.DisableKeyDownEvents = false;
    this.tcnolcElemRoutes.DisableKeyUpEvents = true;
    this.tcnolcElemRoutes.DisableStatusBar = true;
    componentResourceManager.ApplyResources((object) this.tcnolcElemRoutes, "tcnolcElemRoutes");
    this.tcnolcElemRoutes.EditingMode = false;
    this.tcnolcElemRoutes.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.tcnolcElemRoutes.Name = "tcnolcElemRoutes";
    this.tcnolcElemRoutes.ViewContentType = ContentType.NonFolders;
    this.cmsCehRoutes.Items.AddRange(new ToolStripItem[9]
    {
      (ToolStripItem) this.tsmiAdd,
      (ToolStripItem) this.tsmiInsert,
      (ToolStripItem) this.tsmiReplace,
      (ToolStripItem) this.tsmiApplyUpdates,
      (ToolStripItem) this.tsmiMove,
      (ToolStripItem) this.tsmiSep1,
      (ToolStripItem) this.tsmiDelete,
      (ToolStripItem) this.tsmiClear,
      (ToolStripItem) this.tsmiSep2
    });
    this.cmsCehRoutes.Name = "cmsCehRoutes";
    componentResourceManager.ApplyResources((object) this.cmsCehRoutes, "cmsCehRoutes");
    this.cmsCehRoutes.Opening += new CancelEventHandler(this.cmsCehRoutes_Opening);
    componentResourceManager.ApplyResources((object) this.tsmiAdd, "tsmiAdd");
    this.tsmiAdd.Name = "tsmiAdd";
    this.tsmiAdd.Click += new EventHandler(this.tsmiAdd_Click);
    componentResourceManager.ApplyResources((object) this.tsmiInsert, "tsmiInsert");
    this.tsmiInsert.Name = "tsmiInsert";
    this.tsmiInsert.Click += new EventHandler(this.tsmiInsert_Click);
    componentResourceManager.ApplyResources((object) this.tsmiReplace, "tsmiReplace");
    this.tsmiReplace.Name = "tsmiReplace";
    this.tsmiReplace.Click += new EventHandler(this.tsmiReplace_Click);
    this.tsmiApplyUpdates.Name = "tsmiApplyUpdates";
    componentResourceManager.ApplyResources((object) this.tsmiApplyUpdates, "tsmiApplyUpdates");
    this.tsmiApplyUpdates.Click += new EventHandler(this.tsmiApplyUpdates_Click);
    this.tsmiMove.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiMoveFirst,
      (ToolStripItem) this.tsmiMoveUp,
      (ToolStripItem) this.tsmiMoveDown,
      (ToolStripItem) this.tsmiMoveLast
    });
    this.tsmiMove.Name = "tsmiMove";
    componentResourceManager.ApplyResources((object) this.tsmiMove, "tsmiMove");
    this.tsmiMoveFirst.Name = "tsmiMoveFirst";
    componentResourceManager.ApplyResources((object) this.tsmiMoveFirst, "tsmiMoveFirst");
    this.tsmiMoveFirst.Click += new EventHandler(this.tsmiMoveFirst_Click);
    this.tsmiMoveUp.Name = "tsmiMoveUp";
    componentResourceManager.ApplyResources((object) this.tsmiMoveUp, "tsmiMoveUp");
    this.tsmiMoveUp.Click += new EventHandler(this.tsmiMoveUp_Click);
    this.tsmiMoveDown.Name = "tsmiMoveDown";
    componentResourceManager.ApplyResources((object) this.tsmiMoveDown, "tsmiMoveDown");
    this.tsmiMoveDown.Click += new EventHandler(this.tsmiMoveDown_Click);
    this.tsmiMoveLast.Name = "tsmiMoveLast";
    componentResourceManager.ApplyResources((object) this.tsmiMoveLast, "tsmiMoveLast");
    this.tsmiMoveLast.Click += new EventHandler(this.tsmiMoveLast_Click);
    this.tsmiSep1.Name = "tsmiSep1";
    componentResourceManager.ApplyResources((object) this.tsmiSep1, "tsmiSep1");
    this.tsmiDelete.Name = "tsmiDelete";
    componentResourceManager.ApplyResources((object) this.tsmiDelete, "tsmiDelete");
    this.tsmiDelete.Click += new EventHandler(this.tsmiDelete_Click);
    this.tsmiClear.Name = "tsmiClear";
    componentResourceManager.ApplyResources((object) this.tsmiClear, "tsmiClear");
    this.tsmiClear.Click += new EventHandler(this.tsmiClear_Click);
    this.tsmiSep2.Name = "tsmiSep2";
    componentResourceManager.ApplyResources((object) this.tsmiSep2, "tsmiSep2");
    componentResourceManager.ApplyResources((object) this.pnlList, "pnlList");
    this.pnlList.Name = "pnlList";
    this.toolStrip1.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.tsbtnAdd,
      (ToolStripItem) this.tsbtnInsert,
      (ToolStripItem) this.tsbtnApplyUpdates,
      (ToolStripItem) this.tsSep1,
      (ToolStripItem) this.tsbtnDelete,
      (ToolStripItem) this.tsbtnClear,
      (ToolStripItem) this.tsSep2,
      (ToolStripItem) this.tsbtnFilter
    });
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.Name = "toolStrip1";
    this.tsbtnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnAdd, "tsbtnAdd");
    this.tsbtnAdd.Name = "tsbtnAdd";
    this.tsbtnAdd.Click += new EventHandler(this.tsmiAdd_Click);
    this.tsbtnInsert.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnInsert, "tsbtnInsert");
    this.tsbtnInsert.Name = "tsbtnInsert";
    this.tsbtnInsert.Click += new EventHandler(this.tsmiInsert_Click);
    this.tsbtnApplyUpdates.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnApplyUpdates, "tsbtnApplyUpdates");
    this.tsbtnApplyUpdates.Name = "tsbtnApplyUpdates";
    this.tsbtnApplyUpdates.Click += new EventHandler(this.tsmiApplyUpdates_Click);
    this.tsSep1.Name = "tsSep1";
    componentResourceManager.ApplyResources((object) this.tsSep1, "tsSep1");
    this.tsbtnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnDelete, "tsbtnDelete");
    this.tsbtnDelete.Name = "tsbtnDelete";
    this.tsbtnDelete.Click += new EventHandler(this.tsmiDelete_Click);
    this.tsbtnClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnClear, "tsbtnClear");
    this.tsbtnClear.Name = "tsbtnClear";
    this.tsbtnClear.Click += new EventHandler(this.tsmiClear_Click);
    this.tsSep2.Name = "tsSep2";
    componentResourceManager.ApplyResources((object) this.tsSep2, "tsSep2");
    this.tsbtnFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnFilter, "tsbtnFilter");
    this.tsbtnFilter.Name = "tsbtnFilter";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splContainer);
    this.Name = nameof (RouteElementsView);
    this.Tag = (object) " ";
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.Controls.SetChildIndex((Control) this.splContainer, 0);
    this.pnButtons.ResumeLayout(false);
    this.splContainer.Panel1.ResumeLayout(false);
    this.splContainer.Panel2.ResumeLayout(false);
    this.splContainer.Panel2.PerformLayout();
    this.splContainer.EndInit();
    this.splContainer.ResumeLayout(false);
    this.cmsCehRoutes.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>Класс для хранения "заготовки" РЭ</summary>
  /// <remarks>
  /// </remarks>
  private class BlankCehRouteElemClass : CehRouteElementClass
  {
  }
}
