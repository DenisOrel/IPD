
// Type: Intermech.Interfaces.AttributeValueField
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Перечислитель позволяет указать поле атрибута, которое нужно прочитать или модифицировать
    /// </summary>
    public enum AttributeValueField
    {
      /// <summary>Целочисленное поле</summary>
      Integer,
      /// <summary>Дробное поле</summary>
      Double,
      /// <summary>Строковое поле</summary>
      String,
      /// <summary>Поле с датой</summary>
      Date,
    }
}
