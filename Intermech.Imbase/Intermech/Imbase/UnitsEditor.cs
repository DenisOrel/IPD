// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.UnitsEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase;

internal class UnitsEditor : DropDownEditor
{
  private IWindowsFormsEditorService svc;

  private void ListBoxClick(object sender, EventArgs e) => this.svc.CloseDropDown();

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    if (!(context.Instance is StructureEditorPropGridDescriptor instance))
      return value;
    long num1 = -1;
    List<long> longList = (List<long>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(instance.AttributeID);
      if (attributeType != null)
      {
        if (attributeType.AttributeType == FieldTypes.ftMeasured)
        {
          num1 = Convert.ToInt64(attributeType.SizeType);
          if (num1 > 0L)
            longList = new List<long>((IEnumerable<long>) new long[1]
            {
              num1
            });
          else if (num1 == 0L)
          {
            if (attributeType.PropertiesStructure.MetadataExtensions != null)
            {
              if (attributeType.PropertiesStructure.MetadataExtensions.Contains((object) "MU_PHYSICAL_ID"))
              {
                object metadataExtension = attributeType.PropertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
                if (metadataExtension != null)
                  longList = new List<long>((IEnumerable<long>) (long[]) metadataExtension);
              }
            }
          }
        }
      }
    }
    List<MeasureDescriptor> measureDescriptorList;
    if (num1 == -1L)
    {
      measureDescriptorList = new List<MeasureDescriptor>(MeasureHelper.Measures.Length + 1);
      measureDescriptorList.AddRange((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures);
      measureDescriptorList.Insert(0, new MeasureDescriptor());
    }
    else
    {
      measureDescriptorList = new List<MeasureDescriptor>();
      measureDescriptorList.Add(new MeasureDescriptor());
      if (longList != null && longList.Count > 0)
      {
        for (int index = 0; index < MeasureHelper.Measures.Length; ++index)
        {
          if (longList.IndexOf(MeasureHelper.Measures[index].PhysicalQuantityID) != -1)
            measureDescriptorList.Add(MeasureHelper.Measures[index]);
        }
      }
    }
    listBox.Items.AddRange((object[]) measureDescriptorList.ToArray());
    listBox.Height = listBox.Items.Count <= 10 ? listBox.Items.Count * 13 + 6 : 136;
    if (value != null && value != DBNull.Value && !string.IsNullOrEmpty(value.ToString()))
    {
      long num2 = -1;
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          num2 = sessionKeeper.Session.GetObjectInfo(new Guid(value.ToString())).ObjectID;
      }
      catch (Exception ex)
      {
      }
      if (num2 != -1L)
      {
        foreach (object obj in measureDescriptorList)
        {
          if ((obj as MeasureDescriptor).MeasureID == num2)
          {
            listBox.SelectedItem = obj;
            break;
          }
        }
      }
    }
    listBox.Click += new EventHandler(this.ListBoxClick);
    this.svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    if (listBox.SelectedItem == null)
      return (object) Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((listBox.SelectedItem as MeasureDescriptor).Empty)
        return (object) Guid.Empty;
      IDBObject dbObject = sessionKeeper.Session.GetObject((listBox.SelectedItem as MeasureDescriptor).MeasureID);
      return dbObject == null ? (object) null : (object) dbObject.ObjectGUID.ToString();
    }
  }
}
