
// Type: Intermech.PropertyEditors.MeasureEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for MeasureEditor.</summary>
public class MeasureEditor : UITypeEditor
{
  private EventsHolder.GetListDelegate getMeasureDescriptorList;
  private GetDefaultMeasureIDDelegate getDefaultMeasureID;
  private int attributeId;

  public MeasureEditor(
    EventsHolder.GetListDelegate aGetMeasureDescriptorList,
    GetDefaultMeasureIDDelegate aGetDefaultMeasureID)
  {
    this.getMeasureDescriptorList = aGetMeasureDescriptorList;
    this.getDefaultMeasureID = aGetDefaultMeasureID;
  }

  public MeasureEditor(int aAttributeId, GetDefaultMeasureIDDelegate aGetDefaultMeasureID)
  {
    this.attributeId = aAttributeId;
    this.getDefaultMeasureID = aGetDefaultMeasureID;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    string mValue = value == null ? string.Empty : value.ToString();
    MeasureForm measureForm = new MeasureForm();
    MeasuredValue aMeasureValue = (MeasuredValue) null;
    try
    {
      aMeasureValue = MeasureHelper.ConvertToMeasuredValue(mValue);
    }
    catch
    {
    }
    ArrayList arrayList = this.getMeasureDescriptorList == null ? MeasureEditor.GetMeasureDescriptorListByAttributeId(this.attributeId) : this.getMeasureDescriptorList((object) this);
    MeasureDescriptor[] aMeasureDescriptorList = (MeasureDescriptor[]) null;
    if (arrayList != null)
      aMeasureDescriptorList = (MeasureDescriptor[]) arrayList.ToArray(typeof (MeasureDescriptor));
    return measureForm.ExecuteDialog(ref aMeasureValue, aMeasureDescriptorList, this.getDefaultMeasureID) == DialogResult.OK ? (object) MeasureHelper.ConvertToString(aMeasureValue.Value, aMeasureValue.MeasureID, false) : value;
  }

  public static ArrayList GetMeasureDescriptorListByAttributeId(int attrId)
  {
    Guid empty = Guid.Empty;
    ArrayList listByAttributeId = (ArrayList) null;
    long num = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrId);
      if (attributeType != null && attributeType.AttributeType == FieldTypes.ftMeasured)
        num = Convert.ToInt64(attributeType.SizeType);
      Guid guid = (attributeType as IDBGuid).GUID;
      if (num == -1L)
      {
        listByAttributeId = !guid.Equals(new Guid("cad00267-306c-11d8-b4e9-00304f19f545")) ? new ArrayList((ICollection) MeasureHelper.Measures) : MeasureEditor.CollectCountMeasureDescriptors();
      }
      else
      {
        long[] collection;
        if (num <= 0L)
          collection = (long[]) attributeType.PropertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
        else
          collection = new long[1]{ num };
        List<long> longList = new List<long>((IEnumerable<long>) collection);
        listByAttributeId = new ArrayList();
        for (int index = 0; index < MeasureHelper.Measures.Length; ++index)
        {
          if (longList.IndexOf(MeasureHelper.Measures[index].PhysicalQuantityID) != -1)
            listByAttributeId.Add((object) MeasureHelper.Measures[index]);
        }
      }
    }
    return listByAttributeId;
  }

  public static ArrayList CollectCountMeasureDescriptors()
  {
    return new ArrayList((ICollection) MeasureHelper.AsQuantityPhysMeasureDescriptors);
  }
}
