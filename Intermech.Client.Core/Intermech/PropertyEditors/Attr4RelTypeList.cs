
// Type: Intermech.PropertyEditors.Attr4RelTypeList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Data;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for Attr4RelTypeList.</summary>
public class Attr4RelTypeList : Attr4TypeList
{
  public Attr4RelTypeList()
  {
  }

  public Attr4RelTypeList(EventsHolder.GetListDelegate aGetMasterList)
    : base(aGetMasterList)
  {
  }

  public override int Add(object value)
  {
    if (value is Attr4RelTypeClass)
      ((Attr4TypeClass) value).GetMasterList = this.getMasterList;
    return base.Add(value);
  }

  public override void AddRange(ICollection c)
  {
    foreach (Attr4RelTypeClass attr4RelTypeClass in (IEnumerable) c)
    {
      if (attr4RelTypeClass != null)
        attr4RelTypeClass.GetMasterList = this.getMasterList;
    }
    base.AddRange(c);
  }

  public void Assign(Attr4RelTypeList another)
  {
    this.Clear();
    for (int index = 0; index < another.Count; ++index)
      this.Add((object) Attr4RelTypeClass.Clone((Attr4RelTypeClass) another[index]));
  }

  public bool Fill(IDBCollection aiDBCollection)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    this.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) aiDBCollection.Select("").Rows)
    {
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      DataTable possibleValues = attributeType.GetPossibleValues();
      object currentDateTime = row["F_DEFAULT_VALUE"];
      if (attributeType.AttributeType == FieldTypes.ftDateTime && currentDateTime != null && currentDateTime is string)
        currentDateTime = DateTimeCultureConverter.ConvertUniversalDateTimeStringToCurrentDateTime(currentDateTime.ToString());
      if (attributeType.AttributeType == FieldTypes.ftDouble && currentDateTime != null && currentDateTime is string && currentDateTime.ToString() != string.Empty)
        currentDateTime = (object) Convert.ToDouble(currentDateTime.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      this.Add((object) new Attr4RelTypeClass(new Attribute4RelationTypeProperties(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_RELATION_TYPE"]), (RequiredModes) Convert.ToInt32(row["F_REQUIRED"]), row["F_VALIDATION_RULE"].ToString(), (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]), row["F_FORMULA"].ToString(), currentDateTime, (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]), Convert.ToInt32(row["F_CONTENT"]) == 1, (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]), row["F_MASK"].ToString(), Convert.ToInt32(row["F_MASTER_ID"]), Convert.ToInt32(row["F_SOURCE_ID"])), attributeType.PropertiesStructure, possibleValues, this.getMasterList));
    }
    return true;
  }

  public int IndexOfByAttributeID(int aAttributeID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (((Attr4RelTypeClass) this[index]).Attribute4RelationTypeProperties.AttributeID == aAttributeID)
        return index;
    }
    return -1;
  }

  public int IndexOfByAttributeName(string aAttributeName)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (((Attr4RelTypeClass) this[index]).AttributeTypeProperties.Name == aAttributeName)
        return index;
    }
    return -1;
  }
}
