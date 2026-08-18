
// Type: Intermech.Search.LifecycleStepLinkConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search
{
    public sealed class LifecycleStepLinkConverter : TypeConverter
    {
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (!(value is int) || !(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep((int) value);
        return lcStep == null ? (object) null : (object) lcStep.Name;
      }
    }
}
