
// Type: Intermech.Client.Core.FormDesigner.Controls.AttributeInfo2TypeNamesConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Преобразование AttributeInfo в заголовки типов.</summary>
public class AttributeInfo2TypeNamesConverter : TypeConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    object obj = (object) string.Empty;
    if (!(destinationType == typeof (string)) || !(value is AttributeInfo attributeInfo))
      return base.ConvertTo(context, culture, value, destinationType);
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    string empty = string.Empty;
    string str = string.Empty;
    if (attributeInfo.AttributeGuid != Guid.Empty)
    {
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(attributeInfo.AttributeGuid, false);
      if (attributeType != null)
      {
        string name = attributeType.Name;
        if (attributeInfo.TypeGuid != Guid.Empty)
        {
          IDBObjectTypeInfo objectType = service.GetObjectType(attributeInfo.TypeGuid, false);
          if (objectType != null)
          {
            str = objectType.ObjectTypeName;
          }
          else
          {
            IDBRelationTypeInfo relationType = service.GetRelationType(attributeInfo.TypeGuid, false);
            if (relationType != null)
              str = relationType.Description;
          }
        }
        obj = !string.IsNullOrEmpty(str) ? (object) $"{str}.{name}" : (object) name;
      }
    }
    return obj;
  }
}
