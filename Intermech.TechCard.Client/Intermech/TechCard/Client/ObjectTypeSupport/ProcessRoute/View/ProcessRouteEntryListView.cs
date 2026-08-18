// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.View.ProcessRouteEntryListView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.Navigator;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.View;

/// <summary>Закладка "Входимости маршрута обработки"</summary>
/// <summary>RouteTemplateView</summary>
public class ProcessRouteEntryListView : TechCardBaseView, ICommandsFilter
{
  /// <summary>Контейнер служб</summary>
  private readonly IServiceContainer _serviceContainer = (IServiceContainer) new AdvancedServiceContainer();
  /// <summary>Возможность редактирования свойств объекта</summary>
  private bool _canEdit;
  /// <summary>Высота встроенного вида</summary>
  private int _embeddedViewHeight;
  /// <summary>Флаг настроек "Открывать встроенный вид"</summary>
  private bool _isOpenEmbeddedView;
  /// <summary>Флаг выполненного открытия</summary>
  private bool _isOpenedEmbeddedView;
  /// <summary>Image List</summary>
  /// <remarks>Чтобы каждый раз не лазить в ресурсы - объявим его статическим</remarks>
  private static ImageList _imageList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal TechCardNavObjListControl tcnolcRouteEntries;

  private void InitializeCustomServices()
  {
    this._serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInViews));
    this._serviceContainer.StackLocalContextCommandsFilter((ICommandsFilter) this);
    this._serviceContainer.AddService(typeof (INavigatorVirtualColumnProvider), (object) new ProcRouteEntryColumnProvider());
  }

  /// <summary>Инициализация контролов</summary>
  protected override void InitializeCustomControls()
  {
    this.InitializeCustomServices();
    this.InitializeComponent();
    base.InitializeCustomControls();
    this.tcnolcRouteEntries.CustomContextMenuStrip = (ContextMenuStrip) null;
    this.tcnolcRouteEntries.DisableIMContextMenu = false;
    this.tcnolcRouteEntries.DisableToolBar = false;
    this.tcnolcRouteEntries.DisableStatusBar = false;
    this.tcnolcRouteEntries.DisableColumnsGrouping = false;
    this.pnButtons.Enabled = false;
    this.pnButtons.Visible = false;
    this.tcnolcRouteEntries.ViewContentType = ContentType.Folders;
    int num = this.DesignMode ? 1 : 0;
  }

  /// <summary>Инициализация сообщений</summary>
  protected override void InitializeCustomMessages()
  {
    base.InitializeCustomMessages();
    this._caption = "Входимости маршрута обработки";
  }

  /// <summary>Загрузка списка входимостей для текущего объекта</summary>
  /// <param name="needUpdateControls"></param>
  private void LoadProcRouteEntryData(bool needUpdateControls)
  {
    Dictionary<INodeID, int> selectedPositions = this.tcnolcRouteEntries.SelectedPositions;
    this.tcnolcRouteEntries.Grid.BeginUpdate();
    try
    {
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingEntryID).ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
      };
      int versionsObjectNode = Intermech.Navigator.Consts.CategoryVersionsObjectNode;
      int objTypeId = this._objTypeID;
      long objId = this._objID;
      int procRoutingEntryId = TechCardConsts.ObjectTypes.ProcRoutingEntryID;
      int techRelationId = TechCardConsts.RelTypes.TechRelationID;
      string empty = string.Empty;
      TechCompositionConditionFilter compositionConditionFilter = new TechCompositionConditionFilter((IEnumerable<ConditionStructure>) conditions);
      compositionConditionFilter.QueryFilter = (IRelatedObjectQueryFilterMode) new RelatedObjectQueryFilterMode(filterDataByVersionRule: false);
      this.tcnolcRouteEntries.LoadData((IDescriptor) new TechCompositionDescriptor(versionsObjectNode, objTypeId, objId, procRoutingEntryId, techRelationId, empty, RelatedObjectsRole.Composition, (ITechCompositionFilter) compositionConditionFilter), (System.IServiceProvider) this._serviceContainer);
      this.tcnolcRouteEntries.Activate((IView) null);
    }
    finally
    {
      int count = selectedPositions.Count;
      this.tcnolcRouteEntries.Grid.EndUpdate();
    }
    if (!needUpdateControls)
      return;
    this.UpdateControls();
  }

  /// <summary>Открыть встроенный Вид</summary>
  private void OpenEmbeddedViews()
  {
    IEmbeddedViews tcnolcRouteEntries;
    if ((TechCardNavObjListControl) (tcnolcRouteEntries = (IEmbeddedViews) this.tcnolcRouteEntries) == null || tcnolcRouteEntries.IsOpen)
      return;
    this.tcnolcRouteEntries.OpenEmbeddedViews(this._embeddedViewHeight);
    this._isOpenedEmbeddedView = true;
  }

  /// <summary>Загрузка image list из ресурсов</summary>
  /// <param name="forceMode">Режим принудительной загрузки / обновления</param>
  private static void LoadImagesFromResources(bool forceMode)
  {
    if (ProcessRouteEntryListView._imageList != null && !forceMode)
      return;
    ProcessRouteEntryListView._imageList = new ImageList();
    string[] strArray = new string[5]
    {
      "Intermech.TechCard.Client.Resources.ElemRouteAdd.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteInsert.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteChange.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteDel.bmp",
      "Intermech.TechCard.Client.Resources.ElemRouteClean.bmp"
    };
    foreach (string resourceName in strArray)
      ProcessRouteEntryListView.LoadImageFromResources(resourceName);
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
    ProcessRouteEntryListView._imageList.Images.AddStrip((Image) bitmap);
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
    }
    this.LoadProcRouteEntryData(false);
    base.LoadData();
  }

  /// <summary>Сохранить информацию из контрола</summary>
  protected override void SaveData(bool sendNotifications = true)
  {
    if (!this.Modified)
      return;
    base.SaveData(sendNotifications);
  }

  /// <summary>Отмена изменений</summary>
  protected override void CancelChanges()
  {
    if (!this.Modified)
      return;
    this.LoadData();
  }

  public override void Activate(IView previousView)
  {
    if (ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service)
    {
      this._embeddedViewHeight = (int) service.ReadInteger("TechCard.Client", this.GetType().Name, "EMVAbsHeight", 0L, DBConfigMode.UserOnly);
      this._isOpenEmbeddedView = service.ReadBool("TechCard.Client", this.GetType().Name, "IsOpen", false, DBConfigMode.UserOnly);
    }
    base.Activate(previousView);
  }

  protected override void OnLayout(LayoutEventArgs levent)
  {
    base.OnLayout(levent);
    if (!this._isOpenEmbeddedView || this._isOpenedEmbeddedView || this.Height <= this._embeddedViewHeight)
      return;
    this.OpenEmbeddedViews();
  }

  public override void Deactivate(IView nextView)
  {
    bool flag = this.tcnolcRouteEntries != null && ((IEmbeddedViews) this.tcnolcRouteEntries).IsOpen;
    if (flag != this._isOpenEmbeddedView || this._embeddedViewHeight != this.tcnolcRouteEntries.EMVAbsHeight)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBConfigurations configurations = sessionKeeper.Session.Configurations;
        if (this.tcnolcRouteEntries != null)
        {
          if (configurations != null)
          {
            configurations.WriteBool("TechCard.Client", this.GetType().Name, "IsOpen", flag);
            configurations.WriteInteger("TechCard.Client", this.GetType().Name, "EMVAbsHeight", (long) this.tcnolcRouteEntries.EMVAbsHeight);
          }
        }
      }
    }
    base.Deactivate(nextView);
  }

  /// <summary>Конструктор</summary>
  /// <remarks>Добавлен для инициализации изображений</remarks>
  static ProcessRouteEntryListView() => ProcessRouteEntryListView.LoadImagesFromResources(false);

  /// <summary>ImageIndex</summary>
  public override int ImageIndex => -1;

  /// <summary>OrderID</summary>
  public override int OrderID => 0;

  protected override void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    base.NotificationEventFired(sender, e);
    this.tcnolcRouteEntries.NotificationEventFired(sender, e);
  }

  public void FilterCommands(
    ISelectedItems items,
    IEnumerable<CommandAndVisibleStatus> commandWithVisibleStatuses)
  {
    foreach (CommandAndVisibleStatus withVisibleStatuse in commandWithVisibleStatuses)
    {
      if (withVisibleStatuse.IsVisible)
      {
        string name = withVisibleStatuse.Name;
        if (name.StartsWith("add") && !name.Equals("add" + (object) TechCardConsts.ObjectTypes.ProcRoutingEntryID))
          withVisibleStatuse.IsVisible = false;
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcessRouteEntryListView));
    this.tcnolcRouteEntries = new TechCardNavObjListControl();
    this.pnButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.tcnolcRouteEntries.AllowCustomGroupValues = true;
    this.tcnolcRouteEntries.Control = (object) this.tcnolcRouteEntries;
    this.tcnolcRouteEntries.DisableGroupBox = true;
    this.tcnolcRouteEntries.DisableKeyDownEvents = false;
    this.tcnolcRouteEntries.DisableStatusBar = true;
    this.tcnolcRouteEntries.DisableToolBar = true;
    componentResourceManager.ApplyResources((object) this.tcnolcRouteEntries, "tcnolcRouteEntries");
    this.tcnolcRouteEntries.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.tcnolcRouteEntries.Name = "tcnolcRouteEntries";
    this.tcnolcRouteEntries.ViewContentType = ContentType.NonFolders;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tcnolcRouteEntries);
    this.Name = nameof (ProcessRouteEntryListView);
    this.Tag = (object) " ";
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.Controls.SetChildIndex((Control) this.tcnolcRouteEntries, 0);
    this.pnButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
