
// Type: Intermech.Search.UI.PropertyGrid.PropertyDescriptorGridItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;


namespace Intermech.Search.UI.PropertyGrid;

public sealed class PropertyDescriptorGridItem : GridItem
{
  private Color? _backColor;

  public PropertyDescriptorGridItem(System.ComponentModel.PropertyDescriptor propertyDescriptor, object component)
  {
    this.PropertyDescriptor = propertyDescriptor != null ? propertyDescriptor : throw new ArgumentNullException(nameof (propertyDescriptor));
    this.Component = component;
  }

  public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; private set; }

  public object Component { get; private set; }

  public override string Label => this.PropertyDescriptor.DisplayName;

  public override object Value
  {
    get
    {
      try
      {
        object obj = this.PropertyDescriptor.GetValue(this.Component);
        if (obj != null)
        {
          TypeConverter typeConverter = this.PropertyDescriptor.Converter;
          if (typeConverter == null && Attribute.GetCustomAttribute((MemberInfo) obj.GetType(), typeof (TypeConverterAttribute)) is TypeConverterAttribute customAttribute)
          {
            Type type = Type.GetType(customAttribute.ConverterTypeName);
            if (((IEnumerable<ConstructorInfo>) type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)).Where<ConstructorInfo>((Func<ConstructorInfo, bool>) (o =>
            {
              ParameterInfo[] parameters = o.GetParameters();
              return parameters.Length == 1 && parameters[0].ParameterType == typeof (Type);
            })).Count<ConstructorInfo>() == 1)
            {
              typeConverter = Activator.CreateInstance(type, (object) obj.GetType()) as TypeConverter;
            }
            else
            {
              try
              {
                typeConverter = Activator.CreateInstance(type) as TypeConverter;
              }
              catch
              {
              }
            }
          }
          if (typeConverter != null && typeConverter.CanConvertTo(typeof (string)))
            return typeConverter.ConvertTo(obj, typeof (string));
        }
        return obj;
      }
      catch (Exception ex)
      {
        return (object) ex.Message;
      }
    }
  }

  public override List<GridItem> Children
  {
    get
    {
      try
      {
        object component = this.PropertyDescriptor.GetValue(this.Component);
        List<GridItem> children = new List<GridItem>();
        if (component == null || !(Attribute.GetCustomAttribute((MemberInfo) component.GetType(), typeof (TypeConverterAttribute)) is TypeConverterAttribute customAttribute))
          return children;
        TypeConverter typeConverter = (TypeConverter) null;
        try
        {
          typeConverter = Activator.CreateInstance(Type.GetType(customAttribute.ConverterTypeName)) as TypeConverter;
        }
        catch
        {
        }
        if (typeConverter == null || !typeConverter.GetPropertiesSupported())
          return children;
        foreach (System.ComponentModel.PropertyDescriptor property in typeConverter.GetProperties(component))
        {
          PropertyDescriptorGridItem descriptorGridItem = new PropertyDescriptorGridItem(property, component);
          children.Add((GridItem) descriptorGridItem);
        }
        return children;
      }
      catch
      {
        return new List<GridItem>(0);
      }
    }
  }

  public override Color BackColor
  {
    get
    {
      if (!this._backColor.HasValue)
        this._backColor = new Color?(this.GetBackColor());
      return this._backColor.Value;
    }
  }

  private Color GetBackColor()
  {
    object editor = this.PropertyDescriptor.GetEditor(typeof (UITypeEditor));
    return editor is IStyleProvider ? ((IStyleProvider) editor).GetBackColor(this.PropertyDescriptor, this.Component) : base.BackColor;
  }
}
