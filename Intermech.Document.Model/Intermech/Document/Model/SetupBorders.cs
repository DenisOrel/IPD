// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.SetupBorders
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraEditors.Controls;
using Intermech.Controls;
using Intermech.UI;
using MWCommon;
using MWControls;
using OfficePickers.ColorPicker;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary> Форма для настройки границ в таблице </summary>
public class SetupBorders : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Bevel _bevel1;
  private Bevel bevel1;
  private Bevel _bevel2;
  private Label _label1;
  private MWLabel mwLabel1;
  private Label label1;
  private ListBox _listBoxLineTypes;
  private Label label2;
  private Label label3;
  private ComboBoxColorPicker comboBoxColorPicker1;
  private Label label4;
  private MeasureSpinEdit measureSpinEdit1;
  private Panel panelPreDefault;
  private Panel panelExample;

  public SetupBorders() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetupBorders));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._bevel1 = new Bevel();
    this.bevel1 = new Bevel();
    this._bevel2 = new Bevel();
    this._label1 = new Label();
    this.mwLabel1 = new MWLabel();
    this.label1 = new Label();
    this._listBoxLineTypes = new ListBox();
    this.label2 = new Label();
    this.label3 = new Label();
    this.comboBoxColorPicker1 = new ComboBoxColorPicker();
    this.label4 = new Label();
    this.measureSpinEdit1 = new MeasureSpinEdit();
    this.panelPreDefault = new Panel();
    this.panelExample = new Panel();
    this.measureSpinEdit1.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._bevel1, "_bevel1");
    this._bevel1.Name = "_bevel1";
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.Name = "bevel1";
    componentResourceManager.ApplyResources((object) this._bevel2, "_bevel2");
    this._bevel2.Name = "_bevel2";
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this.mwLabel1, "mwLabel1");
    this.mwLabel1.Name = "mwLabel1";
    this.mwLabel1.StringFrmt = StringFormatEnum.GenericTypographic;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this._listBoxLineTypes.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._listBoxLineTypes, "_listBoxLineTypes");
    this._listBoxLineTypes.Name = "_listBoxLineTypes";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.comboBoxColorPicker1.Color = Color.Black;
    this.comboBoxColorPicker1.DrawMode = DrawMode.OwnerDrawFixed;
    this.comboBoxColorPicker1.DropDownHeight = 1;
    this.comboBoxColorPicker1.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxColorPicker1.DropDownWidth = 1;
    this.comboBoxColorPicker1.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.comboBoxColorPicker1, "comboBoxColorPicker1");
    this.comboBoxColorPicker1.Items.AddRange(new object[9]
    {
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items1"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items2"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items3"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items4"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items5"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items6"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items7"),
      (object) componentResourceManager.GetString("comboBoxColorPicker1.Items8")
    });
    this.comboBoxColorPicker1.Name = "comboBoxColorPicker1";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.measureSpinEdit1, "measureSpinEdit1");
    this.measureSpinEdit1.LastValue = 0.0;
    this.measureSpinEdit1.Name = "measureSpinEdit1";
    this.measureSpinEdit1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.measureSpinEdit1.Properties.UseCtrlIncrement = false;
    componentResourceManager.ApplyResources((object) this.panelPreDefault, "panelPreDefault");
    this.panelPreDefault.Name = "panelPreDefault";
    componentResourceManager.ApplyResources((object) this.panelExample, "panelExample");
    this.panelExample.Name = "panelExample";
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.panelExample);
    this.Controls.Add((Control) this.panelPreDefault);
    this.Controls.Add((Control) this.measureSpinEdit1);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.comboBoxColorPicker1);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this._listBoxLineTypes);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.mwLabel1);
    this.Controls.Add((Control) this._label1);
    this.Controls.Add((Control) this._bevel2);
    this.Controls.Add((Control) this.bevel1);
    this.Controls.Add((Control) this._bevel1);
    this.Controls.Add((Control) this._btnOK);
    this.Controls.Add((Control) this._btnCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SetupBorders);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.measureSpinEdit1.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
