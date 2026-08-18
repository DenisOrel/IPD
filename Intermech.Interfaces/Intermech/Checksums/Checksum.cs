
// Type: Intermech.Checksums.Checksum
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Checksums
{
    public class Checksum : IChecksum
    {
      protected ChecksumAlgorithm checksumAlgorithm;

      public ChecksumAlgorithm ChecksumAlgorithm => this.checksumAlgorithm;

      public virtual ChecksumClass Compute(Stream stream) => (ChecksumClass) null;

      public virtual ChecksumClass Compute(byte[] data) => (ChecksumClass) null;
    }
}
