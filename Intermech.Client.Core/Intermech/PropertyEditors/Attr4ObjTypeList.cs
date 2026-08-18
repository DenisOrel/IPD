
// Type: Intermech.PropertyEditors.Attr4ObjTypeList
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

/// <summary>Summary description for Attr4ObjTypeList.</summary>
public class Attr4ObjTypeList : Attr4TypeList
{
  public Attr4ObjTypeList()
  {
  }

  public Attr4ObjTypeList(EventsHolder.GetListDelegate aGetMasterList)
    : base(aGetMasterList)
  {
  }

  public override int Add(object value)
  {
    if (value is Attr4ObjTypeClass)
      ((Attr4TypeClass) value).GetMasterList = this.getMasterList;
    return base.Add(value);
  }

  public override void AddRange(ICollection c)
  {
    foreach (Attr4ObjTypeClass attr4ObjTypeClass in (IEnumerable) c)
    {
      if (attr4ObjTypeClass != null)
        attr4ObjTypeClass.GetMasterList = this.getMasterList;
    }
    base.AddRange(c);
  }

  public void Assign(Attr4ObjTypeList another)
  {
    this.Clear();
    for (int index = 0; index < another.Count; ++index)
      this.Add((object) Attr4ObjTypeClass.Clone((Attr4ObjTypeClass) another[index]));
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
      this.Add((object) new Attr4ObjTypeClass(new Attribute4ObjectTypeProperties(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]), (InheritModes) Convert.ToInt32(row["F_PUBLIC"]), (RequiredModes) Convert.ToInt32(row["F_REQUIRED"]), row["F_VALIDATION_RULE"].ToString(), (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]), row["F_FORMULA"].ToString(), (UniqueValueModes) Convert.ToInt32(row["F_UNIQUE"]), Convert.ToInt32(row["F_LEVEL_ID"]), currentDateTime, (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]), Convert.ToInt32(row["F_CONTENT"]) == 1, (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]), row["F_MASK"].ToString(), Convert.ToInt32(row["F_MASTER_ID"]), Convert.ToInt32(row["F_SOURCE_ID"])), attributeType.PropertiesStructure, possibleValues, this.getMasterList));
    }
    return true;
  }

  public int IndexOfByAttributeID(int aAttributeID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (((Attr4ObjTypeClass) this[index]).Attribute4ObjectTypeProperties.AttributeID == aAttributeID)
        return index;
    }
    return -1;
  }

  public int IndexOfByAttributeName(string aAttributeName)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (((Attr4TypeClass) this[index]).AttributeName == aAttributeName)
        return index;
    }
    return -1;
  }
}
