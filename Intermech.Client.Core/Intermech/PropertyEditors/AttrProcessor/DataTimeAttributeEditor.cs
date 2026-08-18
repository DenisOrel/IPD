
// Type: Intermech.PropertyEditors.AttrProcessor.DataTimeAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.AttrProcessor;

public class DataTimeAttributeEditor : MonthCalendar, IAttributeEditorControl
{
  private Point m_LastClickPosition;
  private long m_LastClickTime;
  private bool m_LastClickRaisedDoubleClick;
  private int attributeId;
  private int? index;
  private Intermech.PropertyEditors.AttrProcessor.AttributeProcessor attributeProcessor;
  private bool blockOnChange;
  private bool inContainer;
  private bool wasChanged;

  public DataTimeAttributeEditor()
  {
    this.GetStyle(ControlStyles.StandardClick);
    this.GetStyle(ControlStyles.StandardDoubleClick);
    this.SetStyle(ControlStyles.StandardClick, true);
    this.SetStyle(ControlStyles.StandardDoubleClick, true);
    this.GetStyle(ControlStyles.StandardClick);
    this.GetStyle(ControlStyles.StandardDoubleClick);
    this.MaxSelectionCount = 1;
  }

  public int AttributeId => this.attributeId;

  public object AttributeProcessor => (object) this.attributeProcessor;

  public int? Index => this.index;

  public void InitControl(int attributeId, object attributeProcessor, int? index)
  {
    this.attributeId = attributeId;
    this.index = index;
    this.attributeProcessor = (Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) attributeProcessor;
    this.blockOnChange = false;
    this.RefreshControl();
    this.wasChanged = false;
  }

  public bool InContainer
  {
    get => this.inContainer;
    set => this.inContainer = value;
  }

  public void RefreshControl()
  {
    AttributeValues attributeValues = this.attributeProcessor.FindAttributeValues(this.attributeId);
    object obj = (object) null;
    if (attributeValues != null)
      obj = !this.index.HasValue ? attributeValues.Values[0] : attributeValues.Values[this.index.Value];
    if (obj == null || obj is DBNull || !(this.attributeProcessor.GetSingleValueConverter(this.attributeId) is CommonTypeConverter singleValueConverter))
      return;
    this.blockOnChange = true;
    try
    {
      if (obj is DateTime dateTime)
        this.SelectionStart = dateTime;
      else
        this.SelectionStart = (DateTime) singleValueConverter.ConvertFrom(obj);
    }
    finally
    {
      this.blockOnChange = false;
    }
  }

  public bool Apply()
  {
    if (this.wasChanged)
    {
      bool flag = false;
      AttributeValues attributeValues = this.attributeProcessor.FindAttributeValues(this.attributeId);
      if (attributeValues == null)
      {
        attributeValues = Intermech.PropertyEditors.AttrProcessor.AttributeProcessor.CreateAttributeValues(this.attributeId, this.attributeProcessor.Id, this.attributeProcessor.ElementKind);
        flag = true;
      }
      if (flag)
      {
        if (attributeValues.ReadOnly)
          return false;
        if (this.index.HasValue && this.index.Value < attributeValues.Values.Length)
          attributeValues.Values[this.index.Value] = (object) this.SelectionStart;
        else
          attributeValues.Values[0] = (object) this.SelectionStart;
        AttributeValuesList list = new AttributeValuesList();
        list.Add(attributeValues);
        this.attributeProcessor.SetAttributeValuesArray(list);
      }
      else if (!AttributeValues.ValueEquals(!this.index.HasValue ? attributeValues.Values[0] : attributeValues.Values[this.index.Value], (object) this.SelectionStart))
      {
        if (this.index.HasValue)
          this.attributeProcessor.SetValue(this.attributeId, this.index.Value, (object) this.SelectionStart);
        else
          this.attributeProcessor.SetValue(this.attributeId, (object) this.SelectionStart);
      }
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

  protected override void OnDateChanged(DateRangeEventArgs drevent)
  {
    this.wasChanged = true;
    if (!this.blockOnChange && this.OnAttributeValueChanged != null)
      this.OnAttributeValueChanged((object) this, new AttributeValuesChangedEventArgs(this.attributeId, AttributeValuesAction.ModifyValue, (object) new object[2]
      {
        (object) 0,
        (object) this.SelectionStart
      }));
    base.OnDateChanged(drevent);
  }

  protected bool IsInDoubleClickArea(Point point1, Point point2)
  {
    return Math.Abs(point1.X - point2.X) <= SystemInformation.DoubleClickSize.Width && Math.Abs(point1.Y - point2.Y) <= SystemInformation.DoubleClickSize.Height;
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left)
    {
      if (!this.m_LastClickRaisedDoubleClick && DateTime.Now.Ticks - this.m_LastClickTime <= (long) (SystemInformation.DoubleClickTime * 10000) && this.IsInDoubleClickArea(this.m_LastClickPosition, Cursor.Position))
      {
        this.OnDoubleClick(EventArgs.Empty);
        this.m_LastClickRaisedDoubleClick = true;
      }
      else
        this.m_LastClickRaisedDoubleClick = false;
      this.m_LastClickPosition = Cursor.Position;
      this.m_LastClickTime = DateTime.Now.Ticks;
    }
    base.OnMouseDown(e);
  }

  protected override void OnDoubleClick(EventArgs e) => base.OnDoubleClick(e);
}
