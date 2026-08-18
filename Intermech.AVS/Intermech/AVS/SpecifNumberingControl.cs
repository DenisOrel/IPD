// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecifNumberingControl
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

/// <summary> Панель настройки схемы нумерации позиций (неполная) </summary>
public class SpecifNumberingControl : ExtUserControl
{
  protected Label label1;
  protected GroupBox groupBox1;
  protected Label label2;
  protected Label label3;
  protected Label label4;
  protected Label label5;
  protected Label label6;
  protected Label label7;
  protected Label label8;
  protected Label label9;
  protected SpinEdit StartNumberUpDown;
  protected SpinEdit BetweenDifferentDesignationsUpDown;
  protected SpinEdit BetweenIspolnsUpDown;
  protected SpinEdit BeforeNewPartUpDown;
  protected SpinEdit BeforeNewRazdelUpDown;
  protected SpinEdit BetweenSameDesignationsUpDown;
  protected SpinEdit BeforeVariableDataUpDown;
  protected SpinEdit BeforeNewObjTypeUpDown;
  protected SpinEdit BeforeNewIspolnUpDown;
  private ToolTipController EditModeToolTip;
  private ToolTipController ReadModeToolTip;
  private CheckEdit cbIzdeliaSameNumbers;
  private IContainer components;
  protected SpecifNumbering _SpecifNumbering;

  public SpecifNumberingControl() => this.InitializeComponent();

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
    this.label1 = new Label();
    this.groupBox1 = new GroupBox();
    this.BeforeNewIspolnUpDown = new SpinEdit();
    this.BeforeNewObjTypeUpDown = new SpinEdit();
    this.BeforeVariableDataUpDown = new SpinEdit();
    this.BeforeNewRazdelUpDown = new SpinEdit();
    this.BeforeNewPartUpDown = new SpinEdit();
    this.BetweenIspolnsUpDown = new SpinEdit();
    this.BetweenDifferentDesignationsUpDown = new SpinEdit();
    this.label9 = new Label();
    this.label8 = new Label();
    this.label7 = new Label();
    this.label6 = new Label();
    this.label5 = new Label();
    this.label4 = new Label();
    this.label3 = new Label();
    this.label2 = new Label();
    this.BetweenSameDesignationsUpDown = new SpinEdit();
    this.StartNumberUpDown = new SpinEdit();
    this.EditModeToolTip = new ToolTipController(this.components);
    this.ReadModeToolTip = new ToolTipController(this.components);
    this.cbIzdeliaSameNumbers = new CheckEdit();
    this.groupBox1.SuspendLayout();
    this.BeforeNewIspolnUpDown.Properties.BeginInit();
    this.BeforeNewObjTypeUpDown.Properties.BeginInit();
    this.BeforeVariableDataUpDown.Properties.BeginInit();
    this.BeforeNewRazdelUpDown.Properties.BeginInit();
    this.BeforeNewPartUpDown.Properties.BeginInit();
    this.BetweenIspolnsUpDown.Properties.BeginInit();
    this.BetweenDifferentDesignationsUpDown.Properties.BeginInit();
    this.BetweenSameDesignationsUpDown.Properties.BeginInit();
    this.StartNumberUpDown.Properties.BeginInit();
    this.cbIzdeliaSameNumbers.Properties.BeginInit();
    this.SuspendLayout();
    this.label1.Location = new Point(19, 12);
    this.label1.Name = "label1";
    this.label1.Size = new Size(171, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Начать нумерацию с номера";
    this.label1.TextAlign = ContentAlignment.MiddleRight;
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.BeforeNewIspolnUpDown);
    this.groupBox1.Controls.Add((Control) this.BeforeNewObjTypeUpDown);
    this.groupBox1.Controls.Add((Control) this.BeforeVariableDataUpDown);
    this.groupBox1.Controls.Add((Control) this.BeforeNewRazdelUpDown);
    this.groupBox1.Controls.Add((Control) this.BeforeNewPartUpDown);
    this.groupBox1.Controls.Add((Control) this.BetweenIspolnsUpDown);
    this.groupBox1.Controls.Add((Control) this.BetweenDifferentDesignationsUpDown);
    this.groupBox1.Controls.Add((Control) this.label9);
    this.groupBox1.Controls.Add((Control) this.label8);
    this.groupBox1.Controls.Add((Control) this.label7);
    this.groupBox1.Controls.Add((Control) this.label6);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.BetweenSameDesignationsUpDown);
    this.groupBox1.FlatStyle = FlatStyle.System;
    this.groupBox1.Location = new Point(5, 58);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(503, 176 /*0xB0*/);
    this.groupBox1.TabIndex = 1;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Шаг позиций между записями";
    this.BeforeNewIspolnUpDown.EditValue = (object) 1;
    this.BeforeNewIspolnUpDown.Location = new Point(451, 112 /*0x70*/);
    this.BeforeNewIspolnUpDown.Name = "BeforeNewIspolnUpDown";
    this.BeforeNewIspolnUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BeforeNewIspolnUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewIspolnUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewIspolnUpDown.Properties.IsFloatValue = false;
    this.BeforeNewIspolnUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BeforeNewIspolnUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BeforeNewIspolnUpDown.Properties.UseCtrlIncrement = false;
    this.BeforeNewIspolnUpDown.Properties.ValidateOnEnterKey = true;
    this.BeforeNewIspolnUpDown.Size = new Size(45, 20);
    this.BeforeNewIspolnUpDown.TabIndex = 8;
    this.BeforeNewIspolnUpDown.ToolTip = "Шаг нумерации перед новым исполнением спецификации";
    this.BeforeNewIspolnUpDown.EditValueChanged += new EventHandler(this.BeforeNewIspolnUpDown_ValueChanged);
    this.BeforeNewIspolnUpDown.EditValueChanging += new ChangingEventHandler(this.BeforeNewIspolnUpDown_EditValueChanging);
    this.BeforeNewObjTypeUpDown.EditValue = (object) 1;
    this.BeforeNewObjTypeUpDown.Location = new Point(451, 50);
    this.BeforeNewObjTypeUpDown.Name = "BeforeNewObjTypeUpDown";
    this.BeforeNewObjTypeUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BeforeNewObjTypeUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewObjTypeUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewObjTypeUpDown.Properties.IsFloatValue = false;
    this.BeforeNewObjTypeUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BeforeNewObjTypeUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BeforeNewObjTypeUpDown.Properties.UseCtrlIncrement = false;
    this.BeforeNewObjTypeUpDown.Properties.ValidateOnEnterKey = true;
    this.BeforeNewObjTypeUpDown.Size = new Size(45, 20);
    this.BeforeNewObjTypeUpDown.TabIndex = 7;
    this.BeforeNewObjTypeUpDown.ToolTip = "Шаг нумерации перед новым типом изделия";
    this.BeforeNewObjTypeUpDown.EditValueChanged += new EventHandler(this.BeforeNewObjTypeUpDown_ValueChanged);
    this.BeforeNewObjTypeUpDown.EditValueChanging += new ChangingEventHandler(this.BeforeNewObjTypeUpDown_EditValueChanging);
    this.BeforeVariableDataUpDown.EditValue = (object) 1;
    this.BeforeVariableDataUpDown.Location = new Point(451, 81);
    this.BeforeVariableDataUpDown.Name = "BeforeVariableDataUpDown";
    this.BeforeVariableDataUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BeforeVariableDataUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeVariableDataUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeVariableDataUpDown.Properties.IsFloatValue = false;
    this.BeforeVariableDataUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BeforeVariableDataUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BeforeVariableDataUpDown.Properties.UseCtrlIncrement = false;
    this.BeforeVariableDataUpDown.Properties.ValidateOnEnterKey = true;
    this.BeforeVariableDataUpDown.Size = new Size(45, 20);
    this.BeforeVariableDataUpDown.TabIndex = 6;
    this.BeforeVariableDataUpDown.ToolTip = "Шаг нумерации перед переменными данными спецификации";
    this.BeforeVariableDataUpDown.EditValueChanged += new EventHandler(this.BeforeVariableDataUpDown_ValueChanged);
    this.BeforeVariableDataUpDown.EditValueChanging += new ChangingEventHandler(this.BeforeVariableDataUpDown_EditValueChanging);
    this.BeforeNewRazdelUpDown.EditValue = (object) 1;
    this.BeforeNewRazdelUpDown.Location = new Point(190, 81);
    this.BeforeNewRazdelUpDown.Name = "BeforeNewRazdelUpDown";
    this.BeforeNewRazdelUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BeforeNewRazdelUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewRazdelUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewRazdelUpDown.Properties.IsFloatValue = false;
    this.BeforeNewRazdelUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BeforeNewRazdelUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BeforeNewRazdelUpDown.Properties.UseCtrlIncrement = false;
    this.BeforeNewRazdelUpDown.Properties.ValidateOnEnterKey = true;
    this.BeforeNewRazdelUpDown.Size = new Size(45, 20);
    this.BeforeNewRazdelUpDown.TabIndex = 3;
    this.BeforeNewRazdelUpDown.ToolTip = "Шаг нумерации перед новым разделом спецификации";
    this.BeforeNewRazdelUpDown.EditValueChanged += new EventHandler(this.BeforeNewRazdelUpDown_ValueChanged);
    this.BeforeNewRazdelUpDown.EditValueChanging += new ChangingEventHandler(this.BeforeNewRazdelUpDown_EditValueChanging);
    this.BeforeNewPartUpDown.EditValue = (object) 1;
    this.BeforeNewPartUpDown.Location = new Point(190, 112 /*0x70*/);
    this.BeforeNewPartUpDown.Name = "BeforeNewPartUpDown";
    this.BeforeNewPartUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BeforeNewPartUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewPartUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewPartUpDown.Properties.IsFloatValue = false;
    this.BeforeNewPartUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BeforeNewPartUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BeforeNewPartUpDown.Properties.UseCtrlIncrement = false;
    this.BeforeNewPartUpDown.Properties.ValidateOnEnterKey = true;
    this.BeforeNewPartUpDown.Size = new Size(45, 20);
    this.BeforeNewPartUpDown.TabIndex = 2;
    this.BeforeNewPartUpDown.ToolTip = "Шаг нумерации перед новой частью спецификации";
    this.BeforeNewPartUpDown.EditValueChanged += new EventHandler(this.BeforeNewPartUpDown_ValueChanged);
    this.BeforeNewPartUpDown.EditValueChanging += new ChangingEventHandler(this.BeforeNewPartUpDown_EditValueChanging);
    this.BetweenIspolnsUpDown.EditValue = (object) 1;
    this.BetweenIspolnsUpDown.Location = new Point(190, 50);
    this.BetweenIspolnsUpDown.Name = "BetweenIspolnsUpDown";
    this.BetweenIspolnsUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BetweenIspolnsUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BetweenIspolnsUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BetweenIspolnsUpDown.Properties.IsFloatValue = false;
    this.BetweenIspolnsUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BetweenIspolnsUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BetweenIspolnsUpDown.Properties.UseCtrlIncrement = false;
    this.BetweenIspolnsUpDown.Properties.ValidateOnEnterKey = true;
    this.BetweenIspolnsUpDown.Size = new Size(45, 20);
    this.BetweenIspolnsUpDown.TabIndex = 1;
    this.BetweenIspolnsUpDown.ToolTip = "Шаг нумерации между различными исполнениями детали";
    this.BetweenIspolnsUpDown.EditValueChanged += new EventHandler(this.BetweenIspolnsUpDown_ValueChanged);
    this.BetweenIspolnsUpDown.EditValueChanging += new ChangingEventHandler(this.BetweenIspolnsUpDown_EditValueChanging);
    this.BetweenDifferentDesignationsUpDown.EditValue = (object) 1;
    this.BetweenDifferentDesignationsUpDown.Location = new Point(190, 19);
    this.BetweenDifferentDesignationsUpDown.Name = "BetweenDifferentDesignationsUpDown";
    this.BetweenDifferentDesignationsUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BetweenDifferentDesignationsUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BetweenDifferentDesignationsUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BetweenDifferentDesignationsUpDown.Properties.IsFloatValue = false;
    this.BetweenDifferentDesignationsUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BetweenDifferentDesignationsUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BetweenDifferentDesignationsUpDown.Properties.UseCtrlIncrement = false;
    this.BetweenDifferentDesignationsUpDown.Properties.ValidateOnEnterKey = true;
    this.BetweenDifferentDesignationsUpDown.Size = new Size(45, 20);
    this.BetweenDifferentDesignationsUpDown.TabIndex = 0;
    this.BetweenDifferentDesignationsUpDown.ToolTip = "Шаг между позициями в спецификации при различных обозначениях у изделий";
    this.BetweenDifferentDesignationsUpDown.EditValueChanged += new EventHandler(this.BetweenDifferentDesignationsUpDown_ValueChanged);
    this.BetweenDifferentDesignationsUpDown.EditValueChanging += new ChangingEventHandler(this.BetweenDifferentDesignationsUpDown_EditValueChanging);
    this.label9.Location = new Point(243, 46);
    this.label9.Name = "label9";
    this.label9.Size = new Size(200, 31 /*0x1F*/);
    this.label9.TabIndex = 15;
    this.label9.Text = "Перед новым классом\r\nстандартного изделия";
    this.label9.TextAlign = ContentAlignment.MiddleRight;
    this.label8.Location = new Point(243, 115);
    this.label8.Name = "label8";
    this.label8.Size = new Size(200, 16 /*0x10*/);
    this.label8.TabIndex = 13;
    this.label8.Text = "Перед новым исполнением";
    this.label8.TextAlign = ContentAlignment.MiddleRight;
    this.label7.Location = new Point(243, 84);
    this.label7.Name = "label7";
    this.label7.Size = new Size(200, 16 /*0x10*/);
    this.label7.TabIndex = 11;
    this.label7.Text = "Перед переменными данными";
    this.label7.TextAlign = ContentAlignment.MiddleRight;
    this.label6.Location = new Point(25, 84);
    this.label6.Name = "label6";
    this.label6.Size = new Size(160 /*0xA0*/, 16 /*0x10*/);
    this.label6.TabIndex = 9;
    this.label6.Text = "Перед новым разделом";
    this.label6.TextAlign = ContentAlignment.MiddleRight;
    this.label5.Location = new Point(25, 115);
    this.label5.Name = "label5";
    this.label5.Size = new Size(160 /*0xA0*/, 16 /*0x10*/);
    this.label5.TabIndex = 7;
    this.label5.Text = "Перед новой частью";
    this.label5.TextAlign = ContentAlignment.MiddleRight;
    this.label4.Location = new Point(11, 53);
    this.label4.Name = "label4";
    this.label4.Size = new Size(174, 16 /*0x10*/);
    this.label4.TabIndex = 5;
    this.label4.Text = "Между исполнениями детали";
    this.label4.TextAlign = ContentAlignment.MiddleRight;
    this.label3.Location = new Point(243, 22);
    this.label3.Name = "label3";
    this.label3.Size = new Size(200, 16 /*0x10*/);
    this.label3.TabIndex = 3;
    this.label3.Text = "При похожих обозначениях";
    this.label3.TextAlign = ContentAlignment.MiddleRight;
    this.label2.Location = new Point(14, 22);
    this.label2.Name = "label2";
    this.label2.Size = new Size(171, 16 /*0x10*/);
    this.label2.TabIndex = 0;
    this.label2.Text = "При различных обозначениях";
    this.label2.TextAlign = ContentAlignment.MiddleRight;
    this.BetweenSameDesignationsUpDown.EditValue = (object) 1;
    this.BetweenSameDesignationsUpDown.Location = new Point(451, 19);
    this.BetweenSameDesignationsUpDown.Name = "BetweenSameDesignationsUpDown";
    this.BetweenSameDesignationsUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.BetweenSameDesignationsUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BetweenSameDesignationsUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BetweenSameDesignationsUpDown.Properties.IsFloatValue = false;
    this.BetweenSameDesignationsUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.BetweenSameDesignationsUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.BetweenSameDesignationsUpDown.Properties.UseCtrlIncrement = false;
    this.BetweenSameDesignationsUpDown.Properties.ValidateOnEnterKey = true;
    this.BetweenSameDesignationsUpDown.Size = new Size(45, 20);
    this.BetweenSameDesignationsUpDown.TabIndex = 4;
    this.BetweenSameDesignationsUpDown.ToolTip = "Шаг между позициями в спецификации при похожих обозначениях у изделий";
    this.BetweenSameDesignationsUpDown.EditValueChanged += new EventHandler(this.BetweenSameDesignationsUpDown_ValueChanged);
    this.BetweenSameDesignationsUpDown.EditValueChanging += new ChangingEventHandler(this.BetweenSameDesignationsUpDown_EditValueChanging);
    this.StartNumberUpDown.EditValue = (object) new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.StartNumberUpDown.Location = new Point(195, 9);
    this.StartNumberUpDown.Name = "StartNumberUpDown";
    this.StartNumberUpDown.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.StartNumberUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.StartNumberUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.StartNumberUpDown.Properties.IsFloatValue = false;
    this.StartNumberUpDown.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this.StartNumberUpDown.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.StartNumberUpDown.Properties.UseCtrlIncrement = false;
    this.StartNumberUpDown.Properties.ValidateOnEnterKey = true;
    this.StartNumberUpDown.Size = new Size(45, 20);
    this.StartNumberUpDown.TabIndex = 0;
    this.StartNumberUpDown.ToolTip = "С какого номера начать нумерации позиций в спецификации (номер первой позиции)";
    this.StartNumberUpDown.EditValueChanged += new EventHandler(this.StartNumberUpDown_ValueChanged);
    this.StartNumberUpDown.EditValueChanging += new ChangingEventHandler(this.StartNumberUpDown_EditValueChanging);
    this.EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this.ReadModeToolTip.Active = false;
    this.ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this.cbIzdeliaSameNumbers.Location = new Point(33, 35);
    this.cbIzdeliaSameNumbers.Name = "cbIzdeliaSameNumbers";
    this.cbIzdeliaSameNumbers.Properties.Caption = "Одинаковые номера изделия в различных исполнениях";
    this.cbIzdeliaSameNumbers.Size = new Size(327, 19);
    this.cbIzdeliaSameNumbers.TabIndex = 19;
    this.cbIzdeliaSameNumbers.EditValueChanged += new EventHandler(this.cbIzdeliaSameNumbers_EditValueChanged);
    this.cbIzdeliaSameNumbers.EditValueChanging += new ChangingEventHandler(this.cbIzdeliaSameNumbers_EditValueChanging);
    this.AutoScroll = true;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.cbIzdeliaSameNumbers);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.StartNumberUpDown);
    this.MinimumSize = new Size(513, 179);
    this.Name = nameof (SpecifNumberingControl);
    this.Size = new Size(513, 237);
    this.groupBox1.ResumeLayout(false);
    this.BeforeNewIspolnUpDown.Properties.EndInit();
    this.BeforeNewObjTypeUpDown.Properties.EndInit();
    this.BeforeVariableDataUpDown.Properties.EndInit();
    this.BeforeNewRazdelUpDown.Properties.EndInit();
    this.BeforeNewPartUpDown.Properties.EndInit();
    this.BetweenIspolnsUpDown.Properties.EndInit();
    this.BetweenDifferentDesignationsUpDown.Properties.EndInit();
    this.BetweenSameDesignationsUpDown.Properties.EndInit();
    this.StartNumberUpDown.Properties.EndInit();
    this.cbIzdeliaSameNumbers.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Обновление параметра Bold у шрифта NumericUpDown</summary>
  /// <param name="numericUpDown">NumericUpDown, у которого надо обновить Bold. Если = null, то обновляется у всех</param>
  public void RefreshBoldUpDown(BaseEdit numericUpDown)
  {
    if (numericUpDown == null || this.StartNumberUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.StartNumberUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.StartNumber != this._SpecifNumbering.ParentLevel.StartNumber);
    if (numericUpDown == null || this.BetweenDifferentDesignationsUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BetweenDifferentDesignationsUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BetweenDifferentDesignations != this._SpecifNumbering.ParentLevel.BetweenDifferentDesignations);
    if (numericUpDown == null || this.BetweenSameDesignationsUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BetweenSameDesignationsUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BetweenSameDesignations != this._SpecifNumbering.ParentLevel.BetweenSameDesignations);
    if (numericUpDown == null || this.BetweenIspolnsUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BetweenIspolnsUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BetweenIspolns != this._SpecifNumbering.ParentLevel.BetweenIspolns);
    if (numericUpDown == null || this.BeforeNewPartUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BeforeNewPartUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BeforeNewPart != this._SpecifNumbering.ParentLevel.BeforeNewPart);
    if (numericUpDown == null || this.BeforeNewRazdelUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BeforeNewRazdelUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BeforeNewRazdel != this._SpecifNumbering.ParentLevel.BeforeNewRazdel);
    if (numericUpDown == null || this.BeforeVariableDataUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BeforeVariableDataUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BeforeVariableData != this._SpecifNumbering.ParentLevel.BeforeVariableData);
    if (numericUpDown == null || this.BeforeNewIspolnUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BeforeNewIspolnUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BeforeNewIspoln != this._SpecifNumbering.ParentLevel.BeforeNewIspoln);
    if (numericUpDown == null || this.BeforeNewObjTypeUpDown == numericUpDown)
      this.ChangeUpDownFontBold((BaseEdit) this.BeforeNewObjTypeUpDown, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.BeforeNewObjType != this._SpecifNumbering.ParentLevel.BeforeNewObjType);
    if (numericUpDown != null && this.cbIzdeliaSameNumbers != numericUpDown)
      return;
    this.ChangeUpDownFontBold((BaseEdit) this.cbIzdeliaSameNumbers, this._SpecifNumbering != null && this._SpecifNumbering.ParentLevel != null && this._SpecifNumbering.IzdelieSameNumbers != this._SpecifNumbering.ParentLevel.IzdelieSameNumbers);
  }

  private void ChangeUpDownFontBold(BaseEdit numericUpDown, bool mustBeBold)
  {
    if (numericUpDown.Font.Bold == mustBeBold)
      return;
    numericUpDown.Font = new Font(numericUpDown.Font.FontFamily, numericUpDown.Font.SizeInPoints, mustBeBold ? FontStyle.Bold : FontStyle.Regular, numericUpDown.Font.Unit, numericUpDown.Font.GdiCharSet, numericUpDown.Font.GdiVerticalFont);
  }

  private void StartNumberUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32((Decimal) Decimal.ToInt32(this.StartNumberUpDown.Value));
  }

  private void StartNumberUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    this._SpecifNumbering.StartNumber = (int) this.StartNumberUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.StartNumberUpDown);
  }

  private void BetweenDifferentDesignationsUpDown_EditValueChanging(
    object sender,
    ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenDifferentDesignationsUpDown.Value);
  }

  private void BetweenDifferentDesignationsUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BetweenDifferentDesignations = (int) this.BetweenDifferentDesignationsUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BetweenDifferentDesignationsUpDown);
  }

  private void BetweenSameDesignationsUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BetweenSameDesignationsUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BetweenSameDesignations = (int) this.BetweenSameDesignationsUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BetweenSameDesignationsUpDown);
  }

  private void BetweenIspolnsUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BetweenIspolnsUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BetweenIspolns = (int) this.BetweenIspolnsUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BetweenIspolnsUpDown);
  }

  private void BeforeNewPartUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BeforeNewPartUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BeforeNewPart = (int) this.BeforeNewPartUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BeforeNewPartUpDown);
  }

  private void BeforeNewRazdelUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BeforeNewRazdelUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BeforeNewRazdel = (int) this.BeforeNewRazdelUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BeforeNewRazdelUpDown);
  }

  private void BeforeVariableDataUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BeforeVariableDataUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BeforeVariableData = (int) this.BeforeVariableDataUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BeforeVariableDataUpDown);
  }

  private void BeforeNewIspolnUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BeforeNewIspolnUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BeforeNewIspoln = (int) this.BeforeNewIspolnUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BeforeNewIspolnUpDown);
  }

  private void BeforeNewObjTypeUpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(this.BetweenSameDesignationsUpDown.Value);
  }

  private void BeforeNewObjTypeUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.BeforeNewObjType = (int) this.BeforeNewObjTypeUpDown.Value;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.BeforeNewObjTypeUpDown);
  }

  /// <summary>Получение/установка схемы нумерации позиций</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SpecifNumbering SpecifNumbering
  {
    get => this._SpecifNumbering;
    set
    {
      if (this._SpecifNumbering == value)
        return;
      this._SpecifNumbering = value;
      this.LockControls();
      try
      {
        this.RefreshReadOnly();
        this.UpdateControls(true);
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
    if (this._SpecifNumbering != null)
    {
      this.StartNumberUpDown.Value = (Decimal) this._SpecifNumbering.StartNumber;
      this.BetweenDifferentDesignationsUpDown.Value = (Decimal) this._SpecifNumbering.BetweenDifferentDesignations;
      this.BetweenSameDesignationsUpDown.Value = (Decimal) this._SpecifNumbering.BetweenSameDesignations;
      this.BetweenIspolnsUpDown.Value = (Decimal) this._SpecifNumbering.BetweenIspolns;
      this.BeforeNewPartUpDown.Value = (Decimal) this._SpecifNumbering.BeforeNewPart;
      this.BeforeNewRazdelUpDown.Value = (Decimal) this._SpecifNumbering.BeforeNewRazdel;
      this.BeforeVariableDataUpDown.Value = (Decimal) this._SpecifNumbering.BeforeVariableData;
      this.BeforeNewIspolnUpDown.Value = (Decimal) this._SpecifNumbering.BeforeNewIspoln;
      this.BeforeNewObjTypeUpDown.Value = (Decimal) this._SpecifNumbering.BeforeNewObjType;
      this.cbIzdeliaSameNumbers.Checked = this._SpecifNumbering.IzdelieSameNumbers;
    }
    else
    {
      this.StartNumberUpDown.Text = string.Empty;
      this.BetweenDifferentDesignationsUpDown.Text = string.Empty;
      this.BetweenSameDesignationsUpDown.Text = string.Empty;
      this.BetweenIspolnsUpDown.Text = string.Empty;
      this.BeforeNewPartUpDown.Text = string.Empty;
      this.BeforeNewRazdelUpDown.Text = string.Empty;
      this.BeforeVariableDataUpDown.Text = string.Empty;
      this.BeforeNewIspolnUpDown.Text = string.Empty;
      this.BeforeNewObjTypeUpDown.Text = string.Empty;
      this.cbIzdeliaSameNumbers.CheckState = CheckState.Indeterminate;
    }
    this.StartNumberUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BetweenDifferentDesignationsUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BetweenSameDesignationsUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BetweenIspolnsUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BeforeNewPartUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BeforeNewRazdelUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BeforeVariableDataUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BeforeNewIspolnUpDown.Properties.ReadOnly = this.ReadOnly;
    this.BeforeNewObjTypeUpDown.Properties.ReadOnly = this.ReadOnly;
    this.cbIzdeliaSameNumbers.Properties.ReadOnly = this.ReadOnly;
    this.StartNumberUpDown.BackColor = this.StartNumberUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BetweenDifferentDesignationsUpDown.BackColor = this.BetweenDifferentDesignationsUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BetweenSameDesignationsUpDown.BackColor = this.BetweenSameDesignationsUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BetweenIspolnsUpDown.BackColor = this.BetweenIspolnsUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BeforeNewPartUpDown.BackColor = this.BeforeNewPartUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BeforeNewRazdelUpDown.BackColor = this.BeforeNewRazdelUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BeforeVariableDataUpDown.BackColor = this.BeforeVariableDataUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BeforeNewIspolnUpDown.BackColor = this.BeforeNewIspolnUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.BeforeNewObjTypeUpDown.BackColor = this.BeforeNewObjTypeUpDown.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this.StartNumberUpDown.Properties.Buttons[0].Visible = !this.StartNumberUpDown.Properties.ReadOnly;
    this.BetweenDifferentDesignationsUpDown.Properties.Buttons[0].Visible = !this.BetweenDifferentDesignationsUpDown.Properties.ReadOnly;
    this.BetweenSameDesignationsUpDown.Properties.Buttons[this.BetweenSameDesignationsUpDown.Properties.SpinButtonIndex].Visible = !this.BetweenSameDesignationsUpDown.Properties.ReadOnly;
    this.BetweenIspolnsUpDown.Properties.Buttons[0].Visible = !this.BetweenIspolnsUpDown.Properties.ReadOnly;
    this.BeforeNewPartUpDown.Properties.Buttons[0].Visible = !this.BeforeNewPartUpDown.Properties.ReadOnly;
    this.BeforeNewRazdelUpDown.Properties.Buttons[0].Visible = !this.BeforeNewRazdelUpDown.Properties.ReadOnly;
    this.BeforeVariableDataUpDown.Properties.Buttons[0].Visible = !this.BeforeVariableDataUpDown.Properties.ReadOnly;
    this.BeforeNewIspolnUpDown.Properties.Buttons[0].Visible = !this.BeforeNewIspolnUpDown.Properties.ReadOnly;
    this.BeforeNewObjTypeUpDown.Properties.Buttons[0].Visible = !this.BeforeNewObjTypeUpDown.Properties.ReadOnly;
    this.RefreshBoldUpDown((BaseEdit) null);
    if (this.EditModeToolTip == null)
      return;
    if (this.ReadOnly)
    {
      if (!this.EditModeToolTip.Active)
        return;
      this.EditModeToolTip.Active = false;
      this.ReadModeToolTip.Active = true;
    }
    else
    {
      if (!this.ReadModeToolTip.Active)
        return;
      this.ReadModeToolTip.Active = false;
      this.EditModeToolTip.Active = true;
    }
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly() => this._SpecifNumbering == null || !this.Enabled;

  private void cbIzdeliaSameNumbers_EditValueChanged(object sender, EventArgs e)
  {
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    bool wasUpdated = false;
    if (!this.CheckCanEdit(ref wasUpdated) | wasUpdated)
      return;
    this._SpecifNumbering.IzdelieSameNumbers = this.cbIzdeliaSameNumbers.Checked;
    this.Changed = true;
    this.RefreshBoldUpDown((BaseEdit) this.cbIzdeliaSameNumbers);
  }

  private void cbIzdeliaSameNumbers_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    bool flag = e.OldValue != null && !(e.OldValue.GetType() != typeof (bool)) && (bool) e.OldValue;
    if (this._SpecifNumbering == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && flag != this.cbIzdeliaSameNumbers.Checked;
  }
}
