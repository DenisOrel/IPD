
// Type: Intermech.Checksums.GostChecksum
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;
using System.IO;


namespace Intermech.Checksums
{
    public class GostChecksum : Checksum
    {
      public GostChecksum()
        : this(ChecksumAlgorithm.Gost3411_2012_256)
      {
      }

      public GostChecksum(ChecksumAlgorithm ca)
      {
        this.checksumAlgorithm = ca == ChecksumAlgorithm.Gost3411_2012_256 || ca == ChecksumAlgorithm.Gost3411_2012_512 ? ca : throw new KernelException("Указан неверный тип алгоритма");
      }

      public override ChecksumClass Compute(Stream stream)
      {
        IHash ihash = this.GetIHash();
        stream.Position = 0L;
        Stream a_stream = stream;
        return new ChecksumClass(this.checksumAlgorithm, (object) ihash.ComputeStream(a_stream).GetBytes());
      }

      public override ChecksumClass Compute(byte[] data)
      {
        return new ChecksumClass(this.checksumAlgorithm, (object) this.GetIHash().ComputeBytes(data).GetBytes());
      }

      private IHash GetIHash()
      {
        return this.checksumAlgorithm != ChecksumAlgorithm.Gost3411_2012_256 ? HashFactory.Crypto.CreateGOST3411_2012_512() : HashFactory.Crypto.CreateGOST3411_2012_256();
      }
    }
}
