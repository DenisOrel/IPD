// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.SpecificationSectionDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal class SpecificationSectionDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null || !(propertyValue is SPSectionInfo) ? (object) null : (object) (propertyValue as SPSectionInfo).SectionNumber;
  }

  public object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new SpecificationSectionEditor();
  }

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType) => typeof (SPSectionInfo);

  public object GetPropDescriptorValue(
    IElementInfo iElementInfo,
    int attributeId,
    object actualValue)
  {
    if (actualValue == null || string.IsNullOrEmpty(actualValue.ToString()))
      return (object) null;
    int result = -1;
    if (!int.TryParse(actualValue.ToString(), out result))
      return (object) new SPSectionInfo(actualValue.ToString(), "-1");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad00254-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(sessionKeeper.Session.GetAttributeType(new Guid("cad00279-306c-11d8-b4e9-00304f19f545")).AttributeID, RelationalOperators.Equal, (object) result, LogicalOperators.NONE, 0, false)
      }, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      })
      {
        Contents = new ColumnContents[2]
        {
          ColumnContents.ID,
          ColumnContents.Text
        }
      });
      return dataTable.Rows.Count == 0 ? (object) null : (object) new SPSectionInfo(dataTable.Rows[0][1].ToString(), actualValue.ToString());
    }
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
