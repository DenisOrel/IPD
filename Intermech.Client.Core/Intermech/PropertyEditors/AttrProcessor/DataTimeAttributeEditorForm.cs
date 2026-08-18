
// Type: Intermech.PropertyEditors.AttrProcessor.DataTimeAttributeEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.AttrProcessor;

public class DataTimeAttributeEditorForm : Form, IAttributeEditorControl
{
  private bool inContainer;
  private DockStyle safeDockStyle;
  private IAttributeEditorControl iAttributeEditorControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DataTimeAttributeEditor monthCalendar;

  public IAttributeEditorControl AttributeEditorControl => this.iAttributeEditorControl;

  public DataTimeAttributeEditorForm()
  {
    this.InitializeComponent();
    this.monthCalendar = new DataTimeAttributeEditor();
    this.monthCalendar.Location = new Point(0, 0);
    this.monthCalendar.Name = nameof (monthCalendar);
    this.monthCalendar.TabIndex = 0;
    this.Controls.Add((Control) this.monthCalendar);
    this.AssignControl((IAttributeEditorControl) this.monthCalendar);
  }

  /// <summary>Назначение контрола форме</summary>
  /// <param name="iAttributeEditorControl"></param>
  public void AssignControl(IAttributeEditorControl iAttributeEditorControl)
  {
    if (this.iAttributeEditorControl != null)
    {
      Control attributeEditorControl = (Control) this.iAttributeEditorControl;
      attributeEditorControl.Parent = (Control) null;
      attributeEditorControl.Dock = this.safeDockStyle;
      attributeEditorControl.MouseUp -= new MouseEventHandler(this.editorControl_MouseUp);
      this.iAttributeEditorControl.InContainer = false;
      this.iAttributeEditorControl.OnAttributeValueChanged -= new AttributeValuesChangedHandler(this.iAttributeEditorControl_AttributeValuesChanged);
    }
    this.iAttributeEditorControl = iAttributeEditorControl;
    if (this.iAttributeEditorControl == null || !(this.iAttributeEditorControl is Control))
      return;
    Control attributeEditorControl1 = (Control) this.iAttributeEditorControl;
    this.safeDockStyle = attributeEditorControl1.Dock;
    attributeEditorControl1.Parent = (Control) this;
    attributeEditorControl1.Dock = DockStyle.Fill;
    attributeEditorControl1.MouseUp += new MouseEventHandler(this.editorControl_MouseUp);
    this.iAttributeEditorControl.InContainer = true;
    this.iAttributeEditorControl.OnAttributeValueChanged += new AttributeValuesChangedHandler(this.iAttributeEditorControl_AttributeValuesChanged);
  }

  private void editorControl_MouseUp(object sender, MouseEventArgs e)
  {
    if (this.monthCalendar.HitTest(new Point(e.X, e.Y)).HitArea != MonthCalendar.HitArea.Date)
      return;
    if (this.iAttributeEditorControl.Apply() && ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).InTransaction)
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).CommitTransaction();
    this.DialogResult = DialogResult.OK;
    this.Close();
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

  private void ApplyCommand()
  {
    if (!this.Apply())
      return;
    if (((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).InTransaction)
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).CommitTransaction();
    this.DialogResult = DialogResult.OK;
  }

  private void CancelCommand()
  {
    this.Cancel();
    if (((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).InTransaction)
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).RollbackTransaction();
    this.DialogResult = DialogResult.Cancel;
  }

  private void EditorControlForm_Load(object sender, EventArgs e)
  {
    ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).StartTransaction();
    this.UpdateControls();
  }

  private void UpdateControls()
  {
  }

  private void EditorControlForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.iAttributeEditorControl == null)
      return;
    if (this.iAttributeEditorControl.WasChanged)
    {
      this.CancelCommand();
    }
    else
    {
      if (!((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).InTransaction)
        return;
      ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).RollbackTransaction();
    }
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData == Keys.Return)
    {
      if (this.iAttributeEditorControl.WasChanged && this.iAttributeEditorControl.Apply())
        ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).CommitTransaction();
      else if (((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).InTransaction)
        ((Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor).RollbackTransaction();
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    return base.ProcessCmdKey(ref msg, keyData);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DataTimeAttributeEditorForm));
    this.SuspendLayout();
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Icon = (Icon) null;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DataTimeAttributeEditorForm);
    this.FormClosing += new FormClosingEventHandler(this.EditorControlForm_FormClosing);
    this.Load += new EventHandler(this.EditorControlForm_Load);
    this.ResumeLayout(false);
  }
}
