// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.UnitsEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для единиц измерения.</summary>
internal class UnitsEditor : BaseDropDownEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    object obj1 = value;
    AttributeInfo attributeInfo = ((context.Instance as IWrapper).BaseClass as IAttributeEditor).AttributeInfo;
    if (attributeInfo != null && attributeInfo.AttributeGuid != Guid.Empty)
    {
      int attributeId = MetaDataHelper.GetAttributeID((object) attributeInfo.AttributeGuid);
      long num = -1;
      List<long> list = (List<long>) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributeId);
        if (attributeType != null)
        {
          if (attributeType.AttributeType == FieldTypes.ftMeasured)
          {
            num = Convert.ToInt64(attributeType.SizeType);
            if (num > 0L)
              list = new List<long>((IEnumerable<long>) new long[1]
              {
                num
              });
            else if (num == 0L)
            {
              AttributeTypeProperties propertiesStructure = attributeType.PropertiesStructure;
              if (propertiesStructure.MetadataExtensions != null)
              {
                propertiesStructure = attributeType.PropertiesStructure;
                if (propertiesStructure.MetadataExtensions.Contains((object) "MU_PHYSICAL_ID"))
                {
                  propertiesStructure = attributeType.PropertiesStructure;
                  object metadataExtension = propertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
                  if (metadataExtension != null)
                    list = new List<long>((IEnumerable<long>) (long[]) metadataExtension);
                }
              }
            }
          }
        }
      }
      List<MeasureDescriptor> measureDescriptorList;
      if (num == -1L)
      {
        measureDescriptorList = new List<MeasureDescriptor>(MeasureHelper.Measures.Length + 1);
        measureDescriptorList.AddRange((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures);
      }
      else
      {
        measureDescriptorList = new List<MeasureDescriptor>();
        if (list != null && list.Count > 0)
          measureDescriptorList = ((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures).Where<MeasureDescriptor>((Func<MeasureDescriptor, bool>) (x => list.Contains(x.PhysicalQuantityID))).Select<MeasureDescriptor, MeasureDescriptor>((Func<MeasureDescriptor, MeasureDescriptor>) (x => x)).ToList<MeasureDescriptor>();
      }
      measureDescriptorList.Insert(0, new MeasureDescriptor());
      object selValue = (object) null;
      string g = Convert.ToString(value);
      if (!string.IsNullOrEmpty(g))
      {
        try
        {
          QuickObjectInfo info = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(new Guid(g));
          selValue = info.ObjectID != -1L ? (object) measureDescriptorList.FirstOrDefault<MeasureDescriptor>((Func<MeasureDescriptor, bool>) (x => x.MeasureID == info.ObjectID)) : (object) (MeasureDescriptor) null;
        }
        catch (Exception ex)
        {
        }
      }
      int height = measureDescriptorList.Count <= 10 ? measureDescriptorList.Count * 13 + 6 : 136;
      object obj2 = this.SetEditor(provider, height, (ICollection) measureDescriptorList, selValue);
      if (obj2 != null)
      {
        obj1 = (object) string.Empty;
        if (!(obj2 as MeasureDescriptor).Empty)
        {
          QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo((obj2 as MeasureDescriptor).MeasureID);
          obj1 = !objectInfo.Empty ? (object) Convert.ToString((object) objectInfo.VersionGuid) : (object) string.Empty;
        }
      }
    }
    return obj1;
  }
}
