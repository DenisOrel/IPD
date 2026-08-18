// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.VariablesForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for VariablesDlg.</summary>
public class VariablesForm : Form
{
  private Panel DlgPanel;
  private Button CancButton;
  private Button OkButton;
  private Panel panel1;
  private Label label1;
  private Panel panel3;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private EnhListView VarListView;
  private IContainer components;
  private VarList _vars;
  private long _objectID;
  public ImageList VarTypeIL;
  private Label label2;
  private Button ChangeButton;
  private Button RemoveVarButton;
  private Button AddVarButton;
  private Panel ButtonsPanel;
  private bool _selectionMode;
  private CheckBox SysVarsBox;
  private CheckBox VarsBox;
  private StatusStrip statusStrip;
  private ToolStripStatusLabel statusLabel;
  public bool Modified;
  private CheckBox globalVariableBox;
  private bool _isScheme;
  private GraphView _view;
  private GlobalVariablesList _globalVariablesList;

  public VariablesForm(long ProcID, IUserSession session)
    : this(ProcID, (List<VarType>) null, session)
  {
  }

  public VariablesForm(GraphView view, IUserSession session)
    : this(view.ProcessID, (List<VarType>) null, session)
  {
    this._view = view;
  }

  public VariablesForm(long ProcID, List<VarType> filterKinds, IUserSession session)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1307);
    this._objectID = ProcID;
    IDBObject src = session.GetObject(this._objectID);
    this._isScheme = src.ObjectType == wfConsts.SchemesTypeID;
    this.AddVarButton.Enabled = this._isScheme;
    this._vars = new VarList(session, false, false);
    this._vars.Load(src);
    this._vars.AddSystemVariables(src);
    this._globalVariablesList = new GlobalVariablesList(session, false, false);
    if (src is IScheme scheme)
      this._globalVariablesList.Load(scheme);
    else if (src is IActivity activity)
      this._globalVariablesList.Load(activity.Process);
    if (filterKinds != null)
    {
      this.FilterVariables(filterKinds, this._vars);
      this.FilterVariables(filterKinds, (VarList) this._globalVariablesList);
      string str = this.FilterTypesStr(filterKinds);
      if (str != "")
      {
        this.statusLabel.Text += str;
        this.statusStrip.Visible = true;
      }
    }
    this._vars.Modified = false;
  }

  private void FilterVariables(List<VarType> filterKinds, VarList filteredList)
  {
    for (int index = filteredList.Count - 1; index >= 0; --index)
    {
      if (filterKinds.IndexOf(filteredList[index].VarType) == -1)
        filteredList.RemoveAt(index);
    }
  }

  private string FilterTypesStr(List<VarType> filter)
  {
    string str = "";
    for (int index = 0; index < filter.Count; ++index)
    {
      if (str != "")
        str += ", ";
      str += MiscFunx.VarTypeToString(filter[index]);
    }
    if (str != "")
      str = $"({str})";
    return str;
  }

  public long ObjectID => this._objectID;

  public bool ReadOnly
  {
    get => !this.ButtonsPanel.Visible;
    set => this.ButtonsPanel.Visible = !value;
  }

  public bool SelectionMode
  {
    get => this._selectionMode;
    set => this._selectionMode = value;
  }

  private void FillVarsView(Variable selected = null)
  {
    int index = 0;
    this.VarListView.SmallImageList = BaseHolder.IconService.ImageList;
    if (this.VarListView.SelectedItems.Count > 0)
      index = this.VarListView.SelectedItems[0].Index;
    ListViewItem selLI1 = (ListViewItem) null;
    this.VarListView.Items.Clear();
    ListViewItem selLI2 = this.AddVariablesInListView(selected, selLI1, this._vars);
    ListViewItem listViewItem = this.AddVariablesInListView(selected, selLI2, (VarList) this._globalVariablesList);
    this.VarListView.HighlightInvalidItems();
    if (this.VarListView.Items.Count <= index)
      index = this.VarListView.Items.Count - 1;
    if (listViewItem != null)
    {
      listViewItem.Selected = true;
      this.VarListView.EnsureVisible(listViewItem.Index);
    }
    else if (index >= 0)
      this.VarListView.Items[index].Selected = true;
    this.VarListView_SelectedIndexChanged((object) null, (EventArgs) null);
  }

  private ListViewItem AddVariablesInListView(
    Variable selected,
    ListViewItem selLI,
    VarList varList)
  {
    for (int index = 0; index < varList.Count; ++index)
    {
      Variable var = varList[index];
      if (!var.Deleted && (var.Kind != VarKind.User || this.VarsBox.Checked) && (var.Kind != VarKind.System || this.SysVarsBox.Checked) && (var.Kind != VarKind.Global || this.globalVariableBox.Checked))
      {
        ListViewItem listViewItem = this.VarListView.Items.Add(var.Name);
        listViewItem.SubItems.Add(MiscFunx.VarTypeToString(var.VarType));
        listViewItem.SubItems.Add(var.UserValue);
        listViewItem.ImageIndex = Holder.VarTypeImageIndex[var.VarType];
        listViewItem.Tag = (object) var;
        listViewItem.ToolTipText = var.Note;
        if (var == selected)
          selLI = listViewItem;
      }
    }
    return selLI;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VariablesForm));
    this.DlgPanel = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.panel1 = new Panel();
    this.SysVarsBox = new CheckBox();
    this.VarsBox = new CheckBox();
    this.label1 = new Label();
    this.panel3 = new Panel();
    this.VarTypeIL = new ImageList(this.components);
    this.ButtonsPanel = new Panel();
    this.label2 = new Label();
    this.ChangeButton = new Button();
    this.RemoveVarButton = new Button();
    this.AddVarButton = new Button();
    this.statusStrip = new StatusStrip();
    this.statusLabel = new ToolStripStatusLabel();
    this.globalVariableBox = new CheckBox();
    this.VarListView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.DlgPanel.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel3.SuspendLayout();
    this.ButtonsPanel.SuspendLayout();
    this.statusStrip.SuspendLayout();
    this.SuspendLayout();
    this.DlgPanel.Controls.Add((Control) this.CancButton);
    this.DlgPanel.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.DlgPanel, "DlgPanel");
    this.DlgPanel.Name = "DlgPanel";
    this.DlgPanel.Resize += new EventHandler(this.DlgPanel_Resize);
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.panel1.Controls.Add((Control) this.globalVariableBox);
    this.panel1.Controls.Add((Control) this.SysVarsBox);
    this.panel1.Controls.Add((Control) this.VarsBox);
    this.panel1.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.SysVarsBox, "SysVarsBox");
    this.SysVarsBox.Name = "SysVarsBox";
    this.SysVarsBox.CheckedChanged += new EventHandler(this.VarsBox_CheckedChanged);
    this.VarsBox.Checked = true;
    this.VarsBox.CheckState = CheckState.Checked;
    componentResourceManager.ApplyResources((object) this.VarsBox, "VarsBox");
    this.VarsBox.Name = "VarsBox";
    this.VarsBox.CheckedChanged += new EventHandler(this.VarsBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel3.Controls.Add((Control) this.VarListView);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    this.VarTypeIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("VarTypeIL.ImageStream");
    this.VarTypeIL.TransparentColor = Color.Fuchsia;
    this.VarTypeIL.Images.SetKeyName(0, "");
    this.VarTypeIL.Images.SetKeyName(1, "");
    this.VarTypeIL.Images.SetKeyName(2, "");
    this.ButtonsPanel.Controls.Add((Control) this.label2);
    this.ButtonsPanel.Controls.Add((Control) this.ChangeButton);
    this.ButtonsPanel.Controls.Add((Control) this.RemoveVarButton);
    this.ButtonsPanel.Controls.Add((Control) this.AddVarButton);
    componentResourceManager.ApplyResources((object) this.ButtonsPanel, "ButtonsPanel");
    this.ButtonsPanel.Name = "ButtonsPanel";
    this.label2.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.ChangeButton, "ChangeButton");
    this.ChangeButton.Name = "ChangeButton";
    this.ChangeButton.Click += new EventHandler(this.ChangeButton_Click);
    componentResourceManager.ApplyResources((object) this.RemoveVarButton, "RemoveVarButton");
    this.RemoveVarButton.Name = "RemoveVarButton";
    this.RemoveVarButton.Click += new EventHandler(this.RemoveVarButton_Click);
    componentResourceManager.ApplyResources((object) this.AddVarButton, "AddVarButton");
    this.AddVarButton.Name = "AddVarButton";
    this.AddVarButton.Click += new EventHandler(this.AddVarButton_Click);
    this.statusStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.statusLabel
    });
    componentResourceManager.ApplyResources((object) this.statusStrip, "statusStrip");
    this.statusStrip.Name = "statusStrip";
    this.statusLabel.Name = "statusLabel";
    componentResourceManager.ApplyResources((object) this.statusLabel, "statusLabel");
    componentResourceManager.ApplyResources((object) this.globalVariableBox, "globalVariableBox");
    this.globalVariableBox.Name = "globalVariableBox";
    this.globalVariableBox.CheckedChanged += new EventHandler(this.VarsBox_CheckedChanged);
    this.VarListView.AllowManualSorting = true;
    this.VarListView.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3
    });
    componentResourceManager.ApplyResources((object) this.VarListView, "VarListView");
    this.VarListView.FullRowSelect = true;
    this.VarListView.HideSelection = false;
    this.VarListView.MultiSelect = false;
    this.VarListView.Name = "VarListView";
    this.VarListView.OwnerDraw = true;
    this.VarListView.RadioGroups = false;
    this.VarListView.ShowItemToolTips = true;
    this.VarListView.SmallImageList = this.VarTypeIL;
    this.VarListView.SortColumn = 0;
    this.VarListView.Sorting = SortOrder.Ascending;
    this.VarListView.SubitemImages = (ImageList) null;
    this.VarListView.UseCompatibleStateImageBehavior = false;
    this.VarListView.View = View.Details;
    this.VarListView.SelectedIndexChanged += new EventHandler(this.VarListView_SelectedIndexChanged);
    this.VarListView.DoubleClick += new EventHandler(this.VarListView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.ButtonsPanel);
    this.Controls.Add((Control) this.DlgPanel);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.statusStrip);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VariablesForm);
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.VariablesForm_Closed);
    this.Load += new EventHandler(this.VariablesForm_Load);
    this.DlgPanel.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.ButtonsPanel.ResumeLayout(false);
    this.statusStrip.ResumeLayout(false);
    this.statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void AddVarButton_Click(object sender, EventArgs e)
  {
    using (EditVarForm editVarForm = new EditVarForm(this))
    {
      if (editVarForm.ShowDialog() != DialogResult.OK)
        return;
      if (editVarForm.IsGlobalVariable)
      {
        if (editVarForm.VarTypeID == 0 && (this._globalVariablesList.GetVariable(editVarForm.VarName) != null || this._vars.GetVariable(editVarForm.VarName) != null))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetAttributesGroup(wfConsts.WorkflowVarsGroupID).HasAttribute(editVarForm.VarTypeID))
            throw new KernelException("Невозможно добавить глобальную переменную т.к. атрибут настроен для локальной (пользовательской) переменной.");
        }
        Variable selected = this._globalVariablesList.AddVariable(editVarForm.VarTypeID, editVarForm.VarName, editVarForm.VarType, editVarForm.AddInfo);
        selected.Kind = VarKind.Global;
        this.FillVarsView(selected);
      }
      else
      {
        if (editVarForm.VarTypeID == 0 && (this._vars.GetVariable(editVarForm.VarName) != null || this._globalVariablesList.GetVariable(editVarForm.VarName) != null))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetAttributesGroup(wfConsts.GlobalVariablesGroupID).HasAttribute(editVarForm.VarTypeID))
            throw new KernelException("Невозможно добавить локальную (пользовательскую) переменную т.к. атрибут настроен для глобальной переменной.");
        }
        this.FillVarsView(this._vars.AddVariable(editVarForm.VarTypeID, editVarForm.VarName, editVarForm.VarType, editVarForm.AddInfo));
      }
    }
  }

  private void ChangeButton_Click(object sender, EventArgs e)
  {
    Variable selectedVar = this.SelectedVar;
    if (selectedVar == null)
      return;
    using (EditVarForm editVarForm1 = new EditVarForm(this))
    {
      editVarForm1.Text = LocalizationHolder.rm.GetString("Workflow.Design_98");
      EditVarForm editVarForm2 = editVarForm1;
      editVarForm2.Text = $"{editVarForm2.Text} \"{selectedVar.Name}\"";
      VarType varType = this.SelectedVar.VarType;
      editVarForm1.ReadOnly = this.ReadOnly || selectedVar.Calculated;
      editVarForm1.Variable = selectedVar;
      if (editVarForm1.ShowDialog() == DialogResult.OK)
      {
        this.FillVarsView();
      }
      else
      {
        if (this.SelectedVar.VarType != varType)
          this.SelectedVar.VarType = varType;
        editVarForm1.Variable = this.SelectedVar;
      }
    }
  }

  private void VariablesForm_Closed(object sender, EventArgs e)
  {
    if (this.DialogResult == DialogResult.OK && !this.ReadOnly && (this._vars.Modified || this._globalVariablesList.Modified))
    {
      if (this.SelectionMode)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID);
        IScheme scheme = dbObject as IScheme;
        bool flag = false;
        if (scheme != null)
        {
          try
          {
            INotificationQueue notificationQueue = (INotificationQueue) new NotificationQueue();
            for (int index = 0; index < this._vars.Count; ++index)
            {
              Variable var = this._vars[index];
              if (var.Deleted && var.AttrTypeID != sc_21910.ssp_workflow_21911(1672313099))
              {
                Trace.Assert(this._view != null);
                if (this._view != null && this._view.DeleteVariable(var.Name, var.AttrTypeID))
                {
                  scheme.DeleteVariable(var.AttrTypeID);
                  this.Modified = true;
                }
              }
              else if ((var.New || var.Modified) && !var.Deleted)
              {
                if (var.AttrTypeID == 0)
                  var.AttrTypeID = scheme.AddVariable(var.Name, var.VarType, var.AddInfo);
                else
                  scheme.UseVariable(var.AttrTypeID, var.AddInfo);
                this._view?.UseVariable(var.AttrTypeID);
                int typeId = scheme.TypeID;
                List<int> addedIDs = new List<int>();
                addedIDs.Add(var.AttrTypeID);
                List<int> changedIDs = new List<int>();
                List<int> removedIDs = new List<int>();
                DBAttributes4TypeEventArgs args = new DBAttributes4TypeEventArgs("Attribute4ObjTypeEvent", typeId, (IList<int>) addedIDs, (IList<int>) changedIDs, (IList<int>) removedIDs);
                notificationQueue.QueueEvent((NotificationEventArgs) args);
                this.Modified = true;
              }
            }
            foreach (Variable globalVariables in (VarList) this._globalVariablesList)
            {
              if (globalVariables.Deleted && globalVariables.AttrTypeID != 0)
              {
                if (this._view != null && this._view.RemoveVariableReferences(globalVariables.Name, globalVariables.AttrTypeID))
                {
                  scheme.DeleteGlobalVariable(globalVariables.AttrTypeID);
                  flag = true;
                  this.Modified = true;
                }
              }
              else if ((globalVariables.New || globalVariables.Modified) && !globalVariables.Deleted)
              {
                int attrTypeId = globalVariables.AttrTypeID;
                int attributeID = scheme.AddGlobalVariable(globalVariables.Name, globalVariables.VarType, globalVariables.AddInfo, attrTypeId);
                if (attrTypeId == 0)
                {
                  DBAttributesEventArgs args1 = new DBAttributesEventArgs("AttributeCreated", attributeID);
                  int typeId = scheme.TypeID;
                  List<int> addedIDs = new List<int>();
                  addedIDs.Add(attributeID);
                  List<int> changedIDs = new List<int>();
                  List<int> removedIDs = new List<int>();
                  DBAttributes4TypeEventArgs args2 = new DBAttributes4TypeEventArgs("Attribute4ObjTypeEvent", typeId, (IList<int>) addedIDs, (IList<int>) changedIDs, (IList<int>) removedIDs);
                  notificationQueue.QueueEvent((NotificationEventArgs) args1);
                  notificationQueue.QueueEvent((NotificationEventArgs) args2);
                }
                this._view?.UseVariable(globalVariables.AttrTypeID);
                flag = true;
                this.Modified = true;
              }
            }
            if (flag)
            {
              if (ApplicationServices.Container.GetService(typeof (IClientCache)) is IClientCache service)
                service.ReloadCacheCategory(3, sessionKeeper.Session);
              notificationQueue.FlushQueue();
            }
            if (this.Modified)
            {
              if (ApplicationServices.Container.GetService(typeof (IClientCache)) is IClientCache service)
                service.ReloadCacheCategory(3, sessionKeeper.Session);
              notificationQueue.FlushQueue();
              MiscFunx.ReloadVariablesCache(sessionKeeper.Session);
            }
          }
          catch (AlreadyExistsException ex)
          {
            throw new HiddenStackException($"{LocalizationHolder.GetString("CantAddVarNameAlreadyExists")} {ex.Message}");
          }
        }
        else
          this._vars.Save(dbObject, false);
      }
    }
    HybridDictionary layoutData = this.VarListView.LayoutData;
    layoutData[(object) "VarsBox.Checked"] = (object) this.VarsBox.Checked;
    layoutData[(object) "SysVarsBox.Checked"] = (object) this.SysVarsBox.Checked;
    layoutData[(object) "GlobalVarsBox.Checked"] = (object) this.globalVariableBox.Checked;
    FormStorage.SaveLayout((Control) this, (IDictionary) layoutData);
  }

  public Variable SelectedVar
  {
    get
    {
      return this.VarListView.SelectedItems.Count > 0 ? (Variable) this.VarListView.SelectedItems[0].Tag : (Variable) null;
    }
  }

  private void VarListView_SelectedIndexChanged(object sender, EventArgs e)
  {
    Variable selectedVar = this.SelectedVar;
    this.RemoveVarButton.Enabled = this._isScheme && selectedVar != null && !(selectedVar is ISystemVariable);
    this.ChangeButton.Enabled = selectedVar != null;
  }

  private void RemoveVarButton_Click(object sender, EventArgs e)
  {
    Variable selectedVar = this.SelectedVar;
    if (selectedVar != null)
      selectedVar.Deleted = true;
    this.FillVarsView();
  }

  private void VarListView_DoubleClick(object sender, EventArgs e)
  {
    if (this.SelectionMode)
    {
      this.DialogResult = DialogResult.OK;
    }
    else
    {
      if (!this.ChangeButton.Enabled)
        return;
      this.ChangeButton_Click(sender, e);
    }
  }

  private void VariablesForm_Load(object sender, EventArgs e)
  {
    HybridDictionary layoutData = this.VarListView.LayoutData;
    FormStorage.LoadLayout((Control) this, (IDictionary) layoutData);
    this.VarListView.LayoutData = layoutData;
    this.VarsBox.Checked = Convert.ToBoolean(layoutData[(object) "VarsBox.Checked"] ?? (object) true);
    this.SysVarsBox.Checked = Convert.ToBoolean(layoutData[(object) "SysVarsBox.Checked"] ?? (object) false);
    this.globalVariableBox.Checked = Convert.ToBoolean(layoutData[(object) "GlobalVarsBox.Checked"] ?? (object) false);
    this.FillVarsView();
    this.DlgPanel_Resize((object) null, (EventArgs) null);
  }

  private void VarsBox_CheckedChanged(object sender, EventArgs e) => this.FillVarsView();

  private void DlgPanel_Resize(object sender, EventArgs e)
  {
    if (this.ButtonsPanel.Visible)
    {
      int num = (this.VarListView.Width - 12) / 3;
      this.AddVarButton.Left = this.VarListView.Left;
      this.AddVarButton.Width = num;
      this.ChangeButton.Left = this.AddVarButton.Left + this.AddVarButton.Width + 6;
      this.ChangeButton.Width = num;
      this.RemoveVarButton.Left = this.ChangeButton.Left + this.ChangeButton.Width + 6;
      this.RemoveVarButton.Width = num;
    }
    int num1 = this.VarListView.Left + this.VarListView.Width / 2;
    this.OkButton.Left = num1 - 3 - this.OkButton.Width;
    this.CancButton.Left = num1 + 3;
  }
}
