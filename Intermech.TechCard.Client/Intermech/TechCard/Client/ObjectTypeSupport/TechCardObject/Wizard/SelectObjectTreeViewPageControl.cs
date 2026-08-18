// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectTreeViewPageControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

/// <summary>
/// Закладка мастера для выбора объектов из дерева навигатора
/// </summary>
public class SelectObjectTreeViewPageControl : 
  UserControl,
  IWizardPage,
  ISelectedItemsHost,
  ISelectedItemsModeHost
{
  /// <summary>Тип выбираемого объекта</summary>
  private int _objectTypeId;
  /// <summary>Иконка страницы мастера</summary>
  private Image _image;
  /// <summary>Признак наличия загруженных данных</summary>
  private bool _dataLoaded;
  /// <summary>
  /// 
  /// </summary>
  private ICommandManager _commandManager;
  /// <summary>
  /// 
  /// </summary>
  private readonly AdvancedServiceContainer _serviceContainer = new AdvancedServiceContainer();
  /// <summary>
  /// Коллекция всех настроек, которые надо сохранять в настройках пользователя.
  /// Каждый элемент ссылается на экземпляр HybridDictionary.
  /// </summary>
  private readonly IDictionary _controlSettings = (IDictionary) new HybridDictionary(0, true);
  /// <summary>Режим загрузки содержимого</summary>
  private bool _loadingMode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected internal TechCardNavTreeViewControl techTreeView;

  /// <summary>Загрузка списка объектов</summary>
  private bool LoadControlData([NotNull] IWizardPage prevPage)
  {
    this._loadingMode = true;
    try
    {
      return this.DoLoadControlData(prevPage);
    }
    finally
    {
      this._loadingMode = false;
    }
  }

  /// <summary>Инициализация пользовательских контролов</summary>
  private void InitializeCustomControls()
  {
    this._commandManager = ServiceUtils.GetService<ICommandManager>((object) ApplicationServices.Container, false);
    this._serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInTree));
    if (this._commandManager != null)
      this._serviceContainer.AddService(typeof (ICommandManager), (object) this._commandManager);
    this.techTreeView.MultiSelect = false;
    this.techTreeView.DisableIMContextMenu = false;
    this.techTreeView.SelectedItemsChanged += new EventHandler(this.techTreeView_SelectionChanged);
    this.techTreeView.CheckStateChanged += new EventHandler<NodeEventArgs>(this.techTreeView_CheckStateChanged);
    this.techTreeView.DisableColumnsSorting = true;
    this.techTreeView.DisableKeyUpEvents = false;
    this.techTreeView.Services = (System.IServiceProvider) this._serviceContainer;
    this.techTreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    string caption = "";
    IDescriptor descriptor = (IDescriptor) new HiveDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, 0, caption);
    this.techTreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending), descriptor);
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadSettings()
  {
    FormStorage.LoadLayout((Control) this, this._controlSettings);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.techTreeView);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveSettings()
  {
    FormStorage.SaveLayout((Control) this, this._controlSettings);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.techTreeView);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="prevPage"></param>
  /// <returns></returns>
  protected virtual bool DoLoadControlData([NotNull] IWizardPage prevPage)
  {
    LoadPageControlEventArgs e = new LoadPageControlEventArgs(prevPage);
    LoadPageControlEventHandler loadPageControlData = this.LoadPageControlData;
    if (loadPageControlData != null)
      loadPageControlData((Control) this, e);
    return e.DataLoaded;
  }

  /// <summary>Конструктор</summary>
  public SelectObjectTreeViewPageControl()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
  }

  /// <summary>Активация закладки</summary>
  /// <param name="prevPage"></param>
  /// <param name="rollback"></param>
  public void Activate(IWizardPage prevPage, bool rollback)
  {
    if (rollback || this._dataLoaded)
      return;
    this._dataLoaded = this.LoadControlData(prevPage);
    this.LoadSettings();
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete == null)
      return;
    ISelectedItems selectedItems = this.SelectedItems;
    pageComplete((object) this, new PageCompleteEventArgs(selectedItems != null && selectedItems.Any()));
  }

  /// <summary>Деактивация закладки</summary>
  /// <param name="nextPage"></param>
  /// <param name="rollback"></param>
  public void Deactivate(IWizardPage nextPage, bool rollback) => this.SaveSettings();

  /// <summary>
  /// Признак, если работа пользователя с этой страницей действительно может быть закончена.
  /// Вызывается при нажатии пользователем кнопки "Вперед/Готово".
  /// </summary>
  public bool ReallyComplete
  {
    get
    {
      ISelectedItems selectedItems = this.SelectedItems;
      return selectedItems != null && selectedItems.Any();
    }
  }

  /// <summary>
  /// Позволяет сохранить/обработать результаты работы страницы мастера. Вызывается при нажатии
  /// пользователем кнопки "Вперед/Готово" до смены страниц мастера.
  /// </summary>
  public void DoMagic()
  {
  }

  /// <summary>
  /// Визуальный элемент управления, реализующий страницу мастера.
  /// </summary>
  public Control Control
  {
    get => (Control) this;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public IWizard Wizard { get; set; }

  /// <summary>Название страницы мастера.</summary>
  public string Caption { get; set; }

  /// <summary>Описание страницы мастера.</summary>
  public string Description { get; set; }

  /// <summary>Иконка страницы мастера.</summary>
  public Image Image
  {
    get
    {
      if (this._image != null)
        return this._image;
      Icon icon1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false)?.GetIcon(4, this.ObjectTypeId);
      if (icon1 != null)
      {
        using (Icon icon2 = ImagesResizeHelper.ResizeIconTo16x16(icon1, Color.Transparent))
          this._image = (Image) icon2.ToBitmap();
      }
      return this._image;
    }
  }

  /// <summary>
  /// Событие, когда пользователь ввел все необходимые данные на этой странице и может
  /// перейти к следующей странице мастера. По этому событию мастер включает и выключает
  /// кнопку "Далее/Готово".
  /// </summary>
  public event EventHandler<PageCompleteEventArgs> PageComplete;

  /// <summary>Описание выбранных элементов</summary>
  public ISelectedItems SelectedItems
  {
    get
    {
      switch (this.ItemsMode)
      {
        case SelectedItemsMode.Default:
          return this.techTreeView?.SelectedItems;
        case SelectedItemsMode.FocusedItems:
          return this.techTreeView?.FocusedItems;
        case SelectedItemsMode.CheckedItems:
          return this.techTreeView?.CheckedItems;
        default:
          return this.techTreeView?.SelectedItems;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>Текущий режим выбора элементов</summary>
  public SelectedItemsMode ItemsMode { get; set; }

  /// <summary>Дерево навигатора</summary>
  public TechCardNavTreeViewControl TreeViewControl => this.techTreeView;

  /// <summary>Тип объектов</summary>
  public int ObjectTypeId
  {
    get => this._objectTypeId;
    set
    {
      if (this._objectTypeId == value)
        return;
      this._objectTypeId = value;
      this._dataLoaded = false;
    }
  }

  /// <summary>Событие на загрузку данных закладки</summary>
  public event LoadPageControlEventHandler LoadPageControlData;

  /// <summary>Контейнер сервисов</summary>
  public IServiceContainer ServiceContainer => (IServiceContainer) this._serviceContainer;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techTreeView_DoubleClick(object sender, EventArgs e)
  {
    ISelectedItems selectedItems = this.SelectedItems;
    if ((selectedItems != null ? (selectedItems.Any() ? 1 : 0) : 0) == 0 || !(this.Wizard is Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl wizard))
      return;
    wizard.GotoNextPage();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techTreeView_SelectionChanged(object sender, EventArgs e)
  {
    if (this._loadingMode || this.ItemsMode == SelectedItemsMode.CheckedItems)
      return;
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete != null)
    {
      ISelectedItems selectedItems = this.SelectedItems;
      pageComplete((object) this, new PageCompleteEventArgs(selectedItems != null && selectedItems.Any()));
    }
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techTreeView_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (this._loadingMode || this.ItemsMode == SelectedItemsMode.FocusedItems)
      return;
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete != null)
    {
      ISelectedItems selectedItems = this.SelectedItems;
      pageComplete((object) this, new PageCompleteEventArgs(selectedItems != null && selectedItems.Any()));
    }
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged(sender, (EventArgs) e);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectObjectTreeViewPageControl));
    this.techTreeView = new TechCardNavTreeViewControl();
    this.techTreeView.BeginInit();
    this.SuspendLayout();
    this.techTreeView.AllowDrop = true;
    this.techTreeView.AllowMultiSelect = false;
    this.techTreeView.AllowUserPinnedColumns = false;
    this.techTreeView.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("techTreeView.CheckedNodesStates");
    this.techTreeView.CheckoutMode = TechCheckoutMode.Auto;
    this.techTreeView.CheckRootNode = false;
    this.techTreeView.ContextMenuBarItem = (ContextMenuBarItem) null;
    this.techTreeView.DisableCheckedOutColumn = true;
    this.techTreeView.DisableIMContextMenu = true;
    this.techTreeView.DisableKeyUpEvents = true;
    this.techTreeView.Dock = DockStyle.Fill;
    this.techTreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.techTreeView.ImageList = (ImageList) null;
    this.techTreeView.LineStyle = LineStyle.Dot;
    this.techTreeView.Location = new Point(0, 0);
    this.techTreeView.Name = "techTreeView";
    this.techTreeView.RowEvenStyle.WordWrap = false;
    this.techTreeView.RowOddStyle.WordWrap = false;
    this.techTreeView.RowSelectedStyle.WordWrap = false;
    this.techTreeView.RowStyle.BorderColor = SystemColors.Control;
    this.techTreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.techTreeView.RowStyle.BorderWidth = 1;
    this.techTreeView.RowStyle.WordWrap = false;
    this.techTreeView.SelectBeforeEdit = true;
    this.techTreeView.ShowRootRow = false;
    this.techTreeView.Size = new Size(490, 466);
    this.techTreeView.SuppressErrorMessages = true;
    this.techTreeView.TabIndex = 2;
    this.techTreeView.Tag = (object) " ";
    this.techTreeView.SelectionChanged += new EventHandler(this.techTreeView_SelectionChanged);
    this.techTreeView.DoubleClick += new EventHandler(this.techTreeView_DoubleClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.techTreeView);
    this.Name = "SelectObjectFromCompositionPageControl";
    this.Size = new Size(490, 466);
    this.techTreeView.EndInit();
    this.ResumeLayout(false);
  }

  [SpecialName]
  string IWizardPage.get_Name() => this.Name;
}
