
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.SelectAttributeStepControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class SelectAttributeStepControl : UserControl
{
  private IConditionDataProvider _dataProvider;
  private int[] _objectTypeIDs;
  private bool _selfSet;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox groupBox1;
  private Label label2;
  private RadioButton rbRelation;
  private RadioButton rbObject;
  private RadioButton rbAuto;
  private Panel pAttributeControl;
  private TextBox textBox1;
  private Button bAttrSelect;

  public SelectAttributeStepControl(IConditionDataProvider dataProvider, int[] objectTypeIDs)
  {
    this.InitializeComponent();
    this._dataProvider = dataProvider;
    this._objectTypeIDs = objectTypeIDs;
  }

  public ConditionAttributeInfo Attribute { get; set; }

  public AttributeSourceTypes AttributeSource
  {
    get
    {
      if (this.rbObject.Checked)
        return AttributeSourceTypes.Object;
      return this.rbRelation.Checked ? AttributeSourceTypes.Relation : AttributeSourceTypes.Auto;
    }
    set
    {
      this._selfSet = true;
      try
      {
        if (value == AttributeSourceTypes.Object)
          this.rbObject.Checked = true;
        else if (value == AttributeSourceTypes.Relation)
          this.rbRelation.Checked = true;
        else
          this.rbAuto.Checked = true;
      }
      finally
      {
        this._selfSet = false;
      }
    }
  }

  public event StepControlStateChangedHandler StepControlStateChanged;

  public event AttributeForConditionChangedHandler AttributeForConditionChanged;

  public void RefreshControl()
  {
    StepControlStateChangedHandler controlStateChanged = this.StepControlStateChanged;
    if (controlStateChanged != null)
      controlStateChanged((object) this, new StepControlStateChangedEventArgs(this.Attribute != null));
    AttributeForConditionChangedHandler conditionChanged = this.AttributeForConditionChanged;
    if (conditionChanged != null)
      conditionChanged((object) this, this.Attribute != null ? new AttributeForConditionChangedEventArgs(this.Attribute.Id, this.Attribute.Name) : new AttributeForConditionChangedEventArgs());
    this.textBox1.Text = this.Attribute != null ? this.Attribute.Name : string.Empty;
  }

  private void bAttrSelect_Click(object sender, EventArgs e)
  {
    ConditionAttributeInfo attribute = SelectAttributeHelper.Select(this._dataProvider, this._objectTypeIDs, this.AttributeSource, this.Attribute?.Id);
    if (attribute == null)
      return;
    this.SetAttribute(attribute);
  }

  private void CheckedChanged(object sender, EventArgs e)
  {
    if (this._selfSet || !((RadioButton) sender).Checked)
      return;
    this.SetAttribute(0);
  }

  private void SetAttribute(ConditionAttributeInfo attribute)
  {
    this.Attribute = attribute;
    this.RefreshControl();
  }

  private void SetAttribute(int attributeID)
  {
    this.Attribute = attributeID == 0 ? (ConditionAttributeInfo) null : new ConditionAttributeInfo((object) attributeID, this._dataProvider.GetAttributeName((object) attributeID), this._dataProvider.GetFieldType((object) attributeID));
    this.RefreshControl();
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
    this.groupBox1 = new GroupBox();
    this.pAttributeControl = new Panel();
    this.textBox1 = new TextBox();
    this.bAttrSelect = new Button();
    this.label2 = new Label();
    this.rbRelation = new RadioButton();
    this.rbObject = new RadioButton();
    this.rbAuto = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.pAttributeControl.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.pAttributeControl);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.rbRelation);
    this.groupBox1.Controls.Add((Control) this.rbObject);
    this.groupBox1.Controls.Add((Control) this.rbAuto);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(435, 192 /*0xC0*/);
    this.groupBox1.TabIndex = 12;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Атрибут для сравнения";
    this.pAttributeControl.Controls.Add((Control) this.textBox1);
    this.pAttributeControl.Controls.Add((Control) this.bAttrSelect);
    this.pAttributeControl.Location = new Point(28, 149);
    this.pAttributeControl.Name = "pAttributeControl";
    this.pAttributeControl.Size = new Size(396, 30);
    this.pAttributeControl.TabIndex = 11;
    this.textBox1.BackColor = SystemColors.Window;
    this.textBox1.Location = new Point(4, 3);
    this.textBox1.Multiline = true;
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    this.textBox1.Size = new Size(356, 23);
    this.textBox1.TabIndex = 15;
    this.bAttrSelect.Location = new Point(366, 3);
    this.bAttrSelect.Name = "bAttrSelect";
    this.bAttrSelect.Size = new Size(26, 24);
    this.bAttrSelect.TabIndex = 14;
    this.bAttrSelect.Text = "...";
    this.bAttrSelect.UseVisualStyleBackColor = true;
    this.bAttrSelect.Click += new EventHandler(this.bAttrSelect_Click);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(28, 37);
    this.label2.Name = "label2";
    this.label2.Size = new Size(97, 13);
    this.label2.TabIndex = 10;
    this.label2.Text = "Принадлежность:";
    this.rbRelation.AutoSize = true;
    this.rbRelation.Location = new Point(47, 110);
    this.rbRelation.Name = "rbRelation";
    this.rbRelation.Size = new Size(98, 17);
    this.rbRelation.TabIndex = 8;
    this.rbRelation.Text = "Атрибут связи";
    this.rbRelation.UseVisualStyleBackColor = true;
    this.rbRelation.CheckedChanged += new EventHandler(this.CheckedChanged);
    this.rbObject.AutoSize = true;
    this.rbObject.Location = new Point(47, 87);
    this.rbObject.Name = "rbObject";
    this.rbObject.Size = new Size(110, 17);
    this.rbObject.TabIndex = 6;
    this.rbObject.Text = "Атрибут объекта";
    this.rbObject.UseVisualStyleBackColor = true;
    this.rbObject.CheckedChanged += new EventHandler(this.CheckedChanged);
    this.rbAuto.AutoSize = true;
    this.rbAuto.Checked = true;
    this.rbAuto.Location = new Point(47, 64 /*0x40*/);
    this.rbAuto.Name = "rbAuto";
    this.rbAuto.Size = new Size(126, 17);
    this.rbAuto.TabIndex = 4;
    this.rbAuto.TabStop = true;
    this.rbAuto.Text = "Источник не указан";
    this.rbAuto.UseVisualStyleBackColor = true;
    this.rbAuto.CheckedChanged += new EventHandler(this.CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.groupBox1);
    this.Name = nameof (SelectAttributeStepControl);
    this.Size = new Size(435, 192 /*0xC0*/);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.pAttributeControl.ResumeLayout(false);
    this.pAttributeControl.PerformLayout();
    this.ResumeLayout(false);
  }
}
