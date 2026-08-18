
// Type: Intermech.Search.UI.SelectCommandDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public sealed class SelectCommandDialog : Form
{
  private const string CommandNameColumnKey = "CommandName";
  private ICategoryTypeIconService _categoryTypeIconService;
  private INamedImageList _namedImageList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Search.UI.VirtualTree.VirtualTree _virtualTree;
  private Button button2;
  private Button button1;
  private Panel panel2;
  private Column _commandNameColumn;
  private TableLayoutPanel tableLayoutPanel1;
  private TextBox _textBox;
  private FlowLayoutPanel flowLayoutPanel1;

  public SelectCommandDialog()
  {
    this.InitializeComponent();
    this._commandNameColumn.DataField = "CommandName";
    this._virtualTree.DataSource = new object();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string[] SelectedCommands
  {
    get
    {
      return this._virtualTree.SelectedItems.Cast<MenuTemplateNode>().Select<MenuTemplateNode, string>((Func<MenuTemplateNode, string>) (o => o.Name)).ToArray<string>();
    }
  }

  private void SelectCommandDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this._textBox.AutoCompleteCustomSource.AddRange(this.GetAllCommandNames());
  }

  private string[] GetAllCommandNames()
  {
    return ServiceLocator.Get<IFactory>().ContextMenuTemplate.GetDescendents().Select<MenuTemplateNode, string>((Func<MenuTemplateNode, string>) (o => o.Text)).Where<string>((Func<string, bool>) (o => !string.IsNullOrEmpty(o))).Distinct<string>().ToArray<string>();
  }

  private void SelectCommandDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void TextBox_TextChanged(object sender, EventArgs e)
  {
    this._virtualTree.UpdateRows(true);
    this._virtualTree.RootRow.ExpandChildren(true);
  }

  private void VirtualTree_CellDoubleClick(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void VirtualTree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Column.DataField == "CommandName") || !(e.Row.Item is MenuTemplateNode menuTemplateNode))
      return;
    e.CellData.Value = (object) menuTemplateNode.Text;
  }

  private void VirtualTree_GetChildren(object sender, GetChildrenEventArgs e)
  {
    MenuTemplateNode menuTemplateNode = e.Row.Item as MenuTemplateNode;
    IEnumerable<MenuTemplateNode> source = menuTemplateNode != null ? menuTemplateNode.Nodes.Where<MenuTemplateNode>((Func<MenuTemplateNode, bool>) (node => node.Name.IndexOf("CreateTypeInComposition") < 0)) : (IEnumerable<MenuTemplateNode>) ServiceLocator.Get<IFactory>().ContextMenuTemplate.Nodes;
    if (!string.IsNullOrEmpty(this._textBox.Text))
      source = source.Where<MenuTemplateNode>((Func<MenuTemplateNode, bool>) (o => o.IsSelfOrDescendentsContainsText(this._textBox.Text)));
    e.Children = (IList) source.OrderBy<MenuTemplateNode, string>((Func<MenuTemplateNode, string>) (o => o.Text)).ToList<MenuTemplateNode>();
  }

  private void VirtualTree_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is MenuTemplateNode menuTemplateNode))
      return;
    e.RowData.ImageSize = 32 /*0x20*/;
    if (menuTemplateNode.Image != null)
    {
      e.RowData.Image = menuTemplateNode.Image;
    }
    else
    {
      if (menuTemplateNode.ImageListSource == ImageListSource.CategoryImageList)
        e.RowData.ImageList = ServiceLocator.Get<ICategoryTypeIconService>().ImageList;
      else if (menuTemplateNode.ImageListSource == ImageListSource.NamedImageList)
        e.RowData.ImageList = ServiceLocator.Get<INamedImageList>().ImageList;
      e.RowData.ImageIndex = menuTemplateNode.ImageIndex;
    }
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
    this.button2 = new Button();
    this.button1 = new Button();
    this.panel2 = new Panel();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._virtualTree = new Intermech.Search.UI.VirtualTree.VirtualTree();
    this._commandNameColumn = new Column();
    this._textBox = new TextBox();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.panel2.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this._virtualTree.BeginInit();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.button2.AutoSize = true;
    this.button2.DialogResult = DialogResult.OK;
    this.button2.Location = new Point(263, 3);
    this.button2.Name = "button2";
    this.button2.Size = new Size(75, 23);
    this.button2.TabIndex = 0;
    this.button2.Text = "OK";
    this.button2.UseVisualStyleBackColor = true;
    this.button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.button1.AutoSize = true;
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Location = new Point(344, 3);
    this.button1.Name = "button1";
    this.button1.Size = new Size(75, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "Отмена";
    this.button1.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.tableLayoutPanel1);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(428, 381);
    this.panel2.TabIndex = 2;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this._virtualTree, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this._textBox, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 2);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 3;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(428, 381);
    this.tableLayoutPanel1.TabIndex = 1;
    this._virtualTree.AllowDrop = true;
    this._virtualTree.Columns.Add(this._commandNameColumn);
    this._virtualTree.Dock = DockStyle.Fill;
    this._virtualTree.IconWidth = 0;
    this._virtualTree.ImageList = (ImageList) null;
    this._virtualTree.LineStyle = LineStyle.Dot;
    this._virtualTree.Location = new Point(3, 29);
    this._virtualTree.MainColumn = this._commandNameColumn;
    this._virtualTree.Name = "_virtualTree";
    this._virtualTree.ShowColumnHeaders = false;
    this._virtualTree.ShowRootRow = false;
    this._virtualTree.Size = new Size(422, 314);
    this._virtualTree.TabIndex = 0;
    this._virtualTree.CellDoubleClick += new EventHandler(this.VirtualTree_CellDoubleClick);
    this._virtualTree.GetCellData += new GetCellDataHandler(this.VirtualTree_GetCellData);
    this._virtualTree.GetChildren += new GetChildrenHandler(this.VirtualTree_GetChildren);
    this._virtualTree.GetRowData += new GetRowDataHandler(this.VirtualTree_GetRowData);
    this._commandNameColumn.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this._commandNameColumn.Caption = (string) null;
    this._commandNameColumn.Name = "_commandNameColumn";
    this._commandNameColumn.Width = 30;
    this._textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
    this._textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
    this._textBox.Dock = DockStyle.Fill;
    this._textBox.Location = new Point(3, 3);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(422, 20);
    this._textBox.TabIndex = 1;
    this._textBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.Controls.Add((Control) this.button1);
    this.flowLayoutPanel1.Controls.Add((Control) this.button2);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 349);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(422, 29);
    this.flowLayoutPanel1.TabIndex = 2;
    this.AcceptButton = (IButtonControl) this.button2;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button1;
    this.ClientSize = new Size(428, 381);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (SelectCommandDialog);
    this.ShowIcon = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выберите команду";
    this.FormClosed += new FormClosedEventHandler(this.SelectCommandDialog_FormClosed);
    this.Load += new EventHandler(this.SelectCommandDialog_Load);
    this.panel2.ResumeLayout(false);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this._virtualTree.EndInit();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
