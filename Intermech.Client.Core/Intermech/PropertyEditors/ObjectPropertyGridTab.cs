
// Type: Intermech.PropertyEditors.ObjectPropertyGridTab
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// Абстрактный PropertyTab для работы с ObjectPropertyGrid
/// </summary>
public abstract class ObjectPropertyGridTab : PropertyTab, IObjectPropertyGridTab
{
  public abstract GetAttributeValuesModes TabAttributeValuesModes { get; }

  public virtual void InitTab(GetAttributeValuesModes avm)
  {
  }

  public abstract Guid TabGuid { get; }

  public PropertyDescriptorCollection PropDescriptorCollection(object component)
  {
    return component is IObjectPropDescriptorHolder ? this.GetProperties(component) : (PropertyDescriptorCollection) null;
  }

  public override PropertyDescriptorCollection GetProperties(
    object component,
    Attribute[] attributes)
  {
    return this.GetProperties((ITypeDescriptorContext) null, component, attributes);
  }

  public override PropertyDescriptorCollection GetProperties(object component)
  {
    return this.GetProperties(component, (Attribute[]) null);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object component,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = (PropertyDescriptorCollection) null;
    if (component is IObjectPropDescriptorHolder)
      properties = ((IObjectPropDescriptorHolder) component).ExtendPropDescriptorCollectionbyMode((object) this, this.TabAttributeValuesModes | ClientConsts.GetAttributeValuesModesMinimum, true);
    else if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor.Converter != null)
      properties = context.PropertyDescriptor.Converter.GetProperties(context, component, attributes);
    return properties;
  }

  public override bool CanExtend(object extendee) => base.CanExtend(extendee);
}
