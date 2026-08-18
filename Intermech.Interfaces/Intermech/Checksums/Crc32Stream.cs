
// Type: Intermech.Checksums.Crc32Stream
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Checksums
{
    /// <summary>
    /// http://www.codeproject.com/Articles/11912/CrcStream-stream-checksum-calculator
    /// Encapsulates a <see cref="T:System.IO.Stream" /> to calculate the CRC32 checksum on-the-fly as data passes through.
    /// </summary>
    public class Crc32Stream : Stream
    {
      private Stream stream;
      private static uint[] table = Crc32Stream.GenerateTable();
      private uint readCrc = uint.MaxValue;
      private uint writeCrc = uint.MaxValue;

      /// <summary>
      /// Encapsulate a <see cref="T:System.IO.Stream" />.
      /// </summary>
      /// <param name="stream">The stream to calculate the checksum for.</param>
      public Crc32Stream(Stream stream) => this.stream = stream;

      /// <summary>Gets the underlying stream.</summary>
      public Stream Stream => this.stream;

      public override bool CanRead => this.stream.CanRead;

      public override bool CanSeek => this.stream.CanSeek;

      public override bool CanWrite => this.stream.CanWrite;

      public override void Flush() => this.stream.Flush();

      public override long Length => this.stream.Length;

      public override long Position
      {
        get => this.stream.Position;
        set => this.stream.Position = value;
      }

      public override long Seek(long offset, SeekOrigin origin) => this.stream.Seek(offset, origin);

      public override void SetLength(long value) => this.stream.SetLength(value);

      public override int Read(byte[] buffer, int offset, int count)
      {
        count = this.stream.Read(buffer, offset, count);
        this.readCrc = this.CalculateCrc(this.readCrc, buffer, offset, count);
        return count;
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
        this.stream.Write(buffer, offset, count);
        this.writeCrc = this.CalculateCrc(this.writeCrc, buffer, offset, count);
      }

      private uint CalculateCrc(uint crc, byte[] buffer, int offset, int count)
      {
        int index1 = offset;
        for (int index2 = offset + count; index1 < index2; ++index1)
          crc = crc >> 8 ^ Crc32Stream.table[((int) crc ^ (int) buffer[index1]) & (int) byte.MaxValue];
        return crc;
      }

      private static uint[] GenerateTable()
      {
        uint[] table = new uint[256 /*0x0100*/];
        for (uint index1 = 0; (long) index1 < (long) table.Length; ++index1)
        {
          uint num = index1;
          for (int index2 = 8; index2 > 0; --index2)
          {
            if (((int) num & 1) == 1)
              num = num >> 1 ^ 3988292384U;
            else
              num >>= 1;
          }
          table[(int) index1] = num;
        }
        return table;
      }

      /// <summary>
      /// Gets the CRC checksum of the data that was read by the stream thus far.
      /// </summary>
      public uint ReadCrc => this.readCrc ^ uint.MaxValue;

      /// <summary>
      /// Gets the CRC checksum of the data that was written to the stream thus far.
      /// </summary>
      public uint WriteCrc => this.writeCrc ^ uint.MaxValue;

      /// <summary>Resets the read and write checksums.</summary>
      public void ResetChecksum()
      {
        this.readCrc = uint.MaxValue;
        this.writeCrc = uint.MaxValue;
      }
    }
}
