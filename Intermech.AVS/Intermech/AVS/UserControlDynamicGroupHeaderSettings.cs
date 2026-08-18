// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UserControlDynamicGroupHeaderSettings
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.AVS.Common_Dialogs;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class UserControlDynamicGroupHeaderSettings : ExtUserControl
{
  private IContainer components;
  private ToolTipController _editModeToolTip;
  protected Label _label1;
  private SpinEdit editMinRowsForDynamicHeaderGroup;
  public Button btnReset;
  private GroupBox groupBox1;
  public Button btnReplaceDictionarySettings;
  protected Label label3;
  protected Label label2;
  protected Label label1;
  public Button btGroupCaptionSettings;
  protected Label lbGroupCaptionExample;
  private ToolTipController _readModeToolTip;
  public DynamicGroupHeaderSettings _dynamicGroupHeaderSettings;
  public DynamicGroupHeaderSettings _rootDynamicGroupHeaderSettings;

  public UserControlDynamicGroupHeaderSettings()
  {
    this.InitializeComponent();
    this.Init();
  }

  /// <summary> Инициализация формы </summary>
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

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модифицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlDynamicGroupHeaderSettings));
    this._editModeToolTip = new ToolTipController(this.components);
    this._readModeToolTip = new ToolTipController(this.components);
    this.groupBox1 = new GroupBox();
    this.btGroupCaptionSettings = new Button();
    this.lbGroupCaptionExample = new Label();
    this.btnReplaceDictionarySettings = new Button();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this._label1 = new Label();
    this.editMinRowsForDynamicHeaderGroup = new SpinEdit();
    this.btnReset = new Button();
    this.groupBox1.SuspendLayout();
    this.editMinRowsForDynamicHeaderGroup.Properties.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.btGroupCaptionSettings);
    this.groupBox1.Controls.Add((Control) this.lbGroupCaptionExample);
    this.groupBox1.Controls.Add((Control) this.btnReplaceDictionarySettings);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Controls.Add((Control) this._label1);
    this.groupBox1.Controls.Add((Control) this.editMinRowsForDynamicHeaderGroup);
    this.groupBox1.Location = new Point(13, 15);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(590, 277);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Группировка под общим заголовком";
    this.btGroupCaptionSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btGroupCaptionSettings.FlatStyle = FlatStyle.System;
    this.btGroupCaptionSettings.Location = new Point(554, 130);
    this.btGroupCaptionSettings.Name = "btGroupCaptionSettings";
    this.btGroupCaptionSettings.Size = new Size(22, 22);
    this.btGroupCaptionSettings.TabIndex = 2;
    this.btGroupCaptionSettings.Text = "...";
    this.btGroupCaptionSettings.Click += new EventHandler(this.btGroupCaptionSettings_Click);
    this.lbGroupCaptionExample.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbGroupCaptionExample.BorderStyle = BorderStyle.FixedSingle;
    this.lbGroupCaptionExample.Location = new Point(147, 131);
    this.lbGroupCaptionExample.Name = "lbGroupCaptionExample";
    this.lbGroupCaptionExample.Size = new Size(404, 20);
    this.lbGroupCaptionExample.TabIndex = 1;
    this.lbGroupCaptionExample.Text = "[КЛАСС] [ГОСТ]";
    this.lbGroupCaptionExample.TextAlign = ContentAlignment.MiddleLeft;
    this.btnReplaceDictionarySettings.Enabled = false;
    this.btnReplaceDictionarySettings.FlatStyle = FlatStyle.System;
    this.btnReplaceDictionarySettings.Location = new Point(20, 238);
    this.btnReplaceDictionarySettings.Name = "btnReplaceDictionarySettings";
    this.btnReplaceDictionarySettings.Size = new Size(121, 27);
    this.btnReplaceDictionarySettings.TabIndex = 4;
    this.btnReplaceDictionarySettings.Text = "Словарь замен...";
    this.btnReplaceDictionarySettings.Click += new EventHandler(this.btnReplaceDictionarySettings_Click);
    this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label3.Location = new Point(17, 191);
    this.label3.Name = "label3";
    this.label3.Size = new Size(557, 44);
    this.label3.TabIndex = 3;
    this.label3.Text = "Минимальное количество записей (с одинаковым значением атрибута \"Заголовок группы\"), при котором необходимо производить группировку под общим заголовком.";
    this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label2.Location = new Point(17, 26);
    this.label2.Name = "label2";
    this.label2.Size = new Size(557, 99);
    this.label2.TabIndex = 2;
    this.label2.Text = componentResourceManager.GetString("label2.Text");
    this.label1.Location = new Point(17, 132);
    this.label1.Name = "label1";
    this.label1.Size = new Size(124, 17);
    this.label1.TabIndex = 1;
    this.label1.Text = "Заголовок группы:";
    this.label1.TextAlign = ContentAlignment.MiddleLeft;
    this._label1.Location = new Point(17, 159);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(222, 17);
    this._label1.TabIndex = 3;
    this._label1.Text = "Количество записей для группировки:";
    this._label1.TextAlign = ContentAlignment.MiddleLeft;
    this.editMinRowsForDynamicHeaderGroup.EditValue = (object) 1;
    this.editMinRowsForDynamicHeaderGroup.Location = new Point(245, 159);
    this.editMinRowsForDynamicHeaderGroup.Name = "editMinRowsForDynamicHeaderGroup";
    this.editMinRowsForDynamicHeaderGroup.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.editMinRowsForDynamicHeaderGroup.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.editMinRowsForDynamicHeaderGroup.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.editMinRowsForDynamicHeaderGroup.Properties.IsFloatValue = false;
    this.editMinRowsForDynamicHeaderGroup.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.editMinRowsForDynamicHeaderGroup.Properties.UseCtrlIncrement = false;
    this.editMinRowsForDynamicHeaderGroup.Properties.ValidateOnEnterKey = true;
    this.editMinRowsForDynamicHeaderGroup.Size = new Size(45, 20);
    this.editMinRowsForDynamicHeaderGroup.TabIndex = 3;
    this.editMinRowsForDynamicHeaderGroup.ToolTip = "Сколько строк пропускать между изделиями с различными обозначениями";
    this.editMinRowsForDynamicHeaderGroup.EditValueChanged += new EventHandler(this.MinRowsForDynamicHeaderGroup_EditValueChanged);
    this.editMinRowsForDynamicHeaderGroup.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnReset.Enabled = false;
    this.btnReset.FlatStyle = FlatStyle.System;
    this.btnReset.Location = new Point(12, 297);
    this.btnReset.Name = "btnReset";
    this.btnReset.Size = new Size(121, 27);
    this.btnReset.TabIndex = 6;
    this.btnReset.Text = "По умолчанию";
    this.btnReset.Click += new EventHandler(this._btnReset_Click);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.btnReset);
    this.MinimumSize = new Size(500, 328);
    this.Name = nameof (UserControlDynamicGroupHeaderSettings);
    this.Size = new Size(617, 328);
    this.Load += new EventHandler(this.UserControlDynamicGroupHeaderSettings_Load);
    this.groupBox1.ResumeLayout(false);
    this.editMinRowsForDynamicHeaderGroup.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Корневая настройка </summary>
  public DynamicGroupHeaderSettings RootDynamicGroupHeaderSettings
  {
    get => this._rootDynamicGroupHeaderSettings;
    set => this._rootDynamicGroupHeaderSettings = value;
  }

  /// <summary> Редактируемые настройки </summary>
  public DynamicGroupHeaderSettings DynamicGroupHeaderSettings
  {
    get => this._dynamicGroupHeaderSettings;
    set
    {
      this.LockControls();
      try
      {
        this._dynamicGroupHeaderSettings = value;
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this._dynamicGroupHeaderSettings);
        this.btnReset.Text = value?.Parent == null ? "По умолчанию" : "Наследовать";
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
    this.editMinRowsForDynamicHeaderGroup.Properties.ReadOnly = this.ReadOnly;
    this.editMinRowsForDynamicHeaderGroup.Properties.Buttons[0].Visible = !this.ReadOnly;
    this.editMinRowsForDynamicHeaderGroup.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    if (this._dynamicGroupHeaderSettings == null)
      this.editMinRowsForDynamicHeaderGroup.Text = string.Empty;
    else
      this.editMinRowsForDynamicHeaderGroup.Value = (Decimal) this._dynamicGroupHeaderSettings.MinRowsForDynamicHeaderGroup;
    this.RefreshControlBold((Control) null);
    this.btnReplaceDictionarySettings.Enabled = !this.ReadOnly;
    this.btnReset.Enabled = !this.ReadOnly;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this._dynamicGroupHeaderSettings == null || this._dynamicGroupHeaderSettings.ReadOnly;
  }

  /// <summary>Обновление параметра Bold у шрифта control</summary>
  /// <param name="control">control, у которого надо обновить Bold. Если = null, то обновляется у всех</param>
  public void RefreshControlBold(Control control)
  {
    if (this._dynamicGroupHeaderSettings == null)
      return;
    if (control == null || control == this.editMinRowsForDynamicHeaderGroup)
      this.ChangeControlFontBold((Control) this.editMinRowsForDynamicHeaderGroup, this._dynamicGroupHeaderSettings.MinRowsForDynamicHeaderGroupChanged);
    if (control != null && control != this.btnReplaceDictionarySettings)
      return;
    bool flag = this._dynamicGroupHeaderSettings != null && this._dynamicGroupHeaderSettings.Parent != null && (this._dynamicGroupHeaderSettings.MinRowsForDynamicHeaderGroupChanged || this._dynamicGroupHeaderSettings.DynamicHeaderCaptionSettings != null && this._dynamicGroupHeaderSettings.DynamicHeaderCaptionSettings.Changed);
    if (this.btnReplaceDictionarySettings.Font.Bold != flag)
      return;
    this.btnReplaceDictionarySettings.Font = new Font(this.btnReplaceDictionarySettings.Font.FontFamily, this.btnReplaceDictionarySettings.Font.SizeInPoints, flag ? FontStyle.Bold : FontStyle.Regular, this.btnReplaceDictionarySettings.Font.Unit, this.btnReplaceDictionarySettings.Font.GdiCharSet, this.btnReplaceDictionarySettings.Font.GdiVerticalFont);
  }

  private void ChangeControlFontBold(Control control, bool mustBeBold)
  {
    if (control.Font.Bold == mustBeBold)
      return;
    control.Font = new Font(control.Font.FontFamily, control.Font.SizeInPoints, mustBeBold ? FontStyle.Bold : FontStyle.Regular, control.Font.Unit, control.Font.GdiCharSet, control.Font.GdiVerticalFont);
  }

  private void BeforeChangeUpDown(SpinEdit spinEdit, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._dynamicGroupHeaderSettings == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(spinEdit.Value);
  }

  private bool BeforeUpDownEdit()
  {
    if (this._dynamicGroupHeaderSettings == null || this.ControlsAreUpdating)
      return false;
    bool wasUpdated = false;
    return !(!this.CheckCanEdit(ref wasUpdated) | wasUpdated);
  }

  private void AfterUpDownEdit() => this.Changed = true;

  private void UpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    this.BeforeChangeUpDown((SpinEdit) sender, e);
  }

  private void MinRowsForDynamicHeaderGroup_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._dynamicGroupHeaderSettings.MinRowsForDynamicHeaderGroup = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _btnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || MessageBox.Show("Сбросить изменения в настройках к значениям по умолчанию?", "Группировка записей под общим заголовком", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      this._dynamicGroupHeaderSettings.LoadDefaultParams(false);
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void btnReplaceDictionarySettings_Click(object sender, EventArgs e)
  {
    if (new KeywordReplacementDictForm(AVSDocument.ObjID_CommonSpecificationTemplate).ShowDialog() != DialogResult.OK)
      return;
    this.UpdateControls(true);
  }

  private void btGroupCaptionSettings_Click(object sender, EventArgs e)
  {
    DynamicHeaderCaptionSettings captionSettings = this._dynamicGroupHeaderSettings.DynamicHeaderCaptionSettings.Clone();
    if (new DynamicHeaderCaptionSettingsForm(captionSettings).ShowDialog() != DialogResult.OK)
      return;
    this._dynamicGroupHeaderSettings.DynamicHeaderCaptionSettings = captionSettings;
    this.Changed = true;
    this.UpdateControls(true);
  }

  public override void UpdateControls(bool recurce)
  {
    base.UpdateControls(recurce);
    this.lbGroupCaptionExample.Text = this.DynamicGroupHeaderSettings?.DynamicHeaderCaptionSettings?.ToString();
  }

  private void UserControlDynamicGroupHeaderSettings_Load(object sender, EventArgs e)
  {
    this.ActiveControl = (Control) this.editMinRowsForDynamicHeaderGroup;
  }
}
