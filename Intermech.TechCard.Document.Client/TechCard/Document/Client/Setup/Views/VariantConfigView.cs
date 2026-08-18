// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.Views.VariantConfigView
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup.Views;

public class VariantConfigView : UserControl, IConfigView
{
  [NotNull]
  private IConfigViewSettings _settings;
  [NotNull]
  private IConfigViewController _controller;
  [NotNull]
  private IConfigViewController _variantConditionViewController;
  private bool _isUpdating;
  private IContainer components;
  private TableLayoutPanel tlpPageVariant;
  private Label lblVarProperties;
  private Label lblVarType;
  private ComboBox cbxVarType;
  private Label lblVarNumber;
  private NumericUpDown udVarNumber;
  private CheckBox cbVarDetail;
  private Panel pnlVarCond;

  [NotNull]
  private VariantConfig VariantConfig => this._settings.ConfigElement as VariantConfig;

  private void SetupControls(System.IServiceProvider services)
  {
    this._isUpdating = true;
    this.cbxVarType.DataSource = (object) null;
    this.cbxVarType.Items.Clear();
    this.cbxVarType.DataSource = (object) MetaDataHelper.GetObjectTypesList().OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (item => item.ObjectName)).ToList<IMSObjectType>();
    this.cbxVarType.DisplayMember = "ObjectName";
    this.cbxVarType.ValueMember = "ObjectTypeID";
    this._variantConditionViewController = services.GetService<IConfigViewService>().CreateViewController(DocumentConfigElementType.FormulaFieldContents, services);
    this._isUpdating = false;
  }

  private void EnableControls()
  {
    this._isUpdating = true;
    this.cbxVarType.Enabled = !this._settings.ReadOnly;
    this.udVarNumber.Enabled = !this._settings.ReadOnly;
    this.cbVarDetail.Enabled = !this._settings.ReadOnly;
    this._isUpdating = false;
  }

  private void FillControlsFromConfig()
  {
    this._isUpdating = true;
    IMSObjectType objType = this.VariantConfig.ObjType;
    this.cbxVarType.SelectedValue = (object) (objType != null ? objType.ObjectTypeID : 0);
    this.udVarNumber.Value = (Decimal) this.VariantConfig.Number;
    this.cbVarDetail.Checked = this.VariantConfig.OnDetail;
    IConfigViewController conditionViewController = this._variantConditionViewController;
    Panel pnlVarCond = this.pnlVarCond;
    FieldContentsConfigViewSettings settings = new FieldContentsConfigViewSettings(this._settings.Services);
    settings.ConfigElement = this.VariantConfig.Condition as IDocumentConfigElement;
    settings.ConfigElementType = DocumentConfigElementType.FormulaFieldContents;
    settings.DefaultFieldContentsType = FieldContentsType.Formula;
    settings.ReadOnly = this._settings.ReadOnly;
    settings.OnDataChanged = (Action<IConfigViewController, bool>) ((ctrl, changed) =>
    {
      Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
      if (onDataChanged == null)
        return;
      onDataChanged(this._controller, changed);
    });
    settings.Caption = LocalizationHolder.rm.GetString("TechCard.Document_184");
    conditionViewController.Show((Control) pnlVarCond, (IConfigViewSettings) settings);
    this._isUpdating = false;
  }

  private void SaveValuesFromControls()
  {
    this.VariantConfig.ObjType = this.cbxVarType.Enabled ? this.cbxVarType.SelectedItem as IMSObjectType : (IMSObjectType) null;
    this.VariantConfig.Number = this.udVarNumber.Enabled ? Convert.ToInt32(this.udVarNumber.Value) : 0;
    this.VariantConfig.OnDetail = this.cbVarDetail.Enabled && this.cbVarDetail.Checked;
    IDocumentConfigElement config;
    if (!this._variantConditionViewController.ApplyChanges(out config))
      return;
    this.VariantConfig.Condition = config as IFieldContents;
  }

  private void Control_ValueChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.EnableControls();
    bool flag = false;
    if (sender == this.cbxVarType)
      flag = this.cbxVarType.SelectedItem as IMSObjectType != this.VariantConfig.ObjType;
    else if (sender == this.udVarNumber)
      flag = Convert.ToInt32(this.udVarNumber.Value) != this.VariantConfig.Number;
    else if (sender == this.cbVarDetail)
      flag = this.cbVarDetail.Checked != this.VariantConfig.OnDetail;
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  public bool ApplyChanges(out IDocumentConfigElement config)
  {
    config = (IDocumentConfigElement) this.VariantConfig;
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

  public VariantConfigView([NotNull] IConfigViewController controller, System.IServiceProvider services)
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
    this.tlpPageVariant = new TableLayoutPanel();
    this.lblVarProperties = new Label();
    this.lblVarType = new Label();
    this.cbxVarType = new ComboBox();
    this.lblVarNumber = new Label();
    this.udVarNumber = new NumericUpDown();
    this.cbVarDetail = new CheckBox();
    this.pnlVarCond = new Panel();
    this.tlpPageVariant.SuspendLayout();
    this.udVarNumber.BeginInit();
    this.SuspendLayout();
    this.tlpPageVariant.ColumnCount = 4;
    this.tlpPageVariant.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageVariant.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110f));
    this.tlpPageVariant.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpPageVariant.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageVariant.Controls.Add((Control) this.lblVarProperties, 1, 1);
    this.tlpPageVariant.Controls.Add((Control) this.lblVarType, 1, 2);
    this.tlpPageVariant.Controls.Add((Control) this.cbxVarType, 2, 2);
    this.tlpPageVariant.Controls.Add((Control) this.lblVarNumber, 1, 3);
    this.tlpPageVariant.Controls.Add((Control) this.udVarNumber, 2, 3);
    this.tlpPageVariant.Controls.Add((Control) this.cbVarDetail, 1, 4);
    this.tlpPageVariant.Controls.Add((Control) this.pnlVarCond, 1, 6);
    this.tlpPageVariant.Dock = DockStyle.Fill;
    this.tlpPageVariant.Location = new Point(0, 0);
    this.tlpPageVariant.Margin = new Padding(0);
    this.tlpPageVariant.Name = "tlpPageVariant";
    this.tlpPageVariant.RowCount = 8;
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 10f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle(SizeType.Absolute, 115f));
    this.tlpPageVariant.RowStyles.Add(new RowStyle());
    this.tlpPageVariant.Size = new Size(430, 506);
    this.tlpPageVariant.TabIndex = 126;
    this.lblVarProperties.AutoSize = true;
    this.lblVarProperties.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblVarProperties.Location = new Point(10, 10);
    this.lblVarProperties.Margin = new Padding(0);
    this.lblVarProperties.Name = "lblVarProperties";
    this.lblVarProperties.Size = new Size(67, 13);
    this.lblVarProperties.TabIndex = 112 /*0x70*/;
    this.lblVarProperties.Text = "Свойства:";
    this.lblVarType.AutoSize = true;
    this.lblVarType.Location = new Point(10, 32 /*0x20*/);
    this.lblVarType.Margin = new Padding(0, 2, 0, 0);
    this.lblVarType.Name = "lblVarType";
    this.lblVarType.Size = new Size(83, 13);
    this.lblVarType.TabIndex = 116;
    this.lblVarType.Text = "Тип записи ТП";
    this.cbxVarType.FormattingEnabled = true;
    this.cbxVarType.Location = new Point(120, 30);
    this.cbxVarType.Margin = new Padding(0);
    this.cbxVarType.Name = "cbxVarType";
    this.cbxVarType.Size = new Size(220, 21);
    this.cbxVarType.TabIndex = 117;
    this.cbxVarType.SelectedValueChanged += new EventHandler(this.Control_ValueChanged);
    this.lblVarNumber.AutoSize = true;
    this.lblVarNumber.Location = new Point(10, 57);
    this.lblVarNumber.Margin = new Padding(0, 2, 0, 0);
    this.lblVarNumber.Name = "lblVarNumber";
    this.lblVarNumber.Size = new Size(100, 13);
    this.lblVarNumber.TabIndex = 118;
    this.lblVarNumber.Text = "Номер по порядку";
    this.udVarNumber.Location = new Point(120, 55);
    this.udVarNumber.Margin = new Padding(0);
    this.udVarNumber.Name = "udVarNumber";
    this.udVarNumber.Size = new Size(50, 20);
    this.udVarNumber.TabIndex = 119;
    this.udVarNumber.ValueChanged += new EventHandler(this.Control_ValueChanged);
    this.cbVarDetail.AutoSize = true;
    this.tlpPageVariant.SetColumnSpan((Control) this.cbVarDetail, 2);
    this.cbVarDetail.Location = new Point(10, 80 /*0x50*/);
    this.cbVarDetail.Margin = new Padding(0);
    this.cbVarDetail.Name = "cbVarDetail";
    this.cbVarDetail.Size = new Size(140, 17);
    this.cbVarDetail.TabIndex = 120;
    this.cbVarDetail.Text = "Вариант для ГТП\\ТТП";
    this.cbVarDetail.UseVisualStyleBackColor = true;
    this.cbVarDetail.CheckedChanged += new EventHandler(this.Control_ValueChanged);
    this.tlpPageVariant.SetColumnSpan((Control) this.pnlVarCond, 2);
    this.pnlVarCond.Dock = DockStyle.Fill;
    this.pnlVarCond.Location = new Point(10, 125);
    this.pnlVarCond.Margin = new Padding(0);
    this.pnlVarCond.Name = "pnlVarCond";
    this.pnlVarCond.Size = new Size(410, 115);
    this.pnlVarCond.TabIndex = 125;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpPageVariant);
    this.Name = nameof (VariantConfigView);
    this.Size = new Size(430, 506);
    this.tlpPageVariant.ResumeLayout(false);
    this.tlpPageVariant.PerformLayout();
    this.udVarNumber.EndInit();
    this.ResumeLayout(false);
  }
}
