// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SignsForType
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class SignsForType : UserControl
{
  public SignsDataItemModel SignsDataItemModel = new SignsDataItemModel();
  private Bitmap nullBitmap;
  private Style firstRowStyle;
  private Style bigValueStyle;
  private CellEditor _strongControlEditor;
  private int _signsGroupID;
  private bool _isFirst = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolBar buttonsBar;
  private ToolBarButton addTypeBtn;
  private ToolBarButton addGraphBtn;
  private ToolBarButton delBtn;
  private Infralution.Controls.VirtualTree.VirtualTree graphForTypeTree;
  private Column objectTypeColumn;
  private Column graphColumn;
  private Column strongControlColumn;
  private ImageList buttonsIcons;
  private CheckBox personalSignsCheckBox;
  private BindingSource signsDataItemModelBindingSource;

  public SignsForType()
  {
    this.InitializeComponent();
    this.InitializeTreeViewStyles();
    this.nullBitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    Icon icon = Icon.FromHandle(this.nullBitmap.GetHicon());
    this.graphForTypeTree.CollapseIcon = icon;
    this.graphForTypeTree.ExpandIcon = icon;
    this.SignsDataItemModel.Nodes.ListChanged += new ListChangedEventHandler(this.Nodes_ListChanged);
    this.SignsDataItemModel.PropertyChanged += new PropertyChangedEventHandler(this.SignsDataItemModel_PropertyChanged);
    this.graphForTypeTree.DataSource = (object) this.SignsDataItemModel;
    CheckBox checkBox1 = new CheckBox();
    checkBox1.Text = string.Empty;
    checkBox1.CheckAlign = ContentAlignment.MiddleCenter;
    checkBox1.AutoSize = false;
    checkBox1.Anchor = AnchorStyles.None;
    CheckBox checkBox2 = checkBox1;
    this._strongControlEditor = new CellEditor()
    {
      DisplayMode = CellEditorDisplayMode.Always,
      Control = (Control) checkBox2,
      CellAlignment = ContentAlignment.MiddleCenter
    };
    this.Disposed += new EventHandler(this.SignsForType_Disposed);
  }

  private void SignsForType_Disposed(object sender, EventArgs e)
  {
    this.nullBitmap?.Dispose();
    this._strongControlEditor?.Dispose();
  }

  private void InitializeTreeViewStyles()
  {
    this.firstRowStyle = new Style(this.graphForTypeTree.RowStyle);
    this.firstRowStyle.Font = new Font(this.firstRowStyle.Font.FontFamily, 9f, FontStyle.Bold);
    this.bigValueStyle = new Style(this.graphForTypeTree.RowStyle);
    this.bigValueStyle.Font = new Font(this.bigValueStyle.Font, FontStyle.Bold);
    this.bigValueStyle.ForeColor = Color.Red;
    this.graphForTypeTree.HeaderContextMenu = new ContextMenuStrip();
  }

  private void buttonsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e.Button == this.addTypeBtn)
      this.ChooseSignDocTypes();
    else if (e.Button == this.addGraphBtn)
    {
      this.ChooseRequiredSigns();
    }
    else
    {
      if (e.Button != this.delBtn)
        return;
      Row parentRow = (Row) null;
      if (this.graphForTypeTree.SelectedRow != null && !(this.graphForTypeTree.SelectedRow.Item is SignsDataItemModel))
        parentRow = this.graphForTypeTree.SelectedRow.ParentRow.Item is SignsDataItemModel ? this.graphForTypeTree.SelectedRow : (this.graphForTypeTree.SelectedRow.ParentRow.Item is SignsGroup ? this.graphForTypeTree.SelectedRow.ParentRow.ParentRow : this.graphForTypeTree.SelectedRow.ParentRow);
      if (this.graphForTypeTree.SelectedItem is SignsDataItem selectedItem2)
        this.SignsDataItemModel.Nodes.Remove(selectedItem2);
      else if (this.graphForTypeTree.SelectedItem is SignsGroup selectedItem1)
      {
        this.RemoveGroupInTree(selectedItem1, parentRow);
      }
      else
      {
        if (!(this.graphForTypeTree.SelectedItem is SignsDataItemChildren selectedItem))
          return;
        SignsDataItem signsDataItem = this.SignsDataItemModel[selectedItem.Parent.ObjectType];
        BindingList<SignsDataItemChildren> children = signsDataItem[selectedItem.GroupID].Children;
        children.Remove(selectedItem);
        if (children.Count != 0)
          return;
        this.RemoveGroupInTree(signsDataItem[selectedItem.GroupID], parentRow);
      }
    }
  }

  private void RemoveGroupInTree(SignsGroup group, Row parentRow)
  {
    BindingList<SignsGroup> groups = this.SignsDataItemModel[group.Parent.ObjectType].Groups;
    groups.Remove(group);
    if (group.GroupID == 0 && groups.Count > 0)
    {
      foreach (SignsGroup signsGroup in (Collection<SignsGroup>) groups)
      {
        --signsGroup.GroupID;
        foreach (SignsDataItemChildren child in (Collection<SignsDataItemChildren>) signsGroup.Children)
          child.GroupID = signsGroup.GroupID;
      }
    }
    if (parentRow?.Item is SignsDataItem)
      parentRow.UpdateChildren(true, false);
    else
      this.graphForTypeTree.UpdateRows(true);
  }

  private void ChooseSignDocTypes()
  {
    using (SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString("ObjectTypes"), 4, false))
    {
      if (selectorForm.ShowDialog() != DialogResult.OK)
        return;
      foreach (object id in selectorForm.IDList)
      {
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(Convert.ToInt32(id));
        if (!this.SignsDataItemModel.Contains(objectTypeGuid))
          this.SignsDataItemModel.Nodes.Add(new SignsDataItem(objectTypeGuid));
      }
    }
  }

  private void ChooseRequiredSigns()
  {
    if (this.addGraphBtn.Tag.ToString() == "-1")
      return;
    SignsDataItem parent = this.SignsDataItemModel[new Guid(this.addGraphBtn.Tag.ToString())];
    using (AddSignGraphsForm addSignGraphsForm = new AddSignGraphsForm())
    {
      addSignGraphsForm.NewGroupBox.Visible = parent.Groups.Count > 0;
      if (addSignGraphsForm.ShowDialog() != DialogResult.OK)
        return;
      GraphInfoList selected = addSignGraphsForm.Selected;
      if (selected.Count <= sc_21885.ssp_workflow_21886(2103092825))
        return;
      if (addSignGraphsForm.NewGroupBox.Checked)
      {
        this._signsGroupID = parent.Groups.Count;
        parent.Groups.Add(new SignsGroup(parent)
        {
          GroupID = this._signsGroupID
        });
      }
      else if (parent.Groups.Count == 0)
      {
        this._signsGroupID = 0;
        parent.Groups.Add(new SignsGroup(parent)
        {
          GroupID = this._signsGroupID
        });
      }
      foreach (GraphInfo graphInfo in (List<GraphInfo>) selected)
        parent.Groups[this._signsGroupID].Children.Add(new SignsDataItemChildren(parent)
        {
          GraphForType = graphInfo.GraphVal,
          StrongControl = graphInfo.StrongSign,
          GroupID = this._signsGroupID
        });
    }
  }

  private void graphForTypeTree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Item is SignsDataItemModel)
    {
      if (!(e.Column.Name == "objectTypeColumn"))
        return;
      e.CellData.Value = (object) "signsDataItemModel";
      e.CellData.EvenStyle = this.firstRowStyle;
      e.CellData.OddStyle = this.firstRowStyle;
    }
    else if (e.Row.Item is SignsDataItem signsDataItem)
    {
      if (e.Column.Name == "objectTypeColumn")
      {
        e.CellData.Value = (object) signsDataItem.ObjectTypeName;
        e.CellData.EvenStyle = this.firstRowStyle;
        e.CellData.OddStyle = this.firstRowStyle;
      }
      else
      {
        if (!(e.Column.Name == "graphColumn"))
          return;
        e.CellData.Value = signsDataItem.SignAnyGraph ? (object) "Подпись в любой графе" : (object) string.Empty;
      }
    }
    else if (e.Row.Item is SignsGroup signsGroup)
    {
      if (signsGroup.GroupID <= 0 || !(e.Column.Name == "graphColumn"))
        return;
      e.CellData.Value = (object) "ИЛИ";
      e.CellData.EvenStyle = this.firstRowStyle;
      e.CellData.OddStyle = this.firstRowStyle;
    }
    else
    {
      if (!(e.Row.Item is SignsDataItemChildren dataItemChildren))
        return;
      switch (e.Column.Name)
      {
        case "graphColumn":
          e.CellData.Value = (object) MiscFunx.GetSignGraphCaption(dataItemChildren.GraphForType);
          break;
        case "strongControlColumn":
          e.CellData.Value = (object) dataItemChildren.StrongControl;
          e.CellData.Editor = this._strongControlEditor;
          break;
      }
    }
  }

  private void graphForTypeTree_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is SignsDataItemModel signsDataItemModel)
      e.Children = (IList) signsDataItemModel.Nodes;
    else if (e.Row.Item is SignsDataItem signsDataItem)
    {
      e.Children = (IList) signsDataItem.Groups;
      e.Row.Expand();
    }
    else
    {
      if (!(e.Row.Item is SignsGroup signsGroup))
        return;
      e.Children = (IList) signsGroup.Children;
      e.Row.Expand();
    }
  }

  private void graphForTypeTree_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!(e.Row.Item is SignsDataItemChildren dataItemChildren) || !(e.Column.Name == "strongControlColumn"))
      return;
    dataItemChildren.StrongControl = Convert.ToBoolean(e.NewValue);
  }

  private void graphForTypeTree_RowCollapse(object sender, RowEventArgs e)
  {
    if (e.Row.Item is SignsGroup)
    {
      e.Row.Expand();
    }
    else
    {
      if (!(e.Row.Item is SignsDataItem))
        return;
      e.Row.Expand();
    }
  }

  private void graphForTypeTree_SelectionChanged(object sender, EventArgs e)
  {
    switch (this.graphForTypeTree.SelectedItem)
    {
      case SignsDataItem signsDataItem:
        this.addGraphBtn.Tag = (object) signsDataItem.ObjectType.ToString();
        this.addGraphBtn.Enabled = true;
        this.delBtn.Enabled = true;
        break;
      case SignsDataItemChildren dataItemChildren:
        this.addGraphBtn.Tag = (object) dataItemChildren.Parent.ObjectType.ToString();
        this.addGraphBtn.Enabled = true;
        this.delBtn.Enabled = true;
        break;
      case SignsGroup signsGroup:
        this.addGraphBtn.Tag = (object) signsGroup.Parent.ObjectType.ToString();
        this.addGraphBtn.Enabled = true;
        this.delBtn.Enabled = true;
        break;
      default:
        this.addGraphBtn.Tag = (object) -1;
        this.addGraphBtn.Enabled = false;
        break;
    }
  }

  private void graphForTypeTree_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.RowData.ImageList == null)
      e.RowData.ImageList = BaseHolder.IconService.ImageList;
    if (!(e.Row.Item is SignsDataItem signsDataItem))
      return;
    e.RowData.ImageIndex = BaseHolder.IconService.IndexOf(4, MetaDataHelper.GetObjectTypeID(signsDataItem.ObjectType));
  }

  private void personalSignsCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this.SignsDataItemModel.Nodes.Count == 0 && this.personalSignsCheckBox.Checked)
      this.personalSignsCheckBox.Checked = false;
    this.SignsDataItemModel.PropertyChanged -= new PropertyChangedEventHandler(this.SignsDataItemModel_PropertyChanged);
    this.SignsDataItemModel.PersonalSigns = this.personalSignsCheckBox.Checked;
    this.SignsDataItemModel.PropertyChanged += new PropertyChangedEventHandler(this.SignsDataItemModel_PropertyChanged);
  }

  private void SignsDataItemModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "PersonalSigns"))
      return;
    this.personalSignsCheckBox.Checked = this.SignsDataItemModel.PersonalSigns;
  }

  private void Nodes_ListChanged(object sender, ListChangedEventArgs e)
  {
    this.personalSignsCheckBox.Enabled = this.SignsDataItemModel.Nodes.Count != 0;
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (!this._isFirst)
      return;
    this.graphForTypeTree.UpdateRows(true);
    this._isFirst = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SignsForType));
    this.buttonsBar = new ToolBar();
    this.addTypeBtn = new ToolBarButton();
    this.addGraphBtn = new ToolBarButton();
    this.delBtn = new ToolBarButton();
    this.buttonsIcons = new ImageList(this.components);
    this.graphForTypeTree = new Infralution.Controls.VirtualTree.VirtualTree();
    this.objectTypeColumn = new Column();
    this.graphColumn = new Column();
    this.strongControlColumn = new Column();
    this.personalSignsCheckBox = new CheckBox();
    this.signsDataItemModelBindingSource = new BindingSource(this.components);
    this.graphForTypeTree.BeginInit();
    ((ISupportInitialize) this.signsDataItemModelBindingSource).BeginInit();
    this.SuspendLayout();
    this.buttonsBar.Appearance = ToolBarAppearance.Flat;
    this.buttonsBar.Buttons.AddRange(new ToolBarButton[3]
    {
      this.addTypeBtn,
      this.addGraphBtn,
      this.delBtn
    });
    this.buttonsBar.Divider = false;
    this.buttonsBar.DropDownArrows = true;
    this.buttonsBar.ImageList = this.buttonsIcons;
    this.buttonsBar.Location = new Point(0, 0);
    this.buttonsBar.Name = "buttonsBar";
    this.buttonsBar.ShowToolTips = true;
    this.buttonsBar.Size = new Size(552, 26);
    this.buttonsBar.TabIndex = 1;
    this.buttonsBar.ButtonClick += new ToolBarButtonClickEventHandler(this.buttonsBar_ButtonClick);
    this.addTypeBtn.ImageIndex = 0;
    this.addTypeBtn.Name = "addTypeBtn";
    this.addTypeBtn.Tag = (object) "0";
    this.addTypeBtn.ToolTipText = "Добавить тип";
    this.addGraphBtn.Enabled = false;
    this.addGraphBtn.ImageIndex = 2;
    this.addGraphBtn.Name = "addGraphBtn";
    this.addGraphBtn.Tag = (object) "-1";
    this.addGraphBtn.ToolTipText = "Добавить графы";
    this.delBtn.Enabled = false;
    this.delBtn.ImageIndex = 1;
    this.delBtn.Name = "delBtn";
    this.delBtn.Tag = (object) "2";
    this.delBtn.ToolTipText = "Удалить запись";
    this.buttonsIcons.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("buttonsIcons.ImageStream");
    this.buttonsIcons.TransparentColor = Color.Fuchsia;
    this.buttonsIcons.Images.SetKeyName(0, "add.ico");
    this.buttonsIcons.Images.SetKeyName(1, "del.ico");
    this.buttonsIcons.Images.SetKeyName(2, "icons8-create-16.png");
    this.graphForTypeTree.AllowDrop = true;
    this.graphForTypeTree.AllowMultiSelect = false;
    this.graphForTypeTree.AutoFitColumns = true;
    this.graphForTypeTree.Columns.Add(this.objectTypeColumn);
    this.graphForTypeTree.Columns.Add(this.graphColumn);
    this.graphForTypeTree.Columns.Add(this.strongControlColumn);
    this.graphForTypeTree.Dock = DockStyle.Fill;
    this.graphForTypeTree.ImageList = (ImageList) null;
    this.graphForTypeTree.LineStyle = LineStyle.None;
    this.graphForTypeTree.Location = new Point(0, 26);
    this.graphForTypeTree.MainColumn = this.objectTypeColumn;
    this.graphForTypeTree.Name = "graphForTypeTree";
    this.graphForTypeTree.ShowRootRow = false;
    this.graphForTypeTree.Size = new Size(552, 161);
    this.graphForTypeTree.SortColumn = this.objectTypeColumn;
    this.graphForTypeTree.TabIndex = 0;
    this.graphForTypeTree.GetCellData += new GetCellDataHandler(this.graphForTypeTree_GetCellData);
    this.graphForTypeTree.GetChildren += new GetChildrenHandler(this.graphForTypeTree_GetChildren);
    this.graphForTypeTree.GetRowData += new GetRowDataHandler(this.graphForTypeTree_GetRowData);
    this.graphForTypeTree.RowCollapse += new RowEventHandler(this.graphForTypeTree_RowCollapse);
    this.graphForTypeTree.SelectionChanged += new EventHandler(this.graphForTypeTree_SelectionChanged);
    this.graphForTypeTree.SetCellValue += new SetCellValueHandler(this.graphForTypeTree_SetCellValue);
    this.objectTypeColumn.Caption = "Тип объекта";
    this.objectTypeColumn.MinWidth = 100;
    this.objectTypeColumn.Movable = false;
    this.objectTypeColumn.Name = "objectTypeColumn";
    this.objectTypeColumn.Width = 196;
    this.graphColumn.Caption = "Графа";
    this.graphColumn.MinWidth = 50;
    this.graphColumn.Movable = false;
    this.graphColumn.Name = "graphColumn";
    this.graphColumn.Width = 146;
    this.strongControlColumn.Caption = "Строгий контроль";
    this.strongControlColumn.MinWidth = 110;
    this.strongControlColumn.Movable = false;
    this.strongControlColumn.Name = "strongControlColumn";
    this.strongControlColumn.Width = 206;
    this.personalSignsCheckBox.AutoSize = true;
    this.personalSignsCheckBox.Dock = DockStyle.Bottom;
    this.personalSignsCheckBox.Enabled = false;
    this.personalSignsCheckBox.Location = new Point(0, 187);
    this.personalSignsCheckBox.Name = "personalSignsCheckBox";
    this.personalSignsCheckBox.Size = new Size(552, 17);
    this.personalSignsCheckBox.TabIndex = 2;
    this.personalSignsCheckBox.Text = "Требовать персональную подпись исполнителя";
    this.personalSignsCheckBox.UseVisualStyleBackColor = true;
    this.personalSignsCheckBox.CheckedChanged += new EventHandler(this.personalSignsCheckBox_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.graphForTypeTree);
    this.Controls.Add((Control) this.personalSignsCheckBox);
    this.Controls.Add((Control) this.buttonsBar);
    this.Name = nameof (SignsForType);
    this.Size = new Size(552, 204);
    this.graphForTypeTree.EndInit();
    ((ISupportInitialize) this.signsDataItemModelBindingSource).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
