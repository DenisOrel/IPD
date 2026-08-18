
// Type: Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using Intermech.UI.Winforms;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl;

/// <summary>
/// Реализует базу для создания мастеров на DockWizardControl с реализацией интерфейсов для закладки
/// </summary>
public class DockWizardView : Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl, IView, ISelectedItemsHost, IIOSource, IIODestination
{
  /// <summary>
  /// 
  /// </summary>
  protected readonly IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>
  /// 
  /// </summary>
  protected readonly IAdvancedServiceContainer _serviceContainer = (IAdvancedServiceContainer) new AdvancedServiceContainer();
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Конструктор</summary>
  public DockWizardView()
  {
    this.InitializeComponent();
    this.InitializeCustomComponent();
  }

  /// <summary>Инициализация "прочих" пользовательских контролов</summary>
  private void InitializeCustomComponent()
  {
    this._ioDispatcher.RegisterDestination((IIODestination) this);
    this._serviceContainer.AddService<IIODispatcher>(this._ioDispatcher);
    this.ShowCancelButton = false;
    this.ShowHeaderPanel = false;
    this.ShowFinishButton = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public virtual void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.Services = provider;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="previousView"></param>
  public virtual void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.ChangePage(this.GetFirstPage() ?? throw new Exception("Первая страница мастера не задана!"), false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nextView"></param>
  public virtual void Deactivate(IView nextView)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public string Caption { get; protected set; } = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  public int ImageIndex { get; protected set; } = -1;

  /// <summary>
  /// 
  /// </summary>
  public int OrderID { get; protected set; }

  /// <summary>
  /// 
  /// </summary>
  public object Control
  {
    get => (object) this;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public System.IServiceProvider Services
  {
    get => (System.IServiceProvider) this._serviceContainer;
    set => this._serviceContainer.AdvancedProvider = value;
  }

  public ISelectedItems SelectedItems
  {
    get
    {
      if (this.ActivePage != this.Pages.Last<IWizardPage>() || !this.ActivePage.ReallyComplete)
        return (ISelectedItems) new EmptySelectedItems();
      return !(this.ActivePage is ISelectedItemsHost activePage) ? (ISelectedItems) null : activePage.SelectedItems;
    }
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>Вызов события SelectedItemsChanged</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void DoSelectedItemsChanged(object sender, EventArgs e)
  {
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ioEvent"></param>
  /// <returns></returns>
  public bool ProcessEvent(IIOEvent ioEvent)
  {
    if (ioEvent == null || this.Pages.Last<IWizardPage>() != this.ActivePage || !(ioEvent.Source.Control is System.Windows.Forms.Control control) || this.ActivePage.Control != control && !this.ActivePage.Control.Controls.Contains(control))
      return false;
    ioEvent = (IIOEvent) new IOEvent((IIOSource) this, ioEvent.EventFlags, ioEvent.EventType, ioEvent.EventData, ioEvent.Tag);
    IIODispatcher service = ServiceUtils.GetService<IIODispatcher>((object) this._serviceContainer.AdvancedProvider, false);
    if (service == null)
      return false;
    service.ProcessEvent(ioEvent);
    return true;
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
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
