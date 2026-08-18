
// Type: Intermech.Client.Core.CompositionView.CompositionView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Контрол редактора состава</summary>
internal class CompositionView : DockControl, IIODestination
{
  /// <summary>Текущее окно навигатора</summary>
  private NavWindowBase _navWindow;
  /// <summary>Менеджер закладок навигатора</summary>
  private PageViewsManager _viewsManager = new PageViewsManager();
  /// <summary>Дерево навигатора</summary>
  private NavigatorTreeView _treeView1 = new NavigatorTreeView();
  /// <summary>
  /// 
  /// </summary>
  private DockManager _dockManager;
  /// <summary>
  /// 
  /// </summary>
  private ServiceContainer _services = new ServiceContainer();
  /// <summary>
  /// 
  /// </summary>
  private INamedImageList _imList;
  /// <summary>
  /// 
  /// </summary>
  private CommonButtonService _commonBS;
  /// <summary>
  /// 
  /// </summary>
  private CustomButtonService _customBS;
  /// <summary>
  /// 
  /// </summary>
  private IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>Кеш типов элементов</summary>
  private CompositionCacheServices _treeTypeCacheSrv;
  /// <summary>
  /// Служба уведомлений окна "Навигатора", на котором расположена закладка
  /// </summary>
  private INotificationService _notificationService;
  /// <summary>Глоб. ид. дочернего типа объекта</summary>
  private Guid _childTypeGuid = Guid.Empty;
  /// <summary>Глоб. ид. родительского типа объекта</summary>
  private Guid _parentTypeGuid = Guid.Empty;
  /// <summary>Ид. дочернего типа объекта</summary>
  private int _childTypeID = -1;
  /// <summary>Ид. родительского типа объекта</summary>
  private int _parentTypeID = -1;
  /// <summary>
  /// Кеш допустимого соства (тип объекта / связи) для дочернего типа объекта
  /// </summary>
  private Dictionary<int, List<cvRelationInfo>> _childTypeHash;
  /// <summary>
  /// Запись кеша с дереврм навигатора для текцущих типов объектов
  /// </summary>
  private CompositionCacheServices.TypeCacheRec _typeCacheRec;
  /// <summary>Гл. индентификатор редактора</summary>
  public static Guid CompositionViewGuid = new Guid("D34700EF-1C61-414a-B827-20144469D9A9");
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip toolBarContext;
  private ToolStripMenuItem cmiSetupButtons;
  private ToolStrip toolStrip1;
  private ToolStripSplitButton biAction;
  private ToolStripMenuItem bmiAdd;
  private ToolStripSeparator bmis1;
  private ToolStripMenuItem bmiInsertBefore;
  private ToolStripMenuItem bmiInsertInto;
  private ToolStripMenuItem bmiInsertAfter;
  private ToolStripSeparator bmis2;
  private ToolStripMenuItem bmiReplace;
  private ToolStripSeparator ts1;
  private ToolStripButton biChildrenTypes;
  private ToolStripSeparator ts2;
  private SplitContainer splitContainer1;
  private ToolStripButton biParentTypes;
  private ToolStripSeparator cmiS1;
  private ToolStripMenuItem cmiRemoveUserSettings;
  private ToolStripButton biSetup;
  private ToolStripSeparator ts3;

  /// <summary>Инициадизация контролов</summary>
  private void InitializeCustomControls()
  {
    this.Guid = Intermech.Client.Core.CompositionView.CompositionView.CompositionViewGuid;
    this._imList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    if (this._imList != null)
    {
      this.toolStrip1.ImageList = this._imList.ImageList;
      this.biSetup.ImageIndex = this._imList.ImageIndex("imgSystemVariables");
      this.biChildrenTypes.ImageIndex = this._imList.ImageIndex("imgContains");
      this.biParentTypes.ImageIndex = this._imList.ImageIndex("imgEntersTo");
    }
    this._treeView1.Dock = DockStyle.Fill;
    this._treeView1.AllowMultiSelect = true;
    this._treeView1.Parent = (Control) this.splitContainer1.Panel1;
    this._treeView1.DisableIMContextMenu = false;
    this._viewsManager.Dock = DockStyle.Fill;
    this._viewsManager.AllowedViews = new string[3]
    {
      "ChildrenView",
      "ObjectProperties",
      "ImbaseTableView"
    };
    this._viewsManager.Parent = (Control) this.splitContainer1.Panel2;
    if (this.NavWindow == null)
      this.Enabled = false;
    this.SetAction((ToolStripItem) this.bmiAdd);
  }

  /// <summary>Инициализация сервисов</summary>
  private void InitializeServices()
  {
    this._commonBS = CompositionViewHolder.Services.GetService(typeof (CommonButtonService)) as CommonButtonService;
    this._customBS = CompositionViewHolder.Services.GetService(typeof (CustomButtonService)) as CustomButtonService;
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._ioDispatcher.RegisterDestination((IIODestination) this);
    this._treeTypeCacheSrv = CompositionViewHolder.Services.GetService(typeof (CompositionCacheServices)) as CompositionCacheServices;
    this._services.AddService(typeof (IIODispatcher), (object) this._ioDispatcher);
    this._services.AddService(typeof (IViewsManager), (object) this._viewsManager);
    this._services.AddService(typeof (IViewState), (object) new ViewStateService());
    this._services.AddService(typeof (INotificationService), ServicesManager.GetService(typeof (INotificationService)));
    ICommandManager service = (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
    if (service != null)
      this._services.AddService(typeof (ICommandManager), (object) service);
    this._treeView1.Services = (System.IServiceProvider) this._services;
    this._viewsManager.Services = (System.IServiceProvider) this._services;
  }

  /// <summary>Удаление кнопок с тулбара</summary>
  /// <param name="clearUserButton"></param>
  private void tsClear(bool clearUserButton)
  {
    if (this.toolStrip1 == null)
      return;
    for (int index = this.toolStrip1.Items.Count - 1; index >= 0; --index)
    {
      ToolStripItem toolStripItem = this.toolStrip1.Items[index];
      if (toolStripItem != null && toolStripItem.Tag is CVButtonBase)
        this.toolStrip1.Items.Remove(toolStripItem);
    }
    if (!(this._typeCacheRec != null & clearUserButton))
      return;
    this._typeCacheRec.UserButton = (CVButtonBase) null;
  }

  /// <summary>Запол-нение тулбара кнопками</summary>
  /// <param name="list"></param>
  private void tsFill(List<CVButtonBase> list)
  {
    if (this.toolStrip1 == null)
      return;
    foreach (CVButtonBase cvButtonBase in list)
    {
      if (cvButtonBase != null)
      {
        ToolStripButton toolStripButton = new ToolStripButton();
        toolStripButton.Image = cvButtonBase.Image;
        if (cvButtonBase.Image is Bitmap)
          toolStripButton.ImageTransparentColor = (cvButtonBase.Image as Bitmap).GetPixel(0, 0);
        toolStripButton.ToolTipText = cvButtonBase.Hint;
        toolStripButton.Tag = (object) cvButtonBase;
        toolStripButton.Click += new EventHandler(this.tsBtn_Click);
        toolStripButton.Enabled = cvButtonBase.Node == null;
        this.toolStrip1.Items.Add((ToolStripItem) toolStripButton);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsBtn_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    CVButtonBase tag = (sender as ToolStripItem).Tag as CVButtonBase;
    IDescriptor rootDescriptor = (IDescriptor) null;
    if (tag != null)
      rootDescriptor = tag.BuildTree();
    if (rootDescriptor != null)
    {
      this._typeCacheRec.DrawingType = CompositionCacheServices.TreeViewDrawingType.UserMode;
      this._typeCacheRec.UserButton = tag;
      this._treeView1.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
      this._treeView1.Build(rootDescriptor);
      this.Switch2ChildrenView();
    }
    else
      this._typeCacheRec.UserButton = (CVButtonBase) null;
    this.ChangeChecked(sender as ToolStripButton);
  }

  /// <summary>Настройка кнопок</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmiSetupButtons_Click(object sender, EventArgs e)
  {
    using (ButtonsEditor buttonsEditor = new ButtonsEditor(this._childTypeGuid))
    {
      if (!buttonsEditor.ShowDialog().Equals((object) DialogResult.OK))
        return;
      this.tsClear(true);
      List<CVButtonBase> buttonsList = this._customBS.GetButtonsList(this._childTypeGuid);
      if (buttonsList.Count == 0)
        buttonsList = this._commonBS.GetButtonsList(this._childTypeGuid);
      this.tsFill(buttonsList);
      if (this.biParentTypes.Enabled)
        this.biParentTypes.PerformClick();
      else
        this.biChildrenTypes.PerformClick();
    }
  }

  /// <summary>
  /// Удалить конфигурацию кнопок пользователя на данный тип объекта
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmiRemoveUserSettings_Click(object sender, EventArgs e)
  {
    this._customBS.ClearButtons(this._childTypeGuid);
    this._customBS.SaveToBase();
    this.tsClear(true);
    this.tsFill(this._commonBS.GetButtonsList(this._childTypeGuid));
    if (this.biParentTypes.Enabled)
      this.biParentTypes.PerformClick();
    else
      this.biChildrenTypes.PerformClick();
  }

  /// <summary>
  ///  Кнопка по-умолчанию 1 (показывает типы объекты, которые могут входить в родительский тип данного объекта)
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void biParentTypes_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    this._typeCacheRec.DrawingType = CompositionCacheServices.TreeViewDrawingType.ParentTypes;
    this._typeCacheRec.UserButton = (CVButtonBase) null;
    this._treeView1.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int key in CompositionViewHelper.GetPossibleRelations(this._parentTypeID, false).Keys)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(key));
    this._treeView1.Build((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_26"), descriptors));
    this.ChangeChecked(this.biParentTypes);
  }

  /// <summary>
  /// Кнопка по-умолчанию 2 (показывает типы объекты, которые могут входить в выделенный тип объектов)
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void biChildrenTypes_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    this._typeCacheRec.DrawingType = CompositionCacheServices.TreeViewDrawingType.ChildrenTypes;
    this._typeCacheRec.UserButton = (CVButtonBase) null;
    this._treeView1.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int key in CompositionViewHelper.GetPossibleRelations(this._childTypeID, false).Keys)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(key));
    this._treeView1.Build((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_27"), descriptors));
    this.ChangeChecked(this.biChildrenTypes);
  }

  /// <summary>Изменение состояния кнопки</summary>
  /// <param name="button"></param>
  private void ChangeChecked(ToolStripButton button)
  {
    foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.toolStrip1.Items)
    {
      if (toolStripItem is ToolStripButton)
        (toolStripItem as ToolStripButton).Checked = false;
    }
    if (button == null)
      return;
    button.Checked = true;
  }

  /// <summary>Изменение состояния кнопки</summary>
  /// <param name="button"></param>
  private void ChangeChecked(CVButtonBase button)
  {
    foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.toolStrip1.Items)
    {
      if (toolStripItem is ToolStripButton toolStripButton)
        toolStripButton.Checked = toolStripButton.Tag == button;
    }
  }

  /// <summary>Получение выделенных items</summary>
  /// <returns></returns>
  private List<IDBTypedObjectID> GetSelectedItems(CVButtonBase button = null)
  {
    return button == null ? CompositionViewHelper.GetSelectedItems(this._treeView1, (IViewsManager) this._viewsManager) : button.GetSelectedItems(this._treeView1, (IViewsManager) this._viewsManager);
  }

  /// <summary>
  /// 
  /// </summary>
  private void Switch2ChildrenView()
  {
    bool flag = false;
    for (int index = 0; index < this._viewsManager.ViewPages.Count; ++index)
    {
      IViewPage viewPage = this._viewsManager.ViewPages[index];
      if (viewPage != null)
      {
        if (viewPage.Name.Equals("ChildrenView") && viewPage.Control is ChildrenView)
        {
          Control control = viewPage.Control;
          this._viewsManager.ActiveViewPage = viewPage;
          if (viewPage.Control is ISelectedItemsHost)
          {
            (viewPage.Control as ISelectedItemsHost).SelectedItemsChanged -= new EventHandler(this.CompositionView_SelectedItemsChanged);
            (viewPage.Control as ISelectedItemsHost).SelectedItemsChanged += new EventHandler(this.CompositionView_SelectedItemsChanged);
            this.CompositionView_SelectedItemsChanged((object) this, EventArgs.Empty);
            flag = true;
            break;
          }
          break;
        }
        if (viewPage.Name.Equals("ImbaseTableView") && viewPage.Control is ISelectedItemsHost)
        {
          this._viewsManager.ActiveViewPage = viewPage;
          if (viewPage.Control is ISelectedItemsHost)
          {
            (viewPage.Control as ISelectedItemsHost).SelectedItemsChanged -= new EventHandler(this.CompositionView_SelectedItemsChanged);
            (viewPage.Control as ISelectedItemsHost).SelectedItemsChanged += new EventHandler(this.CompositionView_SelectedItemsChanged);
            this.CompositionView_SelectedItemsChanged((object) this, EventArgs.Empty);
            flag = true;
            break;
          }
          break;
        }
      }
    }
    if (flag)
      return;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls()
  {
    if (this._typeCacheRec == null || this._navWindow == null)
      return;
    this.bmiAdd.Enabled = this.bmiInsertBefore.Enabled = this.bmiInsertInto.Enabled = this.bmiInsertAfter.Enabled = this.bmiReplace.Enabled = false;
    switch (this._typeCacheRec.DrawingType)
    {
      case CompositionCacheServices.TreeViewDrawingType.ChildrenTypes:
        List<IDBTypedObjectID> selectedItems = this.GetSelectedItems();
        this.bmiInsertInto.Enabled = CompositionViewHelper.IsObjectsCanAddToObject(selectedItems, this._childTypeHash) && CompositionViewHelper.IsRelationTypesInVisibleRelations(selectedItems, this._childTypeID, this._childTypeHash);
        break;
      case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
      case CompositionCacheServices.TreeViewDrawingType.UserMode:
        CVButtonBase button = this._typeCacheRec.DrawingType != CompositionCacheServices.TreeViewDrawingType.ParentTypes ? this._typeCacheRec.UserButton : (CVButtonBase) new cvCompositionButton();
        if (button == null)
          return;
        CVLocalButton.CVButtonArgs args = new CVLocalButton.CVButtonArgs(this._navWindow.TreeView, this.GetSelectedItems(button));
        CVButtonEnabled cvButtonEnabled = button.Check(args);
        this.bmiAdd.Enabled = cvButtonEnabled.Add;
        this.bmiInsertBefore.Enabled = cvButtonEnabled.InsertBefore;
        this.bmiInsertInto.Enabled = cvButtonEnabled.InsertInto;
        this.bmiInsertAfter.Enabled = cvButtonEnabled.InsertAfter;
        this.bmiReplace.Enabled = cvButtonEnabled.Replace;
        break;
    }
    this.UpdateAction();
  }

  /// <summary>Установка экшена</summary>
  /// <param name="action"></param>
  private void SetAction(ToolStripItem action)
  {
    if (action == null || this.biAction == null)
      return;
    this.biAction.Text = action.Text;
    this.biAction.Tag = (object) action;
    this.UpdateAction();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateAction()
  {
    if (!(this.biAction.Tag is ToolStripItem))
      return;
    ToolStripItem tag = this.biAction.Tag as ToolStripItem;
    this.biAction.Enabled = tag.Enabled;
    if (!tag.Enabled)
    {
      foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) this.biAction.DropDownItems)
      {
        if (dropDownItem.Enabled && !dropDownItem.Text.Equals("-") && !dropDownItem.Text.Equals(string.Empty))
        {
          this.SetAction(dropDownItem);
          break;
        }
      }
    }
    else
      this.biAction.Enabled = true;
  }

  /// <summary>
  /// Текущее окно (если это Навигатор или Администрарор БД то null)
  /// </summary>
  private NavWindowBase NavWindow
  {
    get => this._navWindow;
    set
    {
      if (value == this._navWindow)
        return;
      if (this._navWindow != null)
      {
        this._navWindow.TreeView.AfterFocusNode -= new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_AfterFocusNode);
        this._navWindow.TreeView.BeforeColumnsSorting -= new EventHandler(this.TreeView_BeforeColumnsSorting);
      }
      this._navWindow = value;
      this.Enabled = this._navWindow != null;
      if (this._navWindow == null)
        return;
      this._navWindow.TreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_AfterFocusNode);
      this._navWindow.TreeView.BeforeColumnsSorting += new EventHandler(this.TreeView_BeforeColumnsSorting);
      this.TreeView_AfterFocusNode((object) this._navWindow.TreeView, new NavigatorTreeNodeEventArgs(this._navWindow.TreeView.FocusedNode));
    }
  }

  /// <summary>Получение девева объектов</summary>
  /// <returns></returns>
  private bool GetTreeView(Guid parentTypeGuid, Guid childTypeGuid)
  {
    bool treeView = false;
    this._typeCacheRec = this._treeTypeCacheSrv.GetTypeCacheRec(parentTypeGuid, childTypeGuid, true);
    if (this._typeCacheRec.TreeView == null)
    {
      this._typeCacheRec.TreeView = new NavigatorTreeView();
      this._typeCacheRec.TreeView.Dock = DockStyle.Fill;
      this._typeCacheRec.TreeView.Services = this._viewsManager.Services;
      this._typeCacheRec.TreeView.DisableIMContextMenu = false;
      treeView = true;
    }
    if (this._treeView1 == this._typeCacheRec.TreeView)
      return treeView;
    if (this._treeView1 != null)
    {
      this._treeView1.Parent = (Control) null;
      this._treeView1.AfterFocusNode -= new EventHandler<NavigatorTreeNodeEventArgs>(this._treeView1_AfterFocusNode);
      this._services.RemoveService(typeof (NavigatorTreeView), true);
    }
    this._treeView1 = this._typeCacheRec.TreeView;
    this._services.AddService(typeof (NavigatorTreeView), (object) this._treeView1);
    this._treeView1.AfterFocusNode -= new EventHandler<NavigatorTreeNodeEventArgs>(this._treeView1_AfterFocusNode);
    this._treeView1.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this._treeView1_AfterFocusNode);
    this._treeView1.Parent = (Control) this.splitContainer1.Panel1;
    this._treeView1.AllowMultiSelect = true;
    return treeView;
  }

  /// <summary>Уведомление дерева навигатора</summary>
  /// <param name="args"></param>
  private void UpdateSourceTreeView(NotificationEventArgs args)
  {
    if (this._notificationService == null)
      return;
    this._notificationService.FireEvent((object) this, args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _treeView1_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if (e == null || this._viewsManager == null)
      return;
    this._viewsManager.CloseViews();
    NavigatorTreeNode node = e.Node;
    if (node == null || !node.NodeID.CategoryID.Equals(1))
      this.bmiAdd.Enabled = this.bmiInsertBefore.Enabled = this.bmiInsertInto.Enabled = this.bmiInsertAfter.Enabled = this.bmiReplace.Enabled = false;
    this._viewsManager.UpdateViews(this._treeView1.SelectedItems, true);
    this.Switch2ChildrenView();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CompositionView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Обработка событие изменения узла внешнего дерева</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TreeView_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    NavigatorTreeNode node = this._navWindow == null || e.Node == null ? (NavigatorTreeNode) null : e.Node;
    NavigatorTreeNode parent = this._navWindow == null || e.Node == null || e.Node.Parent == null ? (NavigatorTreeNode) null : e.Node.Parent;
    if (node != null && node.NodeID != null)
    {
      if (node.NodeID.CategoryID == 1)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(node.NodeID.TypeID);
        Guid guid = objectType != null ? objectType.Guid : Guid.Empty;
        this.biParentTypes.Enabled = parent != null;
        IMSObjectType imsObjectType = (IMSObjectType) null;
        if (parent != null && parent.NodeID != null && parent.NodeID.TypeID > 0)
          imsObjectType = MetaDataHelper.GetObjectType(parent.NodeID.TypeID);
        Guid parentTypeGuid = imsObjectType != null ? imsObjectType.Guid : Guid.Empty;
        bool treeView = this.GetTreeView(parentTypeGuid, guid);
        bool flag1;
        switch (this._typeCacheRec.DrawingType)
        {
          case CompositionCacheServices.TreeViewDrawingType.ChildrenTypes:
            flag1 = this._childTypeGuid != guid;
            break;
          case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
            flag1 = this._childTypeGuid != guid || this._parentTypeGuid != parentTypeGuid;
            break;
          case CompositionCacheServices.TreeViewDrawingType.UserMode:
            flag1 = this._childTypeGuid != guid || this._parentTypeGuid != parentTypeGuid;
            break;
          default:
            flag1 = guid != Guid.Empty;
            break;
        }
        bool flag2 = !this._childTypeGuid.Equals(guid) && this._typeCacheRec.DrawingType.Equals((object) CompositionCacheServices.TreeViewDrawingType.None);
        this._childTypeGuid = guid;
        this._childTypeID = objectType != null ? objectType.ObjectTypeID : -1;
        this._childTypeHash = CompositionViewHelper.GetPossibleRelations(this._childTypeID, true);
        this._parentTypeGuid = parentTypeGuid;
        this._parentTypeID = imsObjectType != null ? imsObjectType.ObjectTypeID : -1;
        this.Enabled = this.toolStrip1.Enabled = true;
        if (flag1)
        {
          this.tsClear(false);
          List<CVButtonBase> buttonsList = this._customBS.GetButtonsList(this._childTypeGuid);
          if (buttonsList.Count == 0)
            buttonsList = this._commonBS.GetButtonsList(this._childTypeGuid);
          this.tsFill(buttonsList);
          if (treeView)
          {
            if (parent != null)
              this.biParentTypes.PerformClick();
            else
              this.biChildrenTypes.PerformClick();
          }
          else
          {
            if (flag2)
            {
              this._typeCacheRec.DrawingType = parent == null || parent.NodeID == null ? CompositionCacheServices.TreeViewDrawingType.ChildrenTypes : CompositionCacheServices.TreeViewDrawingType.ParentTypes;
              this._typeCacheRec.UserButton = (CVButtonBase) null;
            }
            switch (this._typeCacheRec.DrawingType)
            {
              case CompositionCacheServices.TreeViewDrawingType.ChildrenTypes:
                this.ChangeChecked(this.biChildrenTypes);
                break;
              case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
                this.ChangeChecked(this.biParentTypes);
                break;
              case CompositionCacheServices.TreeViewDrawingType.UserMode:
                this.ChangeChecked(this._typeCacheRec.UserButton);
                break;
            }
            this._viewsManager.UpdateViews(this._treeView1.SelectedItems, true);
            this.Switch2ChildrenView();
          }
        }
        else
          this.UpdateControls();
      }
      else
      {
        this._childTypeGuid = Guid.Empty;
        this._parentTypeGuid = Guid.Empty;
        this._childTypeID = -1;
        this._parentTypeID = -1;
        this._childTypeHash = (Dictionary<int, List<cvRelationInfo>>) null;
        this.Enabled = this.toolStrip1.Enabled = false;
        return;
      }
    }
    else if (this._typeCacheRec != null)
      this.Enabled = this.toolStrip1.Enabled = false;
    if (this._typeCacheRec != null && !this._typeCacheRec.DrawingType.Equals((object) CompositionCacheServices.TreeViewDrawingType.None))
      return;
    this.GetTreeView(this._parentTypeGuid, this._childTypeGuid);
    this.Enabled = this.toolStrip1.Enabled = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="dockManager"></param>
  public CompositionView(DockManager dockManager)
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
    this.InitializeServices();
    this._dockManager = dockManager;
  }

  /// <summary>Допустипые события</summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>Обработка событий</summary>
  /// <param name="Event"></param>
  /// <returns></returns>
  public bool ProcessEvent(IIOEvent Event)
  {
    if (Event == null || !Event.EventType.Equals((object) IOEventType.evMouseDoubleClick) || !this.biAction.Enabled)
      return false;
    Event.EventFlags |= IOEventFlags.efProcessed;
    (this.biAction.Tag as ToolStripMenuItem).PerformClick();
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void biAction_ButtonClick(object sender, EventArgs e)
  {
    if (!(this.biAction.Tag is ToolStripMenuItem))
      return;
    (this.biAction.Tag as ToolStripMenuItem).PerformClick();
  }

  /// <summary>Проверка на доступность кнопок</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void biAction_DropDownOpening(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Добавление</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bmiAdd_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    switch (this._typeCacheRec.DrawingType)
    {
      case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
        this.ButtonClick((CVButtonBase) new cvCompositionButton(), CVButtonMethod.Add);
        break;
      case CompositionCacheServices.TreeViewDrawingType.UserMode:
        this.ButtonClick(this._typeCacheRec.UserButton, CVButtonMethod.Add);
        break;
    }
    this.SetAction((ToolStripItem) this.bmiAdd);
  }

  /// <summary>Вставка перед текущим объектом</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bmiInsertBefore_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    switch (this._typeCacheRec.DrawingType)
    {
      case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
        this.ButtonClick((CVButtonBase) new cvCompositionButton(), CVButtonMethod.InsertBefore);
        break;
      case CompositionCacheServices.TreeViewDrawingType.UserMode:
        this.ButtonClick(this._typeCacheRec.UserButton, CVButtonMethod.InsertBefore);
        break;
    }
    this.SetAction((ToolStripItem) this.bmiInsertBefore);
  }

  /// <summary>Вставка в текущий объект</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bmiInsertInto_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    switch (this._typeCacheRec.DrawingType)
    {
      case CompositionCacheServices.TreeViewDrawingType.ChildrenTypes:
        this.ButtonClick((CVButtonBase) new cvCompositionButton(), CVButtonMethod.InsertInto);
        break;
      case CompositionCacheServices.TreeViewDrawingType.UserMode:
        this.ButtonClick(this._typeCacheRec.UserButton, CVButtonMethod.InsertInto);
        break;
    }
    this.SetAction((ToolStripItem) this.bmiInsertInto);
  }

  /// <summary>Вставка после текущего объекта</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bmiInsertAfter_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    switch (this._typeCacheRec.DrawingType)
    {
      case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
        this.ButtonClick((CVButtonBase) new cvCompositionButton(), CVButtonMethod.InsertAfter);
        break;
      case CompositionCacheServices.TreeViewDrawingType.UserMode:
        this.ButtonClick(this._typeCacheRec.UserButton, CVButtonMethod.InsertAfter);
        break;
    }
    this.SetAction((ToolStripItem) this.bmiInsertAfter);
  }

  /// <summary>Замена</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bmiReplace_Click(object sender, EventArgs e)
  {
    if (this._typeCacheRec == null)
      return;
    switch (this._typeCacheRec.DrawingType)
    {
      case CompositionCacheServices.TreeViewDrawingType.ParentTypes:
        this.ButtonClick((CVButtonBase) new cvCompositionButton(), CVButtonMethod.Replace);
        break;
      case CompositionCacheServices.TreeViewDrawingType.UserMode:
        this.ButtonClick(this._typeCacheRec.UserButton, CVButtonMethod.Replace);
        break;
    }
    this.SetAction((ToolStripItem) this.bmiReplace);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="method"></param>
  private void ButtonClick(CVButtonBase button, CVButtonMethod method)
  {
    if (button == null)
      return;
    button.Click(new CVLocalButton.CVButtonClickArgs(method, this._navWindow.TreeView, this.GetSelectedItems(button))
    {
      TargetWindow = this._navWindow
    });
  }

  /// <summary>Событие приходит перед началм сортировки в TreeView</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TreeView_BeforeColumnsSorting(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _dockManager_DockControlActivated(object sender, DockControlEventArgs e)
  {
    if (this._dockManager == null || this._dockManager.ActiveDocument == null || e.DockControl is Intermech.Client.Core.CompositionView.CompositionView || !(this._dockManager.ActiveDocument is NavWindowBase))
      return;
    this.NavWindow = this._dockManager.ActiveDocument as NavWindowBase;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  internal void CompositionView_Open(object sender, EventArgs e)
  {
    if (this._dockManager == null)
      return;
    this._dockManager.DockControlActivated += new DockControlEventHandler(this._dockManager_DockControlActivated);
    this.NavWindow = this._dockManager.ActiveDocument as NavWindowBase;
    this.PersistString = "Visible";
    if (this._services.GetService(typeof (IObjectListFiltration)) != null)
      return;
    object service = ServicesManager.GetService(typeof (IObjectListFiltration));
    if (service == null)
      return;
    this._services.AddService(typeof (IObjectListFiltration), service);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CompositionView_Close(object sender, EventArgs e)
  {
    if (this._dockManager == null)
      return;
    this._dockManager.DockControlActivated -= new DockControlEventHandler(this._dockManager_DockControlActivated);
    this.NavWindow = (NavWindowBase) null;
    this.PersistString = string.Empty;
  }

  /// <summary>раздел справки для данного контрола</summary>
  public override string HelpID => "1618";

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._viewsManager.CloseViews();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Intermech.Client.Core.CompositionView.CompositionView));
    this.splitContainer1 = new SplitContainer();
    this.toolStrip1 = new ToolStrip();
    this.toolBarContext = new ContextMenuStrip(this.components);
    this.cmiSetupButtons = new ToolStripMenuItem();
    this.cmiS1 = new ToolStripSeparator();
    this.cmiRemoveUserSettings = new ToolStripMenuItem();
    this.biAction = new ToolStripSplitButton();
    this.bmiAdd = new ToolStripMenuItem();
    this.bmis1 = new ToolStripSeparator();
    this.bmiInsertBefore = new ToolStripMenuItem();
    this.bmiInsertInto = new ToolStripMenuItem();
    this.bmiInsertAfter = new ToolStripMenuItem();
    this.bmis2 = new ToolStripSeparator();
    this.bmiReplace = new ToolStripMenuItem();
    this.ts1 = new ToolStripSeparator();
    this.biSetup = new ToolStripButton();
    this.ts2 = new ToolStripSeparator();
    this.biParentTypes = new ToolStripButton();
    this.biChildrenTypes = new ToolStripButton();
    this.ts3 = new ToolStripSeparator();
    this.splitContainer1.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.toolBarContext.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.toolStrip1.ContextMenuStrip = this.toolBarContext;
    this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip1.Items.AddRange(new ToolStripItem[7]
    {
      (ToolStripItem) this.biAction,
      (ToolStripItem) this.ts1,
      (ToolStripItem) this.biSetup,
      (ToolStripItem) this.ts2,
      (ToolStripItem) this.biParentTypes,
      (ToolStripItem) this.biChildrenTypes,
      (ToolStripItem) this.ts3
    });
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.Name = "toolStrip1";
    this.toolBarContext.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.cmiSetupButtons,
      (ToolStripItem) this.cmiS1,
      (ToolStripItem) this.cmiRemoveUserSettings
    });
    this.toolBarContext.Name = "toolBarContext";
    componentResourceManager.ApplyResources((object) this.toolBarContext, "toolBarContext");
    this.cmiSetupButtons.Name = "cmiSetupButtons";
    componentResourceManager.ApplyResources((object) this.cmiSetupButtons, "cmiSetupButtons");
    this.cmiSetupButtons.Click += new EventHandler(this.cmiSetupButtons_Click);
    this.cmiS1.Name = "cmiS1";
    componentResourceManager.ApplyResources((object) this.cmiS1, "cmiS1");
    this.cmiRemoveUserSettings.Name = "cmiRemoveUserSettings";
    componentResourceManager.ApplyResources((object) this.cmiRemoveUserSettings, "cmiRemoveUserSettings");
    this.cmiRemoveUserSettings.Click += new EventHandler(this.cmiRemoveUserSettings_Click);
    this.biAction.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.biAction.DropDownItems.AddRange(new ToolStripItem[7]
    {
      (ToolStripItem) this.bmiAdd,
      (ToolStripItem) this.bmis1,
      (ToolStripItem) this.bmiInsertBefore,
      (ToolStripItem) this.bmiInsertInto,
      (ToolStripItem) this.bmiInsertAfter,
      (ToolStripItem) this.bmis2,
      (ToolStripItem) this.bmiReplace
    });
    componentResourceManager.ApplyResources((object) this.biAction, "biAction");
    this.biAction.Name = "biAction";
    this.biAction.ButtonClick += new EventHandler(this.biAction_ButtonClick);
    this.bmiAdd.Name = "bmiAdd";
    componentResourceManager.ApplyResources((object) this.bmiAdd, "bmiAdd");
    this.bmiAdd.Click += new EventHandler(this.bmiAdd_Click);
    this.bmis1.Name = "bmis1";
    componentResourceManager.ApplyResources((object) this.bmis1, "bmis1");
    this.bmiInsertBefore.Name = "bmiInsertBefore";
    componentResourceManager.ApplyResources((object) this.bmiInsertBefore, "bmiInsertBefore");
    this.bmiInsertBefore.Click += new EventHandler(this.bmiInsertBefore_Click);
    this.bmiInsertInto.Name = "bmiInsertInto";
    componentResourceManager.ApplyResources((object) this.bmiInsertInto, "bmiInsertInto");
    this.bmiInsertInto.Click += new EventHandler(this.bmiInsertInto_Click);
    this.bmiInsertAfter.Name = "bmiInsertAfter";
    componentResourceManager.ApplyResources((object) this.bmiInsertAfter, "bmiInsertAfter");
    this.bmiInsertAfter.Click += new EventHandler(this.bmiInsertAfter_Click);
    this.bmis2.Name = "bmis2";
    componentResourceManager.ApplyResources((object) this.bmis2, "bmis2");
    this.bmiReplace.Name = "bmiReplace";
    componentResourceManager.ApplyResources((object) this.bmiReplace, "bmiReplace");
    this.bmiReplace.Click += new EventHandler(this.bmiReplace_Click);
    this.ts1.Name = "ts1";
    componentResourceManager.ApplyResources((object) this.ts1, "ts1");
    this.biSetup.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.biSetup, "biSetup");
    this.biSetup.Name = "biSetup";
    this.biSetup.Click += new EventHandler(this.cmiSetupButtons_Click);
    this.ts2.Name = "ts2";
    componentResourceManager.ApplyResources((object) this.ts2, "ts2");
    this.biParentTypes.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.biParentTypes, "biParentTypes");
    this.biParentTypes.Name = "biParentTypes";
    this.biParentTypes.Click += new EventHandler(this.biParentTypes_Click);
    this.biChildrenTypes.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.biChildrenTypes, "biChildrenTypes");
    this.biChildrenTypes.Name = "biChildrenTypes";
    this.biChildrenTypes.Click += new EventHandler(this.biChildrenTypes_Click);
    this.ts3.Name = "ts3";
    componentResourceManager.ApplyResources((object) this.ts3, "ts3");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.toolStrip1);
    this.Guid = new Guid("d34700ef-1c61-414a-b827-20144469d9a9");
    this.HideOnClose = true;
    this.Name = nameof (CompositionView);
    this.ShowHint = DockState.DockBottomAutoHide;
    this.Closed += new EventHandler(this.CompositionView_Close);
    this.AutoHidePopupClosed += new EventHandler(this.CompositionView_Close);
    this.AutoHidePopupOpened += new EventHandler(this.CompositionView_Open);
    this.splitContainer1.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.toolBarContext.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
