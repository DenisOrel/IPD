// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.EnterPointDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal sealed class EnterPointDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attrID) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attrID, bool baseReadonly) => baseReadonly;

  public object GetPropDescriptorEditor(int attrID) => (object) new EnterPointEditor();

  public object GetAttributeValue(IElementInfo iElementInfo, int attrID, object propertyValue)
  {
    object attributeValue = (object) null;
    if (propertyValue != null && propertyValue is EnterPoint)
      attributeValue = (object) (propertyValue as EnterPoint).SiteCode;
    return attributeValue;
  }

  public Type GetPropDescriptorType(int attrID, FieldTypes baseType) => typeof (EnterPoint);

  public object GetPropDescriptorValue(IElementInfo iElementInfo, int attrID, object actualValue)
  {
    object propDescriptorValue;
    if (actualValue != DBNull.Value && actualValue != null && actualValue is string && ((string) actualValue).Length == 1)
    {
      SiteInfo site = ((ISitesCacheService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISitesCacheService))).GetSite(((string) actualValue)[0]);
      propDescriptorValue = site != null ? (object) new EnterPoint(site.Code.ToString(), site.Caption) : (object) new EnterPoint((string) actualValue);
    }
    else
      propDescriptorValue = actualValue;
    return propDescriptorValue;
  }

  public bool GetPropDescriptorReset(int attrID, bool baseReset) => true;

  public string GetPropDescriptorMask(int attrID, string baseMask) => baseMask;

  public TypeConverter GetConverter(int attrID, object attrProcessor) => (TypeConverter) null;
}
