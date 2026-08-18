// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UserControlDesignationTrim
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса UserControlSetupSkipPositions </summary>
public class UserControlDesignationTrim : ExtUserControl
{
  private IContainer components;
  private ToolTipController _editModeToolTip;
  public Button _btnReset;
  private GroupBox gbDesignation;
  private RadioButton rbDifferent;
  private RadioButton rbSame;
  protected Label lLength;
  private CheckBox cbDocs;
  private SpinEdit seLength;
  private Label lSampleSame;
  private Label lSampleOutput;
  private Label lHint;
  private Label label1;
  private CheckBox cbUseSameDesignationForProducts;
  private CheckBox cbuseGroupNumberAttribute;
  private ToolTipController _readModeToolTip;
  public DesignationTrimSchema designationTrimSchema;

  public UserControlDesignationTrim()
  {
    this.InitializeComponent();
    this.Init();
  }

  /// <summary> Инциализация формы </summary>
  protected void Init()
  {
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlDesignationTrim));
    this._editModeToolTip = new ToolTipController(this.components);
    this.rbDifferent = new RadioButton();
    this.rbSame = new RadioButton();
    this.cbDocs = new CheckBox();
    this.cbUseSameDesignationForProducts = new CheckBox();
    this.cbuseGroupNumberAttribute = new CheckBox();
    this._readModeToolTip = new ToolTipController(this.components);
    this._btnReset = new Button();
    this.gbDesignation = new GroupBox();
    this.lHint = new Label();
    this.lSampleOutput = new Label();
    this.label1 = new Label();
    this.seLength = new SpinEdit();
    this.lSampleSame = new Label();
    this.lLength = new Label();
    this.gbDesignation.SuspendLayout();
    this.seLength.Properties.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this.rbDifferent.AutoSize = true;
    this.rbDifferent.Location = new Point(21, (int) sbyte.MaxValue);
    this.rbDifferent.Name = "rbDifferent";
    this.rbDifferent.Size = new Size(195, 17);
    this.rbDifferent.TabIndex = 1;
    this.rbDifferent.TabStop = true;
    this.rbDifferent.Text = "Разные обозначения исполнений";
    this._editModeToolTip.SetToolTip((Control) this.rbDifferent, "Обозначения исполнений выводятся в записях полностью.");
    this._readModeToolTip.SetToolTip((Control) this.rbDifferent, "Обозначения исполнений выводятся в записях полностью.");
    this.rbDifferent.UseVisualStyleBackColor = true;
    this.rbDifferent.CheckedChanged += new EventHandler(this.rbCheckedChanged);
    this.rbSame.AutoSize = true;
    this.rbSame.Location = new Point(21, 104);
    this.rbSame.Name = "rbSame";
    this.rbSame.Size = new Size(220, 17);
    this.rbSame.TabIndex = 0;
    this.rbSame.TabStop = true;
    this.rbSame.Text = "Одинаковые обозначения исполнений";
    this._editModeToolTip.SetToolTip((Control) this.rbSame, componentResourceManager.GetString("rbSame.ToolTip"));
    this._readModeToolTip.SetToolTip((Control) this.rbSame, componentResourceManager.GetString("rbSame.ToolTip1"));
    this.rbSame.UseVisualStyleBackColor = true;
    this.rbSame.CheckedChanged += new EventHandler(this.rbCheckedChanged);
    this.cbDocs.AutoSize = true;
    this.cbDocs.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.cbDocs.Location = new Point(21, 305);
    this.cbDocs.Name = "cbDocs";
    this.cbDocs.Size = new Size(275, 17);
    this.cbDocs.TabIndex = 4;
    this.cbDocs.Text = "Использовать также в разделе \"Документация\"";
    this._editModeToolTip.SetToolTip((Control) this.cbDocs, "Применять данные настройки для записей в разделе \"Документация\"");
    this._readModeToolTip.SetToolTip((Control) this.cbDocs, "Применять данные настройки для записей в разделе \"Документация\"");
    this.cbDocs.UseVisualStyleBackColor = true;
    this.cbDocs.CheckedChanged += new EventHandler(this.cbDocs_CheckedChanged);
    this.cbUseSameDesignationForProducts.AutoSize = true;
    this.cbUseSameDesignationForProducts.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.cbUseSameDesignationForProducts.Location = new Point(13, 371);
    this.cbUseSameDesignationForProducts.Name = "cbUseSameDesignationForProducts";
    this.cbUseSameDesignationForProducts.Size = new Size(441, 17);
    this.cbUseSameDesignationForProducts.TabIndex = 4;
    this.cbUseSameDesignationForProducts.Text = "Использовать одинаковые обозначения исполнений специфицируемого изделия";
    this._editModeToolTip.SetToolTip((Control) this.cbUseSameDesignationForProducts, "Использовать одинаковые обозначения исполнений для изделий на которые выпущена спецификация при создании новых исполнений и формировании номера исполнения в графе \"Количество\"");
    this._readModeToolTip.SetToolTip((Control) this.cbUseSameDesignationForProducts, "Использовать одинаковые обозначения исполнений для изделий на которые выпущена спецификация при создании новых исполнений и формировании номера исполнения в графе \"Количество\"");
    this.cbUseSameDesignationForProducts.UseVisualStyleBackColor = true;
    this.cbUseSameDesignationForProducts.CheckedChanged += new EventHandler(this.cbUseSameDesignationForProducts_CheckedChanged);
    this.cbuseGroupNumberAttribute.AutoSize = true;
    this.cbuseGroupNumberAttribute.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.cbuseGroupNumberAttribute.Location = new Point(21, 328);
    this.cbuseGroupNumberAttribute.Name = "cbuseGroupNumberAttribute";
    this.cbuseGroupNumberAttribute.Size = new Size(339, 17);
    this.cbuseGroupNumberAttribute.TabIndex = 7;
    this.cbuseGroupNumberAttribute.Text = "Использовать атрибут \"Идентификатор группового изделия\"";
    this._editModeToolTip.SetToolTip((Control) this.cbuseGroupNumberAttribute, "Использовать атрибут \"Идентификатор группового изделия\" для определения одинаковые ли исполнения");
    this._readModeToolTip.SetToolTip((Control) this.cbuseGroupNumberAttribute, "Использовать атрибут \"Идентификатор группового изделия\" для определения одинаковые ли исполнения");
    this.cbuseGroupNumberAttribute.UseVisualStyleBackColor = true;
    this.cbuseGroupNumberAttribute.CheckedChanged += new EventHandler(this.cbuseGroupNumberAttribute_CheckedChanged);
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this._btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnReset.Enabled = false;
    this._btnReset.FlatStyle = FlatStyle.System;
    this._btnReset.Location = new Point(13, 394);
    this._btnReset.Name = "_btnReset";
    this._btnReset.Size = new Size(121, 27);
    this._btnReset.TabIndex = 18;
    this._btnReset.Text = "По умолчанию";
    this._btnReset.Click += new EventHandler(this._btnReset_Click);
    this.gbDesignation.Controls.Add((Control) this.cbuseGroupNumberAttribute);
    this.gbDesignation.Controls.Add((Control) this.cbDocs);
    this.gbDesignation.Controls.Add((Control) this.lHint);
    this.gbDesignation.Controls.Add((Control) this.lSampleOutput);
    this.gbDesignation.Controls.Add((Control) this.label1);
    this.gbDesignation.Controls.Add((Control) this.seLength);
    this.gbDesignation.Controls.Add((Control) this.lSampleSame);
    this.gbDesignation.Controls.Add((Control) this.rbDifferent);
    this.gbDesignation.Controls.Add((Control) this.lLength);
    this.gbDesignation.Controls.Add((Control) this.rbSame);
    this.gbDesignation.Location = new Point(13, 7);
    this.gbDesignation.Name = "gbDesignation";
    this.gbDesignation.Size = new Size(554, 358);
    this.gbDesignation.TabIndex = 19;
    this.gbDesignation.TabStop = false;
    this.gbDesignation.Text = "Обозначения исполнений в записях";
    this.lHint.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lHint.Location = new Point(18, 258);
    this.lHint.Name = "lHint";
    this.lHint.Size = new Size(520, 44);
    this.lHint.TabIndex = 2;
    this.lHint.Text = "После указанного количества символов будет производиться поиск знака '-' перед номером исполнения, например XXXXXX-XX.";
    this.lSampleOutput.AutoSize = true;
    this.lSampleOutput.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lSampleOutput.Location = new Point(373, 157);
    this.lSampleOutput.Name = "lSampleOutput";
    this.lSampleOutput.Size = new Size(161, 56);
    this.lSampleOutput.TabIndex = 2;
    this.lSampleOutput.Text = "Вывод в спецификации: \r\n  ИНТМ.123456.001 \r\n                 -01 \r\n                 -02";
    this.label1.Location = new Point(18, 26);
    this.label1.Name = "label1";
    this.label1.Size = new Size(516, 75);
    this.label1.TabIndex = 2;
    this.label1.Text = componentResourceManager.GetString("label1.Text");
    this.seLength.EditValue = (object) 1;
    this.seLength.Location = new Point(293, 234);
    this.seLength.Name = "seLength";
    this.seLength.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.seLength.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.seLength.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.seLength.Properties.IsFloatValue = false;
    this.seLength.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.seLength.Properties.UseCtrlIncrement = false;
    this.seLength.Properties.ValidateOnEnterKey = true;
    this.seLength.Size = new Size(45, 20);
    this.seLength.TabIndex = 6;
    this.seLength.ToolTip = "Количество символов в базовом обозначении, в которых игнорируется знак '-'";
    this.seLength.EditValueChanged += new EventHandler(this.seLength_EditValueChanged);
    this.seLength.EditValueChanging += new ChangingEventHandler(this.seLength_EditValueChanging);
    this.lSampleSame.AutoSize = true;
    this.lSampleSame.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lSampleSame.Location = new Point(18, 147);
    this.lSampleSame.Name = "lSampleSame";
    this.lSampleSame.Size = new Size(154, 56);
    this.lSampleSame.TabIndex = 2;
    this.lSampleSame.Text = "Пример: \r\n  ИНТМ.123456.001 \r\n  ИНТМ.123456.001-01 \r\n  ИНТМ.123456.001-02";
    this.lLength.Location = new Point(18, 233);
    this.lLength.Name = "lLength";
    this.lLength.Size = new Size(273, 19);
    this.lLength.TabIndex = 3;
    this.lLength.Text = "Количество символов в базовом обозначении: ";
    this.lLength.TextAlign = ContentAlignment.MiddleLeft;
    this.Controls.Add((Control) this.cbUseSameDesignationForProducts);
    this.Controls.Add((Control) this._btnReset);
    this.Controls.Add((Control) this.gbDesignation);
    this.Name = nameof (UserControlDesignationTrim);
    this.Size = new Size(580, 420);
    this.gbDesignation.ResumeLayout(false);
    this.gbDesignation.PerformLayout();
    this.seLength.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary> Схема пропуска строк </summary>
  public DesignationTrimSchema DesignationTrimSchema
  {
    get => this.designationTrimSchema;
    set
    {
      this.LockControls();
      try
      {
        this.designationTrimSchema = value;
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this.designationTrimSchema);
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    if (this._editModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._editModeToolTip.Active)
        {
          this._editModeToolTip.Active = false;
          this._readModeToolTip.Active = true;
        }
      }
      else if (this._readModeToolTip.Active)
      {
        this._readModeToolTip.Active = false;
        this._editModeToolTip.Active = true;
      }
    }
    this.rbSame.Enabled = !this.ReadOnly;
    this.rbDifferent.Enabled = !this.ReadOnly;
    this.seLength.Value = 5M;
    this.seLength.Properties.ReadOnly = this.ReadOnly;
    this.cbDocs.Enabled = !this.ReadOnly;
    this.cbUseSameDesignationForProducts.Enabled = !this.ReadOnly;
    this.cbuseGroupNumberAttribute.Enabled = !this.ReadOnly;
    this.seLength.Properties.Buttons[0].Visible = !this.ReadOnly;
    if (this.designationTrimSchema == null)
    {
      this.seLength.Text = string.Empty;
      this.rbDifferent.Checked = false;
      this.rbSame.Checked = false;
      this.cbDocs.CheckState = CheckState.Indeterminate;
      this.cbuseGroupNumberAttribute.CheckState = CheckState.Indeterminate;
      this.cbUseSameDesignationForProducts.CheckState = CheckState.Indeterminate;
    }
    else
    {
      this.seLength.Value = (Decimal) this.designationTrimSchema.LengthBasePart;
      this.rbDifferent.Checked = !this.designationTrimSchema.UseSameProductDesignationsInRows;
      this.rbSame.Checked = this.designationTrimSchema.UseSameProductDesignationsInRows;
      this.cbDocs.Checked = this.designationTrimSchema.UseInDocumentation;
      this.cbuseGroupNumberAttribute.Checked = this.designationTrimSchema.UseGroupNumberAttribute;
      this.cbUseSameDesignationForProducts.Checked = this.designationTrimSchema.UseSameDesignationForProducts;
    }
    this.RefreshBoldUpDown((Control) null);
    this._btnReset.Enabled = !this.ReadOnly;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this.designationTrimSchema == null || this.designationTrimSchema.ReadOnly;
  }

  /// <summary>Обновление параметра Bold у шрифта NumericUpDown</summary>
  /// <param name="numericUpDown">NumericUpDown, у которого надо обновить Bold. Если = null, то обновляется у всех</param>
  public void RefreshBoldUpDown(Control control)
  {
    if (this.designationTrimSchema == null)
      return;
    if (control == null || this.rbSame == control || this.rbDifferent == control)
    {
      this.ChangeUpDownFontBold((Control) this.rbSame, this.designationTrimSchema.SameDesignationChanged);
      this.ChangeUpDownFontBold((Control) this.rbDifferent, this.designationTrimSchema.SameDesignationChanged);
    }
    if (control == null || this.cbDocs == control)
      this.ChangeUpDownFontBold((Control) this.cbDocs, this.designationTrimSchema.UseInDocumentationChanged);
    if (control == null || this.cbuseGroupNumberAttribute == control)
      this.ChangeUpDownFontBold((Control) this.cbuseGroupNumberAttribute, this.designationTrimSchema.UseGroupNumberAttributeChanged);
    if (control == null || this.cbUseSameDesignationForProducts == control)
      this.ChangeUpDownFontBold((Control) this.cbUseSameDesignationForProducts, this.designationTrimSchema.UseSameDesignationForProductsChanged);
    if (control != null && this.seLength != control)
      return;
    this.ChangeUpDownFontBold((Control) this.seLength, this.designationTrimSchema.LengthBasePartChanged);
  }

  private void ChangeUpDownFontBold(Control control, bool mustBeBold)
  {
    if (control.Font.Bold == mustBeBold)
      return;
    control.Font = new Font(control.Font.FontFamily, control.Font.SizeInPoints, mustBeBold ? FontStyle.Bold : FontStyle.Regular, control.Font.Unit, control.Font.GdiCharSet, control.Font.GdiVerticalFont);
  }

  private void BeforeChangeUpDown(SpinEdit spinEdit, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this.designationTrimSchema == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(spinEdit.Value);
  }

  private bool BeforeUpDownEdit()
  {
    if (this.designationTrimSchema == null || this.ControlsAreUpdating)
      return false;
    bool wasUpdated = false;
    return this.CheckCanEdit(ref wasUpdated);
  }

  private void AfterUpDownEdit() => this.Changed = true;

  internal void _btnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || MessageBox.Show("Сбросить изменения в настройках?", "Обозначения исполнений", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      this.designationTrimSchema.LoadDefaultParams();
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void rbCheckedChanged(object sender, EventArgs e)
  {
    if (this.DesignationTrimSchema != null && this.DesignationTrimSchema.UseSameProductDesignationsInRows != this.rbSame.Checked)
    {
      if (this.BeforeUpDownEdit())
      {
        this.DesignationTrimSchema.UseSameProductDesignationsInRows = this.rbSame.Checked;
        this.AfterUpDownEdit();
        this.RefreshBoldUpDown((Control) this.rbSame);
        this.RefreshBoldUpDown((Control) this.rbDifferent);
      }
      else
        this.rbSame.Checked = !this.rbSame.Checked;
    }
    string str = "";
    if (this.rbSame.Checked)
    {
      str = "Например:  \r\n   ИНТМ.123456.001 \r\n   ИНТМ.123456.001-01 \r\n   ИНТМ.123456.001-02";
      this.seLength.Visible = true;
      this.cbDocs.Visible = true;
      this.lLength.Visible = true;
      this.lHint.Text = "После указанного количества символов будет производиться поиск знака '-' перед номером исполнения, например XXXXXX-XX.";
      this.lSampleOutput.Text = "Вывод в спецификации: \r\n   ИНТМ.123456.001 \r\n                  -01 \r\n                  -02";
    }
    if (this.rbDifferent.Checked)
    {
      str = "Например: \r\n   20-356-67-945 \r\n   20-657-67-945 \r\n   20-768-67-945";
      this.seLength.Visible = false;
      this.cbDocs.Visible = false;
      this.lLength.Visible = false;
      this.lHint.Text = "Все обозначения \nвыводятся полностью.";
      this.lSampleOutput.Text = "Вывод в спецификации: \r\n   20-356-67-945 \r\n   20-657-67-945 \r\n   20-768-67-945";
    }
    if (!this.rbDifferent.Checked && !this.rbSame.Checked)
    {
      this.lSampleOutput.Text = "";
      this.lHint.Text = "";
    }
    this.lSampleSame.Text = str;
  }

  private void seLength_EditValueChanging(object sender, ChangingEventArgs e)
  {
    this.BeforeChangeUpDown(this.seLength, e);
  }

  private void cbDocs_CheckedChanged(object sender, EventArgs e)
  {
    if (this.DesignationTrimSchema == null || this.DesignationTrimSchema.UseInDocumentation == this.cbDocs.Checked)
      return;
    if (this.BeforeUpDownEdit())
    {
      this.DesignationTrimSchema.UseInDocumentation = this.cbDocs.Checked;
      this.AfterUpDownEdit();
      this.RefreshBoldUpDown((Control) this.cbDocs);
    }
    else
      this.cbDocs.Checked = this.DesignationTrimSchema.UseInDocumentation;
  }

  private void cbUseSameDesignationForProducts_CheckedChanged(object sender, EventArgs e)
  {
    if (this.DesignationTrimSchema == null || this.DesignationTrimSchema.UseSameDesignationForProducts == this.cbUseSameDesignationForProducts.Checked)
      return;
    if (this.BeforeUpDownEdit())
    {
      this.DesignationTrimSchema.UseSameDesignationForProducts = this.cbUseSameDesignationForProducts.Checked;
      this.AfterUpDownEdit();
      this.RefreshBoldUpDown((Control) this.cbUseSameDesignationForProducts);
    }
    else
      this.cbUseSameDesignationForProducts.Checked = this.DesignationTrimSchema.UseSameDesignationForProducts;
  }

  private void seLength_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this.DesignationTrimSchema.LengthBasePart = (int) this.seLength.Value;
    this.AfterUpDownEdit();
    this.RefreshBoldUpDown((Control) this.seLength);
  }

  private void cbuseGroupNumberAttribute_CheckedChanged(object sender, EventArgs e)
  {
    if (this.DesignationTrimSchema == null || this.DesignationTrimSchema.UseGroupNumberAttribute == this.cbuseGroupNumberAttribute.Checked)
      return;
    if (this.BeforeUpDownEdit())
    {
      this.DesignationTrimSchema.UseGroupNumberAttribute = this.cbuseGroupNumberAttribute.Checked;
      this.AfterUpDownEdit();
      this.RefreshBoldUpDown((Control) this.cbuseGroupNumberAttribute);
    }
    else
      this.cbuseGroupNumberAttribute.Checked = this.DesignationTrimSchema.UseGroupNumberAttribute;
  }
}
