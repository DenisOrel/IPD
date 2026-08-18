
// Type: Intermech.PropertyEditors.AttrProcessor.DropDownEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.AttrProcessor;

public class DropDownEditorControl : ListBox, IAttributeEditorControl
{
  private bool blockOnChange;
  private bool inContainer;
  private int attributeId;
  private int? index;
  private Intermech.PropertyEditors.AttrProcessor.AttributeProcessor attributeProcessor;
  private bool wasChanged;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public DropDownEditorControl() => this.InitializeComponent();

  public int AttributeId => this.attributeId;

  public object AttributeProcessor => (object) this.attributeProcessor;

  public int? Index => this.index;

  public void InitControl(int attributeId, object attributeProcessor, int? index)
  {
    this.attributeId = attributeId;
    this.index = index;
    this.attributeProcessor = (Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) attributeProcessor;
    this.wasChanged = false;
    this.blockOnChange = false;
    this.RefreshControl();
  }

  public bool InContainer
  {
    get => this.inContainer;
    set
    {
      this.inContainer = value;
      if (this.inContainer)
        this.BorderStyle = BorderStyle.Fixed3D;
      else
        this.BorderStyle = BorderStyle.None;
    }
  }

  /// <summary>Выясняем, может ли значение атрибута быть пустым.</summary>
  /// <returns></returns>
  private bool AttributeValueCanNull()
  {
    bool flag = false;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeId);
    if (attributeType != null && (attributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
      flag = true;
    return flag;
  }

  public void RefreshControl()
  {
    this.BeginUpdate();
    try
    {
      this.Items.Clear();
      if (this.attributeProcessor.FindAttributeValues(this.attributeId) == null || !(this.attributeProcessor.GetSingleValueConverter(this.attributeId) is CommonTypeConverter singleValueConverter) || !singleValueConverter.GetStandardValuesSupported())
        return;
      ICollection standardValues = singleValueConverter.GetStandardValues();
      if (standardValues == null)
        return;
      if (this.AttributeValueCanNull())
        this.Items.Add((object) new EmptyValue());
      foreach (object obj in (IEnumerable) standardValues)
        this.Items.Add(obj);
      object obj1 = this.attributeProcessor.GetValue(this.attributeId, this.index.Value);
      this.blockOnChange = true;
      try
      {
        this.SelectedIndex = this.FindObjIndex(obj1);
      }
      finally
      {
        this.blockOnChange = false;
      }
    }
    finally
    {
      this.EndUpdate();
    }
  }

  public bool Apply()
  {
    if (this.wasChanged)
    {
      object y = (object) null;
      if (!(this.SelectedItem is EmptyValue))
        y = this.SelectedItem;
      if (!AttributeValues.ValueEquals(this.attributeProcessor.GetValue(this.attributeId, this.index.Value), y))
        this.attributeProcessor.SetValue(this.attributeId, this.index.Value, y);
      this.wasChanged = false;
    }
    return true;
  }

  public event AttributeValuesChangedHandler OnAttributeValueChanged;

  public event CloseDemandHandler OnCloseDemand;

  public bool WasChanged => this.wasChanged;

  public void Cancel()
  {
    this.wasChanged = false;
    this.RefreshControl();
  }

  public bool IsDropDownResizable => false;

  public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

  public void PaintValue(PaintValueEventArgs e)
  {
  }

  protected override void OnDrawItem(DrawItemEventArgs e)
  {
    if (e.Index == -1)
      return;
    string empty = string.Empty;
    object obj = this.Items[e.Index];
    TypeConverter singleValueConverter = this.attributeProcessor.GetSingleValueConverter(this.attributeId);
    string s = singleValueConverter == null || !singleValueConverter.CanConvertTo(typeof (string)) ? empty.ToString() : (string) singleValueConverter.ConvertTo(obj, typeof (string));
    e.DrawBackground();
    using (Brush brush = (Brush) new SolidBrush(e.ForeColor))
      e.Graphics.DrawString(s, e.Font, brush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }

  protected override void OnSelectedIndexChanged(EventArgs e)
  {
    try
    {
      if (this.blockOnChange)
        return;
      this.wasChanged = true;
      if (this.OnAttributeValueChanged == null)
        return;
      this.OnAttributeValueChanged((object) this, new AttributeValuesChangedEventArgs(this.attributeId, AttributeValuesAction.ModifyValue, (object) new object[2]
      {
        (object) this.SelectedIndex,
        this.SelectedItem
      }));
    }
    finally
    {
      base.OnSelectedIndexChanged(e);
    }
  }

  protected override void OnClick(EventArgs e)
  {
    base.OnClick(e);
    if (this.inContainer)
      return;
    this.wasChanged = true;
    this.Apply();
    if (this.OnCloseDemand == null)
      return;
    this.OnCloseDemand((object) this, new CloseControlEventArgs(false, DialogResult.OK));
  }

  protected override void OnDoubleClick(EventArgs e)
  {
    base.OnDoubleClick(e);
    if (!this.inContainer)
      return;
    this.wasChanged = true;
    this.Apply();
    this.attributeProcessor.CommitTransaction();
    Form form = this.FindForm();
    if (form == null)
      return;
    form.DialogResult = DialogResult.OK;
    form.Close();
  }

  protected override void OnKeyPress(KeyPressEventArgs e)
  {
    base.OnKeyPress(e);
    if (e.KeyChar != '\r' || this.inContainer)
      return;
    this.wasChanged = true;
    this.Apply();
    if (this.OnCloseDemand == null)
      return;
    this.OnCloseDemand((object) this, new CloseControlEventArgs(false, DialogResult.OK));
  }

  private int FindObjIndex(object obj)
  {
    int objIndex = -1;
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (this.Items[index] != null && this.Items[index].Equals(obj) || this.Items[index] == null && obj == null)
      {
        objIndex = index;
        break;
      }
    }
    return objIndex;
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
    this.SuspendLayout();
    this.DrawMode = DrawMode.OwnerDrawFixed;
    this.ResumeLayout(false);
  }
}
