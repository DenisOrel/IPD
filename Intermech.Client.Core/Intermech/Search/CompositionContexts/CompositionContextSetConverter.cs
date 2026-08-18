
// Type: Intermech.Search.CompositionContexts.CompositionContextSetConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;


namespace Intermech.Search.CompositionContexts;

public sealed class CompositionContextSetConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value is CompositionContextSet)
    {
      CompositionContextSet compositionContextSet = (CompositionContextSet) value;
      if (destinationType == typeof (string))
        return (object) string.Join(", ", ((IEnumerable<CompositionContext>) compositionContextSet.CompositionContexts).Select<CompositionContext, string>((Func<CompositionContext, string>) (o => o.Description)));
    }
    return base.ConvertTo(context, culture, value, destinationType);
  }
}
