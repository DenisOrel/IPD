
// Type: Intermech.Interfaces.CompareValues.MeasureComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.CompareValues
{
    internal sealed class MeasureComparer : Comparer<MeasuredValue>
    {
      protected override MeasuredValue ConvertTo(object value)
      {
        if (value is MeasuredValue)
          return (MeasuredValue) value;
        try
        {
          return MeasureHelper.ConvertToMeasuredValue(Convert.ToString(value));
        }
        catch (KernelExceptionID ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          throw new Exception($"Не удалось привести \"{value}\" к типу MeasuredValue: {ex.Message}", ex);
        }
      }

      protected override bool OnCompare(MeasuredValue value1, MeasuredValue value2)
      {
        return MeasureHelper.Compare(value1, value2) == CompareResult.Equal;
      }
    }
}
