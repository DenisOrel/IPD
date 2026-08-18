
// Type: Intermech.Checksums.IChecksum
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Checksums
{
    public interface IChecksum
    {
      /// <summary>Тип контрольной суммы</summary>
      ChecksumAlgorithm ChecksumAlgorithm { get; }

      /// <summary>Вычислить контрольную сумму для потока</summary>
      /// <param name="stream"></param>
      /// <returns></returns>
      ChecksumClass Compute(Stream stream);

      /// <summary>Вычислить контрольную сумму для массива данных</summary>
      /// <param name="data"></param>
      /// <returns></returns>
      ChecksumClass Compute(byte[] data);
    }
}
