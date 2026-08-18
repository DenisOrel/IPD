
// Type: Intermech.Navigator.Conditions.FormulaConditionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class FormulaConditionForm : ConditionForm
{
  private int _attributeID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bOK;
  private Button bCancel;
  private TextBoxButton tbbLeftAttribute;
  private Label label1;
  private ComboBox cbRelationalOperators;
  private Label label2;
  private Label label3;
  private TextBox textBox1;
  private Panel panel1;
  private Button button1;
  private Button bLeftAttributeClean;

  public FormulaConditionForm() => this.InitializeComponent();

  protected override void OnInitialized()
  {
    if (this.conditionStructure.Value != null && this.conditionStructure.Value is ConditionFormula)
    {
      ConditionFormula conditionFormula = (ConditionFormula) this.conditionStructure.Value;
      this._attributeID = this.dataProvider.GetAttributeID(this.conditionStructure.Attribute);
      this.textBox1.Text = conditionFormula.Formula;
      this.ReloadControl();
      if (this._attributeID != 0)
      {
        for (int index = 0; index < this.cbRelationalOperators.Items.Count; ++index)
        {
          if (this.conditionStructure.RelationalOperator == (RelationalOperators) this.cbRelationalOperators.Items[index])
          {
            this.cbRelationalOperators.SelectedIndex = index;
            break;
          }
        }
      }
    }
    this.ButtonOkRefresh();
  }

  private void ReloadControl()
  {
    this.cbRelationalOperators.Items.Clear();
    if (this._attributeID != 0)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._attributeID);
      this.tbbLeftAttribute.SetText(attributeType.Name);
      foreach (int relationalOperator in AttributeCacheHelper.GetFormulaRelationalOperators(attributeType.RealFieldType == FieldTypes.ftString))
        this.cbRelationalOperators.Items.Add((object) (RelationalOperators) relationalOperator);
    }
    else
      this.tbbLeftAttribute.SetText(string.Empty);
    this.bLeftAttributeClean.Enabled = this._attributeID != 0;
    this.ButtonOkRefresh();
  }

  private void ButtonOkRefresh()
  {
    this.bOK.Enabled = this._attributeID != 0 && this.cbRelationalOperators.SelectedItem != null || this.textBox1.Text.Length > 0;
  }

  public override ConditionStructure Result
  {
    get
    {
      this.conditionStructure.Attribute = (object) this._attributeID;
      this.conditionStructure.RelationalOperator = this._attributeID == 0 || this.cbRelationalOperators.SelectedItem == null ? RelationalOperators.Equal : (RelationalOperators) this.cbRelationalOperators.SelectedItem;
      this.conditionStructure.Value = (object) new ConditionFormula(this.textBox1.Text, Array.Empty<DBDataParam>());
      return this.conditionStructure;
    }
  }

  private bool TextBoxButton1_OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    ConditionAttributeInfo conditionAttributeInfo = SelectAttributeHelper.Select(this.dataProvider, this.objectTypeIDs, (object) this._attributeID);
    if (conditionAttributeInfo == null || (int) conditionAttributeInfo.Id == this._attributeID)
      return false;
    this._attributeID = (int) conditionAttributeInfo.Id;
    this.ReloadControl();
    return true;
  }

  private void LeftAttributeClean_Click(object sender, EventArgs e)
  {
    this._attributeID = 0;
    this.ReloadControl();
  }

  private void Button1_Click(object sender, EventArgs e)
  {
    ConditionAttributeInfo conditionAttributeInfo = SelectAttributeHelper.Select(this.dataProvider, this.objectTypeIDs, (object) this._attributeID);
    if (conditionAttributeInfo == null || (int) conditionAttributeInfo.Id == this._attributeID)
      return;
    this.textBox1.Paste($"[{conditionAttributeInfo.Name}]");
  }

  private void RelationalOperators_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ButtonOkRefresh();
  }

  private void TextBox1_TextChanged(object sender, EventArgs e) => this.ButtonOkRefresh();

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
    this.bOK = new Button();
    this.bCancel = new Button();
    this.tbbLeftAttribute = new TextBoxButton();
    this.label1 = new Label();
    this.cbRelationalOperators = new ComboBox();
    this.label2 = new Label();
    this.label3 = new Label();
    this.textBox1 = new TextBox();
    this.panel1 = new Panel();
    this.button1 = new Button();
    this.bLeftAttributeClean = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(111, 289);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 4;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(238, 289);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 5;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.tbbLeftAttribute.AutoSize = true;
    this.tbbLeftAttribute.Dock = DockStyle.Fill;
    this.tbbLeftAttribute.Location = new Point(0, 0);
    this.tbbLeftAttribute.Margin = new Padding(0);
    this.tbbLeftAttribute.Name = "tbbLeftAttribute";
    this.tbbLeftAttribute.Size = new Size(288, 26);
    this.tbbLeftAttribute.TabIndex = 0;
    this.tbbLeftAttribute.OnOpenDialog += new OnOpenDialogEventHandler(this.TextBoxButton1_OnOpenDialog);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(35, 25);
    this.label1.Name = "label1";
    this.label1.Size = new Size(125, 13);
    this.label1.TabIndex = 19;
    this.label1.Text = "Атрибут для сравнения";
    this.cbRelationalOperators.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbRelationalOperators.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbRelationalOperators.FormattingEnabled = true;
    this.cbRelationalOperators.Location = new Point(38, 101);
    this.cbRelationalOperators.Name = "cbRelationalOperators";
    this.cbRelationalOperators.Size = new Size(321, 21);
    this.cbRelationalOperators.TabIndex = 1;
    this.cbRelationalOperators.SelectedIndexChanged += new EventHandler(this.RelationalOperators_SelectedIndexChanged);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(35, 85);
    this.label2.Name = "label2";
    this.label2.Size = new Size(56, 13);
    this.label2.TabIndex = 21;
    this.label2.Text = "Оператор";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(35, 145);
    this.label3.Name = "label3";
    this.label3.Size = new Size(55, 13);
    this.label3.TabIndex = 22;
    this.label3.Text = "Формула";
    this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.textBox1.Location = new Point(38, 161);
    this.textBox1.Multiline = true;
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(321, 73);
    this.textBox1.TabIndex = 2;
    this.textBox1.TextChanged += new EventHandler(this.TextBox1_TextChanged);
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Controls.Add((Control) this.tbbLeftAttribute);
    this.panel1.Location = new Point(38, 41);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(288, 26);
    this.panel1.TabIndex = 24;
    this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.button1.Location = new Point(229, 240 /*0xF0*/);
    this.button1.Name = "button1";
    this.button1.Size = new Size(130, 27);
    this.button1.TabIndex = 3;
    this.button1.Text = "Вставить атрибут";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.Button1_Click);
    this.bLeftAttributeClean.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bLeftAttributeClean.Image = (Image) Resources.Clean;
    this.bLeftAttributeClean.Location = new Point(332, 41);
    this.bLeftAttributeClean.Name = "bLeftAttributeClean";
    this.bLeftAttributeClean.Size = new Size(27, 26);
    this.bLeftAttributeClean.TabIndex = 25;
    this.bLeftAttributeClean.Text = "-";
    this.bLeftAttributeClean.UseVisualStyleBackColor = true;
    this.bLeftAttributeClean.Click += new EventHandler(this.LeftAttributeClean_Click);
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(371, 328);
    this.Controls.Add((Control) this.bLeftAttributeClean);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.cbRelationalOperators);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.MinimumSize = new Size(290, 320);
    this.Name = nameof (FormulaConditionForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Сравнение значений атрибутов";
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
