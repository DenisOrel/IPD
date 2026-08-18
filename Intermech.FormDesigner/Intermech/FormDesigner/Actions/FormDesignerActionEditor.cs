// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Actions.FormDesignerActionEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Actions;

/// <summary>Класс выбора действия на кнопку.</summary>
internal class FormDesignerActionEditor : Form
{
  private IFormDesignerActionManager _actionManager;
  private FormDesignerAction _selectAtStart;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel _layoutPnl;
  private Button _btnCancel;
  private Button _btnOK;
  private ListBox _lst;

  /// <summary>Выбранное действие.</summary>
  public FormDesignerAction SelectedAction => this._lst.SelectedItem as FormDesignerAction;

  /// <summary>Конструктор.</summary>
  /// <param name="selectedAction">Выделенное действие</param>
  public FormDesignerActionEditor(FormDesignerAction selectedAction = null)
  {
    this.InitializeComponent();
    this._actionManager = ServiceUtils.GetService<IFormDesignerActionManager>((object) ApplicationServices.Container, false);
    this.LoadActions();
    this._selectAtStart = selectedAction;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_DoubleClick(object sender, EventArgs e)
  {
    if (this._lst.SelectedIndex <= -1)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnOK.Enabled = this._lst.SelectedIndex >= 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnFormClosed(FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e) => FormStorage.LoadLayout((Control) this);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnShown(EventArgs e)
  {
    if (this._selectAtStart == null)
      return;
    this._lst.SelectedItem = (object) this._selectAtStart;
  }

  /// <summary>Загрузить действия.</summary>
  private void LoadActions()
  {
    this._lst.BeginUpdate();
    try
    {
      this._lst.Items.Clear();
      this._lst.Items.AddRange((object[]) this._actionManager.ToArray<FormDesignerAction>());
    }
    finally
    {
      this._lst.EndUpdate();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDesignerActionEditor));
    this._layoutPnl = new TableLayoutPanel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._lst = new ListBox();
    this._layoutPnl.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._layoutPnl, "_layoutPnl");
    this._layoutPnl.Controls.Add((Control) this._btnCancel, 2, 1);
    this._layoutPnl.Controls.Add((Control) this._btnOK, 1, 1);
    this._layoutPnl.Controls.Add((Control) this._lst, 0, 0);
    this._layoutPnl.Name = "_layoutPnl";
    this._btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._layoutPnl.SetColumnSpan((Control) this._lst, 3);
    componentResourceManager.ApplyResources((object) this._lst, "_lst");
    this._lst.FormattingEnabled = true;
    this._lst.Name = "_lst";
    this._lst.SelectedIndexChanged += new EventHandler(this.On_lst_SelectedIndexChanged);
    this._lst.DoubleClick += new EventHandler(this.On_lst_DoubleClick);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._layoutPnl);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormDesignerActionEditor);
    this.ShowInTaskbar = false;
    this._layoutPnl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
