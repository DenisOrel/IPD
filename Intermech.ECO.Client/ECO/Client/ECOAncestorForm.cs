// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOAncestorForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Bars;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOAncestorForm : ImDocumentEditorForm, IFiltrationClass
{
  protected Intermech.ECO.Client.ECO eco;
  protected ECOPlugin plugin;
  protected INamedImageList iNIL;
  protected ServiceContainer _navigatorViewServices;
  protected Intermech.Bars.ToolBar ecoToolBar;
  private ECOAncestorForm.StructureChanged_EventHandler sChanged;
  [NonSerialized]
  protected string _FiltrationOwnerID = string.Empty;
  protected IFiltrationService _filtrationService;
  protected bool _activated;
  protected EditingContextMode? _saveMode;
  private IContainer components;

  public override string DocumentCaption
  {
    get
    {
      string documentCaption = base.DocumentCaption;
      if (documentCaption != null && documentCaption != "")
        return documentCaption;
      string documentName = this.DocumentName;
      string documentDesignation = this.DocumentDesignation;
      if (documentName != null && documentName != "" && documentDesignation != null && documentDesignation != "")
        return $"{documentDesignation}({documentName})";
      if (documentName != null && documentName != "")
        return documentName;
      return documentDesignation != null && documentDesignation != "" ? documentDesignation : LocalizationHolder.rm.GetString("ECO.Client_22");
    }
    set => base.DocumentCaption = value;
  }

  public DocumentTreeNode[] GetCommandContext()
  {
    DocumentTreeNode[] commandContext = NodeContextMenu.ContextForContextMenu;
    if (commandContext == null || !NodeContextMenu.ContextMenuCommand)
      commandContext = this.DocumentControl.GetSelectedNodes();
    return commandContext;
  }

  private void AddEnabledContextMenu(
    string commandName,
    ArrayList contextMenuItems,
    ICommandManager commandManager)
  {
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem(commandName);
    if (contextMenuItem == null)
      return;
    ICommandState command = commandManager.FindCommand(commandName);
    if (command != null)
      this.QueryStatus(command);
    if (!contextMenuItem.Enabled)
      return;
    contextMenuItems.Add((object) contextMenuItem);
  }

  protected ECOAncestorForm() => this.Init();

  protected ECOAncestorForm(IImDocumentManager documentManager, bool createDocument, bool readOnly)
    : base(documentManager, createDocument, readOnly)
  {
    this.Init();
  }

  public ECOAncestorForm(IImDocumentManager documentManager, ImDocument document, bool readOnly)
    : base(documentManager, document, readOnly)
  {
  }

  protected override void Init()
  {
    base.Init();
    this.InitializeComponent();
    int num = ImDocumentEditorConfig.Instance.ShowDebugInfo ? 1 : 0;
    this.DefaultFileExtension = ".revx";
    this.AskForSaveBeforeClose = true;
    this.plugin = ECOPlugin.FindPlugin();
    if (this.plugin == null)
      throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_23"));
    this.UndoManager = (IUndoManager) new Intermech.Document.Model.Undo.UndoManager((ImDocumentEditorFormBase) this);
    this.DocumentControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.MyGetCustomElementContextMenu);
    this.InitDragEvents();
    this.DocumentControl_ActivePageChanged((object) this, new EventArgs());
    this.DocumentControl.SelectionChanged += new SelectionChanged_EventHandler(this.ControlSelChanged);
    this.DocumentControl.SelectedElementBoundsChanging += new SelectedElementBoundsChanging_EventHandler(this.ElemBoundsChanging);
    this.DocumentControl.BeforeSelectionChanged += new BeforeSelectionChanged_EventHandler(this.DocumentControl_BeforeSelectionChanged);
    this.DocumentControl.MultiSelect = false;
    if (this._navigatorViewServices == null)
    {
      this._navigatorViewServices = new ServiceContainer();
      this._navigatorViewServices.AddService(typeof (IViewState), (object) new ViewStateService());
      this._navigatorViewServices.AddService(typeof (ECOAncestorForm), (object) this);
    }
    this.Document.DistributePageFinished += new DistributePageFinished_EventHandler(this.Document_DistributePageFinished);
    this.iNIL = (INamedImageList) ECOPlugin.serviceProvider.GetService(typeof (INamedImageList));
  }

  protected override void InitBarManager()
  {
    bool showDebugInfo = ImDocumentEditorConfig.Instance.ShowDebugInfo;
    this.SetBaseEditCommandsEnabled(showDebugInfo, showDebugInfo);
    base.InitBarManager();
  }

  public override DocumentMenuHelper CreateDocumentMenuHelper()
  {
    return (DocumentMenuHelper) new ECOMenuHelper(ECOPlugin.plugin.CommandManager)
    {
      EcoForm = this
    };
  }

  public Intermech.ECO.Client.ECO ECO
  {
    get => this.eco;
    set => this.eco = value;
  }

  public long ecoID => this.DocumentID;

  private void Document_DistributePageFinished(object sender, DistributePageFinishedArgs e)
  {
    this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
  }

  public event ECOAncestorForm.StructureChanged_EventHandler StructureChanged
  {
    add => this.sChanged += value;
    remove => this.sChanged -= value;
  }

  public virtual void OnStructureChanged(StructureChanged_EventArgs e)
  {
    if (this.sChanged == null)
      return;
    this.sChanged((object) this, e);
  }

  protected virtual void DocumentControl_BeforeSelectionChanged(
    object sender,
    BeforeSelectionChanged_EventArgs e)
  {
  }

  protected virtual void MyGetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
  }

  private void InitDragEvents()
  {
    this.DocumentControl.ActivePageChanged += new ActivePageChanged_EventHandler(this.DocumentControl_ActivePageChanged);
  }

  internal virtual void DocumentControl_ActivePageChanged(object sender, EventArgs e)
  {
    if (this.DocumentControl.ActivePage == null)
      return;
    this.DocumentControl.ActivePage.PageControl.AllowDrop = false;
  }

  protected virtual void ControlSelChanged(object sender, SelectionChanged_EventArgs e)
  {
  }

  private void ElemBoundsChanging(object sender, BoundsChangingEventArgs e)
  {
    if (e.Element.Template == null)
      return;
    if (e.Element.Template.Name == Intermech.ECO.Client.ECO.fldVar3 || e.Element.Template.Name == Intermech.ECO.Client.ECO.fldVar4 || e.Element.Template.Name == Intermech.ECO.Client.ECO.fldVar5 || e.Element.Template.Name == Intermech.ECO.Client.ECO.fldVar6)
    {
      RectangleElement ce = (RectangleElement) e.Element.Nodes[0];
      if (e.Element.Nodes.Count >= 2)
      {
        RectangleElement node = (RectangleElement) e.Element.Nodes[1];
        if (ce is ContainerElement && node is ContainerElement)
        {
          SizeF size = (node as ContainerElement).Size;
          double height1 = (double) size.Height;
          size = (ce as ContainerElement).Size;
          double height2 = (double) size.Height;
          if (height1 > height2)
          {
            ce = node;
            goto label_9;
          }
        }
        if (!(ce is ContainerElement))
          ce = node;
      }
label_9:
      if (ce is ContainerElement)
        this.ShowScale(ce as ContainerElement, e.NewElementBounds.Width, e.NewElementBounds.Height);
    }
    if (!(e.Element is ContainerElement))
      return;
    this.ShowScale(e.Element as ContainerElement, e.NewElementBounds.Width, e.NewElementBounds.Height);
  }

  protected void ShowScale(ContainerElement ce, float w, float h)
  {
    if ((double) ce.OriginalSize.Height > 0.0 && (double) ce.OriginalSize.Width > 0.0)
    {
      float width = ce.OriginalSize.Width;
      float height = ce.OriginalSize.Height;
      float num = (double) height * (double) w <= (double) width * (double) h ? w / width : h / height;
      string str = "";
      if ((double) num > 0.0)
        str = (double) num < 1.0 ? "1 : " + (1f / num).ToString("0.00") : num.ToString("0.00") + " : 1";
      this.plugin.scalePanel.Text = str;
    }
    else
      this.plugin.scalePanel.Text = "";
  }

  protected string Get_FiltrationOwnerID()
  {
    if (this._FiltrationOwnerID.Length <= 0)
      this._FiltrationOwnerID = Convert.ToString((object) Guid.NewGuid());
    return this._FiltrationOwnerID;
  }

  public new string FiltrationOwnerID => this.Get_FiltrationOwnerID();

  public virtual void FiltrationChanged(IFiltrationSettings NewFiltration, bool FiltrationValid)
  {
  }

  public IFiltrationService FiltrationService => this._filtrationService;

  protected virtual IFiltrationService InitializeFiltrationService()
  {
    this.Get_FiltrationOwnerID();
    return (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
  }

  protected virtual void DisposeFiltrationService(IFiltrationService filtrationService)
  {
  }

  protected new void FiltrationInitToolbar()
  {
    if (this.FiltrationService == null)
      return;
    this.FiltrationService.FiltrationServiceOwnerID = this.Get_FiltrationOwnerID();
    this.FiltrationService.Enabled = false;
    if (this.FiltrationService.FiltrationToolbarHidden)
      return;
    this.FiltrationService.FiltrationToolbarVisible = true;
  }

  internal new void FiltrationClearToolbar()
  {
    if (this.FiltrationService == null)
      return;
    this.FiltrationService.FiltrationServiceOwnerID = string.Empty;
  }

  public override void Activated()
  {
    base.Activated();
    if (this._activated)
      return;
    try
    {
      if (!(this is CJEditorForm))
      {
        ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
        this.FiltrationInitToolbar();
        this._saveMode = new EditingContextMode?(this.TryActivateContext());
        service.LockEditingContextID = true;
      }
      ECOPlugin plugin = ECOPlugin.FindPlugin();
      if (plugin == null)
        return;
      plugin.CurRevId = this.ECO.EcoObjectID;
      plugin.UpdateISimpleSelectedItemsService();
    }
    finally
    {
      this._activated = true;
    }
  }

  public override void Deactivated()
  {
    base.Deactivated();
    if (!this._activated)
      return;
    try
    {
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      service.LockEditingContextID = false;
      if (this._saveMode.HasValue)
        service.EditingContextMode = this._saveMode.Value;
      this.FiltrationClearToolbar();
      ECOPlugin plugin = ECOPlugin.FindPlugin();
      if (plugin == null)
        return;
      plugin.CurRevId = 0L;
      plugin.NavigatorMenuItems = (ISelectedItems) null;
    }
    finally
    {
      this._activated = false;
    }
  }

  protected virtual void Do_DeleteFiltrationSettings()
  {
    if (this._FiltrationOwnerID.Length <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).DeleteRuleTuning((object) sessionKeeper.Session.SessionGUID, this._FiltrationOwnerID);
      this._FiltrationOwnerID = string.Empty;
    }
  }

  public virtual EditingContextMode TryActivateContext()
  {
    ICurrentUserAndRole service1 = ServicesManager.GetService<ICurrentUserAndRole>();
    int num = service1 != null ? (int) service1.EditingContextMode : 1;
    if ((service1 == null ? 0 : (!this.ReadOnly ? 1 : 0)) == 0)
      return (EditingContextMode) num;
    if (service1.EditingContextID != this.eco.EcoObjectID)
    {
      service1.EditingContextID = this.eco.EcoObjectID;
      if (ServicesManager.GetService(typeof (IEditingContextToolbar)) is IEditingContextToolbar service2)
        service2.RefreshEditingContextToolbar();
    }
    service1.EditingContextMode = EditingContextMode.AutoUpdate;
    return (EditingContextMode) num;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (disposing && this._navigatorViewServices != null)
      this._navigatorViewServices.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Text = nameof (ECOAncestorForm);
  }

  public delegate void StructureChanged_EventHandler(object sender, StructureChanged_EventArgs e);
}
