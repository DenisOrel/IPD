
// Type: Intermech.Interfaces.Sets.ICodedStringFrom
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Sets
{
    /// <summary>
    /// Интерфейс, позволяющий хранить содержимое класса в закодированной строке
    /// </summary>
    public interface ICodedStringFrom
    {
      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      void FromString(string val);

      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      /// <param name="withLimits">true - в строке хранится граница множеств</param>
      void FromString(string val, bool withLimits);

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      string ToString();

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <param name="withLimits">true - записывать в строку границы</param>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      string ToString(bool withLimits);
    }
}
