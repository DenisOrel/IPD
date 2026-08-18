
// Type: Intermech.Client.Core.MultiValueEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Редактор значения атрибутом, которые могут содержать множество значений с функциями добавления/удаления/замены части значений
/// </summary>
internal class MultiValueEditorForm : Form
{
  private readonly int _attrType;
  private readonly string _mask;
  private IAttributeEditor _editorControl;
  private IAttributeEditor _replaceEditorControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlBottom;
  private Panel _pnlButton;
  private Button _btnCancel;
  private Button _btnOK;
  private TabControl tabControl;
  private TabPage tabSetValues;
  private TabPage tabAddValues;
  private TabPage tabDelValues;
  private TabPage tabReplaceValue;
  private SplitContainer splitContainer;
  private Panel panelReplaceEditor;
  private Panel panel8;
  private Label label4;
  private Panel panelForTabPage;
  private Panel panelEditor;
  private Panel panelForLabel;
  private Label labelHelpSet;

  public MultiValueEditorForm(int attrType, AttributeValues values, string mask)
  {
    this._attrType = attrType;
    this.Values = values;
    this._mask = mask;
    this.ReplaceValues = (AttributeValues) null;
    this.InitializeComponent();
    this.InitializeCustomComponent();
  }

  private void InitializeCustomComponent()
  {
    if (this.DesignMode)
      return;
    this.tabSetValues.Tag = (object) MultiValueEditorMode.SetValue;
    this.tabAddValues.Tag = (object) MultiValueEditorMode.AddValue;
    this.tabDelValues.Tag = (object) MultiValueEditorMode.DelValue;
    this.tabReplaceValue.Tag = (object) MultiValueEditorMode.ReplaceValue;
  }

  /// <summary>Выбранный режим изменения значений атрибута</summary>
  internal MultiValueEditorMode EditorMode { get; private set; }

  /// <summary>
  /// Выбранные значения в редакторе. В зависимости от режима редактирования имеют разное назначение
  /// </summary>
  internal AttributeValues Values { get; private set; }

  /// <summary>
  /// Выбранные значения в редакторе нового значения для режима Замены значения
  /// </summary>
  internal AttributeValues ReplaceValues { get; private set; }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._attrType);
    this.Text += attributeType.Name;
    System.Type componentType = ComponentTypeProducer.GetComponentType(attributeType.MultiValueMode, attributeType.FieldType, this._mask != string.Empty);
    this._editorControl = this.CreateEditorControl(componentType, attributeType, (Control) this.panelEditor);
    this._replaceEditorControl = this.CreateEditorControl(componentType, attributeType, (Control) this.panelReplaceEditor);
    this.EditorMode = MultiValueEditorMode.SetValue;
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      return;
    try
    {
      if (this._editorControl is IDataFormatError editorControl && editorControl.IsDataFormatError)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Client.Core.FormDesigner.DataFormatError"), LocalizationHolder.rm.GetString("Client.Core_1149"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        e.Cancel = true;
      }
      else
      {
        string text = (string) null;
        if (this.EditorMode == MultiValueEditorMode.SetValue)
        {
          if (!e.Cancel && this._editorControl.Values.Values[0] == DBNull.Value)
            text = LocalizationHolder.rm.GetString("Attributes_ChangeValues_NullValue");
        }
        else if (this._editorControl.Values.Values[0] == DBNull.Value || this.EditorMode == MultiValueEditorMode.ReplaceValue && this._replaceEditorControl.Values.Values[0] == DBNull.Value)
          text = "На форме есть незаполненные элементы управления. Прервать выполнение?";
        else if (this.EditorMode == MultiValueEditorMode.ReplaceValue && this._replaceEditorControl.Values.Values.Length > 1)
          text = "Замена будет выполнена на первое значение из списка. Продолжить?";
        if (string.IsNullOrEmpty(text))
          return;
        string caption = LocalizationHolder.rm.GetString("Client.Core_971");
        if (MessageBox.Show((IWin32Window) this, text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
          return;
        e.Cancel = true;
      }
    }
    finally
    {
      if (!e.Cancel)
      {
        this.Values = this._editorControl?.Values;
        if (this.tabControl.SelectedTab == this.tabReplaceValue && this._replaceEditorControl != null)
          this.ReplaceValues = this._replaceEditorControl?.Values;
        base.OnClosing(e);
      }
    }
  }

  /// <summary>Создать и настроить контрол редактора</summary>
  /// <param name="editorControlType"></param>
  /// <param name="dbAttributeType"></param>
  /// <param name="parentControl"></param>
  /// <returns></returns>
  private IAttributeEditor CreateEditorControl(
    System.Type editorControlType,
    IMSAttributeType attributeType,
    Control parentControl)
  {
    IAttributeEditor instance = editorControlType != (System.Type) null ? Activator.CreateInstance(editorControlType) as IAttributeEditor : (IAttributeEditor) null;
    if (instance == null)
      return (IAttributeEditor) null;
    if (instance is ILockModify lockModify)
      lockModify.LockModify = true;
    try
    {
      if (!(instance is Control control))
        return instance;
      control.Dock = DockStyle.Fill;
      instance.Values = new AttributeValues(this.Values.AttributeID, this.Values.AttributeType, this.Values.MultipleValued, new object[0]);
      instance.AttributeInfo = new AttributeInfo(attributeType.AttributeGuid, Guid.Empty);
      if (instance is AttrMaskedTextEdit attrMaskedTextEdit)
        attrMaskedTextEdit.Mask = this._mask;
      parentControl.Controls.Add(control);
    }
    finally
    {
      if (lockModify != null)
        lockModify.LockModify = false;
    }
    return instance;
  }

  private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
  {
    if (this._editorControl != null && !AttributeValues.ValuesEquals(this._editorControl.Values.Values, this.Values.Values))
    {
      if (DialogResult.Cancel == MessageBox.Show("Данные не сохранены. Отменить изменения?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
      {
        e.Cancel = true;
        return;
      }
      this._editorControl.Values = this._replaceEditorControl.Values = new AttributeValues(this.Values.AttributeID, this.Values.AttributeType, this.Values.MultipleValued, new object[0]);
    }
    this.panelForTabPage.Parent?.Controls.Clear();
    MultiValueEditorMode result;
    if (e.TabPage.Tag == null || !Enum.TryParse<MultiValueEditorMode>(e.TabPage.Tag.ToString(), out result))
      return;
    this.EditorMode = result;
    this.labelHelpSet.Text = this.EditorMode.GetDescription<MultiValueEditorMode>();
  }

  private void TabControl_Selected(object sender, TabControlEventArgs e)
  {
    if (this.tabControl.SelectedTab != this.tabReplaceValue)
      e.TabPage.Controls.Add((Control) this.panelForTabPage);
    else
      this.splitContainer.Panel1.Controls.Add((Control) this.panelForTabPage);
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
    this._pnlBottom = new Panel();
    this._pnlButton = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this.tabControl = new TabControl();
    this.tabSetValues = new TabPage();
    this.panelForTabPage = new Panel();
    this.panelEditor = new Panel();
    this.panelForLabel = new Panel();
    this.labelHelpSet = new Label();
    this.tabAddValues = new TabPage();
    this.tabDelValues = new TabPage();
    this.tabReplaceValue = new TabPage();
    this.splitContainer = new SplitContainer();
    this.panelReplaceEditor = new Panel();
    this.panel8 = new Panel();
    this.label4 = new Label();
    this._pnlBottom.SuspendLayout();
    this._pnlButton.SuspendLayout();
    this.tabControl.SuspendLayout();
    this.tabSetValues.SuspendLayout();
    this.panelForTabPage.SuspendLayout();
    this.panelForLabel.SuspendLayout();
    this.tabReplaceValue.SuspendLayout();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.panel8.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._pnlButton);
    this._pnlBottom.Dock = DockStyle.Bottom;
    this._pnlBottom.Location = new Point(0, 222);
    this._pnlBottom.Name = "_pnlBottom";
    this._pnlBottom.Size = new Size(434, 40);
    this._pnlBottom.TabIndex = 2;
    this._pnlButton.Controls.Add((Control) this._btnCancel);
    this._pnlButton.Controls.Add((Control) this._btnOK);
    this._pnlButton.Dock = DockStyle.Right;
    this._pnlButton.Location = new Point(160 /*0xA0*/, 0);
    this._pnlButton.Name = "_pnlButton";
    this._pnlButton.Size = new Size(274, 40);
    this._pnlButton.TabIndex = 0;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.FlatStyle = FlatStyle.System;
    this._btnCancel.ImeMode = ImeMode.NoControl;
    this._btnCancel.Location = new Point(141, 8);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(121, 27);
    this._btnCancel.TabIndex = 1;
    this._btnCancel.Text = "Отмена";
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.FlatStyle = FlatStyle.System;
    this._btnOK.ImeMode = ImeMode.NoControl;
    this._btnOK.Location = new Point(14, 8);
    this._btnOK.Name = "_btnOK";
    this._btnOK.Size = new Size(121, 27);
    this._btnOK.TabIndex = 0;
    this._btnOK.Text = "OK";
    this.tabControl.Controls.Add((Control) this.tabSetValues);
    this.tabControl.Controls.Add((Control) this.tabAddValues);
    this.tabControl.Controls.Add((Control) this.tabDelValues);
    this.tabControl.Controls.Add((Control) this.tabReplaceValue);
    this.tabControl.Dock = DockStyle.Fill;
    this.tabControl.Location = new Point(0, 0);
    this.tabControl.Name = "tabControl";
    this.tabControl.SelectedIndex = 0;
    this.tabControl.Size = new Size(434, 222);
    this.tabControl.TabIndex = 3;
    this.tabControl.Selecting += new TabControlCancelEventHandler(this.TabControl_Selecting);
    this.tabControl.Selected += new TabControlEventHandler(this.TabControl_Selected);
    this.tabSetValues.Controls.Add((Control) this.panelForTabPage);
    this.tabSetValues.Location = new Point(4, 22);
    this.tabSetValues.Name = "tabSetValues";
    this.tabSetValues.Padding = new Padding(3);
    this.tabSetValues.Size = new Size(426, 196);
    this.tabSetValues.TabIndex = 0;
    this.tabSetValues.Text = "Задать";
    this.tabSetValues.UseVisualStyleBackColor = true;
    this.panelForTabPage.Controls.Add((Control) this.panelEditor);
    this.panelForTabPage.Controls.Add((Control) this.panelForLabel);
    this.panelForTabPage.Dock = DockStyle.Fill;
    this.panelForTabPage.Location = new Point(3, 3);
    this.panelForTabPage.Name = "panelForTabPage";
    this.panelForTabPage.Size = new Size(420, 190);
    this.panelForTabPage.TabIndex = 1;
    this.panelEditor.Dock = DockStyle.Fill;
    this.panelEditor.Location = new Point(0, 25);
    this.panelEditor.Name = "panelEditor";
    this.panelEditor.Size = new Size(420, 165);
    this.panelEditor.TabIndex = 3;
    this.panelForLabel.BackColor = Color.Transparent;
    this.panelForLabel.Controls.Add((Control) this.labelHelpSet);
    this.panelForLabel.Dock = DockStyle.Top;
    this.panelForLabel.Location = new Point(0, 0);
    this.panelForLabel.Name = "panelForLabel";
    this.panelForLabel.Size = new Size(420, 25);
    this.panelForLabel.TabIndex = 2;
    this.labelHelpSet.Dock = DockStyle.Fill;
    this.labelHelpSet.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.labelHelpSet.Location = new Point(0, 0);
    this.labelHelpSet.Name = "labelHelpSet";
    this.labelHelpSet.Size = new Size(420, 25);
    this.labelHelpSet.TabIndex = 0;
    this.labelHelpSet.Text = "Задать новые значения";
    this.labelHelpSet.TextAlign = ContentAlignment.MiddleCenter;
    this.tabAddValues.Location = new Point(4, 22);
    this.tabAddValues.Name = "tabAddValues";
    this.tabAddValues.Padding = new Padding(3);
    this.tabAddValues.Size = new Size(426, 196);
    this.tabAddValues.TabIndex = 1;
    this.tabAddValues.Text = "Добавить";
    this.tabAddValues.UseVisualStyleBackColor = true;
    this.tabDelValues.Location = new Point(4, 22);
    this.tabDelValues.Name = "tabDelValues";
    this.tabDelValues.Padding = new Padding(3);
    this.tabDelValues.Size = new Size(426, 196);
    this.tabDelValues.TabIndex = 2;
    this.tabDelValues.Text = "Удалить";
    this.tabDelValues.UseVisualStyleBackColor = true;
    this.tabReplaceValue.Controls.Add((Control) this.splitContainer);
    this.tabReplaceValue.Location = new Point(4, 22);
    this.tabReplaceValue.Name = "tabReplaceValue";
    this.tabReplaceValue.Padding = new Padding(3);
    this.tabReplaceValue.Size = new Size(426, 196);
    this.tabReplaceValue.TabIndex = 3;
    this.tabReplaceValue.Text = "Заменить";
    this.tabReplaceValue.UseVisualStyleBackColor = true;
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.Location = new Point(3, 3);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel2.BackgroundImageLayout = ImageLayout.None;
    this.splitContainer.Panel2.Controls.Add((Control) this.panelReplaceEditor);
    this.splitContainer.Panel2.Controls.Add((Control) this.panel8);
    this.splitContainer.Size = new Size(420, 190);
    this.splitContainer.SplitterDistance = 205;
    this.splitContainer.TabIndex = 1;
    this.panelReplaceEditor.Dock = DockStyle.Fill;
    this.panelReplaceEditor.Location = new Point(0, 25);
    this.panelReplaceEditor.Name = "panelReplaceEditor";
    this.panelReplaceEditor.Size = new Size(211, 165);
    this.panelReplaceEditor.TabIndex = 3;
    this.panel8.BackColor = Color.Transparent;
    this.panel8.Controls.Add((Control) this.label4);
    this.panel8.Dock = DockStyle.Top;
    this.panel8.Location = new Point(0, 0);
    this.panel8.Name = "panel8";
    this.panel8.Size = new Size(211, 25);
    this.panel8.TabIndex = 2;
    this.label4.Dock = DockStyle.Fill;
    this.label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label4.Location = new Point(0, 0);
    this.label4.Name = "label4";
    this.label4.Size = new Size(211, 25);
    this.label4.TabIndex = 0;
    this.label4.Text = "Новое значение";
    this.label4.TextAlign = ContentAlignment.MiddleCenter;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(434, 262);
    this.Controls.Add((Control) this.tabControl);
    this.Controls.Add((Control) this._pnlBottom);
    this.MinimumSize = new Size(450, 300);
    this.Name = nameof (MultiValueEditorForm);
    this.Text = "Значения атрибута: ";
    this._pnlBottom.ResumeLayout(false);
    this._pnlButton.ResumeLayout(false);
    this.tabControl.ResumeLayout(false);
    this.tabSetValues.ResumeLayout(false);
    this.panelForTabPage.ResumeLayout(false);
    this.panelForLabel.ResumeLayout(false);
    this.tabReplaceValue.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.panel8.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
