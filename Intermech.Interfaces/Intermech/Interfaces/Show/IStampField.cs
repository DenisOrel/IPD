
// Type: Intermech.Interfaces.Show.IStampField
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Show
{
    /// <summary>поле штампа</summary>
    public interface IStampField
    {
      /// <summary>имя поля</summary>
      string Name { get; }

      /// <summary> значение поля</summary>
      string Value { get; }
    }
}
