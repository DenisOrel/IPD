
// Type: Intermech.Navigator.NodeColumnsSetupForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Summary description for NodeColumnsSetupForm.</summary>
public class NodeColumnsSetupForm : Form
{
  private NodeColumnCollection _supportedColumns;
  private NodeColumnCollection _columns;
  private Button btOk;
  private Button btCancel;
  private GroupBox gbAttributes;
  private Button btDown;
  private Button btUp;
  private Button btRemove;
  private Button btAdd;
  private ListBox lbAttributes;
  private Label lbWidth;
  private TextBox tbWidth;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public NodeColumnsSetupForm() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NodeColumnsSetupForm));
    this.btOk = new Button();
    this.btCancel = new Button();
    this.gbAttributes = new GroupBox();
    this.tbWidth = new TextBox();
    this.lbWidth = new Label();
    this.btDown = new Button();
    this.btUp = new Button();
    this.btRemove = new Button();
    this.btAdd = new Button();
    this.lbAttributes = new ListBox();
    this.gbAttributes.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btOk, "btOk");
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Name = "btOk";
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    componentResourceManager.ApplyResources((object) this.gbAttributes, "gbAttributes");
    this.gbAttributes.Controls.Add((Control) this.tbWidth);
    this.gbAttributes.Controls.Add((Control) this.lbWidth);
    this.gbAttributes.Controls.Add((Control) this.btDown);
    this.gbAttributes.Controls.Add((Control) this.btUp);
    this.gbAttributes.Controls.Add((Control) this.btRemove);
    this.gbAttributes.Controls.Add((Control) this.btAdd);
    this.gbAttributes.Controls.Add((Control) this.lbAttributes);
    this.gbAttributes.FlatStyle = FlatStyle.System;
    this.gbAttributes.Name = "gbAttributes";
    this.gbAttributes.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tbWidth, "tbWidth");
    this.tbWidth.Name = "tbWidth";
    this.tbWidth.Validating += new CancelEventHandler(this.tbWidth_Validating);
    componentResourceManager.ApplyResources((object) this.lbWidth, "lbWidth");
    this.lbWidth.Name = "lbWidth";
    componentResourceManager.ApplyResources((object) this.btDown, "btDown");
    this.btDown.Name = "btDown";
    this.btDown.Click += new EventHandler(this.btDown_Click);
    componentResourceManager.ApplyResources((object) this.btUp, "btUp");
    this.btUp.Name = "btUp";
    this.btUp.Click += new EventHandler(this.btUp_Click);
    componentResourceManager.ApplyResources((object) this.btRemove, "btRemove");
    this.btRemove.Name = "btRemove";
    this.btRemove.Click += new EventHandler(this.btRemove_Click);
    componentResourceManager.ApplyResources((object) this.btAdd, "btAdd");
    this.btAdd.Name = "btAdd";
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    componentResourceManager.ApplyResources((object) this.lbAttributes, "lbAttributes");
    this.lbAttributes.Name = "lbAttributes";
    this.lbAttributes.SelectedIndexChanged += new EventHandler(this.lbAttributes_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.btOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.gbAttributes);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NodeColumnsSetupForm);
    this.ShowInTaskbar = false;
    this.gbAttributes.ResumeLayout(false);
    this.gbAttributes.PerformLayout();
    this.ResumeLayout(false);
  }

  public NodeColumnCollection Columns => this._columns;

  public DialogResult ShowDialog(
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns)
  {
    this._supportedColumns = supportedColumns;
    this._columns = columns;
    for (int index = 0; index < this._columns.Count; ++index)
      this.lbAttributes.Items.Add((object) this._columns[index].Caption);
    this.UpdateButtons();
    return this.ShowDialog();
  }

  private void btAdd_Click(object sender, EventArgs e)
  {
    NodeColumnCollection columnCollection = AppendColumnDialog.ShowDialog(this._supportedColumns, this._columns);
    if (columnCollection == null)
      return;
    for (int index = 0; index < columnCollection.Count; ++index)
    {
      this._columns.Add(columnCollection[index]);
      this.lbAttributes.Items.Add((object) columnCollection[index].Caption);
    }
  }

  private void btRemove_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbAttributes.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this._columns.RemoveAt(selectedIndex);
    this.lbAttributes.Items.RemoveAt(selectedIndex);
    if (selectedIndex >= this.lbAttributes.Items.Count)
      return;
    this.lbAttributes.SelectedIndex = selectedIndex;
  }

  private void btUp_Click(object sender, EventArgs e)
  {
    if (this.lbAttributes.SelectedIndex <= 0)
      return;
    int selectedIndex = this.lbAttributes.SelectedIndex;
    object obj = this.lbAttributes.Items[selectedIndex];
    this.lbAttributes.Items[selectedIndex] = this.lbAttributes.Items[selectedIndex - 1];
    this.lbAttributes.Items[selectedIndex - 1] = obj;
    NodeColumn column = this._columns[selectedIndex];
    this._columns[selectedIndex] = this._columns[selectedIndex - 1];
    this._columns[selectedIndex - 1] = column;
    this.lbAttributes.SelectedIndex = selectedIndex - 1;
  }

  private void btDown_Click(object sender, EventArgs e)
  {
    if (this.lbAttributes.SelectedIndex < 0 || this.lbAttributes.SelectedIndex + 1 >= this.lbAttributes.Items.Count)
      return;
    int selectedIndex = this.lbAttributes.SelectedIndex;
    object obj = this.lbAttributes.Items[selectedIndex];
    this.lbAttributes.Items[selectedIndex] = this.lbAttributes.Items[selectedIndex + 1];
    this.lbAttributes.Items[selectedIndex + 1] = obj;
    NodeColumn column = this._columns[selectedIndex];
    this._columns[selectedIndex] = this._columns[selectedIndex + 1];
    this._columns[selectedIndex + 1] = column;
    this.lbAttributes.SelectedIndex = selectedIndex + 1;
  }

  private void lbAttributes_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateButtons();
  }

  private void tbWidth_Validating(object sender, CancelEventArgs e)
  {
    if (this.lbAttributes.SelectedIndex < 0)
      return;
    try
    {
      int num = int.Parse(this.tbWidth.Text);
      if (num < 20)
        e.Cancel = true;
      else
        this._columns[this.lbAttributes.SelectedIndex].Width = num;
    }
    catch
    {
      e.Cancel = true;
    }
  }

  private void UpdateButtons()
  {
    int selectedIndex = this.lbAttributes.SelectedIndex;
    if (selectedIndex >= 0)
    {
      this.tbWidth.Enabled = true;
      this.tbWidth.Text = this._columns[this.lbAttributes.SelectedIndex].Width.ToString();
    }
    else
    {
      this.tbWidth.Enabled = false;
      this.tbWidth.Text = "";
    }
    this.btRemove.Enabled = selectedIndex >= 0;
    this.btUp.Enabled = selectedIndex >= 1;
    this.btDown.Enabled = selectedIndex >= 0 && selectedIndex < this.lbAttributes.Items.Count - 1;
  }
}
