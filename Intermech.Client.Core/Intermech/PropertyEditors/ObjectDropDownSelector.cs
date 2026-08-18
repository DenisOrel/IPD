
// Type: Intermech.PropertyEditors.ObjectDropDownSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

/// <summary>Выбор из списка выпадающих объектов</summary>
public class ObjectDropDownSelector : UITypeEditor
{
  private ObjectPropertyClass selectedOPC;
  private ListBox lb;
  private IWindowsFormsEditorService edSvc;
  private EventsHolder.GetListDelegate getIdList;
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  /// <summary>инициализация списка объектов через событие</summary>
  public ObjectDropDownSelector(
    EventsHolder.GetListDelegate aGetIdList,
    bool _objectVersionProcessed = true)
  {
    this.getIdList = aGetIdList;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context != null && context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (this.getIdList != null)
    {
      ArrayList al = new ArrayList();
      ArrayList arrayList = this.getIdList((object) this, (object) typeof (ObjectPropertyClass));
      if (arrayList != null)
      {
        for (int index = 0; index < arrayList.Count; ++index)
        {
          object obj = (object) null;
          if (arrayList[index] is long)
            obj = (object) new ObjectPropertyClass((long) arrayList[index], this.objectVersionProcessed);
          else if (arrayList[index] is object[] && arrayList[index] is object[])
            obj = ((object[]) arrayList[index]).Length > 2 ? (object) new ObjectPropertyClass((long) ((object[]) arrayList[index])[0], (string) ((object[]) arrayList[index])[1], (string) ((object[]) arrayList[index])[2], this.objectVersionProcessed) : (object) new ObjectPropertyClass((long) ((object[]) arrayList[index])[0], (string) ((object[]) arrayList[index])[1], this.objectVersionProcessed);
          if (obj != null)
            al.Add(obj);
        }
      }
      if (this.lb == null)
      {
        this.lb = new ListBox();
        this.lb.BorderStyle = BorderStyle.None;
        this.lb.SelectedIndexChanged += new EventHandler(this.lb_SelectedIndexChanged);
      }
      this.FillCB(this.lb, al);
      this.edSvc = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
      try
      {
        this.selectedOPC = (ObjectPropertyClass) null;
        this.edSvc.DropDownControl((Control) this.lb);
        if (this.selectedOPC != null)
        {
          value = (object) this.selectedOPC;
          if (value is ObjectPropertyClass)
          {
            if (((ObjectPropertyClass) value).NullObject)
              value = (object) null;
          }
        }
      }
      finally
      {
        this.edSvc = (IWindowsFormsEditorService) null;
      }
    }
    return value;
  }

  private void FillCB(ListBox alb, ArrayList al)
  {
    alb.Items.Clear();
    alb.Items.AddRange((object[]) al.ToArray(typeof (ObjectPropertyClass)));
  }

  private void lb_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lb.SelectedIndex == -1)
      return;
    this.selectedOPC = (ObjectPropertyClass) this.lb.Items[this.lb.SelectedIndex];
    this.edSvc.CloseDropDown();
  }
}
