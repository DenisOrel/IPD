
// Type: Intermech.PropertyEditors.ListPropDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// идет в связке с ObjectGridExpandableObjectConverter, который отвечает за раскрытие property
/// </summary>
public class ListPropDescriptor : PropDescriptor
{
  private bool isList;
  private PropertyDescriptorCollection pdcList;

  public AttributeValuesPropertyClass AttributeValuesPropertyClass
  {
    get => (AttributeValuesPropertyClass) this.GetValue((object) null);
  }

  public PropertyDescriptorCollection PdcList
  {
    get => this.pdcList;
    set => this.pdcList = value;
  }

  public static bool IsList(AttributeValues aAttributeValue)
  {
    return aAttributeValue.MultipleValued == MultiValueModes.MultiValues || aAttributeValue.MultipleValued == MultiValueModes.MultiValuesFromList;
  }

  public PropDescriptor GetPdcListItemByPropID(int lPropID)
  {
    PropDescriptor listItemByPropId = (PropDescriptor) null;
    for (int index = 0; index < this.PdcList.Count; ++index)
    {
      if (((PropDescriptor) this.PdcList[index]).PropID == lPropID)
      {
        listItemByPropId = (PropDescriptor) this.PdcList[index];
        break;
      }
    }
    return listItemByPropId;
  }

  public ListPropDescriptor(
    int propID,
    object parent,
    string name,
    object value,
    Type type,
    TypeConverter typeConverter,
    object editor,
    string category,
    string description,
    bool onlyread,
    bool browsable,
    bool reset,
    string mask)
    : this(propID, parent, name, value, type, typeConverter, editor, category, description, onlyread, browsable, reset, mask, false)
  {
  }

  public ListPropDescriptor(
    int propID,
    object parent,
    string name,
    object value,
    Type type,
    TypeConverter typeConverter,
    object editor,
    string category,
    string description,
    bool onlyread,
    bool browsable,
    bool reset,
    string mask,
    bool disableManualEdit)
    : base(propID, parent, name, value, type, typeConverter, editor, category, description, onlyread, browsable, reset, mask, disableManualEdit)
  {
    this.isList = value != null && ListPropDescriptor.IsList(((AttributeValuesPropertyClass) value).AttributeValue);
  }

  public override bool ValueChanged
  {
    get
    {
      if (base.ValueChanged)
        return true;
      bool valueChanged = false;
      if (this.pdcList != null)
      {
        for (int index = 0; index < this.pdcList.Count; ++index)
        {
          valueChanged |= ((PropDescriptor) this.pdcList[index]).ValueChanged;
          if (valueChanged)
            break;
        }
      }
      return valueChanged;
    }
  }

  public override void ResetValueChanged(object component)
  {
    base.ResetValueChanged(component);
    for (int index = 0; index < this.pdcList.Count; ++index)
      ((PropDescriptor) this.pdcList[index]).ResetValueChanged(component);
  }
}
