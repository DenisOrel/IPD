
// Type: Intermech.Actions.ActionConverter
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;


namespace Intermech.Actions
{
    public class ActionConverter : TypeConverter
    {
      private ActionList _actionList;

      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
      }

      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        if (!(value is string))
          return base.ConvertFrom(context, culture, value);
        try
        {
          if ((string) value == "" || (string) value == "(none)")
            return (object) this._actionList.Actions.Null;
          IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
          return service != null ? service.GetReference((string) value) : base.ConvertFrom(context, culture, value);
        }
        catch
        {
          throw new ArgumentException($"Can not convert '{(string) value}' to type Object");
        }
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (destinationType == typeof (string) && context != null)
        {
          IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
          if (service != null && value is Action reference)
          {
            this._actionList = reference.Parent;
            return reference == this._actionList.Actions.Null ? (object) "(none)" : (object) service.GetName((object) reference);
          }
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }

      public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

      public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

      public override TypeConverter.StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext context)
      {
        ArrayList values = new ArrayList();
        values.Add((object) this._actionList.Actions.Null);
        foreach (object action in this._actionList.Actions)
          values.Add(action);
        return new TypeConverter.StandardValuesCollection((ICollection) values);
      }

      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context,
        object value,
        Attribute[] attributes)
      {
        return TypeDescriptor.GetProperties(value, attributes);
      }

      public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
    }
}
