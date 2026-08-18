
// Type: Intermech.PropertyEditors.ObjectGridExpandableObjectConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Data;


namespace Intermech.PropertyEditors;

/// <summary>
/// TypeConverter для раскрытия property описываемого ListPropDescriptor
/// </summary>
public class ObjectGridExpandableObjectConverter : ExpandableObjectConverter
{
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    if (!(value is BugFixObject) && !(context.PropertyDescriptor is ListPropDescriptor))
      return base.GetProperties(context, value, attributes);
    object aObjectHolder = value is BugFixObject ? ((BugFixObject) value).Args[0] : context.Instance;
    ListPropDescriptor listPropDescriptor = value is BugFixObject ? (ListPropDescriptor) ((BugFixObject) value).Args[1] : (ListPropDescriptor) context.PropertyDescriptor;
    PropertyDescriptorCollection properties = listPropDescriptor.PdcList;
    if (properties == null)
    {
      properties = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
      listPropDescriptor.PdcList = properties;
      AttributeValuesPropertyClass valuesPropertyClass = listPropDescriptor.AttributeValuesPropertyClass;
      int id = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      Type type = (Type) null;
      TypeConverter typeConverter = (TypeConverter) null;
      object editor = (object) null;
      bool ro = true;
      bool reset = false;
      bool disableManualEdit = false;
      string empty4 = string.Empty;
      if (AttributeValuesEditor.GetPDAttributes(aObjectHolder, valuesPropertyClass.AttributeValue, ref id, ref empty1, ref empty2, ref empty3, ref type, ref typeConverter, ref editor, ref ro, ref reset, ref empty4, ref disableManualEdit))
      {
        IElementInfo component = listPropDescriptor.Component as IElementInfo;
        DataTable possibleValues = (DataTable) null;
        if (MultiValueModesHelper.IsValuedFromList(valuesPropertyClass.AttributeValue.MultipleValued))
          possibleValues = ClientCommons.GetPossibleValues(valuesPropertyClass.AttributeValue.AttributeID);
        for (int index = 0; index < valuesPropertyClass.AttributeValue.Values.Length; ++index)
        {
          object pdValue = AttributeValuesEditor.GetPDValue(valuesPropertyClass.AttributeValue, index, component.ElementIdentifier, component.ElementKind, empty4, possibleValues);
          properties.Add((PropertyDescriptor) new SimplePropDescriptor(index, listPropDescriptor.Component, $"[{index.ToString(ClientConsts.MultiValueEnumerateFormat)}]", pdValue, type, typeConverter, editor, empty3, empty2, ro, true, reset, empty4, disableManualEdit, (AttributeValuesPropertyClass) null)
          {
            ParentListPropDescriptor = listPropDescriptor
          });
        }
      }
    }
    return properties;
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    return base.GetPropertiesSupported(context);
  }
}
