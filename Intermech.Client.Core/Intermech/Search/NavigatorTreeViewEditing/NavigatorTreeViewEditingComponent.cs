
// Type: Intermech.Search.NavigatorTreeViewEditing.NavigatorTreeViewEditingComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.NavigatorTreeViewEditing;

public sealed class NavigatorTreeViewEditingComponent : AttributeEditingComponent
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public NavigatorTreeViewEditingComponent() => this.InitializeComponent();

  public NavigatorTreeViewEditingComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
  }

  protected override void DoAttach()
  {
    this.NavigatorTreeView.CellClick += new EventHandler(this.NavigatorTreeView_CellClick);
  }

  protected override void DoDetach()
  {
    this.NavigatorTreeView.CellClick -= new EventHandler(this.NavigatorTreeView_CellClick);
  }

  protected override void DoHideEditor()
  {
    this.NavigatorTreeView.BuildTree -= new EventHandler(this.NavigatorTreeView_BuildTree);
    this.NavigatorTreeView.MouseWheel -= new MouseEventHandler(this.NavigatorTreeView_MouseWheel);
    this.NavigatorTreeView.SortColumnChanged -= new EventHandler(this.NavigatorTreeView_SortColumnChanged);
  }

  protected override void DoShowEditor()
  {
    this.NavigatorTreeView.BuildTree += new EventHandler(this.NavigatorTreeView_BuildTree);
    this.NavigatorTreeView.MouseWheel += new MouseEventHandler(this.NavigatorTreeView_MouseWheel);
    this.NavigatorTreeView.SortColumnChanged += new EventHandler(this.NavigatorTreeView_SortColumnChanged);
  }

  public override int[] GetPresentAttributes()
  {
    return this.NavigatorTreeView.GetColumns().Select<NodeColumn, IMSAttributeType>((Func<NodeColumn, IMSAttributeType>) (o => o.Attribute)).Where<IMSAttributeType>((Func<IMSAttributeType, bool>) (o => o != null)).Select<IMSAttributeType, int>((Func<IMSAttributeType, int>) (o => o.AttributeID)).Distinct<int>().ToArray<int>();
  }

  private void NavigatorTreeView_BuildTree(object sender, EventArgs e) => this.HideEditor();

  private void NavigatorTreeView_CellClick(object sender, EventArgs e)
  {
    if (sender is NavigatorCellWidget navigatorCellWidget)
    {
      NavigatorTreeColumn column = navigatorCellWidget.Column as NavigatorTreeColumn;
      NavigatorTreeNode navigatorTreeNode = navigatorCellWidget.Row.Item as NavigatorTreeNode;
      if (column != null && navigatorTreeNode != null)
      {
        this.NodeColumn = column.NavigatorColumn;
        this.NodeID = navigatorTreeNode.NodeID;
        this.Bounds = navigatorCellWidget.Bounds;
        this.InitializeEditor();
        this.SetCellsReadOnly(navigatorTreeNode);
        if (!this.IsUndetermined)
        {
          this.ShowEditor();
        }
        else
        {
          if (this.NodeColumn == null)
            return;
          navigatorTreeNode.SetCellReadOnly(this.NodeColumn, true);
        }
      }
      else
        this.SetUndetermined();
    }
    else
      this.SetUndetermined();
  }

  private void NavigatorTreeView_MouseWheel(object sender, MouseEventArgs e) => this.HideEditor();

  private void NavigatorTreeView_SortColumnChanged(object sender, EventArgs e) => this.HideEditor();

  private NavigatorTreeView NavigatorTreeView => (NavigatorTreeView) this.Control;

  private void SetCellsReadOnly(NavigatorTreeNode navigatorTreeNode)
  {
    if (this.AttributesValues == null || this.NodeColumn == null)
      return;
    foreach (AttributeValues attributesValue in this.AttributesValues)
      navigatorTreeNode.SetCellReadOnly(this.NodeColumn, attributesValue.ReadOnly);
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
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
