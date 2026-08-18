
// Type: Intermech.PropertyEditors.AttrProcessor.EditorControlForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Форма, в которую запихивается Control при необходимости модального показа Control в составе формы
/// </summary>
public class EditorControlForm : Form, IAttributeEditorControl
{
  private bool inContainer;
  private DockStyle safeDockStyle;
  private IAttributeEditorControl iAttributeEditorControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button btnApply;
  private Button btnCancel;
  private Button btnOk;
  public Panel holderPanel;

  public IAttributeEditorControl AttributeEditorControl => this.iAttributeEditorControl;

  public EditorControlForm() => this.InitializeComponent();

  /// <summary>Назначение контрола форме</summary>
  /// <param name="iAttributeEditorControl"></param>
  public void AssignControl(IAttributeEditorControl iAttributeEditorControl)
  {
    if (this.iAttributeEditorControl != null)
    {
      ((Control) this.iAttributeEditorControl).Parent = (Control) null;
      ((Control) this.iAttributeEditorControl).Dock = this.safeDockStyle;
      this.iAttributeEditorControl.InContainer = false;
      this.iAttributeEditorControl.OnAttributeValueChanged -= new AttributeValuesChangedHandler(this.iAttributeEditorControl_AttributeValuesChanged);
    }
    this.iAttributeEditorControl = iAttributeEditorControl;
    if (this.iAttributeEditorControl == null || !(this.iAttributeEditorControl is Control))
      return;
    this.safeDockStyle = ((Control) this.iAttributeEditorControl).Dock;
    ((Control) this.iAttributeEditorControl).Parent = (Control) this.holderPanel;
    ((Control) this.iAttributeEditorControl).Dock = DockStyle.Fill;
    this.iAttributeEditorControl.InContainer = true;
    this.iAttributeEditorControl.OnAttributeValueChanged += new AttributeValuesChangedHandler(this.iAttributeEditorControl_AttributeValuesChanged);
  }

  private void iAttributeEditorControl_AttributeValuesChanged(
    object sender,
    AttributeValuesChangedEventArgs e)
  {
    this.UpdateControls();
    if (this.OnAttributeValueChanged == null)
      return;
    this.OnAttributeValueChanged((object) this, e);
  }

  public int AttributeId
  {
    get => this.iAttributeEditorControl == null ? 0 : this.iAttributeEditorControl.AttributeId;
  }

  public object AttributeProcessor
  {
    get
    {
      return this.iAttributeEditorControl == null ? (object) null : this.iAttributeEditorControl.AttributeProcessor;
    }
  }

  public int? Index
  {
    get => this.iAttributeEditorControl == null ? new int?() : this.iAttributeEditorControl.Index;
  }

  public void InitControl(int attributeId, object attributeProcessor, int? index)
  {
    if (this.iAttributeEditorControl == null)
      throw new Exception(AttributeProcessorConsts.msgControlNotInitialized);
    this.iAttributeEditorControl.InitControl(attributeId, attributeProcessor, index);
  }

  public bool InContainer
  {
    get => this.inContainer;
    set => this.inContainer = value;
  }

  public void RefreshControl()
  {
    if (this.iAttributeEditorControl == null)
      throw new Exception(AttributeProcessorConsts.msgControlNotInitialized);
    this.iAttributeEditorControl.RefreshControl();
  }

  public bool Apply()
  {
    return this.iAttributeEditorControl != null ? this.iAttributeEditorControl.Apply() : throw new Exception(AttributeProcessorConsts.msgControlNotInitialized);
  }

  public bool WasChanged
  {
    get
    {
      return this.iAttributeEditorControl != null ? this.iAttributeEditorControl.WasChanged : throw new Exception(AttributeProcessorConsts.msgControlNotInitialized);
    }
  }

  public void Cancel()
  {
    if (this.iAttributeEditorControl == null)
      throw new Exception(AttributeProcessorConsts.msgControlNotInitialized);
    this.iAttributeEditorControl.Cancel();
  }

  public event AttributeValuesChangedHandler OnAttributeValueChanged;

  public event CloseDemandHandler OnCloseDemand;

  public bool IsDropDownResizable => false;

  public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

  public void PaintValue(PaintValueEventArgs e)
  {
  }

  private void btnOk_Click(object sender, EventArgs e) => this.ApplyCommand();

  private void ApplyCommand()
  {
    if (!this.Apply())
      return;
    ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).CommitTransaction();
    this.DialogResult = DialogResult.OK;
  }

  private void btnCancel_Click(object sender, EventArgs e) => this.CancelCommand();

  private void CancelCommand()
  {
    this.Cancel();
    ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).RollbackTransaction();
    this.DialogResult = DialogResult.Cancel;
  }

  private void btnApply_Click(object sender, EventArgs e)
  {
    if (this.Apply())
    {
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).CommitTransaction();
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).StartTransaction();
    }
    this.UpdateControls();
  }

  private void EditorControlForm_Load(object sender, EventArgs e)
  {
    ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).StartTransaction();
    this.UpdateControls();
  }

  private void UpdateControls()
  {
    this.btnOk.Enabled = this.iAttributeEditorControl != null && this.iAttributeEditorControl.WasChanged;
    this.btnApply.Enabled = this.iAttributeEditorControl != null && this.iAttributeEditorControl.WasChanged;
  }

  private void EditorControlForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason == CloseReason.None || this.iAttributeEditorControl == null)
      return;
    if (this.iAttributeEditorControl.WasChanged)
    {
      switch (MessageBox.Show(MessageDialogs.msgNeedSave, MessageDialogs.msgConfirmSave, MessageBoxButtons.YesNoCancel))
      {
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
        case DialogResult.Yes:
          if (!this.iAttributeEditorControl.Apply())
          {
            e.Cancel = true;
            break;
          }
          ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).CommitTransaction();
          break;
        case DialogResult.No:
          this.iAttributeEditorControl.Cancel();
          ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).RollbackTransaction();
          break;
      }
    }
    else
    {
      if (!((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).InTransaction)
        return;
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).RollbackTransaction();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditorControlForm));
    this.panel1 = new Panel();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.holderPanel = new Panel();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnApply);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOk);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.holderPanel, "holderPanel");
    this.holderPanel.Name = "holderPanel";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.holderPanel);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditorControlForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.EditorControlForm_Load);
    this.FormClosing += new FormClosingEventHandler(this.EditorControlForm_FormClosing);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
