
// Type: Intermech.Interfaces.IStreamSerializable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс позволяет сериализовать/десериализовать реализующий его класс
    /// </summary>
    public interface IStreamSerializable
    {
      /// <summary>Сериализовать класс в поток</summary>
      /// <param name="packMode">Степень сжатия</param>
      /// <returns>Сериализованный класс</returns>
      MemoryStream SerializeToStream(ZLibCompressLevels packMode);

      /// <summary>Десериализовать класс из указанного потока</summary>
      /// <param name="stream">Поток</param>
      /// <returns>true - десериализация выполнена успешно</returns>
      bool DeserializeFromStream(Stream stream);
    }
}
