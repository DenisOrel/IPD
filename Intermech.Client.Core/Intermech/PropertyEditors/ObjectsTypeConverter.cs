
// Type: Intermech.PropertyEditors.ObjectsTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>Конвертор типа для объектов, а не для типов объектов.</summary>
public class ObjectsTypeConverter : DropDownTypeConverter
{
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getDataList;
  private ArrayList list;
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  public ObjectsTypeConverter(
    IPossibleValuesHolder aIPossibleValuesHolder,
    bool _objectVersionProcessed = true)
    : base((EventsHolder.GetListDelegate) null, true)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  public ObjectsTypeConverter(
    IPossibleValuesHolder aIPossibleValuesHolder,
    bool valCanNull,
    bool _objectVersionProcessed = true)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  public ObjectsTypeConverter(
    EventsHolder.GetListDelegate aGetDataList,
    bool _objectVersionProcessed = true)
    : base((EventsHolder.GetListDelegate) null, true)
  {
    this.getDataList = aGetDataList;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  public ObjectsTypeConverter(
    EventsHolder.GetListDelegate aGetDataList,
    bool valCanNull,
    bool _objectVersionProcessed = true)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.getDataList = aGetDataList;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) ? this.list != null : base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value.GetType() == typeof (string) ? (object) this.ConvertToObjectPropertyClass(value.ToString()) : base.ConvertFrom(context, culture, value);
  }

  private ObjectPropertyClass ConvertToObjectPropertyClass(string aValue)
  {
    for (int index = 0; index < this.list.Count; ++index)
    {
      if (aValue == this.list[index].ToString())
        return (ObjectPropertyClass) this.list[index];
    }
    return (ObjectPropertyClass) null;
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    this.list = (ArrayList) null;
    if (this.iPossibleValuesHolder != null)
    {
      DataTable possibleValues = this.iPossibleValuesHolder.GetPossibleValues(context);
      if (possibleValues == null)
        return (ArrayList) null;
      this.list = new ArrayList();
      if (this.valueCanNull)
        this.list.Insert(0, (object) new ObjectPropertyClass(0L, this.objectVersionProcessed)
        {
          NullObject = true
        });
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      {
        try
        {
          this.list.Add((object) new ObjectPropertyClass(Convert.ToInt64(row["F_INTEGER_VALUE"]), this.objectVersionProcessed));
        }
        catch
        {
        }
      }
    }
    if (this.getDataList != null)
    {
      this.list = new ArrayList();
      ArrayList arrayList = this.getDataList((object) this, (object) typeof (ObjectPropertyClass));
      if (arrayList != null)
      {
        for (int index = 0; index < arrayList.Count; ++index)
        {
          object obj = (object) null;
          if (arrayList[index] is long)
            obj = (object) new ObjectPropertyClass((long) arrayList[index], this.objectVersionProcessed);
          else if (arrayList[index] is object[])
            obj = ((object[]) arrayList[index]).Length > 2 ? (object) new ObjectPropertyClass((long) ((object[]) arrayList[index])[0], (string) ((object[]) arrayList[index])[1], (string) ((object[]) arrayList[index])[2], this.objectVersionProcessed) : (object) new ObjectPropertyClass((long) ((object[]) arrayList[index])[0], (string) ((object[]) arrayList[index])[1], this.objectVersionProcessed);
          if (obj != null)
            this.list.Add(obj);
        }
      }
    }
    return this.list;
  }
}
