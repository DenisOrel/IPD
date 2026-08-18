
// Type: Intermech.Interfaces.CompareValues.Comparer`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.CompareValues
{
    internal abstract class Comparer<T>
    {
      public bool Compare(object value1, object value2)
      {
        value1 = CompareValuesHelper.NormalizedValue(value1);
        value2 = CompareValuesHelper.NormalizedValue(value2);
        if (value1 == null && value2 != null || value2 == null && value1 != null)
          return false;
        return value1 == null && value2 == null || this.OnCompare(this.ConvertTo(value1), this.ConvertTo(value2));
      }

      protected virtual bool OnCompare(T value1, T value2)
      {
        return object.Equals((object) value1, (object) value2);
      }

      protected abstract T ConvertTo(object value);
    }
}
