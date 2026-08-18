// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.wfEditorForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Map;
using Intermech.Navigator.DBObjects;
using Intermech.Remoting.Sponsors;
using Intermech.Workflow.Briefcase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for Form1.</summary>
public class wfEditorForm : FormEx, ICommandTarget, IBriefcaseContext
{
  private DockManager dockManager1;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private DockControl PaletteDockControl;
  private DockControl PropsDockControl;
  private WorkflowPalette Palette;
  private GraphView wfView;
  private PropertyGrid propertyGrid;
  private IContainer components;
  public bool IsProcess;
  private Label isDebugLabel;
  private bool _isEditMode = true;
  private bool _isNew;
  private bool _wasCheckedOut;
  private bool _saved;
  public DialogResult AutoSaveOnClose;

  public bool IsEditMode => this._isEditMode;

  public wfEditorForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1283);
  }

  public wfEditorForm(DockControl dc, long processid, bool editMode)
    : this()
  {
    this._isEditMode = editMode;
    if (!editMode)
    {
      this.wfView.AllowDelete = false;
      this.wfView.AllowEdit = false;
      this.wfView.AllowInsert = false;
      this.wfView.AllowDrop = false;
    }
    if (dc != null)
    {
      this.TopLevel = false;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Parent = (Control) dc;
      this.Dock = DockStyle.Fill;
    }
    else
    {
      this.HelpButton = false;
      this.Icon = Holder.SchemeIcon;
      FormStorage.LoadLayout((Control) this);
    }
    this.wfView.Doc.ReadOnly = !editMode;
    this.wfView.DocumentChanged += new MapChangedEventHandler(this.DocumentChanged);
    this.InitControls(processid);
  }

  private void DocumentChanged(object sender, MapChangedEventArgs e)
  {
    if (e.Hint != 902 && e.Hint != 903 || !(e.Object is WorkflowNode workflowNode) || workflowNode.ActivityKind != ActivityKind.Start)
      return;
    this.Palette.ShowStart = this.wfView.Doc.FindNode(ActivityKind.Start) == null;
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (wfEditorForm));
    this.dockManager1 = new DockManager();
    this.leftDock = new DockContainer();
    this.rightDock = new DockContainer();
    this.PropsDockControl = new DockControl();
    this.propertyGrid = new PropertyGrid();
    this.bottomDock = new DockContainer();
    this.topDock = new DockContainer();
    this.isDebugLabel = new Label();
    this.PaletteDockControl = new DockControl();
    this.wfView = new GraphView();
    this.Palette = new WorkflowPalette();
    this.leftDock.SuspendLayout();
    this.rightDock.SuspendLayout();
    this.PropsDockControl.SuspendLayout();
    this.PaletteDockControl.SuspendLayout();
    this.SuspendLayout();
    this.dockManager1.OwnerForm = (Form) this;
    this.leftDock.Controls.Add((Control) this.PaletteDockControl);
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Guid = new Guid("a591c031-5182-4a3e-b91c-84e8ecd6196e");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(124, 464, new DockControl[1]
      {
        this.PaletteDockControl
      }, this.PaletteDockControl)
    });
    this.leftDock.Manager = this.dockManager1;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    this.rightDock.Controls.Add((Control) this.PropsDockControl);
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Guid = new Guid("6f6f7d91-15dd-45bc-8a04-feb12b4f7257");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(196, 464, new DockControl[1]
      {
        this.PropsDockControl
      }, this.PropsDockControl)
    });
    this.rightDock.Manager = this.dockManager1;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    this.PropsDockControl.Closable = false;
    this.PropsDockControl.Controls.Add((Control) this.propertyGrid);
    componentResourceManager.ApplyResources((object) this.PropsDockControl, "PropsDockControl");
    this.PropsDockControl.Floatable = false;
    this.PropsDockControl.FloatingLocation = new Point(835, 325);
    this.PropsDockControl.Guid = new Guid("0bdc2575-e973-44e1-9f10-7a3fef836013");
    this.PropsDockControl.Name = "PropsDockControl";
    this.propertyGrid.CategoryForeColor = SystemColors.InactiveCaptionText;
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("e0acc605-1450-469b-967b-3b3f9d784bdf");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.bottomDock.Manager = this.dockManager1;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("4cd5cfed-a20d-47a7-939f-3404eeac290d");
    this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.topDock.Manager = this.dockManager1;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.isDebugLabel, "isDebugLabel");
    this.isDebugLabel.BackColor = Color.Transparent;
    this.isDebugLabel.ForeColor = Color.Red;
    this.isDebugLabel.Name = "isDebugLabel";
    this.PaletteDockControl.Closable = false;
    this.PaletteDockControl.Controls.Add((Control) this.Palette);
    componentResourceManager.ApplyResources((object) this.PaletteDockControl, "PaletteDockControl");
    this.PaletteDockControl.Floatable = false;
    this.PaletteDockControl.FloatingLocation = new Point(835, 325);
    this.PaletteDockControl.Guid = new Guid("035154b1-53cd-4080-b2b0-6f0786aed872");
    this.PaletteDockControl.Name = "PaletteDockControl";
    this.wfView.AllowDrop = true;
    this.wfView.AllowLink = false;
    this.wfView.BackColor = Color.White;
    this.wfView.BoundingHandlePenWidth = 1f;
    componentResourceManager.ApplyResources((object) this.wfView, "wfView");
    this.wfView.DragsRealtime = true;
    this.wfView.Form = (wfEditorForm) null;
    this.wfView.InterpolationMode = InterpolationMode.Default;
    this.wfView.Modified = false;
    this.wfView.Name = "wfView";
    this.wfView.PortHighlightBrush = (Brush) null;
    this.wfView.PrimarySelectionColor = Color.DimGray;
    this.Palette.AlignsSelectionObject = false;
    this.Palette.AllowDelete = false;
    this.Palette.AllowDrop = true;
    this.Palette.AllowEdit = false;
    this.Palette.AllowInsert = false;
    this.Palette.AllowLink = false;
    this.Palette.AllowMove = false;
    this.Palette.AllowReshape = false;
    this.Palette.AllowResize = false;
    this.Palette.AutoScrollRegion = new Size(0, 0);
    this.Palette.BackColor = Color.White;
    this.Palette.BoundingHandlePenWidth = 1f;
    componentResourceManager.ApplyResources((object) this.Palette, "Palette");
    this.Palette.InterpolationMode = InterpolationMode.Default;
    this.Palette.Name = "Palette";
    this.Palette.PrimarySelectionColor = Color.DimGray;
    this.Palette.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
    this.Palette.ShowsNegativeCoordinates = false;
    this.Palette.ShowStart = false;
    this.Palette.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
    this.Palette.Sorting = SortOrder.None;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.wfView);
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.rightDock);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.topDock);
    this.Controls.Add((Control) this.isDebugLabel);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (wfEditorForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.FormClosing += new FormClosingEventHandler(this.wfEditorForm_FormClosing);
    this.Shown += new EventHandler(this.FormShown);
    this.leftDock.ResumeLayout(false);
    this.rightDock.ResumeLayout(false);
    this.PropsDockControl.ResumeLayout(false);
    this.PaletteDockControl.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void LoadProcess(long processid)
  {
    this._saved = false;
    this.wfView.ProcessID = processid;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject dbObject = this.wfView.GetProcess(sessionKeeper.Session);
        remoteLock.Add((object) dbObject);
        this.IsProcess = dbObject.TypeID == wfConsts.ProcessesTypeID;
        if (this.IsEditMode && !this.IsProcess)
        {
          this._wasCheckedOut = dbObject.ObjectID < 0L;
          if (!this._wasCheckedOut)
          {
            dbObject = dbObject.CheckOut();
            this.wfView.ProcessID = dbObject.ObjectID;
            DBObjectsCheckOutEventArgs e = new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
            {
              processid
            }, (IList<long>) new long[1]
            {
              this.wfView.ProcessID
            });
            BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
          }
          Holder.RecentSchemes.AddRecent(Math.Abs(this.wfView.ProcessID));
        }
        this.wfView.LoadProcess(dbObject);
        RecentObjectsNode.MRUObjects.Add(Math.Abs(this.wfView.ProcessID), this.IsEditMode ? ObjectAction.View : ObjectAction.Edit, DateTime.UtcNow);
      }
    }
  }

  public GraphView View => this.wfView;

  private void AddStartStop()
  {
    int num = 300;
    WorkflowNode workflowNode1 = new WorkflowNode(-1L, ActivityInfos.FindByKind(ActivityKind.Start));
    workflowNode1.Top = (float) num;
    workflowNode1.Left = 100f;
    this.wfView.Doc.Add((MapObject) workflowNode1);
    WorkflowNode workflowNode2 = new WorkflowNode(-1L, ActivityInfos.FindByKind(ActivityKind.Stop));
    workflowNode2.Top = (float) num;
    workflowNode2.Left = 800f;
    this.wfView.Doc.Add((MapObject) workflowNode2);
    this.wfView.SnapToGrid();
  }

  private void InitControls(long processid)
  {
    this.Palette.GridCellSize = new SizeF((float) this.Palette.Width, this.Palette.GridCellSize.Height);
    this.wfView.Form = this;
    if (this.IsEditMode)
      this.Palette.Fill(-1L);
    else
      this.PaletteDockControl.Close();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (processid == 0L)
      {
        this._isNew = true;
        using (SetSchemeName setSchemeName = new SetSchemeName())
        {
          if (setSchemeName.ShowDialog() != DialogResult.OK)
            throw new AbortException();
          using (RemoteLock remoteLock = new RemoteLock())
          {
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.SchemesTypeID);
            remoteLock.Add((object) objectCollection);
            IDBObject objToLock = objectCollection.Create();
            remoteLock.Add((object) objToLock);
            objToLock.Caption = setSchemeName.SchemeName;
            objToLock.CommitCreation(false, true);
            this.wfView.ProcessID = objToLock.ObjectID;
            this.wfView.ProcessName = setSchemeName.SchemeName;
            this.wfView.Modified = false;
            this.AddStartStop();
            if (setSchemeName.CategoryID != 0L)
              MiscFunx.AddProcessToCategory(sessionKeeper.Session, this.wfView.ProcessID, setSchemeName.CategoryID);
          }
        }
      }
      else
        this.LoadProcess(processid);
    }
    this.wfView.UpdateTitle();
    Holder.Editors.RegisterEditor((Control) this, this.wfView.ProcessID, this.IsEditMode);
    Holder.EditorSettings.SetProperties(this.wfView);
  }

  public void SetPropertiesInfo(object obj)
  {
    if (obj == null)
      obj = (object) this.wfView;
    if (!this.propertyGrid.Visible)
      return;
    this.propertyGrid.SelectedObject = obj;
  }

  public void UpdateCommands() => BaseHolder.CommandManager.QueryStatus();

  /// <summary>Returns false if dialog cancelled</summary>
  /// <param name="showPrompt"></param>
  /// <returns></returns>
  public bool Save(bool showPrompt)
  {
    if (this.AllowEdit && this.wfView.Modified | showPrompt)
    {
      string name = "";
      long newID = (long) sc_21919.ssp_workflow_21920(1649041034);
      SaveDialogResult saveDialogResult = (SaveDialogResult) null;
      if (showPrompt)
      {
        saveDialogResult = OpenSaveSchemeForm.ExecuteSave();
        if (saveDialogResult.DialogResult == DialogResult.Cancel)
          return false;
        newID = saveDialogResult.SchemeID;
        if (this.wfView.ProcessName != saveDialogResult.Name)
          name = saveDialogResult.Name;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject process = this.wfView.GetProcess(sessionKeeper.Session);
        if (newID == 0L && !showPrompt)
        {
          if (name != "")
            this.wfView.ProcessName = name;
          this.wfView.SaveProcess(process);
        }
        else
        {
          bool wasCheckedOut = this._wasCheckedOut;
          this.wfView.SaveProcess(process, newID, name, this._saved, !this._wasCheckedOut);
          this.LoadProcess(this.wfView.ProcessID);
          this._wasCheckedOut = wasCheckedOut;
        }
        if (!this.IsProcess)
        {
          Holder.RecentSchemes.AddRecent(Math.Abs(this.wfView.ProcessID));
          RecentObjectsNode.MRUObjects.Add(Math.Abs(this.wfView.ProcessID), this._isNew ? ObjectAction.Create : ObjectAction.SaveChanges, DateTime.UtcNow);
        }
        if (saveDialogResult != null)
        {
          if (saveDialogResult.CategoryID != 0L)
            MiscFunx.AddProcessToCategory(process.Session, this.wfView.ProcessID, saveDialogResult.CategoryID);
        }
      }
      this._saved = true;
      this._isNew = false;
    }
    return true;
  }

  public bool Save() => this.Save(false);

  public bool SaveAs() => this.Save(true);

  public bool AllowEdit => this.IsEditMode && !this.IsProcess;

  /// <summary>
  /// Используется для показа подверждения сохранения при закрытии окна
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void FormClosingHandler(object sender, CancelEventArgs e)
  {
    if (!this.AllowEdit)
      return;
    bool flag1 = false;
    bool flag2 = false;
    long processId = this.wfView.ProcessID;
    if (this.wfView.Modified)
    {
      DialogResult dialogResult = this.AutoSaveOnClose;
      if (dialogResult == DialogResult.None)
      {
        dialogResult = MessageBox.Show((IWin32Window) null, string.Format(LocalizationHolder.rm.GetString(sc_21919.ssp_workflow_21921()), (object) this.wfView.ProcessName), LocalizationHolder.rm.GetString("Workflow.Design_148"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        flag2 = dialogResult == DialogResult.No;
      }
      if (dialogResult != DialogResult.Cancel)
      {
        if (dialogResult != DialogResult.Yes)
        {
          if (dialogResult == DialogResult.No)
          {
            flag1 = !this._saved;
            this.wfView.CancelLocalScriptDelete();
          }
        }
        else
          e.Cancel = !this.Save();
      }
      else
        e.Cancel = true;
    }
    else
      flag1 = !this._saved;
    if (flag1)
    {
      if (this.wfView.IsCreationMode || this._isNew)
        this.wfView.DeleteProcess(!this.wfView.IsCreationMode);
      else
        this.wfView.CancelProcessChanges(!this._wasCheckedOut);
      if (flag2 && this._wasCheckedOut)
      {
        int num = (int) MessageBox.Show((IWin32Window) null, LocalizationHolder.rm.GetString(sc_21919.ssp_workflow_21922()), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    if (e.Cancel)
      return;
    if (this._saved && this.wfView.ProcessID < 0L)
      this.wfView.CheckInProcess(!this._wasCheckedOut);
    this.propertyGrid.SelectedObject = (object) null;
    Holder.Editors.UnregisterEditor((Control) this);
    this.wfView.EditorClosed();
  }

  public bool Execute(ICommandState commandState)
  {
    bool flag = true;
    switch (commandState.CommandName)
    {
      case "Copy":
        this.wfView.EditCopy();
        break;
      case "Cut":
        this.wfView.EditCut();
        break;
      case "Delete":
        this.wfView.EditDelete();
        break;
      case "Paste":
        this.wfView.EditPaste();
        break;
      case "Redo":
        this.wfView.Redo();
        break;
      case "Save":
        this.Save();
        break;
      case "SaveAs":
        this.SaveAs();
        break;
      case "Undo":
        this.wfView.Undo();
        break;
      case "wfEditorSettings":
        EditorSettingsForm.EditEditorProperties(this);
        break;
      case "wfExport":
        this.Export();
        break;
      case "wfSchemeCheck":
        this.wfView.ValidateScheme((object) null, (EventArgs) null);
        break;
      case "wfSchemeRelease":
        this.wfView.SetSchemeToRelease(this.isDebugLabel);
        break;
      case "wfSchemeReleaseAll":
        this.wfView.SetAllSchemesToRelease(this.isDebugLabel);
        break;
      case "wfSnapToGrid":
        this.wfView.SnapToGrid();
        break;
      case "wfVariables":
        this.wfView.EditVariables((object) null, (EventArgs) null);
        break;
      default:
        flag = false;
        break;
    }
    return flag;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    bool flag1 = true;
    bool flag2 = false;
    try
    {
      switch (commandState.CommandName)
      {
        case "Copy":
          flag2 = this.wfView.CanEditCopy();
          break;
        case "Cut":
          flag2 = this.wfView.CanEditCut();
          break;
        case "Delete":
          flag2 = this.wfView.CanEditDelete();
          break;
        case "Paste":
          flag2 = this.wfView.CanEditPaste();
          break;
        case "Redo":
          flag2 = this.wfView.CanRedo();
          break;
        case "Save":
          flag2 = this.AllowEdit && this.wfView.Modified;
          break;
        case "SaveAs":
          flag2 = false;
          break;
        case "Undo":
          flag2 = this.wfView.CanUndo();
          break;
        case "wfEditorSettings":
          flag2 = true;
          break;
        case "wfExport":
          flag2 = true;
          break;
        case "wfSchemeCheck":
          flag2 = !this.IsProcess;
          break;
        case "wfSchemeRelease":
          flag2 = this.isDebugLabel.Visible = this.wfView.SchemeIsDebug();
          break;
        case "wfSchemeReleaseAll":
          flag2 = this.wfView.SchemeIsDebug() && this.wfView.SchemeHaveSubProcess();
          break;
        case "wfSnapToGrid":
          flag2 = this.AllowEdit;
          break;
        case "wfVariables":
          flag2 = true;
          break;
        default:
          flag1 = false;
          break;
      }
    }
    catch
    {
    }
    if (flag1)
      commandState.Enabled = flag2;
    return flag1;
  }

  private void wfEditorForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.Modal)
      return;
    FormStorage.SaveLayout((Control) this);
    this.FormClosingHandler(sender, (CancelEventArgs) e);
  }

  public void FormShown(object sender, EventArgs e)
  {
    if (!this.View.FixErrors())
      return;
    this.LoadProcess(this.View.ProcessID);
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    if (e.Shift && e.Control && e.Alt && e.KeyCode == Keys.B && this.Briefcase != null)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "*.iwf|*.iwf";
      saveFileDialog.DefaultExt = "iwf";
      saveFileDialog.FileName = this.wfView.ProcessName + ".iwf";
      saveFileDialog.RestoreDirectory = true;
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        string fileName = saveFileDialog.FileName;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = this.View.GetProcess(sessionKeeper.Session).GetAttributeByID(wfConsts.AttrBriefcaseID);
          if (attributeById != null)
          {
            using (FileStream aDestStream = new FileStream(fileName, FileMode.Create))
              new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
          }
        }
      }
    }
    base.OnKeyDown(e);
  }

  private void Export() => WorkflowBriefcase.Export(this.wfView.ProcessID);

  public SimpleBriefcase Briefcase => this.wfView.Briefcase;
}
