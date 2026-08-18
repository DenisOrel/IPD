// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCSchema4ObjTypeForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Map;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCSchema4ObjTypeForm : TabPageForm, ISecurityCallback
{
  private IContainer components;
  private LCView lcView;
  private Panel buttonChangePanel;
  private PropertyGrid lcPropertyGrid;
  private Panel lcPanel;
  private MenuButtonItem cmdOneside;
  private MenuButtonItem cmdTwoside;
  private MenuButtonItem cmdReverse;
  private MenuButtonItem cmdSecurity;
  private MenuButtonItem cmdAttrSecurity;
  private MenuButtonItem cmdViewByDefault;
  private MenuButtonItem cmdSetSystemGuid;
  private static readonly string configId = "LCSchema4ObjType";
  private static readonly string dockingId = "Docking";
  private ContextMenuBarItem contextMenuBarItem = new ContextMenuBarItem();
  private LCSchema lcSchema;
  private DateTime lastReloadSafe;
  private bool blockOnChange;
  private bool blockOnPropertyValueChange;
  private DockManager dockManager;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private MapPalette lcPalette;
  private DockControl paletteDockControl;
  private DockControl propDockControl;
  private Button btnSchema;
  private Label schemaName;
  private IGuidService guidService;

  public LCSchema4ObjTypeForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.InitializeContextMenu();
    this.lcSchema = new LCSchema(this.lcPalette, this.lcView);
    this.InitializeMapPalette();
    this.propDockControl.LayoutSystem.LockControls = true;
    this.paletteDockControl.LayoutSystem.LockControls = true;
  }

  private void InitializeContextMenu()
  {
    int num = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadInteger("KERNEL", "SECURITY", "CHECK_ATTR_LCACCESS", 0L, DBConfigMode.GlobalOnly) != 0L ? 1 : 0;
    this.cmdOneside = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_8"), new EventHandler(this.contextMenuItemClick));
    this.cmdTwoside = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_9"), new EventHandler(this.contextMenuItemClick));
    this.cmdReverse = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_10"), new EventHandler(this.contextMenuItemClick));
    this.cmdSecurity = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_11"), new EventHandler(this.contextMenuItemClick));
    this.cmdAttrSecurity = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_11a"), new EventHandler(this.contextMenuItemClick));
    this.cmdViewByDefault = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_12"), new EventHandler(this.contextMenuItemClick));
    this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdOneside);
    this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdTwoside);
    this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdReverse);
    this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdSecurity);
    if (num != 0)
      this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdAttrSecurity);
    this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdViewByDefault);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.guidService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      if (this.guidService != null)
      {
        this.cmdSetSystemGuid = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_220"), new EventHandler(this.contextMenuItemClick));
        this.contextMenuBarItem.Items.Add((ToolbarItemBase) this.cmdSetSystemGuid);
      }
    }
    this.contextMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
    ((BarManager) ServicesManager.GetService(typeof (BarManager)))?.MenuBar?.SetPopupMenu((Control) this.lcView, (MenuBarItem) this.contextMenuBarItem);
  }

  private void contextMenuBarItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (this.lcSchema.LCView.Selection.Count == 0)
      AbortException.Abort();
    bool flag1 = true;
    bool flag2 = true;
    foreach (MapObject mapObject in (MapCollection) this.lcSchema.LCView.Selection)
    {
      if (!(mapObject is LCLink))
        flag1 = false;
      if (!(mapObject is LCNode lcNode))
        flag2 = false;
      if (this.cmdSetSystemGuid != null)
      {
        if (lcNode != null)
          this.cmdSetSystemGuid.Visible = !SystemGUIDs.IsSystemGUID(lcNode.LCStepObject.LCStepProperties.StepGuid);
        else
          this.cmdSetSystemGuid.Visible = false;
      }
    }
    if (flag1 && this.lcSchema.ReadOnly)
      AbortException.Abort();
    if (flag1 && this.lcSchema.LCView.Selection.Count == 1)
    {
      this.cmdOneside.Visible = false;
      this.cmdTwoside.Visible = false;
      this.cmdReverse.Visible = false;
      if ((this.lcSchema.LCView.Selection.First as LCLink).LCLinkObject.Reversible)
      {
        this.cmdOneside.Visible = true;
      }
      else
      {
        this.cmdTwoside.Visible = true;
        this.cmdReverse.Visible = true;
      }
    }
    else
    {
      this.cmdOneside.Visible = flag1;
      this.cmdTwoside.Visible = flag1;
      this.cmdReverse.Visible = flag1;
    }
    this.cmdSecurity.Visible = flag2;
    this.cmdAttrSecurity.Visible = flag2;
    this.cmdViewByDefault.Visible = true;
  }

  private void contextMenuItemClick(object sender, EventArgs e)
  {
    if ((sender == this.cmdSecurity || sender == this.cmdAttrSecurity || sender == this.cmdViewByDefault) && StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage))
    {
      if (MessageBox.Show($"{MessageDialogs.msgNeedSave}\n{MessageDialogs.msgReallySave}", MessageDialogs.msgQuery, MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      EventsHolder.FireApply(sender, this.instGuid, new EventsHolder.BoolArgs(false));
    }
    bool flag1 = false;
    ArrayList arrayList = (ArrayList) null;
    foreach (MapObject mapObject1 in (MapCollection) this.lcSchema.LCView.Selection)
    {
      if (mapObject1 is LCLink link)
      {
        if (sender == this.cmdOneside && link.LCLinkObject.Reversible)
        {
          link.LCLinkObject.Reversible = false;
          link.LCLinkObject.ReversibleParams &= -2;
          if (link.LCLinkObject.LCStepLinkProperties.FromStepID == (link.FromNode as LCNode).LCStepObject.LCStepProperties.LCStep)
          {
            link.FromArrow = false;
            link.ToArrow = true;
          }
          else
          {
            link.FromArrow = true;
            link.ToArrow = false;
          }
          flag1 = true;
        }
        if (sender == this.cmdTwoside && !link.LCLinkObject.Reversible)
        {
          link.LCLinkObject.Reversible = true;
          link.LCLinkObject.ReversibleParams = 0;
          link.FromArrow = true;
          link.ToArrow = true;
          flag1 = true;
        }
        if (sender == this.cmdReverse && !link.LCLinkObject.Reversible)
        {
          this.lcSchema.LCView.StartTransaction();
          try
          {
            LCNode mapObject2 = link.FromNode.MapObject as LCNode;
            LCNode mapObject3 = link.ToNode.MapObject as LCNode;
            mapObject2.Port.RemoveLink((IMapLink) link);
            mapObject3.Port.RemoveLink((IMapLink) link);
            mapObject3.Port.AddDestinationLink((IMapLink) link);
            mapObject2.Port.AddSourceLink((IMapLink) link);
            LCStepsLinkProperties stepLinkProperties = link.LCLinkObject.LCStepLinkProperties;
            int fromStepId = stepLinkProperties.FromStepID;
            stepLinkProperties.FromStepID = stepLinkProperties.ToStepID;
            stepLinkProperties.ToStepID = fromStepId;
            stepLinkProperties.Params &= -2;
            link.LCLinkObject.LCStepLinkProperties = stepLinkProperties;
            link.LCLinkObject.ReversibleParams &= -2;
          }
          finally
          {
            flag1 = true;
            this.lcSchema.LCView.FinishTransaction("Reversed");
          }
        }
      }
      if (mapObject1 is LCNode lcNode)
      {
        if (arrayList == null)
          arrayList = new ArrayList();
        if (sender == this.cmdSetSystemGuid && this.guidService != null)
        {
          PropertyDescriptorCollection properties = (this.lcPropertyGrid.SelectedObject as LCStepObject).GetProperties();
          properties[7].SetValue((object) this.lcPropertyGrid, (object) this.guidService.GenerateNextSystemGuid(7, properties[2].GetValue((object) this.lcPropertyGrid).ToString(), properties[3].GetValue((object) this.lcPropertyGrid).ToString()));
          (this.lcPropertyGrid.SelectedObject as ILCObject).SaveProps();
          StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, true);
          EventsHolder.FireWasChanged(sender, this.instGuid, e);
          EventsHolder.FireApply(sender, this.instGuid, new EventsHolder.BoolArgs(true));
          this.lcPropertyGrid.Refresh();
        }
        arrayList.Add((object) lcNode.LCStepObject.LCStepProperties.LCStep);
      }
    }
    if ((sender == this.cmdSecurity || sender == this.cmdAttrSecurity) && arrayList != null)
    {
      object[] array = (object[]) arrayList.ToArray(typeof (object));
      bool flag2 = this.lcSchema.ReadOnly && this._folder.Category != 4;
      if (sender == this.cmdSecurity)
      {
        using (SecurityEditorForm securityEditorForm = new SecurityEditorForm())
          securityEditorForm.Execute(array, (ISecurityCallback) this, flag2);
      }
      if (sender == this.cmdAttrSecurity)
      {
        int num = 0;
        if (this._folder.Category == 4)
          num = (int) this._folder.Id;
        DataTable dataTable = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(num, true).Attributes.Select("");
        List<int> attrIdArray = new List<int>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          attrIdArray.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        using (SecurityEditor4AttrForm securityEditor4AttrForm = new SecurityEditor4AttrForm())
          securityEditor4AttrForm.Execute(attrIdArray, (ISecurityCallback) new AttrSecurity4LCStep4ObjType((int) arrayList[arrayList.Count - 1], num), flag2);
      }
    }
    if (sender == this.cmdViewByDefault)
    {
      this.lcSchema.ClearDrawInfoStep();
      this.lcSchema.FillView();
      flag1 = true;
    }
    if (!flag1)
      return;
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, true);
    EventsHolder.FireWasChanged(sender, this.instGuid, e);
  }

  private void InitializeMapPalette()
  {
    this.lcSchema.FillPalette();
    this.lastReloadSafe = DataHolders.LevelsHolder.LastReload;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LCSchema4ObjTypeForm));
    this.dockManager = new DockManager();
    this.lcPanel = new Panel();
    this.lcView = new LCView();
    this.buttonChangePanel = new Panel();
    this.btnSchema = new Button();
    this.schemaName = new Label();
    this.leftDock = new DockContainer();
    this.paletteDockControl = new DockControl();
    this.lcPalette = new MapPalette();
    this.rightDock = new DockContainer();
    this.propDockControl = new DockControl();
    this.lcPropertyGrid = new PropertyGrid();
    this.bottomDock = new DockContainer();
    this.topDock = new DockContainer();
    this.lcPanel.SuspendLayout();
    this.buttonChangePanel.SuspendLayout();
    this.leftDock.SuspendLayout();
    this.paletteDockControl.SuspendLayout();
    this.rightDock.SuspendLayout();
    this.propDockControl.SuspendLayout();
    this.SuspendLayout();
    this.dockManager.DocumentContainer = (DocumentContainer) null;
    this.dockManager.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.lcPanel, "lcPanel");
    this.lcPanel.Controls.Add((Control) this.lcView);
    this.lcPanel.Controls.Add((Control) this.buttonChangePanel);
    this.lcPanel.Name = "lcPanel";
    componentResourceManager.ApplyResources((object) this.lcView, "lcView");
    this.lcView.AllowDrop = true;
    this.lcView.BackColor = Color.White;
    this.lcView.GridSnapDrag = MapViewSnapStyle.Jump;
    this.lcView.Name = "lcView";
    this.lcView.PortGravity = 30f;
    this.lcView.DocumentChanged += new MapChangedEventHandler(this.lcView_DocumentChanged);
    this.lcView.LinkCreated += new MapSelectionEventHandler(this.lcView_LinkCreated);
    this.lcView.LinkRelinked += new MapSelectionEventHandler(this.lcView_LinkRelinked);
    this.lcView.ObjectGotSelection += new MapSelectionEventHandler(this.lcView_ObjectGotSelection);
    this.lcView.ObjectLostSelection += new MapSelectionEventHandler(this.lcView_ObjectLostSelection);
    this.lcView.SelectionDeleting += new System.ComponentModel.CancelEventHandler(this.lcView_SelectionDeleting);
    componentResourceManager.ApplyResources((object) this.buttonChangePanel, "buttonChangePanel");
    this.buttonChangePanel.Controls.Add((Control) this.btnSchema);
    this.buttonChangePanel.Controls.Add((Control) this.schemaName);
    this.buttonChangePanel.Name = "buttonChangePanel";
    componentResourceManager.ApplyResources((object) this.btnSchema, "btnSchema");
    this.btnSchema.Name = "btnSchema";
    this.btnSchema.UseVisualStyleBackColor = true;
    this.btnSchema.Click += new EventHandler(this.btnSchema_Click);
    componentResourceManager.ApplyResources((object) this.schemaName, "schemaName");
    this.schemaName.Name = "schemaName";
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Controls.Add((Control) this.paletteDockControl);
    this.leftDock.Guid = new Guid("327aa37c-6df4-4f10-ac5f-496d1ea4ce09");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(180, 381, new DockControl[1]
      {
        this.paletteDockControl
      }, this.paletteDockControl)
    });
    this.leftDock.Manager = this.dockManager;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.paletteDockControl, "paletteDockControl");
    this.paletteDockControl.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.paletteDockControl.Closable = false;
    this.paletteDockControl.Controls.Add((Control) this.lcPalette);
    this.paletteDockControl.FloatingLocation = new Point(835, 325);
    this.paletteDockControl.Guid = new Guid("b80b6849-d888-40e7-b050-800d8833a3b5");
    this.paletteDockControl.Name = "paletteDockControl";
    componentResourceManager.ApplyResources((object) this.lcPalette, "lcPalette");
    this.lcPalette.AllowDelete = false;
    this.lcPalette.AllowDrop = true;
    this.lcPalette.AllowEdit = false;
    this.lcPalette.AllowInsert = false;
    this.lcPalette.AllowLink = false;
    this.lcPalette.AllowMove = false;
    this.lcPalette.AllowReshape = false;
    this.lcPalette.AllowResize = false;
    this.lcPalette.AutoScrollRegion = new Size(0, 0);
    this.lcPalette.BackColor = Color.WhiteSmoke;
    this.lcPalette.Border3DStyle = Border3DStyle.Sunken;
    this.lcPalette.Name = "lcPalette";
    this.lcPalette.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
    this.lcPalette.ShowsNegativeCoordinates = false;
    this.lcPalette.Sorting = SortOrder.None;
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Controls.Add((Control) this.propDockControl);
    this.rightDock.Guid = new Guid("9fbbe2ce-ca9c-44bb-a24b-1639ad784a2a");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(269, 381, new DockControl[1]
      {
        this.propDockControl
      }, this.propDockControl)
    });
    this.rightDock.Manager = this.dockManager;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.propDockControl, "propDockControl");
    this.propDockControl.AllowedStates = DockLocation.Left | DockLocation.Right;
    this.propDockControl.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.propDockControl.Closable = false;
    this.propDockControl.Controls.Add((Control) this.lcPropertyGrid);
    this.propDockControl.FloatingLocation = new Point(715, 325);
    this.propDockControl.Guid = new Guid("d36cfaac-38c6-4e76-bd03-bd78dae7ac68");
    this.propDockControl.Name = "propDockControl";
    componentResourceManager.ApplyResources((object) this.lcPropertyGrid, "lcPropertyGrid");
    this.lcPropertyGrid.LineColor = SystemColors.ScrollBar;
    this.lcPropertyGrid.Name = "lcPropertyGrid";
    this.lcPropertyGrid.PropertySort = PropertySort.Alphabetical;
    this.lcPropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.lcPropertyGrid_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("6e06e1c7-9609-426b-976b-a94ebcedb4a2");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.bottomDock.Manager = this.dockManager;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("e0d73d54-1c59-4702-a6d0-005a6fce768a");
    this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.topDock.Manager = this.dockManager;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.lcPanel);
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.rightDock);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.topDock);
    this.Name = nameof (LCSchema4ObjTypeForm);
    this.Load += new EventHandler(this.LCSchema4ObjTypeForm_Load);
    this.lcPanel.ResumeLayout(false);
    this.buttonChangePanel.ResumeLayout(false);
    this.buttonChangePanel.PerformLayout();
    this.leftDock.ResumeLayout(false);
    this.paletteDockControl.ResumeLayout(false);
    this.rightDock.ResumeLayout(false);
    this.propDockControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (DataHolders.LevelsHolder.LastReload != this.lastReloadSafe)
    {
      this.lcSchema.FillPalette();
      this.lastReloadSafe = DataHolders.LevelsHolder.LastReload;
    }
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage))
      return;
    this.lcPropertyGrid.SelectedObject = (object) null;
    this.blockOnChange = true;
    try
    {
      this.lcSchema.LoadSchema((int) this._folder.Id, this._folder.Category);
      this.lcSchema.FillView();
      this.lcSchema.ReadOnly = this._folder.Category != 16 /*0x10*/;
      this.leftDock.Visible = !this.lcSchema.ReadOnly;
      this.buttonChangePanel.Visible = this._folder.Category == 4;
      this.cmdOneside.Enabled = this._folder.Category == 16 /*0x10*/;
      this.cmdTwoside.Enabled = this._folder.Category == 16 /*0x10*/;
      this.cmdReverse.Enabled = this._folder.Category == 16 /*0x10*/;
      this.cmdSecurity.Enabled = this._folder.Category == 4;
      this.cmdAttrSecurity.Enabled = this._folder.Category == 4;
      this.cmdViewByDefault.Enabled = this._folder.Category == 16 /*0x10*/;
      this.FillSchemaName();
    }
    finally
    {
      this.blockOnChange = false;
    }
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, true);
  }

  private void FillSchemaName()
  {
    if (!this.buttonChangePanel.Visible)
      return;
    this.schemaName.Text = this.lcSchema.GetSchemaName();
  }

  public override bool SaveForm(IFolder folder)
  {
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage))
    {
      this.lcSchema.WriteSchema();
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, false);
    }
    return true;
  }

  public override bool RefreshAfterCanceling()
  {
    foreach (MapObject mapObject in this.lcView.Document)
    {
      ILCObject lcObject = (ILCObject) null;
      if (mapObject is LCLink)
        lcObject = (ILCObject) (mapObject as LCLink).LCLinkObject;
      if (mapObject is LCNode)
        lcObject = (ILCObject) (mapObject as LCNode).LCStepObject;
      lcObject?.Cancel();
    }
    return true;
  }

  private void lcView_DocumentChanged(object sender, MapChangedEventArgs e)
  {
    if (this.blockOnChange)
      return;
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, true);
    EventsHolder.FireWasChanged(sender, this.instGuid, (EventArgs) e);
  }

  private void lcView_LinkCreated(object sender, MapSelectionEventArgs e)
  {
    if (!(e.MapObject is LCLink mapObject1))
      return;
    LCNode node1 = (LCNode) mapObject1.FromPort.Node;
    LCNode node2 = (LCNode) mapObject1.ToPort.Node;
    if (node1 == null || node2 == null)
      return;
    int lcStep1 = node1.LCStepObject.LCStepProperties.LCStep;
    int lcStep2 = node2.LCStepObject.LCStepProperties.LCStep;
    bool flag = false;
    foreach (MapObject mapObject2 in this.lcView.Document)
    {
      if (mapObject2 is LCLink lcLink && lcLink != e.MapObject)
      {
        int lcStep3 = ((LCNode) lcLink.FromPort.Node).LCStepObject.LCStepProperties.LCStep;
        int lcStep4 = ((LCNode) lcLink.ToPort.Node).LCStepObject.LCStepProperties.LCStep;
        if (lcStep1 == lcStep3 && lcStep2 == lcStep4)
        {
          flag = true;
          break;
        }
        if (lcStep1 == lcStep4 && lcStep2 == lcStep3)
        {
          if (!lcLink.LCLinkObject.Reversible)
          {
            lcLink.LCLinkObject.Reversible = true;
            lcLink.FromArrow = true;
          }
          flag = true;
          break;
        }
      }
    }
    if (flag)
    {
      this.blockOnChange = true;
      try
      {
        mapObject1.Remove();
      }
      finally
      {
        this.blockOnChange = false;
      }
    }
    else
    {
      LCLinkObject lcLinkObject = new LCLinkObject(new LCStepsLinkProperties(lcStep1, lcStep2, string.Empty, 0, 0), this.lcSchema);
      mapObject1.LCLinkObject = lcLinkObject;
      mapObject1.ToArrow = true;
    }
  }

  private void lcView_LinkRelinked(object sender, MapSelectionEventArgs e)
  {
    if (!(e.MapObject is LCLink mapObject1))
      return;
    LCNode mapObject2 = mapObject1.FromNode.MapObject as LCNode;
    LCNode mapObject3 = mapObject1.ToNode.MapObject as LCNode;
    LCStepsLinkProperties stepLinkProperties = mapObject1.LCLinkObject.LCStepLinkProperties with
    {
      FromStepID = mapObject2.LCStepObject.LCStepProperties.LCStep,
      ToStepID = mapObject3.LCStepObject.LCStepProperties.LCStep
    };
    mapObject1.LCLinkObject.LCStepLinkProperties = stepLinkProperties;
  }

  private void lcView_ObjectGotSelection(object sender, MapSelectionEventArgs e)
  {
    LCNode mapObject1 = e.MapObject as LCNode;
    LCLink mapObject2 = e.MapObject as LCLink;
    if (mapObject1 == null && mapObject2 == null)
    {
      this.lcPropertyGrid.SelectedObject = (object) null;
    }
    else
    {
      if (mapObject1 != null && mapObject1.InPalette)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DBLifecycleStepProperties sp = sessionKeeper.Session.GetLifecycleLevel(mapObject1.LevelId).DefaultPropertiesForLCStep() with
          {
            ObjectTypeID = this._folder.Category == 4 ? (int) this._folder.Id : 0,
            LCStep = CoreConsts.IDGeneratorNextValue
          };
          List<string> stringList = new List<string>();
          MapLayerCollectionObjectEnumerator enumerator = this.lcView.Document.GetEnumerator();
          while (enumerator.MoveNext())
          {
            if (enumerator.Current is LCNode current && current.LCStepObject != null)
            {
              if (sp.FirstStep && current.LCStepObject.LCStepProperties.FirstStep)
                sp.FirstStep = false;
              if (current.LCStepObject.LCStepProperties.LCName.StartsWith(sp.LCName))
                stringList.Add(current.LCStepObject.LCStepProperties.LCName);
            }
          }
          if (stringList.Count > 0 && stringList.IndexOf(sp.LCName) != -1)
          {
            for (int index = 1; index < 100000; ++index)
            {
              if (stringList.IndexOf($"{sp.LCName} {index.ToString()}") == -1)
              {
                ref string local = ref sp.LCName;
                local = $"{local} {index.ToString()}";
                break;
              }
            }
          }
          LCStepObject lcSPD = new LCStepObject(sp, this.lcSchema);
          mapObject1.SetNotInPalette(lcSPD);
        }
      }
      if (this.lcView.Selection.Count != 1)
        return;
      if (mapObject1 != null)
        this.lcPropertyGrid.SelectedObject = (object) mapObject1.LCStepObject;
      if (mapObject2 != null)
        this.lcPropertyGrid.SelectedObject = (object) mapObject2.LCLinkObject;
      if (this.lcPropertyGrid.SelectedObject == null)
        return;
      (this.lcPropertyGrid.SelectedObject as ILCObject).LoadProps();
      this.lcPropertyGrid.Refresh();
    }
  }

  private void lcView_ObjectLostSelection(object sender, MapSelectionEventArgs e)
  {
    if (this.lcView.Selection.Count == 1)
      this.lcView_ObjectGotSelection(sender, new MapSelectionEventArgs(this.lcView.Selection.First));
    else
      this.lcView_ObjectGotSelection(sender, new MapSelectionEventArgs((MapObject) null));
  }

  private void lcPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (this.blockOnPropertyValueChange)
      return;
    bool isFirstHardly = false;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (this.lcPropertyGrid.SelectedObject is LCStepObject)
    {
      this.blockOnPropertyValueChange = true;
      try
      {
        if (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID == 6 && !((BoolPropertyClass) e.OldValue).Boolean && ((BoolPropertyClass) e.ChangedItem.PropertyDescriptor.GetValue(this.lcPropertyGrid.SelectedObject)).Boolean)
        {
          isFirstHardly = true;
          foreach (MapObject mapObject in this.lcView.Document)
          {
            if (mapObject is LCNode lcNode && lcNode.LCStepObject != this.lcPropertyGrid.SelectedObject && lcNode.LCStepObject.LCStepProperties.FirstStep)
            {
              DBLifecycleStepProperties lcStepProperties = lcNode.LCStepObject.LCStepProperties with
              {
                FirstStep = false
              };
              lcNode.LCStepObject.LCStepProperties = lcStepProperties;
              lcNode.ComplexInit();
            }
          }
        }
        if (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID == 1)
        {
          flag3 = true;
          string str = ((LevelPropertyClass) e.ChangedItem.PropertyDescriptor.GetValue(this.lcPropertyGrid.SelectedObject)).ToString();
          if (((PropDescriptorHolder) this.lcPropertyGrid.SelectedObject).PropDescriptorCollection[2].GetValue(this.lcPropertyGrid.SelectedObject).ToString() == e.OldValue.ToString())
          {
            flag1 = true;
            ((PropDescriptorHolder) this.lcPropertyGrid.SelectedObject).PropDescriptorCollection[2].SetValue(this.lcPropertyGrid.SelectedObject, (object) str);
          }
        }
        if (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID == 2)
          flag1 = true;
        if (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID == 3)
          flag2 = true;
      }
      finally
      {
        this.blockOnPropertyValueChange = false;
      }
      foreach (MapObject mapObject in (MapCollection) this.lcSchema.LCView.Selection)
      {
        if (mapObject is LCNode lcNode)
        {
          string caption = lcNode.Caption;
          if (flag1)
            caption = ((PropDescriptorHolder) this.lcPropertyGrid.SelectedObject).PropDescriptorCollection[2].GetValue(this.lcPropertyGrid.SelectedObject).ToString();
          string note = lcNode.Note;
          if (flag2)
            note = ((PropDescriptorHolder) this.lcPropertyGrid.SelectedObject).PropDescriptorCollection[3].GetValue(this.lcPropertyGrid.SelectedObject).ToString();
          Icon ic = lcNode.IconImage;
          if (flag3)
          {
            int level = ((LevelPropertyClass) ((PropDescriptorHolder) this.lcPropertyGrid.SelectedObject).PropDescriptorCollection[1].GetValue(this.lcPropertyGrid.SelectedObject)).Level;
            ic = Statics.IconSrv == null ? (Icon) null : Statics.IconSrv.GetIcon(8, level);
          }
          if (isFirstHardly | flag1 | flag2 | flag3)
            lcNode.ComplexInit(caption, note, ic, isFirstHardly);
        }
      }
      if (flag3 | flag2 | flag1)
        this.lcPropertyGrid.Refresh();
    }
    if (this.lcPropertyGrid.SelectedObject != null)
    {
      (this.lcPropertyGrid.SelectedObject as ILCObject).SaveProps();
      (this.lcPropertyGrid.SelectedObject as ILCObject).ChangeEvent((EventArgs) e);
    }
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, true);
    EventsHolder.FireWasChanged(s, this.instGuid, (EventArgs) e);
  }

  private void inheritCB_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void lcView_SelectionDeleting(object sender, CancelEventArgs e)
  {
    if (this.lcView.Selection == null || this.lcView.Selection.IsEmpty)
      return;
    bool flag = false;
    MapObject[] mapObjectArray = this.lcView.Selection.CopyArray();
    for (int index = 0; index < mapObjectArray.Length; ++index)
    {
      if (mapObjectArray[index] is LCNode && ((LCNode) mapObjectArray[index]).LCStepObject.LCStepProperties.FirstStep)
      {
        flag = true;
        break;
      }
    }
    if (mapObjectArray.Length == 1 & flag)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_16"), MessageDialogs.msgInformation, MessageBoxButtons.OK);
      e.Cancel = true;
    }
    else
    {
      string str = flag ? "\n" + LocalizationHolder.rm.GetString("DatabaseConfigurator_17") : string.Empty;
      e.Cancel = MessageBox.Show(MessageDialogs.msgReallyDelete + str, MessageDialogs.msgConfirmDelete, MessageBoxButtons.YesNo) != DialogResult.Yes;
    }
  }

  private void LCSchema4ObjTypeForm_Load(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    service.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.iConfigurationManager_ConfigurationBeforeSave);
    this.LoadConfig(service);
  }

  private void iConfigurationManager_ConfigurationBeforeSave(
    IConfigurationManager configurationManager)
  {
    this.SaveConfig(configurationManager);
  }

  private void LoadConfig(IConfigurationManager configurationManager)
  {
    if (configurationManager == null)
      return;
    IConfiguration configuration = configurationManager.Open(LCSchema4ObjTypeForm.configId);
    if (configuration == null)
      return;
    string property = configuration.GetProperty(LCSchema4ObjTypeForm.dockingId);
    if (property == null || property.Length <= 0)
      return;
    this.dockManager.SetLayout(property);
  }

  private void SaveConfig(IConfigurationManager configurationManager)
  {
    if (configurationManager == null)
      return;
    IConfiguration configuration = configurationManager.Create(LCSchema4ObjTypeForm.configId);
    if (configuration == null)
      return;
    string layout = this.dockManager.GetLayout();
    configuration.SetProperty(LCSchema4ObjTypeForm.dockingId, layout);
  }

  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    int objectTypeID = 0;
    if (this._folder.Category == 4)
      objectTypeID = (int) this._folder.Id;
    return session.GetLifecycleStep((int) id, objectTypeID) as IDBSecurity;
  }

  public int MaintainedCategory => 7;

  public Tuple<int, object> Applicability
  {
    get
    {
      return this._folder.Category == 4 ? new Tuple<int, object>(4, (object) (int) this._folder.Id) : (Tuple<int, object>) null;
    }
  }

  private void btnSchema_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (LCSchemasFolder), LocalizationHolder.rm.GetString("DatabaseConfigurator_18"), typeof (LCSchemaFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0 || this._folder.Category != 4)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_19"), LocalizationHolder.rm.GetString("DatabaseConfigurator_20"), MessageBoxButtons.YesNo) == DialogResult.Yes)
      (this._folder as ObjectTypeFolder).ChangeObjectsSchema = true;
    else
      (this._folder as ObjectTypeFolder).ChangeObjectsSchema = false;
    (this._folder as ObjectTypeFolder).SchemaId = Convert.ToInt32(selectorForm.IDList[0]);
    try
    {
      if (!EventsHolder.FireApply(sender, this.instGuid, new EventsHolder.BoolArgs(false)))
        EventsHolder.FireCancel(sender, this.instGuid, new EventArgs());
      else
        this._folder.Update();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      EventsHolder.FireCancel(sender, this.instGuid, new EventArgs());
    }
  }

  public override string HelpTopicID
  {
    get
    {
      if (this._folder == null)
        return base.HelpTopicID;
      return this._folder is ObjectTypeFolder ? "1026" : "1045";
    }
  }
}
