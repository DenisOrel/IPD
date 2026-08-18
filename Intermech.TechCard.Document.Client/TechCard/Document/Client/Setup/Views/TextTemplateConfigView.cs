// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.Views.TextTemplateConfigView
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Localization;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup.Views;

public class TextTemplateConfigView : UserControl, IConfigView
{
  [NotNull]
  private IConfigViewSettings _settings;
  [NotNull]
  private readonly IConfigViewController _controller;
  private bool _isUpdating;
  private IConfigViewController _fieldConditionViewController;
  private IConfigViewController _fieldValueViewController;
  private IContainer components;
  private TableLayoutPanel tlpPageTemplate;
  private CheckBox cbTempCalcOnFill;
  private CheckBox cbTempNotRepeat;
  private TableLayoutPanel tlpDigitsNum;
  private Label lblTempDigits;
  private NumericUpDown udTempDigits;
  private Panel pnlFieldCond;
  private Panel pnlFieldValue;

  [NotNull]
  private TextFieldConfig TextTemplateConfig => this._settings.ConfigElement as TextFieldConfig;

  private void SetupControls(System.IServiceProvider services)
  {
    this._isUpdating = true;
    IConfigViewService service = services.GetService<IConfigViewService>();
    this._fieldConditionViewController = service.CreateViewController(DocumentConfigElementType.FormulaFieldContents, services);
    this._fieldValueViewController = service.CreateViewController(DocumentConfigElementType.AttributeFieldContents, services);
    this._isUpdating = false;
  }

  private void EnableControls()
  {
    this._isUpdating = true;
    this.cbTempNotRepeat.Enabled = !this._settings.ReadOnly;
    this.cbTempCalcOnFill.Enabled = !this._settings.ReadOnly;
    this.udTempDigits.Enabled = !this._settings.ReadOnly;
    this._isUpdating = false;
  }

  private void FillControlsFromConfig()
  {
    this._isUpdating = true;
    this.cbTempNotRepeat.Checked = this.TextTemplateConfig.NotRepeated;
    this.cbTempCalcOnFill.Checked = this.TextTemplateConfig.CalcOnFill;
    this.udTempDigits.Value = (Decimal) this.TextTemplateConfig.Digits;
    IConfigViewController conditionViewController = this._fieldConditionViewController;
    Panel pnlFieldCond = this.pnlFieldCond;
    FieldContentsConfigViewSettings settings1 = new FieldContentsConfigViewSettings(this._settings.Services);
    settings1.ConfigElement = this.TextTemplateConfig.Condition as IDocumentConfigElement;
    settings1.ConfigElementType = DocumentConfigElementType.FormulaFieldContents;
    settings1.DefaultFieldContentsType = FieldContentsType.Formula;
    settings1.ReadOnly = this._settings.ReadOnly;
    settings1.OnDataChanged = (Action<IConfigViewController, bool>) ((ctrl, changed) =>
    {
      Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
      if (onDataChanged == null)
        return;
      onDataChanged(this._controller, changed);
    });
    settings1.Caption = LocalizationHolder.rm.GetString("TechCard.Document_184");
    conditionViewController.Show((Control) pnlFieldCond, (IConfigViewSettings) settings1);
    IConfigViewController valueViewController = this._fieldValueViewController;
    Panel pnlFieldValue = this.pnlFieldValue;
    FieldContentsConfigViewSettings settings2 = new FieldContentsConfigViewSettings(this._settings.Services);
    settings2.ConfigElement = this.TextTemplateConfig.FieldContents as IDocumentConfigElement;
    settings2.ConfigElementType = DocumentConfigElementType.AttributeFieldContents;
    settings2.DefaultFieldContentsType = FieldContentsType.Attribute;
    settings2.DataType = DataType.String;
    settings2.ReadOnly = this._settings.ReadOnly;
    settings2.OnDataChanged = (Action<IConfigViewController, bool>) ((ctrl, changed) =>
    {
      Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
      if (onDataChanged == null)
        return;
      onDataChanged(this._controller, changed);
    });
    settings2.Caption = LocalizationHolder.rm.GetString("TechCard.Document_185");
    valueViewController.Show((Control) pnlFieldValue, (IConfigViewSettings) settings2);
    this._isUpdating = false;
  }

  private void SaveValuesFromControls()
  {
    this.TextTemplateConfig.NotRepeated = this.cbTempNotRepeat.Enabled && this.cbTempNotRepeat.Checked;
    this.TextTemplateConfig.CalcOnFill = this.cbTempCalcOnFill.Enabled && this.cbTempCalcOnFill.Checked;
    this.TextTemplateConfig.Digits = this.udTempDigits.Enabled ? Convert.ToInt32(this.udTempDigits.Value) : 0;
    IDocumentConfigElement config1;
    if (this._fieldValueViewController.ApplyChanges(out config1))
      this.TextTemplateConfig.FieldContents = config1 as IFieldContents;
    IDocumentConfigElement config2;
    if (!this._fieldConditionViewController.ApplyChanges(out config2))
      return;
    this.TextTemplateConfig.Condition = config2 as IFieldContents;
  }

  private void Control_ValueChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.EnableControls();
    bool flag = false;
    if (sender == this.cbTempNotRepeat)
      flag = this.cbTempNotRepeat.Checked != this.TextTemplateConfig.NotRepeated;
    if (sender == this.cbTempCalcOnFill)
      flag = this.cbTempCalcOnFill.Checked != this.TextTemplateConfig.CalcOnFill;
    else if (sender == this.udTempDigits)
      flag = Convert.ToInt32(this.udTempDigits.Value) != this.TextTemplateConfig.Digits;
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  public bool ApplyChanges(out IDocumentConfigElement config)
  {
    config = (IDocumentConfigElement) this.TextTemplateConfig;
    if (this._settings.ReadOnly)
      return false;
    this.SaveValuesFromControls();
    return true;
  }

  public void CancelChanges()
  {
    if (this._settings.ReadOnly)
      return;
    this.SetupView(this._settings);
  }

  public void SetupView(IConfigViewSettings settings)
  {
    this._settings = settings;
    this.FillControlsFromConfig();
    this.EnableControls();
  }

  public TextTemplateConfigView([NotNull] IConfigViewController controller, System.IServiceProvider services)
  {
    this.InitializeComponent();
    this._controller = controller;
    this.SetupControls(services);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tlpPageTemplate = new TableLayoutPanel();
    this.cbTempCalcOnFill = new CheckBox();
    this.cbTempNotRepeat = new CheckBox();
    this.tlpDigitsNum = new TableLayoutPanel();
    this.lblTempDigits = new Label();
    this.udTempDigits = new NumericUpDown();
    this.pnlFieldCond = new Panel();
    this.pnlFieldValue = new Panel();
    this.tlpPageTemplate.SuspendLayout();
    this.tlpDigitsNum.SuspendLayout();
    this.udTempDigits.BeginInit();
    this.SuspendLayout();
    this.tlpPageTemplate.ColumnCount = 3;
    this.tlpPageTemplate.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageTemplate.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpPageTemplate.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageTemplate.Controls.Add((Control) this.cbTempCalcOnFill, 1, 6);
    this.tlpPageTemplate.Controls.Add((Control) this.cbTempNotRepeat, 1, 5);
    this.tlpPageTemplate.Controls.Add((Control) this.tlpDigitsNum, 1, 7);
    this.tlpPageTemplate.Controls.Add((Control) this.pnlFieldCond, 1, 1);
    this.tlpPageTemplate.Controls.Add((Control) this.pnlFieldValue, 1, 3);
    this.tlpPageTemplate.Dock = DockStyle.Fill;
    this.tlpPageTemplate.Location = new Point(0, 0);
    this.tlpPageTemplate.Margin = new Padding(0);
    this.tlpPageTemplate.Name = "tlpPageTemplate";
    this.tlpPageTemplate.RowCount = 8;
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 10f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 115f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 115f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageTemplate.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageTemplate.Size = new Size(626, 665);
    this.tlpPageTemplate.TabIndex = 123;
    this.cbTempCalcOnFill.AutoSize = true;
    this.cbTempCalcOnFill.Location = new Point(10, 305);
    this.cbTempCalcOnFill.Margin = new Padding(0);
    this.cbTempCalcOnFill.Name = "cbTempCalcOnFill";
    this.cbTempCalcOnFill.Size = new Size((int) byte.MaxValue, 17);
    this.cbTempCalcOnFill.TabIndex = 119;
    this.cbTempCalcOnFill.Text = "Рассчитывать при формировании документа";
    this.cbTempCalcOnFill.UseVisualStyleBackColor = true;
    this.cbTempCalcOnFill.Click += new EventHandler(this.Control_ValueChanged);
    this.cbTempNotRepeat.AutoSize = true;
    this.cbTempNotRepeat.Location = new Point(10, 280);
    this.cbTempNotRepeat.Margin = new Padding(0);
    this.cbTempNotRepeat.Name = "cbTempNotRepeat";
    this.cbTempNotRepeat.Size = new Size(95, 17);
    this.cbTempNotRepeat.TabIndex = 110;
    this.cbTempNotRepeat.Text = "Не повторять";
    this.cbTempNotRepeat.UseVisualStyleBackColor = true;
    this.cbTempNotRepeat.Click += new EventHandler(this.Control_ValueChanged);
    this.tlpDigitsNum.ColumnCount = 2;
    this.tlpDigitsNum.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 172f));
    this.tlpDigitsNum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpDigitsNum.Controls.Add((Control) this.lblTempDigits, 0, 0);
    this.tlpDigitsNum.Controls.Add((Control) this.udTempDigits, 1, 0);
    this.tlpDigitsNum.Dock = DockStyle.Fill;
    this.tlpDigitsNum.Location = new Point(10, 330);
    this.tlpDigitsNum.Margin = new Padding(0);
    this.tlpDigitsNum.Name = "tlpDigitsNum";
    this.tlpDigitsNum.RowCount = 1;
    this.tlpDigitsNum.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tlpDigitsNum.Size = new Size(606, 335);
    this.tlpDigitsNum.TabIndex = 120;
    this.lblTempDigits.AutoSize = true;
    this.lblTempDigits.Location = new Point(0, 2);
    this.lblTempDigits.Margin = new Padding(0, 2, 0, 0);
    this.lblTempDigits.Name = "lblTempDigits";
    this.lblTempDigits.Size = new Size(159, 13);
    this.lblTempDigits.TabIndex = 120;
    this.lblTempDigits.Text = "Количество цифр после точки";
    this.udTempDigits.Location = new Point(172, 0);
    this.udTempDigits.Margin = new Padding(0);
    this.udTempDigits.Name = "udTempDigits";
    this.udTempDigits.Size = new Size(50, 20);
    this.udTempDigits.TabIndex = 121;
    this.udTempDigits.ValueChanged += new EventHandler(this.Control_ValueChanged);
    this.pnlFieldCond.Dock = DockStyle.Fill;
    this.pnlFieldCond.Location = new Point(10, 10);
    this.pnlFieldCond.Margin = new Padding(0);
    this.pnlFieldCond.Name = "pnlFieldCond";
    this.pnlFieldCond.Size = new Size(606, 115);
    this.pnlFieldCond.TabIndex = 121;
    this.pnlFieldValue.Dock = DockStyle.Fill;
    this.pnlFieldValue.Location = new Point(10, 145);
    this.pnlFieldValue.Margin = new Padding(0);
    this.pnlFieldValue.Name = "pnlFieldValue";
    this.pnlFieldValue.Size = new Size(606, 115);
    this.pnlFieldValue.TabIndex = 122;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpPageTemplate);
    this.Name = nameof (TextTemplateConfigView);
    this.Size = new Size(626, 665);
    this.tlpPageTemplate.ResumeLayout(false);
    this.tlpPageTemplate.PerformLayout();
    this.tlpDigitsNum.ResumeLayout(false);
    this.tlpDigitsNum.PerformLayout();
    this.udTempDigits.EndInit();
    this.ResumeLayout(false);
  }
}
