// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.Step2
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>
/// 
/// </summary>
internal class Step2 : UserControl
{
  private Button _next;
  private Button _prev;
  private bool _useButtons;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel4;
  private TextBox _txtType;
  private Label label1;
  private PictureBox pictureBox2;
  private Panel panel1;
  private Panel panel2;
  private Label label8;
  private Label label7;
  private PictureBox pictureBox1;
  private Label label6;
  private NumericUpDown numericUpDown3;
  private Label label5;
  private NumericUpDown numericUpDown2;
  private Label label4;
  private NumericUpDown numericUpDown1;
  private Label label3;
  private ComboBox _cmbArrange;
  private Label label2;
  private Panel panel3;
  private TableLayoutPanel tableLayoutPanel1;
  private Label label9;
  private Label label10;
  private Label _btnBottommost;
  private Label _btnDown;
  private Label _btnUp;
  private Label _btnTopmost;
  private ListBox _lstAttr;
  private Label _lbMsg;
  private GroupBox _groupBox1;

  /// <summary>
  /// 
  /// </summary>
  public ArrayList AttributeModels => new ArrayList((ICollection) this._lstAttr.Items);

  /// <summary>
  /// 
  /// </summary>
  public Point OriginLocation
  {
    get
    {
      return new Point(Convert.ToInt32(this.numericUpDown1.Value), Convert.ToInt32(this.numericUpDown2.Value));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int OriginBetween => Convert.ToInt32(this.numericUpDown3.Value);

  /// <summary>
  /// 
  /// </summary>
  public string[] Attributes
  {
    get => (string[]) null;
    set
    {
      this._lstAttr.BeginUpdate();
      try
      {
        this._lstAttr.Items.Clear();
        float num = 8f;
        using (Graphics graphics = this.CreateGraphics())
          num = graphics.MeasureString("w", this.Font).Width;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (string str in value)
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(str, false);
            if (attributeType != null)
            {
              AttributeModel attributeModel = new AttributeModel(str, LabelArrange.laLeft);
              attributeModel.ControlType = ComponentTypeProducer.GetComponentType(attributeType.MultipleValued, attributeType.AttributeType, attributeType.Computed);
              if (attributeType.AttributeType == FieldTypes.ftString)
                attributeModel.Width = Convert.ToInt32((float) attributeType.SizeType * num);
              this._lstAttr.Items.Add((object) attributeModel);
            }
          }
        }
      }
      finally
      {
        this._lstAttr.EndUpdate();
      }
      this._lstAttr.SelectedIndex = 0;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool UseButtons
  {
    get => this._useButtons;
    set
    {
      this._useButtons = this.Visible = value;
      if (!value)
        return;
      this._next.Visible = true;
      this._prev.Visible = true;
      this._prev.Enabled = true;
    }
  }

  /// <summary>Коструктор.</summary>
  /// <param name="next"></param>
  /// <param name="prev"></param>
  public Step2(Button next, Button prev)
  {
    this.InitializeComponent();
    this._next = next;
    this._prev = prev;
    for (LabelArrange la = LabelArrange.laNone; la <= LabelArrange.laTop; ++la)
      this._cmbArrange.Items.Add((object) LabelArrangeHelper.GetCaption(la));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lstAttr_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._lstAttr.SelectedItem == null)
    {
      this._btnTopmost.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottommost.Enabled = false;
    }
    else
    {
      AttributeModel selectedItem = this._lstAttr.SelectedItem as AttributeModel;
      if (selectedItem.ControlType == (System.Type) null)
      {
        this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_2");
      }
      else
      {
        this._txtType.Text = selectedItem.ControlType.Name;
        if (selectedItem.ControlType == typeof (Label))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_166");
        else if (selectedItem.ControlType == typeof (PictureBox))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_170");
        else if (selectedItem.ControlType == typeof (AttrTextEdit))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_172");
        else if (selectedItem.ControlType == typeof (AttrPassword))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_173");
        else if (selectedItem.ControlType == typeof (AttrListBox))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_174");
        else if (selectedItem.ControlType == typeof (AttrComboBox))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_175");
        else if (selectedItem.ControlType == typeof (AttrCheckedListBox))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_176");
        else if (selectedItem.ControlType == typeof (AttrMemoEdit))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_177");
        else if (selectedItem.ControlType == typeof (AttrDateEdit))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_178");
        else if (selectedItem.ControlType == typeof (AttrCheckBox))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_179");
        else if (selectedItem.ControlType == typeof (AttrTextBtn))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_180");
        else if (selectedItem.ControlType == typeof (AttrTextBtnComp))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_181");
        else if (selectedItem.ControlType == typeof (AttrListBoxBtn))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_182");
        if (selectedItem.ControlType == typeof (AttrObjectsList))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_182_t");
        else if (selectedItem.ControlType == typeof (AttrMeasuredEdit))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_183");
        else if (selectedItem.ControlType == typeof (AttrMeasuredListBox))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_184");
        else if (selectedItem.ControlType == typeof (AttrButton))
          this._txtType.Text = LocalizationHolder.rm.GetString("FormDesigner_185");
      }
      this._cmbArrange.SelectedItem = (object) LabelArrangeHelper.GetCaption(selectedItem.Arrange);
      int selectedIndex = this._lstAttr.SelectedIndex;
      this._btnTopmost.Enabled = this._btnUp.Enabled = selectedIndex > 0;
      this._btnBottommost.Enabled = this._btnDown.Enabled = selectedIndex < this._lstAttr.Items.Count - 1;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbArrange_SelectedIndexChanged(object sender, EventArgs e)
  {
    foreach (AttributeModel attributeModel in this._lstAttr.Items)
      attributeModel.Arrange = LabelArrangeHelper.GetEnumValue(Convert.ToString(this._cmbArrange.SelectedItem));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void MoveButtonsClick(object sender, EventArgs e)
  {
    if (this._lstAttr.SelectedItem == null || !(sender is Label label))
      return;
    AttributeModel selectedItem = this._lstAttr.SelectedItem as AttributeModel;
    int selectedIndex = this._lstAttr.SelectedIndex;
    if (label == this._btnTopmost)
    {
      this._lstAttr.Items.Remove((object) selectedItem);
      this._lstAttr.Items.Insert(0, (object) selectedItem);
    }
    else if (label == this._btnUp)
    {
      this._lstAttr.Items.Remove((object) selectedItem);
      this._lstAttr.Items.Insert(selectedIndex - 1, (object) selectedItem);
    }
    else if (label == this._btnDown)
    {
      this._lstAttr.Items.Remove((object) selectedItem);
      this._lstAttr.Items.Insert(selectedIndex + 1, (object) selectedItem);
    }
    else if (label == this._btnBottommost)
    {
      this._lstAttr.Items.Remove((object) selectedItem);
      this._lstAttr.Items.Add((object) selectedItem);
    }
    this._lstAttr.SelectedItem = (object) selectedItem;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step2));
    this.panel4 = new Panel();
    this._btnBottommost = new Label();
    this._btnDown = new Label();
    this._btnUp = new Label();
    this._btnTopmost = new Label();
    this.panel3 = new Panel();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.label9 = new Label();
    this.label10 = new Label();
    this._txtType = new TextBox();
    this.label1 = new Label();
    this._lstAttr = new ListBox();
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.label8 = new Label();
    this.label7 = new Label();
    this.pictureBox1 = new PictureBox();
    this.label6 = new Label();
    this.numericUpDown3 = new NumericUpDown();
    this.label5 = new Label();
    this.numericUpDown2 = new NumericUpDown();
    this.label4 = new Label();
    this.numericUpDown1 = new NumericUpDown();
    this.label3 = new Label();
    this._cmbArrange = new ComboBox();
    this.label2 = new Label();
    this.pictureBox2 = new PictureBox();
    this._lbMsg = new Label();
    this._groupBox1 = new GroupBox();
    this.panel4.SuspendLayout();
    this.panel3.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.numericUpDown3.BeginInit();
    this.numericUpDown2.BeginInit();
    this.numericUpDown1.BeginInit();
    ((ISupportInitialize) this.pictureBox2).BeginInit();
    this.SuspendLayout();
    this.panel4.BackColor = SystemColors.Control;
    this.panel4.Controls.Add((Control) this._btnBottommost);
    this.panel4.Controls.Add((Control) this._btnDown);
    this.panel4.Controls.Add((Control) this._btnUp);
    this.panel4.Controls.Add((Control) this._btnTopmost);
    this.panel4.Controls.Add((Control) this.panel3);
    this.panel4.Controls.Add((Control) this._txtType);
    this.panel4.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    this._btnBottommost.Image = (Image) Intermech.FormDesigner.Properties.Resources.Bottommost;
    componentResourceManager.ApplyResources((object) this._btnBottommost, "_btnBottommost");
    this._btnBottommost.Name = "_btnBottommost";
    this._btnBottommost.Click += new EventHandler(this.MoveButtonsClick);
    this._btnDown.Image = (Image) Intermech.FormDesigner.Properties.Resources.Down;
    componentResourceManager.ApplyResources((object) this._btnDown, "_btnDown");
    this._btnDown.Name = "_btnDown";
    this._btnDown.Click += new EventHandler(this.MoveButtonsClick);
    this._btnUp.Image = (Image) Intermech.FormDesigner.Properties.Resources.Up;
    componentResourceManager.ApplyResources((object) this._btnUp, "_btnUp");
    this._btnUp.Name = "_btnUp";
    this._btnUp.Click += new EventHandler(this.MoveButtonsClick);
    this._btnTopmost.Image = (Image) Intermech.FormDesigner.Properties.Resources.Topmost;
    componentResourceManager.ApplyResources((object) this._btnTopmost, "_btnTopmost");
    this._btnTopmost.Name = "_btnTopmost";
    this._btnTopmost.Click += new EventHandler(this.MoveButtonsClick);
    this.panel3.Controls.Add((Control) this.tableLayoutPanel1);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.label9, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.label10, 1, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this._txtType, "_txtType");
    this._txtType.Name = "_txtType";
    this._txtType.ReadOnly = true;
    this._txtType.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this._lstAttr, "_lstAttr");
    this._lstAttr.Name = "_lstAttr";
    this._lstAttr.SelectedIndexChanged += new EventHandler(this.On_lstAttr_SelectedIndexChanged);
    this.panel1.BackColor = Color.FromArgb(100, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    this.panel1.Controls.Add((Control) this.panel2);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel2.BackColor = Color.Transparent;
    this.panel2.Controls.Add((Control) this.label8);
    this.panel2.Controls.Add((Control) this.label7);
    this.panel2.Controls.Add((Control) this.pictureBox1);
    this.panel2.Controls.Add((Control) this.label6);
    this.panel2.Controls.Add((Control) this.numericUpDown3);
    this.panel2.Controls.Add((Control) this.label5);
    this.panel2.Controls.Add((Control) this.numericUpDown2);
    this.panel2.Controls.Add((Control) this.label4);
    this.panel2.Controls.Add((Control) this.numericUpDown1);
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Controls.Add((Control) this._cmbArrange);
    this.panel2.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    this.pictureBox1.Image = (Image) Intermech.FormDesigner.Properties.Resources.Arrow_Right;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.numericUpDown3, "numericUpDown3");
    this.numericUpDown3.Maximum = new Decimal(new int[4]
    {
      25,
      0,
      0,
      0
    });
    this.numericUpDown3.Minimum = new Decimal(new int[4]
    {
      2,
      0,
      0,
      0
    });
    this.numericUpDown3.Name = "numericUpDown3";
    this.numericUpDown3.Value = new Decimal(new int[4]
    {
      8,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.numericUpDown2, "numericUpDown2");
    this.numericUpDown2.Maximum = new Decimal(new int[4]
    {
      600,
      0,
      0,
      0
    });
    this.numericUpDown2.Minimum = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numericUpDown2.Name = "numericUpDown2";
    this.numericUpDown2.Value = new Decimal(new int[4]
    {
      8,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.numericUpDown1, "numericUpDown1");
    this.numericUpDown1.Maximum = new Decimal(new int[4]
    {
      800,
      0,
      0,
      0
    });
    this.numericUpDown1.Minimum = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numericUpDown1.Name = "numericUpDown1";
    this.numericUpDown1.Value = new Decimal(new int[4]
    {
      8,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this._cmbArrange, "_cmbArrange");
    this._cmbArrange.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbArrange.Name = "_cmbArrange";
    this._cmbArrange.SelectedIndexChanged += new EventHandler(this.On_chbArrange_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.pictureBox2.BackgroundImage = (Image) Intermech.FormDesigner.Properties.Resources.Horizontal_Line;
    componentResourceManager.ApplyResources((object) this.pictureBox2, "pictureBox2");
    this.pictureBox2.Name = "pictureBox2";
    this.pictureBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this._lbMsg, "_lbMsg");
    this._lbMsg.ForeColor = Color.Red;
    this._lbMsg.Name = "_lbMsg";
    componentResourceManager.ApplyResources((object) this._groupBox1, "_groupBox1");
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.TabStop = false;
    this.Controls.Add((Control) this._lstAttr);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.pictureBox2);
    this.Controls.Add((Control) this.panel4);
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._lbMsg);
    this.MinimumSize = new Size(630, 400);
    this.Name = nameof (Step2);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.panel4.ResumeLayout(false);
    this.panel4.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.numericUpDown3.EndInit();
    this.numericUpDown2.EndInit();
    this.numericUpDown1.EndInit();
    ((ISupportInitialize) this.pictureBox2).EndInit();
    this.ResumeLayout(false);
  }
}
