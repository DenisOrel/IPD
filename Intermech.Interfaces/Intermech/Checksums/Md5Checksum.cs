
// Type: Intermech.Checksums.Md5Checksum
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;
using System.Security.Cryptography;


namespace Intermech.Checksums
{
    public class Md5Checksum : Checksum
    {
      public Md5Checksum() => this.checksumAlgorithm = ChecksumAlgorithm.Md5;

      public override ChecksumClass Compute(Stream stream)
      {
        using (MD5 md5 = MD5.Create())
        {
          if (stream.CanSeek && stream.Position != 0L)
            stream.Position = 0L;
          return new ChecksumClass(ChecksumAlgorithm.Md5, (object) md5.ComputeHash(stream));
        }
      }

      public override ChecksumClass Compute(byte[] data)
      {
        using (MD5 md5 = MD5.Create())
          return new ChecksumClass(ChecksumAlgorithm.Md5, (object) md5.ComputeHash(data));
      }
    }
}
