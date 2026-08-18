// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.ReportValuesFormatter
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using System;
using System.Globalization;

#nullable disable
namespace Intermech.Document.Client.Report;

public class ReportValuesFormatter
{
  public static string Format(FieldTypes fieldType, string formatString, object fieldValue)
  {
    if (fieldValue == null)
      return string.Empty;
    try
    {
      switch (fieldType)
      {
        case FieldTypes.ftInteger:
        case FieldTypes.ftAutoInc:
          if (!(fieldValue is long num1))
            num1 = Convert.ToInt64(fieldValue.ToString());
          long num2 = num1;
          return formatString == string.Empty ? num2.ToString((IFormatProvider) CultureInfo.CurrentCulture) : num2.ToString(formatString, (IFormatProvider) CultureInfo.CurrentCulture);
        case FieldTypes.ftDouble:
          if (!(fieldValue is double num3))
            num3 = Convert.ToDouble(fieldValue.ToString());
          double num4 = num3;
          return formatString == string.Empty ? num4.ToString((IFormatProvider) CultureInfo.CurrentCulture) : num4.ToString(formatString, (IFormatProvider) CultureInfo.CurrentCulture);
        case FieldTypes.ftDateTime:
          if (!(fieldValue is DateTime dateTime1))
            dateTime1 = Convert.ToDateTime(fieldValue.ToString());
          DateTime dateTime2 = dateTime1;
          return formatString == string.Empty ? dateTime2.ToString((IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.ToString(formatString, (IFormatProvider) CultureInfo.CurrentCulture);
        case FieldTypes.ftBoolean:
          bool flag1 = fieldValue is bool flag2 ? flag2 : Convert.ToBoolean(fieldValue.ToString());
          if (formatString == string.Empty)
            return flag1 ? Consts.YesValue : Consts.NoValue;
          string[] strArray1 = formatString.Split(';');
          return flag1 ? strArray1[0] : strArray1[1];
        case FieldTypes.ftMeasured:
          MeasuredValue measuredValue = fieldValue is MeasuredValue ? (MeasuredValue) fieldValue : MeasureHelper.ConvertToMeasuredValue(fieldValue.ToString());
          if (formatString == string.Empty)
            return measuredValue.Caption;
          string[] strArray2 = formatString.Split(';');
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
          string str = measuredValue.Value.ToString(strArray2[0], (IFormatProvider) CultureInfo.CurrentCulture) + " ";
          if (strArray2[1] == "L")
            str += descriptor.LongName;
          else if (strArray2[1] == "S")
            str += descriptor.ShortName;
          return str;
        default:
          return fieldValue.ToString();
      }
    }
    catch
    {
      return Convert.ToString(fieldValue, (IFormatProvider) CultureInfo.CurrentCulture);
    }
  }
}
