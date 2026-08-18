// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.Views.PictureFieldConfigView
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.TechCard.Document.Client.Configs.Visual;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup.Views;

public class PictureFieldConfigView : UserControl, IConfigView
{
  [NotNull]
  private IConfigViewController _controller;
  [NotNull]
  private IConfigViewSettings _settings;
  private bool _isUpdating;
  private IContainer components;
  private TableLayoutPanel tlpPageSketch;
  private Label lblSketchProperties;
  private CheckBox cbSketchField;
  private ComboBox cbxSketchType;
  private Label lblSketchType;

  [NotNull]
  private PictureFieldConfig SketchConfig => this._settings.ConfigElement as PictureFieldConfig;

  private void SetupControls()
  {
    this._isUpdating = true;
    this.cbxSketchType.BindEnumToCombobox<SketchTypes>(SketchTypes.Dwg, (Func<SketchTypes, bool>) (source => Array.IndexOf<SketchTypes>(new SketchTypes[2]
    {
      SketchTypes.Unsupported,
      SketchTypes.Pdf
    }, source) < 0));
    this._isUpdating = false;
  }

  private void FillControlsFromConfig()
  {
    this._isUpdating = true;
    this.cbSketchField.Checked = this.SketchConfig.SketchField;
    this.cbxSketchType.SelectedValue = (object) (SketchTypes) (this.SketchConfig.SketchField ? (int) this.SketchConfig.SketchType : 1);
    this._isUpdating = false;
  }

  private void SaveValuesFromControls()
  {
    this.SketchConfig.SketchField = this.cbSketchField.Checked;
    this.SketchConfig.SketchType = this.SketchConfig.SketchField ? this.cbxSketchType.SelectedValue.ToString().ToEnum<SketchTypes>() : SketchTypes.Unsupported;
  }

  private void EnableControls()
  {
    this.cbSketchField.Enabled = !this._settings.ReadOnly;
    this.cbxSketchType.Enabled = !this._settings.ReadOnly && this.cbSketchField.Checked;
  }

  private void cbSketchField_CheckedChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.EnableControls();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, this.cbSketchField.Checked != this.SketchConfig.SketchField);
  }

  private void cbxSketchType_SelectedValueChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.EnableControls();
    SketchTypes sketchTypes = this.cbxSketchType.SelectedValue.ToString().ToEnum<SketchTypes>();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, sketchTypes != this.SketchConfig.SketchType);
  }

  public bool ApplyChanges(out IDocumentConfigElement config)
  {
    config = (IDocumentConfigElement) this.SketchConfig;
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

  public PictureFieldConfigView([NotNull] IConfigViewController controller, System.IServiceProvider services)
  {
    this.InitializeComponent();
    this._controller = controller;
    this.SetupControls();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tlpPageSketch = new TableLayoutPanel();
    this.lblSketchProperties = new Label();
    this.cbSketchField = new CheckBox();
    this.cbxSketchType = new ComboBox();
    this.lblSketchType = new Label();
    this.tlpPageSketch.SuspendLayout();
    this.SuspendLayout();
    this.tlpPageSketch.ColumnCount = 3;
    this.tlpPageSketch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageSketch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpPageSketch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
    this.tlpPageSketch.Controls.Add((Control) this.lblSketchProperties, 1, 1);
    this.tlpPageSketch.Controls.Add((Control) this.cbSketchField, 1, 2);
    this.tlpPageSketch.Controls.Add((Control) this.cbxSketchType, 1, 4);
    this.tlpPageSketch.Controls.Add((Control) this.lblSketchType, 1, 3);
    this.tlpPageSketch.Dock = DockStyle.Fill;
    this.tlpPageSketch.Location = new Point(0, 0);
    this.tlpPageSketch.Margin = new Padding(0);
    this.tlpPageSketch.Name = "tlpPageSketch";
    this.tlpPageSketch.RowCount = 5;
    this.tlpPageSketch.RowStyles.Add(new RowStyle(SizeType.Absolute, 10f));
    this.tlpPageSketch.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageSketch.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageSketch.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpPageSketch.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpPageSketch.Size = new Size(169, 146);
    this.tlpPageSketch.TabIndex = 114;
    this.lblSketchProperties.AutoSize = true;
    this.lblSketchProperties.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblSketchProperties.Location = new Point(10, 10);
    this.lblSketchProperties.Margin = new Padding(0);
    this.lblSketchProperties.Name = "lblSketchProperties";
    this.lblSketchProperties.Size = new Size(67, 13);
    this.lblSketchProperties.TabIndex = 111;
    this.lblSketchProperties.Text = "Свойства:";
    this.cbSketchField.AutoSize = true;
    this.cbSketchField.Location = new Point(10, 30);
    this.cbSketchField.Margin = new Padding(0);
    this.cbSketchField.Name = "cbSketchField";
    this.cbSketchField.Size = new Size(91, 17);
    this.cbSketchField.TabIndex = 112 /*0x70*/;
    this.cbSketchField.Text = "Поле эскиза";
    this.cbSketchField.UseVisualStyleBackColor = true;
    this.cbSketchField.CheckedChanged += new EventHandler(this.cbSketchField_CheckedChanged);
    this.cbxSketchType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxSketchType.FormattingEnabled = true;
    this.cbxSketchType.Location = new Point(10, 75);
    this.cbxSketchType.Margin = new Padding(0);
    this.cbxSketchType.Name = "cbxSketchType";
    this.cbxSketchType.Size = new Size(100, 21);
    this.cbxSketchType.TabIndex = 113;
    this.cbxSketchType.SelectedValueChanged += new EventHandler(this.cbxSketchType_SelectedValueChanged);
    this.lblSketchType.AutoSize = true;
    this.lblSketchType.Location = new Point(10, 55);
    this.lblSketchType.Margin = new Padding(0);
    this.lblSketchType.Name = "lblSketchType";
    this.lblSketchType.Size = new Size(65, 13);
    this.lblSketchType.TabIndex = 114;
    this.lblSketchType.Text = "Тип эскиза";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpPageSketch);
    this.Name = "SketchConfigView";
    this.Size = new Size(169, 146);
    this.tlpPageSketch.ResumeLayout(false);
    this.tlpPageSketch.PerformLayout();
    this.ResumeLayout(false);
  }
}
