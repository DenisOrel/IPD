
// Type: Intermech.Client.Core.CompositionView.ButtonParamsEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Редактор параметров кнопок</summary>
public class ButtonParamsEditor : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button bOk;
  private Button bCancel;
  private PropertyGrid propertyGrid1;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.bOk.Enabled = false;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1618);
  }

  /// <summary>Конструктор</summary>
  /// <param name="paramsObject">объект описывающий параметры кнопки</param>
  public ButtonParamsEditor(object paramsObject)
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
    this.propertyGrid1.SelectedObject = paramsObject;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  /// <param name="e"></param>
  private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.bOk.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ButtonParamsEditor_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ButtonParamsEditor_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ButtonParamsEditor));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.bOk = new Button();
    this.bCancel = new Button();
    this.propertyGrid1 = new PropertyGrid();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.bOk, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.bCancel, 2, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.propertyGrid1, 0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.bOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.bOk, "bOk");
    this.bOk.Name = "bOk";
    this.bOk.UseVisualStyleBackColor = true;
    this.bCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.propertyGrid1, 3);
    componentResourceManager.ApplyResources((object) this.propertyGrid1, "propertyGrid1");
    this.propertyGrid1.Name = "propertyGrid1";
    this.propertyGrid1.ToolbarVisible = false;
    this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ButtonParamsEditor);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.ButtonParamsEditor_FormClosed);
    this.Load += new EventHandler(this.ButtonParamsEditor_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
