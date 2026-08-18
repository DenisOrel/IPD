
// Type: Intermech.PropertyEditors.SomethingListEditor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

public class SomethingListEditor<T> : UITypeEditor where T : ObjectListPropertyClass, new()
{
  private IWindowsFormsEditorService edSvc;
  private CheckedListBox clb;
  private bool blockOnCheck;
  protected EventsHolder.GetListDelegate getObjList;
  protected ArrayList objList;

  public SomethingListEditor()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aGetObjList">событие возвращает ArrayList of Int64 или ArrayList of object[]{ int, string,string } для создания ObjectPropertyClass</param>
  public SomethingListEditor(EventsHolder.GetListDelegate aGetObjList)
  {
    this.getObjList = aGetObjList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aObjList">ArrayList of Int64 или ArrayList of object[]{ int, string,string } для создания ObjectPropertyClass</param>
  public SomethingListEditor(ArrayList aObjList) => this.objList = aObjList;

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context != null && context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (value == null || !(value is T))
      return value;
    this.edSvc = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    if (this.edSvc == null)
      return value;
    List<long> objectIdList = ((T) value).ObjectIDList;
    this.clb = new CheckedListBox();
    this.clb.BorderStyle = BorderStyle.None;
    this.clb.CheckOnClick = true;
    this.FillCLB(this.clb);
    this.SetClb(objectIdList);
    this.clb.ItemCheck += new ItemCheckEventHandler(this.clb_ItemCheck);
    this.edSvc.DropDownControl((Control) this.clb);
    List<ObjectPropertyClass> clb = this.GetClb();
    if (clb == null)
      return value;
    T obj = new T();
    obj.ObjectPropertyClassList = clb;
    return (object) obj;
  }

  private void FillCLB(CheckedListBox aCLB)
  {
    aCLB.Items.Clear();
    ArrayList arrayList = (ArrayList) null;
    if (this.objList != null)
      arrayList = this.objList;
    if (this.getObjList != null)
      arrayList = this.getObjList((object) this);
    if (arrayList == null)
      return;
    for (int index = 0; index < arrayList.Count; ++index)
    {
      if (arrayList[index] is long)
        aCLB.Items.Add((object) new ObjectPropertyClass((long) arrayList[index]));
      if (arrayList[index] is object[])
        aCLB.Items.Add((object) new ObjectPropertyClass((long) ((object[]) arrayList[index])[0], (string) ((object[]) arrayList[index])[1], (string) ((object[]) arrayList[index])[2]));
    }
  }

  private void SetClb(List<long> list)
  {
    this.blockOnCheck = true;
    try
    {
      list.IndexOf(-1L);
      if (list.IndexOf(-1L) != -1)
      {
        for (int index = 0; index < this.clb.Items.Count; ++index)
          this.clb.SetItemChecked(index, true);
      }
      else
      {
        for (int index = 0; index < this.clb.Items.Count; ++index)
          this.clb.SetItemChecked(index, false);
        for (int index = 0; index < list.Count; ++index)
          this.SetClbItemCheckedByID(list[index]);
      }
    }
    finally
    {
      this.blockOnCheck = false;
    }
  }

  private void SetClbItemCheckedByID(long li)
  {
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (((ObjectPropertyClass) this.clb.Items[index]).ObjectID == li)
      {
        this.clb.SetItemChecked(index, true);
        break;
      }
    }
  }

  private List<ObjectPropertyClass> GetClb()
  {
    List<ObjectPropertyClass> clb = new List<ObjectPropertyClass>();
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (((ObjectPropertyClass) this.clb.Items[index]).ObjectID == -1L && this.clb.GetItemChecked(index))
      {
        clb.Add((ObjectPropertyClass) this.clb.Items[index]);
        break;
      }
    }
    if (clb.Count == 0)
    {
      ObjectPropertyClass objectPropertyClass = (ObjectPropertyClass) null;
      for (int index = 0; index < this.clb.Items.Count; ++index)
      {
        if (((ObjectPropertyClass) this.clb.Items[index]).ObjectID == -1L)
          objectPropertyClass = (ObjectPropertyClass) this.clb.Items[index];
        if (this.clb.GetItemChecked(index))
          clb.Add((ObjectPropertyClass) this.clb.Items[index]);
      }
      if (clb.Count == 0)
        clb.Add(objectPropertyClass);
    }
    return clb;
  }

  private bool GetClbItemCheckedByID(long li)
  {
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (((ObjectPropertyClass) this.clb.Items[index]).ObjectID == li)
        return this.clb.GetItemChecked(index);
    }
    return false;
  }

  private void clb_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this.blockOnCheck)
      return;
    long objectId = ((ObjectPropertyClass) this.clb.Items[e.Index]).ObjectID;
    if (objectId == -1L)
    {
      bool flag = e.NewValue == CheckState.Checked;
      this.blockOnCheck = true;
      try
      {
        for (int index = 1; index < this.clb.Items.Count; ++index)
          this.clb.SetItemChecked(index, flag);
      }
      finally
      {
        this.blockOnCheck = false;
      }
    }
    else
    {
      if (objectId == -1L || e.NewValue != CheckState.Unchecked)
        return;
      this.blockOnCheck = true;
      try
      {
        this.clb.SetItemChecked(0, false);
      }
      finally
      {
        this.blockOnCheck = false;
      }
    }
  }
}
