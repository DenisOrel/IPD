
// Type: Intermech.Navigator.Controls.NavTreeViewWithProps
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using NJFLib.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>User Control с деревом навигации, тулбаром сортировки и фильтрации, а так же панелью свойств выбранной в дереве сущности
/// (панель расположена внизу дерева и может быть скрыта)</summary>
public class NavTreeViewWithProps : 
  NavigatorTreeViewWithObjectTypeFiltration,
  ITreeListColumns,
  ICommandTarget,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  /// <summary>Контейнер сервисов для менеджера закладок, расположенного под деревом "Навигатора"</summary>
  [CanBeNull]
  protected AdvancedServiceContainer _servicesTreePages;
  /// <summary>Ссылка на глобальную службу уведомлений</summary>
  [CanBeNull]
  protected INotificationService _mainNotificationService;
  /// <summary>Опции отображения панели свойств выбранной в дереве сущности (панель свойств под деревом)</summary>
  [CanBeNull]
  protected ViewStateService _treePagesViewStateService;
  /// <summary>Признак того, что панель панели свойств выбранной в дереве сущности должна быть в режиме "только для чтения"</summary>
  private bool _treePagesReadOnly;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeViewsBridge _bridgeTree;
  private CollapsibleSplitter _splitterTree;
  private PageViewsManager _viewsTree;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal TreeViewsBridge BridgeTree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bridgeTree.CheckInitializedIn<TreeViewsBridge>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal CollapsibleSplitter SplitterTree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._splitterTree.CheckInitializedIn<CollapsibleSplitter>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal PageViewsManager ViewsTree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._viewsTree.CheckInitializedIn<PageViewsManager>((object) this);
    }
  }

  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass = value;
    }
  }

  /// <summary>Default constructor</summary>
  public NavTreeViewWithProps()
  {
    this.InitializeComponent();
    if (!this.InDesignMode)
    {
      this._servicesTreePages = new AdvancedServiceContainer(this.Services);
      this._treePagesViewStateService = new ViewStateService(this.CreateTreePagesViewStateFlags());
      this._servicesTreePages.AddService<IViewState>((IViewState) this._treePagesViewStateService);
      this._servicesTreePages.AddService<NavigatorViewOptions>(new NavigatorViewOptions(NavigatorViewContext.TreeViews));
      this._mainNotificationService = ApplicationServices.Container.GetService<INotificationService>();
      if (this._mainNotificationService != null)
      {
        this._mainNotificationService.Subscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingEventFired));
        this._mainNotificationService.Subscribe("ApplicationClosed", new NotificationEventHandler(this.ApplicationClosedEventFired));
      }
      this.ViewsTree.Services = (System.IServiceProvider) this._servicesTreePages;
    }
    this.ViewsInTree = new PageViewsManagerWrapper(this.ViewsTree);
    this.BridgeTree.BridgeEnabled = this.ViewsTree.Visible;
  }

  /// <summary>Конструктор сервиса ViewStateService</summary>
  /// <returns>The new tree pages view state service</returns>
  private ViewStateFlags CreateTreePagesViewStateFlags()
  {
    return !this._treePagesReadOnly ? ViewStateFlags.NodeInViews | ViewStateFlags.NodeUnderTree : ViewStateFlags.ReadOnly | ViewStateFlags.NodeInViews | ViewStateFlags.NodeUnderTree;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.ViewsInTree.Visible = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._mainNotificationService != null)
      {
        this._mainNotificationService.Unsubscribe("ApplicationClosed", new NotificationEventHandler(this.ApplicationClosedEventFired));
        this._mainNotificationService.Unsubscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingEventFired));
      }
      this._treePagesViewStateService = (ViewStateService) null;
      this._servicesTreePages?.Dispose();
      this.ViewsInTree.Dispose();
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [NotNull]
  public PageViewsManagerWrapper ViewsInTree { get; protected set; }

  /// <summary>Признак того, что панель панели свойств выбранной в дереве сущности должна быть в режиме "только для чтения"</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool TreePagesReadOnly
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._treePagesReadOnly;
    }
    set
    {
      if (this._treePagesReadOnly == value)
        return;
      this._treePagesReadOnly = value;
      if (this.Disposing || this._treePagesViewStateService == null)
        return;
      this._treePagesViewStateService.SetViewStateFlags(this.CreateTreePagesViewStateFlags());
    }
  }

  /// <summary>Контейнер сервисов для менеджера закладок, расположенного под деревом "Навигатора"</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public virtual AdvancedServiceContainer ServicesTreePages
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._servicesTreePages;
    }
  }

  /// <summary>Меняется видимость дерева</summary>
  private void ViewsTree_VisibleChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.Disposing)
      return;
    if (this.IsDisposed)
      return;
    try
    {
      this.BridgeTree.BridgeEnabled = this.ViewsTree.Visible;
      if (!this.ViewsTree.Visible)
        this.ViewsTree.CloseViews();
      else
        this.ViewsTree.UpdateViews(this.TreeView.SelectedItems, true);
    }
    catch
    {
    }
  }

  /// <summary>Обработчик события "Закрывается IPS"</summary>
  public void ApplicationClosingEventFired([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
    if (e.EventName != "ApplicationClosing" || !(e is ApplicationClosingEventArgs closingEventArgs))
      return;
    if (!this.ViewsTree.CanClose((object) this))
      closingEventArgs.Cancel = true;
    else
      this.ViewsTree.SaveChanges();
  }

  /// <summary>Application closed event fired</summary>
  public void ApplicationClosedEventFired([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
    if (this._viewsTree == null)
      return;
    this.ViewsTree.VisibleChanged -= new EventHandler(this.ViewsTree_VisibleChanged);
  }

  /// <summary>Выполнить команду</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда выполнена успешно</returns>
  public override bool Execute(ICommandState commandState)
  {
    if (base.Execute(commandState))
      return true;
    PageViewsManager viewsTree = this._viewsTree;
    return (viewsTree != null ? (viewsTree.Visible ? 1 : 0) : 0) != 0 && this.ViewsTree.Focused && this.ViewsTree.Execute(commandState);
  }

  /// <summary>Установить статус команде</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, статус команды установлен</returns>
  public override bool QueryStatus(ICommandState commandState)
  {
    if (this.Disposing || this.IsDisposed)
      return false;
    if (base.QueryStatus(commandState))
      return true;
    PageViewsManager viewsTree = this._viewsTree;
    return (viewsTree != null ? (viewsTree.Visible ? 1 : 0) : 0) != 0 && this.ViewsTree.Focused && this.ViewsTree.Execute(commandState);
  }

  /// <summary>Список контролов, дизайнеры которых должны быть активированы</summary>
  /// <returns>&gt;Или список, или null, если таковых не должно быть
  /// Пара "Контрол"-"имя поля, в которые будут сохранятся правки" (полем может выступать wrapper для контрола)</returns>
  protected override List<(Control DesignModeControl, string FieldName)> GetDesignModeChildControls()
  {
    List<(Control, string)> modeChildControls = base.GetDesignModeChildControls();
    modeChildControls.Add(((Control) this._viewsTree, "ViewsInTree"));
    return modeChildControls;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._bridgeTree = new TreeViewsBridge(this.components);
    this._viewsTree = new PageViewsManager();
    this._splitterTree = new CollapsibleSplitter();
    this._treeView.BeginInit();
    this.SuspendLayout();
    this._treeView.BackgroundImageMode = ImageDrawMode.Tile;
    this._treeView.BorderStyle = BorderStyle.Fixed3D;
    this._treeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._treeView.RowEvenStyle.WordWrap = false;
    this._treeView.RowOddStyle.WordWrap = false;
    this._treeView.RowSelectedStyle.WordWrap = false;
    this._treeView.RowStyle.BorderColor = SystemColors.Control;
    this._treeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._treeView.RowStyle.BorderWidth = 1;
    this._treeView.RowStyle.WordWrap = false;
    this._treeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this._treeView.Size = new Size(820, 488);
    this._bridgeTree.NavTreeView = this.TreeView;
    this._bridgeTree.ViewsManager = (IViewsManager) this._viewsTree;
    this._viewsTree.ActiveViewPage = (IViewPage) null;
    this._viewsTree.CausesValidation = false;
    this._viewsTree.Dock = DockStyle.Bottom;
    this._viewsTree.Font = new Font("Tahoma", 8.25f);
    this._viewsTree.HeaderAlignment = Intermech.Docking.TabAlignment.Bottom;
    this._viewsTree.Location = new Point(0, 312);
    this._viewsTree.MinimumSize = new Size(4, 50);
    this._viewsTree.Name = "_viewsTree";
    this._viewsTree.Padding = new Padding(10, 0, 0, 0);
    this._viewsTree.Size = new Size(820, 200);
    this._viewsTree.TabIndex = 10;
    this._viewsTree.Visible = false;
    this._viewsTree.VisibleChanged += new EventHandler(this.ViewsTree_VisibleChanged);
    this._splitterTree.AnimationDelay = 20;
    this._splitterTree.AnimationStep = 20;
    this._splitterTree.BorderStyle3D = Border3DStyle.Etched;
    this._splitterTree.ControlToHide = (Control) this._viewsTree;
    this._splitterTree.Dock = DockStyle.Bottom;
    this._splitterTree.ExpandParentForm = false;
    this._splitterTree.ImeMode = ImeMode.NoControl;
    this._splitterTree.Location = new Point(0, 309);
    this._splitterTree.MinSize = 150;
    this._splitterTree.Name = "spTreeView";
    this._splitterTree.TabIndex = 11;
    this._splitterTree.TabStop = false;
    this._splitterTree.UseAnimations = false;
    this._splitterTree.VisualStyle = VisualStyles.Mozilla;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._splitterTree);
    this.Controls.Add((Control) this._viewsTree);
    this.Name = nameof (NavTreeViewWithProps);
    this.Size = new Size(820, 512 /*0x0200*/);
    this.Controls.SetChildIndex((Control) this._treeView, 0);
    this.Controls.SetChildIndex((Control) this._viewsTree, 0);
    this.Controls.SetChildIndex((Control) this._splitterTree, 0);
    this._treeView.EndInit();
    this.ResumeLayout(false);
  }
}
