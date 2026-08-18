// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecifNumberingControlFull
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Панель настройки схемы нумерации позиций (полная) </summary>
public class SpecifNumberingControlFull : ExtUserControl
{
  public Button BtnObjTypes;
  public Button BtnRazdels;
  private SpecifNumberingControl specifNumberingControl;
  private ToolTipController EditModeToolTip;
  private ToolTipController ReadModeToolTip;
  private Button SameDesiognationSetupButton;
  public Button BtnReset;
  private IContainer components;
  private SpecifNumberingFull _SpecifNumberingFull;
  private long _specificationTemplateObjectId = -1;

  public SpecifNumberingControlFull() => this.InitializeComponent();

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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.BtnObjTypes = new Button();
    this.BtnRazdels = new Button();
    this.specifNumberingControl = new SpecifNumberingControl();
    this.EditModeToolTip = new ToolTipController(this.components);
    this.SameDesiognationSetupButton = new Button();
    this.BtnReset = new Button();
    this.ReadModeToolTip = new ToolTipController(this.components);
    this.SuspendLayout();
    this.BtnObjTypes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.BtnObjTypes.FlatStyle = FlatStyle.System;
    this.BtnObjTypes.Location = new Point(138, 213);
    this.BtnObjTypes.Name = "BtnObjTypes";
    this.BtnObjTypes.Size = new Size(174, 27);
    this.BtnObjTypes.TabIndex = 0;
    this.BtnObjTypes.Text = "Нумерация для разделов...";
    this.EditModeToolTip.SetToolTip((Control) this.BtnObjTypes, "Определить специальные правила нумерации различных типов изделий");
    this.ReadModeToolTip.SetToolTip((Control) this.BtnObjTypes, "Просмотреть специальные правила нумерации различных типов изделий");
    this.BtnObjTypes.Click += new EventHandler(this.BtnObjTypes_Click);
    this.BtnRazdels.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.BtnRazdels.FlatStyle = FlatStyle.System;
    this.BtnRazdels.Location = new Point(314, 213);
    this.BtnRazdels.Name = "BtnRazdels";
    this.BtnRazdels.Size = new Size(141, 27);
    this.BtnRazdels.TabIndex = 1;
    this.BtnRazdels.Text = "Исключить разделы...";
    this.EditModeToolTip.SetToolTip((Control) this.BtnRazdels, "Редактировать список разделов спецификации, которые не должны нумероваться");
    this.ReadModeToolTip.SetToolTip((Control) this.BtnRazdels, "Просмотреть список разделов спецификации, которые не должны нумероваться");
    this.BtnRazdels.Click += new EventHandler(this.BtnRazdels_Click);
    this.specifNumberingControl.AutoScroll = true;
    this.specifNumberingControl.BackColor = SystemColors.Control;
    this.specifNumberingControl.Dock = DockStyle.Top;
    this.specifNumberingControl.Location = new Point(0, 0);
    this.specifNumberingControl.MinimumSize = new Size(513, 179);
    this.specifNumberingControl.Name = "specifNumberingControl";
    this.specifNumberingControl.Size = new Size(648, 207);
    this.specifNumberingControl.TabIndex = 0;
    this.EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this.SameDesiognationSetupButton.FlatStyle = FlatStyle.System;
    this.SameDesiognationSetupButton.Location = new Point(510, 73);
    this.SameDesiognationSetupButton.Name = "SameDesiognationSetupButton";
    this.SameDesiognationSetupButton.Size = new Size(121, 27);
    this.SameDesiognationSetupButton.TabIndex = 1;
    this.SameDesiognationSetupButton.Text = "Сходство...";
    this.EditModeToolTip.SetToolTip((Control) this.SameDesiognationSetupButton, "Определить критерии \"похожести\" обозначений");
    this.ReadModeToolTip.SetToolTip((Control) this.SameDesiognationSetupButton, "Просмотреть критерии \"похожести\" обозначений");
    this.SameDesiognationSetupButton.Click += new EventHandler(this.SameDesiognationSetupButton_Click);
    this.BtnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.BtnReset.Enabled = false;
    this.BtnReset.FlatStyle = FlatStyle.System;
    this.BtnReset.Location = new Point(15, 213);
    this.BtnReset.Name = "BtnReset";
    this.BtnReset.Size = new Size(121, 27);
    this.BtnReset.TabIndex = 4;
    this.BtnReset.Text = "По умолчанию";
    this.EditModeToolTip.SetToolTip((Control) this.BtnReset, "Вернуть настройки к значениям по умолчанию");
    this.ReadModeToolTip.SetToolTip((Control) this.BtnReset, "Вернуть настройки к значениям по умолчанию");
    this.BtnReset.Click += new EventHandler(this.BtnReset_Click);
    this.ReadModeToolTip.Active = false;
    this.ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.BtnRazdels);
    this.Controls.Add((Control) this.BtnObjTypes);
    this.Controls.Add((Control) this.BtnReset);
    this.Controls.Add((Control) this.SameDesiognationSetupButton);
    this.Controls.Add((Control) this.specifNumberingControl);
    this.Name = nameof (SpecifNumberingControlFull);
    this.Size = new Size(648, 240 /*0xF0*/);
    this.ResumeLayout(false);
  }

  /// <summary> Идентификатор шаблона спецификации </summary>
  public long SpecificationTemplateObjectId
  {
    get => this._specificationTemplateObjectId;
    set => this._specificationTemplateObjectId = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SpecifNumberingFull SpecifNumberingFull
  {
    get => this._SpecifNumberingFull;
    set
    {
      this.LockControls();
      try
      {
        this._SpecifNumberingFull = value;
        this.specifNumberingControl.SpecifNumbering = (SpecifNumbering) value;
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this._SpecifNumberingFull);
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
    this.BtnObjTypes.Enabled = this._SpecifNumberingFull != null;
    this.BtnRazdels.Enabled = this._SpecifNumberingFull != null;
    this.SameDesiognationSetupButton.Enabled = this._SpecifNumberingFull != null;
    this.BtnReset.Enabled = !this.ReadOnly;
    if (this.EditModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this.EditModeToolTip.Active)
        {
          this.EditModeToolTip.Active = false;
          this.ReadModeToolTip.Active = true;
        }
      }
      else if (this.ReadModeToolTip.Active)
      {
        this.ReadModeToolTip.Active = false;
        this.EditModeToolTip.Active = true;
      }
    }
    if (this._SpecifNumberingFull == null)
      return;
    bool flag1 = this._SpecifNumberingFull.SpecifRazdelNumbering.Changed && this._SpecifNumberingFull.ParentLevel != null;
    if (this.BtnObjTypes.Font.Bold && !flag1 || !this.BtnObjTypes.Font.Bold & flag1)
      this.BtnObjTypes.Font = new Font(this.BtnObjTypes.Font.FontFamily, this.BtnObjTypes.Font.SizeInPoints, flag1 ? FontStyle.Bold : FontStyle.Regular, this.BtnObjTypes.Font.Unit, this.BtnObjTypes.Font.GdiCharSet, this.BtnObjTypes.Font.GdiVerticalFont);
    bool flag2 = this._SpecifNumberingFull.CompareDesignationSchema.Changed && this._SpecifNumberingFull.ParentLevel != null;
    if (this.SameDesiognationSetupButton.Font.Bold && !flag2 || !this.SameDesiognationSetupButton.Font.Bold & flag2)
      this.SameDesiognationSetupButton.Font = new Font(this.BtnObjTypes.Font.FontFamily, this.BtnObjTypes.Font.SizeInPoints, flag2 ? FontStyle.Bold : FontStyle.Regular, this.BtnObjTypes.Font.Unit, this.BtnObjTypes.Font.GdiCharSet, this.BtnObjTypes.Font.GdiVerticalFont);
    bool flag3 = this._SpecifNumberingFull.NonNumneringRazdelsChanged && this._SpecifNumberingFull.ParentLevel != null;
    if ((!this.BtnRazdels.Font.Bold || flag3) && !(!this.BtnRazdels.Font.Bold & flag3))
      return;
    this.BtnRazdels.Font = new Font(this.BtnObjTypes.Font.FontFamily, this.BtnObjTypes.Font.SizeInPoints, flag3 ? FontStyle.Bold : FontStyle.Regular, this.BtnObjTypes.Font.Unit, this.BtnObjTypes.Font.GdiCharSet, this.BtnObjTypes.Font.GdiVerticalFont);
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this._SpecifNumberingFull == null || this._SpecifNumberingFull.ReadOnly || !this.Enabled;
  }

  /// <summary> Сбросить настройки к значениям по умолчанию </summary>
  public void Clear()
  {
    if (this.ReadOnly || this._SpecifNumberingFull == null)
      return;
    this.LockControls();
    try
    {
      this._SpecifNumberingFull.Clear();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>Нажата кнопка "по умолчанию"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BtnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated || MessageBox.Show("Сбросить изменения в схеме нумерации позиций к значениям по умолчанию?", "Схема нумерации позиций", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      this._SpecifNumberingFull.Clear();
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Нажата кнопка "Специальные настройки для разделов спецификации" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BtnObjTypes_Click(object sender, EventArgs e)
  {
    NumberingForSpecifRazdelsForm specifRazdelsForm = new NumberingForSpecifRazdelsForm((Control) this, this.SpecifNumberingFull.SpecifRazdelNumbering, (IStructualControlSupport) this);
    int num = (int) specifRazdelsForm.ShowDialog();
    specifRazdelsForm.Dispose();
    this.UpdateControls(true);
  }

  /// <summary> Нажата кнопка "Выбрать ненумеруемые разделы спецификации" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BtnRazdels_Click(object sender, EventArgs e)
  {
    ExcludedRazdels excludedRazdels = new ExcludedRazdels((Control) this, this.SpecifNumberingFull, (IStructualControlSupport) this);
    int num = (int) excludedRazdels.ShowDialog();
    excludedRazdels.Dispose();
    this.UpdateControls(true);
  }

  /// <summary> Нажата кнопка "Настроить правила определения похожих обозначений" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SameDesiognationSetupButton_Click(object sender, EventArgs e)
  {
    SameDesignationsSetupForm designationsSetupForm = new SameDesignationsSetupForm((Control) this, this.SpecifNumberingFull.CompareDesignationSchema, (IStructualControlSupport) this);
    int num = (int) designationsSetupForm.ShowDialog();
    designationsSetupForm.Dispose();
    this.UpdateControls(true);
  }
}
