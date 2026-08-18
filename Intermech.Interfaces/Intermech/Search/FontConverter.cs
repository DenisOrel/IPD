
// Type: Intermech.Search.FontConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Search
{
    public sealed class FontConverter : System.Drawing.FontConverter
    {
      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context,
        object value,
        Attribute[] attributes)
      {
        List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
        foreach (System.ComponentModel.PropertyDescriptor property in base.GetProperties(context, value, attributes))
        {
          Attribute[] attributes1 = (Attribute[]) null;
          if (property.Name == "Name")
            attributes1 = new Attribute[2]
            {
              (Attribute) new DisplayNameAttribute("Имя"),
              (Attribute) new DescriptionAttribute("Имя шрифта")
            };
          else if (property.Name == "Size")
            attributes1 = new Attribute[2]
            {
              (Attribute) new DisplayNameAttribute("Размер"),
              (Attribute) new DescriptionAttribute("Размер шрифта")
            };
          else if (property.Name == "Bold")
            attributes1 = new Attribute[3]
            {
              (Attribute) new DisplayNameAttribute("Полужирный"),
              (Attribute) new DescriptionAttribute("Полужирное начертание шрифта"),
              (Attribute) new TypeConverterAttribute(typeof (YesNoBooleanConverter))
            };
          else if (property.Name == "Italic")
            attributes1 = new Attribute[3]
            {
              (Attribute) new DisplayNameAttribute("Курсив"),
              (Attribute) new DescriptionAttribute("Курсивное начертание шрифта"),
              (Attribute) new TypeConverterAttribute(typeof (YesNoBooleanConverter))
            };
          if (attributes1 != null)
            propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) new FontConverter.PropertyDescriptorAdapter(property, attributes1));
        }
        return new PropertyDescriptorCollection(propertyDescriptorList.ToArray()).Sort(new string[4]
        {
          "Name",
          "Size",
          "Unit",
          "Weight"
        });
      }

      public sealed class PropertyDescriptorAdapter : System.ComponentModel.PropertyDescriptor
      {
        private System.ComponentModel.PropertyDescriptor _propertyDescriptor;
        private Attribute[] _attributes;

        public PropertyDescriptorAdapter(System.ComponentModel.PropertyDescriptor propertyDescriptor, Attribute[] attributes)
          : base((MemberDescriptor) propertyDescriptor, attributes)
        {
          if (propertyDescriptor == null)
            throw new ArgumentException(nameof (propertyDescriptor));
          if (attributes == null)
            throw new ArgumentNullException(nameof (attributes));
          this._propertyDescriptor = propertyDescriptor;
          this._attributes = attributes;
        }

        public override bool CanResetValue(object component)
        {
          return this._propertyDescriptor.CanResetValue(component);
        }

        public override Type ComponentType => this._propertyDescriptor.ComponentType;

        public override object GetValue(object component)
        {
          return this._propertyDescriptor.GetValue(component);
        }

        public override bool IsReadOnly => this._propertyDescriptor.IsReadOnly;

        public override Type PropertyType => this._propertyDescriptor.PropertyType;

        public override void ResetValue(object component)
        {
          this._propertyDescriptor.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
          this._propertyDescriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
          return this._propertyDescriptor.ShouldSerializeValue(component);
        }
      }
    }
}
