
// Type: Intermech.Checksums.Crc32Checksum
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib.Checksums;
using System.IO;


namespace Intermech.Checksums
{
    /// <summary>Калькулятор</summary>
    public class Crc32Checksum : Checksum
    {
      public Crc32Checksum() => this.checksumAlgorithm = ChecksumAlgorithm.Crc32;

      public override ChecksumClass Compute(Stream stream)
      {
        Crc32 crc32 = new Crc32();
        if (stream.Position != 0L)
          stream.Position = 0L;
        int count1 = 16384 /*0x4000*/;
        byte[] buffer = new byte[count1];
        long num = stream.Length % (long) count1 == 0L ? stream.Length / (long) count1 : stream.Length / (long) count1 + 1L;
        for (long index = 0; index < num; ++index)
        {
          int count2 = stream.Read(buffer, 0, count1);
          crc32.Update(buffer, 0, count2);
        }
        return new ChecksumClass(ChecksumAlgorithm.Crc32, (object) crc32.Value);
      }

      public override ChecksumClass Compute(byte[] data)
      {
        Crc32 crc32 = new Crc32();
        crc32.Update(data);
        return new ChecksumClass(ChecksumAlgorithm.Crc32, (object) crc32.Value);
      }
    }
}
