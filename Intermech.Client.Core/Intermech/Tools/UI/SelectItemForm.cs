
// Type: Intermech.Tools.UI.SelectItemForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Tools.UI;

public class SelectItemForm : Form
{
  private IEnumerable items;
  private object selectedItem;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView lvItems;
  private Label lbDescription;
  private Button btOK;
  private Button btCancel;
  private PictureBox pictureBox1;
  private CheckBox cbMakeDefault;
  private TableLayoutPanel tlbMainPanel;
  private ToolTip toolTips;

  public SelectItemForm()
  {
    this.InitializeComponent();
    this.ShowMakeDefaultBox = false;
    this.MakeDefault = false;
    this.TopMost = true;
  }

  public string Description
  {
    get => this.lbDescription.Text;
    set
    {
      this.lbDescription.Text = value;
      this.toolTips.SetToolTip((Control) this.lbDescription, value);
    }
  }

  public bool ShowMakeDefaultBox
  {
    get => this.cbMakeDefault.Visible;
    set => this.cbMakeDefault.Visible = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable Items
  {
    get => this.items;
    set => this.items = value;
  }

  [Browsable(false)]
  public object SelectedItem => this.selectedItem;

  public bool MakeDefault
  {
    get => this.cbMakeDefault.Checked;
    set => this.cbMakeDefault.Checked = value;
  }

  private void SelectItemForm_Shown(object sender, EventArgs e)
  {
    this.lvItems.BeginUpdate();
    try
    {
      this.lvItems.Items.Clear();
      foreach (object obj in this.items)
        this.lvItems.Items.Add(new ListViewItem(obj.ToString())
        {
          Tag = obj
        });
      if (this.lvItems.Items.Count > 0)
        this.lvItems.Items[0].Selected = true;
      this.UpdateButtons();
    }
    finally
    {
      this.lvItems.EndUpdate();
    }
  }

  private void lvItems_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateButtons();

  private void lvItems_DoubleClick(object sender, EventArgs e)
  {
    if (!this.btOK.Enabled)
      return;
    this.btOK.PerformClick();
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    this.selectedItem = this.lvItems.SelectedItems[0].Tag;
  }

  private void UpdateButtons() => this.btOK.Enabled = this.lvItems.SelectedItems.Count > 0;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectItemForm));
    this.lvItems = new ListView();
    this.lbDescription = new Label();
    this.btOK = new Button();
    this.btCancel = new Button();
    this.pictureBox1 = new PictureBox();
    this.cbMakeDefault = new CheckBox();
    this.tlbMainPanel = new TableLayoutPanel();
    this.toolTips = new ToolTip(this.components);
    ColumnHeader columnHeader = new ColumnHeader();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.tlbMainPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) columnHeader, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.lvItems, "lvItems");
    this.lvItems.Columns.AddRange(new ColumnHeader[1]
    {
      columnHeader
    });
    this.lvItems.FullRowSelect = true;
    this.lvItems.HideSelection = false;
    this.lvItems.MultiSelect = false;
    this.lvItems.Name = "lvItems";
    this.lvItems.UseCompatibleStateImageBehavior = false;
    this.lvItems.View = View.Details;
    this.lvItems.SelectedIndexChanged += new EventHandler(this.lvItems_SelectedIndexChanged);
    this.lvItems.DoubleClick += new EventHandler(this.lvItems_DoubleClick);
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    this.btOK.UseVisualStyleBackColor = true;
    this.btOK.Click += new EventHandler(this.btOK_Click);
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbMakeDefault, "cbMakeDefault");
    this.cbMakeDefault.Name = "cbMakeDefault";
    this.cbMakeDefault.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tlbMainPanel, "tlbMainPanel");
    this.tlbMainPanel.Controls.Add((Control) this.cbMakeDefault, 0, 1);
    this.tlbMainPanel.Controls.Add((Control) this.lvItems, 0, 0);
    this.tlbMainPanel.Name = "tlbMainPanel";
    this.toolTips.AutoPopDelay = 50000;
    this.toolTips.InitialDelay = 500;
    this.toolTips.ReshowDelay = 100;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tlbMainPanel);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.pictureBox1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectItemForm);
    this.ShowInTaskbar = false;
    this.Shown += new EventHandler(this.SelectItemForm_Shown);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.tlbMainPanel.ResumeLayout(false);
    this.tlbMainPanel.PerformLayout();
    this.ResumeLayout(false);
  }
}
