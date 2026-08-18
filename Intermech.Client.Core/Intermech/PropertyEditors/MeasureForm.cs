
// Type: Intermech.PropertyEditors.MeasureForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>MeasureEditorForm</summary>
public class MeasureForm : Form, IAttributeEditorControl
{
  protected Button okBtn;
  protected Button cancelBtn;
  private ComboBox measuresCB;
  private Label label1;
  private Label label2;
  protected ComboBox valueEdit;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private MeasureDescriptorComparer mdPhysComparer = new MeasureDescriptorComparer(true, true);
  private MeasureDescriptorComparer mdKComparer = new MeasureDescriptorComparer(false, true);
  private int attributeId = -1;
  private Intermech.PropertyEditors.AttrProcessor.AttributeProcessor attributeProcessor;
  private int? index;
  private bool inContainer;

  public string Caption
  {
    get => this.Text;
    set => this.Text = value;
  }

  public MeasureForm() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MeasureForm));
    this.okBtn = new Button();
    this.cancelBtn = new Button();
    this.measuresCB = new ComboBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.valueEdit = new ComboBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okBtn, "okBtn");
    this.okBtn.DialogResult = DialogResult.OK;
    this.okBtn.Name = "okBtn";
    this.okBtn.Click += new EventHandler(this.okBtn_Click);
    componentResourceManager.ApplyResources((object) this.cancelBtn, "cancelBtn");
    this.cancelBtn.DialogResult = DialogResult.Cancel;
    this.cancelBtn.Name = "cancelBtn";
    componentResourceManager.ApplyResources((object) this.measuresCB, "measuresCB");
    this.measuresCB.DropDownStyle = ComboBoxStyle.DropDownList;
    this.measuresCB.Name = "measuresCB";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.valueEdit.DrawMode = DrawMode.OwnerDrawFixed;
    this.valueEdit.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.valueEdit, "valueEdit");
    this.valueEdit.Name = "valueEdit";
    this.valueEdit.DrawItem += new DrawItemEventHandler(this.valueEdit_DrawItem);
    this.valueEdit.SelectionChangeCommitted += new EventHandler(this.valueEdit_SelectionChangeCommitted);
    this.valueEdit.DropDown += new EventHandler(this.valueEdit_DropDown);
    this.AcceptButton = (IButtonControl) this.okBtn;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.cancelBtn;
    this.Controls.Add((Control) this.valueEdit);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.measuresCB);
    this.Controls.Add((Control) this.cancelBtn);
    this.Controls.Add((Control) this.okBtn);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (MeasureForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public DialogResult ExecuteDialog(
    ref MeasuredValue aMeasureValue,
    MeasureDescriptor[] aMeasureDescriptorList)
  {
    return this.ExecuteDialog(ref aMeasureValue, aMeasureDescriptorList, (GetDefaultMeasureIDDelegate) null);
  }

  /// <summary>ExecuteDialog</summary>
  /// <param name="aMeasureValue"></param>
  /// <param name="aMeasureDescriptorList"></param>
  /// <param name="getDefaultMeasureID"></param>
  /// <returns></returns>
  public DialogResult ExecuteDialog(
    ref MeasuredValue aMeasureValue,
    MeasureDescriptor[] aMeasureDescriptorList,
    GetDefaultMeasureIDDelegate getDefaultMeasureID)
  {
    this.measuresCB.Items.Clear();
    if (aMeasureDescriptorList != null)
    {
      List<MeasureDescriptor> measureDescriptorList = new List<MeasureDescriptor>((IEnumerable<MeasureDescriptor>) aMeasureDescriptorList);
      measureDescriptorList.Sort((IComparer<MeasureDescriptor>) this.mdPhysComparer);
      this.measuresCB.Items.AddRange((object[]) measureDescriptorList.ToArray());
    }
    if (aMeasureValue != null && aMeasureValue.Caption != string.Empty)
    {
      this.valueEdit.Text = aMeasureValue.Value.ToString("#################0.#################");
      this.SetMeasureCB(aMeasureValue.MeasureID);
    }
    else
    {
      this.valueEdit.Text = string.Empty;
      long measureID = -1;
      if (getDefaultMeasureID != null)
        measureID = getDefaultMeasureID((object) this);
      this.SetMeasureCB(measureID);
    }
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    MeasureDescriptor measureCb = this.GetMeasureCB();
    aMeasureValue = MeasureHelper.ConvertToMeasuredValue($"{this.valueEdit.Text} {measureCb.ShortName}");
    return (DialogResult) num;
  }

  /// <summary>ExecuteDialog</summary>
  /// <param name="value">Текстовое значение числа</param>
  /// <param name="measureID">Идентификатор единицы измерения</param>
  /// <param name="aMeasureDescriptorList"></param>
  /// <param name="getDefaultMeasureID"></param>
  /// <returns></returns>
  public DialogResult ExecuteDialog(
    ref string value,
    ref long measureID,
    MeasureDescriptor[] aMeasureDescriptorList,
    GetDefaultMeasureIDDelegate getDefaultMeasureID)
  {
    this.measuresCB.Items.Clear();
    if (aMeasureDescriptorList != null)
      this.measuresCB.Items.AddRange((object[]) aMeasureDescriptorList);
    this.valueEdit.Text = value;
    this.SetMeasureCB(measureID);
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue($"{this.valueEdit.Text} {this.GetMeasureCB().ShortName}");
    value = this.valueEdit.Text;
    measureID = measuredValue.MeasureID;
    return (DialogResult) num;
  }

  private MeasureDescriptor GetMeasureCB() => (MeasureDescriptor) this.measuresCB.SelectedItem;

  private void SetMeasureCB(long measureID)
  {
    if (measureID == -1L)
    {
      this.measuresCB.SelectedItem = (object) null;
    }
    else
    {
      for (int index = 0; index < this.measuresCB.Items.Count; ++index)
      {
        if (((MeasureDescriptor) this.measuresCB.Items[index]).MeasureID == measureID)
        {
          this.measuresCB.SelectedItem = this.measuresCB.Items[index];
          break;
        }
      }
    }
  }

  private bool TryParse(out double d)
  {
    if (!double.TryParse(this.valueEdit.Text, out d))
    {
      if (this.valueEdit.Text.Contains("."))
        this.valueEdit.Text = this.valueEdit.Text.Replace('.', ',');
      else if (this.valueEdit.Text.Contains(","))
        this.valueEdit.Text = this.valueEdit.Text.Replace(',', '.');
    }
    return double.TryParse(this.valueEdit.Text, out d);
  }

  private void okBtn_Click(object sender, EventArgs e)
  {
    double d = 0.0;
    if (!this.TryParse(out d) || this.measuresCB.SelectedItem == null)
    {
      this.DialogResult = DialogResult.None;
    }
    else
    {
      if (this.attributeProcessor == null || !this.Apply())
        return;
      this.DialogResult = DialogResult.OK;
    }
  }

  public int AttributeId => this.attributeId;

  public object AttributeProcessor => (object) this.attributeProcessor;

  public int? Index => this.index;

  public void InitControl(int attributeId, object attributeProcessor, int? index)
  {
    this.attributeId = attributeId;
    this.attributeProcessor = attributeProcessor as Intermech.PropertyEditors.AttrProcessor.AttributeProcessor;
    this.index = index;
    this.RefreshControl();
  }

  public bool InContainer
  {
    get => this.inContainer;
    set => this.inContainer = value;
  }

  public void RefreshControl()
  {
    if (!this.attributeProcessor.Loaded)
      return;
    AttributeValues attributeValues = this.attributeProcessor.FindAttributeValues(this.attributeId);
    object obj = (object) null;
    if (attributeValues != null)
      obj = !this.index.HasValue ? attributeValues.Values[0] : attributeValues.Values[this.index.Value];
    measuredValue = (MeasuredValue) null;
    switch (obj)
    {
      case null:
      case DBNull _:
        this.measuresCB.Items.Clear();
        ArrayList listByAttributeId = MeasureEditor.GetMeasureDescriptorListByAttributeId(this.attributeId);
        if (listByAttributeId != null)
          this.measuresCB.Items.AddRange(listByAttributeId.ToArray());
        if (measuredValue != null && measuredValue.Caption != string.Empty)
        {
          this.valueEdit.Text = measuredValue.Value.ToString();
          this.SetMeasureCB(measuredValue.MeasureID);
          break;
        }
        this.valueEdit.Text = string.Empty;
        if (this.attributeProcessor.Id == -1L)
          break;
        this.SetMeasureCB(new MeasuredIdReceiver((IElementInfo) new MeasureForm.ElementInfo(this.attributeProcessor.Id, this.attributeProcessor.ElementKind), this.attributeId).GetDefaultMeasureID((object) this));
        break;
      case MeasuredValue measuredValue:
label_10:
        if (measuredValue == null && this.attributeProcessor.GetSingleValueConverter(this.attributeId) is CommonTypeConverter singleValueConverter)
        {
          measuredValue = (MeasuredValue) singleValueConverter.ConvertFrom(obj);
          goto case null;
        }
        goto case null;
      case string mValue:
label_6:
        if (mValue != null)
        {
          if (mValue != "")
          {
            try
            {
              measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue);
              goto label_10;
            }
            catch
            {
              goto label_10;
            }
          }
          else
            goto label_10;
        }
        else
          goto label_10;
      default:
        mValue = obj.ToString();
        goto label_6;
    }
  }

  public bool Apply()
  {
    bool flag = false;
    AttributeValues attributeValues = this.attributeProcessor.FindAttributeValues(this.attributeId);
    if (attributeValues == null)
    {
      attributeValues = Intermech.PropertyEditors.AttrProcessor.AttributeProcessor.CreateAttributeValues(this.attributeId, this.attributeProcessor.Id, this.attributeProcessor.ElementKind);
      flag = true;
    }
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue($"{this.valueEdit.Text} {this.GetMeasureCB().ShortName}");
    if (flag)
    {
      if (attributeValues.ReadOnly)
        return false;
      if (this.index.HasValue && this.index.Value < attributeValues.Values.Length)
        attributeValues.Values[this.index.Value] = (object) measuredValue;
      else
        attributeValues.Values[0] = (object) measuredValue;
      AttributeValuesList list = new AttributeValuesList();
      list.Add(attributeValues);
      this.attributeProcessor.SetAttributeValuesArray(list);
    }
    else if (!AttributeValues.ValueEquals(!this.index.HasValue ? attributeValues.Values[0] : attributeValues.Values[this.index.Value], (object) measuredValue))
    {
      if (this.index.HasValue)
        this.attributeProcessor.SetValue(this.attributeId, this.index.Value, (object) measuredValue);
      else
        this.attributeProcessor.SetValue(this.attributeId, (object) measuredValue);
    }
    return true;
  }

  public event AttributeValuesChangedHandler OnAttributeValueChanged;

  public event CloseDemandHandler OnCloseDemand;

  public bool WasChanged => false;

  public void Cancel()
  {
  }

  public bool IsDropDownResizable => false;

  public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

  public void PaintValue(PaintValueEventArgs e)
  {
  }

  private void valueEdit_DropDown(object sender, EventArgs e)
  {
    this.valueEdit.Items.Clear();
    if (this.measuresCB.SelectedItem == null)
      return;
    double d = 0.0;
    if (!this.TryParse(out d))
      return;
    MeasureDescriptor selectedItem = (MeasureDescriptor) this.measuresCB.SelectedItem;
    List<MeasureDescriptor> measureDescriptorList = new List<MeasureDescriptor>();
    for (int index = 0; index < this.measuresCB.Items.Count; ++index)
    {
      if (((MeasureDescriptor) this.measuresCB.Items[index]).PhysicalQuantityID == selectedItem.PhysicalQuantityID)
        measureDescriptorList.Add((MeasureDescriptor) this.measuresCB.Items[index]);
    }
    measureDescriptorList.Sort((IComparer<MeasureDescriptor>) this.mdKComparer);
    for (int index = 0; index < measureDescriptorList.Count; ++index)
    {
      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue($"{(d * selectedItem.K / measureDescriptorList[index].K).ToString()} {measureDescriptorList[index].ShortName}");
      this.valueEdit.Items.Add((object) new MeasuredValueContainer(measuredValue.Value, measuredValue.MeasureID, measuredValue.Caption));
    }
    this.valueEdit.SelectedItem = (object) null;
  }

  private void valueEdit_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (e.Index == -1)
      return;
    string s = string.Empty;
    object obj = this.valueEdit.Items[e.Index];
    if (obj is MeasuredValue)
      s = ((MeasuredValue) obj).Caption;
    e.DrawBackground();
    using (Brush brush = (Brush) new SolidBrush(e.ForeColor))
      e.Graphics.DrawString(s, e.Font, brush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }

  private void valueEdit_SelectionChangeCommitted(object sender, EventArgs e)
  {
    if (!(this.valueEdit.SelectedItem is MeasuredValue selectedItem))
      return;
    this.SetMeasureCB(selectedItem.MeasureID);
  }

  private class ElementInfo : IElementInfo
  {
    private long _id = -1;
    private AttributableElements _kind;

    public ElementInfo(long id, AttributableElements kind)
    {
      this._id = id;
      this._kind = kind;
    }

    public long ElementIdentifier => this._id;

    public AttributableElements ElementKind => this._kind;
  }
}
