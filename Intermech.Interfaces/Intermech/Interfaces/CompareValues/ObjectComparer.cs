
// Type: Intermech.Interfaces.CompareValues.ObjectComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.CompareValues
{
    internal sealed class ObjectComparer : Comparer<object>
    {
      protected override object ConvertTo(object value) => value;

      protected override bool OnCompare(object value1, object value2)
      {
        return value1 == value2 || base.OnCompare(value1, value2);
      }
    }
}
