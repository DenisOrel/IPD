// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.Views.FieldContentsConfigView
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Expert.User;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.TechCard.Document.Client.Configs.Visual;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup.Views;

public class FieldContentsConfigView : UserControl, IConfigView
{
  [NotNull]
  private IConfigViewSettings _settings;
  [NotNull]
  private readonly IConfigViewController _controller;
  private bool _isUpdating;
  [CanBeNull]
  private IFieldContents _contents;
  private IContainer components;
  private TableLayoutPanel tlpFieldContents;
  private Label lblCaption;
  private ComboBox cbxVarCondition;
  private TextBox tbVarCond;
  private ContextMenuStrip mnFieldContents;
  private ToolStripMenuItem miEdit;
  private ToolStripMenuItem miClear;
  private ToolStripSeparator miConditionSep1;
  private ToolStripMenuItem miCopy;
  private ToolStripMenuItem miCut;
  private ToolStripMenuItem miPaste;

  private void SetupControls()
  {
    this._isUpdating = true;
    try
    {
      this.cbxVarCondition.BindEnumToCombobox<FieldContentsType>(FieldContentsType.Formula);
    }
    finally
    {
      this._isUpdating = false;
    }
  }

  private void EnableControls()
  {
    this._isUpdating = true;
    try
    {
      this.cbxVarCondition.Enabled = !this._settings.ReadOnly;
      this.tbVarCond.Enabled = !this._settings.ReadOnly;
    }
    finally
    {
      this._isUpdating = false;
    }
  }

  private void FillControlsFromConfig()
  {
    this._isUpdating = true;
    try
    {
      if (this._settings is FieldContentsConfigViewSettings settings)
        this.lblCaption.Text = settings.Caption;
      FieldContentsType fieldContentsType = settings != null ? settings.DefaultFieldContentsType : FieldContentsType.Formula;
      ComboBox cbxVarCondition = this.cbxVarCondition;
      IFieldContents contents = this._contents;
      int num = contents != null ? (int) contents.ContentsType : (int) fieldContentsType;
      this.UpdateContentsTypeControl(cbxVarCondition, (FieldContentsType) num);
      this.UpdateContentsControl(this.tbVarCond, this._contents);
    }
    finally
    {
      this._isUpdating = false;
    }
  }

  private void UpdateContentsControl(TextBox textBox, IFieldContents condition)
  {
    textBox.Text = condition?.ToString() ?? string.Empty;
  }

  private void UpdateContentsTypeControl(ComboBox comboBox, FieldContentsType fieldContentsType)
  {
    comboBox.SetSelectedEnumValue<FieldContentsType>(fieldContentsType);
  }

  private IFieldContents SaveValuesFromControls()
  {
    return !this.tbVarCond.Enabled ? (IFieldContents) null : (this._contents is ICloneable contents ? contents.Clone() : (object) null) as IFieldContents;
  }

  private bool EditContents(
    FieldContentsType selectedContentsType,
    ref IFieldContents fieldContents)
  {
    if (fieldContents == null)
      this.SetupContents(selectedContentsType, ref fieldContents);
    return fieldContents != null && Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.SelectFieldContents.Select(ref fieldContents);
  }

  private bool SetupContents(FieldContentsType contentsType, ref IFieldContents fieldContents)
  {
    if (!this.SetupFieldContents(contentsType, ref fieldContents))
      return false;
    if (fieldContents == null || fieldContents.ContentsType != FieldContentsType.Formula)
      return true;
    DataType resType = this._settings is FieldContentsConfigViewSettings settings ? settings.DataType : DataType.Boolean;
    ((FormulaFieldContents) fieldContents).TemplateFormula = new TempFormula(resType);
    return true;
  }

  private bool SetupFieldContents(FieldContentsType contentsType, ref IFieldContents fieldContents)
  {
    if (fieldContents != null && fieldContents.ContentsType == contentsType)
      return false;
    fieldContents = FieldContentsFactory.Instance.Create(contentsType);
    return true;
  }

  private void ClearContents()
  {
    this._contents = (IFieldContents) null;
    this.UpdateContentsControl(this.tbVarCond, (IFieldContents) null);
    this.UpdateContentsTypeControl(this.cbxVarCondition, this._settings is FieldContentsConfigViewSettings settings ? settings.DefaultFieldContentsType : FieldContentsType.Formula);
  }

  private void CopyContents(IFieldContents fieldContents)
  {
    ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true)?.SetDataObject(fieldContents != null ? (fieldContents is ICloneable cloneable ? cloneable.Clone() : (object) null) : (object) null);
  }

  private bool CanPasteContents()
  {
    if (Clipboard.ContainsData(TempFormula.FormulaFormat))
      return true;
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
    return service != null && service.GetDataObject() is IFieldContents;
  }

  private void PasteContents()
  {
    if (Clipboard.ContainsData(TempFormula.FormulaFormat))
    {
      this.PasteTempFormula();
    }
    else
    {
      IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
      if (service == null || !(service.GetDataObject() is IFieldContents dataObject))
        return;
      this._contents = (dataObject is ICloneable cloneable ? cloneable.Clone() : (object) null) as IFieldContents;
      this.UpdateContentsControl(this.tbVarCond, this._contents);
      this.UpdateContentsTypeControl(this.cbxVarCondition, this._contents.ContentsType);
    }
  }

  private void PasteTempFormula()
  {
    if (!Clipboard.ContainsData(TempFormula.FormulaFormat) || !(FieldContentsFactory.Instance.Create(FieldContentsType.Formula) is FormulaFieldContents formulaFieldContents))
      return;
    formulaFieldContents.TemplateFormula = new TempFormula(true);
    ExpertUser.PasteFromClipboard(formulaFieldContents.TemplateFormula);
    formulaFieldContents.TemplateFormula.UpdateTokenBegs();
    this._contents = (IFieldContents) formulaFieldContents;
    this.UpdateContentsControl(this.tbVarCond, this._contents);
    this.UpdateContentsTypeControl(this.cbxVarCondition, this._contents.ContentsType);
  }

  private void cbxVarCondition_SelectedValueChanged(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.EnableControls();
    if (!this.SetupContents(this.cbxVarCondition.SelectedValue.ToString().ToEnum<FieldContentsType>(), ref this._contents))
      return;
    this.UpdateContentsControl(this.tbVarCond, this._contents);
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void tbVarCond_DoubleClick(object sender, EventArgs e)
  {
    if (this._isUpdating)
      return;
    this.miEdit.PerformClick();
  }

  private void mnFieldContents_Opening(object sender, CancelEventArgs e)
  {
    this.miEdit.Enabled = this.miClear.Enabled = !this._settings.ReadOnly && !this._isUpdating;
    bool flag = this._contents != null && !this._isUpdating;
    this.miCopy.Enabled = flag;
    this.miCut.Enabled = !this._settings.ReadOnly & flag;
    this.miPaste.Enabled = !this._settings.ReadOnly && this.CanPasteContents() && !this._isUpdating;
  }

  private void miEdit_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly || !this.EditContents(this.cbxVarCondition.SelectedValue.ToString().ToEnum<FieldContentsType>(), ref this._contents))
      return;
    this.UpdateContentsControl(this.tbVarCond, this._contents);
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void miClear_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly)
      return;
    this.ClearContents();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void miCopy_Click(object sender, EventArgs e) => this.CopyContents(this._contents);

  private void miCut_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly)
    {
      this.miCopy.PerformClick();
    }
    else
    {
      this.ClearContents();
      Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
      if (onDataChanged == null)
        return;
      onDataChanged(this._controller, true);
    }
  }

  private void miPaste_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly)
      return;
    this.PasteContents();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  public bool ApplyChanges(out IDocumentConfigElement config)
  {
    config = (IDocumentConfigElement) null;
    if (this._settings.ReadOnly)
      return false;
    config = this.SaveValuesFromControls() as IDocumentConfigElement;
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
    this._contents = settings.ConfigElement?.Clone() as IFieldContents;
    this.FillControlsFromConfig();
    this.EnableControls();
  }

  public FieldContentsConfigView([NotNull] IConfigViewController controller, System.IServiceProvider services)
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.tlpFieldContents = new TableLayoutPanel();
    this.tbVarCond = new TextBox();
    this.mnFieldContents = new ContextMenuStrip(this.components);
    this.miEdit = new ToolStripMenuItem();
    this.miClear = new ToolStripMenuItem();
    this.miConditionSep1 = new ToolStripSeparator();
    this.miCopy = new ToolStripMenuItem();
    this.miCut = new ToolStripMenuItem();
    this.miPaste = new ToolStripMenuItem();
    this.cbxVarCondition = new ComboBox();
    this.lblCaption = new Label();
    this.tlpFieldContents.SuspendLayout();
    this.mnFieldContents.SuspendLayout();
    this.SuspendLayout();
    this.tlpFieldContents.ColumnCount = 1;
    this.tlpFieldContents.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpFieldContents.Controls.Add((Control) this.tbVarCond, 0, 2);
    this.tlpFieldContents.Controls.Add((Control) this.cbxVarCondition, 0, 1);
    this.tlpFieldContents.Controls.Add((Control) this.lblCaption, 0, 0);
    this.tlpFieldContents.Dock = DockStyle.Fill;
    this.tlpFieldContents.Location = new Point(0, 0);
    this.tlpFieldContents.Margin = new Padding(0);
    this.tlpFieldContents.Name = "tlpFieldContents";
    this.tlpFieldContents.RowCount = 3;
    this.tlpFieldContents.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpFieldContents.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
    this.tlpFieldContents.RowStyles.Add(new RowStyle(SizeType.Absolute, 70f));
    this.tlpFieldContents.Size = new Size(545, 151);
    this.tlpFieldContents.TabIndex = 0;
    this.tbVarCond.ContextMenuStrip = this.mnFieldContents;
    this.tbVarCond.Dock = DockStyle.Fill;
    this.tbVarCond.Location = new Point(0, 45);
    this.tbVarCond.Margin = new Padding(0);
    this.tbVarCond.Multiline = true;
    this.tbVarCond.Name = "tbVarCond";
    this.tbVarCond.ReadOnly = true;
    this.tbVarCond.Size = new Size(545, 106);
    this.tbVarCond.TabIndex = 126;
    this.tbVarCond.DoubleClick += new EventHandler(this.tbVarCond_DoubleClick);
    this.mnFieldContents.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.miEdit,
      (ToolStripItem) this.miClear,
      (ToolStripItem) this.miConditionSep1,
      (ToolStripItem) this.miCopy,
      (ToolStripItem) this.miCut,
      (ToolStripItem) this.miPaste
    });
    this.mnFieldContents.Name = "contextMenuStrip1";
    this.mnFieldContents.Size = new Size(181, 142);
    this.mnFieldContents.Opening += new CancelEventHandler(this.mnFieldContents_Opening);
    this.miEdit.Name = "miEdit";
    this.miEdit.Size = new Size(180, 22);
    this.miEdit.Text = "Редактировать";
    this.miEdit.Click += new EventHandler(this.miEdit_Click);
    this.miClear.Name = "miClear";
    this.miClear.Size = new Size(180, 22);
    this.miClear.Text = "Удалить";
    this.miClear.Click += new EventHandler(this.miClear_Click);
    this.miConditionSep1.Name = "miConditionSep1";
    this.miConditionSep1.Size = new Size(151, 6);
    this.miCopy.Name = "miCopy";
    this.miCopy.Size = new Size(180, 22);
    this.miCopy.Text = "Копировать";
    this.miCopy.Click += new EventHandler(this.miCopy_Click);
    this.miCut.Name = "miCut";
    this.miCut.Size = new Size(180, 22);
    this.miCut.Text = "Вырезать";
    this.miCut.Click += new EventHandler(this.miCut_Click);
    this.miPaste.Name = "miPaste";
    this.miPaste.Size = new Size(180, 22);
    this.miPaste.Text = "Вставить";
    this.miPaste.Click += new EventHandler(this.miPaste_Click);
    this.cbxVarCondition.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbxVarCondition.FormattingEnabled = true;
    this.cbxVarCondition.Location = new Point(0, 20);
    this.cbxVarCondition.Margin = new Padding(0);
    this.cbxVarCondition.Name = "cbxVarCondition";
    this.cbxVarCondition.Size = new Size(220, 21);
    this.cbxVarCondition.TabIndex = 125;
    this.cbxVarCondition.SelectedValueChanged += new EventHandler(this.cbxVarCondition_SelectedValueChanged);
    this.lblCaption.AutoSize = true;
    this.lblCaption.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lblCaption.Location = new Point(0, 0);
    this.lblCaption.Margin = new Padding(0);
    this.lblCaption.Name = "lblCaption";
    this.lblCaption.Size = new Size(62, 13);
    this.lblCaption.TabIndex = 122;
    this.lblCaption.Text = "Условие:";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpFieldContents);
    this.Name = nameof (FieldContentsConfigView);
    this.Size = new Size(545, 151);
    this.tlpFieldContents.ResumeLayout(false);
    this.tlpFieldContents.PerformLayout();
    this.mnFieldContents.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
